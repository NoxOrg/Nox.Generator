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
using TestEntityOwnedRelationshipZeroOrManyEntity = TestWebApp.Domain.TestEntityOwnedRelationshipZeroOrMany;

namespace TestWebApp.Application.Factories;

internal abstract class TestEntityOwnedRelationshipZeroOrManyFactoryBase : IEntityFactory<TestEntityOwnedRelationshipZeroOrManyEntity, TestEntityOwnedRelationshipZeroOrManyCreateDto, TestEntityOwnedRelationshipZeroOrManyUpdateDto>
{
    private static readonly Nox.Types.CultureCode _defaultCultureCode = Nox.Types.CultureCode.From("en-US");
    private readonly IRepository _repository;
    protected IEntityFactory<TestWebApp.Domain.SecondTestEntityOwnedRelationshipZeroOrMany, SecondTestEntityOwnedRelationshipZeroOrManyUpsertDto, SecondTestEntityOwnedRelationshipZeroOrManyUpsertDto> SecondTestEntityOwnedRelationshipZeroOrManyFactory {get;}

    public TestEntityOwnedRelationshipZeroOrManyFactoryBase
    (
        IEntityFactory<TestWebApp.Domain.SecondTestEntityOwnedRelationshipZeroOrMany, SecondTestEntityOwnedRelationshipZeroOrManyUpsertDto, SecondTestEntityOwnedRelationshipZeroOrManyUpsertDto> secondtestentityownedrelationshipzeroormanyfactory,
        IRepository repository
        )
    {
        SecondTestEntityOwnedRelationshipZeroOrManyFactory = secondtestentityownedrelationshipzeroormanyfactory;
        _repository = repository;
    }

    public virtual TestEntityOwnedRelationshipZeroOrManyEntity CreateEntity(TestEntityOwnedRelationshipZeroOrManyCreateDto createDto)
    {
        try
        {
            return ToEntity(createDto);
        }
        catch (NoxTypeValidationException ex)
        {
            throw new Nox.Application.Factories.CreateUpdateEntityInvalidDataException(ex);
        }        
    }

    public virtual void UpdateEntity(TestEntityOwnedRelationshipZeroOrManyEntity entity, TestEntityOwnedRelationshipZeroOrManyUpdateDto updateDto, Nox.Types.CultureCode cultureCode)
    {
        UpdateEntityInternal(entity, updateDto, cultureCode);
    }

    public virtual void PartialUpdateEntity(TestEntityOwnedRelationshipZeroOrManyEntity entity, Dictionary<string, dynamic> updatedProperties, Nox.Types.CultureCode cultureCode)
    {
        PartialUpdateEntityInternal(entity, updatedProperties, cultureCode);
    }

    private TestWebApp.Domain.TestEntityOwnedRelationshipZeroOrMany ToEntity(TestEntityOwnedRelationshipZeroOrManyCreateDto createDto)
    {
        var entity = new TestWebApp.Domain.TestEntityOwnedRelationshipZeroOrMany();
        entity.Id = TestEntityOwnedRelationshipZeroOrManyMetadata.CreateId(createDto.Id);
        entity.TextTestField = TestWebApp.Domain.TestEntityOwnedRelationshipZeroOrManyMetadata.CreateTextTestField(createDto.TextTestField);
        createDto.SecondTestEntityOwnedRelationshipZeroOrManies.ForEach(dto => entity.CreateRefToSecondTestEntityOwnedRelationshipZeroOrManies(SecondTestEntityOwnedRelationshipZeroOrManyFactory.CreateEntity(dto)));
        return entity;
    }

    private void UpdateEntityInternal(TestEntityOwnedRelationshipZeroOrManyEntity entity, TestEntityOwnedRelationshipZeroOrManyUpdateDto updateDto, Nox.Types.CultureCode cultureCode)
    {
        entity.TextTestField = TestWebApp.Domain.TestEntityOwnedRelationshipZeroOrManyMetadata.CreateTextTestField(updateDto.TextTestField.NonNullValue<System.String>());
	    UpdateOwnedEntities(entity, updateDto, cultureCode);
    }

