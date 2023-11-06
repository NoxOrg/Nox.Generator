﻿﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;
using Nox.Application.Commands;
using Nox.Solution;
using Nox.Types;
using Nox.Application.Factories;
using Nox.Exceptions;
using Nox.Extensions;
using Cryptocash.Infrastructure.Persistence;
using Cryptocash.Domain;
using Cryptocash.Application.Dto;
using CashStockOrderEntity = Cryptocash.Domain.CashStockOrder;

namespace Cryptocash.Application.Commands;

public partial record UpdateCashStockOrderCommand(System.Int64 keyId, CashStockOrderUpdateDto EntityDto, System.Guid? Etag) : IRequest<CashStockOrderKeyDto?>;

internal partial class UpdateCashStockOrderCommandHandler : UpdateCashStockOrderCommandHandlerBase
{
	public UpdateCashStockOrderCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<CashStockOrderEntity, CashStockOrderCreateDto, CashStockOrderUpdateDto> entityFactory) : base(dbContext, noxSolution, entityFactory)
	{
	}
}

internal abstract class UpdateCashStockOrderCommandHandlerBase : CommandBase<UpdateCashStockOrderCommand, CashStockOrderEntity>, IRequestHandler<UpdateCashStockOrderCommand, CashStockOrderKeyDto?>
{
	public AppDbContext DbContext { get; }
	private readonly IEntityFactory<CashStockOrderEntity, CashStockOrderCreateDto, CashStockOrderUpdateDto> _entityFactory;

	public UpdateCashStockOrderCommandHandlerBase(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<CashStockOrderEntity, CashStockOrderCreateDto, CashStockOrderUpdateDto> entityFactory) : base(noxSolution)
	{
		DbContext = dbContext;
		_entityFactory = entityFactory;
	}

	public virtual async Task<CashStockOrderKeyDto?> Handle(UpdateCashStockOrderCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		await OnExecutingAsync(request);
		var keyId = Cryptocash.Domain.CashStockOrderMetadata.CreateId(request.keyId);

		var entity = await DbContext.CashStockOrders.FindAsync(keyId);
		if (entity == null)
		{
			return null;
		}

		var cashStockOrderForVendingMachineKey = Cryptocash.Domain.VendingMachineMetadata.CreateId(request.EntityDto.CashStockOrderForVendingMachineId);
		var cashStockOrderForVendingMachineEntity = await DbContext.VendingMachines.FindAsync(cashStockOrderForVendingMachineKey);
						
		if(cashStockOrderForVendingMachineEntity is not null)
			entity.CreateRefToCashStockOrderForVendingMachine(cashStockOrderForVendingMachineEntity);
		else
			throw new RelatedEntityNotFoundException("CashStockOrderForVendingMachine", request.EntityDto.CashStockOrderForVendingMachineId.ToString());

		var cashStockOrderReviewedByEmployeeKey = Cryptocash.Domain.EmployeeMetadata.CreateId(request.EntityDto.CashStockOrderReviewedByEmployeeId);
		var cashStockOrderReviewedByEmployeeEntity = await DbContext.Employees.FindAsync(cashStockOrderReviewedByEmployeeKey);
						
		if(cashStockOrderReviewedByEmployeeEntity is not null)
			entity.CreateRefToCashStockOrderReviewedByEmployee(cashStockOrderReviewedByEmployeeEntity);
		else
			throw new RelatedEntityNotFoundException("CashStockOrderReviewedByEmployee", request.EntityDto.CashStockOrderReviewedByEmployeeId.ToString());

		_entityFactory.UpdateEntity(entity, request.EntityDto);
		entity.Etag = request.Etag.HasValue ? request.Etag.Value : System.Guid.Empty;

		await OnCompletedAsync(request, entity);

		DbContext.Entry(entity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();
		if (result < 1)
		{
			return null;
		}

		return new CashStockOrderKeyDto(entity.Id.Value);
	}
}