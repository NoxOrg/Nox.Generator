﻿using MudBlazor.Services;
using Nox.Ui.Blazor.Lib.Services;
using Cryptocash.Ui.Services;

namespace Cryptocash.Ui.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddNoxUi(this IServiceCollection services)
    {
        services.AddMudServices();
        services.AddSingleton<ApplicationState>();
        services.AddSingleton<IEndpointsProvider, EndpointsProvider>();
        services.AddHttpClient<IBookingsService, BookingsService>();
        services.AddHttpClient<ICashStockOrdersService, CashStockOrdersService>();
        services.AddHttpClient<ICommissionsService, CommissionsService>();
        services.AddHttpClient<ICountriesService, CountriesService>();
        services.AddHttpClient<ICurrenciesService, CurrenciesService>();
        services.AddHttpClient<ICustomersService, CustomersService>();
        services.AddHttpClient<IEmployeesService, EmployeesService>();
        services.AddHttpClient<ILandLordsService, LandLordsService>();
        services.AddHttpClient<IMinimumCashStocksService, MinimumCashStocksService>();
        services.AddHttpClient<IPaymentDetailsService, PaymentDetailsService>();
        services.AddHttpClient<IPaymentProvidersService, PaymentProvidersService>();
        services.AddHttpClient<ITransactionsService, TransactionsService>();
        services.AddHttpClient<IVendingMachinesService, VendingMachinesService>();
        
        return services;
    }
}