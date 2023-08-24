﻿// Generated

#nullable enable

using System;
using System.Collections.Generic;

using Nox.Types;
using Nox.Domain;

namespace IamApi.Domain;

/// <summary>
/// Verified Phone.
/// </summary>
public partial class Phone:IOwnedEntity
{
    /// <summary>
    /// Phone (Required).
    /// </summary>
    public PhoneNumber PhoneNumber { get; set; } = null!;

    /// <summary>
    /// Verified (Optional).
    /// </summary>
    public Nox.Types.Boolean? IsVerified { get; set; } = null!;

    /// <summary>
    /// Country code (Optional).
    /// </summary>
    public Nox.Types.CountryCode2? CountryCode { get; set; } = null!;
}