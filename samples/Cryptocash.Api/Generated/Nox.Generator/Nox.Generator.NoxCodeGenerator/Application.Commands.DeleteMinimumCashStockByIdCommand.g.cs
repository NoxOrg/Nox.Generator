﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;
using Nox.Application.Commands;
using Nox.Solution;
using Nox.Types;
using Cryptocash.Infrastructure.Persistence;
using Cryptocash.Domain;
using MinimumCashStock = Cryptocash.Domain.MinimumCashStock;

namespace Cryptocash.Application.Commands;

public record DeleteMinimumCashStockByIdCommand(System.Int64 keyId, System.Guid? Etag) : IRequest<bool>;

public class DeleteMinimumCashStockByIdCommandHandler:DeleteMinimumCashStockByIdCommandHandlerBase
{
	public DeleteMinimumCashStockByIdCommandHandler(
		CryptocashDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider): base(dbContext, noxSolution, serviceProvider)
	{
	}
}
public abstract class DeleteMinimumCashStockByIdCommandHandlerBase: CommandBase<DeleteMinimumCashStockByIdCommand,MinimumCashStock>, IRequestHandler<DeleteMinimumCashStockByIdCommand, bool>
{
	public CryptocashDbContext DbContext { get; }

	public DeleteMinimumCashStockByIdCommandHandlerBase(
		CryptocashDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider): base(noxSolution, serviceProvider)
	{
		DbContext = dbContext;
	}

	public virtual async Task<bool> Handle(DeleteMinimumCashStockByIdCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);
		var keyId = CreateNoxTypeForKey<MinimumCashStock,Nox.Types.AutoNumber>("Id", request.keyId);

		var entity = await DbContext.MinimumCashStocks.FindAsync(keyId);
		if (entity == null || entity.IsDeleted.Value == true)
		{
			return false;
		}

		entity.Etag = request.Etag.HasValue ? request.Etag.Value : System.Guid.Empty;

		OnCompleted(request, entity);
		DbContext.Entry(entity).State = EntityState.Deleted;
		await DbContext.SaveChangesAsync(cancellationToken);
		return true;
	}
}