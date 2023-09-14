﻿﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;
using Nox.Application.Commands;
using Nox.Solution;
using Nox.Types;
using Nox.Factories;
using ClientApi.Infrastructure.Persistence;
using ClientApi.Domain;
using ClientApi.Application.Dto;

namespace ClientApi.Application.Commands;
public record DeleteCountryLocalNameCommand(CountryKeyDto ParentKeyDto, CountryLocalNameKeyDto EntityKeyDto) : IRequest <bool>;

public partial class DeleteCountryLocalNameCommandHandler: CommandBase<DeleteCountryLocalNameCommand, CountryLocalName>, IRequestHandler <DeleteCountryLocalNameCommand, bool>
{
	public ClientApiDbContext DbContext { get; }

	public DeleteCountryLocalNameCommandHandler(
		ClientApiDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider): base(noxSolution, serviceProvider)
	{
		DbContext = dbContext;
	}

	public async Task<bool> Handle(DeleteCountryLocalNameCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);
		var keyId = CreateNoxTypeForKey<Country,Nox.Types.AutoNumber>("Id", request.ParentKeyDto.keyId);
		var parentEntity = await DbContext.Countries.FindAsync(keyId);
		if (parentEntity == null)
		{
			return false;
		}
		var ownedId = CreateNoxTypeForKey<CountryLocalName,Nox.Types.AutoNumber>("Id", request.EntityKeyDto.keyId);
		var entity = parentEntity.CountryLocalNames.SingleOrDefault(x => x.Id == ownedId);
		if (entity == null)
		{
			return false;
		}

		parentEntity.CountryLocalNames.Remove(entity);
		
		OnCompleted(request, entity);

		DbContext.Entry(entity).State = EntityState.Deleted;
		var result = await DbContext.SaveChangesAsync(cancellationToken);
		if (result < 1)
		{
			return false;
		}

		return true;
	}
}