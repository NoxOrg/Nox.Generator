﻿// Generated

#nullable enable

using Nox.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleWebApp.Domain;

/// <summary>
/// The list of countries.
/// </summary>
public partial class Country : AuditableEntityBase
{
    
    /// <summary>
    /// (Required)
    /// </summary>
    public Text Id { get; set; } = null!;
    
    /// <summary>
    /// The country's common name (required).
    /// </summary>
    public Text Name { get; set; } = null!;
    
    /// <summary>
    /// The country's official name (required).
    /// </summary>
    public Text FormalName { get; set; } = null!;
}
