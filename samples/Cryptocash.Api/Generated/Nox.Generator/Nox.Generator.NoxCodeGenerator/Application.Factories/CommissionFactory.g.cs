﻿
// Generated
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
using CommissionEntity = Cryptocash.Domain.Commission;

namespace Cryptocash.Application.Factories;

internal partial class CommissionFactory : CommissionFactoryBase
{
    public CommissionFactory
    (
    ) : base()
    {}
}

internal abstract class CommissionFactoryBase : IEntityFactory<CommissionEntity, CommissionCreateDto, CommissionUpdateDto>
{

    public CommissionFactoryBase(
        )
    {
    }

    public virtual async Task<CommissionEntity> CreateEntityAsync(CommissionCreateDto createDto, Nox.Types.CultureCode cultureCode)
    {
<<<<<<< main
        return await ToEntityAsync(createDto);
=======
        try
        {
            var entity =  await ToEntityAsync(createDto, cultureCode);
            return entity;
        }
        catch (NoxTypeValidationException ex)
        {
            throw new Nox.Application.Factories.CreateUpdateEntityInvalidDataException(ex);
        }        
>>>>>>> Factory classes refactor has been completed (without tests)
    }

    public virtual async Task UpdateEntityAsync(CommissionEntity entity, CommissionUpdateDto updateDto, Nox.Types.CultureCode cultureCode)
    {
        await UpdateEntityInternalAsync(entity, updateDto, cultureCode);
    }

    public virtual async Task PartialUpdateEntityAsync(CommissionEntity entity, Dictionary<string, dynamic> updatedProperties, Nox.Types.CultureCode cultureCode)
    {
<<<<<<< main
        PartialUpdateEntityInternal(entity, updatedProperties, cultureCode);
=======
<<<<<<< main
        try
        {
             PartialUpdateEntityInternal(entity, updatedProperties, cultureCode);
        }
        catch (NoxTypeValidationException ex)
        {
            throw new Nox.Application.Factories.CreateUpdateEntityInvalidDataException(ex);
        }   
=======
        PartialUpdateEntityInternal(entity, updatedProperties, cultureCode);
        await Task.CompletedTask;
>>>>>>> Factory classes refactor has been completed (without tests)
>>>>>>> Factory classes refactor has been completed (without tests)
    }

    private async Task<Cryptocash.Domain.Commission> ToEntityAsync(CommissionCreateDto createDto, Nox.Types.CultureCode cultureCode)
    {
        ExceptionCollector<NoxTypeValidationException> exceptionCollector = new();
        var entity = new Cryptocash.Domain.Commission();
        exceptionCollector.Collect("Rate", () => entity.SetIfNotNull(createDto.Rate, (entity) => entity.Rate = 
            Cryptocash.Domain.CommissionMetadata.CreateRate(createDto.Rate.NonNullValue<System.Single>())));
        exceptionCollector.Collect("EffectiveAt", () => entity.SetIfNotNull(createDto.EffectiveAt, (entity) => entity.EffectiveAt = 
            Cryptocash.Domain.CommissionMetadata.CreateEffectiveAt(createDto.EffectiveAt.NonNullValue<System.DateTimeOffset>())));

        CreateUpdateEntityInvalidDataException.ThrowIfAnyNoxTypeValidationException(exceptionCollector.ValidationErrors);
        entity.EnsureId(createDto.Id);        
        return await Task.FromResult(entity);
    }

    private async Task UpdateEntityInternalAsync(CommissionEntity entity, CommissionUpdateDto updateDto, Nox.Types.CultureCode cultureCode)
    {
        ExceptionCollector<NoxTypeValidationException> exceptionCollector = new();
        exceptionCollector.Collect("Rate",() => entity.Rate = Cryptocash.Domain.CommissionMetadata.CreateRate(updateDto.Rate.NonNullValue<System.Single>()));
        exceptionCollector.Collect("EffectiveAt",() => entity.EffectiveAt = Cryptocash.Domain.CommissionMetadata.CreateEffectiveAt(updateDto.EffectiveAt.NonNullValue<System.DateTimeOffset>()));

        CreateUpdateEntityInvalidDataException.ThrowIfAnyNoxTypeValidationException(exceptionCollector.ValidationErrors);
        await Task.CompletedTask;
    }

    private void PartialUpdateEntityInternal(CommissionEntity entity, Dictionary<string, dynamic> updatedProperties, Nox.Types.CultureCode cultureCode)
    {
        ExceptionCollector<NoxTypeValidationException> exceptionCollector = new();

        if (updatedProperties.TryGetValue("Rate", out var RateUpdateValue))
        {
            ArgumentNullException.ThrowIfNull(RateUpdateValue, "Attribute 'Rate' can't be null.");
            {
                exceptionCollector.Collect("Rate",() =>entity.Rate = Cryptocash.Domain.CommissionMetadata.CreateRate(RateUpdateValue));
            }
        }

        if (updatedProperties.TryGetValue("EffectiveAt", out var EffectiveAtUpdateValue))
        {
            ArgumentNullException.ThrowIfNull(EffectiveAtUpdateValue, "Attribute 'EffectiveAt' can't be null.");
            {
                exceptionCollector.Collect("EffectiveAt",() =>entity.EffectiveAt = Cryptocash.Domain.CommissionMetadata.CreateEffectiveAt(EffectiveAtUpdateValue));
            }
        }
        CreateUpdateEntityInvalidDataException.ThrowIfAnyNoxTypeValidationException(exceptionCollector.ValidationErrors);
    }
}