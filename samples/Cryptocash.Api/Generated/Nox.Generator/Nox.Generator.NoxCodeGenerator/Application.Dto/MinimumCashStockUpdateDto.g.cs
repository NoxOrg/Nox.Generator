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
/// Minimum cash stock required for vending machine
/// </summary>
public partial class MinimumCashStockUpdateDto : IEntityDto<DomainNamespace.MinimumCashStock>
{
    /// <summary>
    /// Cash stock amount 
    /// <remarks>Required.</remarks>    
    /// </summary>
    [Required(ErrorMessage = "Amount is required")]
    
    public MoneyDto Amount { get; set; } = default!;

    /// <summary>
    /// MinimumCashStock required by ZeroOrMany VendingMachines
    /// </summary>
    public List<System.Guid> VendingMachinesId { get; set; } = new();

    /// <summary>
    /// MinimumCashStock related to ExactlyOne Currencies
    /// </summary>
    [Required(ErrorMessage = "Currency is required")]
    public System.String CurrencyId { get; set; } = default!;
}