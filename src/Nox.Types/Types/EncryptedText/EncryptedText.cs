using System;
using System.Linq;
using Nox.Types.EncryptionText.EncryptionMethods;

namespace Nox.Types;

/// <summary>
/// Represents a Nox <see cref="EncryptedText"/> type and value object.
/// </summary>
public sealed class EncryptedText : ValueObject<byte[], EncryptedText>
{
    public EncryptedText()
    {
        Value = new byte[] { };
    }

    /// <summary>
    /// Creates an <see cref="EncryptedText"/> from a string using the provided <paramref name="typeOptions"/>.
    /// </summary>
    /// <param name="value">Plain text to be encrypted.</param>
    /// <param name="typeOptions">Options used for encryption.</param>
    /// <returns>Encrypted text.</returns>
    /// <exception cref="TypeValidationException"></exception>
    public static EncryptedText FromPlainText(string value, EncryptedTextTypeOptions typeOptions)
    {
        var newObject = new EncryptedText
        {
            Value = EncryptText(value, typeOptions)
        };

        var validationResult = newObject.Validate();

        if (!validationResult.IsValid)
        {
            throw new TypeValidationException(validationResult.Errors);
        }

        return newObject;
    }

    /// <summary>
    /// Creates an <see cref="EncryptedText"/> from an encrypted data as byte array.
    /// </summary>
    /// <param name="value">Encrypted data as byte array.</param>
    /// <returns></returns>
    /// <exception cref="TypeValidationException"></exception>
    public static EncryptedText FromEncryptedTextBytes(byte[] value)
    {
        var newObject = new EncryptedText
        {
            Value = value
        };

        var validationResult = newObject.Validate();

        if (!validationResult.IsValid)
            throw new TypeValidationException(validationResult.Errors);

        return newObject;
    }

    /// <summary>
    /// Decrypts the value using the provided algorithm options.
    /// </summary>
    /// <returns>Decrypted text representation of the <see cref="EncryptedText"/> object.</returns>
    public string DecryptText(EncryptedTextTypeOptions typeOptions) => DecryptText(Value, typeOptions);

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        return Value.SequenceEqual(((EncryptedText)obj).Value);
    }

    /// <inheritdoc />
    public override int GetHashCode() => base.GetHashCode();

    /// <summary>
    /// Validates a <see cref="EncryptedText"/> object.
    /// </summary>
    /// <returns>true if the <see cref="EncryptedText"/> value is valid.</returns>
    internal override ValidationResult Validate()
    {
        var result = new ValidationResult();

        if (Value.Length < 1)
            result.Errors.Add(new ValidationFailure(nameof(Value),
                $"Could not create a Nox {nameof(EncryptedText)} type as an empty {nameof(Value)} is not allowed."));

        return result;
    }

    private static byte[] EncryptText(string plainText, EncryptedTextTypeOptions encryptedTextTypeOptions)
    {
        switch (encryptedTextTypeOptions.EncryptionAlgorithm)
        {
            case EncryptionAlgorithm.Aes:
                return Aes.EncryptStringToBytes(plainText, encryptedTextTypeOptions);
            default:
                throw new NotImplementedException();
        }
    }

    private static string DecryptText(byte[] encryptedText, EncryptedTextTypeOptions encryptedTextTypeOptions)
    {
        switch (encryptedTextTypeOptions.EncryptionAlgorithm)
        {
            case EncryptionAlgorithm.Aes:
                return Aes.DecryptStringFromBytes(encryptedText, encryptedTextTypeOptions);
            default:
                throw new NotImplementedException();
        }
    }
}