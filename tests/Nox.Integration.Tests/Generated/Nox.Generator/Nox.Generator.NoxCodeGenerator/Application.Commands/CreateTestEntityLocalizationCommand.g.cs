﻿﻿// Generated

#nullable enable

using MediatR;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nox.Abstractions;
using Nox.Application;
using Nox.Application.Commands;
using Nox.Exceptions;
using Nox.Extensions;
using Nox.Application.Factories;
using Nox.Solution;

using TestWebApp.Infrastructure.Persistence;
using TestWebApp.Domain;
using TestWebApp.Application.Dto;
using TestEntityLocalizationEntity = TestWebApp.Domain.TestEntityLocalization;

namespace TestWebApp.Application.Commands;

public record CreateTestEntityLocalizationCommand(TestEntityLocalizationCreateDto EntityDto) : IRequest<TestEntityLocalizationKeyDto>;

internal partial class CreateTestEntityLocalizationCommandHandler : CreateTestEntityLocalizationCommandHandlerBase
{
	public CreateTestEntityLocalizationCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<TestEntityLocalizationEntity, TestEntityLocalizationCreateDto, TestEntityLocalizationUpdateDto> entityFactory)
		: base(dbContext, noxSolution,entityFactory)
	{
	}
}


internal abstract class CreateTestEntityLocalizationCommandHandlerBase : CommandBase<CreateTestEntityLocalizationCommand,TestEntityLocalizationEntity>, IRequestHandler <CreateTestEntityLocalizationCommand, TestEntityLocalizationKeyDto>
{
	protected readonly AppDbContext DbContext;
	protected readonly IEntityFactory<TestEntityLocalizationEntity, TestEntityLocalizationCreateDto, TestEntityLocalizationUpdateDto> EntityFactory;

	public CreateTestEntityLocalizationCommandHandlerBase(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<TestEntityLocalizationEntity, TestEntityLocalizationCreateDto, TestEntityLocalizationUpdateDto> entityFactory) : base(noxSolution)
	{
		DbContext = dbContext;
		EntityFactory = entityFactory;
	}

	public virtual async Task<TestEntityLocalizationKeyDto> Handle(CreateTestEntityLocalizationCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);

		var entityToCreate = EntityFactory.CreateEntity(request.EntityDto);

		await OnCompletedAsync(request, entityToCreate);
		DbContext.TestEntityLocalizations.Add(entityToCreate);
		await DbContext.SaveChangesAsync();
		return new TestEntityLocalizationKeyDto(entityToCreate.Id.Value);
	}
}