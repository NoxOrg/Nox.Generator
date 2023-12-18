﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;
using Nox.Application.Commands;
using Nox.Application.Factories;
using Nox.Solution;
using Nox.Types;
using Nox.Exceptions;

using ClientApi.Infrastructure.Persistence;
using ClientApi.Domain;
using ClientApi.Application.Dto;
using TenantEntity = ClientApi.Domain.Tenant;

namespace ClientApi.Application.Commands;

public partial record PartialUpdateTenantCommand(System.UInt32 keyId, Dictionary<string, dynamic> UpdatedProperties, Nox.Types.CultureCode CultureCode, System.Guid? Etag) : IRequest <TenantKeyDto>;

internal partial class PartialUpdateTenantCommandHandler : PartialUpdateTenantCommandHandlerBase
{
	public PartialUpdateTenantCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<TenantEntity, TenantCreateDto, TenantUpdateDto> entityFactory)
		: base(dbContext,noxSolution, entityFactory)
	{
	}
}
internal abstract class PartialUpdateTenantCommandHandlerBase : CommandBase<PartialUpdateTenantCommand, TenantEntity>, IRequestHandler<PartialUpdateTenantCommand, TenantKeyDto>
{
	public AppDbContext DbContext { get; }
	public IEntityFactory<TenantEntity, TenantCreateDto, TenantUpdateDto> EntityFactory { get; }

	public PartialUpdateTenantCommandHandlerBase(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<TenantEntity, TenantCreateDto, TenantUpdateDto> entityFactory)
		: base(noxSolution)
	{
		DbContext = dbContext;
		EntityFactory = entityFactory;
	}

	public virtual async Task<TenantKeyDto> Handle(PartialUpdateTenantCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		await OnExecutingAsync(request);
		var keyId = ClientApi.Domain.TenantMetadata.CreateId(request.keyId);

		var entity = await DbContext.Tenants.FindAsync(keyId);
		if (entity == null)
		{
			throw new EntityNotFoundException("Tenant",  $"{keyId.ToString()}");
		}
		await EntityFactory.PartialUpdateEntityAsync(entity, request.UpdatedProperties, request.CultureCode);
		entity.Etag = request.Etag.HasValue ? request.Etag.Value : System.Guid.Empty;

		await OnCompletedAsync(request, entity);

		DbContext.Entry(entity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();
		return new TenantKeyDto(entity.Id.Value);
	}
}