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
using TestEntityOneOrManyToZeroOrOne = TestWebApp.Domain.TestEntityOneOrManyToZeroOrOne;

namespace TestWebApp.Application.Commands;

public record UpdateTestEntityOneOrManyToZeroOrOneCommand(System.String keyId, TestEntityOneOrManyToZeroOrOneUpdateDto EntityDto, System.Guid? Etag) : IRequest<TestEntityOneOrManyToZeroOrOneKeyDto?>;

internal partial class UpdateTestEntityOneOrManyToZeroOrOneCommandHandler: UpdateTestEntityOneOrManyToZeroOrOneCommandHandlerBase
{
	public UpdateTestEntityOneOrManyToZeroOrOneCommandHandler(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider,
		IEntityFactory<TestEntityOneOrManyToZeroOrOne, TestEntityOneOrManyToZeroOrOneCreateDto, TestEntityOneOrManyToZeroOrOneUpdateDto> entityFactory): base(dbContext, noxSolution, serviceProvider, entityFactory)
	{
	}
}

internal abstract class UpdateTestEntityOneOrManyToZeroOrOneCommandHandlerBase: CommandBase<UpdateTestEntityOneOrManyToZeroOrOneCommand, TestEntityOneOrManyToZeroOrOne>, IRequestHandler<UpdateTestEntityOneOrManyToZeroOrOneCommand, TestEntityOneOrManyToZeroOrOneKeyDto?>
{
	public TestWebAppDbContext DbContext { get; }
	private readonly IEntityFactory<TestEntityOneOrManyToZeroOrOne, TestEntityOneOrManyToZeroOrOneCreateDto, TestEntityOneOrManyToZeroOrOneUpdateDto> _entityFactory;

	public UpdateTestEntityOneOrManyToZeroOrOneCommandHandlerBase(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider,
		IEntityFactory<TestEntityOneOrManyToZeroOrOne, TestEntityOneOrManyToZeroOrOneCreateDto, TestEntityOneOrManyToZeroOrOneUpdateDto> entityFactory): base(noxSolution, serviceProvider)
	{
		DbContext = dbContext;
		_entityFactory = entityFactory;
	}

	public virtual async Task<TestEntityOneOrManyToZeroOrOneKeyDto?> Handle(UpdateTestEntityOneOrManyToZeroOrOneCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);
		var keyId = CreateNoxTypeForKey<TestEntityOneOrManyToZeroOrOne,Nox.Types.Text>("Id", request.keyId);

		var entity = await DbContext.TestEntityOneOrManyToZeroOrOnes.FindAsync(keyId);
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

		return new TestEntityOneOrManyToZeroOrOneKeyDto(entity.Id.Value);
	}
}