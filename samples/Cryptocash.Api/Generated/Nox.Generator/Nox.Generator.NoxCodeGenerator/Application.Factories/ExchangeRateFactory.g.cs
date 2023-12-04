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

using Cryptocash.Application.Dto;
using Cryptocash.Domain;
using ExchangeRateEntity = Cryptocash.Domain.ExchangeRate;

namespace Cryptocash.Application.Factories;

internal abstract class ExchangeRateFactoryBase : IEntityFactory<ExchangeRateEntity, ExchangeRateUpsertDto, ExchangeRateUpsertDto>
{
    private static readonly Nox.Types.CultureCode _defaultCultureCode = Nox.Types.CultureCode.From("en-US");
    private readonly IRepository _repository;

    public ExchangeRateFactoryBase
    (
        IRepository repository
        )
    {
        _repository = repository;
    }

    public virtual ExchangeRateEntity CreateEntity(ExchangeRateUpsertDto createDto)
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

    public virtual void UpdateEntity(ExchangeRateEntity entity, ExchangeRateUpsertDto updateDto, Nox.Types.CultureCode cultureCode)
    {
        UpdateEntityInternal(entity, updateDto, cultureCode);
    }

    public virtual void PartialUpdateEntity(ExchangeRateEntity entity, Dictionary<string, dynamic> updatedProperties, Nox.Types.CultureCode cultureCode)
    {
        PartialUpdateEntityInternal(entity, updatedProperties, cultureCode);
    }

    private Cryptocash.Domain.ExchangeRate ToEntity(ExchangeRateUpsertDto createDto)
    {
        var entity = new Cryptocash.Domain.ExchangeRate();
        entity.EffectiveRate = Cryptocash.Domain.ExchangeRateMetadata.CreateEffectiveRate(createDto.EffectiveRate);
        entity.EffectiveAt = Cryptocash.Domain.ExchangeRateMetadata.CreateEffectiveAt(createDto.EffectiveAt);
        return entity;
    }

    private void UpdateEntityInternal(ExchangeRateEntity entity, ExchangeRateUpsertDto updateDto, Nox.Types.CultureCode cultureCode)
    {
        entity.EffectiveRate = Cryptocash.Domain.ExchangeRateMetadata.CreateEffectiveRate(updateDto.EffectiveRate.NonNullValue<System.Int32>());
        entity.EffectiveAt = Cryptocash.Domain.ExchangeRateMetadata.CreateEffectiveAt(updateDto.EffectiveAt.NonNullValue<System.DateTimeOffset>());
    }

    private void PartialUpdateEntityInternal(ExchangeRateEntity entity, Dictionary<string, dynamic> updatedProperties, Nox.Types.CultureCode cultureCode)
    {

        if (updatedProperties.TryGetValue("EffectiveRate", out var EffectiveRateUpdateValue))
        {
            if (EffectiveRateUpdateValue == null)
            {
                throw new ArgumentException("Attribute 'EffectiveRate' can't be null");
            }
            {
                entity.EffectiveRate = Cryptocash.Domain.ExchangeRateMetadata.CreateEffectiveRate(EffectiveRateUpdateValue);
            }
        }

        if (updatedProperties.TryGetValue("EffectiveAt", out var EffectiveAtUpdateValue))
        {
            if (EffectiveAtUpdateValue == null)
            {
                throw new ArgumentException("Attribute 'EffectiveAt' can't be null");
            }
            {
                entity.EffectiveAt = Cryptocash.Domain.ExchangeRateMetadata.CreateEffectiveAt(EffectiveAtUpdateValue);
            }
        }
    }

    private static bool IsDefaultCultureCode(Nox.Types.CultureCode cultureCode)
        => cultureCode == _defaultCultureCode;
}

internal partial class ExchangeRateFactory : ExchangeRateFactoryBase
{
    public ExchangeRateFactory
    (
        IRepository repository
    ) : base( repository)
    {}
}