﻿﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;
using Nox.Application.Commands;
using Nox.Factories;
using Nox.Solution;
using Nox.Types;

using ClientApi.Infrastructure.Persistence;
using ClientApi.Domain;
using ClientApi.Application.Dto;
using Workplace = ClientApi.Domain.Workplace;

namespace ClientApi.Application.Commands;

public record PartialUpdateWorkplaceCommand(System.UInt32 keyId, Dictionary<string, dynamic> UpdatedProperties, System.Guid? Etag) : IRequest <WorkplaceKeyDto?>;

public class PartialUpdateWorkplaceCommandHandler: CommandBase<PartialUpdateWorkplaceCommand, Workplace>, IRequestHandler<PartialUpdateWorkplaceCommand, WorkplaceKeyDto?>
{
	public ClientApiDbContext DbContext { get; }
	public IEntityMapper<Workplace> EntityMapper { get; }

	public PartialUpdateWorkplaceCommandHandler(
		ClientApiDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider,
		IEntityMapper<Workplace> entityMapper): base(noxSolution, serviceProvider)
	{
		DbContext = dbContext;
		EntityMapper = entityMapper;
	}

	public async Task<WorkplaceKeyDto?> Handle(PartialUpdateWorkplaceCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);
		var keyId = CreateNoxTypeForKey<Workplace,Nuid>("Id", request.keyId);

		var entity = await DbContext.Workplaces.FindAsync(keyId);
		if (entity == null)
		{
			return null;
		}
		EntityMapper.PartialMapToEntity(entity, GetEntityDefinition<Workplace>(), request.UpdatedProperties);
		entity.Etag = request.Etag.HasValue ? Nox.Types.Guid.From(request.Etag.Value) : Nox.Types.Guid.Empty;

		OnCompleted(entity);

		DbContext.Entry(entity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();
		return new WorkplaceKeyDto(entity.Id.Value);
	}
}