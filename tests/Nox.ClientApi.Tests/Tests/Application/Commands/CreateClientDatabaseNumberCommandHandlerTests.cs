﻿using FluentAssertions;
using Nox.ClientApp.Tests.FixtureConfig;
using ClientApi.Application.Dto;
using ClientApi.Presentation.Api.OData;
using Microsoft.AspNetCore.OData.Results;
using AutoFixture;
using Microsoft.AspNetCore.Http.HttpResults;
using Nox.Types;
using Nox.ClientApi.Tests.Tests;

namespace Nox.ClientApi.Tests.Tests.Controllers
{
    [Collection("Sequential")]
    public class CreateClientDatabaseNumberCommandHandlerTests
    {
        /// <summary>
        /// Test a command extension for <see cref="CreateClientDatabaseNumberCommandHandler"/>
        /// For Request Validation, before command handler is executed use <see cref="IValidator"/> instead IValidator<CreateClientDatabaseNumberCommand>.
        /// </summary>        
        [Theory, AutoMoqData]        
        public async void Put_NumberNegative_ShouldUpdateTo0(ApiFixture apiFixture)
        {
            // Arrange            
            var expectedNumber = 00;

            // Act 
            var result = (CreatedODataResult<ClientDatabaseNumberKeyDto>)await apiFixture.ClientDatabaseNumbersController!.Post(
               new ClientDatabaseNumberCreateDto
               {
                   Name = apiFixture.Fixture.Create<string>(),
                   Number = -1
               });

            var queryResult = await apiFixture.ClientDatabaseNumbersController!.Get(result.Entity.keyId);

            //Assert
            
            queryResult.Should().NotBeNull();
            queryResult!.ToDto().Number.Should().Be(expectedNumber);
        }

    }
}
