﻿// Generated

#nullable enable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Nox.Abstractions;
using Nox.Domain;
using Nox.Extensions;
using Nox.Types;

using SampleWebApp.Domain;

namespace SampleWebApp.Application.Dto;

/// <summary>
/// The list of countries.
/// </summary>
public partial class CountryCreateDto 
{    
    /// <summary>
    /// The country's common name (Required).
    /// </summary>
    [Required(ErrorMessage = "Name is required")]
    
    public System.String Name { get; set; } = default!;    
    /// <summary>
    /// The country's official name (Required).
    /// </summary>
    [Required(ErrorMessage = "FormalName is required")]
    
    public System.String FormalName { get; set; } = default!;    
    /// <summary>
    /// The country's official ISO 4217 alpha-3 code (Optional).
    /// </summary>
    public System.String? AlphaCode3 { get; set; }    
    /// <summary>
    /// The country's official ISO 4217 alpha-2 code (Required).
    /// </summary>
    [Required(ErrorMessage = "AlphaCode2 is required")]
    
    public System.String AlphaCode2 { get; set; } = default!;    
    /// <summary>
    /// The country's official ISO 4217 alpha-3 code (Required).
    /// </summary>
    [Required(ErrorMessage = "NumericCode is required")]
    
    public System.Int16 NumericCode { get; set; } = default!;    
    /// <summary>
    /// The country's phone dialing codes (comma-delimited) (Optional).
    /// </summary>
    public System.String? DialingCodes { get; set; }    
    /// <summary>
    /// The capital city of the country (Optional).
    /// </summary>
    public System.String? Capital { get; set; }    
    /// <summary>
    /// Noun denoting the natives of the country (Optional).
    /// </summary>
    public System.String? Demonym { get; set; }    
    /// <summary>
    /// Country area in square kilometers (Required).
    /// </summary>
    [Required(ErrorMessage = "AreaInSquareKilometres is required")]
    
    public System.Decimal AreaInSquareKilometres { get; set; } = default!;    
    /// <summary>
    /// The the position of the workplace's point on the surface of the Earth (Optional).
    /// </summary>
    public LatLongDto? GeoCoord { get; set; }    
    /// <summary>
    /// The region the country is in (Required).
    /// </summary>
    [Required(ErrorMessage = "GeoRegion is required")]
    
    public System.String GeoRegion { get; set; } = default!;    
    /// <summary>
    /// The sub-region the country is in (Required).
    /// </summary>
    [Required(ErrorMessage = "GeoSubRegion is required")]
    
    public System.String GeoSubRegion { get; set; } = default!;    
    /// <summary>
    /// The world region the country is in (Required).
    /// </summary>
    [Required(ErrorMessage = "GeoWorldRegion is required")]
    
    public System.String GeoWorldRegion { get; set; } = default!;    
    /// <summary>
    /// The estimated population of the country (Optional).
    /// </summary>
    public System.Int32? Population { get; set; }    
    /// <summary>
    /// The top level internet domains regitered to the country (comma-delimited) (Optional).
    /// </summary>
    public System.String? TopLevelDomains { get; set; }

    /// <summary>
    /// Country is also know as OneOrMany CountryLocalNames
    /// </summary>
    public virtual List<CountryLocalNameCreateDto> CountryLocalNames { get; set; } = new();

    public SampleWebApp.Domain.Country ToEntity()
    {
        var entity = new SampleWebApp.Domain.Country();
        entity.Name = SampleWebApp.Domain.Country.CreateName(Name);
        entity.FormalName = SampleWebApp.Domain.Country.CreateFormalName(FormalName);
        if (AlphaCode3 is not null)entity.AlphaCode3 = SampleWebApp.Domain.Country.CreateAlphaCode3(AlphaCode3.NonNullValue<System.String>());
        entity.AlphaCode2 = SampleWebApp.Domain.Country.CreateAlphaCode2(AlphaCode2);
        entity.NumericCode = SampleWebApp.Domain.Country.CreateNumericCode(NumericCode);
        if (DialingCodes is not null)entity.DialingCodes = SampleWebApp.Domain.Country.CreateDialingCodes(DialingCodes.NonNullValue<System.String>());
        if (Capital is not null)entity.Capital = SampleWebApp.Domain.Country.CreateCapital(Capital.NonNullValue<System.String>());
        if (Demonym is not null)entity.Demonym = SampleWebApp.Domain.Country.CreateDemonym(Demonym.NonNullValue<System.String>());
        entity.AreaInSquareKilometres = SampleWebApp.Domain.Country.CreateAreaInSquareKilometres(AreaInSquareKilometres);
        if (GeoCoord is not null)entity.GeoCoord = SampleWebApp.Domain.Country.CreateGeoCoord(GeoCoord.NonNullValue<LatLongDto>());
        entity.GeoRegion = SampleWebApp.Domain.Country.CreateGeoRegion(GeoRegion);
        entity.GeoSubRegion = SampleWebApp.Domain.Country.CreateGeoSubRegion(GeoSubRegion);
        entity.GeoWorldRegion = SampleWebApp.Domain.Country.CreateGeoWorldRegion(GeoWorldRegion);
        if (Population is not null)entity.Population = SampleWebApp.Domain.Country.CreatePopulation(Population.NonNullValue<System.Int32>());
        if (TopLevelDomains is not null)entity.TopLevelDomains = SampleWebApp.Domain.Country.CreateTopLevelDomains(TopLevelDomains.NonNullValue<System.String>());
        //entity.Currencies = Currencies.Select(dto => dto.ToEntity()).ToList();
        entity.CountryLocalNames = CountryLocalNames.Select(dto => dto.ToEntity()).ToList();
        return entity;
    }
}