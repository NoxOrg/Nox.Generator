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
/// Minimum cash stock required for vending machine.
/// </summary>
public partial class MinimumCashStockUpdateDto : MinimumCashStockUpdateDtoBase
{

}

/// <summary>
/// Minimum cash stock required for vending machine
/// </summary>
public partial class MinimumCashStockUpdateDtoBase: EntityDtoBase, IEntityDto<DomainNamespace.MinimumCashStock>
{
    /// <summary>
    /// Cash stock amount     
    /// </summary>
    /// <remarks>Required.</remarks>
    [Required(ErrorMessage = "Amount is required")]
    
    public virtual MoneyDto Amount { get; set; } = default!;
}