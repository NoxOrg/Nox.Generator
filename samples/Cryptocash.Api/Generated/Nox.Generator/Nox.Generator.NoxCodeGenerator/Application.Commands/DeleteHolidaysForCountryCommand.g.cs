﻿﻿
﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;
using Nox.Application.Commands;
using Nox.Solution;
using Nox.Types;
using Nox.Application.Factories;
using Cryptocash.Infrastructure.Persistence;
using Cryptocash.Domain;
using Cryptocash.Application.Dto;
using HolidayEntity = Cryptocash.Domain.Holiday;

namespace Cryptocash.Application.Commands;
public partial record DeleteHolidaysForCountryCommand(CountryKeyDto ParentKeyDto, HolidayKeyDto EntityKeyDto) : IRequest <bool>;

internal partial class DeleteHolidaysForCountryCommandHandler : DeleteHolidaysForCountryCommandHandlerBase
{
	public DeleteHolidaysForCountryCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution)
		: base(dbContext, noxSolution)
	{
	}
}

internal partial class DeleteHolidaysForCountryCommandHandlerBase : CommandBase<DeleteHolidaysForCountryCommand, HolidayEntity>, IRequestHandler <DeleteHolidaysForCountryCommand, bool>
{
	public AppDbContext DbContext { get; }

	public DeleteHolidaysForCountryCommandHandlerBase(
        AppDbContext dbContext,
		NoxSolution noxSolution) : base(noxSolution)
	{
		DbContext = dbContext;
	}

	public virtual async Task<bool> Handle(DeleteHolidaysForCountryCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		await OnExecutingAsync(request);
		var keyId = Cryptocash.Domain.CountryMetadata.CreateId(request.ParentKeyDto.keyId);
		var parentEntity = await DbContext.Countries.FindAsync(keyId);
		if (parentEntity == null)
		{
			return false;
		}
		await DbContext.Entry(parentEntity).Collection(p => p.Holidays).LoadAsync(cancellationToken);
		var ownedId = Cryptocash.Domain.HolidayMetadata.CreateId(request.EntityKeyDto.keyId);
		var entity = parentEntity.Holidays.SingleOrDefault(x => x.Id == ownedId);
		if (entity == null)
		{
			return false;
		}
		parentEntity.Holidays.Remove(entity);
		await OnCompletedAsync(request, entity);
		DbContext.Entry(entity).State = EntityState.Deleted;

		var result = await DbContext.SaveChangesAsync(cancellationToken);
		if (result < 1)
		{
			return false;
		}

		return true;
	}
}