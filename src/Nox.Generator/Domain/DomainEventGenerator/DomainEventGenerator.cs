﻿using Microsoft.CodeAnalysis;
using Nox.Generator.Common;
using Nox.Solution;
using Nox.Solution.Events;
using System.Linq;
using System.Reflection;
using static Nox.Generator.Common.BaseGenerator;

namespace Nox.Generator.Domain.DomainEventGenerator;

internal class DomainEventGenerator : INoxCodeGenerator
{
    public NoxGeneratorKind GeneratorKind => NoxGeneratorKind.Domain;

    public void Generate(SourceProductionContext context, NoxSolutionCodeGeneratorState codeGeneratorState, GeneratorConfig config, string? projectRootPath)
    {
        context.CancellationToken.ThrowIfCancellationRequested();

        if (codeGeneratorState.Solution.Domain == null) return;

#pragma warning disable S3267 // Loops should be simplified with "LINQ" expressions
        foreach (var entity in codeGeneratorState.Solution.Domain.Entities)
        {
            context.CancellationToken.ThrowIfCancellationRequested();
            if (entity.Events == null || !entity.Events.Any()) continue;
            foreach (var evt in entity.Events)
            {
                context.CancellationToken.ThrowIfCancellationRequested();
                GenerateEvent(context, codeGeneratorState, evt);
            }
        }
#pragma warning restore S3267 // Loops should be simplified with "LINQ" expressions
    }

    private static void GenerateEvent(SourceProductionContext context, NoxSolutionCodeGeneratorState codeGeneratorState, DomainEvent evt)
    {
        var code = new CodeBuilder($"{evt.Name}.g.cs", context);

        code.AppendLine($"// Generated by {nameof(DomainEventGenerator)}::{MethodBase.GetCurrentMethod()!.Name}");
        code.AppendLine();
        //NOTE: this must point to Nox abstractions
        code.AppendLine($"using Nox.Abstractions;");
        code.AppendLine($"using Nox.Types;");
        code.AppendLine();
        code.AppendLine($"namespace {codeGeneratorState.DomainNameSpace};");

        GenerateDocs(code, evt.Description);

        code.AppendLine($"public partial class {evt.Name} : INoxDomainEvent");
        code.StartBlock();

        GenerateProperties(context, code, evt);

        code.EndBlock();

        code.GenerateSourceCode();
    }

    private static void GenerateProperties(SourceProductionContext context, CodeBuilder code, DomainEvent evt)
    {
        if (evt.ObjectTypeOptions != null)
        {
            foreach (var attribute in evt.ObjectTypeOptions.Attributes)
            {
                context.CancellationToken.ThrowIfCancellationRequested();

                GenerateDocs(code, attribute.Description);

                var propType = attribute.Type;
                var propName = attribute.Name;
                var nullable = attribute.IsRequired ? string.Empty : "?";

                code.AppendLine($"public {propType}{nullable} {propName} {{ get; set; }} = null!;");
            }
        }
    }
}