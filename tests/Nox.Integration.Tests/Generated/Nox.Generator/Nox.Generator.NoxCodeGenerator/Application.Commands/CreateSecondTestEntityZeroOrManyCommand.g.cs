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
using FluentValidation;
using Microsoft.Extensions.Logging;

using TestWebApp.Infrastructure.Persistence;
using TestWebApp.Domain;
using TestWebApp.Application.Dto;
using SecondTestEntityZeroOrManyEntity = TestWebApp.Domain.SecondTestEntityZeroOrMany;

namespace TestWebApp.Application.Commands;

public partial record CreateSecondTestEntityZeroOrManyCommand(SecondTestEntityZeroOrManyCreateDto EntityDto, Nox.Types.CultureCode CultureCode) : IRequest<SecondTestEntityZeroOrManyKeyDto>;

internal partial class CreateSecondTestEntityZeroOrManyCommandHandler : CreateSecondTestEntityZeroOrManyCommandHandlerBase
{
	public CreateSecondTestEntityZeroOrManyCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<TestWebApp.Domain.TestEntityZeroOrMany, TestEntityZeroOrManyCreateDto, TestEntityZeroOrManyUpdateDto> TestEntityZeroOrManyFactory,
		IEntityFactory<SecondTestEntityZeroOrManyEntity, SecondTestEntityZeroOrManyCreateDto, SecondTestEntityZeroOrManyUpdateDto> entityFactory)
		: base(dbContext, noxSolution,TestEntityZeroOrManyFactory, entityFactory)
	{
	}
}


internal abstract class CreateSecondTestEntityZeroOrManyCommandHandlerBase : CommandBase<CreateSecondTestEntityZeroOrManyCommand,SecondTestEntityZeroOrManyEntity>, IRequestHandler <CreateSecondTestEntityZeroOrManyCommand, SecondTestEntityZeroOrManyKeyDto>
{
	protected readonly AppDbContext DbContext;
	protected readonly IEntityFactory<SecondTestEntityZeroOrManyEntity, SecondTestEntityZeroOrManyCreateDto, SecondTestEntityZeroOrManyUpdateDto> EntityFactory;
	protected readonly IEntityFactory<TestWebApp.Domain.TestEntityZeroOrMany, TestEntityZeroOrManyCreateDto, TestEntityZeroOrManyUpdateDto> TestEntityZeroOrManyFactory;

	protected CreateSecondTestEntityZeroOrManyCommandHandlerBase(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<TestWebApp.Domain.TestEntityZeroOrMany, TestEntityZeroOrManyCreateDto, TestEntityZeroOrManyUpdateDto> TestEntityZeroOrManyFactory,
		IEntityFactory<SecondTestEntityZeroOrManyEntity, SecondTestEntityZeroOrManyCreateDto, SecondTestEntityZeroOrManyUpdateDto> entityFactory)
		: base(noxSolution)
	{
		DbContext = dbContext;
		EntityFactory = entityFactory;
		this.TestEntityZeroOrManyFactory = TestEntityZeroOrManyFactory;
	}

	public virtual async Task<SecondTestEntityZeroOrManyKeyDto> Handle(CreateSecondTestEntityZeroOrManyCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		await OnExecutingAsync(request);

		var entityToCreate = await EntityFactory.CreateEntityAsync(request.EntityDto, request.CultureCode);
		if(request.EntityDto.TestEntityZeroOrManiesId.Any())
		{
			foreach(var relatedId in request.EntityDto.TestEntityZeroOrManiesId)
			{
				var relatedKey = TestWebApp.Domain.TestEntityZeroOrManyMetadata.CreateId(relatedId);
				var relatedEntity = await DbContext.TestEntityZeroOrManies.FindAsync(relatedKey);

				if(relatedEntity is not null)
					entityToCreate.CreateRefToTestEntityZeroOrManies(relatedEntity);
				else
					throw new RelatedEntityNotFoundException("TestEntityZeroOrManies", relatedId.ToString());
			}
		}
		else
		{
			foreach(var relatedCreateDto in request.EntityDto.TestEntityZeroOrManies)
			{
				var relatedEntity = await TestEntityZeroOrManyFactory.CreateEntityAsync(relatedCreateDto, request.CultureCode);
				entityToCreate.CreateRefToTestEntityZeroOrManies(relatedEntity);
			}
		}

		await OnCompletedAsync(request, entityToCreate);
		DbContext.SecondTestEntityZeroOrManies.Add(entityToCreate);
		await DbContext.SaveChangesAsync();
		return new SecondTestEntityZeroOrManyKeyDto(entityToCreate.Id.Value);
	}
}