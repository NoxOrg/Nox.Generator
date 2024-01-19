﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;

using Nox.Application.Queries;

using Cryptocash.Application.Dto;
using Cryptocash.Infrastructure.Persistence;

namespace Cryptocash.Application.Queries;

public partial record GetCurrenciesQuery() : IRequest<IQueryable<CurrencyDto>>;

internal partial class GetCurrenciesQueryHandler: GetCurrenciesQueryHandlerBase
{
    public GetCurrenciesQueryHandler(DtoDbContext dataDbContext): base(dataDbContext)
    {
    
    }
}

internal abstract class GetCurrenciesQueryHandlerBase : QueryBase<IQueryable<CurrencyDto>>, IRequestHandler<GetCurrenciesQuery, IQueryable<CurrencyDto>>
{
    public  GetCurrenciesQueryHandlerBase(DtoDbContext dataDbContext)
    {
        DataDbContext = dataDbContext;
    }

    public DtoDbContext DataDbContext { get; }

    public virtual Task<IQueryable<CurrencyDto>> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
    {
        var item = (IQueryable<CurrencyDto>)DataDbContext.Currencies
            .AsNoTracking()
            .Include(e => e.BankNotes)
            .Include(e => e.ExchangeRates);
       return Task.FromResult(OnResponse(item));
    }
}