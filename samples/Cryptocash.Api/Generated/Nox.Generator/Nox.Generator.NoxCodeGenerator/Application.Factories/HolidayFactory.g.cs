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
using HolidayEntity = Cryptocash.Domain.Holiday;

namespace Cryptocash.Application.Factories;

internal partial class HolidayFactory : HolidayFactoryBase
{
    public HolidayFactory
    (
    ) : base()
    {}
}

internal abstract class HolidayFactoryBase : IEntityFactory<HolidayEntity, HolidayUpsertDto, HolidayUpsertDto>
{

    public HolidayFactoryBase(
        )
    {
    }

    public virtual async Task<HolidayEntity> CreateEntityAsync(HolidayUpsertDto createDto, Nox.Types.CultureCode cultureCode)
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

    public virtual async Task UpdateEntityAsync(HolidayEntity entity, HolidayUpsertDto updateDto, Nox.Types.CultureCode cultureCode)
    {
        await UpdateEntityInternalAsync(entity, updateDto, cultureCode);
    }

    public virtual async Task PartialUpdateEntityAsync(HolidayEntity entity, Dictionary<string, dynamic> updatedProperties, Nox.Types.CultureCode cultureCode)
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

    private async Task<Cryptocash.Domain.Holiday> ToEntityAsync(HolidayUpsertDto createDto, Nox.Types.CultureCode cultureCode)
    {
        ExceptionCollector<NoxTypeValidationException> exceptionCollector = new();
        var entity = new Cryptocash.Domain.Holiday();
        exceptionCollector.Collect("Name", () => entity.SetIfNotNull(createDto.Name, (entity) => entity.Name = 
            Cryptocash.Domain.HolidayMetadata.CreateName(createDto.Name.NonNullValue<System.String>())));
        exceptionCollector.Collect("Type", () => entity.SetIfNotNull(createDto.Type, (entity) => entity.Type = 
            Cryptocash.Domain.HolidayMetadata.CreateType(createDto.Type.NonNullValue<System.String>())));
        exceptionCollector.Collect("Date", () => entity.SetIfNotNull(createDto.Date, (entity) => entity.Date = 
            Cryptocash.Domain.HolidayMetadata.CreateDate(createDto.Date.NonNullValue<System.DateTime>())));

        CreateUpdateEntityInvalidDataException.ThrowIfAnyNoxTypeValidationException(exceptionCollector.ValidationErrors);        
        return await Task.FromResult(entity);
    }

    private async Task UpdateEntityInternalAsync(HolidayEntity entity, HolidayUpsertDto updateDto, Nox.Types.CultureCode cultureCode)
    {
        ExceptionCollector<NoxTypeValidationException> exceptionCollector = new();
        exceptionCollector.Collect("Name",() => entity.Name = Cryptocash.Domain.HolidayMetadata.CreateName(updateDto.Name.NonNullValue<System.String>()));
        exceptionCollector.Collect("Type",() => entity.Type = Cryptocash.Domain.HolidayMetadata.CreateType(updateDto.Type.NonNullValue<System.String>()));
        exceptionCollector.Collect("Date",() => entity.Date = Cryptocash.Domain.HolidayMetadata.CreateDate(updateDto.Date.NonNullValue<System.DateTime>()));

        CreateUpdateEntityInvalidDataException.ThrowIfAnyNoxTypeValidationException(exceptionCollector.ValidationErrors);
        await Task.CompletedTask;
    }

    private void PartialUpdateEntityInternal(HolidayEntity entity, Dictionary<string, dynamic> updatedProperties, Nox.Types.CultureCode cultureCode)
    {
        ExceptionCollector<NoxTypeValidationException> exceptionCollector = new();

        if (updatedProperties.TryGetValue("Name", out var NameUpdateValue))
        {
            ArgumentNullException.ThrowIfNull(NameUpdateValue, "Attribute 'Name' can't be null.");
            {
                exceptionCollector.Collect("Name",() =>entity.Name = Cryptocash.Domain.HolidayMetadata.CreateName(NameUpdateValue));
            }
        }

        if (updatedProperties.TryGetValue("Type", out var TypeUpdateValue))
        {
            ArgumentNullException.ThrowIfNull(TypeUpdateValue, "Attribute 'Type' can't be null.");
            {
                exceptionCollector.Collect("Type",() =>entity.Type = Cryptocash.Domain.HolidayMetadata.CreateType(TypeUpdateValue));
            }
        }

        if (updatedProperties.TryGetValue("Date", out var DateUpdateValue))
        {
            ArgumentNullException.ThrowIfNull(DateUpdateValue, "Attribute 'Date' can't be null.");
            {
                exceptionCollector.Collect("Date",() =>entity.Date = Cryptocash.Domain.HolidayMetadata.CreateDate(DateUpdateValue));
            }
        }
        CreateUpdateEntityInvalidDataException.ThrowIfAnyNoxTypeValidationException(exceptionCollector.ValidationErrors);
    }
}