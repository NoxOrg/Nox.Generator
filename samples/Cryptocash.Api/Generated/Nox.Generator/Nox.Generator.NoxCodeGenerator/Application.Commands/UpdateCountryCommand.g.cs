﻿﻿﻿// Generated

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
using Cryptocash.Infrastructure.Persistence;
using Cryptocash.Domain;
using Cryptocash.Application.Dto;
using CountryEntity = Cryptocash.Domain.Country;

namespace Cryptocash.Application.Commands;

public partial record UpdateCountryCommand(System.String keyId, CountryUpdateDto EntityDto, Nox.Types.CultureCode CultureCode, System.Guid? Etag) : IRequest<CountryKeyDto?>;

internal partial class UpdateCountryCommandHandler : UpdateCountryCommandHandlerBase
{
	public UpdateCountryCommandHandler(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<CountryEntity, CountryCreateDto, CountryUpdateDto> entityFactory) 
		: base(dbContext, noxSolution, entityFactory)
	{
	}
}

internal abstract class UpdateCountryCommandHandlerBase : CommandBase<UpdateCountryCommand, CountryEntity>, IRequestHandler<UpdateCountryCommand, CountryKeyDto?>
{
	public AppDbContext DbContext { get; }
	private readonly IEntityFactory<CountryEntity, CountryCreateDto, CountryUpdateDto> _entityFactory;

	public UpdateCountryCommandHandlerBase(
        AppDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<CountryEntity, CountryCreateDto, CountryUpdateDto> entityFactory)
		: base(noxSolution)
	{
		DbContext = dbContext;
		_entityFactory = entityFactory;
	}

	public virtual async Task<CountryKeyDto?> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		await OnExecutingAsync(request);
		var keyId = Cryptocash.Domain.CountryMetadata.CreateId(request.keyId);

		var entity = await DbContext.Countries.FindAsync(keyId);
		if (entity == null)
		{
			return null;
		}
		await DbContext.Entry(entity).Collection(x => x.CountryTimeZones).LoadAsync();
		var keysToUpdateCountryTimeZones = request.EntityDto.CountryTimeZones
			.Where(x => x.Id != null)
			.Select(x => Cryptocash.Domain.CountryTimeZoneMetadata.CreateId(x.Id.NonNullValue<System.Int64>()));
		foreach(var ownedEntity in entity.CountryTimeZones)
		{
			if(!keysToUpdateCountryTimeZones.Any(x => x == ownedEntity.Id))
				DbContext.Entry(ownedEntity).State = EntityState.Deleted;
		}
		await DbContext.Entry(entity).Collection(x => x.Holidays).LoadAsync();
		var keysToUpdateHolidays = request.EntityDto.Holidays
			.Where(x => x.Id != null)
			.Select(x => Cryptocash.Domain.HolidayMetadata.CreateId(x.Id.NonNullValue<System.Int64>()));
		foreach(var ownedEntity in entity.Holidays)
		{
			if(!keysToUpdateHolidays.Any(x => x == ownedEntity.Id))
				DbContext.Entry(ownedEntity).State = EntityState.Deleted;
		}

		_entityFactory.UpdateEntity(entity, request.EntityDto, request.CultureCode);
		entity.Etag = request.Etag.HasValue ? request.Etag.Value : System.Guid.Empty;

		await OnCompletedAsync(request, entity);

		DbContext.Entry(entity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();
		if (result < 1)
		{
			return null;
		}

		return new CountryKeyDto(entity.Id.Value);
	}
}

public class UpdateCountryValidator : AbstractValidator<UpdateCountryCommand>
{
    public UpdateCountryValidator()
    {
    }
}