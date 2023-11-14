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
/// Vending machine definition and related data
/// </summary>
public partial class VendingMachineUpdateDto : IEntityDto<DomainNamespace.VendingMachine>
{
    /// <summary>
    /// Vending machine mac address 
    /// <remarks>Required.</remarks>    
    /// </summary>
    [Required(ErrorMessage = "MacAddress is required")]
    
    public System.String MacAddress { get; set; } = default!;
    /// <summary>
    /// Vending machine public ip 
    /// <remarks>Required.</remarks>    
    /// </summary>
    [Required(ErrorMessage = "PublicIp is required")]
    
    public System.String PublicIp { get; set; } = default!;
    /// <summary>
    /// Vending machine geo location 
    /// <remarks>Required.</remarks>    
    /// </summary>
    [Required(ErrorMessage = "GeoLocation is required")]
    
    public LatLongDto GeoLocation { get; set; } = default!;
    /// <summary>
    /// Vending machine street address 
    /// <remarks>Required.</remarks>    
    /// </summary>
    [Required(ErrorMessage = "StreetAddress is required")]
    
    public StreetAddressDto StreetAddress { get; set; } = default!;
    /// <summary>
    /// Vending machine serial number 
    /// <remarks>Required.</remarks>    
    /// </summary>
    [Required(ErrorMessage = "SerialNumber is required")]
    
    public System.String SerialNumber { get; set; } = default!;
    /// <summary>
    /// Vending machine installation area 
    /// <remarks>Optional.</remarks>    
    /// </summary>
    public System.Decimal? InstallationFootPrint { get; set; }
    /// <summary>
    /// Landlord rent amount based on area of the vending machine installation 
    /// <remarks>Optional.</remarks>    
    /// </summary>
    public MoneyDto? RentPerSquareMetre { get; set; }

    /// <summary>
    /// VendingMachine installed in ExactlyOne Countries
    /// </summary>
    [Required(ErrorMessage = "Country is required")]
    public System.String CountryId { get; set; } = default!;

    /// <summary>
    /// VendingMachine contracted area leased by ExactlyOne LandLords
    /// </summary>
    [Required(ErrorMessage = "LandLord is required")]
    public System.Int64 LandLordId { get; set; } = default!;
}