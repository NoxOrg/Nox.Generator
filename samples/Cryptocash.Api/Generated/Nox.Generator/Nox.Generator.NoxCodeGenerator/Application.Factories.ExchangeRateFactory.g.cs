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

using Cryptocash.Application.Dto;
using Cryptocash.Domain;
using ExchangeRate = Cryptocash.Domain.ExchangeRate;

namespace Cryptocash.Application.Factories;

internal abstract class ExchangeRateFactoryBase : IEntityFactory<ExchangeRate, ExchangeRateCreateDto, ExchangeRateUpdateDto>
{

    public ExchangeRateFactoryBase
    (
        )
    {
    }

    public virtual ExchangeRate CreateEntity(ExchangeRateCreateDto createDto)
    {
        return ToEntity(createDto);
    }

    public virtual void UpdateEntity(ExchangeRate entity, ExchangeRateUpdateDto updateDto)
    {
        UpdateEntityInternal(entity, updateDto);
    }

    private Cryptocash.Domain.ExchangeRate ToEntity(ExchangeRateCreateDto createDto)
    {
        var entity = new Cryptocash.Domain.ExchangeRate();
        entity.EffectiveRate = Cryptocash.Domain.ExchangeRate.CreateEffectiveRate(createDto.EffectiveRate);
        entity.EffectiveAt = Cryptocash.Domain.ExchangeRate.CreateEffectiveAt(createDto.EffectiveAt);
        return entity;
    }

    private void UpdateEntityInternal(ExchangeRate entity, ExchangeRateUpdateDto updateDto)
    {
        entity.EffectiveRate = Cryptocash.Domain.ExchangeRate.CreateEffectiveRate(updateDto.EffectiveRate.NonNullValue<System.Int32>());
        entity.EffectiveAt = Cryptocash.Domain.ExchangeRate.CreateEffectiveAt(updateDto.EffectiveAt.NonNullValue<System.DateTimeOffset>());
    }
}

internal partial class ExchangeRateFactory : ExchangeRateFactoryBase
{
}