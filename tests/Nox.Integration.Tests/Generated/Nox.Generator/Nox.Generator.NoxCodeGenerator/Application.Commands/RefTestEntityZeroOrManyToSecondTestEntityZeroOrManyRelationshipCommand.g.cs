﻿
// Generated

#nullable enable

using MediatR;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nox.Application;
using Nox.Application.Commands;
using Nox.Application.Factories;
using Nox.Solution;
using Nox.Types;

using TestWebApp.Infrastructure.Persistence;
using TestWebApp.Domain;
using TestWebApp.Application.Dto;
using TestEntityZeroOrManyEntity = TestWebApp.Domain.TestEntityZeroOrMany;

namespace TestWebApp.Application.Commands;

public abstract record RefTestEntityZeroOrManyToSecondTestEntityZeroOrManyRelationshipCommand(TestEntityZeroOrManyKeyDto EntityKeyDto, SecondTestEntityZeroOrManyKeyDto? RelatedEntityKeyDto) : IRequest <bool>;

public record CreateRefTestEntityZeroOrManyToSecondTestEntityZeroOrManyRelationshipCommand(TestEntityZeroOrManyKeyDto EntityKeyDto, SecondTestEntityZeroOrManyKeyDto RelatedEntityKeyDto)
	: RefTestEntityZeroOrManyToSecondTestEntityZeroOrManyRelationshipCommand(EntityKeyDto, RelatedEntityKeyDto);

internal partial class CreateRefTestEntityZeroOrManyToSecondTestEntityZeroOrManyRelationshipCommandHandler
	: RefTestEntityZeroOrManyToSecondTestEntityZeroOrManyRelationshipCommandHandlerBase<CreateRefTestEntityZeroOrManyToSecondTestEntityZeroOrManyRelationshipCommand>
{
	public CreateRefTestEntityZeroOrManyToSecondTestEntityZeroOrManyRelationshipCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution
		)
		: base(dbContext, noxSolution, RelationshipAction.Create)
	{ }
}

public record DeleteRefTestEntityZeroOrManyToSecondTestEntityZeroOrManyRelationshipCommand(TestEntityZeroOrManyKeyDto EntityKeyDto, SecondTestEntityZeroOrManyKeyDto RelatedEntityKeyDto)
	: RefTestEntityZeroOrManyToSecondTestEntityZeroOrManyRelationshipCommand(EntityKeyDto, RelatedEntityKeyDto);

internal partial class DeleteRefTestEntityZeroOrManyToSecondTestEntityZeroOrManyRelationshipCommandHandler
	: RefTestEntityZeroOrManyToSecondTestEntityZeroOrManyRelationshipCommandHandlerBase<DeleteRefTestEntityZeroOrManyToSecondTestEntityZeroOrManyRelationshipCommand>
{
	public DeleteRefTestEntityZeroOrManyToSecondTestEntityZeroOrManyRelationshipCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution
		)
		: base(dbContext, noxSolution, RelationshipAction.Delete)
	{ }
}

public record DeleteAllRefTestEntityZeroOrManyToSecondTestEntityZeroOrManyRelationshipCommand(TestEntityZeroOrManyKeyDto EntityKeyDto)
	: RefTestEntityZeroOrManyToSecondTestEntityZeroOrManyRelationshipCommand(EntityKeyDto, null);

internal partial class DeleteAllRefTestEntityZeroOrManyToSecondTestEntityZeroOrManyRelationshipCommandHandler
	: RefTestEntityZeroOrManyToSecondTestEntityZeroOrManyRelationshipCommandHandlerBase<DeleteAllRefTestEntityZeroOrManyToSecondTestEntityZeroOrManyRelationshipCommand>
{
	public DeleteAllRefTestEntityZeroOrManyToSecondTestEntityZeroOrManyRelationshipCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution
		)
		: base(dbContext, noxSolution, RelationshipAction.DeleteAll)
	{ }
}

internal abstract class RefTestEntityZeroOrManyToSecondTestEntityZeroOrManyRelationshipCommandHandlerBase<TRequest> : CommandBase<TRequest, TestEntityZeroOrManyEntity>,
	IRequestHandler <TRequest, bool> where TRequest : RefTestEntityZeroOrManyToSecondTestEntityZeroOrManyRelationshipCommand
{
	public AppDbContext DbContext { get; }

	public RelationshipAction Action { get; }

	public enum RelationshipAction { Create, Delete, DeleteAll };

	public RefTestEntityZeroOrManyToSecondTestEntityZeroOrManyRelationshipCommandHandlerBase(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		RelationshipAction action)
		: base(noxSolution)
	{
		DbContext = dbContext;
		Action = action;
	}

	public virtual async Task<bool> Handle(TRequest request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		await OnExecutingAsync(request);
		var keyId = TestWebApp.Domain.TestEntityZeroOrManyMetadata.CreateId(request.EntityKeyDto.keyId);
		var entity = await DbContext.TestEntityZeroOrManies.FindAsync(keyId);
		if (entity == null)
		{
			return false;
		}

		TestWebApp.Domain.SecondTestEntityZeroOrMany? relatedEntity = null!;
		if(request.RelatedEntityKeyDto is not null)
		{
			var relatedKeyId = TestWebApp.Domain.SecondTestEntityZeroOrManyMetadata.CreateId(request.RelatedEntityKeyDto.keyId);
			relatedEntity = await DbContext.SecondTestEntityZeroOrManies.FindAsync(relatedKeyId);
			if (relatedEntity == null)
			{
				return false;
			}
		}

		switch (Action)
		{
			case RelationshipAction.Create:
				entity.CreateRefToSecondTestEntityZeroOrManyRelationship(relatedEntity);
				break;
			case RelationshipAction.Delete:
				entity.DeleteRefToSecondTestEntityZeroOrManyRelationship(relatedEntity);
				break;
			case RelationshipAction.DeleteAll:
				await DbContext.Entry(entity).Collection(x => x.SecondTestEntityZeroOrManyRelationship).LoadAsync();
				entity.DeleteAllRefToSecondTestEntityZeroOrManyRelationship();
				break;
		}

		await OnCompletedAsync(request, entity);

		DbContext.Entry(entity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();
		return true;
	}
}