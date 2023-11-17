﻿// Generated

#nullable enable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Nox.Abstractions;
using Nox.Application.Dto;
using Nox.Types;

using DomainNamespace = ClientApi.Domain;

namespace ClientApi.Application.Dto;

/// <summary>
/// Workplace.
/// </summary>
public partial class WorkplaceUpdateDto : WorkplaceUpdateDtoBase
{

}

/// <summary>
/// Workplace
/// </summary>
public partial class WorkplaceUpdateDtoBase: EntityDtoBase, IEntityDto<DomainNamespace.Workplace>
{
    /// <summary>
    /// Workplace Name     
    /// </summary>
    /// <remarks>Required.</remarks>
    [Required(ErrorMessage = "Name is required")]
    
    public virtual System.String Name { get; set; } = default!;
    /// <summary>
    /// Workplace Description     
    /// </summary>
    /// <remarks>Optional.</remarks>
    public virtual System.String? Description { get; set; }
}