﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Solution;
using Nox.Types.EntityFramework.Abstractions;

namespace Nox.Types.EntityFramework.Types;

public class AreaDatabaseConfigurator : INoxTypeDatabaseConfigurator
{
    public NoxType ForNoxType => NoxType.Area;
    public bool IsDefault => true;

    public void ConfigureEntityProperty(
        NoxSolutionCodeGeneratorState noxSolutionCodeGeneratorState,
        EntityTypeBuilder builder,
        NoxSimpleTypeDefinition property,
        Entity entity,
        bool isKey)
    {
        var typeOptions = property.AreaTypeOptions ?? new AreaTypeOptions();

        builder
            .Property(property.Name)
            .IsRequired(property.IsRequired)
            .HasConversion(GetConverterBasedOn(typeOptions.Unit));
    }

    public string GetKeyPropertyName(NoxSimpleTypeDefinition key) => key.Name;
    
    private static Type? GetConverterBasedOn(AreaTypeUnit unit)
    {
        if (unit == AreaTypeUnit.SquareFoot)
            return typeof(AreaToSquareFeetConverter);

        return typeof(AreaToSquareMeterConverter);
    }
}