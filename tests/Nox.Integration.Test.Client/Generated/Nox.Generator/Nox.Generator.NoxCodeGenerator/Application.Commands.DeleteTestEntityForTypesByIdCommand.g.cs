﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;
using Nox.Application.Commands;
using Nox.Solution;
using Nox.Types;
using TestWebApp.Infrastructure.Persistence;
using TestWebApp.Domain;
using TestEntityForTypes = TestWebApp.Domain.TestEntityForTypes;

namespace TestWebApp.Application.Commands;

public record DeleteTestEntityForTypesByIdCommand(System.String keyId, System.Guid? Etag) : IRequest<bool>;

internal class DeleteTestEntityForTypesByIdCommandHandler:DeleteTestEntityForTypesByIdCommandHandlerBase
{
	public DeleteTestEntityForTypesByIdCommandHandler(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider): base(dbContext, noxSolution, serviceProvider)
	{
	}
}
internal abstract class DeleteTestEntityForTypesByIdCommandHandlerBase: CommandBase<DeleteTestEntityForTypesByIdCommand,TestEntityForTypes>, IRequestHandler<DeleteTestEntityForTypesByIdCommand, bool>
{
	public TestWebAppDbContext DbContext { get; }

	public DeleteTestEntityForTypesByIdCommandHandlerBase(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider): base(noxSolution, serviceProvider)
	{
		DbContext = dbContext;
	}

	public virtual async Task<bool> Handle(DeleteTestEntityForTypesByIdCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);
		var keyId = CreateNoxTypeForKey<TestEntityForTypes,Nox.Types.Text>("Id", request.keyId);

		var entity = await DbContext.TestEntityForTypes.FindAsync(keyId);
		if (entity == null || entity.IsDeleted == true)
		{
			return false;
		}

		entity.Etag = request.Etag.HasValue ? request.Etag.Value : System.Guid.Empty;

		OnCompleted(request, entity);
		DbContext.Entry(entity).State = EntityState.Deleted;
		await DbContext.SaveChangesAsync(cancellationToken);
		return true;
	}
}