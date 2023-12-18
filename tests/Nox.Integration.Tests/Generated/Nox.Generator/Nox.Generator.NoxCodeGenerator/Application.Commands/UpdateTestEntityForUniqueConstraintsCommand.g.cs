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
using TestWebApp.Infrastructure.Persistence;
using TestWebApp.Domain;
using TestWebApp.Application.Dto;
using TestEntityForUniqueConstraintsEntity = TestWebApp.Domain.TestEntityForUniqueConstraints;

namespace TestWebApp.Application.Commands;

public partial record UpdateTestEntityForUniqueConstraintsCommand(System.String keyId, TestEntityForUniqueConstraintsUpdateDto EntityDto, Nox.Types.CultureCode CultureCode, System.Guid? Etag) : IRequest<TestEntityForUniqueConstraintsKeyDto>;

internal partial class UpdateTestEntityForUniqueConstraintsCommandHandler : UpdateTestEntityForUniqueConstraintsCommandHandlerBase
{
	public UpdateTestEntityForUniqueConstraintsCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<TestEntityForUniqueConstraintsEntity, TestEntityForUniqueConstraintsCreateDto, TestEntityForUniqueConstraintsUpdateDto> entityFactory)
		: base(dbContext, noxSolution,entityFactory)
	{
	}
}

internal abstract class UpdateTestEntityForUniqueConstraintsCommandHandlerBase : CommandBase<UpdateTestEntityForUniqueConstraintsCommand, TestEntityForUniqueConstraintsEntity>, IRequestHandler<UpdateTestEntityForUniqueConstraintsCommand, TestEntityForUniqueConstraintsKeyDto>
{
	public AppDbContext DbContext { get; }
	private readonly IEntityFactory<TestEntityForUniqueConstraintsEntity, TestEntityForUniqueConstraintsCreateDto, TestEntityForUniqueConstraintsUpdateDto> _entityFactory;

	protected UpdateTestEntityForUniqueConstraintsCommandHandlerBase(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<TestEntityForUniqueConstraintsEntity, TestEntityForUniqueConstraintsCreateDto, TestEntityForUniqueConstraintsUpdateDto> entityFactory)
		: base(noxSolution)
	{
		DbContext = dbContext;
		_entityFactory = entityFactory;
	}

	public virtual async Task<TestEntityForUniqueConstraintsKeyDto> Handle(UpdateTestEntityForUniqueConstraintsCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		await OnExecutingAsync(request);
		var keyId = TestWebApp.Domain.TestEntityForUniqueConstraintsMetadata.CreateId(request.keyId);

		var entity = await DbContext.TestEntityForUniqueConstraints.FindAsync(keyId);
		if (entity == null)
		{
			throw new EntityNotFoundException("TestEntityForUniqueConstraints",  $"{keyId.ToString()}");
		}

		await _entityFactory.UpdateEntityAsync(entity, request.EntityDto, request.CultureCode);
		entity.Etag = request.Etag.HasValue ? request.Etag.Value : System.Guid.Empty;

		await OnCompletedAsync(request, entity);

		DbContext.Entry(entity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();
		if (result < 1)
		{
			throw new DatabaseSaveException();
		}

		return new TestEntityForUniqueConstraintsKeyDto(entity.Id.Value);
	}
}