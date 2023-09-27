// Generated

#nullable enable

using System;
using System.Collections.Generic;

using Nox.Abstractions;
using Nox.Domain;
using Nox.Solution;
using Nox.Types;

namespace Cryptocash.Domain;
internal partial class CashStockOrder:CashStockOrderBase
{

}
/// <summary>
/// Record for CashStockOrder created event.
/// </summary>
public record CashStockOrderCreated(CashStockOrderBase CashStockOrder) : IDomainEvent;
/// <summary>
/// Record for CashStockOrder updated event.
/// </summary>
public record CashStockOrderUpdated(CashStockOrderBase CashStockOrder) : IDomainEvent;
/// <summary>
/// Record for CashStockOrder deleted event.
/// </summary>
public record CashStockOrderDeleted(CashStockOrderBase CashStockOrder) : IDomainEvent;

/// <summary>
/// Vending machine cash stock order and related data.
/// </summary>
public abstract class CashStockOrderBase : AuditableEntityBase, IEntityConcurrent, IEntityHaveDomainEvents
{
    /// <summary>
    /// Vending machine's order unique identifier (Required).
    /// </summary>
    public Nox.Types.AutoNumber Id { get; set; } = null!;

    /// <summary>
    /// Order amount (Required).
    /// </summary>
    public Nox.Types.Money Amount { get; set; } = null!;

    /// <summary>
    /// Order requested delivery date (Required).
    /// </summary>
    public Nox.Types.Date RequestedDeliveryDate { get; set; } = null!;

    /// <summary>
    /// Order delivery date (Optional).
    /// </summary>
    public Nox.Types.DateTime? DeliveryDateTime { get; set; } = null!;

    /// <summary>
    /// Order status (Optional).
    /// </summary>
    public string? Status
    { 
        get { return DeliveryDateTime != null ? "delivered" : "ordered"; }
        private set { }
    }

	///<inheritdoc/>
	public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

	private readonly List<IDomainEvent> _domainEvents = new();
	
	///<inheritdoc/>
	public virtual void RaiseCreateEvent()
	{
		_domainEvents.Add(new CashStockOrderCreated(this));     
	}
	
	///<inheritdoc/>
	public virtual void RaiseUpdateEvent()
	{
		_domainEvents.Add(new CashStockOrderUpdated(this));  
	}
	
	///<inheritdoc/>
	public virtual void RaiseDeleteEvent()
	{
		_domainEvents.Add(new CashStockOrderDeleted(this)); 
	}
	
	///<inheritdoc />
    public virtual void ClearDomainEvents()
	{
		_domainEvents.Clear();
	}

    /// <summary>
    /// CashStockOrder for ExactlyOne VendingMachines
    /// </summary>
    public virtual VendingMachine CashStockOrderForVendingMachine { get; private set; } = null!;

    /// <summary>
    /// Foreign key for relationship ExactlyOne to entity VendingMachine
    /// </summary>
    public Nox.Types.Guid CashStockOrderForVendingMachineId { get; set; } = null!;

    public virtual void CreateRefToCashStockOrderForVendingMachine(VendingMachine relatedVendingMachine)
    {
        CashStockOrderForVendingMachine = relatedVendingMachine;
    }

    public virtual void DeleteRefToCashStockOrderForVendingMachine(VendingMachine relatedVendingMachine)
    {
        throw new Exception($"The relationship cannot be deleted.");
    }

    public virtual void DeleteAllRefToCashStockOrderForVendingMachine()
    {
        throw new Exception($"The relationship cannot be deleted.");
    }

    /// <summary>
    /// CashStockOrder reviewed by ExactlyOne Employees
    /// </summary>
    public virtual Employee CashStockOrderReviewedByEmployee { get; private set; } = null!;

    public virtual void CreateRefToCashStockOrderReviewedByEmployee(Employee relatedEmployee)
    {
        CashStockOrderReviewedByEmployee = relatedEmployee;
    }

    public virtual void DeleteRefToCashStockOrderReviewedByEmployee(Employee relatedEmployee)
    {
        throw new Exception($"The relationship cannot be deleted.");
    }

    public virtual void DeleteAllRefToCashStockOrderReviewedByEmployee()
    {
        throw new Exception($"The relationship cannot be deleted.");
    }

    /// <summary>
    /// Entity tag used as concurrency token.
    /// </summary>
    public System.Guid Etag { get; set; } = System.Guid.NewGuid();
}