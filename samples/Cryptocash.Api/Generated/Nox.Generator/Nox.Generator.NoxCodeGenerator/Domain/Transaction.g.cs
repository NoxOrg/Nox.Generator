﻿// Generated

#nullable enable

using System;
using System.Collections.Generic;

using MediatR;

using Nox.Abstractions;
using Nox.Domain;
using Nox.Solution;
using Nox.Types;

namespace Cryptocash.Domain;

internal partial class Transaction : TransactionBase, IEntityHaveDomainEvents
{
    ///<inheritdoc/>
    public void RaiseCreateEvent()
    {
        InternalRaiseCreateEvent(this);
    }
    ///<inheritdoc/>
    public void RaiseDeleteEvent()
    {
        InternalRaiseDeleteEvent(this);
    }
    ///<inheritdoc/>
    public void RaiseUpdateEvent()
    {
        InternalRaiseUpdateEvent(this);
    }
}
/// <summary>
/// Record for Transaction created event.
/// </summary>
internal record TransactionCreated(Transaction Transaction) :  IDomainEvent, INotification;
/// <summary>
/// Record for Transaction updated event.
/// </summary>
internal record TransactionUpdated(Transaction Transaction) : IDomainEvent, INotification;
/// <summary>
/// Record for Transaction deleted event.
/// </summary>
internal record TransactionDeleted(Transaction Transaction) : IDomainEvent, INotification;

/// <summary>
/// Customer transaction log and related data.
/// </summary>
internal abstract partial class TransactionBase : AuditableEntityBase, IEntityConcurrent
{
    /// <summary>
    /// Customer transaction unique identifier
    /// <remarks>Required.</remarks>   
    /// </summary>
    public Nox.Types.AutoNumber Id { get; set; } = null!;

    /// <summary>
    /// Transaction type
    /// <remarks>Required.</remarks>   
    /// </summary>
    public Nox.Types.Text TransactionType { get; set; } = null!;

    /// <summary>
    /// Transaction processed datetime
    /// <remarks>Required.</remarks>   
    /// </summary>
    public Nox.Types.DateTime ProcessedOnDateTime { get; set; } = null!;

    /// <summary>
    /// Transaction amount
    /// <remarks>Required.</remarks>   
    /// </summary>
    public Nox.Types.Money Amount { get; set; } = null!;

    /// <summary>
    /// Transaction external reference
    /// <remarks>Required.</remarks>   
    /// </summary>
    public Nox.Types.Text Reference { get; set; } = null!;
    /// <summary>
    /// Domain events raised by this entity.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => InternalDomainEvents;
    protected readonly List<IDomainEvent> InternalDomainEvents = new();

	protected virtual void InternalRaiseCreateEvent(Transaction transaction)
	{
		InternalDomainEvents.Add(new TransactionCreated(transaction));
    }
	
	protected virtual void InternalRaiseUpdateEvent(Transaction transaction)
	{
		InternalDomainEvents.Add(new TransactionUpdated(transaction));
    }
	
	protected virtual void InternalRaiseDeleteEvent(Transaction transaction)
	{
		InternalDomainEvents.Add(new TransactionDeleted(transaction));
    }
    /// <summary>
    /// Clears all domain events associated with the entity.
    /// </summary>
    public virtual void ClearDomainEvents()
    {
        InternalDomainEvents.Clear();
    }

    /// <summary>
    /// Transaction for ExactlyOne Customers
    /// </summary>
    public virtual Customer Customer { get; private set; } = null!;

    /// <summary>
    /// Foreign key for relationship ExactlyOne to entity Customer
    /// </summary>
    public Nox.Types.AutoNumber CustomerId { get; set; } = null!;

    public virtual void CreateRefToCustomer(Customer relatedCustomer)
    {
        Customer = relatedCustomer;
    }

    public virtual void DeleteRefToCustomer(Customer relatedCustomer)
    {
        throw new RelationshipDeletionException($"The relationship cannot be deleted.");
    }

    public virtual void DeleteAllRefToCustomer()
    {
        throw new RelationshipDeletionException($"The relationship cannot be deleted.");
    }

    /// <summary>
    /// Transaction for ExactlyOne Bookings
    /// </summary>
    public virtual Booking Booking { get; private set; } = null!;

    /// <summary>
    /// Foreign key for relationship ExactlyOne to entity Booking
    /// </summary>
    public Nox.Types.Guid BookingId { get; set; } = null!;

    public virtual void CreateRefToBooking(Booking relatedBooking)
    {
        Booking = relatedBooking;
    }

    public virtual void DeleteRefToBooking(Booking relatedBooking)
    {
        throw new RelationshipDeletionException($"The relationship cannot be deleted.");
    }

    public virtual void DeleteAllRefToBooking()
    {
        throw new RelationshipDeletionException($"The relationship cannot be deleted.");
    }

    /// <summary>
    /// Entity tag used as concurrency token.
    /// </summary>
    public System.Guid Etag { get; set; } = System.Guid.NewGuid();
}