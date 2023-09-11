﻿﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;
using Nox.Application.Commands;
using Nox.Solution;
using Nox.Types;
using Nox.Factories;
using ClientApi.Infrastructure.Persistence;
using ClientApi.Domain;
using ClientApi.Application.Dto;
using StoreOwner = ClientApi.Domain.StoreOwner;

namespace ClientApi.Application.Commands;

public record UpdateStoreOwnerCommand(System.String keyId, StoreOwnerUpdateDto EntityDto) : IRequest<StoreOwnerKeyDto?>;

public class UpdateStoreOwnerCommandHandler: CommandBase<UpdateStoreOwnerCommand, StoreOwner>, IRequestHandler<UpdateStoreOwnerCommand, StoreOwnerKeyDto?>
{
	public ClientApiDbContext DbContext { get; }
	public IEntityMapper<StoreOwner> EntityMapper { get; }

	public UpdateStoreOwnerCommandHandler(
		ClientApiDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider,
		IEntityMapper<StoreOwner> entityMapper): base(noxSolution, serviceProvider)
	{
		DbContext = dbContext;
		EntityMapper = entityMapper;
	}
	
	public async Task<StoreOwnerKeyDto?> Handle(UpdateStoreOwnerCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);
		var keyId = CreateNoxTypeForKey<StoreOwner,Text>("Id", request.keyId);
	
		var entity = await DbContext.StoreOwners.FindAsync(keyId);
		if (entity == null)
		{
			return null;
		}
		EntityMapper.MapToEntity(entity, GetEntityDefinition<StoreOwner>(), request.EntityDto);

		OnCompleted(request, entity);

		DbContext.Entry(entity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();
		if (result < 1)
		{
			return null;
		}

		return new StoreOwnerKeyDto(entity.Id.Value);
	}
}