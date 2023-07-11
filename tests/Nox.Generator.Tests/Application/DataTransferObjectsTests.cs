﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Nox.Generator.Tests.Application;

public class DataTransferObjectsTests: IClassFixture<GeneratorFixture>
{
    private readonly GeneratorFixture _fixture;

    public DataTransferObjectsTests(GeneratorFixture fixture)
    {
        _fixture = fixture;
    }
    
    
    [Fact]
    public void Can_generate_a_dto_file()
    {
        var path = "files/yaml/application/";
        var additionalFiles = new List<AdditionalSourceText>
        {
            new AdditionalSourceText(File.ReadAllText($"./{path}generator.nox.yaml"), $"{path}/generator.nox.yaml"),
            new AdditionalSourceText(File.ReadAllText($"./{path}dto.solution.nox.yaml"), $"{path}/dto.solution.nox.yaml")
        };

        // trackIncrementalGeneratorSteps allows to report info about each step of the generator
        GeneratorDriver driver = CSharpGeneratorDriver.Create(
            generators: new [] { _fixture.TestGenerator },
            additionalTexts: additionalFiles,
            driverOptions: new GeneratorDriverOptions(default, trackIncrementalGeneratorSteps: true));
        
        // Run the generator
        driver = driver.RunGenerators(_fixture.TestCompilation!);

        // Assert the driver doesn't recompute the output
        var result = driver.GetRunResult().Results.Single();
        var allOutputs = result.TrackedOutputSteps.SelectMany(outputStep => outputStep.Value).SelectMany(output => output.Outputs);
        Assert.NotNull(allOutputs);
        Assert.Single(allOutputs);

        var generatedSources = result.GeneratedSources;
        Assert.Equal(3, generatedSources.Length);

        Assert.True(generatedSources.Any(s => s.HintName == "NoxServiceCollectionExtension.g.cs"), "NoxServiceCollectionExtension.g.cs not generated");
        Assert.True(generatedSources.Any(s => s.HintName == "Generator.g.cs"), "Generator not generated");

        var countryDtoFileName = "CountryDto.g.cs";
        Assert.True(generatedSources.Any(s => s.HintName == countryDtoFileName), $"{countryDtoFileName} not generated");
        Assert.Equal(File.ReadAllText("./ExpectedGeneratedFiles/CountryDto.expected.g.cs"), generatedSources.First(s => s.HintName == countryDtoFileName).SourceText.ToString());
        //can further extend this test to verify contents of source files.
    }
}