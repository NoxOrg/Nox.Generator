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

public partial class TestEntityExactlyOneToOneOrManyCreateDto : TestEntityExactlyOneToOneOrManyCreateDtoBase
{

}

/// <summary>
/// Entity created for testing database.
/// </summary>
public abstract class TestEntityExactlyOneToOneOrManyCreateDtoBase : IEntityDto<TestEntityExactlyOneToOneOrMany>
{
    /// <summary>
    ///  (Required).
    /// </summary>
    [Required(ErrorMessage = "Id is required")]
    public System.String Id { get; set; } = default!;
    /// <summary>
    ///  (Required).
    /// </summary>
    [Required(ErrorMessage = "TextTestField is required")]
    
    public virtual System.String TextTestField { get; set; } = default!;

    /// <summary>
    /// TestEntityExactlyOneToOneOrMany Test entity relationship to TestEntityOneOrManyToExactlyOne ExactlyOne TestEntityOneOrManyToExactlyOnes
    /// </summary>
    public virtual TestEntityOneOrManyToExactlyOneCreateDto? TestEntityOneOrManyToExactlyOne { get; set; } = default!;
}