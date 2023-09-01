﻿﻿// Generated

#nullable enable

using MediatR;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nox.Abstractions;
using Nox.Application;
using Nox.Application.Commands;
using Nox.Factories;
using Nox.Solution;

using Cryptocash.Infrastructure.Persistence;
using Cryptocash.Domain;
using Cryptocash.Application.Dto;
using CountryTimeZones = Cryptocash.Domain.CountryTimeZones;

namespace Cryptocash.Application.Commands;
public record CreateCountryTimeZonesCommand(CountryTimeZonesCreateDto EntityDto) : IRequest<CountryTimeZonesKeyDto>;

public partial class CreateCountryTimeZonesCommandHandler: CommandBase<CreateCountryTimeZonesCommand,CountryTimeZones>, IRequestHandler <CreateCountryTimeZonesCommand, CountryTimeZonesKeyDto>
{
	public CryptocashDbContext DbContext { get; }
	public IEntityFactory<CountryTimeZonesCreateDto,CountryTimeZones> EntityFactory { get; }

	public CreateCountryTimeZonesCommandHandler(
		CryptocashDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider,
		IEntityFactory<CountryTimeZonesCreateDto,CountryTimeZones> entityFactory): base(noxSolution, serviceProvider)
	{
		DbContext = dbContext;
		EntityFactory = entityFactory;
	}

	public async Task<CountryTimeZonesKeyDto> Handle(CreateCountryTimeZonesCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);

		var entityToCreate = EntityFactory.CreateEntity(request.EntityDto);
	
		OnCompleted(entityToCreate);
		DbContext.CountryTimeZones.Add(entityToCreate);
		await DbContext.SaveChangesAsync();
		return new CountryTimeZonesKeyDto(entityToCreate.Id.Value);
	}
}