﻿// Generated

#nullable enable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Nox.Abstractions;
using Nox.Application.Dto;
using Nox.Domain;
using Nox.Extensions;
using Nox.Types;

using DomainNamespace = Cryptocash.Domain;

namespace Cryptocash.Application.Dto;

public partial class MinimumCashStockCreateDto : MinimumCashStockCreateDtoBase
{

}

/// <summary>
/// Minimum cash stock required for vending machine.
/// </summary>
public abstract class MinimumCashStockCreateDtoBase : IEntityDto<DomainNamespace.MinimumCashStock>
{
    /// <summary>
    /// Cash stock amount (Required).
    /// </summary>
    [Required(ErrorMessage = "Amount is required")]
    
    public virtual MoneyDto Amount { get; set; } = default!;

    /// <summary>
    /// MinimumCashStock required by ZeroOrMany VendingMachines
    /// </summary>
    public virtual List<System.Guid> VendingMachinesId { get; set; } = new();
    
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual List<VendingMachineCreateDto> VendingMachines { get; set; } = new();

    /// <summary>
    /// MinimumCashStock related to ExactlyOne Currencies
    /// </summary>
    public System.String? CurrencyId { get; set; } = default!;
    
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual CurrencyCreateDto? Currency { get; set; } = default!;
}