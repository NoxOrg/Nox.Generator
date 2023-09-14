// Generated

#nullable enable

using System;
using System.Collections.Generic;

using Nox.Types;
using Nox.Domain;

namespace TestWebApp.Domain;

/// <summary>
/// Entity created for testing database.
/// </summary>
public partial class TestEntityZeroOrOneToZeroOrMany : AuditableEntityBase
{
    /// <summary>
    ///  (Required).
    /// </summary>
    public Nox.Types.Text Id { get; set; } = null!;

    /// <summary>
    ///  (Required).
    /// </summary>
    public Nox.Types.Text TextTestField { get; set; } = null!;

    /// <summary>
    /// TestEntityZeroOrOneToZeroOrMany Test entity relationship to TestEntityZeroOrManyToZeroOrOne ZeroOrOne TestEntityZeroOrManyToZeroOrOnes
    /// </summary>
    public virtual TestEntityZeroOrManyToZeroOrOne? TestEntityZeroOrManyToZeroOrOne { get; set; } = null!;

    /// <summary>
    /// Foreign key for relationship ZeroOrOne to entity TestEntityZeroOrManyToZeroOrOne
    /// </summary>
    public Nox.Types.Text? TestEntityZeroOrManyToZeroOrOneId { get; set; } = null!;
}