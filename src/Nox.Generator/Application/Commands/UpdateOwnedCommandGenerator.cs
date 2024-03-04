﻿using Microsoft.CodeAnalysis;
using Nox.Generator.Common;
using Nox.Solution;
using Nox.Solution.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Nox.Generator.Application.Commands;

internal class UpdateOwnedCommandGenerator : ApplicationEntityDependentGeneratorBase
{
    protected override void DoGenerate(SourceProductionContext context, NoxCodeGenConventions codeGenConventions, IEnumerable<Entity> entities)
    {
        foreach (var entity in entities)
        {
            context.CancellationToken.ThrowIfCancellationRequested();

            foreach (var ownedRelationship in entity.OwnedRelationships)
            {
                var templateName = ownedRelationship.WithSingleEntity
                    ? "Application.Commands.UpdateOwnedSingleCommand"
                    : "Application.Commands.UpdateOwnedManyCommand";

                var ownedEntity = entities.Single(entity => entity.Name == ownedRelationship.Entity);
                var relationshipName = entity.GetNavigationPropertyName(ownedRelationship);

                var primaryKeysReturnQuery = string.Join(", ", ownedEntity.Keys.Select(k => $"entity.{k.Name}.Value"));
                var parentKeysFindQuery = string.Join(", ", entity.Keys.Select(k => $"key{k.Name}"));
                var ownedKeysFindQuery = string.Join(" && ", ownedEntity.Keys.Select(k => $"x.{k.Name} == owned{k.Name}"));

                new TemplateCodeBuilder(context, codeGenConventions)
                    .WithClassName($"Update{relationshipName}For{entity.Name}Command")
                    .WithFileNamePrefix($"Application.Commands")
                    .WithObject("relationship", ownedRelationship)
                    .WithObject("entity", ownedEntity)
                    .WithObject("entityKeys", ownedEntity.GetKeys())
                    .WithObject("parent", entity)
                    .WithObject("primaryKeysReturnQuery", primaryKeysReturnQuery)
                    .WithObject("parentKeysFindQuery", parentKeysFindQuery)
                    .WithObject("ownedKeysFindQuery", ownedKeysFindQuery)
                    .GenerateSourceCodeFromResource(templateName);
            }
        }
    }
}