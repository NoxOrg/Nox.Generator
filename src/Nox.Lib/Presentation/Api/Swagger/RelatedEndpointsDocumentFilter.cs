﻿using Microsoft.OpenApi.Models;
using Nox.Solution;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Nox.Presentation.Api.Swagger;

public class RelatedEndpointsDocumentFilter : IDocumentFilter
{
    private readonly IReadOnlyList<Entity> _entities;

    private readonly string _apiRoutePrefix;

    private readonly int _endpointsMaxDepth;

    private readonly RelatedEndpointsPathBuilder _pathBuilder;

    public RelatedEndpointsDocumentFilter(NoxSolution solution)
    {
        _entities = solution.Domain is not null ? solution.Domain.Entities : Array.Empty<Entity>();
        _apiRoutePrefix = solution.Presentation.ApiConfiguration.ApiRoutePrefix;
        _endpointsMaxDepth = solution.Presentation.ApiConfiguration.ApiGenerateRelatedEndpointsMaxDepth;
        _pathBuilder = new RelatedEndpointsPathBuilder(_entities);
    }

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var relatedPathItems = new List<(string Path, OpenApiPathItem PathItem)>();
        foreach (var entity in _entities)
        {
            relatedPathItems.AddRange(_pathBuilder.GetAllRelatedPathesForEntity(entity, _endpointsMaxDepth));
        }

        foreach(var item in relatedPathItems)
        {
            swaggerDoc.Paths.Add($"{_apiRoutePrefix}/{item.Path}", item.PathItem);
        }

    }
}