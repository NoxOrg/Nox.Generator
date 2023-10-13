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
using ThirdTestEntityZeroOrOneEntity = TestWebApp.Domain.ThirdTestEntityZeroOrOne;

namespace TestWebApp.Application.Commands;

public record CreateThirdTestEntityZeroOrOneCommand(ThirdTestEntityZeroOrOneCreateDto EntityDto) : IRequest<ThirdTestEntityZeroOrOneKeyDto>;

internal partial class CreateThirdTestEntityZeroOrOneCommandHandler : CreateThirdTestEntityZeroOrOneCommandHandlerBase
{
	public CreateThirdTestEntityZeroOrOneCommandHandler(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<TestWebApp.Domain.ThirdTestEntityExactlyOne, ThirdTestEntityExactlyOneCreateDto, ThirdTestEntityExactlyOneUpdateDto> thirdtestentityexactlyonefactory,
		IEntityFactory<ThirdTestEntityZeroOrOneEntity, ThirdTestEntityZeroOrOneCreateDto, ThirdTestEntityZeroOrOneUpdateDto> entityFactory)
		: base(dbContext, noxSolution,thirdtestentityexactlyonefactory, entityFactory)
	{
	}
}


internal abstract class CreateThirdTestEntityZeroOrOneCommandHandlerBase : CommandBase<CreateThirdTestEntityZeroOrOneCommand,ThirdTestEntityZeroOrOneEntity>, IRequestHandler <CreateThirdTestEntityZeroOrOneCommand, ThirdTestEntityZeroOrOneKeyDto>
{
	private readonly TestWebAppDbContext _dbContext;
	private readonly IEntityFactory<ThirdTestEntityZeroOrOneEntity, ThirdTestEntityZeroOrOneCreateDto, ThirdTestEntityZeroOrOneUpdateDto> _entityFactory;
	private readonly IEntityFactory<TestWebApp.Domain.ThirdTestEntityExactlyOne, ThirdTestEntityExactlyOneCreateDto, ThirdTestEntityExactlyOneUpdateDto> _thirdtestentityexactlyonefactory;

	public CreateThirdTestEntityZeroOrOneCommandHandlerBase(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<TestWebApp.Domain.ThirdTestEntityExactlyOne, ThirdTestEntityExactlyOneCreateDto, ThirdTestEntityExactlyOneUpdateDto> thirdtestentityexactlyonefactory,
		IEntityFactory<ThirdTestEntityZeroOrOneEntity, ThirdTestEntityZeroOrOneCreateDto, ThirdTestEntityZeroOrOneUpdateDto> entityFactory) : base(noxSolution)
	{
		_dbContext = dbContext;
		_entityFactory = entityFactory;
		_thirdtestentityexactlyonefactory = thirdtestentityexactlyonefactory;
	}

	public virtual async Task<ThirdTestEntityZeroOrOneKeyDto> Handle(CreateThirdTestEntityZeroOrOneCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);

		var entityToCreate = _entityFactory.CreateEntity(request.EntityDto);
		if(request.EntityDto.ThirdTestEntityExactlyOneRelationshipId is not null)
		{
			var relatedKey = TestWebApp.Domain.ThirdTestEntityExactlyOneMetadata.CreateId(request.EntityDto.ThirdTestEntityExactlyOneRelationshipId.NonNullValue<System.String>());
			var relatedEntity = await _dbContext.ThirdTestEntityExactlyOnes.FindAsync(relatedKey);
			if(relatedEntity is not null)
				entityToCreate.CreateRefToThirdTestEntityExactlyOneRelationship(relatedEntity);
			else
				throw new RelatedEntityNotFoundException("ThirdTestEntityExactlyOneRelationship", request.EntityDto.ThirdTestEntityExactlyOneRelationshipId.NonNullValue<System.String>().ToString());
		}
		else if(request.EntityDto.ThirdTestEntityExactlyOneRelationship is not null)
		{
			var relatedEntity = _thirdtestentityexactlyonefactory.CreateEntity(request.EntityDto.ThirdTestEntityExactlyOneRelationship);
			entityToCreate.CreateRefToThirdTestEntityExactlyOneRelationship(relatedEntity);
		}

		await OnCompletedAsync(request, entityToCreate);
		_dbContext.ThirdTestEntityZeroOrOnes.Add(entityToCreate);
		await _dbContext.SaveChangesAsync();
		return new ThirdTestEntityZeroOrOneKeyDto(entityToCreate.Id.Value);
	}
}