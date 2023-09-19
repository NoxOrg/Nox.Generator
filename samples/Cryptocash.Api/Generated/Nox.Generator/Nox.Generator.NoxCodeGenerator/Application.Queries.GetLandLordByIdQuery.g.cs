﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;

using Nox.Application.Commands;

using Cryptocash.Application.Dto;
using Cryptocash.Infrastructure.Persistence;

namespace Cryptocash.Application.Queries;

public record GetLandLordByIdQuery(System.Int64 keyId) : IRequest <IQueryable<LandLordDto>>;

public partial class GetLandLordByIdQueryHandler:GetLandLordByIdQueryHandlerBase
{
    public  GetLandLordByIdQueryHandler(DtoDbContext dataDbContext): base(dataDbContext)
    {
    
    }
}

public abstract class GetLandLordByIdQueryHandlerBase:  QueryBase<IQueryable<LandLordDto>>, IRequestHandler<GetLandLordByIdQuery, IQueryable<LandLordDto>>
{
    public  GetLandLordByIdQueryHandlerBase(DtoDbContext dataDbContext)
    {
        DataDbContext = dataDbContext;
    }

    public DtoDbContext DataDbContext { get; }

    public virtual Task<IQueryable<LandLordDto>> Handle(GetLandLordByIdQuery request, CancellationToken cancellationToken)
    {    
        var query = DataDbContext.LandLords
            .AsNoTracking()
            .Where(r =>
                r.Id.Equals(request.keyId) &&
                r.DeletedAtUtc == null);
        return Task.FromResult(OnResponse(query));
    }
}