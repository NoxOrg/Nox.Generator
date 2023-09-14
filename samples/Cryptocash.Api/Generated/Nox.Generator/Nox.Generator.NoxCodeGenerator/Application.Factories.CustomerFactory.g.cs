﻿using System;// Generated

#nullable enable

using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using MediatR;

using Nox.Abstractions;
using Nox.Solution;
using Nox.Domain;
using Nox.Factories;
using Nox.Types;
using Nox.Application;
using Nox.Extensions;
using Nox.Exceptions;

using Cryptocash.Application.Dto;
using Cryptocash.Domain;
using Customer = Cryptocash.Domain.Customer;

namespace Cryptocash.Application.Factories;

public abstract class CustomerFactoryBase: IEntityFactory<Customer,CustomerCreateDto>
{

    public CustomerFactoryBase
    (
        )
    {
    }

    public virtual Customer CreateEntity(CustomerCreateDto createDto)
    {
        return ToEntity(createDto);
    }
    private Cryptocash.Domain.Customer ToEntity(CustomerCreateDto createDto)
    {
        var entity = new Cryptocash.Domain.Customer();
        entity.FirstName = Cryptocash.Domain.Customer.CreateFirstName(createDto.FirstName);
        entity.LastName = Cryptocash.Domain.Customer.CreateLastName(createDto.LastName);
        entity.EmailAddress = Cryptocash.Domain.Customer.CreateEmailAddress(createDto.EmailAddress);
        entity.Address = Cryptocash.Domain.Customer.CreateAddress(createDto.Address);
        if (createDto.MobileNumber is not null)entity.MobileNumber = Cryptocash.Domain.Customer.CreateMobileNumber(createDto.MobileNumber.NonNullValue<System.String>());
        //entity.PaymentDetails = PaymentDetails.Select(dto => dto.ToEntity()).ToList();
        //entity.Bookings = Bookings.Select(dto => dto.ToEntity()).ToList();
        //entity.Transactions = Transactions.Select(dto => dto.ToEntity()).ToList();
        //entity.Country = Country.ToEntity();
        return entity;
    }
}

public partial class CustomerFactory : CustomerFactoryBase
{
}