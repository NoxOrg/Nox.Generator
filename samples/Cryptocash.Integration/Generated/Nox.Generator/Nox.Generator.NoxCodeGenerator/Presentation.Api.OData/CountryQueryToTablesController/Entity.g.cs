﻿// Generated

#nullable enable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Nox.Application;
using Nox.Extensions;

using System;
using System.Net.Http.Headers;
using CryptocashIntegration.Application;
using CryptocashIntegration.Application.Dto;
using CryptocashIntegration.Application.Queries;
using CryptocashIntegration.Application.Commands;
using CryptocashIntegration.Domain;
using CryptocashIntegration.Infrastructure.Persistence;

namespace CryptocashIntegration.Presentation.Api.OData;

public partial class CountryQueryToTablesController : CountryQueryToTablesControllerBase
{
    public CountryQueryToTablesController(
            IMediator mediator,
            Nox.Presentation.Api.IHttpLanguageProvider httpLanguageProvider
        ): base(mediator, httpLanguageProvider)
    {}
}

public abstract partial class CountryQueryToTablesControllerBase : ODataController
{
    /// <summary>
    /// The Mediator.
    /// </summary>
    protected readonly IMediator _mediator;

    /// <symmary>
    /// The Culture Code from the HTTP request.
    /// </symmary>
    protected readonly Nox.Types.CultureCode _cultureCode;

    public CountryQueryToTablesControllerBase(
        IMediator mediator,
        Nox.Presentation.Api.IHttpLanguageProvider httpLanguageProvider
    )
    {
        _mediator = mediator;
        _cultureCode = httpLanguageProvider.GetLanguage();
    }

    [EnableQuery]
    public virtual async Task<ActionResult<IQueryable<CountryQueryToTableDto>>> Get()
    {
        var result = await _mediator.Send(new GetCountryQueryToTablesQuery());
        return Ok(result);
    }

    [EnableQuery]
    public virtual async Task<SingleResult<CountryQueryToTableDto>> Get([FromRoute] System.Int32 key)
    {
        var result = await _mediator.Send(new GetCountryQueryToTableByIdQuery(key));
        return SingleResult.Create(result);
    }

    public virtual async Task<ActionResult<CountryQueryToTableDto>> Post([FromBody] CountryQueryToTableCreateDto countryQueryToTable)
    {
        if(countryQueryToTable is null)
        {
            throw new Nox.Exceptions.BadRequestInvalidFieldException();
        }
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }

        var createdKey = await _mediator.Send(new CreateCountryQueryToTableCommand(countryQueryToTable, _cultureCode));

        var item = (await _mediator.Send(new GetCountryQueryToTableByIdQuery(createdKey.keyId))).SingleOrDefault();

        return Created(item);
    }

    public virtual async Task<ActionResult<CountryQueryToTableDto>> Put([FromRoute] System.Int32 key, [FromBody] CountryQueryToTableUpdateDto countryQueryToTable)
    {
        if(countryQueryToTable is null)
        {
            throw new Nox.Exceptions.BadRequestInvalidFieldException();
        }
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }

        var etag = Request.GetDecodedEtagHeader();
        var updatedKey = await _mediator.Send(new UpdateCountryQueryToTableCommand(key, countryQueryToTable, _cultureCode, etag));

        if (updatedKey is null)
        {
            return NotFound();
        }

        var item = (await _mediator.Send(new GetCountryQueryToTableByIdQuery(updatedKey.keyId))).SingleOrDefault();

        return Ok(item);
    }

    public virtual async Task<ActionResult<CountryQueryToTableDto>> Patch([FromRoute] System.Int32 key, [FromBody] Delta<CountryQueryToTablePartialUpdateDto> countryQueryToTable)
    {
        if(countryQueryToTable is null)
        {
            throw new Nox.Exceptions.BadRequestInvalidFieldException();
        }
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }

        var updatedProperties = Nox.Presentation.Api.OData.ODataApi.GetDeltaUpdatedProperties<CountryQueryToTablePartialUpdateDto>(countryQueryToTable);

        var etag = Request.GetDecodedEtagHeader();
        var updatedKey = await _mediator.Send(new PartialUpdateCountryQueryToTableCommand(key, updatedProperties, _cultureCode, etag));

        var item = (await _mediator.Send(new GetCountryQueryToTableByIdQuery(updatedKey.keyId))).SingleOrDefault();

        return Ok(item);
    }

    public virtual async Task<ActionResult> Delete([FromRoute] System.Int32 key)
    {
        var etag = Request.GetDecodedEtagHeader();
        var result = await _mediator.Send(new DeleteCountryQueryToTableByIdCommand(new List<CountryQueryToTableKeyDto> { new CountryQueryToTableKeyDto(key) }, etag));

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}