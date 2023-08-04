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

namespace SampleWebApp.Presentation.Api.OData;

/// <summary>
/// The list of currencies.
/// </summary>
[AutoMap(typeof(CurrencyDto))]
public partial class OCurrency : AuditableEntityBase
{

    /// <summary>
    /// The currency's primary key / identifier (Required).
    /// </summary>
    public String Id { get; set; } = null!;

    /// <summary>
    /// The currency's name (Required).
    /// </summary>
    public String Name { get; set; } =default!;

    /// <summary>
    /// Currency is legal tender for ZeroOrMany Countries
    /// </summary>
    public virtual List<OCountry> Countries { get; set; } = new();

    public List<OCountry> CurrencyIsLegalTenderForCountry => Countries;
}