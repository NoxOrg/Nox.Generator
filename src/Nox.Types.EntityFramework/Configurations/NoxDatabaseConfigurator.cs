﻿using Nox.Solution;
using Nox.Solution.Extensions;
using Nox.Types.EntityFramework.Abstractions;
using Nox.Types.EntityFramework.EntityBuilderAdapter;
using System.Diagnostics;

namespace Nox.Types.EntityFramework.Configurations
{
    public abstract class NoxDatabaseConfigurator : INoxDatabaseConfigurator
    {
        //We could use the container to manage this
        protected readonly Dictionary<NoxType, INoxTypeDatabaseConfigurator> TypesDatabaseConfigurations = new();

        private static readonly NoxSimpleTypeDefinition[] AuditableEntityAttributes = new AuditableEntityBaseConfiguration().ToArray();

        /// <summary>
        ///
        /// </summary>
        /// <param name="configurators">List of all loaded <see cref="INoxTypeDatabaseConfigurator"/></param>
        /// <param name="databaseProviderSpecificOverrides">Configurator type specific to database provider</param>
        protected NoxDatabaseConfigurator(
            IEnumerable<INoxTypeDatabaseConfigurator> configurators,
            Type databaseProviderSpecificOverrides)
        {
            var noxTypeDatabaseConfigurators =
                configurators as INoxTypeDatabaseConfigurator[] ?? configurators.ToArray();

            foreach (var configurator in noxTypeDatabaseConfigurators)
            {
                if (configurator.IsDefault)
                {
                    TypesDatabaseConfigurations.Add(configurator.ForNoxType, configurator);
                }
            }

            // Override specific database provider configurators
            foreach (var configurator in noxTypeDatabaseConfigurators.Where(databaseProviderSpecificOverrides.IsInstanceOfType))
            {
                TypesDatabaseConfigurations[configurator.ForNoxType] = configurator;
            }
        }

        public virtual void ConfigureEntity(
            NoxSolutionCodeGeneratorState codeGeneratorState,
            IEntityBuilder builder,
            Entity entity)
        {
            var relationshipsToCreate = codeGeneratorState.Solution.GetRelationshipsToCreate(codeGeneratorState, entity);
            var ownedRelationshipsToCreate = codeGeneratorState.Solution.GetOwnedRelationshipsToCreate(codeGeneratorState, entity);

            ConfigureKeys(codeGeneratorState, builder, entity);

            ConfigureAttributes(codeGeneratorState, builder, entity);

            ConfigureRelationships(codeGeneratorState, builder, entity, relationshipsToCreate);

            ConfigureOwnedRelationships(codeGeneratorState, builder, entity, ownedRelationshipsToCreate);
        }

