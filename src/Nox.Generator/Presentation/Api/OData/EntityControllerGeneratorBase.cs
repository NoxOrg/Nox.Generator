﻿using Microsoft.CodeAnalysis;

using Nox.Generator.Common;
using Nox.Solution;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nox.Generator.Presentation.Api.OData;

internal abstract class EntityControllerGeneratorBase : INoxCodeGenerator
{
    public NoxGeneratorKind GeneratorKind => NoxGeneratorKind.Presentation;

    public abstract void Generate(SourceProductionContext context, NoxSolutionCodeGeneratorState codeGeneratorState, GeneratorConfig config);

    protected static string GetPrimaryKeysQuery(Entity entity, string prefix = "key", bool withKeyName = false)
    {
        if (entity?.Keys?.Count > 1)
        {
            return string.Join(", ", entity.Keys.Select(k => $"{prefix}{k.Name}"));
        }
        else if (entity?.Keys is not null)
        {
            return withKeyName ? $"{prefix}{entity.Keys[0].Name}" : prefix;
        }

        return string.Empty;
    }

    protected static string GetPrimaryKeysRoute(Entity entity, NoxSolution solution, string keyPrefix = "key", string attributePrefix = "[FromRoute]")
    {
        if (entity?.Keys?.Count > 1)
        {
            return string.Join(", ", entity.Keys.Select(k => $"{attributePrefix} {solution.GetSinglePrimitiveTypeForKey(k)} {keyPrefix}{k.Name}"))
                .Trim();
        }
        else if (entity?.Keys is not null)
        {
            return $"{attributePrefix} {solution.GetSinglePrimitiveTypeForKey(entity.Keys[0])} {keyPrefix}"
                .Trim();
        }

        return string.Empty;
    }
}
