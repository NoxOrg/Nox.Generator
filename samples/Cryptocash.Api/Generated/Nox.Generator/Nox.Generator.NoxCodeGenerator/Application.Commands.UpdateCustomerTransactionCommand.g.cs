﻿﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;
using Nox.Application.Commands;
using Nox.Solution;
using Nox.Types;
using Nox.Factories;
using Cryptocash.Infrastructure.Persistence;
using Cryptocash.Domain;
using Cryptocash.Application.Dto;
using CustomerTransaction = Cryptocash.Domain.CustomerTransaction;

namespace Cryptocash.Application.Commands;

public record UpdateCustomerTransactionCommand(System.Int64 keyId, CustomerTransactionUpdateDto EntityDto) : IRequest<CustomerTransactionKeyDto?>;

public class UpdateCustomerTransactionCommandHandler: CommandBase<UpdateCustomerTransactionCommand, CustomerTransaction>, IRequestHandler<UpdateCustomerTransactionCommand, CustomerTransactionKeyDto?>
{
	public CryptocashDbContext DbContext { get; }
	public IEntityMapper<CustomerTransaction> EntityMapper { get; }

	public UpdateCustomerTransactionCommandHandler(
		CryptocashDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider,
		IEntityMapper<CustomerTransaction> entityMapper): base(noxSolution, serviceProvider)
	{
		DbContext = dbContext;
		EntityMapper = entityMapper;
	}
	
	public async Task<CustomerTransactionKeyDto?> Handle(UpdateCustomerTransactionCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);
		var keyId = CreateNoxTypeForKey<CustomerTransaction,DatabaseNumber>("Id", request.keyId);
	
		var entity = await DbContext.CustomerTransactions.FindAsync(keyId);
		if (entity == null)
		{
			return null;
		}
		EntityMapper.MapToEntity(entity, GetEntityDefinition<CustomerTransaction>(), request.EntityDto);

		OnCompleted(entity);

		DbContext.Entry(entity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();
		if(result < 1)
			return null;

		return new CustomerTransactionKeyDto(entity.Id.Value);
	}
}