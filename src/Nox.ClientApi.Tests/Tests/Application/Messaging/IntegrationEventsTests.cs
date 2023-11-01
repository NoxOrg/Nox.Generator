﻿using AutoFixture;
using ClientApi.Application.Dto;
using ClientApi.Application.IntegrationEvents;
using ClientApi.Tests.Controllers;
using FluentAssertions;
using MassTransit.Testing;
using Nox.Infrastructure.Messaging;
using Nox.Types;
using Xunit.Abstractions;

namespace ClientApi.Tests.Application.Messaging
{
    [Collection("Sequential")]
    public class IntegrationEventsTests : NoxWebApiTestBase
    {
        private const string StoreOwnersControllerName = "api/storeowners";
        private const string CountriesControllerName = "api/countries";
        private const string WorkplacesControllerName = "api/workplaces";

        public IntegrationEventsTests(
            ITestOutputHelper testOutput,
            TestDatabaseContainerService containerService
            //For Development purposes
            //TestDatabaseInstanceService containerService
            )
           : base(testOutput, containerService, true) { }

        #region Integration Events

        [Fact]
        public async Task Post_StoreOwner_SendsCustomIntegrationEvent()
        {
            // Arrange
            var expectedVatNumber = "515714941";
            var createDto = new StoreOwnerCreateDto
            {
                Id = "002",
                Name = _fixture.Create<string>(),
                TemporaryOwnerName = "unknow",
                VatNumber = new VatNumberDto(expectedVatNumber, CountryCode.PT)
            };

            // Act
            var result = await PostAsync<StoreOwnerCreateDto, StoreOwnerDto>(StoreOwnersControllerName, createDto);

            //Assert
            result.Should().NotBeNull();

            (await MassTransitTestHarness.Published.Any<CloudEventMessage<ClientApi.Application.IntegrationEvents.StoreOwnerCreated>>()).Should().BeTrue();
        }

        [Fact]
        public async Task Post_Country_SendsIntegrationEvents()
        {
            // Arrange
            var createDto = new CountryCreateDto
            {
                Name = _fixture.Create<string>(),
                Population = 100_000_001,
            };

            // Act
            var result = await PostAsync<CountryCreateDto, CountryDto>(CountriesControllerName, createDto);

            //Assert
            result.Should().NotBeNull();

            MassTransitTestHarness.AssertAnyPublished<CountryPopulationHigherThan100M>();
        }

        [Fact]
        public async Task Put_Country_SendsIntegrationEvents()
        {
            // Arrange
            var createDto = new CountryCreateDto
            {
                Name = _fixture.Create<string>(),
                Population = 99_999_999,
            };

            var createResult = await PostAsync<CountryCreateDto, CountryDto>(CountriesControllerName, createDto);

            var updateDto = new CountryUpdateDto
            {
                Name = createDto.Name,
                Population = 100_000_001,
            };

            // Act
            var headers = CreateEtagHeader(createResult?.Etag);
            var updateResult = await PutAsync<CountryUpdateDto, CountryDto>($"{CountriesControllerName}/{createResult!.Id}", updateDto, headers);

            //Assert
            updateResult.Should().NotBeNull();

            MassTransitTestHarness.AssertAnyPublished<CountryCreated>();
            MassTransitTestHarness.AssertAnyPublished<CountryPopulationHigherThan100M>();
        }

        [Fact]
        public async Task Delete_Country_SendsIntegrationEvents()
        {
            // Arrange
            var createDto = new CountryCreateDto
            {
                Name = _fixture.Create<string>(),
                Population = 99_999_999,
            };

            var createResult = await PostAsync<CountryCreateDto, CountryDto>(CountriesControllerName, createDto);

            // Act
            var headers = CreateEtagHeader(createResult?.Etag);
            var deleteResult = await DeleteAsync($"{CountriesControllerName}/{createResult!.Id}", headers);

            //Assert
            deleteResult.Should().NotBeNull();
            // TODO: extend with event count so no additional events are sent, right now failing due to events from other test cases

            MassTransitTestHarness.AssertAnyPublished<CountryCreated>();
            MassTransitTestHarness.AssertAnyPublished<CountryUpdated>();
        }

        [Fact]
        public async Task Delete_Workplace_SendsIntegrationEvents()
        {
            // Arrange            
            var dto = new CountryCreateDto
            {
                Name = _fixture.Create<string>()
            };
                        
            var countryId = (await PostAsync<CountryCreateDto, CountryDto>(Endpoints.CountriesUrl, dto))!.Id;
            
            var createDto = new WorkplaceCreateDto
            {
                Name = _fixture.Create<string>(),
                BelongsToCountryId = countryId,
            };

            var createResult = await PostAsync<WorkplaceCreateDto, WorkplaceDto>(WorkplacesControllerName, createDto);

            // Act
            var headers = CreateEtagHeader(createResult?.Etag);
            var deleteResult = await DeleteAsync($"{WorkplacesControllerName}/{createResult!.Id}", headers);

            //Assert
            deleteResult.Should().NotBeNull();

            // TODO: extend with event count so no additional events are sent, right now failing due to events from other test cases
            MassTransitTestHarness.AssertAnyPublished<WorkplaceDeleted>();
        }

        [Fact(Skip ="Cloud event is being create in serialization phase, need to investigate how to assert the serialization")]
        public async Task Put_Country_SetsMessageValuesProperly()
        {
            // Arrange
            var createDto = new CountryCreateDto
            {
                Name = _fixture.Create<string>(),
                Population = 99_999_999,
            };

            var createResult = await PostAsync<CountryCreateDto, CountryDto>(CountriesControllerName, createDto);

            var updateDto = new CountryUpdateDto
            {
                Name = createDto.Name,
                Population = 100_000_001,
            };

            // Act
            var headers = CreateEtagHeader(createResult?.Etag);
            var updateResult = await PutAsync<CountryUpdateDto, CountryDto>($"{CountriesControllerName}/{createResult!.Id}", updateDto, headers);

            //Assert
            updateResult.Should().NotBeNull();

            //var events = MassTransitTestHarness.Published.S<CloudEventMessage>().Select(x => x.MessageObject as CloudEventMessage).ToArray();
            //var first = events[0];
            var events = MassTransitTestHarness.Sent.Select(x=>true).AsEnumerable().ToArray();
            //first.IntegrationEvent.source!.OriginalString.Should().Be("https://Nox-Tests.com/ClientApi");
            //((CloudEventMessage<CountryCreated>)messageObject).type.Should().Be("Nox-Tests.ClientApi.Country.v1.0.created");
            //((CloudEventMessage<CountryCreated>)messageObject).dataschema!.OriginalString.Should().Be("https://Nox-Tests.com/schemas/ClientApi/Country/v1.0/created.json");
        }

        #endregion Integration Events
    }
}