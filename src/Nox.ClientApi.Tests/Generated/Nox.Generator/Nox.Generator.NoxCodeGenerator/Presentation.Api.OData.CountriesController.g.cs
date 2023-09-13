﻿// Generated

#nullable enable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Net.Http.Headers;
using Nox.Application;
using Nox.Extensions;
using ClientApi.Application;
using ClientApi.Application.Dto;
using ClientApi.Application.Queries;
using ClientApi.Application.Commands;
using ClientApi.Domain;
using ClientApi.Infrastructure.Persistence;
using Nox.Types;

namespace ClientApi.Presentation.Api.OData;

public partial class CountriesController : CountriesControllerBase
            {
                public CountriesController(IMediator mediator, DtoDbContext databaseContext):base(databaseContext, mediator)
                {}
            }
public abstract class CountriesControllerBase : ODataController
{
    
    /// <summary>
    /// The OData DbContext for CRUD operations.
    /// </summary>
    protected readonly DtoDbContext _databaseContext;
    
    /// <summary>
    /// The Mediator.
    /// </summary>
    protected readonly IMediator _mediator;
    
    public CountriesControllerBase(
        DtoDbContext databaseContext,
        IMediator mediator
    )
    {
        _databaseContext = databaseContext;
        _mediator = mediator;
    }
    
    [EnableQuery]
    public virtual async Task<ActionResult<IQueryable<CountryDto>>> Get()
    {
        var result = await _mediator.Send(new GetCountriesQuery());
        return Ok(result);
    }
    
    [EnableQuery]
    public async Task<ActionResult<CountryDto>> Get([FromRoute] System.Int64 key)
    {
        var item = await _mediator.Send(new GetCountryByIdQuery(key));
        
        if (item == null)
        {
            return NotFound();
        }
        
        return Ok(item);
    }
    
    public virtual async Task<ActionResult<CountryDto>> Post([FromBody]CountryCreateDto country)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var createdKey = await _mediator.Send(new CreateCountryCommand(country));
        
        var item = await _mediator.Send(new GetCountryByIdQuery(createdKey.keyId));
        
