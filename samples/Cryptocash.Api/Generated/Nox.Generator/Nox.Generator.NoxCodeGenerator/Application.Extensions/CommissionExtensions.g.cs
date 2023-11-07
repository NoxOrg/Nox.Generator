﻿// Generated

#nullable enable
using System;
using System.Linq;

using Nox.Extensions;

namespace Cryptocash.Application.Dto;

internal static class CommissionExtensions
{
    public static CommissionDto ToDto(this Cryptocash.Domain.Commission entity)
    {
        var dto = new CommissionDto();
        dto.SetIfNotNull(entity?.Id, (dto) => dto.Id = entity!.Id.Value);
        dto.SetIfNotNull(entity?.Rate, (dto) => dto.Rate =entity!.Rate!.Value);
        dto.SetIfNotNull(entity?.EffectiveAt, (dto) => dto.EffectiveAt =entity!.EffectiveAt!.Value);
        dto.SetIfNotNull(entity?.CountryId, (dto) => dto.CountryId = entity!.CountryId!.Value);
        dto.SetIfNotNull(entity?.Bookings, (dto) => dto.Bookings = entity!.Bookings.Select(e => e.ToDto()).ToList());

        return dto;
    }
}