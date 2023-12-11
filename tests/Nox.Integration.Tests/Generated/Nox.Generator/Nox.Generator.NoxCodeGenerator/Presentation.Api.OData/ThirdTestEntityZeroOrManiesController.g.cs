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
using Nox.Application.Dto;
using Nox.Extensions;
using TestWebApp.Application;
using TestWebApp.Application.Dto;
using TestWebApp.Application.Queries;
using TestWebApp.Application.Commands;
using TestWebApp.Domain;
using TestWebApp.Infrastructure.Persistence;
using Nox.Types;

namespace TestWebApp.Presentation.Api.OData;

public abstract partial class ThirdTestEntityZeroOrManiesControllerBase : ODataController
{
    
    #region Relationships
    
    public virtual async Task<ActionResult> CreateRefToThirdTestEntityOneOrManies([FromRoute] System.String key, [FromRoute] System.String relatedKey)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var createdRef = await _mediator.Send(new CreateRefThirdTestEntityZeroOrManyToThirdTestEntityOneOrManiesCommand(new ThirdTestEntityZeroOrManyKeyDto(key), new ThirdTestEntityOneOrManyKeyDto(relatedKey)));
        if (!createdRef)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    [HttpPut("/api/v1/ThirdTestEntityZeroOrManies/{key}/ThirdTestEntityOneOrManies/$ref")]
    public virtual async Task<ActionResult> UpdateRefToThirdTestEntityOneOrManiesNonConventional([FromRoute] System.String key, [FromBody] ReferencesDto<System.String> referencesDto)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var relatedKeysDto = referencesDto.References.Select(x => new ThirdTestEntityOneOrManyKeyDto(x)).ToList();
        var updatedRef = await _mediator.Send(new UpdateRefThirdTestEntityZeroOrManyToThirdTestEntityOneOrManiesCommand(new ThirdTestEntityZeroOrManyKeyDto(key), relatedKeysDto));
        if (!updatedRef)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    public virtual async Task<ActionResult> GetRefToThirdTestEntityOneOrManies([FromRoute] System.String key)
    {
        var entity = (await _mediator.Send(new GetThirdTestEntityZeroOrManyByIdQuery(key))).Include(x => x.ThirdTestEntityOneOrManies).SingleOrDefault();
        if (entity is null)
        {
            return NotFound();
        }
        
        IList<System.Uri> references = new List<System.Uri>();
        foreach (var item in entity.ThirdTestEntityOneOrManies)
        {
            references.Add(new System.Uri($"ThirdTestEntityOneOrManies/{item.Id}", UriKind.Relative));
        }
        return Ok(references);
    }
    
    public virtual async Task<ActionResult> DeleteRefToThirdTestEntityOneOrManies([FromRoute] System.String key, [FromRoute] System.String relatedKey)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var deletedRef = await _mediator.Send(new DeleteRefThirdTestEntityZeroOrManyToThirdTestEntityOneOrManiesCommand(new ThirdTestEntityZeroOrManyKeyDto(key), new ThirdTestEntityOneOrManyKeyDto(relatedKey)));
        if (!deletedRef)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    public virtual async Task<ActionResult> DeleteRefToThirdTestEntityOneOrManies([FromRoute] System.String key)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var deletedAllRef = await _mediator.Send(new DeleteAllRefThirdTestEntityZeroOrManyToThirdTestEntityOneOrManiesCommand(new ThirdTestEntityZeroOrManyKeyDto(key)));
        if (!deletedAllRef)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    public virtual async Task<ActionResult> PostToThirdTestEntityOneOrManies([FromRoute] System.String key, [FromBody] ThirdTestEntityOneOrManyCreateDto thirdTestEntityOneOrMany)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        thirdTestEntityOneOrMany.ThirdTestEntityZeroOrManiesId = new List<System.String> { key };
        var createdKey = await _mediator.Send(new CreateThirdTestEntityOneOrManyCommand(thirdTestEntityOneOrMany, _cultureCode));
        
        var createdItem = (await _mediator.Send(new GetThirdTestEntityOneOrManyByIdQuery(createdKey.keyId))).SingleOrDefault();
        
