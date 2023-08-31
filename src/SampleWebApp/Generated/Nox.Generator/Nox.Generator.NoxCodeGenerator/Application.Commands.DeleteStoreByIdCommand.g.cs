﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;
using Nox.Application.Commands;
using Nox.Solution;
using Nox.Types;
using SampleWebApp.Infrastructure.Persistence;
using SampleWebApp.Domain;
using Store = SampleWebApp.Domain.Store;

namespace SampleWebApp.Application.Commands;

public record DeleteStoreByIdCommand(System.String keyId) : IRequest<bool>;

public class DeleteStoreByIdCommandHandler: CommandBase<DeleteStoreByIdCommand,Store>, IRequestHandler<DeleteStoreByIdCommand, bool>
{
	public SampleWebAppDbContext DbContext { get; }

	public DeleteStoreByIdCommandHandler(
		SampleWebAppDbContext dbContext,
		NoxSolution noxSolution, 
		IServiceProvider serviceProvider): base(noxSolution, serviceProvider)
	{
		DbContext = dbContext;
	}

	public async Task<bool> Handle(DeleteStoreByIdCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);
		var keyId = CreateNoxTypeForKey<Store,Text>("Id", request.keyId);

		var entity = await DbContext.Stores.FindAsync(keyId);
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