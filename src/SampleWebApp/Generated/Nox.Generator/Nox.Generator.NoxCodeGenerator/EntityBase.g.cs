﻿// Generated

#nullable enable

using System;

namespace SampleWebApp.Domain;

/// <summary>
/// The base class for all domain entities.
/// </summary>
public partial class EntityBase
{
    /// <summary>
    /// The state of the entity as at this date.
    /// </summary>
    public DateTime AsAt { get; set; }
}
