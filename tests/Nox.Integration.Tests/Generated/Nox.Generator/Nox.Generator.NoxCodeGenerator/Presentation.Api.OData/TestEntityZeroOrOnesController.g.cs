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
using TestWebApp.Application;
using TestWebApp.Application.Dto;
using TestWebApp.Application.Queries;
using TestWebApp.Application.Commands;
using TestWebApp.Domain;
using TestWebApp.Infrastructure.Persistence;
using Nox.Types;

namespace TestWebApp.Presentation.Api.OData;

public abstract partial class TestEntityZeroOrOnesControllerBase : ODataController
{
    
    #region Relationships
    
    public async Task<ActionResult> CreateRefToSecondTestEntityZeroOrOne([FromRoute] System.String key, [FromRoute] System.String relatedKey)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var createdRef = await _mediator.Send(new CreateRefTestEntityZeroOrOneToSecondTestEntityZeroOrOneCommand(new TestEntityZeroOrOneKeyDto(key), new SecondTestEntityZeroOrOneKeyDto(relatedKey)));
        if (!createdRef)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    public virtual async Task<ActionResult> PostToSecondTestEntityZeroOrOne([FromRoute] System.String key, [FromBody] SecondTestEntityZeroOrOneCreateDto secondTestEntityZeroOrOne)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var etag = Request.GetDecodedEtagHeader();
        secondTestEntityZeroOrOne.TestEntityZeroOrOneId = key;
        var createdKey = await _mediator.Send(new CreateSecondTestEntityZeroOrOneCommand(secondTestEntityZeroOrOne, _cultureCode));
        
        var createdItem = (await _mediator.Send(new GetSecondTestEntityZeroOrOneByIdQuery(createdKey.keyId))).SingleOrDefault();
        
        return Created(createdItem);
    }
    
    public async Task<ActionResult> GetRefToSecondTestEntityZeroOrOne([FromRoute] System.String key)
    {
        var related = (await _mediator.Send(new GetTestEntityZeroOrOneByIdQuery(key))).Select(x => x.SecondTestEntityZeroOrOne).SingleOrDefault();
        if (related is null)
        {
            return NotFound();
        }
        
        var references = new System.Uri($"SecondTestEntityZeroOrOnes/{related.Id}", UriKind.Relative);
        return Ok(references);
    }
    
    public async Task<ActionResult> DeleteRefToSecondTestEntityZeroOrOne([FromRoute] System.String key, [FromRoute] System.String relatedKey)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var deletedRef = await _mediator.Send(new DeleteRefTestEntityZeroOrOneToSecondTestEntityZeroOrOneCommand(new TestEntityZeroOrOneKeyDto(key), new SecondTestEntityZeroOrOneKeyDto(relatedKey)));
        if (!deletedRef)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    public async Task<ActionResult> DeleteRefToSecondTestEntityZeroOrOne([FromRoute] System.String key)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var deletedAllRef = await _mediator.Send(new DeleteAllRefTestEntityZeroOrOneToSecondTestEntityZeroOrOneCommand(new TestEntityZeroOrOneKeyDto(key)));
        if (!deletedAllRef)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    [HttpDelete("api/TestEntityZeroOrOnes/{key}/SecondTestEntityZeroOrOne")]
    public async Task<ActionResult> DeleteToSecondTestEntityZeroOrOne([FromRoute] System.String key)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var related = (await _mediator.Send(new GetTestEntityZeroOrOneByIdQuery(key))).Select(x => x.SecondTestEntityZeroOrOne).SingleOrDefault();
        if (related == null)
        {
            return NotFound();
        }
        
        var etag = Request.GetDecodedEtagHeader();
        var deleted = await _mediator.Send(new DeleteSecondTestEntityZeroOrOneByIdCommand(related.Id, etag));
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
    
    #endregion
    
}
