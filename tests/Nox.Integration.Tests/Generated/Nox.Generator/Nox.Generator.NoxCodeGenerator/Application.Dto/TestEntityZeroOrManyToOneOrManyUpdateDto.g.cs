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
/// 
/// </summary>
public partial class TestEntityZeroOrManyToOneOrManyUpdateDto : IEntityDto<DomainNamespace.TestEntityZeroOrManyToOneOrMany>
{
    /// <summary>
    ///  
    /// <remarks>Required.</remarks>    
    /// </summary>
    [Required(ErrorMessage = "TextTestField2 is required")]
    
    public System.String TextTestField2 { get; set; } = default!;

    /// <summary>
    /// TestEntityZeroOrManyToOneOrMany Test entity relationship to TestEntityOneOrManyToZeroOrMany ZeroOrMany TestEntityOneOrManyToZeroOrManies
    /// </summary>
    public List<System.String> TestEntityOneOrManyToZeroOrManiesId { get; set; } = new();
}