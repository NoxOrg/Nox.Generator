﻿﻿// Generated

#nullable enable

using MediatR;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nox.Application;
using Nox.Application.Commands;
using Nox.Factories;
using Nox.Solution;
using Nox.Types;

using SampleWebApp.Infrastructure.Persistence;
using SampleWebApp.Domain;
using SampleWebApp.Application.Dto;
using CountryLocalName = SampleWebApp.Domain.CountryLocalName;

namespace SampleWebApp.Application.Commands;
public record AddCountryLocalNameCommand(CountryKeyDto ParentKeyDto, CountryLocalNameCreateDto EntityDto) : IRequest <CountryLocalNameKeyDto?>;

public partial class AddCountryLocalNameCommandHandler: CommandBase<AddCountryLocalNameCommand, CountryLocalName>, IRequestHandler <AddCountryLocalNameCommand, CountryLocalNameKeyDto?>
{
	public SampleWebAppDbContext DbContext { get; }

	public AddCountryLocalNameCommandHandler(
		SampleWebAppDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider): base(noxSolution, serviceProvider)
	{
		DbContext = dbContext;		
	}

	public async Task<CountryLocalNameKeyDto?> Handle(AddCountryLocalNameCommand request, CancellationToken cancellationToken)
	{
		OnExecuting(request);
		var keyId = CreateNoxTypeForKey<Country,DatabaseNumber>("Id", request.ParentKeyDto.keyId);

		var parentEntity = await DbContext.Countries.FindAsync(keyId);
		if (parentEntity == null)
		{
			return null;
		}

		var entity = request.EntityDto.ToEntity();
		
		parentEntity.CountryLocalNames.Add(entity);

		OnCompleted(entity);
	
		DbContext.Entry(parentEntity).State = EntityState.Modified;
		var result = await DbContext.SaveChangesAsync();
		return new CountryLocalNameKeyDto(entity.Id.Value);
	}
}