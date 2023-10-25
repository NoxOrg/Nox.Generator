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
using ClientApi.Application;
using ClientApi.Application.Dto;
using ClientApi.Application.Queries;
using ClientApi.Application.Commands;
using ClientApi.Domain;
using ClientApi.Infrastructure.Persistence;

using Nox.Types;
using Nox.Presentation.Api;

namespace ClientApi.Presentation.Api.OData;

public abstract partial class StoreLicensesControllerBase : ODataController
{
    /// <summary>
    /// The Mediator.
    /// </summary>
    protected readonly IMediator _mediator;

    /// <symmary>
    /// The HTTP language provider.
    /// </symmary>
    protected readonly IHttpLanguageProvider _languageProvider;

    protected StoreLicensesControllerBase(
        IMediator mediator,
        IHttpLanguageProvider languageProvider
    )
    {
        _mediator = mediator;
        _languageProvider = languageProvider;
    }

    [EnableQuery]
    public virtual async Task<ActionResult<IQueryable<StoreLicenseDto>>> Get()
    {
        var result = await _mediator.Send(new GetStoreLicensesQuery());
        return Ok(result);
    }

    [EnableQuery]
    public async Task<SingleResult<StoreLicenseDto>> Get([FromRoute] System.Int64 key)
    {
        var result = await _mediator.Send(new GetStoreLicenseByIdQuery(key));
        return SingleResult.Create(result);
    }

    public virtual async Task<ActionResult<StoreLicenseDto>> Post([FromBody] StoreLicenseCreateDto storeLicense)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var language = _languageProvider.GetLanguage();
        var createdKey = await _mediator.Send(new CreateStoreLicenseCommand(storeLicense, language));

        var item = (await _mediator.Send(new GetStoreLicenseByIdQuery(createdKey.keyId))).SingleOrDefault();

        return Created(item);
    }

    public virtual async Task<ActionResult<StoreLicenseDto>> Put([FromRoute] System.Int64 key, [FromBody] StoreLicenseUpdateDto storeLicense)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var etag = Request.GetDecodedEtagHeader();
        var updatedKey = await _mediator.Send(new UpdateStoreLicenseCommand(key, storeLicense, etag));

        if (updatedKey is null)
        {
            return NotFound();
        }

        var item = (await _mediator.Send(new GetStoreLicenseByIdQuery(updatedKey.keyId))).SingleOrDefault();

        return Ok(item);
    }

    public virtual async Task<ActionResult<StoreLicenseDto>> Patch([FromRoute] System.Int64 key, [FromBody] Delta<StoreLicenseDto> storeLicense)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedProperties = new Dictionary<string, dynamic>();

        foreach (var propertyName in storeLicense.GetChangedPropertyNames())
        {
            if (storeLicense.TryGetPropertyValue(propertyName, out dynamic value))
            {
                updatedProperties[propertyName] = value;
            }
        }

        var etag = Request.GetDecodedEtagHeader();
        var updatedKey = await _mediator.Send(new PartialUpdateStoreLicenseCommand(key, updatedProperties, etag));

        if (updatedKey is null)
        {
            return NotFound();
        }

        var item = (await _mediator.Send(new GetStoreLicenseByIdQuery(updatedKey.keyId))).SingleOrDefault();

        return Ok(item);
    }

    public virtual async Task<ActionResult> Delete([FromRoute] System.Int64 key)
    {
        var etag = Request.GetDecodedEtagHeader();
        var result = await _mediator.Send(new DeleteStoreLicenseByIdCommand(key, etag));

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}

public partial class StoreLicensesController : StoreLicensesControllerBase
{
    public StoreLicensesController(IMediator mediator, IHttpLanguageProvider languageProvider)
        : base(mediator, languageProvider)
    {}
}