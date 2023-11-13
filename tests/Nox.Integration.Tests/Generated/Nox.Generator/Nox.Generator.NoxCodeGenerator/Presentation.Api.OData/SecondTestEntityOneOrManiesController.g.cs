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

public abstract partial class SecondTestEntityOneOrManiesControllerBase : ODataController
{
    
    #region Relationships
    
    public async Task<ActionResult> CreateRefToTestEntityOneOrManies([FromRoute] System.String key, [FromRoute] System.String relatedKey)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var createdRef = await _mediator.Send(new CreateRefSecondTestEntityOneOrManyToTestEntityOneOrManiesCommand(new SecondTestEntityOneOrManyKeyDto(key), new TestEntityOneOrManyKeyDto(relatedKey)));
        if (!createdRef)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    public virtual async Task<ActionResult> PostToTestEntityOneOrManies([FromRoute] System.String key, [FromBody] TestEntityOneOrManyCreateDto testEntityOneOrMany)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var etag = Request.GetDecodedEtagHeader();
        testEntityOneOrMany.SecondTestEntityOneOrManiesId = new List<System.String> { key };
        var createdKey = await _mediator.Send(new CreateTestEntityOneOrManyCommand(testEntityOneOrMany, _cultureCode));
        
        var createdItem = (await _mediator.Send(new GetTestEntityOneOrManyByIdQuery(createdKey.keyId))).SingleOrDefault();
        
        return Created(createdItem);
    }
    
    public async Task<ActionResult> GetRefToTestEntityOneOrManies([FromRoute] System.String key)
    {
        var related = (await _mediator.Send(new GetSecondTestEntityOneOrManyByIdQuery(key))).Select(x => x.TestEntityOneOrManies).SingleOrDefault();
        if (related is null)
        {
            return NotFound();
        }
        
        IList<System.Uri> references = new List<System.Uri>();
        foreach (var item in related)
        {
            references.Add(new System.Uri($"TestEntityOneOrManies/{item.Id}", UriKind.Relative));
        }
        return Ok(references);
    }
    
    public async Task<ActionResult> DeleteRefToTestEntityOneOrManies([FromRoute] System.String key, [FromRoute] System.String relatedKey)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var deletedRef = await _mediator.Send(new DeleteRefSecondTestEntityOneOrManyToTestEntityOneOrManiesCommand(new SecondTestEntityOneOrManyKeyDto(key), new TestEntityOneOrManyKeyDto(relatedKey)));
        if (!deletedRef)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    public async Task<ActionResult> DeleteRefToTestEntityOneOrManies([FromRoute] System.String key)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var deletedAllRef = await _mediator.Send(new DeleteAllRefSecondTestEntityOneOrManyToTestEntityOneOrManiesCommand(new SecondTestEntityOneOrManyKeyDto(key)));
        if (!deletedAllRef)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    [HttpPut("api/SecondTestEntityOneOrManies/{key}/TestEntityOneOrManies/{relatedKey}")]
    public virtual async Task<ActionResult<TestEntityOneOrManyDto>> PutToTestEntityOneOrManiesNonConventional(System.String key, System.String relatedKey, [FromBody] TestEntityOneOrManyUpdateDto testEntityOneOrMany)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var related = (await _mediator.Send(new GetSecondTestEntityOneOrManyByIdQuery(key))).Select(x => x.TestEntityOneOrManies).SingleOrDefault()?.Any(x => x.Id == relatedKey);
        if (related == null || related == false)
        {
            return NotFound();
        }
        
        var etag = Request.GetDecodedEtagHeader();
        var updated = await _mediator.Send(new UpdateTestEntityOneOrManyCommand(relatedKey, testEntityOneOrMany, _cultureCode, etag));
        if (updated == null)
        {
            return NotFound();
        }
        
        return Ok();
    }
    
    #endregion
    
}
