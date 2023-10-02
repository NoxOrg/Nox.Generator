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

public abstract record RefTestEntityZeroOrOneToExactlyOneToTestEntityExactlyOneToZeroOrOneCommand(TestEntityZeroOrOneToExactlyOneKeyDto EntityKeyDto, TestEntityExactlyOneToZeroOrOneKeyDto? RelatedEntityKeyDto) : IRequest <bool>;

public record CreateRefTestEntityZeroOrOneToExactlyOneToTestEntityExactlyOneToZeroOrOneCommand(TestEntityZeroOrOneToExactlyOneKeyDto EntityKeyDto, TestEntityExactlyOneToZeroOrOneKeyDto RelatedEntityKeyDto)
	: RefTestEntityZeroOrOneToExactlyOneToTestEntityExactlyOneToZeroOrOneCommand(EntityKeyDto, RelatedEntityKeyDto);

internal partial class CreateRefTestEntityZeroOrOneToExactlyOneToTestEntityExactlyOneToZeroOrOneCommandHandler
	: RefTestEntityZeroOrOneToExactlyOneToTestEntityExactlyOneToZeroOrOneCommandHandlerBase<CreateRefTestEntityZeroOrOneToExactlyOneToTestEntityExactlyOneToZeroOrOneCommand>
{
	public CreateRefTestEntityZeroOrOneToExactlyOneToTestEntityExactlyOneToZeroOrOneCommandHandler(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider
		)
		: base(dbContext, noxSolution, serviceProvider, RelationshipAction.Create)
	{ }
}

public record DeleteRefTestEntityZeroOrOneToExactlyOneToTestEntityExactlyOneToZeroOrOneCommand(TestEntityZeroOrOneToExactlyOneKeyDto EntityKeyDto, TestEntityExactlyOneToZeroOrOneKeyDto RelatedEntityKeyDto)
	: RefTestEntityZeroOrOneToExactlyOneToTestEntityExactlyOneToZeroOrOneCommand(EntityKeyDto, RelatedEntityKeyDto);

internal partial class DeleteRefTestEntityZeroOrOneToExactlyOneToTestEntityExactlyOneToZeroOrOneCommandHandler
	: RefTestEntityZeroOrOneToExactlyOneToTestEntityExactlyOneToZeroOrOneCommandHandlerBase<DeleteRefTestEntityZeroOrOneToExactlyOneToTestEntityExactlyOneToZeroOrOneCommand>
{
	public DeleteRefTestEntityZeroOrOneToExactlyOneToTestEntityExactlyOneToZeroOrOneCommandHandler(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider
		)
		: base(dbContext, noxSolution, serviceProvider, RelationshipAction.Delete)
	{ }
}

public record DeleteAllRefTestEntityZeroOrOneToExactlyOneToTestEntityExactlyOneToZeroOrOneCommand(TestEntityZeroOrOneToExactlyOneKeyDto EntityKeyDto)
	: RefTestEntityZeroOrOneToExactlyOneToTestEntityExactlyOneToZeroOrOneCommand(EntityKeyDto, null);

internal partial class DeleteAllRefTestEntityZeroOrOneToExactlyOneToTestEntityExactlyOneToZeroOrOneCommandHandler
	: RefTestEntityZeroOrOneToExactlyOneToTestEntityExactlyOneToZeroOrOneCommandHandlerBase<DeleteAllRefTestEntityZeroOrOneToExactlyOneToTestEntityExactlyOneToZeroOrOneCommand>
{
	public DeleteAllRefTestEntityZeroOrOneToExactlyOneToTestEntityExactlyOneToZeroOrOneCommandHandler(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider
		)
		: base(dbContext, noxSolution, serviceProvider, RelationshipAction.DeleteAll)
	{ }
}

internal abstract class RefTestEntityZeroOrOneToExactlyOneToTestEntityExactlyOneToZeroOrOneCommandHandlerBase<TRequest> : CommandBase<TRequest, TestEntityZeroOrOneToExactlyOne>,
	IRequestHandler <TRequest, bool> where TRequest : RefTestEntityZeroOrOneToExactlyOneToTestEntityExactlyOneToZeroOrOneCommand
{
	public TestWebAppDbContext DbContext { get; }

	public RelationshipAction Action { get; }

	public enum RelationshipAction { Create, Delete, DeleteAll };

	public RefTestEntityZeroOrOneToExactlyOneToTestEntityExactlyOneToZeroOrOneCommandHandlerBase(
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
		var keyId = CreateNoxTypeForKey<TestEntityZeroOrOneToExactlyOne, Nox.Types.Text>("Id", request.EntityKeyDto.keyId);
		var entity = await DbContext.TestEntityZeroOrOneToExactlyOnes.FindAsync(keyId);
		if (entity == null)
		{
			return false;
		}

		TestEntityExactlyOneToZeroOrOne? relatedEntity = null!;
		if(request.RelatedEntityKeyDto is not null)
		{
			var relatedKeyId = CreateNoxTypeForKey<TestEntityExactlyOneToZeroOrOne, Nox.Types.Text>("Id", request.RelatedEntityKeyDto.keyId);
			relatedEntity = await DbContext.TestEntityExactlyOneToZeroOrOnes.FindAsync(relatedKeyId);
			if (relatedEntity == null)
			{
				return false;
			}
		}

		switch (Action)
		{
			case RelationshipAction.Create:
				entity.CreateRefToTestEntityExactlyOneToZeroOrOne(relatedEntity);
				break;
			case RelationshipAction.Delete:
				entity.DeleteRefToTestEntityExactlyOneToZeroOrOne(relatedEntity);
				break;
			case RelationshipAction.DeleteAll:
				entity.DeleteAllRefToTestEntityExactlyOneToZeroOrOne();
				break;
		}

		OnCompleted(request, entity);

		DbContext.Entry(entity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();
		return true;
	}
}