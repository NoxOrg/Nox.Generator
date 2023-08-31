﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;

using Nox.Application.Commands;

using CryptocashApi.Application.Dto;
using CryptocashApi.Infrastructure.Persistence;

namespace CryptocashApi.Application.Queries;

public record GetCustomerPaymentDetailsQuery() : IRequest<IQueryable<CustomerPaymentDetailsDto>>;

public partial class GetCustomerPaymentDetailsQueryHandler : QueryBase<IQueryable<CustomerPaymentDetailsDto>>, IRequestHandler<GetCustomerPaymentDetailsQuery, IQueryable<CustomerPaymentDetailsDto>>
{
    public  GetCustomerPaymentDetailsQueryHandler(DtoDbContext dataDbContext)
    {
        DataDbContext = dataDbContext;
    }

    public DtoDbContext DataDbContext { get; }

    public Task<IQueryable<CustomerPaymentDetailsDto>> Handle(GetCustomerPaymentDetailsQuery request, CancellationToken cancellationToken)
    {
        var item = (IQueryable<CustomerPaymentDetailsDto>)DataDbContext.CustomerPaymentDetails
            .Where(r => r.DeletedAtUtc == null)
            .AsNoTracking();
       return Task.FromResult(OnResponse(item));
    }
}