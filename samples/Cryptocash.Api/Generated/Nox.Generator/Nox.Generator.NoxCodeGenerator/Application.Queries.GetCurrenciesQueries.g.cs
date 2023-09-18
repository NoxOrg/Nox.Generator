﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;

using Nox.Application.Commands;

using Cryptocash.Application.Dto;
using Cryptocash.Infrastructure.Persistence;

namespace Cryptocash.Application.Queries;

public record GetCurrenciesQuery() : IRequest<IQueryable<CurrencyDto>>;

public partial class GetCurrenciesQueryHandler: GetCurrenciesQueryHandlerBase
{
    public GetCurrenciesQueryHandler(DtoDbContext dataDbContext): base(dataDbContext)
    {
    
    }
}

public partial class GetCurrenciesQueryHandlerBase : QueryBase<IQueryable<CurrencyDto>>, IRequestHandler<GetCurrenciesQuery, IQueryable<CurrencyDto>>
{
    public  GetCurrenciesQueryHandlerBase(DtoDbContext dataDbContext)
    {
        DataDbContext = dataDbContext;
    }

    public DtoDbContext DataDbContext { get; }

    public virtual Task<IQueryable<CurrencyDto>> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
    {
        var item = (IQueryable<CurrencyDto>)DataDbContext.Currencies
            .Where(r => r.DeletedAtUtc == null)
            .AsNoTracking();
       return Task.FromResult(OnResponse(item));
    }
}