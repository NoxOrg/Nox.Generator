﻿﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;
using Nox.Application.Commands;
using Nox.Factories;
using Nox.Solution;
using Nox.Types;

using TestWebApp.Infrastructure.Persistence;
using TestWebApp.Domain;
using TestWebApp.Application.Dto;
using TestEntityExactlyOne = TestWebApp.Domain.TestEntityExactlyOne;

namespace TestWebApp.Application.Commands;

public record PartialUpdateTestEntityExactlyOneCommand(System.String keyId, Dictionary<string, dynamic> UpdatedProperties, System.Guid? Etag) : IRequest <TestEntityExactlyOneKeyDto?>;

internal class PartialUpdateTestEntityExactlyOneCommandHandler: PartialUpdateTestEntityExactlyOneCommandHandlerBase
{
	public PartialUpdateTestEntityExactlyOneCommandHandler(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider,
		IEntityFactory<TestEntityExactlyOne, TestEntityExactlyOneCreateDto, TestEntityExactlyOneUpdateDto> entityFactory) : base(dbContext,noxSolution, serviceProvider, entityFactory)
	{
	}
}
internal class PartialUpdateTestEntityExactlyOneCommandHandlerBase: CommandBase<PartialUpdateTestEntityExactlyOneCommand, TestEntityExactlyOne>, IRequestHandler<PartialUpdateTestEntityExactlyOneCommand, TestEntityExactlyOneKeyDto?>
{
	public TestWebAppDbContext DbContext { get; }
	public IEntityFactory<TestEntityExactlyOne, TestEntityExactlyOneCreateDto, TestEntityExactlyOneUpdateDto> EntityFactory { get; }

	public PartialUpdateTestEntityExactlyOneCommandHandlerBase(
		TestWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider,
		IEntityFactory<TestEntityExactlyOne, TestEntityExactlyOneCreateDto, TestEntityExactlyOneUpdateDto> entityFactory) : base(noxSolution, serviceProvider)
	{
		DbContext = dbContext;
		EntityFactory = entityFactory;
	}

	public virtual async Task<TestEntityExactlyOneKeyDto?> Handle(PartialUpdateTestEntityExactlyOneCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);
		var keyId = CreateNoxTypeForKey<TestEntityExactlyOne,Nox.Types.Text>("Id", request.keyId);

		var entity = await DbContext.TestEntityExactlyOnes.FindAsync(keyId);
		if (entity == null)
		{
			return null;
		}
		EntityFactory.PartialUpdateEntity(entity, request.UpdatedProperties);
		entity.Etag = request.Etag.HasValue ? request.Etag.Value : System.Guid.Empty;

		OnCompleted(request, entity);

		DbContext.Entry(entity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();
		return new TestEntityExactlyOneKeyDto(entity.Id.Value);
	}
}