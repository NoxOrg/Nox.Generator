﻿﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;
using Nox.Application.Commands;
using Nox.Solution;
using Nox.Types;
using Nox.Application.Factories;
using Nox.Exceptions;
using Nox.Extensions;
using TestWebApp.Infrastructure.Persistence;
using TestWebApp.Domain;
using TestWebApp.Application.Dto;
using ThirdTestEntityOneOrManyEntity = TestWebApp.Domain.ThirdTestEntityOneOrMany;

namespace TestWebApp.Application.Commands;

public record UpdateThirdTestEntityOneOrManyCommand(System.String keyId, ThirdTestEntityOneOrManyUpdateDto EntityDto, System.Guid? Etag) : IRequest<ThirdTestEntityOneOrManyKeyDto?>;

internal partial class UpdateThirdTestEntityOneOrManyCommandHandler : UpdateThirdTestEntityOneOrManyCommandHandlerBase
{
	public UpdateThirdTestEntityOneOrManyCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<ThirdTestEntityOneOrManyEntity, ThirdTestEntityOneOrManyCreateDto, ThirdTestEntityOneOrManyUpdateDto> entityFactory) : base(dbContext, noxSolution, entityFactory)
	{
	}
}

internal abstract class UpdateThirdTestEntityOneOrManyCommandHandlerBase : CommandBase<UpdateThirdTestEntityOneOrManyCommand, ThirdTestEntityOneOrManyEntity>, IRequestHandler<UpdateThirdTestEntityOneOrManyCommand, ThirdTestEntityOneOrManyKeyDto?>
{
	public AppDbContext DbContext { get; }
	private readonly IEntityFactory<ThirdTestEntityOneOrManyEntity, ThirdTestEntityOneOrManyCreateDto, ThirdTestEntityOneOrManyUpdateDto> _entityFactory;

	public UpdateThirdTestEntityOneOrManyCommandHandlerBase(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<ThirdTestEntityOneOrManyEntity, ThirdTestEntityOneOrManyCreateDto, ThirdTestEntityOneOrManyUpdateDto> entityFactory) : base(noxSolution)
	{
		DbContext = dbContext;
		_entityFactory = entityFactory;
	}

	public virtual async Task<ThirdTestEntityOneOrManyKeyDto?> Handle(UpdateThirdTestEntityOneOrManyCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);
		var keyId = TestWebApp.Domain.ThirdTestEntityOneOrManyMetadata.CreateId(request.keyId);

		var entity = await DbContext.ThirdTestEntityOneOrManies.FindAsync(keyId);
		if (entity == null)
		{
			return null;
		}

		await DbContext.Entry(entity).Collection(x => x.ThirdTestEntityZeroOrManyRelationship).LoadAsync();
		var thirdTestEntityZeroOrManyRelationshipEntities = new List<ThirdTestEntityZeroOrMany>();
		foreach(var relatedEntityId in request.EntityDto.ThirdTestEntityZeroOrManyRelationshipId)
		{
			var relatedKey = TestWebApp.Domain.ThirdTestEntityZeroOrManyMetadata.CreateId(relatedEntityId);
			var relatedEntity = await DbContext.ThirdTestEntityZeroOrManies.FindAsync(relatedKey);
						
			if(relatedEntity is not null)
				thirdTestEntityZeroOrManyRelationshipEntities.Add(relatedEntity);
			else
				throw new RelatedEntityNotFoundException("ThirdTestEntityZeroOrManyRelationship", relatedEntityId.ToString());
		}
		entity.UpdateAllRefToThirdTestEntityZeroOrManyRelationship(thirdTestEntityZeroOrManyRelationshipEntities);

		_entityFactory.UpdateEntity(entity, request.EntityDto);
		entity.Etag = request.Etag.HasValue ? request.Etag.Value : System.Guid.Empty;

		await OnCompletedAsync(request, entity);

		DbContext.Entry(entity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();
		if (result < 1)
		{
			return null;
		}

		return new ThirdTestEntityOneOrManyKeyDto(entity.Id.Value);
	}
}