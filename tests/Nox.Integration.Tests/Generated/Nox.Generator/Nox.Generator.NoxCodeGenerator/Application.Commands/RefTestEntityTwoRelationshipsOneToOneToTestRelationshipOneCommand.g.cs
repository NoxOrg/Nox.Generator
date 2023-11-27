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
using TestEntityTwoRelationshipsOneToOneEntity = TestWebApp.Domain.TestEntityTwoRelationshipsOneToOne;

namespace TestWebApp.Application.Commands;

public abstract record RefTestEntityTwoRelationshipsOneToOneToTestRelationshipOneCommand(TestEntityTwoRelationshipsOneToOneKeyDto EntityKeyDto) : IRequest <bool>;

#region CreateRefTo

public partial record CreateRefTestEntityTwoRelationshipsOneToOneToTestRelationshipOneCommand(TestEntityTwoRelationshipsOneToOneKeyDto EntityKeyDto, SecondTestEntityTwoRelationshipsOneToOneKeyDto RelatedEntityKeyDto)
	: RefTestEntityTwoRelationshipsOneToOneToTestRelationshipOneCommand(EntityKeyDto);

internal partial class CreateRefTestEntityTwoRelationshipsOneToOneToTestRelationshipOneCommandHandler
	: RefTestEntityTwoRelationshipsOneToOneToTestRelationshipOneCommandHandlerBase<CreateRefTestEntityTwoRelationshipsOneToOneToTestRelationshipOneCommand>
{
	public CreateRefTestEntityTwoRelationshipsOneToOneToTestRelationshipOneCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution
		)
		: base(dbContext, noxSolution)
	{ }

	protected override async Task<bool> ExecuteAsync(CreateRefTestEntityTwoRelationshipsOneToOneToTestRelationshipOneCommand request)
    {
		var entity = await GetTestEntityTwoRelationshipsOneToOne(request.EntityKeyDto);
		if (entity == null)
		{
			return false;
		}

		var relatedEntity = await GetSecondTestEntityTwoRelationshipsOneToOne(request.RelatedEntityKeyDto);
		if (relatedEntity == null)
		{
			return false;
		}

		entity.CreateRefToTestRelationshipOne(relatedEntity);

		return await SaveChangesAsync(request, entity);
    }
}

#endregion CreateRefTo

#region DeleteRefTo

public record DeleteRefTestEntityTwoRelationshipsOneToOneToTestRelationshipOneCommand(TestEntityTwoRelationshipsOneToOneKeyDto EntityKeyDto, SecondTestEntityTwoRelationshipsOneToOneKeyDto RelatedEntityKeyDto)
	: RefTestEntityTwoRelationshipsOneToOneToTestRelationshipOneCommand(EntityKeyDto);

internal partial class DeleteRefTestEntityTwoRelationshipsOneToOneToTestRelationshipOneCommandHandler
	: RefTestEntityTwoRelationshipsOneToOneToTestRelationshipOneCommandHandlerBase<DeleteRefTestEntityTwoRelationshipsOneToOneToTestRelationshipOneCommand>
{
	public DeleteRefTestEntityTwoRelationshipsOneToOneToTestRelationshipOneCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution
		)
		: base(dbContext, noxSolution)
	{ }

	protected override async Task<bool> ExecuteAsync(DeleteRefTestEntityTwoRelationshipsOneToOneToTestRelationshipOneCommand request)
    {
        var entity = await GetTestEntityTwoRelationshipsOneToOne(request.EntityKeyDto);
		if (entity == null)
		{
			return false;
		}

		var relatedEntity = await GetSecondTestEntityTwoRelationshipsOneToOne(request.RelatedEntityKeyDto);
		if (relatedEntity == null)
		{
			return false;
		}

		entity.DeleteRefToTestRelationshipOne(relatedEntity);

		return await SaveChangesAsync(request, entity);
    }
}

#endregion DeleteRefTo

#region DeleteAllRefTo

public record DeleteAllRefTestEntityTwoRelationshipsOneToOneToTestRelationshipOneCommand(TestEntityTwoRelationshipsOneToOneKeyDto EntityKeyDto)
	: RefTestEntityTwoRelationshipsOneToOneToTestRelationshipOneCommand(EntityKeyDto);

internal partial class DeleteAllRefTestEntityTwoRelationshipsOneToOneToTestRelationshipOneCommandHandler
	: RefTestEntityTwoRelationshipsOneToOneToTestRelationshipOneCommandHandlerBase<DeleteAllRefTestEntityTwoRelationshipsOneToOneToTestRelationshipOneCommand>
{
	public DeleteAllRefTestEntityTwoRelationshipsOneToOneToTestRelationshipOneCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution
		)
		: base(dbContext, noxSolution)
	{ }

	protected override async Task<bool> ExecuteAsync(DeleteAllRefTestEntityTwoRelationshipsOneToOneToTestRelationshipOneCommand request)
    {
        var entity = await GetTestEntityTwoRelationshipsOneToOne(request.EntityKeyDto);
		if (entity == null)
		{
			return false;
		}
		entity.DeleteAllRefToTestRelationshipOne();

		return await SaveChangesAsync(request, entity);
    }
}

#endregion DeleteAllRefTo

internal abstract class RefTestEntityTwoRelationshipsOneToOneToTestRelationshipOneCommandHandlerBase<TRequest> : CommandBase<TRequest, TestEntityTwoRelationshipsOneToOneEntity>,
	IRequestHandler <TRequest, bool> where TRequest : RefTestEntityTwoRelationshipsOneToOneToTestRelationshipOneCommand
{
	public AppDbContext DbContext { get; }

	public RefTestEntityTwoRelationshipsOneToOneToTestRelationshipOneCommandHandlerBase(
        AppDbContext dbContext,
		NoxSolution noxSolution)
		: base(noxSolution)
	{
		DbContext = dbContext;
	}

	public virtual async Task<bool> Handle(TRequest request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		await OnExecutingAsync(request);
		return await ExecuteAsync(request);
	}

	protected abstract Task<bool> ExecuteAsync(TRequest request);

	protected async Task<TestEntityTwoRelationshipsOneToOneEntity?> GetTestEntityTwoRelationshipsOneToOne(TestEntityTwoRelationshipsOneToOneKeyDto entityKeyDto)
	{
		var keyId = TestWebApp.Domain.TestEntityTwoRelationshipsOneToOneMetadata.CreateId(entityKeyDto.keyId);
		return await DbContext.TestEntityTwoRelationshipsOneToOnes.FindAsync(keyId);
	}

	protected async Task<TestWebApp.Domain.SecondTestEntityTwoRelationshipsOneToOne?> GetSecondTestEntityTwoRelationshipsOneToOne(SecondTestEntityTwoRelationshipsOneToOneKeyDto relatedEntityKeyDto)
	{
		var relatedKeyId = TestWebApp.Domain.SecondTestEntityTwoRelationshipsOneToOneMetadata.CreateId(relatedEntityKeyDto.keyId);
		return await DbContext.SecondTestEntityTwoRelationshipsOneToOnes.FindAsync(relatedKeyId);
	}

	protected async Task<bool> SaveChangesAsync(TRequest request, TestEntityTwoRelationshipsOneToOneEntity entity)
	{
		await OnCompletedAsync(request, entity);
		DbContext.Entry(entity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();
		if (result < 1)
		{
			return false;
		}
		return true;
	}
}