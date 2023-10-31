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
using ThirdTestEntityZeroOrManyEntity = TestWebApp.Domain.ThirdTestEntityZeroOrMany;

namespace TestWebApp.Application.Commands;

public record CreateThirdTestEntityZeroOrManyCommand(ThirdTestEntityZeroOrManyCreateDto EntityDto) : IRequest<ThirdTestEntityZeroOrManyKeyDto>;

internal partial class CreateThirdTestEntityZeroOrManyCommandHandler : CreateThirdTestEntityZeroOrManyCommandHandlerBase
{
	public CreateThirdTestEntityZeroOrManyCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<TestWebApp.Domain.ThirdTestEntityOneOrMany, ThirdTestEntityOneOrManyCreateDto, ThirdTestEntityOneOrManyUpdateDto> ThirdTestEntityOneOrManyFactory,
		IEntityFactory<ThirdTestEntityZeroOrManyEntity, ThirdTestEntityZeroOrManyCreateDto, ThirdTestEntityZeroOrManyUpdateDto> entityFactory)
		: base(dbContext, noxSolution,ThirdTestEntityOneOrManyFactory, entityFactory)
	{
	}
}


internal abstract class CreateThirdTestEntityZeroOrManyCommandHandlerBase : CommandBase<CreateThirdTestEntityZeroOrManyCommand,ThirdTestEntityZeroOrManyEntity>, IRequestHandler <CreateThirdTestEntityZeroOrManyCommand, ThirdTestEntityZeroOrManyKeyDto>
{
	protected readonly AppDbContext DbContext;
	protected readonly IEntityFactory<ThirdTestEntityZeroOrManyEntity, ThirdTestEntityZeroOrManyCreateDto, ThirdTestEntityZeroOrManyUpdateDto> EntityFactory;
	protected readonly IEntityFactory<TestWebApp.Domain.ThirdTestEntityOneOrMany, ThirdTestEntityOneOrManyCreateDto, ThirdTestEntityOneOrManyUpdateDto> ThirdTestEntityOneOrManyFactory;

	public CreateThirdTestEntityZeroOrManyCommandHandlerBase(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<TestWebApp.Domain.ThirdTestEntityOneOrMany, ThirdTestEntityOneOrManyCreateDto, ThirdTestEntityOneOrManyUpdateDto> ThirdTestEntityOneOrManyFactory,
		IEntityFactory<ThirdTestEntityZeroOrManyEntity, ThirdTestEntityZeroOrManyCreateDto, ThirdTestEntityZeroOrManyUpdateDto> entityFactory) : base(noxSolution)
	{
		DbContext = dbContext;
		EntityFactory = entityFactory;
		this.ThirdTestEntityOneOrManyFactory = ThirdTestEntityOneOrManyFactory;
	}

	public virtual async Task<ThirdTestEntityZeroOrManyKeyDto> Handle(CreateThirdTestEntityZeroOrManyCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		await OnExecutingAsync(request);

		var entityToCreate = EntityFactory.CreateEntity(request.EntityDto);
		if(request.EntityDto.ThirdTestEntityOneOrManyRelationshipId.Any())
		{
			foreach(var relatedId in request.EntityDto.ThirdTestEntityOneOrManyRelationshipId)
			{
				var relatedKey = TestWebApp.Domain.ThirdTestEntityOneOrManyMetadata.CreateId(relatedId);
				var relatedEntity = await DbContext.ThirdTestEntityOneOrManies.FindAsync(relatedKey);

				if(relatedEntity is not null)
					entityToCreate.CreateRefToThirdTestEntityOneOrManyRelationship(relatedEntity);
				else
					throw new RelatedEntityNotFoundException("ThirdTestEntityOneOrManyRelationship", relatedId.ToString());
			}
		}
		else
		{
			foreach(var relatedCreateDto in request.EntityDto.ThirdTestEntityOneOrManyRelationship)
			{
				var relatedEntity = ThirdTestEntityOneOrManyFactory.CreateEntity(relatedCreateDto);
				entityToCreate.CreateRefToThirdTestEntityOneOrManyRelationship(relatedEntity);
			}
		}

		await OnCompletedAsync(request, entityToCreate);
		DbContext.ThirdTestEntityZeroOrManies.Add(entityToCreate);
		await DbContext.SaveChangesAsync();
		return new ThirdTestEntityZeroOrManyKeyDto(entityToCreate.Id.Value);
	}
}