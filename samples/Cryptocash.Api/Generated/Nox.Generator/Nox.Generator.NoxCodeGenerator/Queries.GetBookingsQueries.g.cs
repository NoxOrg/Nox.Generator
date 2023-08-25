﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;

using Nox.Application.Commands;

using CryptocashApi.Application.Dto;
using CryptocashApi.Infrastructure.Persistence;

namespace CryptocashApi.Application.Queries;

public record GetBookingsQuery() : IRequest<IQueryable<BookingDto>>;

public partial class GetBookingsQueryHandler : QueryBase<IQueryable<BookingDto>>, IRequestHandler<GetBookingsQuery, IQueryable<BookingDto>>
{
    public  GetBookingsQueryHandler(DtoDbContext dataDbContext)
    {
        DataDbContext = dataDbContext;
    }

    public DtoDbContext DataDbContext { get; }

    public Task<IQueryable<BookingDto>> Handle(GetBookingsQuery request, CancellationToken cancellationToken)
    {
        var item = (IQueryable<BookingDto>)DataDbContext.Bookings
            .Where(r => r.DeletedAtUtc == null)
            .AsNoTracking();
       return Task.FromResult(OnResponse(item));
    }
}