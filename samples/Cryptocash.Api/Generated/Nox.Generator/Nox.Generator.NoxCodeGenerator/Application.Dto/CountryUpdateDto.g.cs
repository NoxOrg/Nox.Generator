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
/// Country and related data.
/// </summary>
public partial class CountryUpdateDto : CountryUpdateDtoBase
{

}

/// <summary>
/// Country and related data
/// </summary>
public partial class CountryUpdateDtoBase: EntityDtoBase, IEntityDto<DomainNamespace.Country>
{
    /// <summary>
    /// Country's name 
    /// <remarks>Required.</remarks>    
    /// </summary>
    [Required(ErrorMessage = "Name is required")]
    
    public virtual System.String Name { get; set; } = default!;
    /// <summary>
    /// Country's official name 
    /// <remarks>Optional.</remarks>    
    /// </summary>
    public virtual System.String? OfficialName { get; set; }
    /// <summary>
    /// Country's iso number id 
    /// <remarks>Optional.</remarks>    
    /// </summary>
    public virtual System.UInt16? CountryIsoNumeric { get; set; }
    /// <summary>
    /// Country's iso alpha3 id 
    /// <remarks>Optional.</remarks>    
    /// </summary>
    public virtual System.String? CountryIsoAlpha3 { get; set; }
    /// <summary>
    /// Country's geo coordinates 
    /// <remarks>Optional.</remarks>    
    /// </summary>
    public virtual LatLongDto? GeoCoords { get; set; }
    /// <summary>
    /// Country's flag emoji 
    /// <remarks>Optional.</remarks>    
    /// </summary>
    public virtual System.String? FlagEmoji { get; set; }
    /// <summary>
    /// Country's flag in svg format 
    /// <remarks>Optional.</remarks>    
    /// </summary>
    public virtual ImageDto? FlagSvg { get; set; }
    /// <summary>
    /// Country's flag in png format 
    /// <remarks>Optional.</remarks>    
    /// </summary>
    public virtual ImageDto? FlagPng { get; set; }
    /// <summary>
    /// Country's coat of arms in svg format 
    /// <remarks>Optional.</remarks>    
    /// </summary>
    public virtual ImageDto? CoatOfArmsSvg { get; set; }
    /// <summary>
    /// Country's coat of arms in png format 
    /// <remarks>Optional.</remarks>    
    /// </summary>
    public virtual ImageDto? CoatOfArmsPng { get; set; }
    /// <summary>
    /// Country's map via google maps 
    /// <remarks>Optional.</remarks>    
    /// </summary>
    public virtual System.String? GoogleMapsUrl { get; set; }
    /// <summary>
    /// Country's map via open street maps 
    /// <remarks>Optional.</remarks>    
    /// </summary>
    public virtual System.String? OpenStreetMapsUrl { get; set; }
    /// <summary>
    /// Country's start of week day 
    /// <remarks>Required.</remarks>    
    /// </summary>
    [Required(ErrorMessage = "StartOfWeek is required")]
    
    public virtual System.UInt16 StartOfWeek { get; set; } = default!;
}