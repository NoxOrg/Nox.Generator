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
using ThirdTestEntityOneOrMany = TestWebApp.Domain.ThirdTestEntityOneOrMany;

namespace TestWebApp.Application.Commands;

public record CreateThirdTestEntityOneOrManyCommand(ThirdTestEntityOneOrManyCreateDto EntityDto) : IRequest<ThirdTestEntityOneOrManyKeyDto>;

internal partial class CreateThirdTestEntityOneOrManyCommandHandler: CreateThirdTestEntityOneOrManyCommandHandlerBase
{
	public CreateThirdTestEntityOneOrManyCommandHandler(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<ThirdTestEntityZeroOrMany, ThirdTestEntityZeroOrManyCreateDto, ThirdTestEntityZeroOrManyUpdateDto> thirdtestentityzeroormanyfactory,
		IEntityFactory<ThirdTestEntityOneOrMany, ThirdTestEntityOneOrManyCreateDto, ThirdTestEntityOneOrManyUpdateDto> entityFactory,
		IServiceProvider serviceProvider)
		: base(dbContext, noxSolution,thirdtestentityzeroormanyfactory, entityFactory, serviceProvider)
	{
	}
}


internal abstract class CreateThirdTestEntityOneOrManyCommandHandlerBase: CommandBase<CreateThirdTestEntityOneOrManyCommand,ThirdTestEntityOneOrMany>, IRequestHandler <CreateThirdTestEntityOneOrManyCommand, ThirdTestEntityOneOrManyKeyDto>
{
	private readonly TestWebAppDbContext _dbContext;
	private readonly IEntityFactory<ThirdTestEntityOneOrMany, ThirdTestEntityOneOrManyCreateDto, ThirdTestEntityOneOrManyUpdateDto> _entityFactory;
	private readonly IEntityFactory<ThirdTestEntityZeroOrMany, ThirdTestEntityZeroOrManyCreateDto, ThirdTestEntityZeroOrManyUpdateDto> _thirdtestentityzeroormanyfactory;

	public CreateThirdTestEntityOneOrManyCommandHandlerBase(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<ThirdTestEntityZeroOrMany, ThirdTestEntityZeroOrManyCreateDto, ThirdTestEntityZeroOrManyUpdateDto> thirdtestentityzeroormanyfactory,
		IEntityFactory<ThirdTestEntityOneOrMany, ThirdTestEntityOneOrManyCreateDto, ThirdTestEntityOneOrManyUpdateDto> entityFactory,
		IServiceProvider serviceProvider): base(noxSolution, serviceProvider)
	{
		_dbContext = dbContext;
		_entityFactory = entityFactory;
		_thirdtestentityzeroormanyfactory = thirdtestentityzeroormanyfactory;
	}

	public virtual async Task<ThirdTestEntityOneOrManyKeyDto> Handle(CreateThirdTestEntityOneOrManyCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);

		var entityToCreate = _entityFactory.CreateEntity(request.EntityDto);
		foreach(var relatedCreateDto in request.EntityDto.ThirdTestEntityZeroOrManyRelationship)
		{
			var relatedEntity = _thirdtestentityzeroormanyfactory.CreateEntity(relatedCreateDto);
			entityToCreate.CreateRefToThirdTestEntityZeroOrManyRelationship(relatedEntity);
		}

		OnCompleted(request, entityToCreate);
		_dbContext.ThirdTestEntityOneOrManies.Add(entityToCreate);
		await _dbContext.SaveChangesAsync();
		return new ThirdTestEntityOneOrManyKeyDto(entityToCreate.Id.Value);
	}
}