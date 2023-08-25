﻿﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;
using Nox.Application.Commands;
using Nox.Solution;
using Nox.Types;
using Nox.Factories;
using CryptocashApi.Infrastructure.Persistence;
using CryptocashApi.Domain;
using CryptocashApi.Application.Dto;

namespace CryptocashApi.Application.Commands;

public record UpdateCountryHolidayCommand(System.Int64 keyId, CountryHolidayUpdateDto EntityDto) : IRequest<CountryHolidayKeyDto?>;

public class UpdateCountryHolidayCommandHandler: CommandBase<UpdateCountryHolidayCommand, CountryHoliday>, IRequestHandler<UpdateCountryHolidayCommand, CountryHolidayKeyDto?>
{
	public CryptocashApiDbContext DbContext { get; }
	public IEntityMapper<CountryHoliday> EntityMapper { get; }

	public UpdateCountryHolidayCommandHandler(
		CryptocashApiDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider,
		IEntityMapper<CountryHoliday> entityMapper): base(noxSolution, serviceProvider)
	{
		DbContext = dbContext;
		EntityMapper = entityMapper;
	}
	
	public async Task<CountryHolidayKeyDto?> Handle(UpdateCountryHolidayCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);
		var keyId = CreateNoxTypeForKey<CountryHoliday,DatabaseNumber>("Id", request.keyId);
	
		var entity = await DbContext.CountryHolidays.FindAsync(keyId);
		if (entity == null)
		{
			return null;
		}
		EntityMapper.MapToEntity(entity, GetEntityDefinition<CountryHoliday>(), request.EntityDto);

		OnCompleted(entity);

		DbContext.Entry(entity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();
		if(result < 1)
			return null;

		return new CountryHolidayKeyDto(entity.Id.Value);
	}
}