﻿// Generated

#nullable enable

using MediatR;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nox.Types;
using Nox.Application;
using Nox.Factories;
using Nox.Solution.Extensions;
using SampleWebApp.Infrastructure.Persistence;
using SampleWebApp.Domain;
using SampleWebApp.Application.Dto;


namespace SampleWebApp.Application.Commands;

//TODO support multiple keys and generated keys like nuid database number
public record CreateCountryLocalNamesCommand(CountryLocalNamesCreateDto EntityDto) : IRequest<Text>;

public class CreateCountryLocalNamesCommandHandler: IRequestHandler<CreateCountryLocalNamesCommand, Text>
{
    public SampleWebAppDbContext DbContext { get; }
    public IEntityFactory<CountryLocalNamesCreateDto,CountryLocalNames> EntityFactory { get; }

    public  CreateCountryLocalNamesCommandHandler(
        SampleWebAppDbContext dbContext,
        IEntityFactory<CountryLocalNamesCreateDto,CountryLocalNames> entityFactory)
    {
        DbContext = dbContext;
        EntityFactory = entityFactory;
    }
    
    public async Task<Text> Handle(CreateCountryLocalNamesCommand request, CancellationToken cancellationToken)
    {    
        var entityToCreate = EntityFactory.CreateEntity(request.EntityDto);
	
        DbContext.CountryLocalNames.Add(entityToCreate);
        await DbContext.SaveChangesAsync();
        return entityToCreate.Id;
    }
}