        return Created(item);
    }
    
    public virtual async Task<ActionResult<CountryDto>> Put([FromRoute] System.Int64 key, [FromBody] CountryUpdateDto country)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var etag = Request.GetDecodedEtagHeader();
        var updated = await _mediator.Send(new UpdateCountryCommand(key, country, etag));
        
        if (updated is null)
        {
            return NotFound();
        }
        
        var item = await _mediator.Send(new GetCountryByIdQuery(updated.keyId));
        
        return Ok(item);
    }
    
    public virtual async Task<ActionResult<CountryDto>> Patch([FromRoute] System.Int64 key, [FromBody] Delta<CountryDto> country)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var updateProperties = new Dictionary<string, dynamic>();
        
        foreach (var propertyName in country.GetChangedPropertyNames())
        {
            if(country.TryGetPropertyValue(propertyName, out dynamic value))
            {
                updateProperties[propertyName] = value;                
            }           
        }
        
        var etag = Request.GetDecodedEtagHeader();
        var updated = await _mediator.Send(new PartialUpdateCountryCommand(key, updateProperties, etag));
        
        if (updated is null)
        {
            return NotFound();
        }
        var item = await _mediator.Send(new GetCountryByIdQuery(updated.keyId));
        return Ok(item);
    }
    
    public virtual async Task<ActionResult> Delete([FromRoute] System.Int64 key)
    {
        var etag = Request.GetDecodedEtagHeader();
        var result = await _mediator.Send(new DeleteCountryByIdCommand(key, etag));
        
        if (!result)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    #region Owned Relationships
    
    [EnableQuery]
    public virtual async Task<ActionResult<IQueryable<CountryLocalNameDto>>> GetCountryLocalNames([FromRoute] System.Int64 key)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var item = await _mediator.Send(new GetCountryByIdQuery(key));
        
        if (item is null)
        {
            return NotFound();
        }
        
        return Ok(item.CountryLocalNames);
    }
    
    [EnableQuery]
    [HttpGet("api/Countries/{key}/CountryLocalNames/{relatedKey}")]
    public virtual async Task<ActionResult<CountryLocalNameDto>> GetCountryLocalNameNonConventional(System.Int64 key, System.Int64 relatedKey)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var child = await TryGetCountryLocalName(key, new CountryLocalNameKeyDto(relatedKey));
        if (child == null)
        {
            return NotFound();
        }
        
        return Ok(child);
    }
    
    public virtual async Task<ActionResult> PostToCountryLocalNames([FromRoute] System.Int64 key, [FromBody] CountryLocalNameCreateDto countryLocalName)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var etag = Request.GetDecodedEtagHeader();
        var createdKey = await _mediator.Send(new AddCountryLocalNameCommand(new CountryKeyDto(key), countryLocalName, etag));
        if (createdKey == null)
        {
            return NotFound();
        }
        
        var child = await TryGetCountryLocalName(key, createdKey);
        if (child == null)
        {
            return NotFound();
        }
        
        return Created(child);
    }
    
    [HttpPut("api/Countries/{key}/CountryLocalNames/{relatedKey}")]
    public virtual async Task<ActionResult<CountryLocalNameDto>> PutToCountryLocalNamesNonConventional(System.Int64 key, System.Int64 relatedKey, [FromBody] CountryLocalNameUpdateDto countryLocalName)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var etag = Request.GetDecodedEtagHeader();
        var updatedKey = await _mediator.Send(new UpdateCountryLocalNameCommand(new CountryKeyDto(key), new CountryLocalNameKeyDto(relatedKey), countryLocalName, etag));
        if (updatedKey == null)
        {
            return NotFound();
        }
        
        var child = await TryGetCountryLocalName(key, updatedKey);
        if (child == null)
        {
            return NotFound();
        }
        
        return Ok(child);
    }
    
    [HttpPatch("api/Countries/{key}/CountryLocalNames/{relatedKey}")]
    public virtual async Task<ActionResult> PatchToCountryLocalNamesNonConventional(System.Int64 key, System.Int64 relatedKey, [FromBody] Delta<CountryLocalNameDto> countryLocalName)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var updateProperties = new Dictionary<string, dynamic>();
        
        foreach (var propertyName in countryLocalName.GetChangedPropertyNames())
        {
            if(countryLocalName.TryGetPropertyValue(propertyName, out dynamic value))
            {
                updateProperties[propertyName] = value;                
            }           
        }
        
        var etag = Request.GetDecodedEtagHeader();
        var updated = await _mediator.Send(new PartialUpdateCountryLocalNameCommand(new CountryKeyDto(key), new CountryLocalNameKeyDto(relatedKey), updateProperties, etag));
        
        if (updated is null)
        {
            return NotFound();
        }
        var child = await TryGetCountryLocalName(key, updated);
        if (child == null)
        {
            return NotFound();
        }
        
        return Ok(child);
    }
    
    [HttpDelete("api/Countries/{key}/CountryLocalNames/{relatedKey}")]
    public virtual async Task<ActionResult> DeleteCountryLocalNameNonConventional(System.Int64 key, System.Int64 relatedKey)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _mediator.Send(new DeleteCountryLocalNameCommand(new CountryKeyDto(key), new CountryLocalNameKeyDto(relatedKey)));
        if (!result)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    private async Task<CountryLocalNameDto?> TryGetCountryLocalName(System.Int64 key, CountryLocalNameKeyDto childKeyDto)
    {
        var parent = await _mediator.Send(new GetCountryByIdQuery(key));
        return parent?.CountryLocalNames.SingleOrDefault(x => x.Id == childKeyDto.keyId);
    }
    
    [EnableQuery]
    public virtual async Task<ActionResult<CountryBarCodeDto>> GetCountryBarCode([FromRoute] System.Int64 key)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var item = await _mediator.Send(new GetCountryByIdQuery(key));
        
        if (item is null)
        {
            return NotFound();
        }
        
        return Ok(item.CountryBarCode);
    }
    
    public virtual async Task<ActionResult> PostToCountryBarCode([FromRoute] System.Int64 key, [FromBody] CountryBarCodeCreateDto countryBarCode)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var etag = Request.GetDecodedEtagHeader();
        var createdKey = await _mediator.Send(new AddCountryBarCodeCommand(new CountryKeyDto(key), countryBarCode, etag));
        if (createdKey == null)
        {
            return NotFound();
        }
        
        var child = (await _mediator.Send(new GetCountryByIdQuery(key)))?.CountryBarCode;
        if (child == null)
        {
            return NotFound();
        }
        
        return Created(child);
    }
    
    public virtual async Task<ActionResult<CountryBarCodeDto>> PutToCountryBarCode(System.Int64 key, [FromBody] CountryBarCodeUpdateDto countryBarCode)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var etag = Request.GetDecodedEtagHeader();
        var updatedKey = await _mediator.Send(new UpdateCountryBarCodeCommand(new CountryKeyDto(key), countryBarCode, etag));
        if (updatedKey == null)
        {
            return NotFound();
        }
        
        var child = (await _mediator.Send(new GetCountryByIdQuery(key)))?.CountryBarCode;
        if (child == null)
        {
            return NotFound();
        }
        
        return Ok(child);
    }
    
    public virtual async Task<ActionResult> PatchToCountryBarCode(System.Int64 key, [FromBody] Delta<CountryBarCodeDto> countryBarCode)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var updateProperties = new Dictionary<string, dynamic>();
        
        foreach (var propertyName in countryBarCode.GetChangedPropertyNames())
        {
            if(countryBarCode.TryGetPropertyValue(propertyName, out dynamic value))
            {
                updateProperties[propertyName] = value;                
            }           
        }
        
        var etag = Request.GetDecodedEtagHeader();
        var updated = await _mediator.Send(new PartialUpdateCountryBarCodeCommand(new CountryKeyDto(key), updateProperties, etag));
        
        if (updated is null)
        {
            return NotFound();
        }
        var child = (await _mediator.Send(new GetCountryByIdQuery(key)))?.CountryBarCode;
        if (child == null)
        {
            return NotFound();
        }
        
        return Ok(child);
    }
    
    [HttpDelete("api/Countries/{key}/CountryBarCode")]
    public virtual async Task<ActionResult> DeleteCountryBarCodeNonConventional(System.Int64 key)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _mediator.Send(new DeleteCountryBarCodeCommand(new CountryKeyDto(key)));
        if (!result)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    #endregion
    
}
