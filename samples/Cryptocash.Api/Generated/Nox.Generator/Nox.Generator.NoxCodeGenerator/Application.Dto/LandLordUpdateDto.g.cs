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
/// Landlord related data.
/// </summary>
public partial class LandLordUpdateDto : LandLordUpdateDtoBase
{

}

/// <summary>
/// Landlord related data
/// </summary>
public partial class LandLordUpdateDtoBase: EntityDtoBase, IEntityDto<DomainNamespace.LandLord>
{
    /// <summary>
    /// Landlord name 
    /// <remarks>Required.</remarks>    
    /// </summary>
    [Required(ErrorMessage = "Name is required")]
    
    public virtual System.String Name { get; set; } = default!;
    /// <summary>
    /// Landlord's street address 
    /// <remarks>Required.</remarks>    
    /// </summary>
    [Required(ErrorMessage = "Address is required")]
    
    public virtual StreetAddressDto Address { get; set; } = default!;

    /// <summary>
    /// LandLord leases an area to house ZeroOrMany VendingMachines
    /// </summary>
    public virtual List<System.Guid> VendingMachinesId { get; set; } = new();
}