﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;
using Nox.Application.Commands;
using Nox.Solution;
using Nox.Types;
using TestWebApp.Infrastructure.Persistence;
using TestWebApp.Domain;
using TestWebApp.Application.Dto;
using TestEntityZeroOrManyEntity = TestWebApp.Domain.TestEntityZeroOrMany;

namespace TestWebApp.Application.Commands;

public partial record DeleteTestEntityZeroOrManyByIdCommand(IEnumerable<TestEntityZeroOrManyKeyDto> KeyDtos, System.Guid? Etag) : IRequest<bool>;

internal class DeleteTestEntityZeroOrManyByIdCommandHandler : DeleteTestEntityZeroOrManyByIdCommandHandlerBase
{
	public DeleteTestEntityZeroOrManyByIdCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution) : base(dbContext, noxSolution)
	{
	}
}
internal abstract class DeleteTestEntityZeroOrManyByIdCommandHandlerBase : CommandBase<DeleteTestEntityZeroOrManyByIdCommand, TestEntityZeroOrManyEntity>, IRequestHandler<DeleteTestEntityZeroOrManyByIdCommand, bool>
{
	public AppDbContext DbContext { get; }

	public DeleteTestEntityZeroOrManyByIdCommandHandlerBase(
        AppDbContext dbContext,
		NoxSolution noxSolution) : base(noxSolution)
	{
		DbContext = dbContext;
	}

	public virtual async Task<bool> Handle(DeleteTestEntityZeroOrManyByIdCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		await OnExecutingAsync(request);
		
		foreach(var keyDto in request.KeyDtos)
		{
			var keyId = TestWebApp.Domain.TestEntityZeroOrManyMetadata.CreateId(keyDto.keyId);		

			var entity = await DbContext.TestEntityZeroOrManies.FindAsync(keyId);
			if (entity == null || entity.IsDeleted == true)
			{
				return false;
			}

			entity.Etag = request.Etag.HasValue ? request.Etag.Value : System.Guid.Empty;
			DbContext.Entry(entity).State = EntityState.Deleted;
		}

		await OnCompletedAsync(request, new TestEntityZeroOrManyEntity());

		await DbContext.SaveChangesAsync(cancellationToken);
		return true;
	}
}