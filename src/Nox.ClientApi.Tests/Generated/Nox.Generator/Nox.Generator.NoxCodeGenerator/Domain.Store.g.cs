﻿// Generated

#nullable enable

using System;
using System.Collections.Generic;

using Nox.Types;
using Nox.Domain;

namespace ClientApi.Domain;

/// <summary>
/// Stores.
/// </summary>
public partial class Store : AuditableEntityBase
{
    /// <summary>
    ///  (Required).
    /// </summary>
    public DatabaseGuid Id { get; set; } = null!;

    /// <summary>
    /// Store Name (Required).
    /// </summary>
    public Nox.Types.Text Name { get; set; } = null!;

    /// <summary>
    /// Street Address (Required).
    /// </summary>
    public Nox.Types.StreetAddress Address { get; set; } = null!;

    /// <summary>
    /// Location (Required).
    /// </summary>
    public Nox.Types.LatLong Location { get; set; } = null!;

    /// <summary>
    /// Store Store owner relationship ZeroOrOne StoreOwners
    /// </summary>
    public virtual StoreOwner? OwnerRel { get; set; } = null!;

    /// <summary>
    /// Foreign key for relationship ZeroOrOne to entity StoreOwner
    /// </summary>
    public Nox.Types.Text? OwnerRelId { get; set; } = null!;

    /// <summary>
    /// Store Verified emails ZeroOrOne EmailAddresses
    /// </summary>
     public virtual EmailAddress? EmailAddress { get; set; } = null!;
}