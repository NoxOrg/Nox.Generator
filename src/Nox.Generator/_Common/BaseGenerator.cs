﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Nox.Generator.Application.DtoGenerator;
using Nox.Solution;
using Nox.Types;

namespace Nox.Generator._Common;

internal class BaseGenerator
{
    internal static void GenerateDocs(CodeBuilder code, string? description)
    {
        if (!string.IsNullOrWhiteSpace(description))
        {
            code.AppendLine();
            code.AppendLine($"/// <summary>");
            code.AppendLine($"/// {description!.EnsureEndsWith('.')}");
            code.AppendLine($"/// </summary>");
        }
    }

    internal static void AddProperty(CodeBuilder code, string type, string name, string description)
    {
        code.AppendLine();
        GenerateDocs(code, description);
        code.AppendLine($"protected {type} {name} {{ get; set; }} = null!;");
    }

    internal static string GetParametersString(IEnumerable<DomainQueryRequestInput>? input)
    {
        if (input != null)
        {
            // TODO: switch to a general type resolver and error processing
            return string.Join(", ", input
                .Select(parameter =>
                    $"{(parameter.Type != NoxType.Entity ? MapType(parameter.Type) : parameter.EntityTypeOptions!.Entity)} {parameter.Name}"));    
        }

        return "";
    }

    internal static string GetParametersExecuteString(IReadOnlyList<DomainQueryRequestInput> input)
    {
        return string.Join(", ", input.Select(parameter => $"{parameter.Name}"));
    }

    internal static string MapType(NoxType noxType)
    {
        return noxType switch
        {
            NoxType.LatLong => "LatLong",
            _ => noxType.ToString(),
        };
    }

    internal static void AddConstructor(CodeBuilder code, string className, Dictionary<string, string> parameters)
    {
        code.AppendLine();
        code.AppendLine($@"public {className}(");
        code.Indent();
        for (int i = 0; i < parameters.Count; i++)
        {
            var parameter = parameters.ElementAt(i);
            code.AppendLine(
                $@"{parameter.Key} {parameter.Value.ToLowerFirstChar()}{(i < parameters.Count - 1 ? "," : string.Empty)}");
        }

        code.UnIndent();
        code.AppendLine($@")");
        code.AppendLine($@"{{");

        code.Indent();
        foreach (var value in parameters.Select(p => p.Value))
        {
            code.AppendLine($@"{value} = {value.ToLowerFirstChar()};");
        }

        code.UnIndent();
        code.AppendLine($@"}}");
        code.AppendLine($@"");
    }

    public static string GenerateTypeDefinition(SourceProductionContext context, string solutionNameSpace,
        NoxComplexTypeDefinition typeDefinition)
    {
        string stringTypeDefinition;
        string typeName;

        switch (typeDefinition.Type)
        {
            case NoxType.Array:
                var options = typeDefinition.ArrayTypeOptions;
                typeName = options!.Name;
                stringTypeDefinition = $"{typeName}[]";
                if (options is { Type: NoxType.Object, ObjectTypeOptions: not null })
                {
                    DtoGenerator.GenerateDto(context, solutionNameSpace, typeName, options.Description,
                        options.ObjectTypeOptions.Attributes);
                }

                break;
            case NoxType.Collection:
                var collection = typeDefinition.CollectionTypeOptions;
                typeName = collection!.Name;
                stringTypeDefinition = $"IEnumerable<{typeName}>";
                if (collection is { Type: NoxType.Object, ObjectTypeOptions: not null })
                {
                    DtoGenerator.GenerateDto(context, solutionNameSpace, typeName, collection.Description,
                        collection.ObjectTypeOptions.Attributes);
                }

                break;
            case NoxType.Object:
                stringTypeDefinition = typeDefinition.Name;
                DtoGenerator.GenerateDto(context, solutionNameSpace, typeDefinition.Name,
                    typeDefinition.Description, typeDefinition.ObjectTypeOptions!.Attributes);
                break;
            default:
                stringTypeDefinition = MapType(typeDefinition.Type);
                break;
        }

        return stringTypeDefinition;
    }
}