﻿﻿﻿
// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;
using Nox.Application.Commands;
using Nox.Solution;
using Nox.Types;
using Nox.Application.Factories;
using Nox.Exceptions;
using Nox.Extensions;
using FluentValidation;
using TestWebApp.Infrastructure.Persistence;
using TestWebApp.Domain;
using TestWebApp.Application.Dto;
using TestEntityOwnedRelationshipOneOrManyEntity = TestWebApp.Domain.TestEntityOwnedRelationshipOneOrMany;

namespace TestWebApp.Application.Commands;

public partial record UpdateTestEntityOwnedRelationshipOneOrManyCommand(System.String keyId, TestEntityOwnedRelationshipOneOrManyUpdateDto EntityDto, Nox.Types.CultureCode CultureCode, System.Guid? Etag) : IRequest<TestEntityOwnedRelationshipOneOrManyKeyDto>;

internal partial class UpdateTestEntityOwnedRelationshipOneOrManyCommandHandler : UpdateTestEntityOwnedRelationshipOneOrManyCommandHandlerBase
{
	public UpdateTestEntityOwnedRelationshipOneOrManyCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<TestEntityOwnedRelationshipOneOrManyEntity, TestEntityOwnedRelationshipOneOrManyCreateDto, TestEntityOwnedRelationshipOneOrManyUpdateDto> entityFactory)
		: base(dbContext, noxSolution,entityFactory)
	{
	}
}

internal abstract class UpdateTestEntityOwnedRelationshipOneOrManyCommandHandlerBase : CommandBase<UpdateTestEntityOwnedRelationshipOneOrManyCommand, TestEntityOwnedRelationshipOneOrManyEntity>, IRequestHandler<UpdateTestEntityOwnedRelationshipOneOrManyCommand, TestEntityOwnedRelationshipOneOrManyKeyDto>
{
	public AppDbContext DbContext { get; }
	private readonly IEntityFactory<TestEntityOwnedRelationshipOneOrManyEntity, TestEntityOwnedRelationshipOneOrManyCreateDto, TestEntityOwnedRelationshipOneOrManyUpdateDto> _entityFactory;

	protected UpdateTestEntityOwnedRelationshipOneOrManyCommandHandlerBase(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<TestEntityOwnedRelationshipOneOrManyEntity, TestEntityOwnedRelationshipOneOrManyCreateDto, TestEntityOwnedRelationshipOneOrManyUpdateDto> entityFactory)
		: base(noxSolution)
	{
		DbContext = dbContext;
		_entityFactory = entityFactory;
	}

	public virtual async Task<TestEntityOwnedRelationshipOneOrManyKeyDto> Handle(UpdateTestEntityOwnedRelationshipOneOrManyCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		await OnExecutingAsync(request);
		var keyId = TestWebApp.Domain.TestEntityOwnedRelationshipOneOrManyMetadata.CreateId(request.keyId);

		var entity = await DbContext.TestEntityOwnedRelationshipOneOrManies.FindAsync(keyId);
		if (entity == null)
		{
			throw new EntityNotFoundException("TestEntityOwnedRelationshipOneOrMany",  $"{keyId.ToString()}");
		}
		await DbContext.Entry(entity).Collection(x => x.SecEntityOwnedRelOneOrManies).LoadAsync();

		await _entityFactory.UpdateEntityAsync(entity, request.EntityDto, request.CultureCode);
		entity.Etag = request.Etag.HasValue ? request.Etag.Value : System.Guid.Empty;

		await OnCompletedAsync(request, entity);

		DbContext.Entry(entity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();
		if (result < 1)
		{
			throw new DatabaseSaveException();
		}

		return new TestEntityOwnedRelationshipOneOrManyKeyDto(entity.Id.Value);
	}
}

public class UpdateTestEntityOwnedRelationshipOneOrManyValidator : AbstractValidator<UpdateTestEntityOwnedRelationshipOneOrManyCommand>
{
    public UpdateTestEntityOwnedRelationshipOneOrManyValidator()
    {
		RuleFor(x => x.EntityDto.SecEntityOwnedRelOneOrManies)
			.ForEach(item => 
			{
				item.Must(owned => owned.Id != null)
					.WithMessage((item, index) => $"SecEntityOwnedRelOneOrManies[{index}].Id is required.");
			});
    }
}