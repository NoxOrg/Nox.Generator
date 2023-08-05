﻿// Generated

#nullable enable

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using AutoMapper;
using MediatR;
using Nox.Types;
using Nox.Domain;
using SampleWebApp.Application.DataTransferObjects;
using SampleWebApp.Domain;

namespace SampleWebApp.Application.Dto;

/// <summary>
/// The list of countries.
/// </summary>
[AutoMap(typeof(CountryCreateDto))]
public partial class OCountry : AuditableEntityBase
{

    /// <summary>
    ///  (Required).
    /// </summary>
    public String Id { get; set; } = null!;

    /// <summary>
    /// The country's common name (Required).
    /// </summary>
    public String Name { get; set; } = default!;

    /// <summary>
    /// The country's official name (Required).
    /// </summary>
    public String FormalName { get; set; } = default!;

    /// <summary>
    /// The country's official ISO 4217 alpha-3 code (Required).
    /// </summary>
    public String AlphaCode3 { get; set; } = default!;

    /// <summary>
    /// The country's official ISO 4217 alpha-2 code (Required).
    /// </summary>
    public String AlphaCode2 { get; set; } = default!;

    /// <summary>
    /// The country's official ISO 4217 alpha-3 code (Required).
    /// </summary>
    public Int16 NumericCode { get; set; } = default!;

    /// <summary>
    /// The country's phone dialing codes (comma-delimited) (Optional).
    /// </summary>
    public String? DialingCodes { get; set; } 

    /// <summary>
    /// The capital city of the country (Optional).
    /// </summary>
    public String? Capital { get; set; } 

    /// <summary>
    /// Noun denoting the natives of the country (Optional).
    /// </summary>
    public String? Demonym { get; set; } 

    /// <summary>
    /// Country area in square kilometers (Required).
    /// </summary>
    public Int32 AreaInSquareKilometres { get; set; } = default!;

    /// <summary>
    /// The the position of the workplace's point on the surface of the Earth (Optional).
    /// </summary>
    public Double? GeoCoord { get; set; } 

    /// <summary>
    /// The region the country is in (Required).
    /// </summary>
    public String GeoRegion { get; set; } = default!;

    /// <summary>
    /// The sub-region the country is in (Required).
    /// </summary>
    public String GeoSubRegion { get; set; } = default!;

    /// <summary>
    /// The world region the country is in (Required).
    /// </summary>
    public String GeoWorldRegion { get; set; } = default!;

    /// <summary>
    /// The estimated population of the country (Optional).
    /// </summary>
    public Int32? Population { get; set; } 

    /// <summary>
    /// The top level internet domains regitered to the country (comma-delimited) (Optional).
    /// </summary>
    public String? TopLevelDomains { get; set; } 
}