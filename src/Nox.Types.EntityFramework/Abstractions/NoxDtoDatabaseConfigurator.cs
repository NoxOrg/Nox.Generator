﻿using Nox.Solution;
using Nox.Types.EntityFramework.EntityBuilderAdapter;
using Nox.Solution.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Nox.Types.EntityFramework.Abstractions;

public class NoxDtoDatabaseConfigurator : INoxDtoDatabaseConfigurator
{
    public void ConfigureDto(NoxSolutionCodeGeneratorState codeGeneratorState, IEntityBuilder builder, Entity entity)
    {
        ConfigureKeys(builder, entity);

        ConfigureAttributes(codeGeneratorState, builder, entity);

        ConfigureOwnedRelations(codeGeneratorState, builder, entity);
    }

    private static void ConfigureAttributes(NoxSolutionCodeGeneratorState codeGeneratorState, IEntityBuilder builder, Entity entity)
    {
        foreach (var attribute in entity.Attributes!)
        {
            // TODO inject configuration per attribute or use NoxType metadata to it

            if (attribute.Type == NoxType.VatNumber)
            {
                var compoundDtoType = codeGeneratorState.GetEntityDtoType("VatNumberDto")!;

                builder.OwnsOne(compoundDtoType, attribute.Name)
                    .Property(nameof(VatNumber.CountryCode))
                    .HasConversion(new EnumToStringConverter<CountryCode>());
            }
        }
    }
    private static void ConfigureOwnedRelations(NoxSolutionCodeGeneratorState codeGeneratorState, IEntityBuilder builder, Entity entity)
    {
        if (entity.OwnedRelationships != null)
        {
            var keyNames = entity.Keys!.Select(x => x.Name);

            //#pragma warning disable S3267 // Loops should be simplified with "LINQ" expressions
            foreach (var ownedRelationship in entity.OwnedRelationships)
            //#pragma warning restore S3267 // Loops should be simplified with "LINQ" expressions
            {
                if (ownedRelationship.WithSingleEntity())
                {
                    throw new NotImplementedException("Owned Entity ExactlyOne or ZeroOrOne not implemented");
                }
                var relatedEntityDtoType = codeGeneratorState.GetEntityDtoType(ownedRelationship.Related.Entity.Name + "Dto")!;
                builder.OwnsMany(relatedEntityDtoType,
                    ownedRelationship.Related.Entity.PluralName,
                    owned =>
                    {
                        owned.WithOwner().HasForeignKey($"{entity.Name}Id");
                        owned.HasKey(string.Join(",", keyNames));
                        owned.ToTable(ownedRelationship.Related.Entity.Name);
                    });

            }
        }
    }

    private static void ConfigureKeys(IEntityBuilder builder, Entity entity)
    {
        foreach (var key in entity.Keys!)
        {
            builder.HasKey(key.Name);
        }
    }
}