        public virtual void ConfigureRelationships(
            NoxSolutionCodeGeneratorState codeGeneratorState,
            IEntityBuilder builder,
            Entity entity,
            IReadOnlyList<EntityRelationshipWithType> relationshipsToCreate)
        {
            foreach (var relationshipToCreate in relationshipsToCreate)
            {
                // Many to Many
                // Currently, configured bi-directionally, shouldn't cause any issues.
                if (relationshipToCreate.Relationship.WithMultiEntity &&
                    relationshipToCreate.Relationship.Related.EntityRelationship.WithMultiEntity)
                {
                    builder
                        .HasMany(relationshipToCreate.Relationship.Name)
                        .WithMany(relationshipToCreate.Relationship.Related.EntityRelationship.Name);
                }
                // OneToOne and OneToMany, setup should be done only on foreign key side
                else if (relationshipToCreate.Relationship.ShouldGenerateForeignKeyOnThisSide() &&
                    relationshipToCreate.Relationship.WithSingleEntity())
                {
                    //One to Many
                    if (relationshipToCreate.Relationship.Related.EntityRelationship.WithMultiEntity)
                    {
                        //#if DEBUG
                        Console.WriteLine($"***Relationship oneToMany {entity.Name}," +
                           $"Name {relationshipToCreate.Relationship.Name} " +
                           $"HasOne {$"{codeGeneratorState.DomainNameSpace}.{relationshipToCreate.Relationship.Entity}"} , {relationshipToCreate.Relationship.Entity} " +
                           $"WithMany {entity.PluralName} " +
                           $"ForeignKey {relationshipToCreate.Relationship.Entity}Id " +
                           $"");
                        //#endif

                        builder
                            .HasOne($"{codeGeneratorState.DomainNameSpace}.{relationshipToCreate.Relationship.Entity}", relationshipToCreate.Relationship.Name)
                            .WithMany(relationshipToCreate.Relationship.Related.EntityRelationship.Name)
                            .HasForeignKey($"{relationshipToCreate.Relationship.Name}Id");
                    }
                    else //One to One
                    {
                        //#if DEBUG2
                        Console.WriteLine($"***Relationship oneToOne {entity.Name} ," +
                            $"Name {relationshipToCreate.Relationship.Name} " +
                            $"HasOne {relationshipToCreate.Relationship.Entity} " +
                            $"WithOne {entity.Name}" +
                            $"ForeignKey {relationshipToCreate.Relationship.Entity}Id " +
                            $"");
                        //#endif
                        builder
                            .HasOne($"{codeGeneratorState.DomainNameSpace}.{relationshipToCreate.Relationship.Entity}", relationshipToCreate.Relationship.Name)
                            .WithOne(relationshipToCreate.Relationship.Related.EntityRelationship.Name)
                            .HasForeignKey(entity.Name, $"{relationshipToCreate.Relationship.Name}Id");
                    }

                    // Setup foreign key property
                    ConfigureRelationForeignKeyProperty(codeGeneratorState, builder, entity, relationshipToCreate);
                }
            }
        }

        public virtual void ConfigureOwnedRelationships(
            NoxSolutionCodeGeneratorState codeGeneratorState,
            IEntityBuilder builder,
            Entity entity,
            IReadOnlyList<EntityRelationshipWithType> ownedRelationshipsToCreate)
        {
            foreach (var relationshipToCreate in ownedRelationshipsToCreate)
            {
                if (relationshipToCreate.Relationship.WithSingleEntity())
                {
                    builder
                        .OwnsOne(relationshipToCreate.RelationshipEntityType, relationshipToCreate.Relationship.Entity, x =>
                        {
                            ConfigureEntity(codeGeneratorState, new OwnedNavigationBuilderAdapter(x), relationshipToCreate.Relationship.Related.Entity);
                        });
                }
                else
                {
                    builder
                        .OwnsMany(relationshipToCreate.RelationshipEntityType, relationshipToCreate.Relationship.EntityPlural, x =>
                        {
                            ConfigureEntity(codeGeneratorState, new OwnedNavigationBuilderAdapter(x), relationshipToCreate.Relationship.Related.Entity);
                        });
                }

                if (!relationshipToCreate.Relationship.ShouldUseRelationshipNameAsNavigation())
                {
                    builder.Ignore(relationshipToCreate.Relationship.Name);
                }
            }
        }

        private void ConfigureRelationForeignKeyProperty(
            NoxSolutionCodeGeneratorState codeGeneratorState,
            IEntityBuilder builder,
            Entity entity,
            EntityRelationshipWithType relationshipToCreate)
        {
            // Right now assuming that there is always one key present
            var key = relationshipToCreate.Relationship.Related.Entity.Keys![0];
            if (TypesDatabaseConfigurations.TryGetValue(key.Type,
                out var databaseConfiguration))
            {
                Console.WriteLine($"++++ConfigureRelationForeignKeyProperty {entity.Name}, " +
                    $"rel {relationshipToCreate.Relationship.Name} " +
                    $"Property {relationshipToCreate.Relationship.Related.Entity.Name}Id, " +
                    $"Keytype {key.Type}");

                var keyToBeConfigured = key.ShallowCopy();
                keyToBeConfigured.Name = $"{relationshipToCreate.Relationship.Name}Id";
                keyToBeConfigured.Description = $"Foreign key for entity {relationshipToCreate.Relationship.Name}";
                keyToBeConfigured.IsRequired = false;
                keyToBeConfigured.IsReadonly = false;
                databaseConfiguration.ConfigureEntityProperty(codeGeneratorState, builder, keyToBeConfigured, entity, false);
            }
        }

