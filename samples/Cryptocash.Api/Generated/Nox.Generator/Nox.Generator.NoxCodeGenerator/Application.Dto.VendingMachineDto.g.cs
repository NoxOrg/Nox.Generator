﻿// Generated

#nullable enable

using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations.Schema;

using MediatR;

using Nox.Application.Dto;
using Nox.Types;
using Nox.Domain;
using Nox.Extensions;
using System.Text.Json.Serialization;
using Cryptocash.Domain;

namespace Cryptocash.Application.Dto;

public record VendingMachineKeyDto(System.Guid keyId);

public partial class VendingMachineDto : VendingMachineDtoBase
{

}

/// <summary>
/// Vending machine definition and related data.
/// </summary>
public abstract class VendingMachineDtoBase : EntityDtoBase, IEntityDto<VendingMachine>
{

    #region Validation
    public virtual IReadOnlyDictionary<string, IEnumerable<string>> Validate()
    {
        var result = new Dictionary<string, IEnumerable<string>>();
    
        if (this.MacAddress is not null)
            TryGetValidationExceptions("MacAddress", () => Cryptocash.Domain.VendingMachineMetadata.CreateMacAddress(this.MacAddress.NonNullValue<System.String>()), result);
        else
            result.Add("MacAddress", new [] { "MacAddress is Required." });
    
        if (this.PublicIp is not null)
            TryGetValidationExceptions("PublicIp", () => Cryptocash.Domain.VendingMachineMetadata.CreatePublicIp(this.PublicIp.NonNullValue<System.String>()), result);
        else
            result.Add("PublicIp", new [] { "PublicIp is Required." });
    
        if (this.GeoLocation is not null)
            TryGetValidationExceptions("GeoLocation", () => Cryptocash.Domain.VendingMachineMetadata.CreateGeoLocation(this.GeoLocation.NonNullValue<LatLongDto>()), result);
        else
            result.Add("GeoLocation", new [] { "GeoLocation is Required." });
    
        if (this.StreetAddress is not null)
            TryGetValidationExceptions("StreetAddress", () => Cryptocash.Domain.VendingMachineMetadata.CreateStreetAddress(this.StreetAddress.NonNullValue<StreetAddressDto>()), result);
        else
            result.Add("StreetAddress", new [] { "StreetAddress is Required." });
    
        if (this.SerialNumber is not null)
            TryGetValidationExceptions("SerialNumber", () => Cryptocash.Domain.VendingMachineMetadata.CreateSerialNumber(this.SerialNumber.NonNullValue<System.String>()), result);
        else
            result.Add("SerialNumber", new [] { "SerialNumber is Required." });
    
        if (this.InstallationFootPrint is not null)
            TryGetValidationExceptions("InstallationFootPrint", () => Cryptocash.Domain.VendingMachineMetadata.CreateInstallationFootPrint(this.InstallationFootPrint.NonNullValue<System.Decimal>()), result);
        if (this.RentPerSquareMetre is not null)
            TryGetValidationExceptions("RentPerSquareMetre", () => Cryptocash.Domain.VendingMachineMetadata.CreateRentPerSquareMetre(this.RentPerSquareMetre.NonNullValue<MoneyDto>()), result);

        return result;
    }
    #endregion

    /// <summary>
    /// Vending machine unique identifier (Required).
    /// </summary>
    public System.Guid Id { get; set; } = default!;

    /// <summary>
    /// Vending machine mac address (Required).
    /// </summary>
    public System.String MacAddress { get; set; } = default!;

    /// <summary>
    /// Vending machine public ip (Required).
    /// </summary>
    public System.String PublicIp { get; set; } = default!;

    /// <summary>
    /// Vending machine geo location (Required).
    /// </summary>
    public LatLongDto GeoLocation { get; set; } = default!;

    /// <summary>
    /// Vending machine street address (Required).
    /// </summary>
    public StreetAddressDto StreetAddress { get; set; } = default!;

    /// <summary>
    /// Vending machine serial number (Required).
    /// </summary>
    public System.String SerialNumber { get; set; } = default!;

    /// <summary>
    /// Vending machine installation area (Optional).
    /// </summary>
    public System.Decimal? InstallationFootPrint { get; set; }

    /// <summary>
    /// Landlord rent amount based on area of the vending machine installation (Optional).
    /// </summary>
    public MoneyDto? RentPerSquareMetre { get; set; }

    /// <summary>
    /// VendingMachine installed in ExactlyOne Countries
    /// </summary>
    //EF maps ForeignKey Automatically
    public System.String? VendingMachineInstallationCountryId { get; set; } = default!;
    public virtual CountryDto? VendingMachineInstallationCountry { get; set; } = null!;

    /// <summary>
    /// VendingMachine contracted area leased by ExactlyOne LandLords
    /// </summary>
    //EF maps ForeignKey Automatically
    public System.Int64? VendingMachineContractedAreaLandLordId { get; set; } = default!;
    public virtual LandLordDto? VendingMachineContractedAreaLandLord { get; set; } = null!;

    /// <summary>
    /// VendingMachine related to ZeroOrMany Bookings
    /// </summary>
    public virtual List<BookingDto> VendingMachineRelatedBookings { get; set; } = new();

    /// <summary>
    /// VendingMachine related to ZeroOrMany CashStockOrders
    /// </summary>
    public virtual List<CashStockOrderDto> VendingMachineRelatedCashStockOrders { get; set; } = new();

    /// <summary>
    /// VendingMachine required ZeroOrMany MinimumCashStocks
    /// </summary>
    public virtual List<MinimumCashStockDto> VendingMachineRequiredMinimumCashStocks { get; set; } = new();
    public System.DateTime? DeletedAtUtc { get; set; }

    [JsonPropertyName("@odata.etag")]
    public System.Guid Etag { get; init; }
}