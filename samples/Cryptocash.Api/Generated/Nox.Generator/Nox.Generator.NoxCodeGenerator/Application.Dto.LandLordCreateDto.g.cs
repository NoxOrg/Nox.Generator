﻿// Generated

#nullable enable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Nox.Abstractions;
using Nox.Domain;
using Nox.Extensions;
using Nox.Types;

using Cryptocash.Domain;

namespace Cryptocash.Application.Dto;

/// <summary>
/// Landlord related data.
/// </summary>
public partial class LandLordCreateDto 
{    
    /// <summary>
    /// Landlord name (Required).
    /// </summary>
    [Required(ErrorMessage = "Name is required")]
    
    public System.String Name { get; set; } = default!;    
    /// <summary>
    /// Landlord's street address (Required).
    /// </summary>
    [Required(ErrorMessage = "Address is required")]
    
    public StreetAddressDto Address { get; set; } = default!;

    public Cryptocash.Domain.LandLord ToEntity()
    {
        var entity = new Cryptocash.Domain.LandLord();
        entity.Name = Cryptocash.Domain.LandLord.CreateName(Name);
        entity.Address = Cryptocash.Domain.LandLord.CreateAddress(Address);
        //entity.VendingMachines = VendingMachines.Select(dto => dto.ToEntity()).ToList();
        return entity;
    }
}