        private void ConfigureKeys(
            NoxSolutionCodeGeneratorState codeGeneratorState,
            IEntityBuilder builder,
            Entity entity)
        {
            if (entity.Keys is { Count: > 0 })
            {
                var keysPropertyNames = new List<string>(entity.Keys.Count);
                foreach (var key in entity.Keys)
                {
                    if (key.Type == NoxType.EntityId) //its key and foreign key
                    {
                        Console.WriteLine($"    Setup Key {key.Name} as Foreign Key for Entity {entity.Name}");

                        ConfigureEntityKeyForEntityForeignKey(codeGeneratorState, builder, entity, key);
                        keysPropertyNames.Add(key.Name);
                    }
                    else if (TypesDatabaseConfigurations.TryGetValue(key.Type,
                            out var databaseConfiguration))
                    {
                        Console.WriteLine($"    Setup Key {key.Name} for Entity {entity.Name}");
                        keysPropertyNames.Add(databaseConfiguration.GetKeyPropertyName(key));
                        databaseConfiguration.ConfigureEntityProperty(codeGeneratorState, builder, key, entity, true);
                    }
                    else
                    {
                        throw new Exception($"Could not find DatabaseConfigurator for type {key.Type} entity {entity.Name}");
                    }
                }

                builder.HasKey(keysPropertyNames.ToArray());
            }
        }

        private void ConfigureEntityKeyForEntityForeignKey(
            NoxSolutionCodeGeneratorState codeGeneratorState,
            IEntityBuilder builder,
            Entity entity,
            NoxSimpleTypeDefinition key)
        {
            // Key type of the Foreign Entity Key
            var foreignEntityKeyType = codeGeneratorState.Solution.GetSingleKeyTypeForEntity(key.EntityIdTypeOptions!.Entity);

            builder
            .HasOne(key.EntityIdTypeOptions!.Entity)
            .WithOne()
            .HasForeignKey(entity.Name, key.Name);

            //Configure foreign key property
            if (TypesDatabaseConfigurations.TryGetValue(foreignEntityKeyType,
                out var databaseConfigurationForForeignKey))
            {
                var foreignEntityKeyDefinition = codeGeneratorState.Solution.Domain!.GetEntityByName(key.EntityIdTypeOptions!.Entity).Keys![0].ShallowCopy();
                foreignEntityKeyDefinition.Name = key.Name;
                foreignEntityKeyDefinition.Description = "-";
                foreignEntityKeyDefinition.IsRequired = false;
                foreignEntityKeyDefinition.IsReadonly = false;
                databaseConfigurationForForeignKey.ConfigureEntityProperty(codeGeneratorState, builder, foreignEntityKeyDefinition, entity, false);
            }
        }

        private void ConfigureAttributes(
            NoxSolutionCodeGeneratorState codeGeneratorState,
            IEntityBuilder builder,
            Entity entity)
        {
            var allEntityAttributes = GetAllEntityAttributes(entity);

            foreach (var property in allEntityAttributes)
            {
                if (TypesDatabaseConfigurations.TryGetValue(property.Type, out var databaseConfiguration))
                {
                    databaseConfiguration.ConfigureEntityProperty(codeGeneratorState, builder, property, entity, false);
                }
                else
                {
                    Debug.WriteLine($"Type {property.Type} not found");
                }
            }

        }

        private static List<NoxSimpleTypeDefinition> GetAllEntityAttributes(Entity entity)
        {
            var totalCapacity = entity.Attributes?.Count ?? 0 + AuditableEntityAttributes.Length;
            var allEntityAttributes = new List<NoxSimpleTypeDefinition>(totalCapacity);

            if (entity.Attributes is { Count: > 0 })
            {
                allEntityAttributes.AddRange(entity.Attributes);
            }

            // TODO clarify Auditable for owned entities
            if (entity.Persistence?.IsAudited == true && !entity.IsOwnedEntity)
            {
                allEntityAttributes.AddRange(AuditableEntityAttributes);
            }

            return allEntityAttributes;
        }
    }
}