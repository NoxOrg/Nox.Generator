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
/// Workplace
/// </summary>
public partial class WorkplaceUpdateDto : IEntityDto<DomainNamespace.Workplace>
{
    /// <summary>
    /// Workplace Name 
    /// <remarks>Required.</remarks>    
    /// </summary>
    [Required(ErrorMessage = "Name is required")]
    
    public System.String Name { get; set; } = default!;
    /// <summary>
    /// Workplace Description 
    /// <remarks>Optional.</remarks>    
    /// </summary>
    public System.String? Description { get; set; }

    /// <summary>
    /// Workplace Workplace country ZeroOrOne Countries
    /// </summary>
    
    public System.Int64? CountryId { get; set; } = default!;

    /// <summary>
    /// Workplace Actve Tenants in the workplace ZeroOrMany Tenants
    /// </summary>
    public List<System.UInt32> TenantsId { get; set; } = new();
}