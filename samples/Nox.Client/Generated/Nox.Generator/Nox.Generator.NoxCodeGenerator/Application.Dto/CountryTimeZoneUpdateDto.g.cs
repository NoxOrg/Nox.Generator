﻿// Generated

#nullable enable

using Nox.Abstractions;
using Nox.Application.Dto;
using Nox.Types;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Cryptocash.Domain;

using CountryTimeZoneEntity = Cryptocash.Domain.CountryTimeZone;
namespace Cryptocash.Application.Dto;

/// <summary>
/// Time zone related to country.
/// </summary>
public partial class CountryTimeZoneUpdateDto : IEntityDto<CountryTimeZoneEntity>
{
    /// <summary>
    /// Country's related time zone code (Required).
    /// </summary>
    [Required(ErrorMessage = "TimeZoneCode is required")]
    
    public System.String TimeZoneCode { get; set; } = default!;
}