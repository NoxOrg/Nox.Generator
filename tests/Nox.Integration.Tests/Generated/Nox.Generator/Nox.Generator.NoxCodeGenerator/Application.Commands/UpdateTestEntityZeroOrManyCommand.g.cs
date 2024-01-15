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
using Dto = TestWebApp.Application.Dto;
using TestEntityZeroOrManyEntity = TestWebApp.Domain.TestEntityZeroOrMany;

namespace TestWebApp.Application.Commands;

public partial record UpdateTestEntityZeroOrManyCommand(System.String keyId, TestEntityZeroOrManyUpdateDto EntityDto, Nox.Types.CultureCode CultureCode, System.Guid? Etag) : IRequest<TestEntityZeroOrManyKeyDto>;

internal partial class UpdateTestEntityZeroOrManyCommandHandler : UpdateTestEntityZeroOrManyCommandHandlerBase
{
	public UpdateTestEntityZeroOrManyCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<TestEntityZeroOrManyEntity, TestEntityZeroOrManyCreateDto, TestEntityZeroOrManyUpdateDto> entityFactory)
		: base(dbContext, noxSolution, entityFactory)
	{
	}
}

internal abstract class UpdateTestEntityZeroOrManyCommandHandlerBase : CommandBase<UpdateTestEntityZeroOrManyCommand, TestEntityZeroOrManyEntity>, IRequestHandler<UpdateTestEntityZeroOrManyCommand, TestEntityZeroOrManyKeyDto>
{
	public AppDbContext DbContext { get; }
	private readonly IEntityFactory<TestEntityZeroOrManyEntity, TestEntityZeroOrManyCreateDto, TestEntityZeroOrManyUpdateDto> _entityFactory;
	protected UpdateTestEntityZeroOrManyCommandHandlerBase(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<TestEntityZeroOrManyEntity, TestEntityZeroOrManyCreateDto, TestEntityZeroOrManyUpdateDto> entityFactory)
		: base(noxSolution)
	{
		DbContext = dbContext;
		_entityFactory = entityFactory;
	}

	public virtual async Task<TestEntityZeroOrManyKeyDto> Handle(UpdateTestEntityZeroOrManyCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		await OnExecutingAsync(request);
		var keyId = Dto.TestEntityZeroOrManyMetadata.CreateId(request.keyId);

		var entity = await DbContext.TestEntityZeroOrManies.FindAsync(keyId);
		if (entity == null)
		{
			throw new EntityNotFoundException("TestEntityZeroOrMany",  $"{keyId.ToString()}");
		}

		await _entityFactory.UpdateEntityAsync(entity, request.EntityDto, request.CultureCode);
		entity.Etag = request.Etag.HasValue ? request.Etag.Value : System.Guid.Empty;

		await OnCompletedAsync(request, entity);

		DbContext.Entry(entity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();

		return new TestEntityZeroOrManyKeyDto(entity.Id.Value);
	}
}