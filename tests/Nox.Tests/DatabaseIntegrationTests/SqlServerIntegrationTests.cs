using FluentAssertions;
using Nox.Types;
using TestWebApp.Domain;

namespace Nox.Tests.DatabaseIntegrationTests;

public class SqlServerIntegrationTests : SqlServerTestBase
{
    // TODO: uncomment when automated and included into pipeline
    //[Fact]
    public void GeneratedEntity_Sqlite_CanSaveAndReadFields_AllTypes()
    {
        var text = "TestTextValue";
        var number = 123;
        var money = 10;
        var currencyCode = CurrencyCode.UAH;
        var countryCode2 = "UA";
        var currencyCode3 = "USD";
        var addressItem = new StreetAddressItem
        {
            AddressLine1 = "AddressLine1",
            CountryId = CountryCode2.From("UA"),
            PostalCode = "61135"
        };
        var date = new DateOnly(2023, 7, 14);
        var languageCode = "en";
        var area = 198_090M;
        var persistUnitAs = AreaTypeUnit.SquareMeter;
        var cultureCode = "de-CH";

        var newItem = new TestEntityForTypes()
        {
            Id = Text.From(countryCode2),
            TextTestField = Text.From(text),
            NumberTestField = Number.From(number),
            MoneyTestField = Money.From(money, currencyCode),
            CountryCode2TestField = CountryCode2.From(countryCode2),
            AreaTestField = Area.From(area, new AreaTypeOptions() { Units = AreaTypeUnit.SquareFoot, PersistAs = persistUnitAs }),
            StreetAddressTestField = StreetAddress.From(addressItem),
            CurrencyCode3TestField = CurrencyCode3.From(currencyCode3),
            DateTestField = Date.From(date),
            LanguageCodeTestField = LanguageCode.From(languageCode),
            CultureCodeTestField = CultureCode.From(cultureCode),
        };
        DbContext.TestEntityForTypes.Add(newItem);
        DbContext.SaveChanges();

        // Force the recreation of DBContext and ensure we have fresh data from database
        RecreateDbContext();

        var testEntity = DbContext.TestEntityForTypes.First();

        // TODO: make it work without .Value
        testEntity.Id.Value.Should().Be(countryCode2);
        testEntity.TextTestField.Value.Should().Be(text);
        testEntity.NumberTestField.Value.Should().Be(number);
        testEntity.MoneyTestField!.Value.Amount.Should().Be(money);
        testEntity.MoneyTestField.Value.CurrencyCode.Should().Be(currencyCode);
        testEntity.CountryCode2TestField!.Value.Should().Be(countryCode2);
        testEntity.StreetAddressTestField!.Value.Should().BeEquivalentTo(addressItem);
        testEntity.AreaTestField!.ToSquareFeet().Should().Be(area);
        testEntity.AreaTestField!.Unit.Should().Be(persistUnitAs);
        testEntity.CurrencyCode3TestField!.Value.Should().Be(currencyCode3);
        testEntity.DateTestField!.Value.Should().Be(date);
        testEntity.LanguageCodeTestField!.Value.Should().Be(languageCode);
        testEntity.CultureCodeTestField!.Value.Should().Be(cultureCode);
    }
}