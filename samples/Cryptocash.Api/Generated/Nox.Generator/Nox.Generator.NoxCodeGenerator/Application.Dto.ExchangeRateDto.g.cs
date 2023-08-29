﻿// Generated

#nullable enable
using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using Nox.Types;
using Nox.Domain;
//using CryptocashApi.Application.DataTransferObjects;
using CryptocashApi.Domain;

namespace CryptocashApi.Application.Dto;

public record ExchangeRateKeyDto(System.Int64 keyId);

/// <summary>
/// Exchange rate and related data.
/// </summary>
public partial class ExchangeRateDto
{

    /// <summary>
    /// The exchange rate unique identifier (Required).
    /// </summary>
    public System.Int64 Id { get; set; } = default!;

    /// <summary>
    /// The exchange rate conversion amount (Required).
    /// </summary>
    public System.Int32 EffectiveRate { get; set; } = default!;

    /// <summary>
    /// The exchange rate conversion amount (Required).
    /// </summary>
    public System.DateTimeOffset EffectiveAt { get; set; } = default!;

    /// <summary>
    /// ExchangeRate The currency exchanged from ExactlyOne Currencies
    /// </summary>
    //EF maps ForeignKey Automatically
    public string CurrencyId { get; set; } = null!;
    public virtual CurrencyDto Currency { get; set; } = null!;

    public System.DateTime? DeletedAtUtc { get; set; }
}