﻿// Generated

#nullable enable

using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using MediatR;

using Nox.Abstractions;
using Nox.Solution;
using Nox.Domain;
using Nox.Application.Factories;
using Nox.Types;
using Nox.Application;
using Nox.Extensions;
using Nox.Exceptions;

using TestWebApp.Application.Dto;
using TestWebApp.Domain;
using SecondTestEntityTwoRelationshipsOneToOneEntity = TestWebApp.Domain.SecondTestEntityTwoRelationshipsOneToOne;

namespace TestWebApp.Application.Factories;

internal partial class SecondTestEntityTwoRelationshipsOneToOneFactory : SecondTestEntityTwoRelationshipsOneToOneFactoryBase
{
    public SecondTestEntityTwoRelationshipsOneToOneFactory
    (
        IRepository repository
    ) : base( repository)
    {}
}

internal abstract class SecondTestEntityTwoRelationshipsOneToOneFactoryBase : IEntityFactory<SecondTestEntityTwoRelationshipsOneToOneEntity, SecondTestEntityTwoRelationshipsOneToOneCreateDto, SecondTestEntityTwoRelationshipsOneToOneUpdateDto>
{
    private static readonly Nox.Types.CultureCode _defaultCultureCode = Nox.Types.CultureCode.From("en-US");
    private readonly IRepository _repository;

    public SecondTestEntityTwoRelationshipsOneToOneFactoryBase(
        IRepository repository
        )
    {
        _repository = repository;
    }

    public virtual async Task<SecondTestEntityTwoRelationshipsOneToOneEntity> CreateEntityAsync(SecondTestEntityTwoRelationshipsOneToOneCreateDto createDto)
    {
        try
        {
            return await ToEntityAsync(createDto);
        }
        catch (NoxTypeValidationException ex)
        {
            throw new Nox.Application.Factories.CreateUpdateEntityInvalidDataException(ex);
        }        
    }

    public virtual async Task UpdateEntityAsync(SecondTestEntityTwoRelationshipsOneToOneEntity entity, SecondTestEntityTwoRelationshipsOneToOneUpdateDto updateDto, Nox.Types.CultureCode cultureCode)
    {
        await UpdateEntityInternalAsync(entity, updateDto, cultureCode);
    }

    public virtual void PartialUpdateEntity(SecondTestEntityTwoRelationshipsOneToOneEntity entity, Dictionary<string, dynamic> updatedProperties, Nox.Types.CultureCode cultureCode)
    {
        PartialUpdateEntityInternal(entity, updatedProperties, cultureCode);
    }

    private async Task<TestWebApp.Domain.SecondTestEntityTwoRelationshipsOneToOne> ToEntityAsync(SecondTestEntityTwoRelationshipsOneToOneCreateDto createDto)
    {
        var entity = new TestWebApp.Domain.SecondTestEntityTwoRelationshipsOneToOne();
        entity.Id = SecondTestEntityTwoRelationshipsOneToOneMetadata.CreateId(createDto.Id);
        entity.TextTestField2 = TestWebApp.Domain.SecondTestEntityTwoRelationshipsOneToOneMetadata.CreateTextTestField2(createDto.TextTestField2);
        return await Task.FromResult(entity);
    }

    private async Task UpdateEntityInternalAsync(SecondTestEntityTwoRelationshipsOneToOneEntity entity, SecondTestEntityTwoRelationshipsOneToOneUpdateDto updateDto, Nox.Types.CultureCode cultureCode)
    {
        entity.TextTestField2 = TestWebApp.Domain.SecondTestEntityTwoRelationshipsOneToOneMetadata.CreateTextTestField2(updateDto.TextTestField2.NonNullValue<System.String>());
        await Task.CompletedTask;
    }

    private void PartialUpdateEntityInternal(SecondTestEntityTwoRelationshipsOneToOneEntity entity, Dictionary<string, dynamic> updatedProperties, Nox.Types.CultureCode cultureCode)
    {

        if (updatedProperties.TryGetValue("TextTestField2", out var TextTestField2UpdateValue))
        {
            if (TextTestField2UpdateValue == null)
            {
                throw new ArgumentException("Attribute 'TextTestField2' can't be null");
            }
            {
                entity.TextTestField2 = TestWebApp.Domain.SecondTestEntityTwoRelationshipsOneToOneMetadata.CreateTextTestField2(TextTestField2UpdateValue);
            }
        }
    }

    private static bool IsDefaultCultureCode(Nox.Types.CultureCode cultureCode)
        => cultureCode == _defaultCultureCode;
}