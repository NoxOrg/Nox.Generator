﻿// Generated

#nullable enable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System;
using System.Net.Http.Headers;
using Nox.Application;
using Nox.Extensions;
using Cryptocash.Application;
using Cryptocash.Application.Dto;
using Cryptocash.Application.Queries;
using Cryptocash.Application.Commands;
using Cryptocash.Domain;
using Cryptocash.Infrastructure.Persistence;
using Nox.Types;

namespace Cryptocash.Presentation.Api.OData;

public abstract partial class PaymentDetailsControllerBase : ODataController
{
    
    #region Relationships
    
    public async Task<ActionResult> CreateRefToCustomer([FromRoute] System.Int64 key, [FromRoute] System.Int64 relatedKey)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var createdRef = await _mediator.Send(new CreateRefPaymentDetailToCustomerCommand(new PaymentDetailKeyDto(key), new CustomerKeyDto(relatedKey)));
        if (!createdRef)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    public virtual async Task<ActionResult> PostToCustomer([FromRoute] System.Int64 key, [FromBody] CustomerCreateDto customer)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var etag = Request.GetDecodedEtagHeader();
        customer.PaymentDetailsId = new List<System.Int64> { key };
        var createdKey = await _mediator.Send(new CreateCustomerCommand(customer, _cultureCode));
        
        var createdItem = (await _mediator.Send(new GetCustomerByIdQuery(createdKey.keyId))).SingleOrDefault();
        
        return Created(createdItem);
    }
    
    public async Task<ActionResult> GetRefToCustomer([FromRoute] System.Int64 key)
    {
        var related = (await _mediator.Send(new GetPaymentDetailByIdQuery(key))).Select(x => x.Customer).SingleOrDefault();
        if (related is null)
        {
            return NotFound();
        }
        
        var references = new System.Uri($"Customers/{related.Id}", UriKind.Relative);
        return Ok(references);
    }
    
    public async Task<ActionResult> CreateRefToPaymentProvider([FromRoute] System.Int64 key, [FromRoute] System.Int64 relatedKey)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var createdRef = await _mediator.Send(new CreateRefPaymentDetailToPaymentProviderCommand(new PaymentDetailKeyDto(key), new PaymentProviderKeyDto(relatedKey)));
        if (!createdRef)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    public virtual async Task<ActionResult> PostToPaymentProvider([FromRoute] System.Int64 key, [FromBody] PaymentProviderCreateDto paymentProvider)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var etag = Request.GetDecodedEtagHeader();
        paymentProvider.PaymentDetailsId = new List<System.Int64> { key };
        var createdKey = await _mediator.Send(new CreatePaymentProviderCommand(paymentProvider, _cultureCode));
        
        var createdItem = (await _mediator.Send(new GetPaymentProviderByIdQuery(createdKey.keyId))).SingleOrDefault();
        
        return Created(createdItem);
    }
    
    public async Task<ActionResult> GetRefToPaymentProvider([FromRoute] System.Int64 key)
    {
        var related = (await _mediator.Send(new GetPaymentDetailByIdQuery(key))).Select(x => x.PaymentProvider).SingleOrDefault();
        if (related is null)
        {
            return NotFound();
        }
        
        var references = new System.Uri($"PaymentProviders/{related.Id}", UriKind.Relative);
        return Ok(references);
    }
    
    #endregion
    
}
