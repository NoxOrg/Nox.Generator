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
using TestWebApp.Application;
using TestWebApp.Application.Dto;
using TestWebApp.Application.Queries;
using TestWebApp.Application.Commands;
using TestWebApp.Domain;
using TestWebApp.Infrastructure.Persistence;

namespace TestWebApp.Presentation.Api.OData;

public partial class TestEntityZeroOrOneToZeroOrManiesController : TestEntityZeroOrOneToZeroOrManiesControllerBase
{
    public TestEntityZeroOrOneToZeroOrManiesController(
            IMediator mediator,
            Nox.Presentation.Api.Providers.IHttpLanguageProvider httpLanguageProvider
        ): base(mediator, httpLanguageProvider)
    {}
}

public abstract partial class TestEntityZeroOrOneToZeroOrManiesControllerBase : ODataController
{
    /// <summary>
    /// The Mediator.
    /// </summary>
    protected readonly IMediator _mediator;

    /// <symmary>
    /// The Culture Code from the HTTP request.
    /// </symmary>
    protected readonly Nox.Types.CultureCode _cultureCode;

    public TestEntityZeroOrOneToZeroOrManiesControllerBase(
        IMediator mediator,
        Nox.Presentation.Api.Providers.IHttpLanguageProvider httpLanguageProvider
    )
    {
        _mediator = mediator;
        _cultureCode = httpLanguageProvider.GetLanguage();
    }

    [EnableQuery]
    public virtual async Task<ActionResult<IQueryable<TestEntityZeroOrOneToZeroOrManyDto>>> Get()
    {
        var result = await _mediator.Send(new GetTestEntityZeroOrOneToZeroOrManiesQuery());
        return Ok(result);
    }

    [EnableQuery]
    public virtual async Task<SingleResult<TestEntityZeroOrOneToZeroOrManyDto>> Get([FromRoute] System.String key)
    {
        var result = await _mediator.Send(new GetTestEntityZeroOrOneToZeroOrManyByIdQuery(key));
        return SingleResult.Create(result);
    }

    public virtual async Task<ActionResult<TestEntityZeroOrOneToZeroOrManyDto>> Post([FromBody] TestEntityZeroOrOneToZeroOrManyCreateDto testEntityZeroOrOneToZeroOrMany)
    {
        if(testEntityZeroOrOneToZeroOrMany is null)
        {
            throw new Nox.Exceptions.BadRequestInvalidFieldException();
        }
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }

        var createdKey = await _mediator.Send(new CreateTestEntityZeroOrOneToZeroOrManyCommand(testEntityZeroOrOneToZeroOrMany, _cultureCode));

        var item = (await _mediator.Send(new GetTestEntityZeroOrOneToZeroOrManyByIdQuery(createdKey.keyId))).SingleOrDefault();

        return Created(item);
    }

    public virtual async Task<ActionResult<TestEntityZeroOrOneToZeroOrManyDto>> Put([FromRoute] System.String key, [FromBody] TestEntityZeroOrOneToZeroOrManyUpdateDto testEntityZeroOrOneToZeroOrMany)
    {
        if(testEntityZeroOrOneToZeroOrMany is null)
        {
            throw new Nox.Exceptions.BadRequestInvalidFieldException();
        }
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }

        var etag = Request.GetDecodedEtagHeader();
        var updatedKey = await _mediator.Send(new UpdateTestEntityZeroOrOneToZeroOrManyCommand(key, testEntityZeroOrOneToZeroOrMany, _cultureCode, etag));

        var item = (await _mediator.Send(new GetTestEntityZeroOrOneToZeroOrManyByIdQuery(updatedKey.keyId))).SingleOrDefault();

        return Ok(item);
    }

    public virtual async Task<ActionResult<TestEntityZeroOrOneToZeroOrManyDto>> Patch([FromRoute] System.String key, [FromBody] Delta<TestEntityZeroOrOneToZeroOrManyPartialUpdateDto> testEntityZeroOrOneToZeroOrMany)
    {
        if(testEntityZeroOrOneToZeroOrMany is null)
        {
            throw new Nox.Exceptions.BadRequestInvalidFieldException();
        }
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }

        var updatedProperties = Nox.Presentation.Api.OData.ODataApi.GetDeltaUpdatedProperties<TestEntityZeroOrOneToZeroOrManyPartialUpdateDto>(testEntityZeroOrOneToZeroOrMany);

        var etag = Request.GetDecodedEtagHeader();
        var updatedKey = await _mediator.Send(new PartialUpdateTestEntityZeroOrOneToZeroOrManyCommand(key, updatedProperties, _cultureCode, etag));

        var item = (await _mediator.Send(new GetTestEntityZeroOrOneToZeroOrManyByIdQuery(updatedKey.keyId))).SingleOrDefault();

        return Ok(item);
    }

    public virtual async Task<ActionResult> Delete([FromRoute] System.String key)
    {
        var etag = Request.GetDecodedEtagHeader();
        var result = await _mediator.Send(new DeleteTestEntityZeroOrOneToZeroOrManyByIdCommand(new List<TestEntityZeroOrOneToZeroOrManyKeyDto> { new TestEntityZeroOrOneToZeroOrManyKeyDto(key) }, etag));

        return NoContent();
    }
}