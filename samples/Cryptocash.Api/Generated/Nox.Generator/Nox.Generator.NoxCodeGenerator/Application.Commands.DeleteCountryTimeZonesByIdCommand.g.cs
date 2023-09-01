﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;
using Nox.Application.Commands;
using Nox.Solution;
using Nox.Types;
using Cryptocash.Infrastructure.Persistence;
using Cryptocash.Domain;

namespace Cryptocash.Application.Commands;

public record DeleteCountryTimeZonesByIdCommand(System.Int64 keyId) : IRequest<bool>;

public class DeleteCountryTimeZonesByIdCommandHandler: CommandBase<DeleteCountryTimeZonesByIdCommand,CountryTimeZones>, IRequestHandler<DeleteCountryTimeZonesByIdCommand, bool>
{
	public CryptocashDbContext DbContext { get; }

	public DeleteCountryTimeZonesByIdCommandHandler(
		CryptocashDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider): base(noxSolution, serviceProvider)
	{
		DbContext = dbContext;
	}

	public async Task<bool> Handle(DeleteCountryTimeZonesByIdCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);
		var keyId = CreateNoxTypeForKey<CountryTimeZones,DatabaseNumber>("Id", request.keyId);

		var entity = await DbContext.CountryTimeZones.FindAsync(keyId);
		if (entity == null || entity.IsDeleted.Value == true)
		{
			return false;
		}

		OnCompleted(entity);
		DbContext.Entry(entity).State = EntityState.Deleted;
		await DbContext.SaveChangesAsync(cancellationToken);
		return true;
	}
}