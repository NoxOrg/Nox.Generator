﻿// Generated

#nullable enable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Nox.Abstractions;
using Nox.Application.Dto;
using Nox.Types;

namespace TestWebApp.Application.Dto;

/// <summary>
/// Entity created for testing database.
/// </summary>
public partial class TestEntityExactlyOneUpdateDto : TestEntityExactlyOneUpdateDtoBase
{

}

/// <summary>
/// Entity created for testing database
/// </summary>
public partial class TestEntityExactlyOneUpdateDtoBase: EntityDtoBase
{
    /// <summary>
    ///      
    /// </summary>
    /// <remarks>Required.</remarks>
    [Required(ErrorMessage = "TextTestField is required")]
    
    public virtual System.String? TextTestField { get; set; }
}