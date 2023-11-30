﻿// Generated

#nullable enable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Nox.Abstractions;
using Nox.Application.Dto;
using Nox.Types;

using DomainNamespace = TestWebApp.Domain;

namespace TestWebApp.Application.Dto;



/// <summary>
/// Entity created for testing constraints.
/// </summary>
public partial class EntityUniqueConstraintsRelatedForeignKeyPartialUpdateDto : EntityUniqueConstraintsRelatedForeignKeyPartialUpdateDtoBase
{

}

/// <summary>
/// Entity created for testing constraints
/// </summary>
public partial class EntityUniqueConstraintsRelatedForeignKeyPartialUpdateDtoBase: EntityDtoBase, IEntityDto<DomainNamespace.EntityUniqueConstraintsRelatedForeignKey>
{
    /// <summary>
    /// 
    /// </summary>
    public virtual System.String? TextField { get; set; }
}