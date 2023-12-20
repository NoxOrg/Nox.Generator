﻿// Generated

#nullable enable

using System;
using System.Collections.Generic;

using MediatR;

using Nox.Abstractions;
using Nox.Domain;
using Nox.Solution;
using Nox.Types;
using Nox.Extensions;
using Nox.Exceptions;

namespace Cryptocash.Domain;

internal partial class EmployeePhoneNumber : EmployeePhoneNumberBase, IEntityHaveDomainEvents
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
/// Record for EmployeePhoneNumber created event.
/// </summary>
internal record EmployeePhoneNumberCreated(EmployeePhoneNumber EmployeePhoneNumber) :  IDomainEvent, INotification;
/// <summary>
/// Record for EmployeePhoneNumber updated event.
/// </summary>
internal record EmployeePhoneNumberUpdated(EmployeePhoneNumber EmployeePhoneNumber) : IDomainEvent, INotification;
/// <summary>
/// Record for EmployeePhoneNumber deleted event.
/// </summary>
internal record EmployeePhoneNumberDeleted(EmployeePhoneNumber EmployeePhoneNumber) : IDomainEvent, INotification;

/// <summary>
/// Employee phone number and related data.
/// </summary>
internal abstract partial class EmployeePhoneNumberBase : EntityBase, IOwnedEntity
{
    /// <summary>
    /// Employee's phone number identifier    
    /// </summary>
    /// <remarks>Required.</remarks>   
    public Nox.Types.AutoNumber Id { get; private set; } = null!;

    /// <summary>
    /// Employee's phone number type    
    /// </summary>
    /// <remarks>Required.</remarks>   
    public Nox.Types.Text PhoneNumberType { get;  set; } = null!;

    /// <summary>
    /// Employee's phone number    
    /// </summary>
    /// <remarks>Required.</remarks>   
    public Nox.Types.PhoneNumber PhoneNumber { get;  set; } = null!;
    /// <summary>
    /// Domain events raised by this entity.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => InternalDomainEvents;
    protected readonly List<IDomainEvent> InternalDomainEvents = new();

	protected virtual void InternalRaiseCreateEvent(EmployeePhoneNumber employeePhoneNumber)
	{
		InternalDomainEvents.Add(new EmployeePhoneNumberCreated(employeePhoneNumber));
    }
	
	protected virtual void InternalRaiseUpdateEvent(EmployeePhoneNumber employeePhoneNumber)
	{
		InternalDomainEvents.Add(new EmployeePhoneNumberUpdated(employeePhoneNumber));
    }
	
	protected virtual void InternalRaiseDeleteEvent(EmployeePhoneNumber employeePhoneNumber)
	{
		InternalDomainEvents.Add(new EmployeePhoneNumberDeleted(employeePhoneNumber));
    }
    /// <summary>
    /// Clears all domain events associated with the entity.
    /// </summary>
    public virtual void ClearDomainEvents()
    {
        InternalDomainEvents.Clear();
    }

    
}