        return Created(createdItem);
    }
    
    [EnableQuery]
    public virtual async Task<ActionResult<IQueryable<ThirdTestEntityOneOrManyDto>>> GetThirdTestEntityOneOrManies(System.String key)
    {
        var query = await _mediator.Send(new GetThirdTestEntityZeroOrManyByIdQuery(key));
        if (!query.Any())
        {
            return NotFound();
        }
        return Ok(query.Include(x => x.ThirdTestEntityOneOrManies).SelectMany(x => x.ThirdTestEntityOneOrManies));
    }
    
    [EnableQuery]
    [HttpGet("/api/v1/ThirdTestEntityZeroOrManies/{key}/ThirdTestEntityOneOrManies/{relatedKey}")]
    public virtual async Task<SingleResult<ThirdTestEntityOneOrManyDto>> GetThirdTestEntityOneOrManiesNonConventional(System.String key, System.String relatedKey)
    {
        var related = (await _mediator.Send(new GetThirdTestEntityZeroOrManyByIdQuery(key))).SelectMany(x => x.ThirdTestEntityOneOrManies).Where(x => x.Id == relatedKey);
        if (!related.Any())
        {
            return SingleResult.Create<ThirdTestEntityOneOrManyDto>(Enumerable.Empty<ThirdTestEntityOneOrManyDto>().AsQueryable());
        }
        return SingleResult.Create(related);
    }
    
    [HttpPut("/api/v1/ThirdTestEntityZeroOrManies/{key}/ThirdTestEntityOneOrManies/{relatedKey}")]
    public virtual async Task<ActionResult<ThirdTestEntityOneOrManyDto>> PutToThirdTestEntityOneOrManiesNonConventional(System.String key, System.String relatedKey, [FromBody] ThirdTestEntityOneOrManyUpdateDto thirdTestEntityOneOrMany)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var related = (await _mediator.Send(new GetThirdTestEntityZeroOrManyByIdQuery(key))).SelectMany(x => x.ThirdTestEntityOneOrManies).Any(x => x.Id == relatedKey);
        if (!related)
        {
            return NotFound();
        }
        
        var etag = Request.GetDecodedEtagHeader();
        var updated = await _mediator.Send(new UpdateThirdTestEntityOneOrManyCommand(relatedKey, thirdTestEntityOneOrMany, _cultureCode, etag));
        if (updated == null)
        {
            return NotFound();
        }
        
        var updatedItem = (await _mediator.Send(new GetThirdTestEntityOneOrManyByIdQuery(updated.keyId))).SingleOrDefault();
        
        return Ok(updatedItem);
    }
    
    [HttpDelete("/api/v1/ThirdTestEntityZeroOrManies/{key}/ThirdTestEntityOneOrManies/{relatedKey}")]
    public virtual async Task<ActionResult> DeleteToThirdTestEntityOneOrManies([FromRoute] System.String key, [FromRoute] System.String relatedKey)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var related = (await _mediator.Send(new GetThirdTestEntityZeroOrManyByIdQuery(key))).SelectMany(x => x.ThirdTestEntityOneOrManies).Any(x => x.Id == relatedKey);
        if (!related)
        {
            return NotFound();
        }
        
        var etag = Request.GetDecodedEtagHeader();
        var deleted = await _mediator.Send(new DeleteThirdTestEntityOneOrManyByIdCommand(new List<ThirdTestEntityOneOrManyKeyDto> { new ThirdTestEntityOneOrManyKeyDto(relatedKey) }, etag));
        if (!deleted)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    [HttpDelete("/api/v1/ThirdTestEntityZeroOrManies/{key}/ThirdTestEntityOneOrManies")]
    public virtual async Task<ActionResult> DeleteToThirdTestEntityOneOrManies([FromRoute] System.String key)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var related = (await _mediator.Send(new GetThirdTestEntityZeroOrManyByIdQuery(key))).Select(x => x.ThirdTestEntityOneOrManies).SingleOrDefault();
        if (related == null)
        {
            return NotFound();
        }
        
        var etag = Request.GetDecodedEtagHeader();
        await _mediator.Send(new DeleteThirdTestEntityOneOrManyByIdCommand(related.Select(item => new ThirdTestEntityOneOrManyKeyDto(item.Id)), etag));
        return NoContent();
    }
    
    #endregion
    
}
