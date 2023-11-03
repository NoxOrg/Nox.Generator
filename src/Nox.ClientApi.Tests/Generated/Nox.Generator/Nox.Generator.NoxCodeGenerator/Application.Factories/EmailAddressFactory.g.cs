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
using EmailAddressEntity = ClientApi.Domain.EmailAddress;

namespace ClientApi.Application.Factories;

internal abstract class EmailAddressFactoryBase : IEntityFactory<EmailAddressEntity, EmailAddressCreateDto, EmailAddressUpdateDto>
{
    private static readonly Nox.Types.CultureCode _defaultCultureCode = Nox.Types.CultureCode.From("en-US");

    public EmailAddressFactoryBase
    (
        )
    {
    }

    public virtual EmailAddressEntity CreateEntity(EmailAddressCreateDto createDto)
    {
        return ToEntity(createDto);
    }

    public virtual void UpdateEntity(EmailAddressEntity entity, EmailAddressUpdateDto updateDto, Nox.Types.CultureCode cultureCode)
    {
        UpdateEntityInternal(entity, updateDto, cultureCode);
    }

    public virtual void PartialUpdateEntity(EmailAddressEntity entity, Dictionary<string, dynamic> updatedProperties, Nox.Types.CultureCode cultureCode)
    {
        PartialUpdateEntityInternal(entity, updatedProperties, cultureCode);
    }

    private ClientApi.Domain.EmailAddress ToEntity(EmailAddressCreateDto createDto)
    {
        var entity = new ClientApi.Domain.EmailAddress();
        entity.SetIfNotNull(createDto.Email, (entity) => entity.Email =ClientApi.Domain.EmailAddressMetadata.CreateEmail(createDto.Email.NonNullValue<System.String>()));
        entity.SetIfNotNull(createDto.IsVerified, (entity) => entity.IsVerified =ClientApi.Domain.EmailAddressMetadata.CreateIsVerified(createDto.IsVerified.NonNullValue<System.Boolean>()));
        return entity;
    }

    private void UpdateEntityInternal(EmailAddressEntity entity, EmailAddressUpdateDto updateDto, Nox.Types.CultureCode cultureCode)
    {
        entity.SetIfNotNull(updateDto.Email, (entity) => entity.Email = ClientApi.Domain.EmailAddressMetadata.CreateEmail(updateDto.Email.ToValueFromNonNull<System.String>()));
        entity.SetIfNotNull(updateDto.IsVerified, (entity) => entity.IsVerified = ClientApi.Domain.EmailAddressMetadata.CreateIsVerified(updateDto.IsVerified.ToValueFromNonNull<System.Boolean>()));
    }

    private void PartialUpdateEntityInternal(EmailAddressEntity entity, Dictionary<string, dynamic> updatedProperties, Nox.Types.CultureCode cultureCode)
    {

        if (updatedProperties.TryGetValue("Email", out var EmailUpdateValue))
        {
            if (EmailUpdateValue == null) { entity.Email = null; }
            else
            {
                entity.Email = ClientApi.Domain.EmailAddressMetadata.CreateEmail(EmailUpdateValue);
            }
        }

        if (updatedProperties.TryGetValue("IsVerified", out var IsVerifiedUpdateValue))
        {
            if (IsVerifiedUpdateValue == null) { entity.IsVerified = null; }
            else
            {
                entity.IsVerified = ClientApi.Domain.EmailAddressMetadata.CreateIsVerified(IsVerifiedUpdateValue);
            }
        }
    }

    private static bool IsDefaultCultureCode(Nox.Types.CultureCode cultureCode)
        => cultureCode == _defaultCultureCode;
}

internal partial class EmailAddressFactory : EmailAddressFactoryBase
{
}