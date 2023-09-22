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
using ClientApi.Application.Dto;
using ClientApi.Domain;
using Store = ClientApi.Domain.Store;

namespace ClientApi.Application;

public partial class StoreMapper : EntityMapperBase<Store>
{
    public StoreMapper(NoxSolution noxSolution, IServiceProvider serviceProvider) : base(noxSolution, serviceProvider) { }

    public override void PartialMapToEntity(Store entity, Entity entityDefinition, Dictionary<string, dynamic> updatedProperties)
    {
#pragma warning disable CS0168 // Variable is assigned but its value is never used
        dynamic? value;
#pragma warning restore CS0168 // Variable is assigned but its value is never used
        {
            if (updatedProperties.TryGetValue("Name", out value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.Text>(entityDefinition, "Name", value);
                if(noxTypeValue == null)
                {
                    throw new EntityAttributeIsNotNullableException("Store", "Name");
                }
                else
                {
                    entity.Name = noxTypeValue;
                }
            }
        }
        {
            if (updatedProperties.TryGetValue("Address", out value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.StreetAddress>(entityDefinition, "Address", value);
                if(noxTypeValue == null)
                {
                    throw new EntityAttributeIsNotNullableException("Store", "Address");
                }
                else
                {
                    entity.Address = noxTypeValue;
                }
            }
        }
        {
            if (updatedProperties.TryGetValue("Location", out value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.LatLong>(entityDefinition, "Location", value);
                if(noxTypeValue == null)
                {
                    throw new EntityAttributeIsNotNullableException("Store", "Location");
                }
                else
                {
                    entity.Location = noxTypeValue;
                }
            }
        }
        {
            if (updatedProperties.TryGetValue("OpeningDay", out value))
            {
                var noxTypeValue = CreateNoxType<Nox.Types.DateTime>(entityDefinition, "OpeningDay", value);
                if(noxTypeValue == null)
                {
                    entity.OpeningDay = null;
                }
                else
                {
                    entity.OpeningDay = noxTypeValue;
                }
            }
        }
    
    
        /// <summary>
        /// Store Owner of the Store ZeroOrOne StoreOwners
        /// </summary>
        if (updatedProperties.TryGetValue("StoreOwnerId", out value))
        {
            var noxRelationshipTypeValue = CreateNoxType<Nox.Types.Text>(entityDefinition, "Ownership", value);
            if (noxRelationshipTypeValue != null)
            {        
                entity.OwnershipId = noxRelationshipTypeValue;
            }
        }
    }
}