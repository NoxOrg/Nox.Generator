﻿// Generated

#nullable enable

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

using MediatR;

using Nox.Types;
using Nox.Domain;
using Nox.Extensions;

using ClientApi.Domain;

namespace ClientApi.Application.Dto;

public record StoreKeyDto(System.Guid keyId);

/// <summary>
/// Stores.
/// </summary>
public partial class StoreDto
{

    /// <summary>
    ///  (Required).
    /// </summary>
    public System.Guid Id { get; set; } = default!;

    /// <summary>
    /// Store Name (Required).
    /// </summary>
    public System.String Name { get; set; } = default!;

    /// <summary>
    /// Street Address (Required).
    /// </summary>
    public StreetAddressDto Address { get; set; } = default!;

    /// <summary>
    /// Location (Required).
    /// </summary>
    public LatLongDto Location { get; set; } = default!;

    /// <summary>
    /// Store Store owner relationship ZeroOrOne StoreOwners
    /// </summary>
    //EF maps ForeignKey Automatically
    public System.String? OwnerRelId { get; set; } = default!;
    public virtual StoreOwnerDto? OwnerRel { get; set; } = null!;

    /// <summary>
    /// Store Verified emails ZeroOrOne EmailAddresses
    /// </summary>
    public virtual EmailAddressDto? EmailAddress { get; set; } = null!;
    public System.DateTime? DeletedAtUtc { get; set; }    
}