﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;
using Nox.Application.Commands;
using Nox.Solution;
using Nox.Types;
using CryptocashApi.Infrastructure.Persistence;
using CryptocashApi.Domain;
using VendingMachine = CryptocashApi.Domain.VendingMachine;

namespace CryptocashApi.Application.Commands;

public record DeleteVendingMachineByIdCommand(System.Guid keyId) : IRequest<bool>;

public class DeleteVendingMachineByIdCommandHandler: CommandBase<DeleteVendingMachineByIdCommand,VendingMachine>, IRequestHandler<DeleteVendingMachineByIdCommand, bool>
{
	public CryptocashApiDbContext DbContext { get; }

	public DeleteVendingMachineByIdCommandHandler(
		CryptocashApiDbContext dbContext,
		NoxSolution noxSolution, 
		IServiceProvider serviceProvider): base(noxSolution, serviceProvider)
	{
		DbContext = dbContext;
	}

	public async Task<bool> Handle(DeleteVendingMachineByIdCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);
		var keyId = CreateNoxTypeForKey<VendingMachine,DatabaseGuid>("Id", request.keyId);

		var entity = await DbContext.VendingMachines.FindAsync(keyId);
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