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
using Booking = Cryptocash.Domain.Booking;

namespace Cryptocash.Application.Factories;

internal abstract class BookingFactoryBase : IEntityFactory<Booking, BookingCreateDto, BookingUpdateDto>
{

    public BookingFactoryBase
    (
        )
    {
    }

    public virtual Booking CreateEntity(BookingCreateDto createDto)
    {
        return ToEntity(createDto);
    }

    public virtual void UpdateEntity(Booking entity, BookingUpdateDto updateDto)
    {
        UpdateEntityInternal(entity, updateDto);
    }

    public virtual void PartialUpdateEntity(Booking entity, Dictionary<string, dynamic> updatedProperties)
    {
        PartialUpdateEntityInternal(entity, updatedProperties);
    }

    private Cryptocash.Domain.Booking ToEntity(BookingCreateDto createDto)
    {
        var entity = new Cryptocash.Domain.Booking();
        entity.AmountFrom = Cryptocash.Domain.BookingMetadata.CreateAmountFrom(createDto.AmountFrom);
        entity.AmountTo = Cryptocash.Domain.BookingMetadata.CreateAmountTo(createDto.AmountTo);
        entity.RequestedPickUpDate = Cryptocash.Domain.BookingMetadata.CreateRequestedPickUpDate(createDto.RequestedPickUpDate);
        if (createDto.PickedUpDateTime is not null)entity.PickedUpDateTime = Cryptocash.Domain.BookingMetadata.CreatePickedUpDateTime(createDto.PickedUpDateTime.NonNullValue<DateTimeRangeDto>());
        if (createDto.ExpiryDateTime is not null)entity.ExpiryDateTime = Cryptocash.Domain.BookingMetadata.CreateExpiryDateTime(createDto.ExpiryDateTime.NonNullValue<System.DateTimeOffset>());
        if (createDto.CancelledDateTime is not null)entity.CancelledDateTime = Cryptocash.Domain.BookingMetadata.CreateCancelledDateTime(createDto.CancelledDateTime.NonNullValue<System.DateTimeOffset>());
        if (createDto.VatNumber is not null)entity.VatNumber = Cryptocash.Domain.BookingMetadata.CreateVatNumber(createDto.VatNumber.NonNullValue<VatNumberDto>());
        entity.EnsureId(createDto.Id);
        return entity;
    }

    private void UpdateEntityInternal(Booking entity, BookingUpdateDto updateDto)
    {
        entity.AmountFrom = Cryptocash.Domain.BookingMetadata.CreateAmountFrom(updateDto.AmountFrom.NonNullValue<MoneyDto>());
        entity.AmountTo = Cryptocash.Domain.BookingMetadata.CreateAmountTo(updateDto.AmountTo.NonNullValue<MoneyDto>());
        entity.RequestedPickUpDate = Cryptocash.Domain.BookingMetadata.CreateRequestedPickUpDate(updateDto.RequestedPickUpDate.NonNullValue<DateTimeRangeDto>());
        if (updateDto.PickedUpDateTime == null) { entity.PickedUpDateTime = null; } else {
            entity.PickedUpDateTime = Cryptocash.Domain.BookingMetadata.CreatePickedUpDateTime(updateDto.PickedUpDateTime.ToValueFromNonNull<DateTimeRangeDto>());
        }
        if (updateDto.ExpiryDateTime == null) { entity.ExpiryDateTime = null; } else {
            entity.ExpiryDateTime = Cryptocash.Domain.BookingMetadata.CreateExpiryDateTime(updateDto.ExpiryDateTime.ToValueFromNonNull<System.DateTimeOffset>());
        }
        if (updateDto.CancelledDateTime == null) { entity.CancelledDateTime = null; } else {
            entity.CancelledDateTime = Cryptocash.Domain.BookingMetadata.CreateCancelledDateTime(updateDto.CancelledDateTime.ToValueFromNonNull<System.DateTimeOffset>());
        }
        if (updateDto.VatNumber == null) { entity.VatNumber = null; } else {
            entity.VatNumber = Cryptocash.Domain.BookingMetadata.CreateVatNumber(updateDto.VatNumber.ToValueFromNonNull<VatNumberDto>());
        }
    }

    private void PartialUpdateEntityInternal(Booking entity, Dictionary<string, dynamic> updatedProperties)
    {

        if (updatedProperties.TryGetValue("AmountFrom", out var AmountFromUpdateValue))
        {
            if (AmountFromUpdateValue == null)
            {
                throw new ArgumentException("Attribute 'AmountFrom' can't be null");
            }
            {
                entity.AmountFrom = Cryptocash.Domain.BookingMetadata.CreateAmountFrom(AmountFromUpdateValue);
            }
        }

        if (updatedProperties.TryGetValue("AmountTo", out var AmountToUpdateValue))
        {
            if (AmountToUpdateValue == null)
            {
                throw new ArgumentException("Attribute 'AmountTo' can't be null");
            }
            {
                entity.AmountTo = Cryptocash.Domain.BookingMetadata.CreateAmountTo(AmountToUpdateValue);
            }
        }

        if (updatedProperties.TryGetValue("RequestedPickUpDate", out var RequestedPickUpDateUpdateValue))
        {
            if (RequestedPickUpDateUpdateValue == null)
            {
                throw new ArgumentException("Attribute 'RequestedPickUpDate' can't be null");
            }
            {
                entity.RequestedPickUpDate = Cryptocash.Domain.BookingMetadata.CreateRequestedPickUpDate(RequestedPickUpDateUpdateValue);
            }
        }

        if (updatedProperties.TryGetValue("PickedUpDateTime", out var PickedUpDateTimeUpdateValue))
        {
            if (PickedUpDateTimeUpdateValue == null) { entity.PickedUpDateTime = null; }
            else
            {
                entity.PickedUpDateTime = Cryptocash.Domain.BookingMetadata.CreatePickedUpDateTime(PickedUpDateTimeUpdateValue);
            }
        }

        if (updatedProperties.TryGetValue("ExpiryDateTime", out var ExpiryDateTimeUpdateValue))
        {
            if (ExpiryDateTimeUpdateValue == null) { entity.ExpiryDateTime = null; }
            else
            {
                entity.ExpiryDateTime = Cryptocash.Domain.BookingMetadata.CreateExpiryDateTime(ExpiryDateTimeUpdateValue);
            }
        }

        if (updatedProperties.TryGetValue("CancelledDateTime", out var CancelledDateTimeUpdateValue))
        {
            if (CancelledDateTimeUpdateValue == null) { entity.CancelledDateTime = null; }
            else
            {
                entity.CancelledDateTime = Cryptocash.Domain.BookingMetadata.CreateCancelledDateTime(CancelledDateTimeUpdateValue);
            }
        }

        if (updatedProperties.TryGetValue("VatNumber", out var VatNumberUpdateValue))
        {
            if (VatNumberUpdateValue == null) { entity.VatNumber = null; }
            else
            {
                entity.VatNumber = Cryptocash.Domain.BookingMetadata.CreateVatNumber(VatNumberUpdateValue);
            }
        }
    }
}

internal partial class BookingFactory : BookingFactoryBase
{
}