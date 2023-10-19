﻿using Microsoft.CodeAnalysis;
using Nox.Generator.Common;
using Nox.Solution;

namespace Nox.Generator.Application;

internal class CustomIntegrationEventGenerator : INoxCodeGenerator
{
    public NoxGeneratorKind GeneratorKind => NoxGeneratorKind.Application;

    public void Generate(
        SourceProductionContext context,
        NoxSolutionCodeGeneratorState codeGeneratorState,
        GeneratorConfig config,
        System.Action<string> log,
        string? projectRootPath
        )
    {
        context.CancellationToken.ThrowIfCancellationRequested();

        if (codeGeneratorState.Solution.Application?.IntegrationEvents is null)
            return;

        foreach (var integrationEvent in codeGeneratorState.Solution.Application.IntegrationEvents)
        {
            context.CancellationToken.ThrowIfCancellationRequested();

            new TemplateCodeBuilder(context, codeGeneratorState)
                .WithClassName(integrationEvent.Name)
                .WithFileNamePrefix($"Application.IntegrationEvents")
                .WithObject("integrationEvent", integrationEvent)
                .GenerateSourceCodeFromResource("Application.IntegrationEvents.CustomIntegrationEvent");
        }
    }
}
