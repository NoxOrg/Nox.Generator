using Nox.Abstractions;
using Nox.Secrets.Abstractions;
using Nox.Solution;

namespace Nox.Secrets.Hashicorp;

public class HashicorpSecretsResolver: ISecretsResolver
{
    private readonly IPersistedSecretStore _store;
    private readonly SecretsServer _secretsServer;
    private readonly string _storePrefix;

    public HashicorpSecretsResolver(IPersistedSecretStore store, SecretsServer secretsServer, string? storePrefix = null)
    {
        if (secretsServer.Provider != SecretsServerProvider.HashicorpVault) throw new NoxSecretsException("Hashicorp secrets resolver can only be instantiated if provider is set to HashicorpVault");
        _store = store;
        _storePrefix = "";
        if (!string.IsNullOrWhiteSpace(storePrefix)) _storePrefix = storePrefix + '.';
        _secretsServer = secretsServer;
    }
    
    public IReadOnlyDictionary<string, string?> Resolve(string[] keys)
    {
        var unresolvedKeys = new List<string>();
        var ttl = TimeSpan.Zero;
        if (_secretsServer.ValidFor != null)
        {
            ttl = new TimeSpan(
                _secretsServer.ValidFor.Days ?? 0, 
                _secretsServer.ValidFor.Hours ?? 0, 
                _secretsServer.ValidFor.Minutes ?? 0, 
                _secretsServer.ValidFor.Seconds ?? 0);    
        }

        if (ttl == TimeSpan.Zero) ttl = new TimeSpan(0, 0, 30, 0);
        
        var resolvedSecrets = new List<KeyValuePair<string, string?>>();
        foreach (var key in keys)
        {
            var cachedSecret = _store.Load($"{_storePrefix}{key}", ttl);
            if (!string.IsNullOrWhiteSpace(cachedSecret))
            {
                resolvedSecrets.Add(new KeyValuePair<string, string?>(key, cachedSecret));
            }
            else
            {
                unresolvedKeys.Add(key);
            }
        }
        
        if (unresolvedKeys.Any())
        {
            switch (_secretsServer.Provider)
            {
                case SecretsServerProvider.AzureKeyVault:
                    var azureVault = new HashicorpSecretsProvider(_secretsServer.ServerUri, _secretsServer.Password!, _secretsServer.Name);
                    var azureSecrets = azureVault.GetSecretsAsync(unresolvedKeys.ToArray()).Result;
                    if (azureSecrets.Any())
                    {
                        resolvedSecrets.AddRange(azureSecrets);
                    }
                    foreach (var azureSecret in azureSecrets)
                    {
                        if (azureSecret.Value != null) _store.Save($"{_storePrefix}{azureSecret.Key}", azureSecret.Value);
                    }
                    break;
            }
        }
        return resolvedSecrets.ToDictionary(k => k.Key, v => v.Value);
    }
    
}