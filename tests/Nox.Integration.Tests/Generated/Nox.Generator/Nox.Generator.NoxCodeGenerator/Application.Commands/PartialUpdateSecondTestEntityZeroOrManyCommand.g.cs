﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;
using Nox.Application.Commands;
using Nox.Application.Factories;
using Nox.Solution;
using Nox.Types;
using Nox.Exceptions;

using TestWebApp.Infrastructure.Persistence;
using TestWebApp.Domain;
using TestWebApp.Application.Dto;
using SecondTestEntityZeroOrManyEntity = TestWebApp.Domain.SecondTestEntityZeroOrMany;

namespace TestWebApp.Application.Commands;

public partial record PartialUpdateSecondTestEntityZeroOrManyCommand(System.String keyId, Dictionary<string, dynamic> UpdatedProperties, Nox.Types.CultureCode CultureCode, System.Guid? Etag) : IRequest <SecondTestEntityZeroOrManyKeyDto>;

internal partial class PartialUpdateSecondTestEntityZeroOrManyCommandHandler : PartialUpdateSecondTestEntityZeroOrManyCommandHandlerBase
{
	public PartialUpdateSecondTestEntityZeroOrManyCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<SecondTestEntityZeroOrManyEntity, SecondTestEntityZeroOrManyCreateDto, SecondTestEntityZeroOrManyUpdateDto> entityFactory)
		: base(dbContext,noxSolution, entityFactory)
	{
	}
}
internal abstract class PartialUpdateSecondTestEntityZeroOrManyCommandHandlerBase : CommandBase<PartialUpdateSecondTestEntityZeroOrManyCommand, SecondTestEntityZeroOrManyEntity>, IRequestHandler<PartialUpdateSecondTestEntityZeroOrManyCommand, SecondTestEntityZeroOrManyKeyDto>
{
	public AppDbContext DbContext { get; }
	public IEntityFactory<SecondTestEntityZeroOrManyEntity, SecondTestEntityZeroOrManyCreateDto, SecondTestEntityZeroOrManyUpdateDto> EntityFactory { get; }

	public PartialUpdateSecondTestEntityZeroOrManyCommandHandlerBase(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<SecondTestEntityZeroOrManyEntity, SecondTestEntityZeroOrManyCreateDto, SecondTestEntityZeroOrManyUpdateDto> entityFactory)
		: base(noxSolution)
	{
		DbContext = dbContext;
		EntityFactory = entityFactory;
	}

	public virtual async Task<SecondTestEntityZeroOrManyKeyDto> Handle(PartialUpdateSecondTestEntityZeroOrManyCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		await OnExecutingAsync(request);
		var keyId = TestWebApp.Domain.SecondTestEntityZeroOrManyMetadata.CreateId(request.keyId);

		var entity = await DbContext.SecondTestEntityZeroOrManies.FindAsync(keyId);
		if (entity == null)
		{
			throw new EntityNotFoundException("SecondTestEntityZeroOrMany",  $"{keyId.ToString()}");
		}
		await EntityFactory.PartialUpdateEntityAsync(entity, request.UpdatedProperties, request.CultureCode);
		entity.Etag = request.Etag.HasValue ? request.Etag.Value : System.Guid.Empty;

		await OnCompletedAsync(request, entity);

		DbContext.Entry(entity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();
		return new SecondTestEntityZeroOrManyKeyDto(entity.Id.Value);
	}
}