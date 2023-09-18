﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;

using Nox.Application.Commands;

using Cryptocash.Application.Dto;
using Cryptocash.Infrastructure.Persistence;

namespace Cryptocash.Application.Queries;

public record GetCurrencyByIdQuery(System.String keyId) : IRequest <IQueryable<CurrencyDto>>;

public partial class GetCurrencyByIdQueryHandler:GetCurrencyByIdQueryHandlerBase
{
    public  GetCurrencyByIdQueryHandler(DtoDbContext dataDbContext): base(dataDbContext)
    {
    
    }
}

public partial class GetCurrencyByIdQueryHandlerBase:  QueryBase<IQueryable<CurrencyDto>>, IRequestHandler<GetCurrencyByIdQuery, IQueryable<CurrencyDto>>
{
    public  GetCurrencyByIdQueryHandlerBase(DtoDbContext dataDbContext)
    {
        DataDbContext = dataDbContext;
    }

    public DtoDbContext DataDbContext { get; }

    public virtual Task<IQueryable<CurrencyDto>> Handle(GetCurrencyByIdQuery request, CancellationToken cancellationToken)
    {    
        var query = DataDbContext.Currencies
            .AsNoTracking()
            .Where(r =>
                r.Id.Equals(request.keyId) &&
                r.DeletedAtUtc == null);
        return Task.FromResult(OnResponse(query));
    }
}