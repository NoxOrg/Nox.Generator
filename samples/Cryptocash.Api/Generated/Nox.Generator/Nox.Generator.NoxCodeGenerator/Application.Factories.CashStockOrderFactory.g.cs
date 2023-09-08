﻿// Generated

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
using CashStockOrder = Cryptocash.Domain.CashStockOrder;

namespace Cryptocash.Application.Factories;

public abstract class CashStockOrderFactoryBase: IEntityFactory<CashStockOrder,CashStockOrderCreateDto>
{

    public CashStockOrderFactoryBase
    (
        )
    {
    }

    public virtual CashStockOrder CreateEntity(CashStockOrderCreateDto createDto)
    {
        return ToEntity(createDto);
    }
    private Cryptocash.Domain.CashStockOrder ToEntity(CashStockOrderCreateDto createDto)
    {
        var entity = new Cryptocash.Domain.CashStockOrder();
        entity.Amount = Cryptocash.Domain.CashStockOrder.CreateAmount(createDto.Amount);
        entity.RequestedDeliveryDate = Cryptocash.Domain.CashStockOrder.CreateRequestedDeliveryDate(createDto.RequestedDeliveryDate);
        if (createDto.DeliveryDateTime is not null)entity.DeliveryDateTime = Cryptocash.Domain.CashStockOrder.CreateDeliveryDateTime(createDto.DeliveryDateTime.NonNullValue<System.DateTimeOffset>());
        //entity.VendingMachine = VendingMachine.ToEntity();
        //entity.Employee = Employee.ToEntity();
        return entity;
    }
}

public partial class CashStockOrderFactory : CashStockOrderFactoryBase
{
}