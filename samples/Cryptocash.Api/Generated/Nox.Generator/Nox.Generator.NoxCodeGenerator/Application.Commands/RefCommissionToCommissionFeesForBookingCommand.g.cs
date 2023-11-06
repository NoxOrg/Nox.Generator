﻿
// Generated

#nullable enable

using MediatR;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nox.Application;
using Nox.Application.Commands;
using Nox.Application.Factories;
using Nox.Solution;
using Nox.Types;

using Cryptocash.Infrastructure.Persistence;
using Cryptocash.Domain;
using Cryptocash.Application.Dto;
using CommissionEntity = Cryptocash.Domain.Commission;

namespace Cryptocash.Application.Commands;

public abstract record RefCommissionToCommissionFeesForBookingCommand(CommissionKeyDto EntityKeyDto, BookingKeyDto? RelatedEntityKeyDto) : IRequest <bool>;

public partial record CreateRef CommissionToCommissionFeesForBookingCommand(CommissionKeyDto EntityKeyDto, BookingKeyDto RelatedEntityKeyDto)
	: RefCommissionToCommissionFeesForBookingCommand(EntityKeyDto, RelatedEntityKeyDto);

internal partial class CreateRefCommissionToCommissionFeesForBookingCommandHandler
	: RefCommissionToCommissionFeesForBookingCommandHandlerBase<CreateRefCommissionToCommissionFeesForBookingCommand>
{
	public CreateRefCommissionToCommissionFeesForBookingCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution
		)
		: base(dbContext, noxSolution, RelationshipAction.Create)
	{ }
}

public record DeleteRefCommissionToCommissionFeesForBookingCommand(CommissionKeyDto EntityKeyDto, BookingKeyDto RelatedEntityKeyDto)
	: RefCommissionToCommissionFeesForBookingCommand(EntityKeyDto, RelatedEntityKeyDto);

internal partial class DeleteRefCommissionToCommissionFeesForBookingCommandHandler
	: RefCommissionToCommissionFeesForBookingCommandHandlerBase<DeleteRefCommissionToCommissionFeesForBookingCommand>
{
	public DeleteRefCommissionToCommissionFeesForBookingCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution
		)
		: base(dbContext, noxSolution, RelationshipAction.Delete)
	{ }
}

public record DeleteAllRefCommissionToCommissionFeesForBookingCommand(CommissionKeyDto EntityKeyDto)
	: RefCommissionToCommissionFeesForBookingCommand(EntityKeyDto, null);

internal partial class DeleteAllRefCommissionToCommissionFeesForBookingCommandHandler
	: RefCommissionToCommissionFeesForBookingCommandHandlerBase<DeleteAllRefCommissionToCommissionFeesForBookingCommand>
{
	public DeleteAllRefCommissionToCommissionFeesForBookingCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution
		)
		: base(dbContext, noxSolution, RelationshipAction.DeleteAll)
	{ }
}

internal abstract class RefCommissionToCommissionFeesForBookingCommandHandlerBase<TRequest> : CommandBase<TRequest, CommissionEntity>,
	IRequestHandler <TRequest, bool> where TRequest : RefCommissionToCommissionFeesForBookingCommand
{
	public AppDbContext DbContext { get; }

	public RelationshipAction Action { get; }

	public enum RelationshipAction { Create, Delete, DeleteAll };

	public RefCommissionToCommissionFeesForBookingCommandHandlerBase(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		RelationshipAction action)
		: base(noxSolution)
	{
		DbContext = dbContext;
		Action = action;
	}

	public virtual async Task<bool> Handle(TRequest request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		await OnExecutingAsync(request);
		var keyId = Cryptocash.Domain.CommissionMetadata.CreateId(request.EntityKeyDto.keyId);
		var entity = await DbContext.Commissions.FindAsync(keyId);
		if (entity == null)
		{
			return false;
		}

		Cryptocash.Domain.Booking? relatedEntity = null!;
		if(request.RelatedEntityKeyDto is not null)
		{
			var relatedKeyId = Cryptocash.Domain.BookingMetadata.CreateId(request.RelatedEntityKeyDto.keyId);
			relatedEntity = await DbContext.Bookings.FindAsync(relatedKeyId);
			if (relatedEntity == null)
			{
				return false;
			}
		}

		switch (Action)
		{
			case RelationshipAction.Create:
				entity.CreateRefToCommissionFeesForBooking(relatedEntity);
				break;
			case RelationshipAction.Delete:
				entity.DeleteRefToCommissionFeesForBooking(relatedEntity);
				break;
			case RelationshipAction.DeleteAll:
				await DbContext.Entry(entity).Collection(x => x.CommissionFeesForBooking).LoadAsync();
				entity.DeleteAllRefToCommissionFeesForBooking();
				break;
		}

		await OnCompletedAsync(request, entity);

		DbContext.Entry(entity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();
		return true;
	}
}