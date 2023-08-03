﻿// Generated

#nullable enable

using MediatR;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Nox.Solution;
using Nox.Domain;
using Nox.Factories;
using Nox.Types;
using Nox.Application;
using Nox.Extensions;
using SampleWebApp.Presentation.Api.OData;
using SampleWebApp.Domain;


namespace SampleWebApp.Application;

public class CurrencyFactory: EntityFactoryBase<CurrencyDto, Currency>
{
    public  CurrencyFactory(NoxSolution noxSolution, IServiceProvider serviceProvider): base(noxSolution, serviceProvider) { }

    protected override void MapEntity(Currency entity, Entity entityDefinition, CurrencyDto dto)
    {
    #pragma warning disable CS0168 // Variable is declared but never used        
        dynamic? noxTypeValue;
    #pragma warning restore CS0168 // Variable is declared but never used            
    
            noxTypeValue =  CreateNoxType<Text>(entityDefinition,"Name",dto.Name);
            if(noxTypeValue != null)
            {        
                entity.Name = noxTypeValue;
            }
    }
}