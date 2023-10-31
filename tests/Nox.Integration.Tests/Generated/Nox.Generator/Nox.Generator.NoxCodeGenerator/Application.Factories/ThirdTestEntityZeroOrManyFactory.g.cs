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
using ThirdTestEntityZeroOrManyEntity = TestWebApp.Domain.ThirdTestEntityZeroOrMany;

namespace TestWebApp.Application.Factories;

internal abstract class ThirdTestEntityZeroOrManyFactoryBase : IEntityFactory<ThirdTestEntityZeroOrManyEntity, ThirdTestEntityZeroOrManyCreateDto, ThirdTestEntityZeroOrManyUpdateDto>
{

    public ThirdTestEntityZeroOrManyFactoryBase
    (
        )
    {
    }

    public virtual ThirdTestEntityZeroOrManyEntity CreateEntity(ThirdTestEntityZeroOrManyCreateDto createDto)
    {
        return ToEntity(createDto);
    }

    public virtual void UpdateEntity(ThirdTestEntityZeroOrManyEntity entity, ThirdTestEntityZeroOrManyUpdateDto updateDto, Nox.Types.CultureCode cultureCode)
    {
        UpdateEntityInternal(entity, updateDto, cultureCode);
    }

    public virtual void PartialUpdateEntity(ThirdTestEntityZeroOrManyEntity entity, Dictionary<string, dynamic> updatedProperties)
    {
        PartialUpdateEntityInternal(entity, updatedProperties);
    }

    private TestWebApp.Domain.ThirdTestEntityZeroOrMany ToEntity(ThirdTestEntityZeroOrManyCreateDto createDto)
    {
        var entity = new TestWebApp.Domain.ThirdTestEntityZeroOrMany();
        entity.Id = ThirdTestEntityZeroOrManyMetadata.CreateId(createDto.Id);
        entity.TextTestField2 = TestWebApp.Domain.ThirdTestEntityZeroOrManyMetadata.CreateTextTestField2(createDto.TextTestField2);
        return entity;
    }

    private void UpdateEntityInternal(ThirdTestEntityZeroOrManyEntity entity, ThirdTestEntityZeroOrManyUpdateDto updateDto, Nox.Types.CultureCode cultureCode)
    {
        entity.TextTestField2 = TestWebApp.Domain.ThirdTestEntityZeroOrManyMetadata.CreateTextTestField2(updateDto.TextTestField2.NonNullValue<System.String>());
    }

    private void PartialUpdateEntityInternal(ThirdTestEntityZeroOrManyEntity entity, Dictionary<string, dynamic> updatedProperties)
    {

        if (updatedProperties.TryGetValue("TextTestField2", out var TextTestField2UpdateValue))
        {
            if (TextTestField2UpdateValue == null)
            {
                throw new ArgumentException("Attribute 'TextTestField2' can't be null");
            }
            {
                entity.TextTestField2 = TestWebApp.Domain.ThirdTestEntityZeroOrManyMetadata.CreateTextTestField2(TextTestField2UpdateValue);
            }
        }
    }

    private static bool IsDefaultCultureCode(Nox.Types.CultureCode cultureCode)
        => cultureCode == Nox.Types.CultureCode.From("");
}

internal partial class ThirdTestEntityZeroOrManyFactory : ThirdTestEntityZeroOrManyFactoryBase
{
}