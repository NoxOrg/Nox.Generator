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
using TestEntityZeroOrOneToOneOrMany = TestWebApp.Domain.TestEntityZeroOrOneToOneOrMany;

namespace TestWebApp.Application.Commands;

public record UpdateTestEntityZeroOrOneToOneOrManyCommand(System.String keyId, TestEntityZeroOrOneToOneOrManyUpdateDto EntityDto, System.Guid? Etag) : IRequest<TestEntityZeroOrOneToOneOrManyKeyDto?>;

internal partial class UpdateTestEntityZeroOrOneToOneOrManyCommandHandler: UpdateTestEntityZeroOrOneToOneOrManyCommandHandlerBase
{
	public UpdateTestEntityZeroOrOneToOneOrManyCommandHandler(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider,
		IEntityFactory<TestEntityZeroOrOneToOneOrMany, TestEntityZeroOrOneToOneOrManyCreateDto, TestEntityZeroOrOneToOneOrManyUpdateDto> entityFactory): base(dbContext, noxSolution, serviceProvider, entityFactory)
	{
	}
}

internal abstract class UpdateTestEntityZeroOrOneToOneOrManyCommandHandlerBase: CommandBase<UpdateTestEntityZeroOrOneToOneOrManyCommand, TestEntityZeroOrOneToOneOrMany>, IRequestHandler<UpdateTestEntityZeroOrOneToOneOrManyCommand, TestEntityZeroOrOneToOneOrManyKeyDto?>
{
	public TestWebAppDbContext DbContext { get; }
	private readonly IEntityFactory<TestEntityZeroOrOneToOneOrMany, TestEntityZeroOrOneToOneOrManyCreateDto, TestEntityZeroOrOneToOneOrManyUpdateDto> _entityFactory;

	public UpdateTestEntityZeroOrOneToOneOrManyCommandHandlerBase(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider,
		IEntityFactory<TestEntityZeroOrOneToOneOrMany, TestEntityZeroOrOneToOneOrManyCreateDto, TestEntityZeroOrOneToOneOrManyUpdateDto> entityFactory): base(noxSolution, serviceProvider)
	{
		DbContext = dbContext;
		_entityFactory = entityFactory;
	}

	public virtual async Task<TestEntityZeroOrOneToOneOrManyKeyDto?> Handle(UpdateTestEntityZeroOrOneToOneOrManyCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);
		var keyId = CreateNoxTypeForKey<TestEntityZeroOrOneToOneOrMany,Nox.Types.Text>("Id", request.keyId);

		var entity = await DbContext.TestEntityZeroOrOneToOneOrManies.FindAsync(keyId);
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

		return new TestEntityZeroOrOneToOneOrManyKeyDto(entity.Id.Value);
	}
}