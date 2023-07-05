﻿
using FluentAssertions;
using Nox.Types.Tests.Types.LanguageCode;

namespace Nox.Types.Tests;

public class LanguageCodeTests
{
    [Theory]
    [ClassData(typeof(LanguageCodeTestsDataClass))]
    public void CreatingLanguageCode_From_IsValid(string languageCodeString)
    {
        var languageCode = LanguageCode.From(languageCodeString);

        languageCode.Value.Should().Be(languageCodeString);
    }

    [Fact]
    public void CreatingLanguageCode_UnsupportedLanguageCode_ThrowsValidationException()
    {
        var action = () => LanguageCode.From("ABC");

        action.Should().Throw<TypeValidationException>()
            .And.Errors.Should().BeEquivalentTo(new[] { new ValidationFailure("Value", "Could not create a Nox LanguageCode type with unsupported value 'ABC'.") });
    }

    [Fact]
    public void CreatingLanguageCode_SameLanguageCode_ReturnsEqual()
    {
        var languageCode1 = LanguageCode.From("EN");

        var languageCode2 = LanguageCode.From("EN");

        languageCode2.Should().Be(languageCode1);
    }

    [Fact]
    public void CreatingLanguageCode_DifferentLanguageCode_ReturnsUnEqual()
    {
        var languageCode3_1 = LanguageCode.From("EN");

        var languageCode3_2 = LanguageCode.From("PT");

        languageCode3_2.Should().NotBe(languageCode3_1);
    }

    [Fact]
    public void LanguageCode_ToString_ReturnsValueAsString()
    {
        var languageCode = LanguageCode.From("EN");

        languageCode.ToString().Should().Be("EN");
    }
}