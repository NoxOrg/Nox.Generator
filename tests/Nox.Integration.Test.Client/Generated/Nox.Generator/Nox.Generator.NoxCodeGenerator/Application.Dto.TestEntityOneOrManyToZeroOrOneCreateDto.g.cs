﻿// Generated

#nullable enable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Nox.Abstractions;
using Nox.Application.Dto;
using Nox.Domain;
using Nox.Extensions;
using Nox.Types;

using TestWebApp.Domain;

namespace TestWebApp.Application.Dto;

public partial class TestEntityOneOrManyToZeroOrOneCreateDto : TestEntityOneOrManyToZeroOrOneCreateDtoBase
{

}

/// <summary>
/// .
/// </summary>
public abstract class TestEntityOneOrManyToZeroOrOneCreateDtoBase : IEntityDto<TestEntityOneOrManyToZeroOrOne>
{
    /// <summary>
    ///  (Required).
    /// </summary>
    [Required(ErrorMessage = "Id is required")]
    public System.String Id { get; set; } = default!;
    /// <summary>
    ///  (Required).
    /// </summary>
    [Required(ErrorMessage = "TextTestField2 is required")]
    
    public virtual System.String TextTestField2 { get; set; } = default!;

    /// <summary>
    /// TestEntityOneOrManyToZeroOrOne Test entity relationship to TestEntityZeroOrOneToOneOrMany OneOrMany TestEntityZeroOrOneToOneOrManies
    /// </summary>
    public virtual List<TestEntityZeroOrOneToOneOrManyCreateDto> TestEntityZeroOrOneToOneOrMany { get; set; } = new();
}