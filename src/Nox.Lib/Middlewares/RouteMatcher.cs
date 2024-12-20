using Microsoft.AspNetCore.Http;
using Nox.Solution;
using System.Diagnostics;

namespace Nox.Lib;

[DebuggerDisplay("{_apiRoute}")]
internal class RouteMatcher
{
    private readonly ApiRouteMatcher _matcher;

    private readonly ApiRouteMapping _apiRoute;

    private const int _slash = 47;

    public RouteMatcher(ApiRouteMapping apiRoute, string prefix)
    {
        var leadingChar = apiRoute.Route[0] == _slash ? string.Empty : "/";

        var template = $"{prefix}{leadingChar}{apiRoute.Route}";

        _apiRoute = apiRoute;

        _matcher = new ApiRouteMatcher(template, 
            apiRoute
            .RequestInput
            .Where(i => i.Default is not null)
            .ToDictionary(i => i.Name, i => i.Default?.ToString() ?? string.Empty)
        );
    }

    internal ApiRouteMapping ApiRoute => _apiRoute;

    public bool Match(HttpRequest httpRequest, out IDictionary<string, string>? paramValues)
    {
        paramValues = null;
        var isMatch = _matcher.Match($"{httpRequest.Path}{httpRequest.QueryString}", out var values);

        if(isMatch)
            paramValues = values;

        return isMatch;
    }

    public string TransformTo(string toPath, IDictionary<string, string>? paramValues)
    {
        if (paramValues is null) return toPath;

        return _matcher.TransformTo(toPath, paramValues);
    }
}