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

using ClientApi.Infrastructure.Persistence;
using ClientApi.Domain;
using ClientApi.Application.Dto;
using Store = ClientApi.Domain.Store;

namespace ClientApi.Application.Commands;

public record CreateStoreCommand(StoreCreateDto EntityDto) : IRequest<StoreKeyDto>;

internal partial class CreateStoreCommandHandler: CreateStoreCommandHandlerBase
{
	public CreateStoreCommandHandler(
		ClientApiDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<StoreOwner, StoreOwnerCreateDto, StoreOwnerUpdateDto> storeownerfactory,
		IEntityFactory<StoreLicense, StoreLicenseCreateDto, StoreLicenseUpdateDto> storelicensefactory,
		IEntityFactory<Store, StoreCreateDto, StoreUpdateDto> entityFactory)
		: base(dbContext, noxSolution,storeownerfactory, storelicensefactory, entityFactory)
	{
	}
}


internal abstract class CreateStoreCommandHandlerBase: CommandBase<CreateStoreCommand,Store>, IRequestHandler <CreateStoreCommand, StoreKeyDto>
{
	private readonly ClientApiDbContext _dbContext;
	private readonly IEntityFactory<Store, StoreCreateDto, StoreUpdateDto> _entityFactory;
	private readonly IEntityFactory<StoreOwner, StoreOwnerCreateDto, StoreOwnerUpdateDto> _storeownerfactory;
	private readonly IEntityFactory<StoreLicense, StoreLicenseCreateDto, StoreLicenseUpdateDto> _storelicensefactory;

	public CreateStoreCommandHandlerBase(
		ClientApiDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<StoreOwner, StoreOwnerCreateDto, StoreOwnerUpdateDto> storeownerfactory,
		IEntityFactory<StoreLicense, StoreLicenseCreateDto, StoreLicenseUpdateDto> storelicensefactory,
		IEntityFactory<Store, StoreCreateDto, StoreUpdateDto> entityFactory): base(noxSolution)
	{
		_dbContext = dbContext;
		_entityFactory = entityFactory;
		_storeownerfactory = storeownerfactory;
		_storelicensefactory = storelicensefactory;
	}

	public virtual async Task<StoreKeyDto> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);

		var entityToCreate = _entityFactory.CreateEntity(request.EntityDto);
		if(request.EntityDto.OwnershipId is not null)
		{
			var relatedKey = CreateNoxTypeForKey<StoreOwner, Nox.Types.Text>("Id", request.EntityDto.OwnershipId);
			var relatedEntity = await _dbContext.StoreOwners.FindAsync(relatedKey);
			if(relatedEntity is not null && relatedEntity.DeletedAtUtc == null)
				entityToCreate.CreateRefToOwnership(relatedEntity);
		}
		else if(request.EntityDto.Ownership is not null)
		{
			var relatedEntity = _storeownerfactory.CreateEntity(request.EntityDto.Ownership);
			entityToCreate.CreateRefToOwnership(relatedEntity);
		}
		if(request.EntityDto.LicenseId is not null)
		{
			var relatedKey = CreateNoxTypeForKey<StoreLicense, Nox.Types.AutoNumber>("Id", request.EntityDto.LicenseId);
			var relatedEntity = await _dbContext.StoreLicenses.FindAsync(relatedKey);
			if(relatedEntity is not null && relatedEntity.DeletedAtUtc == null)
				entityToCreate.CreateRefToLicense(relatedEntity);
		}
		else if(request.EntityDto.License is not null)
		{
			var relatedEntity = _storelicensefactory.CreateEntity(request.EntityDto.License);
			entityToCreate.CreateRefToLicense(relatedEntity);
		}

		OnCompleted(request, entityToCreate);
		_dbContext.Stores.Add(entityToCreate);
		await _dbContext.SaveChangesAsync();
		return new StoreKeyDto(entityToCreate.Id.Value);
	}
}