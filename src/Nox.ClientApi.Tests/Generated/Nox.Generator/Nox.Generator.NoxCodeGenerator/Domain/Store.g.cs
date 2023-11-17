﻿// Generated

#nullable enable

using System;
using System.Collections.Generic;

using MediatR;

using Nox.Abstractions;
using Nox.Domain;
using Nox.Solution;
using Nox.Types;

namespace ClientApi.Domain;

internal partial class Store : StoreBase, IEntityHaveDomainEvents
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
/// Record for Store created event.
/// </summary>
internal record StoreCreated(Store Store) :  IDomainEvent, INotification;
/// <summary>
/// Record for Store updated event.
/// </summary>
internal record StoreUpdated(Store Store) : IDomainEvent, INotification;
/// <summary>
/// Record for Store deleted event.
/// </summary>
internal record StoreDeleted(Store Store) : IDomainEvent, INotification;

/// <summary>
/// Stores.
/// </summary>
internal abstract partial class StoreBase : AuditableEntityBase, IEntityConcurrent
{
    /// <summary>
    ///     
    /// </summary>
    /// <remarks>Required.</remarks>   
    public Nox.Types.Guid Id {get; set;} = null!;
         /// <summary>
        /// Ensures that a Guid Id is set or will be generate a new one
        /// </summary>
    	public virtual void EnsureId(System.Guid guid)
    	{
    		if(System.Guid.Empty.Equals(guid))
    		{
    			Id = Nox.Types.Guid.From(System.Guid.NewGuid());
    		}
    		else
    		{
    			Id = Nox.Types.Guid.From(guid);
    		}
    	}

    /// <summary>
    /// Store Name    
    /// </summary>
    /// <remarks>Required.</remarks>   
    public Nox.Types.Text Name { get; set; } = null!;

    /// <summary>
    /// Street Address    
    /// </summary>
    /// <remarks>Required.</remarks>   
    public Nox.Types.StreetAddress Address { get; set; } = null!;

    /// <summary>
    /// Location    
    /// </summary>
    /// <remarks>Required.</remarks>   
    public Nox.Types.LatLong Location { get; set; } = null!;

    /// <summary>
    /// Opening day    
    /// </summary>
    /// <remarks>Optional.</remarks>   
    public Nox.Types.DateTime? OpeningDay { get; set; } = null!;

    /// <summary>
    /// Store Status    
    /// </summary>
    /// <remarks>Optional.</remarks>   
    public Nox.Types.Enumeration? Status { get; set; } = null!;
    /// <summary>
    /// Domain events raised by this entity.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => InternalDomainEvents;
    protected readonly List<IDomainEvent> InternalDomainEvents = new();

	protected virtual void InternalRaiseCreateEvent(Store store)
	{
		InternalDomainEvents.Add(new StoreCreated(store));
    }
	
	protected virtual void InternalRaiseUpdateEvent(Store store)
	{
		InternalDomainEvents.Add(new StoreUpdated(store));
    }
	
	protected virtual void InternalRaiseDeleteEvent(Store store)
	{
		InternalDomainEvents.Add(new StoreDeleted(store));
    }
    /// <summary>
    /// Clears all domain events associated with the entity.
    /// </summary>
    public virtual void ClearDomainEvents()
    {
        InternalDomainEvents.Clear();
    }

    /// <summary>
    /// Store Owner of the Store ZeroOrOne StoreOwners
    /// </summary>
    public virtual StoreOwner? StoreOwner { get; private set; } = null!;

    /// <summary>
    /// Foreign key for relationship ZeroOrOne to entity StoreOwner
    /// </summary>
    public Nox.Types.Text? StoreOwnerId { get; set; } = null!;

    public virtual void CreateRefToStoreOwner(StoreOwner relatedStoreOwner)
    {
        StoreOwner = relatedStoreOwner;
    }

    public virtual void DeleteRefToStoreOwner(StoreOwner relatedStoreOwner)
    {
        StoreOwner = null;
    }

    public virtual void DeleteAllRefToStoreOwner()
    {
        StoreOwnerId = null;
    }

    /// <summary>
    /// Store License that this store uses ZeroOrOne StoreLicenses
    /// </summary>
    public virtual StoreLicense? StoreLicense { get; private set; } = null!;

    public virtual void CreateRefToStoreLicense(StoreLicense relatedStoreLicense)
    {
        StoreLicense = relatedStoreLicense;
    }

    public virtual void DeleteRefToStoreLicense(StoreLicense relatedStoreLicense)
    {
        StoreLicense = null;
    }

    public virtual void DeleteAllRefToStoreLicense()
    {
        StoreLicense = null;
    }

    /// <summary>
    /// Store Verified emails ZeroOrOne EmailAddresses
    /// </summary>
    public virtual EmailAddress? VerifiedEmails { get; private set; }
    
    /// <summary>
    /// Creates a new EmailAddress entity.
    /// </summary>
    public virtual void CreateRefToVerifiedEmails(EmailAddress relatedEmailAddress)
    {
        VerifiedEmails = relatedEmailAddress;
    }
    
    /// <summary>
    /// Deletes owned EmailAddress entity.
    /// </summary>
    public virtual void DeleteRefToVerifiedEmails(EmailAddress relatedEmailAddress)
    {
        VerifiedEmails = null;
    }
    
    /// <summary>
    /// Deletes all owned EmailAddress entities.
    /// </summary>
    public virtual void DeleteAllRefToVerifiedEmails()
    {
        VerifiedEmails = null;
    }

    /// <summary>
    /// Entity tag used as concurrency token.
    /// </summary>
    public System.Guid Etag { get; set; } = System.Guid.NewGuid();
}