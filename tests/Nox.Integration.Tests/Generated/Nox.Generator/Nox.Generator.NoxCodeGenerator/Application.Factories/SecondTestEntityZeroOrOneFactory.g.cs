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
using SecondTestEntityZeroOrOneEntity = TestWebApp.Domain.SecondTestEntityZeroOrOne;

namespace TestWebApp.Application.Factories;

internal abstract class SecondTestEntityZeroOrOneFactoryBase : IEntityFactory<SecondTestEntityZeroOrOneEntity, SecondTestEntityZeroOrOneCreateDto, SecondTestEntityZeroOrOneUpdateDto>
{

    public SecondTestEntityZeroOrOneFactoryBase
    (
        )
    {
    }

    public virtual SecondTestEntityZeroOrOneEntity CreateEntity(SecondTestEntityZeroOrOneCreateDto createDto)
    {
        return ToEntity(createDto);
    }

    public virtual void UpdateEntity(SecondTestEntityZeroOrOneEntity entity, SecondTestEntityZeroOrOneUpdateDto updateDto, Nox.Types.CultureCode cultureCode)
    {
        UpdateEntityInternal(entity, updateDto, cultureCode);
    }

    public virtual void PartialUpdateEntity(SecondTestEntityZeroOrOneEntity entity, Dictionary<string, dynamic> updatedProperties)
    {
        PartialUpdateEntityInternal(entity, updatedProperties);
    }

    private TestWebApp.Domain.SecondTestEntityZeroOrOne ToEntity(SecondTestEntityZeroOrOneCreateDto createDto)
    {
        var entity = new TestWebApp.Domain.SecondTestEntityZeroOrOne();
        entity.Id = SecondTestEntityZeroOrOneMetadata.CreateId(createDto.Id);
        entity.TextTestField2 = TestWebApp.Domain.SecondTestEntityZeroOrOneMetadata.CreateTextTestField2(createDto.TextTestField2);
        return entity;
    }

    private void UpdateEntityInternal(SecondTestEntityZeroOrOneEntity entity, SecondTestEntityZeroOrOneUpdateDto updateDto, Nox.Types.CultureCode cultureCode)
    {
        entity.TextTestField2 = TestWebApp.Domain.SecondTestEntityZeroOrOneMetadata.CreateTextTestField2(updateDto.TextTestField2.NonNullValue<System.String>());
    }

    private void PartialUpdateEntityInternal(SecondTestEntityZeroOrOneEntity entity, Dictionary<string, dynamic> updatedProperties)
    {

        if (updatedProperties.TryGetValue("TextTestField2", out var TextTestField2UpdateValue))
        {
            if (TextTestField2UpdateValue == null)
            {
                throw new ArgumentException("Attribute 'TextTestField2' can't be null");
            }
            {
                entity.TextTestField2 = TestWebApp.Domain.SecondTestEntityZeroOrOneMetadata.CreateTextTestField2(TextTestField2UpdateValue);
            }
        }
    }

    private static bool IsDefaultCultureCode(Nox.Types.CultureCode cultureCode)
        => cultureCode == Nox.Types.CultureCode.From("");
}

internal partial class SecondTestEntityZeroOrOneFactory : SecondTestEntityZeroOrOneFactoryBase
{
}