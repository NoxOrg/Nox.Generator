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
using TestEntityOneOrManyToExactlyOneEntity = TestWebApp.Domain.TestEntityOneOrManyToExactlyOne;

namespace TestWebApp.Application.Commands;

public abstract record RefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommand(TestEntityOneOrManyToExactlyOneKeyDto EntityKeyDto) : IRequest <bool>;

#region CreateRefTo

public partial record CreateRefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommand(TestEntityOneOrManyToExactlyOneKeyDto EntityKeyDto, TestEntityExactlyOneToOneOrManyKeyDto RelatedEntityKeyDto)
	: RefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommand(EntityKeyDto);

internal partial class CreateRefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommandHandler
	: RefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommandHandlerBase<CreateRefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommand>
{
	public CreateRefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution
		)
		: base(dbContext, noxSolution)
	{ }

	protected override async Task<bool> ExecuteAsync(CreateRefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommand request)
    {
		var entity = await GetTestEntityOneOrManyToExactlyOne(request.EntityKeyDto);
		if (entity == null)
		{
			return false;
		}

		var relatedEntity = await GetTestEntityExactlyOneToOneOrMany(request.RelatedEntityKeyDto);
		if (relatedEntity == null)
		{
			return false;
		}

		entity.CreateRefToTestEntityExactlyOneToOneOrManies(relatedEntity);

		return await SaveChangesAsync(request, entity);
    }
}

#endregion CreateRefTo

#region UpdateRefTo

public partial record UpdateRefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommand(TestEntityOneOrManyToExactlyOneKeyDto EntityKeyDto, List<TestEntityExactlyOneToOneOrManyKeyDto> RelatedEntityKeyDto)
	: RefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommand(EntityKeyDto);

internal partial class UpdateRefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommandHandler
	: RefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommandHandlerBase<UpdateRefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommand>
{
	public UpdateRefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution
		)
		: base(dbContext, noxSolution)
	{ }

	protected override async Task<bool> ExecuteAsync(UpdateRefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommand request)
    {
		var entity = await GetTestEntityOneOrManyToExactlyOne(request.EntityKeyDto);
		if (entity == null)
		{
			return false;
		}

		var relatedEntities = new List<TestWebApp.Domain.TestEntityExactlyOneToOneOrMany>();
		foreach(var keyDto in request.RelatedEntityKeyDto)
		{
			var relatedEntity = await GetTestEntityExactlyOneToOneOrMany(keyDto);
			if (relatedEntity == null)
			{
				return false;
			}
			relatedEntities.Add(relatedEntity);
		}

		await DbContext.Entry(entity).Collection(x => x.TestEntityExactlyOneToOneOrManies).LoadAsync();
		entity.UpdateRefToTestEntityExactlyOneToOneOrManies(relatedEntities);

		return await SaveChangesAsync(request, entity);
    }
}

#endregion UpdateRefTo

#region DeleteRefTo

public record DeleteRefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommand(TestEntityOneOrManyToExactlyOneKeyDto EntityKeyDto, TestEntityExactlyOneToOneOrManyKeyDto RelatedEntityKeyDto)
	: RefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommand(EntityKeyDto);

internal partial class DeleteRefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommandHandler
	: RefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommandHandlerBase<DeleteRefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommand>
{
	public DeleteRefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution
		)
		: base(dbContext, noxSolution)
	{ }

	protected override async Task<bool> ExecuteAsync(DeleteRefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommand request)
    {
        var entity = await GetTestEntityOneOrManyToExactlyOne(request.EntityKeyDto);
		if (entity == null)
		{
			return false;
		}

		var relatedEntity = await GetTestEntityExactlyOneToOneOrMany(request.RelatedEntityKeyDto);
		if (relatedEntity == null)
		{
			return false;
		}

		entity.DeleteRefToTestEntityExactlyOneToOneOrManies(relatedEntity);

		return await SaveChangesAsync(request, entity);
    }
}

#endregion DeleteRefTo

#region DeleteAllRefTo

public record DeleteAllRefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommand(TestEntityOneOrManyToExactlyOneKeyDto EntityKeyDto)
	: RefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommand(EntityKeyDto);

internal partial class DeleteAllRefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommandHandler
	: RefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommandHandlerBase<DeleteAllRefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommand>
{
	public DeleteAllRefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution
		)
		: base(dbContext, noxSolution)
	{ }

	protected override async Task<bool> ExecuteAsync(DeleteAllRefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommand request)
    {
        var entity = await GetTestEntityOneOrManyToExactlyOne(request.EntityKeyDto);
		if (entity == null)
		{
			return false;
		}
		await DbContext.Entry(entity).Collection(x => x.TestEntityExactlyOneToOneOrManies).LoadAsync();
		entity.DeleteAllRefToTestEntityExactlyOneToOneOrManies();

		return await SaveChangesAsync(request, entity);
    }
}

#endregion DeleteAllRefTo

internal abstract class RefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommandHandlerBase<TRequest> : CommandBase<TRequest, TestEntityOneOrManyToExactlyOneEntity>,
	IRequestHandler <TRequest, bool> where TRequest : RefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommand
{
	public AppDbContext DbContext { get; }

	public RefTestEntityOneOrManyToExactlyOneToTestEntityExactlyOneToOneOrManiesCommandHandlerBase(
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

	protected async Task<TestEntityOneOrManyToExactlyOneEntity?> GetTestEntityOneOrManyToExactlyOne(TestEntityOneOrManyToExactlyOneKeyDto entityKeyDto)
	{
		var keyId = TestWebApp.Domain.TestEntityOneOrManyToExactlyOneMetadata.CreateId(entityKeyDto.keyId);
		return await DbContext.TestEntityOneOrManyToExactlyOnes.FindAsync(keyId);
	}

	protected async Task<TestWebApp.Domain.TestEntityExactlyOneToOneOrMany?> GetTestEntityExactlyOneToOneOrMany(TestEntityExactlyOneToOneOrManyKeyDto relatedEntityKeyDto)
	{
		var relatedKeyId = TestWebApp.Domain.TestEntityExactlyOneToOneOrManyMetadata.CreateId(relatedEntityKeyDto.keyId);
		return await DbContext.TestEntityExactlyOneToOneOrManies.FindAsync(relatedKeyId);
	}

	protected async Task<bool> SaveChangesAsync(TRequest request, TestEntityOneOrManyToExactlyOneEntity entity)
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