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
using Nox.Exceptions;

using System;
using System.Net.Http.Headers;
using ClientApi.Application;
using ClientApi.Application.Dto;
using ClientApi.Application.Queries;
using ClientApi.Application.Commands;
using ClientApi.Domain;
using ClientApi.Infrastructure.Persistence;

namespace ClientApi.Presentation.Api.OData;

public partial class ReferenceNumberEntitiesController : ReferenceNumberEntitiesControllerBase
{
    public ReferenceNumberEntitiesController(
            IMediator mediator,
            Nox.Presentation.Api.Providers.IHttpLanguageProvider httpLanguageProvider
        ): base(mediator, httpLanguageProvider)
    {}
}

public abstract partial class ReferenceNumberEntitiesControllerBase : ODataController
{
    /// <summary>
    /// The Mediator.
    /// </summary>
    protected readonly IMediator _mediator;

    /// <symmary>
    /// The Culture Code from the HTTP request.
    /// </symmary>
    protected readonly Nox.Types.CultureCode _cultureCode;

    public ReferenceNumberEntitiesControllerBase(
        IMediator mediator,
        Nox.Presentation.Api.Providers.IHttpLanguageProvider httpLanguageProvider
    )
    {
        _mediator = mediator;
        _cultureCode = httpLanguageProvider.GetLanguage();
    }

    [EnableQuery]
    public virtual async Task<ActionResult<IQueryable<ReferenceNumberEntityDto>>> Get()
    {
        var result = await _mediator.Send(new GetReferenceNumberEntitiesQuery());
        return Ok(result);
    }

    [EnableQuery]
    public virtual async Task<SingleResult<ReferenceNumberEntityDto>> Get([FromRoute] System.String key)
    {
        var result = await _mediator.Send(new GetReferenceNumberEntityByIdQuery(key));
        return SingleResult.Create(result);
    }

    public virtual async Task<ActionResult<ReferenceNumberEntityDto>> Post([FromBody] ReferenceNumberEntityCreateDto referenceNumberEntity)
    {
        if(referenceNumberEntity is null)
        {
            throw new Nox.Exceptions.BadRequestInvalidFieldException();
        }
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }

        var createdKey = await _mediator.Send(new CreateReferenceNumberEntityCommand(referenceNumberEntity, _cultureCode));

        var item = (await _mediator.Send(new GetReferenceNumberEntityByIdQuery(createdKey.keyId))).SingleOrDefault();

        return Created(item);
    }

    public virtual async Task<ActionResult<ReferenceNumberEntityDto>> Put([FromRoute] System.String key, [FromBody] ReferenceNumberEntityUpdateDto referenceNumberEntity)
    {
        if(referenceNumberEntity is null)
        {
            throw new Nox.Exceptions.BadRequestInvalidFieldException();
        }
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }

        var etag = Request.GetDecodedEtagHeader();
        var updatedKey = await _mediator.Send(new UpdateReferenceNumberEntityCommand(key, referenceNumberEntity, _cultureCode, etag));

        var item = (await _mediator.Send(new GetReferenceNumberEntityByIdQuery(updatedKey.keyId))).SingleOrDefault();

        return Ok(item);
    }

    public virtual async Task<ActionResult<ReferenceNumberEntityDto>> Patch([FromRoute] System.String key, [FromBody] Delta<ReferenceNumberEntityPartialUpdateDto> referenceNumberEntity)
    {
        if(referenceNumberEntity is null)
        {
            throw new Nox.Exceptions.BadRequestInvalidFieldException();
        }
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }

        var updatedProperties = Nox.Presentation.Api.OData.ODataApi.GetDeltaUpdatedProperties<ReferenceNumberEntityPartialUpdateDto>(referenceNumberEntity);

        var etag = Request.GetDecodedEtagHeader();
        var updatedKey = await _mediator.Send(new PartialUpdateReferenceNumberEntityCommand(key, updatedProperties, _cultureCode, etag));

        var item = (await _mediator.Send(new GetReferenceNumberEntityByIdQuery(updatedKey.keyId))).SingleOrDefault();

        return Ok(item);
    }

    public virtual async Task<ActionResult> Delete([FromRoute] System.String key)
    {
        var etag = Request.GetDecodedEtagHeader();
        var result = await _mediator.Send(new DeleteReferenceNumberEntityByIdCommand(new List<ReferenceNumberEntityKeyDto> { new ReferenceNumberEntityKeyDto(key) }, etag));

        return NoContent();
    }
}