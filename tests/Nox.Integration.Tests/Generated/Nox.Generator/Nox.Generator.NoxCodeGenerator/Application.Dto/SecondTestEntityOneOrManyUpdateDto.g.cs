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
/// .
/// </summary>
public partial class SecondTestEntityOneOrManyUpdateDto : SecondTestEntityOneOrManyUpdateDtoBase
{

}

/// <summary>
/// 
/// </summary>
public partial class SecondTestEntityOneOrManyUpdateDtoBase: EntityDtoBase, IEntityDto<DomainNamespace.SecondTestEntityOneOrMany>
{
    /// <summary>
    ///  
    /// <remarks>Required.</remarks>    
    /// </summary>
    [Required(ErrorMessage = "TextTestField2 is required")]
    
    public virtual System.String TextTestField2 { get; set; } = default!;

    /// <summary>
    /// SecondTestEntityOneOrMany Test entity relationship to TestEntityOneOrMany OneOrMany TestEntityOneOrManies
    /// </summary>
    public virtual List<System.String> TestEntityOneOrManiesId { get; set; } = new();
}