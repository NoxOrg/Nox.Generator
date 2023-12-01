﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;
using Nox.Application.Commands;
using Nox.Solution;
using Nox.Types;
using ClientApi.Infrastructure.Persistence;
using ClientApi.Domain;
using ClientApi.Application.Dto;
using WorkplaceEntity = ClientApi.Domain.Workplace;

namespace ClientApi.Application.Commands;

public partial record DeleteWorkplaceByIdCommand(IEnumerable<WorkplaceKeyDto> KeyDtos, System.Guid? Etag) : IRequest<bool>;

internal class DeleteWorkplaceByIdCommandHandler : DeleteWorkplaceByIdCommandHandlerBase
{
	public DeleteWorkplaceByIdCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution) : base(dbContext, noxSolution)
	{
	}
}
internal abstract class DeleteWorkplaceByIdCommandHandlerBase : CommandBase<DeleteWorkplaceByIdCommand, WorkplaceEntity>, IRequestHandler<DeleteWorkplaceByIdCommand, bool>
{
	public AppDbContext DbContext { get; }

	public DeleteWorkplaceByIdCommandHandlerBase(
        AppDbContext dbContext,
		NoxSolution noxSolution) : base(noxSolution)
	{
		DbContext = dbContext;
	}

	public virtual async Task<bool> Handle(DeleteWorkplaceByIdCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		await OnExecutingAsync(request);
		
		foreach(var keyDto in request.KeyDtos)
		{
			var keyId = ClientApi.Domain.WorkplaceMetadata.CreateId(keyDto.keyId);		

			var entity = await DbContext.Workplaces.FindAsync(keyId);
			if (entity == null)
			{
				return false;
			}

			entity.Etag = request.Etag.HasValue ? request.Etag.Value : System.Guid.Empty;DbContext.Workplaces.Remove(entity);
		}

		await OnCompletedAsync(request, new WorkplaceEntity());

		await DbContext.SaveChangesAsync(cancellationToken);
		return true;
	}
}