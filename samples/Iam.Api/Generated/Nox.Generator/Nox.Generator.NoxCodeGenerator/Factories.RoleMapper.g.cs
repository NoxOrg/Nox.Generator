﻿// Generated

#nullable enable

using MediatR;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Nox.Abstractions;
using Nox.Solution;
using Nox.Domain;
using Nox.Factories;
using Nox.Types;
using Nox.Application;
using Nox.Extensions;
using Nox.Exceptions;
using IamApi.Application.Dto;
using IamApi.Domain;

namespace IamApi.Application;

public class RoleMapper: EntityMapperBase<Role>
{
    public  RoleMapper(NoxSolution noxSolution, IServiceProvider serviceProvider): base(noxSolution, serviceProvider) { }

    public override void MapToEntity(Role entity, Entity entityDefinition, dynamic dto)
    {
    #pragma warning disable CS0168 // Variable is declared but never used        
        dynamic? noxTypeValue;
    #pragma warning restore CS0168 // Variable is declared but never used
            
        noxTypeValue = CreateNoxType<Nox.Types.Text>(entityDefinition, "Id", dto.Id);        
        if(noxTypeValue != null)
        {        
            entity.Id = noxTypeValue;
        }
        noxTypeValue = CreateNoxType<Nox.Types.Text>(entityDefinition,"Name",dto.Name);
        if(noxTypeValue != null)
        {        
            entity.Name = noxTypeValue;
        }
    }

    public override void PartialMapToEntity(Role entity, Entity entityDefinition, Dictionary<string, dynamic> updatedProperties)
    {
        {
            if (updatedProperties.TryGetValue("Name", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.Text>(entityDefinition,"Name",value);
                if(noxTypeValue == null)
                {
                    throw new EntityAttributeIsNotNullableException("Role", "Name");
                }
                else
                {
                    entity.Name = noxTypeValue;
                }
            }
        }
    }
}