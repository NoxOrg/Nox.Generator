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
using TestEntityZeroOrOne = TestWebApp.Domain.TestEntityZeroOrOne;

namespace TestWebApp.Application.Commands;

public record CreateTestEntityZeroOrOneCommand(TestEntityZeroOrOneCreateDto EntityDto) : IRequest<TestEntityZeroOrOneKeyDto>;

internal partial class CreateTestEntityZeroOrOneCommandHandler: CreateTestEntityZeroOrOneCommandHandlerBase
{
	public CreateTestEntityZeroOrOneCommandHandler(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<SecondTestEntityZeroOrOne, SecondTestEntityZeroOrOneCreateDto, SecondTestEntityZeroOrOneUpdateDto> secondtestentityzerooronefactory,
		IEntityFactory<TestEntityZeroOrOne, TestEntityZeroOrOneCreateDto, TestEntityZeroOrOneUpdateDto> entityFactory,
		IServiceProvider serviceProvider)
		: base(dbContext, noxSolution,secondtestentityzerooronefactory, entityFactory, serviceProvider)
	{
	}
}


internal abstract class CreateTestEntityZeroOrOneCommandHandlerBase: CommandBase<CreateTestEntityZeroOrOneCommand,TestEntityZeroOrOne>, IRequestHandler <CreateTestEntityZeroOrOneCommand, TestEntityZeroOrOneKeyDto>
{
	private readonly TestWebAppDbContext _dbContext;
	private readonly IEntityFactory<TestEntityZeroOrOne, TestEntityZeroOrOneCreateDto, TestEntityZeroOrOneUpdateDto> _entityFactory;
	private readonly IEntityFactory<SecondTestEntityZeroOrOne, SecondTestEntityZeroOrOneCreateDto, SecondTestEntityZeroOrOneUpdateDto> _secondtestentityzerooronefactory;

	public CreateTestEntityZeroOrOneCommandHandlerBase(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<SecondTestEntityZeroOrOne, SecondTestEntityZeroOrOneCreateDto, SecondTestEntityZeroOrOneUpdateDto> secondtestentityzerooronefactory,
		IEntityFactory<TestEntityZeroOrOne, TestEntityZeroOrOneCreateDto, TestEntityZeroOrOneUpdateDto> entityFactory,
		IServiceProvider serviceProvider): base(noxSolution, serviceProvider)
	{
		_dbContext = dbContext;
		_entityFactory = entityFactory;
		_secondtestentityzerooronefactory = secondtestentityzerooronefactory;
	}

	public virtual async Task<TestEntityZeroOrOneKeyDto> Handle(CreateTestEntityZeroOrOneCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);

		var entityToCreate = _entityFactory.CreateEntity(request.EntityDto);
		if(request.EntityDto.SecondTestEntityZeroOrOneRelationship is not null)
		{
			var relatedEntity = _secondtestentityzerooronefactory.CreateEntity(request.EntityDto.SecondTestEntityZeroOrOneRelationship);
			entityToCreate.CreateRefToSecondTestEntityZeroOrOneRelationship(relatedEntity);
		}

		OnCompleted(request, entityToCreate);
		_dbContext.TestEntityZeroOrOnes.Add(entityToCreate);
		await _dbContext.SaveChangesAsync();
		return new TestEntityZeroOrOneKeyDto(entityToCreate.Id.Value);
	}
}