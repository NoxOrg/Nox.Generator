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

public abstract record RefTestEntityTwoRelationshipsOneToManyToTestRelationshipOneCommand(TestEntityTwoRelationshipsOneToManyKeyDto EntityKeyDto, SecondTestEntityTwoRelationshipsOneToManyKeyDto? RelatedEntityKeyDto) : IRequest <bool>;

public record CreateRefTestEntityTwoRelationshipsOneToManyToTestRelationshipOneCommand(TestEntityTwoRelationshipsOneToManyKeyDto EntityKeyDto, SecondTestEntityTwoRelationshipsOneToManyKeyDto RelatedEntityKeyDto)
	: RefTestEntityTwoRelationshipsOneToManyToTestRelationshipOneCommand(EntityKeyDto, RelatedEntityKeyDto);

internal partial class CreateRefTestEntityTwoRelationshipsOneToManyToTestRelationshipOneCommandHandler
	: RefTestEntityTwoRelationshipsOneToManyToTestRelationshipOneCommandHandlerBase<CreateRefTestEntityTwoRelationshipsOneToManyToTestRelationshipOneCommand>
{
	public CreateRefTestEntityTwoRelationshipsOneToManyToTestRelationshipOneCommandHandler(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider
		)
		: base(dbContext, noxSolution, serviceProvider, RelationshipAction.Create)
	{ }
}

public record DeleteRefTestEntityTwoRelationshipsOneToManyToTestRelationshipOneCommand(TestEntityTwoRelationshipsOneToManyKeyDto EntityKeyDto, SecondTestEntityTwoRelationshipsOneToManyKeyDto RelatedEntityKeyDto)
	: RefTestEntityTwoRelationshipsOneToManyToTestRelationshipOneCommand(EntityKeyDto, RelatedEntityKeyDto);

internal partial class DeleteRefTestEntityTwoRelationshipsOneToManyToTestRelationshipOneCommandHandler
	: RefTestEntityTwoRelationshipsOneToManyToTestRelationshipOneCommandHandlerBase<DeleteRefTestEntityTwoRelationshipsOneToManyToTestRelationshipOneCommand>
{
	public DeleteRefTestEntityTwoRelationshipsOneToManyToTestRelationshipOneCommandHandler(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider
		)
		: base(dbContext, noxSolution, serviceProvider, RelationshipAction.Delete)
	{ }
}

public record DeleteAllRefTestEntityTwoRelationshipsOneToManyToTestRelationshipOneCommand(TestEntityTwoRelationshipsOneToManyKeyDto EntityKeyDto)
	: RefTestEntityTwoRelationshipsOneToManyToTestRelationshipOneCommand(EntityKeyDto, null);

internal partial class DeleteAllRefTestEntityTwoRelationshipsOneToManyToTestRelationshipOneCommandHandler
	: RefTestEntityTwoRelationshipsOneToManyToTestRelationshipOneCommandHandlerBase<DeleteAllRefTestEntityTwoRelationshipsOneToManyToTestRelationshipOneCommand>
{
	public DeleteAllRefTestEntityTwoRelationshipsOneToManyToTestRelationshipOneCommandHandler(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider
		)
		: base(dbContext, noxSolution, serviceProvider, RelationshipAction.DeleteAll)
	{ }
}

internal abstract class RefTestEntityTwoRelationshipsOneToManyToTestRelationshipOneCommandHandlerBase<TRequest> : CommandBase<TRequest, TestEntityTwoRelationshipsOneToMany>,
	IRequestHandler <TRequest, bool> where TRequest : RefTestEntityTwoRelationshipsOneToManyToTestRelationshipOneCommand
{
	public TestWebAppDbContext DbContext { get; }

	public RelationshipAction Action { get; }

	public enum RelationshipAction { Create, Delete, DeleteAll };

	public RefTestEntityTwoRelationshipsOneToManyToTestRelationshipOneCommandHandlerBase(
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
		var keyId = CreateNoxTypeForKey<TestEntityTwoRelationshipsOneToMany, Nox.Types.Text>("Id", request.EntityKeyDto.keyId);
		var entity = await DbContext.TestEntityTwoRelationshipsOneToManies.FindAsync(keyId);
		if (entity == null)
		{
			return false;
		}

		SecondTestEntityTwoRelationshipsOneToMany? relatedEntity = null!;
		if(request.RelatedEntityKeyDto is not null)
		{
			var relatedKeyId = CreateNoxTypeForKey<SecondTestEntityTwoRelationshipsOneToMany, Nox.Types.Text>("Id", request.RelatedEntityKeyDto.keyId);
			relatedEntity = await DbContext.SecondTestEntityTwoRelationshipsOneToManies.FindAsync(relatedKeyId);
			if (relatedEntity == null)
			{
				return false;
			}
		}

		switch (Action)
		{
			case RelationshipAction.Create:
				entity.CreateRefToTestRelationshipOne(relatedEntity);
				break;
			case RelationshipAction.Delete:
				entity.DeleteRefToTestRelationshipOne(relatedEntity);
				break;
			case RelationshipAction.DeleteAll:
				await DbContext.Entry(entity).Collection(x => x.TestRelationshipOne).LoadAsync();
				entity.DeleteAllRefToTestRelationshipOne();
				break;
		}

		OnCompleted(request, entity);

		DbContext.Entry(entity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();
		return true;
	}
}