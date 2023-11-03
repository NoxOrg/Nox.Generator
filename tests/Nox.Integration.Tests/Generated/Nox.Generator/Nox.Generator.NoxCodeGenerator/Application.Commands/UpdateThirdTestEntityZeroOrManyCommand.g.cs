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
using ThirdTestEntityZeroOrManyEntity = TestWebApp.Domain.ThirdTestEntityZeroOrMany;

namespace TestWebApp.Application.Commands;

public record UpdateThirdTestEntityZeroOrManyCommand(System.String keyId, ThirdTestEntityZeroOrManyUpdateDto EntityDto, System.Guid? Etag) : IRequest<ThirdTestEntityZeroOrManyKeyDto?>;

internal partial class UpdateThirdTestEntityZeroOrManyCommandHandler : UpdateThirdTestEntityZeroOrManyCommandHandlerBase
{
	public UpdateThirdTestEntityZeroOrManyCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<ThirdTestEntityZeroOrManyEntity, ThirdTestEntityZeroOrManyCreateDto, ThirdTestEntityZeroOrManyUpdateDto> entityFactory) : base(dbContext, noxSolution, entityFactory)
	{
	}
}

internal abstract class UpdateThirdTestEntityZeroOrManyCommandHandlerBase : CommandBase<UpdateThirdTestEntityZeroOrManyCommand, ThirdTestEntityZeroOrManyEntity>, IRequestHandler<UpdateThirdTestEntityZeroOrManyCommand, ThirdTestEntityZeroOrManyKeyDto?>
{
	public AppDbContext DbContext { get; }
	private readonly IEntityFactory<ThirdTestEntityZeroOrManyEntity, ThirdTestEntityZeroOrManyCreateDto, ThirdTestEntityZeroOrManyUpdateDto> _entityFactory;

	public UpdateThirdTestEntityZeroOrManyCommandHandlerBase(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<ThirdTestEntityZeroOrManyEntity, ThirdTestEntityZeroOrManyCreateDto, ThirdTestEntityZeroOrManyUpdateDto> entityFactory) : base(noxSolution)
	{
		DbContext = dbContext;
		_entityFactory = entityFactory;
	}

	public virtual async Task<ThirdTestEntityZeroOrManyKeyDto?> Handle(UpdateThirdTestEntityZeroOrManyCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		await OnExecutingAsync(request);
		var keyId = TestWebApp.Domain.ThirdTestEntityZeroOrManyMetadata.CreateId(request.keyId);

		var entity = await DbContext.ThirdTestEntityZeroOrManies.FindAsync(keyId);
		if (entity == null)
		{
			return null;
		}

		await DbContext.Entry(entity).Collection(x => x.ThirdTestEntityOneOrManies).LoadAsync();
		var thirdTestEntityOneOrManiesEntities = new List<ThirdTestEntityOneOrMany>();
		foreach(var relatedEntityId in request.EntityDto.ThirdTestEntityOneOrManiesId)
		{
			var relatedKey = TestWebApp.Domain.ThirdTestEntityOneOrManyMetadata.CreateId(relatedEntityId);
			var relatedEntity = await DbContext.ThirdTestEntityOneOrManies.FindAsync(relatedKey);
						
			if(relatedEntity is not null)
				thirdTestEntityOneOrManiesEntities.Add(relatedEntity);
			else
				throw new RelatedEntityNotFoundException("ThirdTestEntityOneOrManies", relatedEntityId.ToString());
		}
		entity.UpdateRefToThirdTestEntityOneOrManies(thirdTestEntityOneOrManiesEntities);

		_entityFactory.UpdateEntity(entity, request.EntityDto);
		entity.Etag = request.Etag.HasValue ? request.Etag.Value : System.Guid.Empty;

		await OnCompletedAsync(request, entity);

		DbContext.Entry(entity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();
		if (result < 1)
		{
			return null;
		}

		return new ThirdTestEntityZeroOrManyKeyDto(entity.Id.Value);
	}
}