﻿
// Generated

#nullable enable

using MediatR;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nox.Application;
using Nox.Application.Commands;
using Nox.Factories;
using Nox.Solution;
using Nox.Types;

using Cryptocash.Infrastructure.Persistence;
using Cryptocash.Domain;
using Cryptocash.Application.Dto;

namespace Cryptocash.Application.Commands;

public record CreateRefCashStockOrderToCashStockOrderReviewedByEmployeeCommand(CashStockOrderKeyDto EntityKeyDto, EmployeeKeyDto RelatedEntityKeyDto) : IRequest <bool>;

public partial class CreateRefCashStockOrderToCashStockOrderReviewedByEmployeeCommandHandler: CreateRefCashStockOrderToCashStockOrderReviewedByEmployeeCommandHandlerBase
{
	public CreateRefCashStockOrderToCashStockOrderReviewedByEmployeeCommandHandler(
		CryptocashDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider
		)
		: base(dbContext, noxSolution, serviceProvider)
	{ }
}

public abstract class CreateRefCashStockOrderToCashStockOrderReviewedByEmployeeCommandHandlerBase: CommandBase<CreateRefCashStockOrderToCashStockOrderReviewedByEmployeeCommand, CashStockOrder>, 
	IRequestHandler <CreateRefCashStockOrderToCashStockOrderReviewedByEmployeeCommand, bool>
{
	public CryptocashDbContext DbContext { get; }

	public CreateRefCashStockOrderToCashStockOrderReviewedByEmployeeCommandHandlerBase(
		CryptocashDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider): base(noxSolution, serviceProvider)
	{
		DbContext = dbContext;
	}

	public virtual async Task<bool> Handle(CreateRefCashStockOrderToCashStockOrderReviewedByEmployeeCommand request, CancellationToken cancellationToken)
	{
		OnExecuting(request);
		var keyId = CreateNoxTypeForKey<CashStockOrder, Nox.Types.AutoNumber>("Id", request.EntityKeyDto.keyId);
		var entity = await DbContext.CashStockOrders.FindAsync(keyId);
		if (entity == null)
		{
			return false;
		}
		var relatedKeyId = CreateNoxTypeForKey<Employee, Nox.Types.AutoNumber>("Id", request.RelatedEntityKeyDto.keyId);
		var relatedEntity = await DbContext.Employees.FindAsync(relatedKeyId);
		if (relatedEntity == null)
		{
			return false;
		}

		entity.CreateRefToEmployeeCashStockOrderReviewedByEmployee(relatedEntity);

		OnCompleted(request, entity);

		DbContext.Entry(entity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();
		return true;
	}
}