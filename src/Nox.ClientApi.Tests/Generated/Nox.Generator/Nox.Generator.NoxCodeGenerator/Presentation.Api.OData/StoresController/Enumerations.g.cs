﻿// Generated

#nullable enable
using Microsoft.AspNetCore.Mvc;

using DtoNameSpace = ClientApi.Application.Dto;
using ApplicationQueriesNameSpace = ClientApi.Application.Queries;

namespace ClientApi.Presentation.Api.OData;

public abstract partial class StoresControllerBase
{
    [HttpGet("api/Stores/Statuses")]
    public virtual async Task<ActionResult<IQueryable<DtoNameSpace.StoreStatusDto>>> GetStatusesNonConventional()
    {            
        var result = await _mediator.Send(new ApplicationQueriesNameSpace.GetStoresStatusesQuery());                        
        return Ok(result);        
    }
}
