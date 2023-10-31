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
using TestEntityZeroOrManyToOneOrManyEntity = TestWebApp.Domain.TestEntityZeroOrManyToOneOrMany;

namespace TestWebApp.Application.Commands;

public record CreateTestEntityZeroOrManyToOneOrManyCommand(TestEntityZeroOrManyToOneOrManyCreateDto EntityDto) : IRequest<TestEntityZeroOrManyToOneOrManyKeyDto>;

internal partial class CreateTestEntityZeroOrManyToOneOrManyCommandHandler : CreateTestEntityZeroOrManyToOneOrManyCommandHandlerBase
{
	public CreateTestEntityZeroOrManyToOneOrManyCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<TestWebApp.Domain.TestEntityOneOrManyToZeroOrMany, TestEntityOneOrManyToZeroOrManyCreateDto, TestEntityOneOrManyToZeroOrManyUpdateDto> TestEntityOneOrManyToZeroOrManyFactory,
		IEntityFactory<TestEntityZeroOrManyToOneOrManyEntity, TestEntityZeroOrManyToOneOrManyCreateDto, TestEntityZeroOrManyToOneOrManyUpdateDto> entityFactory)
		: base(dbContext, noxSolution,TestEntityOneOrManyToZeroOrManyFactory, entityFactory)
	{
	}
}


internal abstract class CreateTestEntityZeroOrManyToOneOrManyCommandHandlerBase : CommandBase<CreateTestEntityZeroOrManyToOneOrManyCommand,TestEntityZeroOrManyToOneOrManyEntity>, IRequestHandler <CreateTestEntityZeroOrManyToOneOrManyCommand, TestEntityZeroOrManyToOneOrManyKeyDto>
{
	protected readonly AppDbContext DbContext;
	protected readonly IEntityFactory<TestEntityZeroOrManyToOneOrManyEntity, TestEntityZeroOrManyToOneOrManyCreateDto, TestEntityZeroOrManyToOneOrManyUpdateDto> EntityFactory;
	protected readonly IEntityFactory<TestWebApp.Domain.TestEntityOneOrManyToZeroOrMany, TestEntityOneOrManyToZeroOrManyCreateDto, TestEntityOneOrManyToZeroOrManyUpdateDto> TestEntityOneOrManyToZeroOrManyFactory;

	public CreateTestEntityZeroOrManyToOneOrManyCommandHandlerBase(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<TestWebApp.Domain.TestEntityOneOrManyToZeroOrMany, TestEntityOneOrManyToZeroOrManyCreateDto, TestEntityOneOrManyToZeroOrManyUpdateDto> TestEntityOneOrManyToZeroOrManyFactory,
		IEntityFactory<TestEntityZeroOrManyToOneOrManyEntity, TestEntityZeroOrManyToOneOrManyCreateDto, TestEntityZeroOrManyToOneOrManyUpdateDto> entityFactory) : base(noxSolution)
	{
		DbContext = dbContext;
		EntityFactory = entityFactory;
		this.TestEntityOneOrManyToZeroOrManyFactory = TestEntityOneOrManyToZeroOrManyFactory;
	}

	public virtual async Task<TestEntityZeroOrManyToOneOrManyKeyDto> Handle(CreateTestEntityZeroOrManyToOneOrManyCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		await OnExecutingAsync(request);

		var entityToCreate = EntityFactory.CreateEntity(request.EntityDto);
		if(request.EntityDto.TestEntityOneOrManyToZeroOrManyId.Any())
		{
			foreach(var relatedId in request.EntityDto.TestEntityOneOrManyToZeroOrManyId)
			{
				var relatedKey = TestWebApp.Domain.TestEntityOneOrManyToZeroOrManyMetadata.CreateId(relatedId);
				var relatedEntity = await DbContext.TestEntityOneOrManyToZeroOrManies.FindAsync(relatedKey);

				if(relatedEntity is not null)
					entityToCreate.CreateRefToTestEntityOneOrManyToZeroOrMany(relatedEntity);
				else
					throw new RelatedEntityNotFoundException("TestEntityOneOrManyToZeroOrMany", relatedId.ToString());
			}
		}
		else
		{
			foreach(var relatedCreateDto in request.EntityDto.TestEntityOneOrManyToZeroOrMany)
			{
				var relatedEntity = TestEntityOneOrManyToZeroOrManyFactory.CreateEntity(relatedCreateDto);
				entityToCreate.CreateRefToTestEntityOneOrManyToZeroOrMany(relatedEntity);
			}
		}

		await OnCompletedAsync(request, entityToCreate);
		DbContext.TestEntityZeroOrManyToOneOrManies.Add(entityToCreate);
		await DbContext.SaveChangesAsync();
		return new TestEntityZeroOrManyToOneOrManyKeyDto(entityToCreate.Id.Value);
	}
}