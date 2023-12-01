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
using SecondTestEntityExactlyOneEntity = TestWebApp.Domain.SecondTestEntityExactlyOne;

namespace TestWebApp.Application.Commands;

public partial record DeleteSecondTestEntityExactlyOneByIdCommand(IEnumerable<SecondTestEntityExactlyOneKeyDto> KeyDtos, System.Guid? Etag) : IRequest<bool>;

internal class DeleteSecondTestEntityExactlyOneByIdCommandHandler : DeleteSecondTestEntityExactlyOneByIdCommandHandlerBase
{
	public DeleteSecondTestEntityExactlyOneByIdCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution) : base(dbContext, noxSolution)
	{
	}
}
internal abstract class DeleteSecondTestEntityExactlyOneByIdCommandHandlerBase : CommandBase<DeleteSecondTestEntityExactlyOneByIdCommand, SecondTestEntityExactlyOneEntity>, IRequestHandler<DeleteSecondTestEntityExactlyOneByIdCommand, bool>
{
	public AppDbContext DbContext { get; }

	public DeleteSecondTestEntityExactlyOneByIdCommandHandlerBase(
        AppDbContext dbContext,
		NoxSolution noxSolution) : base(noxSolution)
	{
		DbContext = dbContext;
	}

	public virtual async Task<bool> Handle(DeleteSecondTestEntityExactlyOneByIdCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		await OnExecutingAsync(request);
		
		foreach(var keyDto in request.KeyDtos)
		{
			var keyId = TestWebApp.Domain.SecondTestEntityExactlyOneMetadata.CreateId(keyDto.keyId);		

			var entity = await DbContext.SecondTestEntityExactlyOnes.FindAsync(keyId);
			if (entity == null || entity.IsDeleted == true)
			{
				return false;
			}

			entity.Etag = request.Etag.HasValue ? request.Etag.Value : System.Guid.Empty;
			DbContext.Entry(entity).State = EntityState.Deleted;
		}

		await OnCompletedAsync(request, new SecondTestEntityExactlyOneEntity());

		await DbContext.SaveChangesAsync(cancellationToken);
		return true;
	}
}