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
using Nox.Exceptions;
using SampleWebApp.Application.Dto;
using SampleWebApp.Domain;


namespace SampleWebApp.Application;

public class StoreMapper: EntityMapperBase<Store>
{
    public  StoreMapper(NoxSolution noxSolution, IServiceProvider serviceProvider): base(noxSolution, serviceProvider) { }

    public override void MapToEntity(Store entity, Entity entityDefinition, dynamic dto)
    {
    #pragma warning disable CS0168 // Variable is declared but never used        
        dynamic? noxTypeValue;
    #pragma warning restore CS0168 // Variable is declared but never used
    
        noxTypeValue = CreateNoxType<Nox.Types.Text>(entityDefinition,"Name",dto.Name);
        if(noxTypeValue != null)
        {        
            entity.Name = noxTypeValue;
        }
        noxTypeValue = CreateNoxType<Nox.Types.Money>(entityDefinition,"PhysicalMoney",dto.PhysicalMoney);
        if(noxTypeValue != null)
        {        
            entity.PhysicalMoney = noxTypeValue;
        }
    }

    public override void PartialMapToEntity(Store entity, Entity entityDefinition, Dictionary<string, dynamic> updatedProperties, HashSet<string> deletedPropertyNames)
    {
      
        if(deletedPropertyNames.Contains("Name"))
        {
            throw new EntityAttributeIsNotNullableException("Store", "Name");
        }  
        if(deletedPropertyNames.Contains("PhysicalMoney"))
        {
            throw new EntityAttributeIsNotNullableException("Store", "PhysicalMoney");
        }    
    }
}