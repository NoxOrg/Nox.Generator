﻿// ReSharper disable once CheckNamespace
using FluentAssertions;

namespace Nox.Types.Tests.Types;

public class TemperatureTests
{
    [Fact]
    public void From_TemperatureUnitSent_Celsius_ReturnsValue()
    {
        var value = 25.05;
        var temperature = Temperature.From(value, TemperatureUnit.Celsius);

        temperature.Value.Should().Be(value);
        temperature.Unit.Should().Be(TemperatureUnit.Celsius);
    }

    [Fact]
    public void From_TemperatureUnitSent_Fahrenheit_ReturnsValue()
    {
        var value = 86.88;
        var temperature = Temperature.From(value, TemperatureUnit.Fahrenheit);

        temperature.Value.Should().Be(value);
        temperature.Unit.Should().Be(TemperatureUnit.Fahrenheit);
    }

    [Fact]
    public void From_TemperatureAbsoluteZero_Celsius_ThrowsException()
    {
        Action comparison = () => Temperature.From(-280, TemperatureUnit.Celsius);

        comparison.Should().Throw<TypeValidationException>();
    }

    [Fact]
    public void From_TemperatureAbsoluteZero_Fahrenheit_ThrowsException()
    {
        Action comparison = () => Temperature.From(-460, TemperatureUnit.Fahrenheit);

        comparison.Should().Throw<TypeValidationException>();
    }

    [Fact]
    public void FromCelsius_ReturnsValue()
    {
        double value = 32.5;
        var temperature = Temperature.FromCelsius(value);

        temperature.Value.Should().Be(value);
        temperature.Unit.Should().Be(TemperatureUnit.Celsius);
    }

    [Fact]
    public void FromCelsius_TemperatureAbsoluteZero_ThrowsException()
    {
        Action comparison = () => Temperature.FromCelsius(-300);

        comparison.Should().Throw<TypeValidationException>();
    }

    [Fact]
    public void FromFahrenheit_ReturnsValue()
    {
        double value = 90.5;
        var temperature = Temperature.FromFahrenheit(value);

        temperature.Value.Should().Be(value);
        temperature.Unit.Should().Be(TemperatureUnit.Fahrenheit);
    }

    [Fact]
    public void FromFahrenheit_TemperatureAbsoluteZero_ThrowsException()
    {
        Action comparison = () => Temperature.FromFahrenheit(-980);

        comparison.Should().Throw<TypeValidationException>();
    }

    [Fact]
    public void From_DefaultUnit_ReturnsValue()
    {
        double value = 30.8;
        var temperature = Temperature.FromCelsius(value);

        temperature.Value.Should().Be(value);
        temperature.Unit.Should().Be(TemperatureUnit.Celsius);
    }

    [Fact]
    public void ToString_Celsius_ReturnsValue()
    {
        var value = 25.05;
        var temperature = Temperature.From(value, TemperatureUnit.Celsius);

        temperature.ToString().Should().Be("25.05 C");
    }

    [Fact]
    public void ToString_Fahrenheit_ReturnsValue()
    {
        var value = 25.05;
        var temperature = Temperature.From(value, TemperatureUnit.Fahrenheit);

        temperature.ToString().Should().Be("25.05 F");
    }

    [Fact]
    public void TemperatureConversion_CelciusToCelsius_ReturnsCorrectValue()
    {
        var temperature = Temperature.From(25.05, TemperatureUnit.Celsius);

        temperature.ToCelsius().Should().Be(25.05);
    }

    [Fact]
    public void TemperatureConversion_FahrenheitToCelsius_ReturnsCorrectValue()
    {
        var temperature = Temperature.From(87.89, TemperatureUnit.Fahrenheit);

        temperature.ToCelsius().Should().Be(31.05);
    }

    [Fact]
    public void TemperatureConversion_CelsiusToFahrenheit_ReturnsCorrectValue()
    {
        var temperature = Temperature.From(31.05, TemperatureUnit.Celsius);

        temperature.ToFahrenheit().Should().Be(87.89);
    }
}