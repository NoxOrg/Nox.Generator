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
using CryptocashApi.Application.Dto;
using CryptocashApi.Domain;
using ExchangeRate = CryptocashApi.Domain.ExchangeRate;

namespace CryptocashApi.Application;

public class ExchangeRateMapper: EntityMapperBase<ExchangeRate>
{
    public  ExchangeRateMapper(NoxSolution noxSolution, IServiceProvider serviceProvider): base(noxSolution, serviceProvider) { }

    public override void MapToEntity(ExchangeRate entity, Entity entityDefinition, dynamic dto)
    {
    #pragma warning disable CS0168 // Variable is declared but never used        
        dynamic? noxTypeValue;
    #pragma warning restore CS0168 // Variable is declared but never used
    
        noxTypeValue = CreateNoxType<Nox.Types.Number>(entityDefinition,"EffectiveRate",dto.EffectiveRate);
        if(noxTypeValue != null)
        {        
            entity.EffectiveRate = noxTypeValue;
        }
        noxTypeValue = CreateNoxType<Nox.Types.DateTime>(entityDefinition,"EffectiveAt",dto.EffectiveAt);
        if(noxTypeValue != null)
        {        
            entity.EffectiveAt = noxTypeValue;
        }
    }

    public override void PartialMapToEntity(ExchangeRate entity, Entity entityDefinition, Dictionary<string, dynamic> updatedProperties)
    {
        {
            if (updatedProperties.TryGetValue("EffectiveRate", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.Number>(entityDefinition,"EffectiveRate",value);
                if(noxTypeValue == null)
                {
                    throw new EntityAttributeIsNotNullableException("ExchangeRate", "EffectiveRate");
                }
                else
                {
                    entity.EffectiveRate = noxTypeValue;
                }
            }
        }
        {
            if (updatedProperties.TryGetValue("EffectiveAt", out dynamic? value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.DateTime>(entityDefinition,"EffectiveAt",value);
                if(noxTypeValue == null)
                {
                    throw new EntityAttributeIsNotNullableException("ExchangeRate", "EffectiveAt");
                }
                else
                {
                    entity.EffectiveAt = noxTypeValue;
                }
            }
        }
    }
}