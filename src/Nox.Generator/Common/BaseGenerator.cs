﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Nox.Generator.Application.DtoGenerator;
using Nox.Solution;
using Nox.Types;

namespace Nox.Generator.Common;

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

    internal static void AddProperty(CodeBuilder code, string type, string name, string? description)
    {
        GenerateDocs(code, description);
        code.AppendLine($"protected {type} {name} {{ get; set; }} = null!;");
    }

    internal static void AddField(CodeBuilder code, string type, string name, string? description)
    {
        GenerateDocs(code, description);
        code.AppendLine($"protected readonly {type} {name.ToLowerFirstCharAndAddUnderscore()};");
    }

    internal static string GetParametersString(IEnumerable<DomainQueryRequestInput>? input)
    {
        if (input != null)
        {
            // TODO: switch to a general type resolver and error processing
            return string.Join(", ", input
                .Select(parameter =>
                    $"{(parameter.Type != NoxType.Entity ? parameter.Type.ToString() : parameter.EntityTypeOptions!.Entity)} {parameter.Name}"));
        }

        return string.Empty;
    }

    internal static string GetParametersExecuteString(IReadOnlyList<DomainQueryRequestInput>? input)
    {
        if (input != null)
        {
            return string.Join(", ", input.Select(parameter => $"{parameter.Name}"));
        }

        return string.Empty;
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
            code.AppendLine($@"{value.ToLowerFirstCharAndAddUnderscore()} = {value.ToLowerFirstChar()};");
        }

        code.UnIndent();
        code.AppendLine($@"}}");
    }

    public static string GenerateTypeDefinition(SourceProductionContext context, string solutionNameSpace, NoxComplexTypeDefinition typeDefinition, bool generateDto = false)
    {
        string stringTypeDefinition;
        string typeName;

        switch (typeDefinition.Type)
        {
            case NoxType.Array:
                var options = typeDefinition.ArrayTypeOptions;
                typeName = options!.Name;
                stringTypeDefinition = $"{typeName}[]";

                if (generateDto && options is { Type: NoxType.Object, ObjectTypeOptions: not null })
                {
                    GenerateDtoFromDefinition(context, solutionNameSpace, typeName, options);
                }

                break;

            case NoxType.Collection:
                var collection = typeDefinition.CollectionTypeOptions;
                typeName = collection!.Name;
                stringTypeDefinition = $"IEnumerable<{typeName}>";

                if (generateDto && collection is { Type: NoxType.Object, ObjectTypeOptions: not null })
                {
                    GenerateDtoFromDefinition(context, solutionNameSpace, typeName, collection);
                }

                break;

            case NoxType.Object:
                stringTypeDefinition = typeDefinition.Name;
                
                if (generateDto)
                {
                    DtoGenerator.GenerateDto(context,
                        solutionNameSpace,
                        typeDefinition.Name,
                        typeDefinition.Description,
                        typeDefinition.ObjectTypeOptions!.Attributes);
                }
                
                break;

            default:
                stringTypeDefinition = typeDefinition.Type.ToString();
                break;
        }

        return stringTypeDefinition;
    }
    internal static void AddDbContextOnConfiguring(CodeBuilder code, string solutionName)
    {
        code.AppendLine("protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)");
        code.StartBlock();
        code.AppendLine("base.OnConfiguring(optionsBuilder);");
        code.AppendLine("if (_noxSolution.Infrastructure is { Persistence.DatabaseServer: not null })");
        code.StartBlock();
        code.AppendLine($"_dbProvider.ConfigureDbContext(optionsBuilder, \"{solutionName}\", _noxSolution.Infrastructure!.Persistence.DatabaseServer); ");
        code.EndBlock();
        code.EndBlock();
        code.AppendLine();
    }

    private static void GenerateDtoFromDefinition(SourceProductionContext context, string solutionNameSpace, string typeName, ArrayTypeOptions options)
    {
        DtoGenerator.GenerateDto(context,
                                solutionNameSpace,
                                typeName.ToUpperFirstChar(),
                                options.Description,
                                options.ObjectTypeOptions!.Attributes);
    }
}