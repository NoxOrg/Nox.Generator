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
using SecondTestEntityTwoRelationshipsManyToManyEntity = TestWebApp.Domain.SecondTestEntityTwoRelationshipsManyToMany;

namespace TestWebApp.Application.Commands;

public abstract record RefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommand(SecondTestEntityTwoRelationshipsManyToManyKeyDto EntityKeyDto) : IRequest <bool>;

#region CreateRefTo

public partial record CreateRefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommand(SecondTestEntityTwoRelationshipsManyToManyKeyDto EntityKeyDto, TestEntityTwoRelationshipsManyToManyKeyDto RelatedEntityKeyDto)
	: RefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommand(EntityKeyDto);

internal partial class CreateRefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommandHandler
	: RefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommandHandlerBase<CreateRefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommand>
{
	public CreateRefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution
		)
		: base(dbContext, noxSolution)
	{ }

	protected override async Task<bool> ExecuteAsync(CreateRefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommand request)
    {
		var entity = await GetSecondTestEntityTwoRelationshipsManyToMany(request.EntityKeyDto);
		if (entity == null)
		{
			return false;
		}

		var relatedEntity = await GetTestEntityTwoRelationshipsManyToMany(request.RelatedEntityKeyDto);
		if (relatedEntity == null)
		{
			return false;
		}

		entity.CreateRefToTestRelationshipTwoOnOtherSide(relatedEntity);

		return await SaveChangesAsync(request, entity);
    }
}

#endregion CreateRefTo

#region UpdateRefTo

public partial record UpdateRefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommand(SecondTestEntityTwoRelationshipsManyToManyKeyDto EntityKeyDto, List<TestEntityTwoRelationshipsManyToManyKeyDto> RelatedEntitiesKeysDtos)
	: RefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommand(EntityKeyDto);

internal partial class UpdateRefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommandHandler
	: RefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommandHandlerBase<UpdateRefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommand>
{
	public UpdateRefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution
		)
		: base(dbContext, noxSolution)
	{ }

	protected override async Task<bool> ExecuteAsync(UpdateRefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommand request)
    {
		var entity = await GetSecondTestEntityTwoRelationshipsManyToMany(request.EntityKeyDto);
		if (entity == null)
		{
			return false;
		}

		var relatedEntities = new List<TestWebApp.Domain.TestEntityTwoRelationshipsManyToMany>();
		foreach(var keyDto in request.RelatedEntitiesKeysDtos)
		{
			var relatedEntity = await GetTestEntityTwoRelationshipsManyToMany(keyDto);
			if (relatedEntity == null)
			{
				return false;
			}
			relatedEntities.Add(relatedEntity);
		}

		await DbContext.Entry(entity).Collection(x => x.TestRelationshipTwoOnOtherSide).LoadAsync();
		entity.UpdateRefToTestRelationshipTwoOnOtherSide(relatedEntities);

		return await SaveChangesAsync(request, entity);
    }
}

#endregion UpdateRefTo

#region DeleteRefTo

public record DeleteRefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommand(SecondTestEntityTwoRelationshipsManyToManyKeyDto EntityKeyDto, TestEntityTwoRelationshipsManyToManyKeyDto RelatedEntityKeyDto)
	: RefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommand(EntityKeyDto);

internal partial class DeleteRefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommandHandler
	: RefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommandHandlerBase<DeleteRefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommand>
{
	public DeleteRefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution
		)
		: base(dbContext, noxSolution)
	{ }

	protected override async Task<bool> ExecuteAsync(DeleteRefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommand request)
    {
        var entity = await GetSecondTestEntityTwoRelationshipsManyToMany(request.EntityKeyDto);
		if (entity == null)
		{
			return false;
		}

		var relatedEntity = await GetTestEntityTwoRelationshipsManyToMany(request.RelatedEntityKeyDto);
		if (relatedEntity == null)
		{
			return false;
		}

		entity.DeleteRefToTestRelationshipTwoOnOtherSide(relatedEntity);

		return await SaveChangesAsync(request, entity);
    }
}

#endregion DeleteRefTo

#region DeleteAllRefTo

public record DeleteAllRefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommand(SecondTestEntityTwoRelationshipsManyToManyKeyDto EntityKeyDto)
	: RefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommand(EntityKeyDto);

internal partial class DeleteAllRefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommandHandler
	: RefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommandHandlerBase<DeleteAllRefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommand>
{
	public DeleteAllRefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution
		)
		: base(dbContext, noxSolution)
	{ }

	protected override async Task<bool> ExecuteAsync(DeleteAllRefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommand request)
    {
        var entity = await GetSecondTestEntityTwoRelationshipsManyToMany(request.EntityKeyDto);
		if (entity == null)
		{
			return false;
		}
		await DbContext.Entry(entity).Collection(x => x.TestRelationshipTwoOnOtherSide).LoadAsync();
		entity.DeleteAllRefToTestRelationshipTwoOnOtherSide();

		return await SaveChangesAsync(request, entity);
    }
}

#endregion DeleteAllRefTo

internal abstract class RefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommandHandlerBase<TRequest> : CommandBase<TRequest, SecondTestEntityTwoRelationshipsManyToManyEntity>,
	IRequestHandler <TRequest, bool> where TRequest : RefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommand
{
	public AppDbContext DbContext { get; }

	public RefSecondTestEntityTwoRelationshipsManyToManyToTestRelationshipTwoOnOtherSideCommandHandlerBase(
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

	protected async Task<SecondTestEntityTwoRelationshipsManyToManyEntity?> GetSecondTestEntityTwoRelationshipsManyToMany(SecondTestEntityTwoRelationshipsManyToManyKeyDto entityKeyDto)
	{
		var keyId = TestWebApp.Domain.SecondTestEntityTwoRelationshipsManyToManyMetadata.CreateId(entityKeyDto.keyId);
		return await DbContext.SecondTestEntityTwoRelationshipsManyToManies.FindAsync(keyId);
	}

	protected async Task<TestWebApp.Domain.TestEntityTwoRelationshipsManyToMany?> GetTestEntityTwoRelationshipsManyToMany(TestEntityTwoRelationshipsManyToManyKeyDto relatedEntityKeyDto)
	{
		var relatedKeyId = TestWebApp.Domain.TestEntityTwoRelationshipsManyToManyMetadata.CreateId(relatedEntityKeyDto.keyId);
		return await DbContext.TestEntityTwoRelationshipsManyToManies.FindAsync(relatedKeyId);
	}

	protected async Task<bool> SaveChangesAsync(TRequest request, SecondTestEntityTwoRelationshipsManyToManyEntity entity)
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