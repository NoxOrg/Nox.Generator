﻿﻿// Generated

#nullable enable

using MediatR;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nox.Abstractions;
using Nox.Application;
using Nox.Application.Commands;
using Nox.Factories;
using Nox.Solution;

using Cryptocash.Infrastructure.Persistence;
using Cryptocash.Domain;
using Cryptocash.Application.Dto;
using Transaction = Cryptocash.Domain.Transaction;

namespace Cryptocash.Application.Commands;

public record CreateTransactionCommand(TransactionCreateDto EntityDto) : IRequest<TransactionKeyDto>;

public partial class CreateTransactionCommandHandler: CommandBase<CreateTransactionCommand,Transaction>, IRequestHandler <CreateTransactionCommand, TransactionKeyDto>
{
	private readonly CryptocashDbContext _dbContext;
	private readonly IEntityFactory<TransactionCreateDto,Transaction> _entityFactory;

	public CreateTransactionCommandHandler(
		CryptocashDbContext dbContext,
		NoxSolution noxSolution,
        IEntityFactory<TransactionCreateDto,Transaction> entityFactory,
		IServiceProvider serviceProvider): base(noxSolution, serviceProvider)
	{
		_dbContext = dbContext;
		_entityFactory = entityFactory;
	}

	public async Task<TransactionKeyDto> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);

		var entityToCreate = _entityFactory.CreateEntity(request.EntityDto);
					
		OnCompleted(entityToCreate);
		_dbContext.Transactions.Add(entityToCreate);
		await _dbContext.SaveChangesAsync();
		return new TransactionKeyDto(entityToCreate.Id.Value);
	}
}