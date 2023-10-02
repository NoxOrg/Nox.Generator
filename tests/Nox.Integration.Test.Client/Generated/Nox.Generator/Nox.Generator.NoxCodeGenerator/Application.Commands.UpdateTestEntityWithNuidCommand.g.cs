﻿﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;
using Nox.Application.Commands;
using Nox.Solution;
using Nox.Types;
using Nox.Factories;
using TestWebApp.Infrastructure.Persistence;
using TestWebApp.Domain;
using TestWebApp.Application.Dto;
using TestEntityWithNuid = TestWebApp.Domain.TestEntityWithNuid;

namespace TestWebApp.Application.Commands;

public record UpdateTestEntityWithNuidCommand(System.UInt32 keyId, TestEntityWithNuidUpdateDto EntityDto, System.Guid? Etag) : IRequest<TestEntityWithNuidKeyDto?>;

internal partial class UpdateTestEntityWithNuidCommandHandler: UpdateTestEntityWithNuidCommandHandlerBase
{
	public UpdateTestEntityWithNuidCommandHandler(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider,
		IEntityFactory<TestEntityWithNuid, TestEntityWithNuidCreateDto, TestEntityWithNuidUpdateDto> entityFactory): base(dbContext, noxSolution, serviceProvider, entityFactory)
	{
	}
}

internal abstract class UpdateTestEntityWithNuidCommandHandlerBase: CommandBase<UpdateTestEntityWithNuidCommand, TestEntityWithNuid>, IRequestHandler<UpdateTestEntityWithNuidCommand, TestEntityWithNuidKeyDto?>
{
	public TestWebAppDbContext DbContext { get; }
	private readonly IEntityFactory<TestEntityWithNuid, TestEntityWithNuidCreateDto, TestEntityWithNuidUpdateDto> _entityFactory;

	public UpdateTestEntityWithNuidCommandHandlerBase(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider,
		IEntityFactory<TestEntityWithNuid, TestEntityWithNuidCreateDto, TestEntityWithNuidUpdateDto> entityFactory): base(noxSolution, serviceProvider)
	{
		DbContext = dbContext;
		_entityFactory = entityFactory;
	}

	public virtual async Task<TestEntityWithNuidKeyDto?> Handle(UpdateTestEntityWithNuidCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);
		var keyId = CreateNoxTypeForKey<TestEntityWithNuid,Nox.Types.Nuid>("Id", request.keyId);

		var entity = await DbContext.TestEntityWithNuids.FindAsync(keyId);
		if (entity == null)
		{
			return null;
		}

		_entityFactory.UpdateEntity(entity, request.EntityDto);
		entity.Etag = request.Etag.HasValue ? request.Etag.Value : System.Guid.Empty;

		OnCompleted(request, entity);

		DbContext.Entry(entity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();
		if (result < 1)
		{
			return null;
		}

		return new TestEntityWithNuidKeyDto(entity.Id.Value);
	}
}