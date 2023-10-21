﻿namespace Nox.Generator.Tests.Flows;

public interface IGeneratorContentTestFlow
{
    IGeneratorContentTestFlow WithExpectedFilesFolder(string expectedFilesFolder);

    IGeneratorContentTestFlow AssertFileExistsAndContent(string expectedFileName, string actualFileName);

    void SourceContains(string sourceName, string content);
}