    private void PartialUpdateEntityInternal(TestEntityOwnedRelationshipZeroOrManyEntity entity, Dictionary<string, dynamic> updatedProperties, Nox.Types.CultureCode cultureCode)
    {

        if (updatedProperties.TryGetValue("TextTestField", out var TextTestFieldUpdateValue))
        {
            if (TextTestFieldUpdateValue == null)
            {
                throw new ArgumentException("Attribute 'TextTestField' can't be null");
            }
            {
                entity.TextTestField = TestWebApp.Domain.TestEntityOwnedRelationshipZeroOrManyMetadata.CreateTextTestField(TextTestFieldUpdateValue);
            }
        }
    }

    private static bool IsDefaultCultureCode(Nox.Types.CultureCode cultureCode)
        => cultureCode == _defaultCultureCode;

	private void UpdateOwnedEntities(TestEntityOwnedRelationshipZeroOrManyEntity entity, TestEntityOwnedRelationshipZeroOrManyUpdateDto updateDto, Nox.Types.CultureCode cultureCode)
	{
        if(!updateDto.SecondTestEntityOwnedRelationshipZeroOrManies.Any())
        { 
            _repository.DeleteOwnedRange(entity.SecondTestEntityOwnedRelationshipZeroOrManies);
			entity.DeleteAllRefToSecondTestEntityOwnedRelationshipZeroOrManies();
        }
		else
		{
			var updatedSecondTestEntityOwnedRelationshipZeroOrManies = new List<TestWebApp.Domain.SecondTestEntityOwnedRelationshipZeroOrMany>();
			foreach(var ownedUpsertDto in updateDto.SecondTestEntityOwnedRelationshipZeroOrManies)
			{
				if(ownedUpsertDto.Id is null)
					updatedSecondTestEntityOwnedRelationshipZeroOrManies.Add(SecondTestEntityOwnedRelationshipZeroOrManyFactory.CreateEntity(ownedUpsertDto));
				else
				{
					var key = TestWebApp.Domain.SecondTestEntityOwnedRelationshipZeroOrManyMetadata.CreateId(ownedUpsertDto.Id.NonNullValue<System.String>());
					var ownedEntity = entity.SecondTestEntityOwnedRelationshipZeroOrManies.FirstOrDefault(x => x.Id == key);
					if(ownedEntity is null)
						updatedSecondTestEntityOwnedRelationshipZeroOrManies.Add(SecondTestEntityOwnedRelationshipZeroOrManyFactory.CreateEntity(ownedUpsertDto));
					else
					{
						SecondTestEntityOwnedRelationshipZeroOrManyFactory.UpdateEntity(ownedEntity, ownedUpsertDto, cultureCode);
						updatedSecondTestEntityOwnedRelationshipZeroOrManies.Add(ownedEntity);
					}
				}
			}
            _repository.DeleteOwnedRange<TestWebApp.Domain.SecondTestEntityOwnedRelationshipZeroOrMany>(
                entity.SecondTestEntityOwnedRelationshipZeroOrManies.Where(x => !updatedSecondTestEntityOwnedRelationshipZeroOrManies.Any(upd => upd.Id == x.Id)).ToList());
			entity.UpdateRefToSecondTestEntityOwnedRelationshipZeroOrManies(updatedSecondTestEntityOwnedRelationshipZeroOrManies);
		}
	}
}

internal partial class TestEntityOwnedRelationshipZeroOrManyFactory : TestEntityOwnedRelationshipZeroOrManyFactoryBase
{
    public TestEntityOwnedRelationshipZeroOrManyFactory
    (
        IEntityFactory<TestWebApp.Domain.SecondTestEntityOwnedRelationshipZeroOrMany, SecondTestEntityOwnedRelationshipZeroOrManyUpsertDto, SecondTestEntityOwnedRelationshipZeroOrManyUpsertDto> secondtestentityownedrelationshipzeroormanyfactory,
        IRepository repository
    ) : base(secondtestentityownedrelationshipzeroormanyfactory, repository)
    {}
}