﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;

using Nox.Application.Commands;

using Cryptocash.Application.Dto;
using Cryptocash.Infrastructure.Persistence;

namespace Cryptocash.Application.Queries;

public partial record Get PaymentProviderByIdQuery(System.Int64 keyId) : IRequest <IQueryable<PaymentProviderDto>>;

internal partial class GetPaymentProviderByIdQueryHandler:GetPaymentProviderByIdQueryHandlerBase
{
    public  GetPaymentProviderByIdQueryHandler(DtoDbContext dataDbContext): base(dataDbContext)
    {
    
    }
}

internal abstract class GetPaymentProviderByIdQueryHandlerBase:  QueryBase<IQueryable<PaymentProviderDto>>, IRequestHandler<GetPaymentProviderByIdQuery, IQueryable<PaymentProviderDto>>
{
    public  GetPaymentProviderByIdQueryHandlerBase(DtoDbContext dataDbContext)
    {
        DataDbContext = dataDbContext;
    }

    public DtoDbContext DataDbContext { get; }

    public virtual Task<IQueryable<PaymentProviderDto>> Handle(GetPaymentProviderByIdQuery request, CancellationToken cancellationToken)
    {    
        var query = DataDbContext.PaymentProviders
            .AsNoTracking()
            .Where(r =>
                r.Id.Equals(request.keyId));
        return Task.FromResult(OnResponse(query));
    }
}