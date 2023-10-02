﻿
// Generated

#nullable enable

using MediatR;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nox.Application;
using Nox.Application.Commands;
using Nox.Factories;
using Nox.Solution;
using Nox.Types;

using TestWebApp.Infrastructure.Persistence;
using TestWebApp.Domain;
using TestWebApp.Application.Dto;

namespace TestWebApp.Application.Commands;

public abstract record RefTestEntityExactlyOneToSecondTestEntityExactlyOneRelationshipCommand(TestEntityExactlyOneKeyDto EntityKeyDto, SecondTestEntityExactlyOneKeyDto? RelatedEntityKeyDto) : IRequest <bool>;

public record CreateRefTestEntityExactlyOneToSecondTestEntityExactlyOneRelationshipCommand(TestEntityExactlyOneKeyDto EntityKeyDto, SecondTestEntityExactlyOneKeyDto RelatedEntityKeyDto)
	: RefTestEntityExactlyOneToSecondTestEntityExactlyOneRelationshipCommand(EntityKeyDto, RelatedEntityKeyDto);

internal partial class CreateRefTestEntityExactlyOneToSecondTestEntityExactlyOneRelationshipCommandHandler
	: RefTestEntityExactlyOneToSecondTestEntityExactlyOneRelationshipCommandHandlerBase<CreateRefTestEntityExactlyOneToSecondTestEntityExactlyOneRelationshipCommand>
{
	public CreateRefTestEntityExactlyOneToSecondTestEntityExactlyOneRelationshipCommandHandler(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider
		)
		: base(dbContext, noxSolution, serviceProvider, RelationshipAction.Create)
	{ }
}

public record DeleteRefTestEntityExactlyOneToSecondTestEntityExactlyOneRelationshipCommand(TestEntityExactlyOneKeyDto EntityKeyDto, SecondTestEntityExactlyOneKeyDto RelatedEntityKeyDto)
	: RefTestEntityExactlyOneToSecondTestEntityExactlyOneRelationshipCommand(EntityKeyDto, RelatedEntityKeyDto);

internal partial class DeleteRefTestEntityExactlyOneToSecondTestEntityExactlyOneRelationshipCommandHandler
	: RefTestEntityExactlyOneToSecondTestEntityExactlyOneRelationshipCommandHandlerBase<DeleteRefTestEntityExactlyOneToSecondTestEntityExactlyOneRelationshipCommand>
{
	public DeleteRefTestEntityExactlyOneToSecondTestEntityExactlyOneRelationshipCommandHandler(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider
		)
		: base(dbContext, noxSolution, serviceProvider, RelationshipAction.Delete)
	{ }
}

public record DeleteAllRefTestEntityExactlyOneToSecondTestEntityExactlyOneRelationshipCommand(TestEntityExactlyOneKeyDto EntityKeyDto)
	: RefTestEntityExactlyOneToSecondTestEntityExactlyOneRelationshipCommand(EntityKeyDto, null);

internal partial class DeleteAllRefTestEntityExactlyOneToSecondTestEntityExactlyOneRelationshipCommandHandler
	: RefTestEntityExactlyOneToSecondTestEntityExactlyOneRelationshipCommandHandlerBase<DeleteAllRefTestEntityExactlyOneToSecondTestEntityExactlyOneRelationshipCommand>
{
	public DeleteAllRefTestEntityExactlyOneToSecondTestEntityExactlyOneRelationshipCommandHandler(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider
		)
		: base(dbContext, noxSolution, serviceProvider, RelationshipAction.DeleteAll)
	{ }
}

internal abstract class RefTestEntityExactlyOneToSecondTestEntityExactlyOneRelationshipCommandHandlerBase<TRequest> : CommandBase<TRequest, TestEntityExactlyOne>,
	IRequestHandler <TRequest, bool> where TRequest : RefTestEntityExactlyOneToSecondTestEntityExactlyOneRelationshipCommand
{
	public TestWebAppDbContext DbContext { get; }

	public RelationshipAction Action { get; }

	public enum RelationshipAction { Create, Delete, DeleteAll };

	public RefTestEntityExactlyOneToSecondTestEntityExactlyOneRelationshipCommandHandlerBase(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider,
		RelationshipAction action)
		: base(noxSolution, serviceProvider)
	{
		DbContext = dbContext;
		Action = action;
	}

	public virtual async Task<bool> Handle(TRequest request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);
		var keyId = CreateNoxTypeForKey<TestEntityExactlyOne, Nox.Types.Text>("Id", request.EntityKeyDto.keyId);
		var entity = await DbContext.TestEntityExactlyOnes.FindAsync(keyId);
		if (entity == null)
		{
			return false;
		}

		SecondTestEntityExactlyOne? relatedEntity = null!;
		if(request.RelatedEntityKeyDto is not null)
		{
			var relatedKeyId = CreateNoxTypeForKey<SecondTestEntityExactlyOne, Nox.Types.Text>("Id", request.RelatedEntityKeyDto.keyId);
			relatedEntity = await DbContext.SecondTestEntityExactlyOnes.FindAsync(relatedKeyId);
			if (relatedEntity == null)
			{
				return false;
			}
		}

		switch (Action)
		{
			case RelationshipAction.Create:
				entity.CreateRefToSecondTestEntityExactlyOneRelationship(relatedEntity);
				break;
			case RelationshipAction.Delete:
				entity.DeleteRefToSecondTestEntityExactlyOneRelationship(relatedEntity);
				break;
			case RelationshipAction.DeleteAll:
				entity.DeleteAllRefToSecondTestEntityExactlyOneRelationship();
				break;
		}

		OnCompleted(request, entity);

		DbContext.Entry(entity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();
		return true;
	}
}