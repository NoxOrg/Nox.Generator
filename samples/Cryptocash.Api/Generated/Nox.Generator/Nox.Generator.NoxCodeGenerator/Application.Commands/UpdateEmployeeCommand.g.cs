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
using EmployeeEntity = Cryptocash.Domain.Employee;

namespace Cryptocash.Application.Commands;

public partial record UpdateEmployeeCommand(System.Guid keyId, EmployeeUpdateDto EntityDto, Nox.Types.CultureCode CultureCode, System.Guid? Etag) : IRequest<EmployeeKeyDto>;

internal partial class UpdateEmployeeCommandHandler : UpdateEmployeeCommandHandlerBase
{
	public UpdateEmployeeCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<EmployeeEntity, EmployeeCreateDto, EmployeeUpdateDto> entityFactory)
		: base(dbContext, noxSolution,entityFactory)
	{
	}
}

internal abstract class UpdateEmployeeCommandHandlerBase : CommandBase<UpdateEmployeeCommand, EmployeeEntity>, IRequestHandler<UpdateEmployeeCommand, EmployeeKeyDto>
{
	public AppDbContext DbContext { get; }
	private readonly IEntityFactory<EmployeeEntity, EmployeeCreateDto, EmployeeUpdateDto> _entityFactory;

	protected UpdateEmployeeCommandHandlerBase(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<EmployeeEntity, EmployeeCreateDto, EmployeeUpdateDto> entityFactory)
		: base(noxSolution)
	{
		DbContext = dbContext;
		_entityFactory = entityFactory;
	}

	public virtual async Task<EmployeeKeyDto> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		await OnExecutingAsync(request);
		var keyId = Cryptocash.Domain.EmployeeMetadata.CreateId(request.keyId);

		var entity = await DbContext.Employees.FindAsync(keyId);
		if (entity == null)
		{
			throw new EntityNotFoundException("Employee",  $"{keyId.ToString()}");
		}
		await DbContext.Entry(entity).Collection(x => x.EmployeePhoneNumbers).LoadAsync(cancellationToken);

		await _entityFactory.UpdateEntityAsync(entity, request.EntityDto, request.CultureCode);
		entity.Etag = request.Etag.HasValue ? request.Etag.Value : System.Guid.Empty;

		await OnCompletedAsync(request, entity);

		DbContext.Entry(entity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();

		return new EmployeeKeyDto(entity.Id.Value);
	}
}

public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeValidator()
    {
    }
}