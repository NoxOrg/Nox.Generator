﻿// Generated

#nullable enable

using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.OData.Formatter.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Nox.Lib;
using ClientApi.Application.Dto;
using DtoNameSpace = ClientApi.Application.Dto;

namespace ClientApi.Presentation.Api.OData;

internal static class ODataServiceCollectionExtensions
{
    public static void AddNoxOdata(this IServiceCollection services)
    {
        services.AddNoxOdata(null);
    }
    public static void AddNoxOdata(this IServiceCollection services, Action<ODataModelBuilder>? configure)
    {
        ODataModelBuilder builder = new ODataConventionModelBuilder();

        builder.EntitySet<CountryDto>("Countries");
		builder.EntityType<CountryDto>().HasKey(e => new { e.Id });
        builder.EntityType<CountryDto>().ContainsMany(e => e.CountryLocalNames).AutoExpand = true;
        builder.EntityType<CountryDto>().ContainsOptional(e => e.CountryBarCode).AutoExpand = true;
        builder.EntityType<CountryDto>().ContainsMany(e => e.Workplaces);
        builder.ComplexType<CountryUpdateDto>();
        builder.EntityType<CountryDto>().Ignore(e => e.DeletedAtUtc);
        builder.EntityType<CountryDto>().Ignore(e => e.Etag);

        builder.EntitySet<CountryLocalNameDto>("CountryLocalNames");
		builder.EntityType<CountryLocalNameDto>().HasKey(e => new { e.Id });
        builder.ComplexType<CountryLocalNameUpsertDto>();

		builder.EntityType<CountryBarCodeDto>().HasKey(e => new {  });
        builder.ComplexType<CountryBarCodeUpsertDto>();

        builder.EntitySet<RatingProgramDto>("RatingPrograms");
		builder.EntityType<RatingProgramDto>().HasKey(e => new { e.StoreId, e.Id });
        builder.ComplexType<RatingProgramUpdateDto>();

        builder.EntitySet<CountryQualityOfLifeIndexDto>("CountryQualityOfLifeIndices");
		builder.EntityType<CountryQualityOfLifeIndexDto>().HasKey(e => new { e.CountryId, e.Id });
        builder.ComplexType<CountryQualityOfLifeIndexUpdateDto>();

        builder.EntitySet<StoreDto>("Stores");
		builder.EntityType<StoreDto>().HasKey(e => new { e.Id });
        builder.EntityType<StoreDto>().ContainsOptional(e => e.EmailAddress).AutoExpand = true;
        builder.EntityType<StoreDto>().ContainsOptional(e => e.StoreOwner);
        builder.EntityType<StoreDto>().ContainsOptional(e => e.StoreLicense);
        builder.ComplexType<StoreUpdateDto>();
        builder.EntityType<StoreDto>().Ignore(e => e.DeletedAtUtc);
        builder.EntityType<StoreDto>().Ignore(e => e.Etag);

        builder.EntitySet<WorkplaceDto>("Workplaces");
		builder.EntityType<WorkplaceDto>().HasKey(e => new { e.Id });
        builder.EntityType<WorkplaceDto>().ContainsOptional(e => e.Country);
        builder.EntityType<WorkplaceDto>().ContainsMany(e => e.Tenants);
        builder.ComplexType<WorkplaceUpdateDto>();
        builder.EntityType<WorkplaceLocalizedDto>().HasKey(e => new { e.Id });
        builder.EntityType<WorkplaceDto>().Function("WorkplacesLocalized").ReturnsCollection<DtoNameSpace.WorkplaceLocalizedDto>();

        builder.EntitySet<StoreOwnerDto>("StoreOwners");
		builder.EntityType<StoreOwnerDto>().HasKey(e => new { e.Id });
        builder.EntityType<StoreOwnerDto>().ContainsMany(e => e.Stores);
        builder.ComplexType<StoreOwnerUpdateDto>();
        builder.EntityType<StoreOwnerDto>().Ignore(e => e.DeletedAtUtc);
        builder.EntityType<StoreOwnerDto>().Ignore(e => e.Etag);

        builder.EntitySet<StoreLicenseDto>("StoreLicenses");
		builder.EntityType<StoreLicenseDto>().HasKey(e => new { e.Id });
        builder.EntityType<StoreLicenseDto>().ContainsRequired(e => e.Store);
        builder.EntityType<StoreLicenseDto>().ContainsOptional(e => e.DefaultCurrency);
        builder.EntityType<StoreLicenseDto>().ContainsOptional(e => e.SoldInCurrency);
        builder.ComplexType<StoreLicenseUpdateDto>();
        builder.EntityType<StoreLicenseDto>().Ignore(e => e.DeletedAtUtc);
        builder.EntityType<StoreLicenseDto>().Ignore(e => e.Etag);

        builder.EntitySet<CurrencyDto>("Currencies");
		builder.EntityType<CurrencyDto>().HasKey(e => new { e.Id });
        builder.EntityType<CurrencyDto>().ContainsMany(e => e.StoreLicenseDefault);
        builder.EntityType<CurrencyDto>().ContainsMany(e => e.StoreLicenseSoldIn);
        builder.ComplexType<CurrencyUpdateDto>();
        builder.EntityType<CurrencyDto>().Ignore(e => e.DeletedAtUtc);
        builder.EntityType<CurrencyDto>().Ignore(e => e.Etag);

        builder.EntitySet<TenantDto>("Tenants");
		builder.EntityType<TenantDto>().HasKey(e => new { e.Id });
        builder.EntityType<TenantDto>().ContainsMany(e => e.TenantBrands).AutoExpand = true;
        builder.EntityType<TenantDto>().ContainsOptional(e => e.TenantContact).AutoExpand = true;
        builder.EntityType<TenantDto>().ContainsMany(e => e.Workplaces);
        builder.ComplexType<TenantUpdateDto>();

        builder.EntitySet<TenantBrandDto>("TenantBrands");
		builder.EntityType<TenantBrandDto>().HasKey(e => new { e.Id });
        builder.ComplexType<TenantBrandUpsertDto>();
        builder.EntityType<TenantBrandLocalizedDto>().HasKey(e => new { e.Id });
        builder.EntityType<TenantBrandDto>().Function("TenantBrandsLocalized").ReturnsCollection<DtoNameSpace.TenantBrandLocalizedDto>();

		builder.EntityType<TenantContactDto>().HasKey(e => new {  });
        builder.ComplexType<TenantContactUpsertDto>();
        builder.EntityType<TenantContactLocalizedDto>().HasKey(e => new {  });
        builder.EntityType<TenantContactDto>().Function("TenantContactsLocalized").ReturnsCollection<DtoNameSpace.TenantContactLocalizedDto>();

		builder.EntityType<EmailAddressDto>().HasKey(e => new {  });
        builder.ComplexType<EmailAddressUpsertDto>(); 
        // Setup Enumeration End Points
        builder.EntityType<CountryDto>()
                            .Collection
                            .Function("CountryContinents")
                            .ReturnsCollection<DtoNameSpace.CountryContinentDto>(); 
        // Setup Enumeration End Points
        builder.EntityType<StoreDto>()
                            .Collection
                            .Function("StoreStatuses")
                            .ReturnsCollection<DtoNameSpace.StoreStatusDto>();

       
        if(configure != null) configure(builder);

        services.AddControllers()
            .AddOData(options =>
                {
                    options.Select()
                        .EnableQueryFeatures(null)
                        .Filter()
                        .OrderBy()
                        .Count()
                        .Expand()
                        .SkipToken()
                        .SetMaxTop(100);
                    var routeOptions = options.AddRouteComponents(Nox.Presentation.Api.OData.ODataApi.GetRoutePrefix("/api/v1"), builder.GetEdmModel(),
                        service => service
                            .AddSingleton<IODataSerializerProvider, NoxODataSerializerProvider>())
                        .RouteOptions;
                    routeOptions.EnableKeyInParenthesis = false;
                }
            );
    }
}
