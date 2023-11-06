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
using TestWebApp.Infrastructure.Persistence;
using TestWebApp.Domain;
using TestWebApp.Application.Dto;
using TestEntityZeroOrOneToZeroOrManyEntity = TestWebApp.Domain.TestEntityZeroOrOneToZeroOrMany;

namespace TestWebApp.Application.Commands;

public partial record UpdateTestEntityZeroOrOneToZeroOrManyCommand(System.String keyId, TestEntityZeroOrOneToZeroOrManyUpdateDto EntityDto, System.Guid? Etag) : IRequest<TestEntityZeroOrOneToZeroOrManyKeyDto?>;

internal partial class UpdateTestEntityZeroOrOneToZeroOrManyCommandHandler : UpdateTestEntityZeroOrOneToZeroOrManyCommandHandlerBase
{
	public UpdateTestEntityZeroOrOneToZeroOrManyCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<TestEntityZeroOrOneToZeroOrManyEntity, TestEntityZeroOrOneToZeroOrManyCreateDto, TestEntityZeroOrOneToZeroOrManyUpdateDto> entityFactory) : base(dbContext, noxSolution, entityFactory)
	{
	}
}

internal abstract class UpdateTestEntityZeroOrOneToZeroOrManyCommandHandlerBase : CommandBase<UpdateTestEntityZeroOrOneToZeroOrManyCommand, TestEntityZeroOrOneToZeroOrManyEntity>, IRequestHandler<UpdateTestEntityZeroOrOneToZeroOrManyCommand, TestEntityZeroOrOneToZeroOrManyKeyDto?>
{
	public AppDbContext DbContext { get; }
	private readonly IEntityFactory<TestEntityZeroOrOneToZeroOrManyEntity, TestEntityZeroOrOneToZeroOrManyCreateDto, TestEntityZeroOrOneToZeroOrManyUpdateDto> _entityFactory;

	public UpdateTestEntityZeroOrOneToZeroOrManyCommandHandlerBase(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<TestEntityZeroOrOneToZeroOrManyEntity, TestEntityZeroOrOneToZeroOrManyCreateDto, TestEntityZeroOrOneToZeroOrManyUpdateDto> entityFactory) : base(noxSolution)
	{
		DbContext = dbContext;
		_entityFactory = entityFactory;
	}

	public virtual async Task<TestEntityZeroOrOneToZeroOrManyKeyDto?> Handle(UpdateTestEntityZeroOrOneToZeroOrManyCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		await OnExecutingAsync(request);
		var keyId = TestWebApp.Domain.TestEntityZeroOrOneToZeroOrManyMetadata.CreateId(request.keyId);

		var entity = await DbContext.TestEntityZeroOrOneToZeroOrManies.FindAsync(keyId);
		if (entity == null)
		{
			return null;
		}

		if(request.EntityDto.TestEntityZeroOrManyToZeroOrOneId is not null)
		{
			var testEntityZeroOrManyToZeroOrOneKey = TestWebApp.Domain.TestEntityZeroOrManyToZeroOrOneMetadata.CreateId(request.EntityDto.TestEntityZeroOrManyToZeroOrOneId.NonNullValue<System.String>());
			var testEntityZeroOrManyToZeroOrOneEntity = await DbContext.TestEntityZeroOrManyToZeroOrOnes.FindAsync(testEntityZeroOrManyToZeroOrOneKey);
						
			if(testEntityZeroOrManyToZeroOrOneEntity is not null)
				entity.CreateRefToTestEntityZeroOrManyToZeroOrOne(testEntityZeroOrManyToZeroOrOneEntity);
			else
				throw new RelatedEntityNotFoundException("TestEntityZeroOrManyToZeroOrOne", request.EntityDto.TestEntityZeroOrManyToZeroOrOneId.NonNullValue<System.String>().ToString());
		}
		else
		{
			entity.DeleteAllRefToTestEntityZeroOrManyToZeroOrOne();
		}

		_entityFactory.UpdateEntity(entity, request.EntityDto);
		entity.Etag = request.Etag.HasValue ? request.Etag.Value : System.Guid.Empty;

		await OnCompletedAsync(request, entity);

		DbContext.Entry(entity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();
		if (result < 1)
		{
			return null;
		}

		return new TestEntityZeroOrOneToZeroOrManyKeyDto(entity.Id.Value);
	}
}