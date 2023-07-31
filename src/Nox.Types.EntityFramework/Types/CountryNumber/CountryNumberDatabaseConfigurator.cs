﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Solution;
using Nox.Types.EntityFramework.Abstractions;

namespace Nox.Types.EntityFramework.Types;

public class CountryNumberDatabaseConfigurator : INoxTypeDatabaseConfigurator
{
    public NoxType ForNoxType => NoxType.CountryNumber;

    public bool IsDefault => true;

    public void ConfigureEntityProperty(NoxSolutionCodeGeneratorState noxSolutionCodeGeneratorState, EntityTypeBuilder builder, NoxSimpleTypeDefinition property, Entity entity, bool isKey)
    {
        builder
            .Property(property.Name)
            .IsRequired(property.IsRequired)
            .HasConversion<CountryNumberConverter>();
    }

    public string GetKeyPropertyName(NoxSimpleTypeDefinition key)=> key.Name;
}
