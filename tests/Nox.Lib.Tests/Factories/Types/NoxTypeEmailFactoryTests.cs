﻿using FluentAssertions;
using Nox.Factories.Types;
using Nox.Lib.Tests.FixtureConfig;
using Nox.Solution;

namespace Nox.Lib.Tests.Factories.Types
{
    public class NoxTypeEmailFactoryTests
    {
        [Theory, AutoMoqData]
        public void CreateNoxType_FromDto_IsValid(NoxSolution noxSolution, EntityDefinitionFixture fixture)
        {
            // Arrange
            NoxTypeEmailFactory sut = new NoxTypeEmailFactory(noxSolution);
            var value = "mail@example.com";

            // Act
            var entity = sut.CreateNoxType(fixture.EntityDefinition, fixture.PropertyName, value);

            // Assert
            entity.Should().NotBeNull();
            entity!.Value.Should().Be(value);
        }
    }
}
