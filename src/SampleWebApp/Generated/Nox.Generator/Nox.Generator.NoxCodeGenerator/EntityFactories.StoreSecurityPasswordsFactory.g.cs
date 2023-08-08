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
using File = Nox.Types.File;
using SampleWebApp.Application.Dto;
using SampleWebApp.Domain;


namespace SampleWebApp.Application;

public class StoreSecurityPasswordsFactory: EntityFactoryBase<StoreSecurityPasswordsCreateDto, StoreSecurityPasswords>
{
    public  StoreSecurityPasswordsFactory(NoxSolution noxSolution, IServiceProvider serviceProvider): base(noxSolution, serviceProvider) { }

    protected override void MapEntity(StoreSecurityPasswords entity, Entity entityDefinition, StoreSecurityPasswordsCreateDto dto)
    {
    #pragma warning disable CS0168 // Variable is declared but never used        
        dynamic? noxTypeValue;
    #pragma warning restore CS0168 // Variable is declared but never used
    
        noxTypeValue =  CreateNoxType<Nox.Types.Text>(entityDefinition,"Name",dto.Name);
        if(noxTypeValue != null)
        {        
            entity.Name = noxTypeValue;
        }
        noxTypeValue =  CreateNoxType<Nox.Types.Text>(entityDefinition,"SecurityCamerasPassword",dto.SecurityCamerasPassword);
        if(noxTypeValue != null)
        {        
            entity.SecurityCamerasPassword = noxTypeValue;
        }
    }
}