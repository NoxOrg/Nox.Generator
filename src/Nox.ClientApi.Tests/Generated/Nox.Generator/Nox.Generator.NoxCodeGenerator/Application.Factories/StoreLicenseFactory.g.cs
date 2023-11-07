﻿// Generated

#nullable enable

using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using MediatR;

using Nox.Abstractions;
using Nox.Solution;
using Nox.Domain;
using Nox.Application.Factories;
using Nox.Types;
using Nox.Application;
using Nox.Extensions;
using Nox.Exceptions;

using ClientApi.Application.Dto;
using ClientApi.Domain;
using StoreLicenseEntity = ClientApi.Domain.StoreLicense;

namespace ClientApi.Application.Factories;

internal abstract class StoreLicenseFactoryBase : IEntityFactory<StoreLicenseEntity, StoreLicenseCreateDto, StoreLicenseUpdateDto>
{

    public StoreLicenseFactoryBase
    (
        )
    {
    }

    public virtual StoreLicenseEntity CreateEntity(StoreLicenseCreateDto createDto)
    {
        return ToEntity(createDto);
    }

    public virtual void UpdateEntity(StoreLicenseEntity entity, StoreLicenseUpdateDto updateDto)
    {
        UpdateEntityInternal(entity, updateDto);
    }

    public virtual void PartialUpdateEntity(StoreLicenseEntity entity, Dictionary<string, dynamic> updatedProperties)
    {
        PartialUpdateEntityInternal(entity, updatedProperties);
    }

    private ClientApi.Domain.StoreLicense ToEntity(StoreLicenseCreateDto createDto)
    {
        var entity = new ClientApi.Domain.StoreLicense();
        entity.Issuer = ClientApi.Domain.StoreLicenseMetadata.CreateIssuer(createDto.Issuer);
        entity.ExternalId = ClientApi.Domain.StoreLicenseMetadata.CreateExternalId(createDto.ExternalId);
        return entity;
    }

    private void UpdateEntityInternal(StoreLicenseEntity entity, StoreLicenseUpdateDto updateDto)
    {
        entity.Issuer = ClientApi.Domain.StoreLicenseMetadata.CreateIssuer(updateDto.Issuer.NonNullValue<System.String>());
        entity.ExternalId = ClientApi.Domain.StoreLicenseMetadata.CreateExternalId(updateDto.ExternalId.NonNullValue<System.Int64>());
    }

    private void PartialUpdateEntityInternal(StoreLicenseEntity entity, Dictionary<string, dynamic> updatedProperties)
    {

        if (updatedProperties.TryGetValue("Issuer", out var IssuerUpdateValue))
        {
            if (IssuerUpdateValue == null)
            {
                throw new ArgumentException("Attribute 'Issuer' can't be null");
            }
            {
                entity.Issuer = ClientApi.Domain.StoreLicenseMetadata.CreateIssuer(IssuerUpdateValue);
            }
        }

        if (updatedProperties.TryGetValue("ExternalId", out var ExternalIdUpdateValue))
        {
            if (ExternalIdUpdateValue == null)
            {
                throw new ArgumentException("Attribute 'ExternalId' can't be null");
            }
            {
                entity.ExternalId = ClientApi.Domain.StoreLicenseMetadata.CreateExternalId(ExternalIdUpdateValue);
            }
        }
    }
}

internal partial class StoreLicenseFactory : StoreLicenseFactoryBase
{
}