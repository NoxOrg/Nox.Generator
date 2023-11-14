﻿// Generated

#nullable enable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Nox.Abstractions;
using Nox.Application.Dto;
using Nox.Types;

using DomainNamespace = Cryptocash.Domain;

namespace Cryptocash.Application.Dto;

/// <summary>
/// Vending machine definition and related data.
/// </summary>
public partial class VendingMachineUpdateDto : VendingMachineUpdateDtoBase
{

}

/// <summary>
/// Vending machine definition and related data
/// </summary>
public partial class VendingMachineUpdateDtoBase: EntityDtoBase, IEntityDto<DomainNamespace.VendingMachine>
{
    /// <summary>
    /// Vending machine mac address 
    /// <remarks>Required.</remarks>    
    /// </summary>
    [Required(ErrorMessage = "MacAddress is required")]
    
    public virtual System.String MacAddress { get; set; } = default!;
    /// <summary>
    /// Vending machine public ip 
    /// <remarks>Required.</remarks>    
    /// </summary>
    [Required(ErrorMessage = "PublicIp is required")]
    
    public virtual System.String PublicIp { get; set; } = default!;
    /// <summary>
    /// Vending machine geo location 
    /// <remarks>Required.</remarks>    
    /// </summary>
    [Required(ErrorMessage = "GeoLocation is required")]
    
    public virtual LatLongDto GeoLocation { get; set; } = default!;
    /// <summary>
    /// Vending machine street address 
    /// <remarks>Required.</remarks>    
    /// </summary>
    [Required(ErrorMessage = "StreetAddress is required")]
    
    public virtual StreetAddressDto StreetAddress { get; set; } = default!;
    /// <summary>
    /// Vending machine serial number 
    /// <remarks>Required.</remarks>    
    /// </summary>
    [Required(ErrorMessage = "SerialNumber is required")]
    
    public virtual System.String SerialNumber { get; set; } = default!;
    /// <summary>
    /// Vending machine installation area 
    /// <remarks>Optional.</remarks>    
    /// </summary>
    public virtual System.Decimal? InstallationFootPrint { get; set; }
    /// <summary>
    /// Landlord rent amount based on area of the vending machine installation 
    /// <remarks>Optional.</remarks>    
    /// </summary>
    public virtual MoneyDto? RentPerSquareMetre { get; set; }
}