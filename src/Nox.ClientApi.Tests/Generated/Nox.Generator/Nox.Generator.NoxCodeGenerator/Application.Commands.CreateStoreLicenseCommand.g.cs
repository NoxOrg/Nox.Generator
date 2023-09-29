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
using StoreLicense = ClientApi.Domain.StoreLicense;

namespace ClientApi.Application.Commands;

public record CreateStoreLicenseCommand(StoreLicenseCreateDto EntityDto) : IRequest<StoreLicenseKeyDto>;

internal partial class CreateStoreLicenseCommandHandler: CreateStoreLicenseCommandHandlerBase
{
	public CreateStoreLicenseCommandHandler(
		ClientApiDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<Store, StoreCreateDto, StoreUpdateDto> storefactory,
		IEntityFactory<StoreLicense, StoreLicenseCreateDto, StoreLicenseUpdateDto> entityFactory,
		IServiceProvider serviceProvider)
		: base(dbContext, noxSolution,storefactory, entityFactory, serviceProvider)
	{
	}
}


internal abstract class CreateStoreLicenseCommandHandlerBase: CommandBase<CreateStoreLicenseCommand,StoreLicense>, IRequestHandler <CreateStoreLicenseCommand, StoreLicenseKeyDto>
{
	private readonly ClientApiDbContext _dbContext;
	private readonly IEntityFactory<StoreLicense, StoreLicenseCreateDto, StoreLicenseUpdateDto> _entityFactory;
	private readonly IEntityFactory<Store, StoreCreateDto, StoreUpdateDto> _storefactory;

	public CreateStoreLicenseCommandHandlerBase(
		ClientApiDbContext dbContext,
		NoxSolution noxSolution,
		IEntityFactory<Store, StoreCreateDto, StoreUpdateDto> storefactory,
		IEntityFactory<StoreLicense, StoreLicenseCreateDto, StoreLicenseUpdateDto> entityFactory,
		IServiceProvider serviceProvider): base(noxSolution, serviceProvider)
	{
		_dbContext = dbContext;
		_entityFactory = entityFactory;
		_storefactory = storefactory;
	}

	public virtual async Task<StoreLicenseKeyDto> Handle(CreateStoreLicenseCommand request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		OnExecuting(request);

		var entityToCreate = _entityFactory.CreateEntity(request.EntityDto);
		if(request.EntityDto.StoreWithLicenseId is not null)
		{
			var relatedKey = CreateNoxTypeForKey<Store, Nox.Types.Guid>("Id", request.EntityDto.StoreWithLicenseId);
			var relatedEntity = await _dbContext.Stores.FindAsync(relatedKey);
			if(relatedEntity is not null && relatedEntity.DeletedAtUtc == null)
				entityToCreate.CreateRefToStoreWithLicense(relatedEntity);
		}
		else if(request.EntityDto.StoreWithLicense is not null)
		{
			var relatedEntity = _storefactory.CreateEntity(request.EntityDto.StoreWithLicense);
			entityToCreate.CreateRefToStoreWithLicense(relatedEntity);
		}

		OnCompleted(request, entityToCreate);
		_dbContext.StoreLicenses.Add(entityToCreate);
		await _dbContext.SaveChangesAsync();
		return new StoreLicenseKeyDto(entityToCreate.Id.Value);
	}
}