﻿// Generated

#nullable enable

using System;
using System.Collections.Generic;

using Nox.Abstractions;
using Nox.Domain;
using Nox.Types;

namespace Cryptocash.Domain;
public partial class VendingMachine:VendingMachineBase
{

}
/// <summary>
/// Record for VendingMachine created event.
/// </summary>
public record VendingMachineCreated(VendingMachine VendingMachine) : IDomainEvent;
/// <summary>
/// Record for VendingMachine updated event.
/// </summary>
public record VendingMachineUpdated(VendingMachine VendingMachine) : IDomainEvent;
/// <summary>
/// Record for VendingMachine deleted event.
/// </summary>
public record VendingMachineDeleted(VendingMachine VendingMachine) : IDomainEvent;

/// <summary>
/// Vending machine definition and related data.
/// </summary>
public abstract class VendingMachineBase : AuditableEntityBase, IEntityConcurrent
{
    /// <summary>
    /// Vending machine unique identifier (Required).
    /// </summary>
    public Nox.Types.Guid Id {get; set;} = null!;
    
    	public virtual void EnsureId(System.Guid guid)
    	{
    		if(System.Guid.Empty.Equals(guid))
    		{
    			Id = Nox.Types.Guid.From(System.Guid.NewGuid());
    		}
    		else
    		{
    			var currentGuid = Nox.Types.Guid.From(guid);
    			if(Id != currentGuid)
    			{
    				throw new NoxGuidTypeException("Immutable guid property Id value is different since it has been initialized");
    			}
    		}
    	}

    /// <summary>
    /// Vending machine mac address (Required).
    /// </summary>
    public Nox.Types.MacAddress MacAddress { get; set; } = null!;

    /// <summary>
    /// Vending machine public ip (Required).
    /// </summary>
    public Nox.Types.IpAddress PublicIp { get; set; } = null!;

    /// <summary>
    /// Vending machine geo location (Required).
    /// </summary>
    public Nox.Types.LatLong GeoLocation { get; set; } = null!;

    /// <summary>
    /// Vending machine street address (Required).
    /// </summary>
    public Nox.Types.StreetAddress StreetAddress { get; set; } = null!;

    /// <summary>
    /// Vending machine serial number (Required).
    /// </summary>
    public Nox.Types.Text SerialNumber { get; set; } = null!;

    /// <summary>
    /// Vending machine installation area (Optional).
    /// </summary>
    public Nox.Types.Area? InstallationFootPrint { get; set; } = null!;

    /// <summary>
    /// Landlord rent amount based on area of the vending machine installation (Optional).
    /// </summary>
    public Nox.Types.Money? RentPerSquareMetre { get; set; } = null!;

    /// <summary>
    /// VendingMachine installed in ExactlyOne Countries
    /// </summary>
    public virtual Country VendingMachineInstallationCountry { get; set; } = null!;

    /// <summary>
    /// Foreign key for relationship ExactlyOne to entity Country
    /// </summary>
    public Nox.Types.CountryCode2 VendingMachineInstallationCountryId { get; set; } = null!;

    public virtual void CreateRefToVendingMachineInstallationCountry(Country relatedCountry)
    {
        VendingMachineInstallationCountry = relatedCountry;
    }

    public virtual void DeleteRefToVendingMachineInstallationCountry(Country relatedCountry)
    {
        throw new Exception($"The relatioship cannot be deleted.");
    }

    /// <summary>
    /// VendingMachine contracted area leased by ExactlyOne LandLords
    /// </summary>
    public virtual LandLord VendingMachineContractedAreaLandLord { get; set; } = null!;

    /// <summary>
    /// Foreign key for relationship ExactlyOne to entity LandLord
    /// </summary>
    public Nox.Types.AutoNumber VendingMachineContractedAreaLandLordId { get; set; } = null!;

    public virtual void CreateRefToVendingMachineContractedAreaLandLord(LandLord relatedLandLord)
    {
        VendingMachineContractedAreaLandLord = relatedLandLord;
    }

    public virtual void DeleteRefToVendingMachineContractedAreaLandLord(LandLord relatedLandLord)
    {
        throw new Exception($"The relatioship cannot be deleted.");
    }

    /// <summary>
    /// VendingMachine related to ZeroOrMany Bookings
    /// </summary>
    public virtual List<Booking> VendingMachineRelatedBookings { get; set; } = new();

    public virtual void CreateRefToVendingMachineRelatedBookings(Booking relatedBooking)
    {
        VendingMachineRelatedBookings.Add(relatedBooking);
    }

    public virtual void DeleteRefToVendingMachineRelatedBookings(Booking relatedBooking)
    {
        VendingMachineRelatedBookings.Remove(relatedBooking);
    }

    /// <summary>
    /// VendingMachine related to ZeroOrMany CashStockOrders
    /// </summary>
    public virtual List<CashStockOrder> VendingMachineRelatedCashStockOrders { get; set; } = new();

    public virtual void CreateRefToVendingMachineRelatedCashStockOrders(CashStockOrder relatedCashStockOrder)
    {
        VendingMachineRelatedCashStockOrders.Add(relatedCashStockOrder);
    }

    public virtual void DeleteRefToVendingMachineRelatedCashStockOrders(CashStockOrder relatedCashStockOrder)
    {
        VendingMachineRelatedCashStockOrders.Remove(relatedCashStockOrder);
    }

    /// <summary>
    /// VendingMachine required ZeroOrMany MinimumCashStocks
    /// </summary>
    public virtual List<MinimumCashStock> VendingMachineRequiredMinimumCashStocks { get; set; } = new();

    public virtual void CreateRefToVendingMachineRequiredMinimumCashStocks(MinimumCashStock relatedMinimumCashStock)
    {
        VendingMachineRequiredMinimumCashStocks.Add(relatedMinimumCashStock);
    }

    public virtual void DeleteRefToVendingMachineRequiredMinimumCashStocks(MinimumCashStock relatedMinimumCashStock)
    {
        VendingMachineRequiredMinimumCashStocks.Remove(relatedMinimumCashStock);
    }

    /// <summary>
    /// Entity tag used as concurrency token.
    /// </summary>
    public System.Guid Etag { get; set; } = System.Guid.NewGuid();
}