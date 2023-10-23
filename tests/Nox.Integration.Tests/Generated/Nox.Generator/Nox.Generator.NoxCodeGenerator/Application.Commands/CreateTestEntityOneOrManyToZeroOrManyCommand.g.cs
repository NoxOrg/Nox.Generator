﻿﻿// Generated

#nullable enable

using MediatR;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nox.Abstractions;
using Nox.Application;
using Nox.Application.Commands;
using Nox.Exceptions;
using Nox.Extensions;
using Nox.Application.Factories;
using Nox.Solution;

using TestWebApp.Infrastructure.Persistence;
using TestWebApp.Domain;
using TestWebApp.Application.Dto;
using TestEntityOneOrManyToZeroOrManyEntity = TestWebApp.Domain.TestEntityOneOrManyToZeroOrMany;

namespace TestWebApp.Application.Commands;

public record CreateTestEntityOneOrManyToZeroOrManyCommand(TestEntityOneOrManyToZeroOrManyCreateDto EntityDto) : IRequest<TestEntityOneOrManyToZeroOrManyKeyDto>;

internal partial class CreateTestEntityOneOrManyToZeroOrManyCommandHandler : CreateTestEntityOneOrManyToZeroOrManyCommandHandlerBase
{
	public CreateTestEntityOneOrManyToZeroOrManyCommandHandler(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<TestWebApp.Domain.TestEntityZeroOrManyToOneOrMany, TestEntityZeroOrManyToOneOrManyCreateDto, TestEntityZeroOrManyToOneOrManyUpdateDto> TestEntityZeroOrManyToOneOrManyFactory,
		IEntityFactory<TestEntityOneOrManyToZeroOrManyEntity, TestEntityOneOrManyToZeroOrManyCreateDto, TestEntityOneOrManyToZeroOrManyUpdateDto> entityFactory)
		: base(dbContext, noxSolution,TestEntityZeroOrManyToOneOrManyFactory, entityFactory)
	{
	}
}


internal abstract class CreateTestEntityOneOrManyToZeroOrManyCommandHandlerBase : CommandBase<CreateTestEntityOneOrManyToZeroOrManyCommand,TestEntityOneOrManyToZeroOrManyEntity>, IRequestHandler <CreateTestEntityOneOrManyToZeroOrManyCommand, TestEntityOneOrManyToZeroOrManyKeyDto>
{
	protected readonly TestWebAppDbContext DbContext;
	protected readonly IEntityFactory<TestEntityOneOrManyToZeroOrManyEntity, TestEntityOneOrManyToZeroOrManyCreateDto, TestEntityOneOrManyToZeroOrManyUpdateDto> EntityFactory;
	protected readonly IEntityFactory<TestWebApp.Domain.TestEntityZeroOrManyToOneOrMany, TestEntityZeroOrManyToOneOrManyCreateDto, TestEntityZeroOrManyToOneOrManyUpdateDto> TestEntityZeroOrManyToOneOrManyFactory;

	public CreateTestEntityOneOrManyToZeroOrManyCommandHandlerBase(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<TestWebApp.Domain.TestEntityZeroOrManyToOneOrMany, TestEntityZeroOrManyToOneOrManyCreateDto, TestEntityZeroOrManyToOneOrManyUpdateDto> TestEntityZeroOrManyToOneOrManyFactory,
		IEntityFactory<TestEntityOneOrManyToZeroOrManyEntity, TestEntityOneOrManyToZeroOrManyCreateDto, TestEntityOneOrManyToZeroOrManyUpdateDto> entityFactory) : base(noxSolution)
	{
		DbContext = dbContext;
		EntityFactory = entityFactory;
		this.TestEntityZeroOrManyToOneOrManyFactory = TestEntityZeroOrManyToOneOrManyFactory;
	}

	public virtual async Task<TestEntityOneOrManyToZeroOrManyKeyDto> Handle(CreateTestEntityOneOrManyToZeroOrManyCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);

		var entityToCreate = EntityFactory.CreateEntity(request.EntityDto);
		foreach(var relatedCreateDto in request.EntityDto.TestEntityZeroOrManyToOneOrMany)
		{
			var relatedEntity = TestEntityZeroOrManyToOneOrManyFactory.CreateEntity(relatedCreateDto);
			entityToCreate.CreateRefToTestEntityZeroOrManyToOneOrMany(relatedEntity);
		}

		await OnCompletedAsync(request, entityToCreate);
		DbContext.TestEntityOneOrManyToZeroOrManies.Add(entityToCreate);
		await DbContext.SaveChangesAsync();
		return new TestEntityOneOrManyToZeroOrManyKeyDto(entityToCreate.Id.Value);
	}
}