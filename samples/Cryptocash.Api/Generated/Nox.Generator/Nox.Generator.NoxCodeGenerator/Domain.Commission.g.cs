﻿// Generated

#nullable enable

using System;
using System.Collections.Generic;

using Nox.Abstractions;
using Nox.Domain;
using Nox.Types;

namespace Cryptocash.Domain;
public partial class Commission:CommissionBase
{

}
/// <summary>
/// Record for Commission created event.
/// </summary>
public record CommissionCreated(Commission Commission) : IDomainEvent;
/// <summary>
/// Record for Commission updated event.
/// </summary>
public record CommissionUpdated(Commission Commission) : IDomainEvent;
/// <summary>
/// Record for Commission deleted event.
/// </summary>
public record CommissionDeleted(Commission Commission) : IDomainEvent;

/// <summary>
/// Exchange commission rate and amount.
/// </summary>
public abstract class CommissionBase : AuditableEntityBase, IEntityConcurrent
{
    /// <summary>
    /// Commission unique identifier (Required).
    /// </summary>
    public Nox.Types.AutoNumber Id { get; set; } = null!;

    /// <summary>
    /// Commission rate (Required).
    /// </summary>
    public Nox.Types.Percentage Rate { get; set; } = null!;

    /// <summary>
    /// Exchange rate conversion amount (Required).
    /// </summary>
    public Nox.Types.DateTime EffectiveAt { get; set; } = null!;

    /// <summary>
    /// Commission fees for ZeroOrOne Countries
    /// </summary>
    public virtual Country? CommissionFeesForCountry { get; private set; } = null!;

    /// <summary>
    /// Foreign key for relationship ZeroOrOne to entity Country
    /// </summary>
    public Nox.Types.CountryCode2? CommissionFeesForCountryId { get; set; } = null!;

    public virtual void CreateRefToCommissionFeesForCountry(Country relatedCountry)
    {
        CommissionFeesForCountry = relatedCountry;
    }

    public virtual void DeleteRefToCommissionFeesForCountry(Country relatedCountry)
    {
        CommissionFeesForCountry = null;
    }

    public virtual void DeleteAllRefToCommissionFeesForCountry()
    {
        CommissionFeesForCountryId = null;
    }

    /// <summary>
    /// Commission fees for ZeroOrMany Bookings
    /// </summary>
    public virtual List<Booking> CommissionFeesForBooking { get; private set; } = new();

    public virtual void CreateRefToCommissionFeesForBooking(Booking relatedBooking)
    {
        CommissionFeesForBooking.Add(relatedBooking);
    }

    public virtual void DeleteRefToCommissionFeesForBooking(Booking relatedBooking)
    {
        CommissionFeesForBooking.Remove(relatedBooking);
    }

    public virtual void DeleteAllRefToCommissionFeesForBooking()
    {
        CommissionFeesForBooking.Clear();
    }

    /// <summary>
    /// Entity tag used as concurrency token.
    /// </summary>
    public System.Guid Etag { get; set; } = System.Guid.NewGuid();
}