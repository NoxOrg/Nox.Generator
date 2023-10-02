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

using TestWebApp.Infrastructure.Persistence;
using TestWebApp.Domain;
using TestWebApp.Application.Dto;
using TestEntityZeroOrOneToZeroOrMany = TestWebApp.Domain.TestEntityZeroOrOneToZeroOrMany;

namespace TestWebApp.Application.Commands;

public record CreateTestEntityZeroOrOneToZeroOrManyCommand(TestEntityZeroOrOneToZeroOrManyCreateDto EntityDto) : IRequest<TestEntityZeroOrOneToZeroOrManyKeyDto>;

internal partial class CreateTestEntityZeroOrOneToZeroOrManyCommandHandler: CreateTestEntityZeroOrOneToZeroOrManyCommandHandlerBase
{
	public CreateTestEntityZeroOrOneToZeroOrManyCommandHandler(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<TestEntityZeroOrManyToZeroOrOne, TestEntityZeroOrManyToZeroOrOneCreateDto, TestEntityZeroOrManyToZeroOrOneUpdateDto> testentityzeroormanytozerooronefactory,
		IEntityFactory<TestEntityZeroOrOneToZeroOrMany, TestEntityZeroOrOneToZeroOrManyCreateDto, TestEntityZeroOrOneToZeroOrManyUpdateDto> entityFactory,
		IServiceProvider serviceProvider)
		: base(dbContext, noxSolution,testentityzeroormanytozerooronefactory, entityFactory, serviceProvider)
	{
	}
}


internal abstract class CreateTestEntityZeroOrOneToZeroOrManyCommandHandlerBase: CommandBase<CreateTestEntityZeroOrOneToZeroOrManyCommand,TestEntityZeroOrOneToZeroOrMany>, IRequestHandler <CreateTestEntityZeroOrOneToZeroOrManyCommand, TestEntityZeroOrOneToZeroOrManyKeyDto>
{
	private readonly TestWebAppDbContext _dbContext;
	private readonly IEntityFactory<TestEntityZeroOrOneToZeroOrMany, TestEntityZeroOrOneToZeroOrManyCreateDto, TestEntityZeroOrOneToZeroOrManyUpdateDto> _entityFactory;
	private readonly IEntityFactory<TestEntityZeroOrManyToZeroOrOne, TestEntityZeroOrManyToZeroOrOneCreateDto, TestEntityZeroOrManyToZeroOrOneUpdateDto> _testentityzeroormanytozerooronefactory;

	public CreateTestEntityZeroOrOneToZeroOrManyCommandHandlerBase(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<TestEntityZeroOrManyToZeroOrOne, TestEntityZeroOrManyToZeroOrOneCreateDto, TestEntityZeroOrManyToZeroOrOneUpdateDto> testentityzeroormanytozerooronefactory,
		IEntityFactory<TestEntityZeroOrOneToZeroOrMany, TestEntityZeroOrOneToZeroOrManyCreateDto, TestEntityZeroOrOneToZeroOrManyUpdateDto> entityFactory,
		IServiceProvider serviceProvider): base(noxSolution, serviceProvider)
	{
		_dbContext = dbContext;
		_entityFactory = entityFactory;
		_testentityzeroormanytozerooronefactory = testentityzeroormanytozerooronefactory;
	}

	public virtual async Task<TestEntityZeroOrOneToZeroOrManyKeyDto> Handle(CreateTestEntityZeroOrOneToZeroOrManyCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);

		var entityToCreate = _entityFactory.CreateEntity(request.EntityDto);
		if(request.EntityDto.TestEntityZeroOrManyToZeroOrOne is not null)
		{
			var relatedEntity = _testentityzeroormanytozerooronefactory.CreateEntity(request.EntityDto.TestEntityZeroOrManyToZeroOrOne);
			entityToCreate.CreateRefToTestEntityZeroOrManyToZeroOrOne(relatedEntity);
		}

		OnCompleted(request, entityToCreate);
		_dbContext.TestEntityZeroOrOneToZeroOrManies.Add(entityToCreate);
		await _dbContext.SaveChangesAsync();
		return new TestEntityZeroOrOneToZeroOrManyKeyDto(entityToCreate.Id.Value);
	}
}