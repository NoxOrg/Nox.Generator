﻿﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;
using Nox.Application.Commands;
using Nox.Factories;
using Nox.Solution;
using Nox.Types;

using CryptocashApi.Infrastructure.Persistence;
using CryptocashApi.Domain;
using CryptocashApi.Application.Dto;
using LandLord = CryptocashApi.Domain.LandLord;

namespace CryptocashApi.Application.Commands;

public record PartialUpdateLandLordCommand(System.Int64 keyId, Dictionary<string, dynamic> UpdatedProperties) : IRequest <LandLordKeyDto?>;

public class PartialUpdateLandLordCommandHandler: CommandBase<PartialUpdateLandLordCommand, LandLord>, IRequestHandler<PartialUpdateLandLordCommand, LandLordKeyDto?>
{
	public CryptocashApiDbContext DbContext { get; }
	public IEntityMapper<LandLord> EntityMapper { get; }

	public PartialUpdateLandLordCommandHandler(
		CryptocashApiDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider,
		IEntityMapper<LandLord> entityMapper): base(noxSolution, serviceProvider)
	{
		DbContext = dbContext;
		EntityMapper = entityMapper;
	}

	public async Task<LandLordKeyDto?> Handle(PartialUpdateLandLordCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);
		var keyId = CreateNoxTypeForKey<LandLord,DatabaseNumber>("Id", request.keyId);

		var entity = await DbContext.LandLords.FindAsync(keyId);
		if (entity == null)
		{
			return null;
		}
		EntityMapper.PartialMapToEntity(entity, GetEntityDefinition<LandLord>(), request.UpdatedProperties);

		OnCompleted(entity);

		DbContext.Entry(entity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();
		return new LandLordKeyDto(entity.Id.Value);
	}
}