﻿// Generated

#nullable enable

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

using MediatR;

using Nox.Types;
using Nox.Domain;
using Nox.Extensions;

using Cryptocash.Domain;

namespace Cryptocash.Application.Dto;

public record MinimumCashStockKeyDto(System.Int64 keyId);

/// <summary>
/// Minimum cash stock required for vending machine.
/// </summary>
public partial class MinimumCashStockDto
{

    /// <summary>
    /// Vending machine cash stock unique identifier (Required).
    /// </summary>
    public System.Int64 Id { get; set; } = default!;

    /// <summary>
    /// Cash stock amount (Required).
    /// </summary>
    public MoneyDto Amount { get; set; } = default!;

    /// <summary>
    /// MinimumCashStock required by ZeroOrMany VendingMachines
    /// </summary>
    public virtual List<VendingMachineDto> VendingMachines { get; set; } = new();

    /// <summary>
    /// MinimumCashStock related to ExactlyOne Currencies
    /// </summary>
    //EF maps ForeignKey Automatically
    public System.String CurrencyId { get; set; } = default!;
    public virtual CurrencyDto Currency { get; set; } = null!;
    public System.DateTime? DeletedAtUtc { get; set; }    
}