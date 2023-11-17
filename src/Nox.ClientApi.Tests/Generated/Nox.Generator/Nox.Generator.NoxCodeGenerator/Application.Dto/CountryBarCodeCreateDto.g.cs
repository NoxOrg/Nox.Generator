﻿// Generated

#nullable enable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Nox.Abstractions;
using Nox.Application.Dto;
using Nox.Domain;
using Nox.Extensions;
using Nox.Types;

using DomainNamespace = ClientApi.Domain;

namespace ClientApi.Application.Dto;

/// <summary>
/// Bar code for country.
/// </summary>
public partial class CountryBarCodeCreateDto : CountryBarCodeCreateDtoBase
{

}

/// <summary>
/// Bar code for country.
/// </summary>
public abstract class CountryBarCodeCreateDtoBase : IEntityDto<DomainNamespace.CountryBarCode>
{
    /// <summary>
    /// Bar code name     
    /// </summary>
    /// <remarks>Required</remarks>
    [Required(ErrorMessage = "BarCodeName is required")]
    
    public virtual System.String BarCodeName { get; set; } = default!;
    /// <summary>
    /// Bar code number     
    /// </summary>
    /// <remarks>Optional</remarks>
    public virtual System.Int32? BarCodeNumber { get; set; }
}