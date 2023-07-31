﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Nox.Solution;
using Nox.Types.EntityFramework.Abstractions;

namespace Nox.Types.EntityFramework.Types;

public class FileDatabaseConfigurator : INoxTypeDatabaseConfigurator
{
    public NoxType ForNoxType => NoxType.File;

    public bool IsDefault => true;

    public void ConfigureEntityProperty(
        NoxSolutionCodeGeneratorState noxSolutionCodeGeneratorState,
        EntityTypeBuilder builder,
        NoxSimpleTypeDefinition property,
        Entity entity,
        bool isKey)
    {
        builder
            .OwnsOne(typeof(File), property.Name)
            .Ignore(nameof(File.Value));
    }

    public string GetKeyPropertyName(NoxSimpleTypeDefinition key) => key.Name;
}
