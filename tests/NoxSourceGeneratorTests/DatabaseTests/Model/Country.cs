﻿// Generated

#nullable enable

using Nox.Types;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nox.Generator.Test.DatabaseTests.Model;

/// <summary>
/// The identifier (primary key) for a Country.
/// </summary>
public class CountryId : ValueObject<Text, CountryId> { }

/// <summary>
/// The list of countries.
/// </summary>
[Table("Countries")]
public partial class Country
{

    /// <summary>
    /// (Optional)
    /// </summary>
    public CountryId Id { get; set; } = null!;

    /// <summary>
    /// The country's common name (required).
    /// </summary>
    public Text Name { get; set; } = null!;

    /// <summary>
    /// The country's official name (required).
    /// </summary>
    public Text FormalName { get; set; } = null!;

    /// <summary>
    /// The country's official ISO 4217 alpha-3 code (required).
    /// </summary>
    public Text AlphaCode3 { get; set; } = null!;

    /// <summary>
    /// The country's official ISO 4217 alpha-2 code (required).
    /// </summary>
    public CountryCode2 AlphaCode2 { get; set; } = null!;

    /// <summary>
    /// The country's official ISO 4217 alpha-3 code (required).
    /// </summary>
    public Number NumericCode { get; set; } = null!;

    /// <summary>
    /// The country's phone dialing codes (comma-delimited) (optional).
    /// </summary>
    public Text? DialingCodes { get; set; } = null!;

    /// <summary>
    /// The capital city of the country (optional).
    /// </summary>
    public Text? Capital { get; set; } = null!;

    /// <summary>
    /// Noun denoting the natives of the country (optional).
    /// </summary>
    public Text? Demonym { get; set; } = null!;

    /// <summary>
    /// Country area in square kilometers (required).
    /// </summary>
    public Number AreaInSquareKilometres { get; set; } = null!;

    /// <summary>
    /// The the position of the workplace's point on the surface of the Earth (optional).
    /// </summary>
    public LatLong? GeoCoord { get; set; } = null!;

    /// <summary>
    /// The region the country is in (required).
    /// </summary>
    public Text GeoRegion { get; set; } = null!;

    /// <summary>
    /// The sub-region the country is in (required).
    /// </summary>
    public Text GeoSubRegion { get; set; } = null!;

    /// <summary>
    /// The world region the country is in (required).
    /// </summary>
    public Text GeoWorldRegion { get; set; } = null!;

    /// <summary>
    /// The estimated population of the country (optional).
    /// </summary>
    public Number? Population { get; set; } = null!;

    /// <summary>
    /// The top level internet domains regitered to the country (comma-delimited) (optional).
    /// </summary>
    public Text? TopLevelDomains { get; set; } = null!;
}
