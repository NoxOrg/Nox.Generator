﻿﻿﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;
using Nox.Application.Commands;
using Nox.Solution;
using Nox.Types;
using Nox.Application.Factories;
using Nox.Extensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using TestWebApp.Infrastructure.Persistence;
using TestWebApp.Domain;
using TestWebApp.Application.Dto;
using SecEntityOwnedRelZeroOrManyEntity = TestWebApp.Domain.SecEntityOwnedRelZeroOrMany;

namespace TestWebApp.Application.Commands;

public partial record UpdateSecEntityOwnedRelZeroOrManiesForTestEntityOwnedRelationshipZeroOrManyCommand(TestEntityOwnedRelationshipZeroOrManyKeyDto ParentKeyDto, SecEntityOwnedRelZeroOrManyUpsertDto EntityDto, Nox.Types.CultureCode CultureCode, System.Guid? Etag) : IRequest <SecEntityOwnedRelZeroOrManyKeyDto?>;

internal partial class UpdateSecEntityOwnedRelZeroOrManiesForTestEntityOwnedRelationshipZeroOrManyCommandHandler : UpdateSecEntityOwnedRelZeroOrManiesForTestEntityOwnedRelationshipZeroOrManyCommandHandlerBase
{
	public UpdateSecEntityOwnedRelZeroOrManiesForTestEntityOwnedRelationshipZeroOrManyCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<SecEntityOwnedRelZeroOrManyEntity, SecEntityOwnedRelZeroOrManyUpsertDto, SecEntityOwnedRelZeroOrManyUpsertDto> entityFactory)
		: base(dbContext, noxSolution, entityFactory)
	{
	}
}

internal partial class UpdateSecEntityOwnedRelZeroOrManiesForTestEntityOwnedRelationshipZeroOrManyCommandHandlerBase : CommandBase<UpdateSecEntityOwnedRelZeroOrManiesForTestEntityOwnedRelationshipZeroOrManyCommand, SecEntityOwnedRelZeroOrManyEntity>, IRequestHandler <UpdateSecEntityOwnedRelZeroOrManiesForTestEntityOwnedRelationshipZeroOrManyCommand, SecEntityOwnedRelZeroOrManyKeyDto?>
{
	private readonly AppDbContext _dbContext;
	private readonly IEntityFactory<SecEntityOwnedRelZeroOrManyEntity, SecEntityOwnedRelZeroOrManyUpsertDto, SecEntityOwnedRelZeroOrManyUpsertDto> _entityFactory;

	protected UpdateSecEntityOwnedRelZeroOrManiesForTestEntityOwnedRelationshipZeroOrManyCommandHandlerBase(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<SecEntityOwnedRelZeroOrManyEntity, SecEntityOwnedRelZeroOrManyUpsertDto, SecEntityOwnedRelZeroOrManyUpsertDto> entityFactory)
		: base(noxSolution)
	{
		_dbContext = dbContext;
		_entityFactory = entityFactory;
	}

	public virtual async Task<SecEntityOwnedRelZeroOrManyKeyDto?> Handle(UpdateSecEntityOwnedRelZeroOrManiesForTestEntityOwnedRelationshipZeroOrManyCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		await OnExecutingAsync(request);
		var keyId = TestWebApp.Domain.TestEntityOwnedRelationshipZeroOrManyMetadata.CreateId(request.ParentKeyDto.keyId);
		var parentEntity = await _dbContext.TestEntityOwnedRelationshipZeroOrManies.FindAsync(keyId);
		if (parentEntity == null)
		{
			return null;
		}
		await _dbContext.Entry(parentEntity).Collection(p => p.SecEntityOwnedRelZeroOrManies).LoadAsync(cancellationToken);
		var ownedId = TestWebApp.Domain.SecEntityOwnedRelZeroOrManyMetadata.CreateId(request.EntityDto.Id.NonNullValue<System.String>());
		var entity = parentEntity.SecEntityOwnedRelZeroOrManies.SingleOrDefault(x => x.Id == ownedId);
		if (entity == null)
		{
			return null;
		}

		await _entityFactory.UpdateEntityAsync(entity, request.EntityDto, request.CultureCode);
		parentEntity.Etag = request.Etag.HasValue ? request.Etag.Value : System.Guid.Empty;
		await OnCompletedAsync(request, entity);

		_dbContext.Entry(parentEntity).State = EntityState.Modified;


		var result = await _dbContext.SaveChangesAsync();
		if (result < 1)
		{
			return null;
		}

		return new SecEntityOwnedRelZeroOrManyKeyDto(entity.Id.Value);
	}
}

public class UpdateSecEntityOwnedRelZeroOrManiesForTestEntityOwnedRelationshipZeroOrManyValidator : AbstractValidator<UpdateSecEntityOwnedRelZeroOrManiesForTestEntityOwnedRelationshipZeroOrManyCommand>
{
    public UpdateSecEntityOwnedRelZeroOrManiesForTestEntityOwnedRelationshipZeroOrManyValidator(ILogger<UpdateSecEntityOwnedRelZeroOrManiesForTestEntityOwnedRelationshipZeroOrManyCommand> logger)
    {
		RuleFor(x => x.EntityDto.Id).NotNull().WithMessage("Id is required.");
    }
}