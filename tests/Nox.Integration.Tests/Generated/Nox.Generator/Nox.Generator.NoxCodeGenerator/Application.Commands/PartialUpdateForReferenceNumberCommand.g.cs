﻿﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;
using Nox.Application.Commands;
using Nox.Application.Factories;
using Nox.Solution;
using Nox.Types;

using TestWebApp.Infrastructure.Persistence;
using TestWebApp.Domain;
using TestWebApp.Application.Dto;
using ForReferenceNumberEntity = TestWebApp.Domain.ForReferenceNumber;

namespace TestWebApp.Application.Commands;

public record PartialUpdateForReferenceNumberCommand(System.String keyId, Dictionary<string, dynamic> UpdatedProperties, Nox.Types.CultureCode CultureCode, System.Guid? Etag) : IRequest <ForReferenceNumberKeyDto?>;

internal class PartialUpdateForReferenceNumberCommandHandler : PartialUpdateForReferenceNumberCommandHandlerBase
{
	public PartialUpdateForReferenceNumberCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<ForReferenceNumberEntity, ForReferenceNumberCreateDto, ForReferenceNumberUpdateDto> entityFactory)
		: base(dbContext,noxSolution, entityFactory)
	{
	}
}
internal class PartialUpdateForReferenceNumberCommandHandlerBase : CommandBase<PartialUpdateForReferenceNumberCommand, ForReferenceNumberEntity>, IRequestHandler<PartialUpdateForReferenceNumberCommand, ForReferenceNumberKeyDto?>
{
	public AppDbContext DbContext { get; }
	public IEntityFactory<ForReferenceNumberEntity, ForReferenceNumberCreateDto, ForReferenceNumberUpdateDto> EntityFactory { get; }

	public PartialUpdateForReferenceNumberCommandHandlerBase(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<ForReferenceNumberEntity, ForReferenceNumberCreateDto, ForReferenceNumberUpdateDto> entityFactory)
		: base(noxSolution)
	{
		DbContext = dbContext;
		EntityFactory = entityFactory;
	}

	public virtual async Task<ForReferenceNumberKeyDto?> Handle(PartialUpdateForReferenceNumberCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		await OnExecutingAsync(request);
		var keyId = TestWebApp.Domain.ForReferenceNumberMetadata.CreateId(request.keyId);

		var entity = await DbContext.ForReferenceNumbers.FindAsync(keyId);
		if (entity == null)
		{
			return null;
		}
		EntityFactory.PartialUpdateEntity(entity, request.UpdatedProperties, request.CultureCode);
		entity.Etag = request.Etag.HasValue ? request.Etag.Value : System.Guid.Empty;

		await OnCompletedAsync(request, entity);

		DbContext.Entry(entity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();
		return new ForReferenceNumberKeyDto(entity.Id.Value);
	}
}