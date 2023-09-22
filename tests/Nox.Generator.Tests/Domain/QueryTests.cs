using System.Collections.Generic;
using System.IO;
using System.Linq;
using Humanizer;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Nox.Generator.Tests.Domain;

public class QueryTests: IClassFixture<GeneratorFixture>
{
    private readonly GeneratorFixture _fixture;

    public QueryTests(GeneratorFixture fixture)
    {
        _fixture = fixture;
    }
    
    
    [Fact]
    public void Can_generate_domain_query_files()
    {
        var path = "files/yaml/domain/";
        var additionalFiles = new List<AdditionalSourceText>
        {
            new AdditionalSourceText(File.ReadAllText($"./{path}generator.nox.yaml"), $"{path}/generator.nox.yaml"),
            new AdditionalSourceText(File.ReadAllText($"./{path}query.solution.nox.yaml"), $"{path}/query.solution.nox.yaml")
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
        Assert.Equal(18, generatedSources.Length);
        Assert.True(generatedSources.Any(s => s.HintName == "Application.NoxWebApplicationExtensions.g.cs"), "NoxWebApplicationExtensions.g.cs not generated");
        Assert.True(generatedSources.Any(s => s.HintName == "0.Generator.g.cs"), "Generator.g.cs not generated");
        Assert.True(generatedSources.Any(s => s.HintName == "DtoDynamic.CountryInfo.g.cs"), "CountryInfo.g.cs not generated");

        var queryFileName = "GetCountriesByContinentQueryBase.g.cs";
        Assert.True(generatedSources.Any(s => s.HintName == queryFileName), $"{queryFileName} not generated");
        Assert.Equal(File.ReadAllText("./ExpectedGeneratedFiles/GetCountriesByContinentQueryBase.expected.g.cs"), generatedSources.First(s => s.HintName == queryFileName).SourceText.ToString());

        var generated = generatedSources.First(s => s.HintName == "Application.Dto.CountryDto.g.cs").SourceText.ToString();
        File.WriteAllText("aaa.txt", generated);
        Assert.Equal(File.ReadAllText("./ExpectedGeneratedFiles/Application.Dto.CountryDto.expected.g.cs"), generated);
        Assert.Equal(File.ReadAllText("./ExpectedGeneratedFiles/Dto.CountryCreateDto.expected.g.cs"), generatedSources.First(s => s.HintName == "Application.Dto.CountryCreateDto.g.cs").SourceText.ToString());        
        //can further extend this test to verify contents of source files.
    }
}