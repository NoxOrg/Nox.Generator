﻿using Nox.Generator.Common;
using Nox.Solution;

namespace Nox.Generator.Tasks.Ui.Components;

internal class SearchFilterGenerator : INoxFileGenerator
{
    public NoxGeneratorKind GeneratorKind => NoxGeneratorKind.Ui;

    public void Generate(
      NoxCodeGenConventions codeGeneratorState,
      GeneratorConfig config,
      System.Action<string> log,
      string absoluteOutputPath
      )
    {

        if (codeGeneratorState.Solution.Domain is null)
        {
            return;
        }

        var templateName = @"Tasks.Ui.Configuration.SearchFilter";

        new TaskTemplateFileBuilder(codeGeneratorState, absoluteOutputPath)
            .WithClassName($"SearchFilter")
            .WithFileNamePrefix($"Ui.Configuration")
            .GenerateSourceCodeFromResource(templateName);
    }
}