﻿// Generated

#nullable enable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Nox.Application;
using Cryptocash.Application;
using Cryptocash.Application.Dto;
using Cryptocash.Application.Queries;
using Cryptocash.Application.Commands;
using Cryptocash.Domain;
using Cryptocash.Infrastructure.Persistence;
using Nox.Types;

namespace Cryptocash.Presentation.Api.OData;

[Route("{controller}")]
public partial class CustomerPaymentDetailsController : ODataController
{
    
    /// <summary>
    /// The OData DbContext for CRUD operations.
    /// </summary>
    protected readonly DtoDbContext _databaseContext;
    
    /// <summary>
    /// The Mediator.
    /// </summary>
    protected readonly IMediator _mediator;
    
    public CustomerPaymentDetailsController(
        DtoDbContext databaseContext,
        IMediator mediator
    )
    {
        _databaseContext = databaseContext;
        _mediator = mediator;
    }
    
    [HttpGet]
    [EnableQuery]
    public async  Task<ActionResult<IQueryable<CustomerPaymentDetailsDto>>> Get()
    {
        var result = await _mediator.Send(new GetCustomerPaymentDetailsQuery());
        return Ok(result);
    }
    
    public async Task<ActionResult<CustomerPaymentDetailsDto>> Get([FromRoute] System.Int64 key)
    {
        var item = await _mediator.Send(new GetCustomerPaymentDetailsByIdQuery(key));
        
        if (item == null)
        {
            return NotFound();
        }
        
        return Ok(item);
    }
    
    [HttpPost]
    public async Task<ActionResult> Post([FromBody]CustomerPaymentDetailsCreateDto customerpaymentdetails)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var createdKey = await _mediator.Send(new CreateCustomerPaymentDetailsCommand(customerpaymentdetails));
        
        return Created(createdKey);
    }
    
    [HttpPut]
    public async Task<ActionResult> Put([FromRoute] System.Int64 key, [FromBody] CustomerPaymentDetailsUpdateDto customerPaymentDetails)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var updated = await _mediator.Send(new UpdateCustomerPaymentDetailsCommand(key, customerPaymentDetails));
        
        if (updated is null)
        {
            return NotFound();
        }
        return Updated(updated);
    }
    
    [HttpPatch]
    public async Task<ActionResult> Patch([FromRoute] System.Int64 key, [FromBody] Delta<CustomerPaymentDetailsUpdateDto> customerPaymentDetails)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var updateProperties = new Dictionary<string, dynamic>();
        
        foreach (var propertyName in customerPaymentDetails.GetChangedPropertyNames())
        {
            if(customerPaymentDetails.TryGetPropertyValue(propertyName, out dynamic value))
            {
                updateProperties[propertyName] = value;                
            }           
        }
        
        var updated = await _mediator.Send(new PartialUpdateCustomerPaymentDetailsCommand(key, updateProperties));
        
        if (updated is null)
        {
            return NotFound();
        }
        return Updated(updated);
    }
    
    [HttpDelete]
    public async Task<ActionResult> Delete([FromRoute] System.Int64 key)
    {
        var result = await _mediator.Send(new DeleteCustomerPaymentDetailsByIdCommand(key));
        if (!result)
        {
            return NotFound();
        }
        
        return NoContent();
    }
}
