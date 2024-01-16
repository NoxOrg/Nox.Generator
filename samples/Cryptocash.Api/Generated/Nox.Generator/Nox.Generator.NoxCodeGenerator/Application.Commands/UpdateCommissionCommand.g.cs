﻿﻿﻿
// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;
using Nox.Application.Commands;
using Nox.Solution;
using Nox.Types;
using Nox.Application.Factories;
using Nox.Exceptions;
using Nox.Extensions;
using FluentValidation;
using Cryptocash.Infrastructure.Persistence;
using Cryptocash.Domain;
using Cryptocash.Application.Dto;
using Dto = Cryptocash.Application.Dto;
using CommissionEntity = Cryptocash.Domain.Commission;

namespace Cryptocash.Application.Commands;

public partial record UpdateCommissionCommand(System.Guid keyId, CommissionUpdateDto EntityDto, Nox.Types.CultureCode CultureCode, System.Guid? Etag) : IRequest<CommissionKeyDto>;

internal partial class UpdateCommissionCommandHandler : UpdateCommissionCommandHandlerBase
{
	public UpdateCommissionCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<CommissionEntity, CommissionCreateDto, CommissionUpdateDto> entityFactory)
		: base(dbContext, noxSolution, entityFactory)
	{
	}
}

internal abstract class UpdateCommissionCommandHandlerBase : CommandBase<UpdateCommissionCommand, CommissionEntity>, IRequestHandler<UpdateCommissionCommand, CommissionKeyDto>
{
	public AppDbContext DbContext { get; }
	private readonly IEntityFactory<CommissionEntity, CommissionCreateDto, CommissionUpdateDto> _entityFactory;
	protected UpdateCommissionCommandHandlerBase(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<CommissionEntity, CommissionCreateDto, CommissionUpdateDto> entityFactory)
		: base(noxSolution)
	{
		DbContext = dbContext;
		_entityFactory = entityFactory;
	}

	public virtual async Task<CommissionKeyDto> Handle(UpdateCommissionCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		await OnExecutingAsync(request);
		var keyId = Dto.CommissionMetadata.CreateId(request.keyId);

		var entity = await DbContext.Commissions.FindAsync(keyId);
		if (entity == null)
		{
			throw new EntityNotFoundException("Commission",  $"{keyId.ToString()}");
		}

		await _entityFactory.UpdateEntityAsync(entity, request.EntityDto, request.CultureCode);
		entity.Etag = request.Etag.HasValue ? request.Etag.Value : System.Guid.Empty;

		await OnCompletedAsync(request, entity);

		DbContext.Entry(entity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();

		return new CommissionKeyDto(entity.Id.Value);
	}
}