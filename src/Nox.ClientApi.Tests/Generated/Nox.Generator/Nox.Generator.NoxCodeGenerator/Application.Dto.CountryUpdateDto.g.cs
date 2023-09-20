﻿// Generated

#nullable enable

using Nox.Abstractions;
using Nox.Application.Dto;
using Nox.Types;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ClientApi.Domain;

namespace ClientApi.Application.Dto;

/// <summary>
/// Country Entity.
/// </summary>
public partial class CountryUpdateDto : IEntityDto<Country>
{
    /// <summary>
    /// The Country Name (Required).
    /// </summary>
    [Required(ErrorMessage = "Name is required")]
    
    public System.String Name { get; set; } = default!;
    /// <summary>
    /// Population (Optional).
    /// </summary>
    public System.Int32? Population { get; set; }
    /// <summary>
    /// The Money (Optional).
    /// </summary>
    public MoneyDto? CountryDebt { get; set; }
    /// <summary>
    /// First Official Language (Optional).
    /// </summary>
    public System.String? FirstLanguageCode { get; set; }
    /// <summary>
    /// Country is also coded as ZeroOrOne CountryBarCodes
    /// </summary>
    public CountryBarCodeUpdateDto? CountryBarCode { get; set; } = null!;
}