﻿// Generated

#nullable enable

using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using MediatR;

using Nox.Abstractions;
using Nox.Solution;
using Nox.Domain;
using Nox.Factories;
using Nox.Types;
using Nox.Application;
using Nox.Extensions;
using Nox.Exceptions;

using TestWebApp.Application.Dto;
using TestWebApp.Domain;
using ThirdTestEntityExactlyOneEntity = TestWebApp.Domain.ThirdTestEntityExactlyOne;

namespace TestWebApp.Application.Factories;

internal abstract class ThirdTestEntityExactlyOneFactoryBase : IEntityFactory<ThirdTestEntityExactlyOneEntity, ThirdTestEntityExactlyOneCreateDto, ThirdTestEntityExactlyOneUpdateDto>
{

    public ThirdTestEntityExactlyOneFactoryBase
    (
        )
    {
    }

    public virtual ThirdTestEntityExactlyOneEntity CreateEntity(ThirdTestEntityExactlyOneCreateDto createDto)
    {
        return ToEntity(createDto);
    }

    public virtual void UpdateEntity(ThirdTestEntityExactlyOneEntity entity, ThirdTestEntityExactlyOneUpdateDto updateDto)
    {
        UpdateEntityInternal(entity, updateDto);
    }

    public virtual void PartialUpdateEntity(ThirdTestEntityExactlyOneEntity entity, Dictionary<string, dynamic> updatedProperties)
    {
        PartialUpdateEntityInternal(entity, updatedProperties);
    }

    private TestWebApp.Domain.ThirdTestEntityExactlyOne ToEntity(ThirdTestEntityExactlyOneCreateDto createDto)
    {
        var entity = new TestWebApp.Domain.ThirdTestEntityExactlyOne();
        entity.Id = ThirdTestEntityExactlyOneMetadata.CreateId(createDto.Id);
        entity.TextTestField = TestWebApp.Domain.ThirdTestEntityExactlyOneMetadata.CreateTextTestField(createDto.TextTestField);
        return entity;
    }

    private void UpdateEntityInternal(ThirdTestEntityExactlyOneEntity entity, ThirdTestEntityExactlyOneUpdateDto updateDto)
    {
        entity.TextTestField = TestWebApp.Domain.ThirdTestEntityExactlyOneMetadata.CreateTextTestField(updateDto.TextTestField.NonNullValue<System.String>());
    }

    private void PartialUpdateEntityInternal(ThirdTestEntityExactlyOneEntity entity, Dictionary<string, dynamic> updatedProperties)
    {

        if (updatedProperties.TryGetValue("TextTestField", out var TextTestFieldUpdateValue))
        {
            if (TextTestFieldUpdateValue == null)
            {
                throw new ArgumentException("Attribute 'TextTestField' can't be null");
            }
            {
                entity.TextTestField = TestWebApp.Domain.ThirdTestEntityExactlyOneMetadata.CreateTextTestField(TextTestFieldUpdateValue);
            }
        }
    }
}

internal partial class ThirdTestEntityExactlyOneFactory : ThirdTestEntityExactlyOneFactoryBase
{
}