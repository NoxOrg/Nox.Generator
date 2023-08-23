﻿// Generated

#nullable enable

using Nox.Abstractions;
using Nox.Types;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClientApi.Application.Dto; 

/// <summary>
/// Client DatabaseNumber Key.
/// </summary>
public partial class ClientDatabaseNumberUpdateDto
{
    //TODO Add owned Entities and update odata endpoints
    /// <summary>
    /// The Text (Required).
    /// </summary>
    [Required(ErrorMessage = "Name is required")]
    
    public System.String Name { get; set; } = default!;
    /// <summary>
    /// The Number (Optional).
    /// </summary>
    public System.Int32? Number { get; set; } 
    /// <summary>
    /// The Money (Optional).
    /// </summary>
    public MoneyDto? AmmountMoney { get; set; } 

    /// <summary>
    /// ClientDatabaseNumber is also know as ZeroOrMany OwnedEntities
    /// </summary>
    public virtual List<OwnedEntityUpdateDto> OwnedEntities { get; set; } = new();
}