﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Solution;
using Nox.Types.EntityFramework.Abstractions;

namespace Nox.Types.EntityFramework.Types;

public class EncryptedTextDatabaseConfigurator : INoxTypeDatabaseConfigurator
{
    public NoxType ForNoxType => NoxType.EncryptedText;
    public virtual bool IsDefault => true;

    public void ConfigureEntityProperty(
        NoxCodeGenConventions noxSolutionCodeGeneratorState,
        AttributeConfiguration property,
        Entity entity,
        bool isKey,
        ModelBuilder modelBuilder, EntityTypeBuilder entityTypeBuilder)
    {
        entityTypeBuilder
            .Property(property.Name)
            .IsRequired(property.IsRequired)
            .IsUnicode(false)
            .HasConversion<EncryptedTextConverter>();
    }

    public string GetKeyPropertyName(NoxSimpleTypeDefinition key) => key.Name;
}