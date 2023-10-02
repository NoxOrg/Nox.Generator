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

namespace Cryptocash.Application.Commands;
public record PartialUpdateEmployeePhoneNumberForEmployeeCommand(EmployeeKeyDto ParentKeyDto, EmployeePhoneNumberKeyDto EntityKeyDto, Dictionary<string, dynamic> UpdatedProperties, System.Guid? Etag) : IRequest <EmployeePhoneNumberKeyDto?>;
internal partial class PartialUpdateEmployeePhoneNumberForEmployeeCommandHandler: PartialUpdateEmployeePhoneNumberForEmployeeCommandHandlerBase
{
	public PartialUpdateEmployeePhoneNumberForEmployeeCommandHandler(
		CryptocashDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<EmployeePhoneNumber, EmployeePhoneNumberCreateDto, EmployeePhoneNumberUpdateDto> entityFactory) : base(dbContext, noxSolution, entityFactory)
	{
	}
}
internal abstract class PartialUpdateEmployeePhoneNumberForEmployeeCommandHandlerBase: CommandBase<PartialUpdateEmployeePhoneNumberForEmployeeCommand, EmployeePhoneNumber>, IRequestHandler <PartialUpdateEmployeePhoneNumberForEmployeeCommand, EmployeePhoneNumberKeyDto?>
{
	public CryptocashDbContext DbContext { get; }
	public IEntityFactory<EmployeePhoneNumber, EmployeePhoneNumberCreateDto, EmployeePhoneNumberUpdateDto> EntityFactory { get; }

	public PartialUpdateEmployeePhoneNumberForEmployeeCommandHandlerBase(
		CryptocashDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<EmployeePhoneNumber, EmployeePhoneNumberCreateDto, EmployeePhoneNumberUpdateDto> entityFactory) : base(noxSolution)
	{
		DbContext = dbContext;
		EntityFactory = entityFactory;
	}

	public virtual async Task<EmployeePhoneNumberKeyDto?> Handle(PartialUpdateEmployeePhoneNumberForEmployeeCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);
		var keyId = Cryptocash.Domain.EmployeeMetadata.CreateId(request.ParentKeyDto.keyId);

		var parentEntity = await DbContext.Employees.FindAsync(keyId);
		if (parentEntity == null)
		{
			return null;
		}
		var ownedId = Cryptocash.Domain.EmployeePhoneNumberMetadata.CreateId(request.EntityKeyDto.keyId);
		var entity = parentEntity.EmployeeContactPhoneNumbers.SingleOrDefault(x => x.Id == ownedId);
		if (entity == null)
		{
			return null;
		}

		EntityFactory.PartialUpdateEntity(entity, request.UpdatedProperties);
		parentEntity.Etag = request.Etag.HasValue ? request.Etag.Value : System.Guid.Empty;

		OnCompleted(request, entity);

		DbContext.Entry(parentEntity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();
		if (result < 1)
		{
			return null;
		}

		return new EmployeePhoneNumberKeyDto(entity.Id.Value);
	}
}