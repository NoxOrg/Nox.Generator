﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;

using Nox.Application.Commands;

using TestWebApp.Application.Dto;
using TestWebApp.Infrastructure.Persistence;

namespace TestWebApp.Application.Queries;

public record GetSecondTestEntityZeroOrManiesQuery() : IRequest<IQueryable<SecondTestEntityZeroOrManyDto>>;

internal partial class GetSecondTestEntityZeroOrManiesQueryHandler: GetSecondTestEntityZeroOrManiesQueryHandlerBase
{
    public GetSecondTestEntityZeroOrManiesQueryHandler(DtoDbContext dataDbContext): base(dataDbContext)
    {
    
    }
}

internal abstract class GetSecondTestEntityZeroOrManiesQueryHandlerBase : QueryBase<IQueryable<SecondTestEntityZeroOrManyDto>>, IRequestHandler<GetSecondTestEntityZeroOrManiesQuery, IQueryable<SecondTestEntityZeroOrManyDto>>
{
    public  GetSecondTestEntityZeroOrManiesQueryHandlerBase(DtoDbContext dataDbContext)
    {
        DataDbContext = dataDbContext;
    }

    public DtoDbContext DataDbContext { get; }

    public virtual Task<IQueryable<SecondTestEntityZeroOrManyDto>> Handle(GetSecondTestEntityZeroOrManiesQuery request, CancellationToken cancellationToken)
    {
        var item = (IQueryable<SecondTestEntityZeroOrManyDto>)DataDbContext.SecondTestEntityZeroOrManies
            .Where(r => r.DeletedAtUtc == null)
            .AsNoTracking();
       return Task.FromResult(OnResponse(item));
    }
}