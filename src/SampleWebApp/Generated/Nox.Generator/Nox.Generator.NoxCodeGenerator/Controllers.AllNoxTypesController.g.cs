﻿// Generated

#nullable enable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using Nox.Application;
using SampleWebApp.Application;
using SampleWebApp.Application.Dto;
using SampleWebApp.Application.Queries;
using SampleWebApp.Application.Commands;
using SampleWebApp.Application.DataTransferObjects;
using SampleWebApp.Domain;
using SampleWebApp.Infrastructure.Persistence;
using Nox.Types;

namespace SampleWebApp.Presentation.Api.OData;

public partial class AllNoxTypesController : ODataController
{
    
    /// <summary>
    /// The OData DbContext for CRUD operations.
    /// </summary>
    protected readonly ODataDbContext _databaseContext;
    
    /// <summary>
    /// The Automapper.
    /// </summary>
    protected readonly IMapper _mapper;
    
    /// <summary>
    /// The Mediator.
    /// </summary>
    protected readonly IMediator _mediator;
    
    public AllNoxTypesController(
        ODataDbContext databaseContext,
        IMapper mapper,
        IMediator mediator
    )
    {
        _databaseContext = databaseContext;
        _mapper = mapper;
        _mediator = mediator;
    }
    
    [EnableQuery]
    public async  Task<ActionResult<IQueryable<AllNoxTypeDto>>> Get()
    {
        var result = await _mediator.Send(new GetAllNoxTypesQuery());
        return Ok(result);
    }
    
    public async Task<ActionResult<AllNoxTypeDto>> Get([FromRoute] System.Int64 keyId, [FromRoute] System.String keyTextId)
    {
        var item = await _mediator.Send(new GetAllNoxTypeByIdQuery(keyId, keyTextId));
        
        if (item == null)
        {
            return NotFound();
        }
        
        return Ok(item);
    }
    
    public async Task<ActionResult> Post([FromBody]AllNoxTypeCreateDto allnoxtype)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var createdKey = await _mediator.Send(new CreateAllNoxTypeCommand(allnoxtype));
        
        return Created(createdKey);
    }
    
    public async Task<ActionResult> Put([FromRoute] System.Int64 keyId, [FromRoute] System.String keyTextId, [FromBody] AllNoxTypeUpdateDto allNoxType)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var updated = await _mediator.Send(new UpdateAllNoxTypeCommand(keyId, keyTextId, allNoxType));
        
        if (!updated)
        {
            return NotFound();
        }
        return Updated(allNoxType);
    }
    
    public async Task<ActionResult> Patch([FromRoute] System.Int64 keyId, [FromRoute] System.String keyTextId, [FromBody] Delta<AllNoxTypeUpdateDto> allNoxType)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var updateProperties = new Dictionary<string, dynamic>();
        var deletedProperties = new List<string>();

        foreach (var propertyName in allNoxType.GetChangedPropertyNames())
        {
            if(allNoxType.TryGetPropertyValue(propertyName, out dynamic value))
            {
                updateProperties[propertyName] = value;                
            }
            else
            {
                deletedProperties.Add(propertyName);
            }
        }
        
        var updated = await _mediator.Send(new PartialUpdateAllNoxTypeCommand(keyId, keyTextId, updateProperties, deletedProperties));
        
        if (!updated)
        {
            return NotFound();
        }
        return Updated(allNoxType);
    }
    
    public async Task<ActionResult> Delete([FromRoute] System.Int64 keyId, [FromRoute] System.String keyTextId)
    {
        var result = await _mediator.Send(new DeleteAllNoxTypeByIdCommand(keyId, keyTextId));
        if (!result)
        {
            return NotFound();
        }
        
        return NoContent();
    }
}
