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

namespace ClientApi.Application.Commands;
public record CreateClientDatabaseNumberCommand(ClientDatabaseNumberCreateDto EntityDto) : IRequest<ClientDatabaseNumberKeyDto>;

public class CreateClientDatabaseNumberCommandHandler: CommandBase, IRequestHandler <CreateClientDatabaseNumberCommand, ClientDatabaseNumberKeyDto>
{
	private readonly IUserProvider _userProvider;
	private readonly ISystemProvider _systemProvider;

	public ClientApiDbContext DbContext { get; }
	public IEntityFactory<ClientDatabaseNumberCreateDto,ClientDatabaseNumber> EntityFactory { get; }

	public CreateClientDatabaseNumberCommandHandler(
		ClientApiDbContext dbContext,
		NoxSolution noxSolution,
		IServiceProvider serviceProvider,
		IEntityFactory<ClientDatabaseNumberCreateDto,ClientDatabaseNumber> entityFactory,
		IUserProvider userProvider,
		ISystemProvider systemProvider): base(noxSolution, serviceProvider)
	{
		DbContext = dbContext;
		EntityFactory = entityFactory;
		_userProvider = userProvider;
		_systemProvider = systemProvider;
	}

	public async Task<ClientDatabaseNumberKeyDto> Handle(CreateClientDatabaseNumberCommand request, CancellationToken cancellationToken)
	{
		var entityToCreate = EntityFactory.CreateEntity(request.EntityDto);
		var createdBy = _userProvider.GetUser();
		var createdVia = _systemProvider.GetSystem();
		entityToCreate.Created(createdBy, createdVia);
	
		DbContext.ClientDatabaseNumbers.Add(entityToCreate);
		await DbContext.SaveChangesAsync();
		return new ClientDatabaseNumberKeyDto(entityToCreate.Id.Value);
	}
}