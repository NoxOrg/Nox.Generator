﻿﻿// Generated

#nullable enable

using MediatR;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nox.Abstractions;
using Nox.Application;
using Nox.Factories;
using SampleWebApp.Infrastructure.Persistence;
using SampleWebApp.Domain;
using SampleWebApp.Application.Dto;

namespace SampleWebApp.Application.Commands;
public record CreateStoreSecurityPasswordsCommand(StoreSecurityPasswordsCreateDto EntityDto) : IRequest<StoreSecurityPasswordsKeyDto>;

public class CreateStoreSecurityPasswordsCommandHandler: IRequestHandler<CreateStoreSecurityPasswordsCommand, StoreSecurityPasswordsKeyDto>
{
	private readonly IUserProvider _userProvider;
	private readonly ISystemProvider _systemProvider;

    public SampleWebAppDbContext DbContext { get; }
    public IEntityFactory<StoreSecurityPasswordsCreateDto,StoreSecurityPasswords> EntityFactory { get; }

    public  CreateStoreSecurityPasswordsCommandHandler(
        SampleWebAppDbContext dbContext,
        IEntityFactory<StoreSecurityPasswordsCreateDto,StoreSecurityPasswords> entityFactory,
		IUserProvider userProvider,
		ISystemProvider systemProvider)
    {
        DbContext = dbContext;
        EntityFactory = entityFactory;
		_userProvider = userProvider;
		_systemProvider = systemProvider;
    }
    
    public async Task<StoreSecurityPasswordsKeyDto> Handle(CreateStoreSecurityPasswordsCommand request, CancellationToken cancellationToken)
    {    
        var entityToCreate = EntityFactory.CreateEntity(request.EntityDto);        
		var createdBy = _userProvider.GetUser();
		var createdVia = _systemProvider.GetSystem();
		entityToCreate.Created(createdBy, createdVia);
	
        DbContext.StoreSecurityPasswords.Add(entityToCreate);
        await DbContext.SaveChangesAsync();
        return new StoreSecurityPasswordsKeyDto(entityToCreate.Id.Value);
}
}