using FluentAssertions;

using Microsoft.EntityFrameworkCore;

using Nox.Types;

using System.Text.Json;

using TestWebApp.Domain;

using DayOfWeek = Nox.Types.DayOfWeek;

namespace Nox.Tests.DatabaseIntegrationTests;

public class SqlServerIntegrationTests : SqlServerTestBase
{
    //[Fact]
    public void GeneratedEntity_SqlServer_CanSaveAndReadFields_AllTypes()
    {
        // TODO:
        // array
        // color
        // colour
        // databaseNumber
        // collection
        // entity
        // formula
        // image
        // imagePng
        // imageJpg
        // imageSvg
        // object
        // user
        // cultureCode
        // languageCode
        // yaml
        // uri
        // url
        // date
        // dateTimeSchedule
        // html
        // json
        // time
        // translatedText
        // markdown
        // jwtToken

        // TODO: commented types

        var text = "TestTextValue";
        var number = 123;
        var money = 10;
        var currencyCode = CurrencyCode.UAH;
        var countryCode2 = "UA";
        var currencyCode3 = "USD";
        var countryCode3 = "UKR";
        var addressItem = new StreetAddressItem
        {
            AddressLine1 = "AddressLine1",
            CountryId = CountryCode2.From(countryCode2),
            PostalCode = "61135"
        };
        var languageCode = "en";
        var area = 198_090M;
        var areaPersistUnitAs = AreaTypeUnit.SquareMeter;
        var cultureCode = "de-CH";
        var macAddress = "A1B2C3D4E5F6";
        var password = "Test123.";
        var dayOfWeek = 1;
        byte month = 7;
        var dateTimeDurationInHours = 30.5;
        var vatNumberValue = "44403198682";
        var vatNumberCountryCode2 = CountryCode2.From("FR");
        var date = new DateOnly(2023, 7, 14);
        var fileName = "MyFile";
        var fileSizeInBytes = 1000000UL;
        var fileUrl = "https://example.com/myfile.pdf";

        var addressJsonPretty = JsonSerializer.Serialize(addressItem, new JsonSerializerOptions { WriteIndented = true });
        var addressJsonMinified = JsonSerializer.Serialize(addressItem, new JsonSerializerOptions { AllowTrailingCommas = false, WriteIndented = false });
        var boolean = true;
        var email = "regus@regusignore.com";
        var switzerlandCitiesCountiesYaml = @"
- Zurich:
    - County: Zurich
    - County: Winterthur
    - County: Baden
- Geneva:
    - County: Geneva
    - County: Lausanne
";
        var internetDomain = "nox.org";

        var temperatureFahrenheit = 88;
        var temperaturePersistUnitAs = TemperatureTypeUnit.Celsius;

        var newItem = new TestEntityForTypes()
        {
            Id = Text.From(countryCode2),
            TextTestField = Text.From(text),
            NumberTestField = Number.From(number),
            MoneyTestField = Money.From(money, currencyCode),
            CountryCode2TestField = CountryCode2.From(countryCode2),
            AreaTestField = Area.From(area, new AreaTypeOptions() { Units = AreaTypeUnit.SquareFoot, PersistAs = areaPersistUnitAs }),
            StreetAddressTestField = StreetAddress.From(addressItem),
            CurrencyCode3TestField = CurrencyCode3.From(currencyCode3),
            LanguageCodeTestField = LanguageCode.From(languageCode),
            CultureCodeTestField = CultureCode.From(cultureCode),
            TranslatedTextTestField = TranslatedText.From((CultureCode.From("ur-PK"), "شادی مبارک")),
            CountryCode3TestField = CountryCode3.From(countryCode3),
            CountryNumberTestField = CountryNumber.From(242),
            TimeZoneCodeTestField = TimeZoneCode.From("utc"),
            MacAddressTestField = MacAddress.From(macAddress),
            HashedTextTestField = HashedText.From(text),
            PasswordTestField = Password.From(password),
            DayOfWeekTestField = DayOfWeek.From(1),
            MonthTestField = Month.From(month),
            DateTimeDurationTestField = DateTimeDuration.FromHours(dateTimeDurationInHours),
            JsonTestField = Json.From(addressJsonPretty),
            BooleanTestField = Types.Boolean.From(boolean),
            EmailTestField = Email.From(email),
            YamlTestField = Yaml.From(switzerlandCitiesCountiesYaml),
            VatNumberTestField = VatNumber.From(vatNumberValue, vatNumberCountryCode2),
            TempratureTestField = Temperature.From(temperatureFahrenheit, new TemperatureTypeOptions() { Units = TemperatureTypeUnit.Fahrenheit, PersistAs = temperaturePersistUnitAs }),
            DateTestField = Date.From(date),
            FileTestField = Types.File.From(fileUrl, fileName, fileSizeInBytes),
            MarkdownTestField = Markdown.From(text),
            InternetDomainTestField = InternetDomain.From(internetDomain),
        };
        var temperatureCelsius = newItem.TempratureTestField.ToCelsius();
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
        testEntity.AreaTestField!.Unit.Should().Be(areaPersistUnitAs);
        testEntity.CurrencyCode3TestField!.Value.Should().Be(currencyCode3);
        testEntity.LanguageCodeTestField!.Value.Should().Be(languageCode);
        testEntity.CultureCodeTestField!.Value.Should().Be(cultureCode);
        testEntity.TranslatedTextTestField!.Value.Phrase.Should().BeEquivalentTo("شادی مبارک");
        testEntity.CountryCode3TestField!.Value.Should().Be(countryCode3);
        testEntity.CountryNumberTestField!.Value.Should().Be(242);
        testEntity.TimeZoneCodeTestField!.Value.Should().Be("UTC");
        testEntity.MacAddressTestField!.Value.Should().Be(macAddress);
        testEntity.HashedTextTestField!.HashText.Should().Be(newItem.HashedTextTestField?.HashText);
        testEntity.HashedTextTestField!.Salt.Should().Be(newItem.HashedTextTestField?.Salt);
        testEntity.PasswordTestField!.HashedPassword.Should().Be(newItem.PasswordTestField.HashedPassword);
        testEntity.PasswordTestField!.Salt.Should().Be(newItem.PasswordTestField.Salt);
        testEntity.DayOfWeekTestField!.Value.Should().Be(dayOfWeek);
        testEntity.MonthTestField!.Value.Should().Be(month);
        testEntity.DateTimeDurationTestField!.TotalHours.Should().Be(dateTimeDurationInHours);
        testEntity.JsonTestField!.Value.Should().Be(addressJsonMinified);
        testEntity.JsonTestField!.ToString(string.Empty).Should().Be(addressJsonPretty);
        testEntity.JsonTestField!.ToString("p").Should().Be(addressJsonPretty);
        testEntity.JsonTestField!.ToString("m").Should().Be(addressJsonMinified);
        testEntity.BooleanTestField!.Value.Should().Be(boolean);
        testEntity.EmailTestField!.Value.Should().Be(email);
        testEntity.YamlTestField!.Value.Should().BeEquivalentTo(switzerlandCitiesCountiesYaml);
        testEntity.VatNumberTestField!.Value.Number.Should().Be(vatNumberValue);
        testEntity.VatNumberTestField!.Value.CountryCode2.Should().Be(vatNumberCountryCode2);
        testEntity.YamlTestField!.Value.Should().BeEquivalentTo(Yaml.From(switzerlandCitiesCountiesYaml).Value);
        testEntity.TempratureTestField!.Value.Should().Be(temperatureCelsius);
        testEntity.TempratureTestField!.ToFahrenheit().Should().Be(temperatureFahrenheit);
        testEntity.TempratureTestField!.Unit.Should().Be(temperaturePersistUnitAs);
        testEntity.DateTestField!.Value.Should().Be(date);
        testEntity.FileTestField!.Value.Url.Should().Be(fileUrl);
        testEntity.FileTestField!.Value.PrettyName.Should().Be(fileName);
        testEntity.FileTestField!.Value.SizeInBytes.Should().Be(fileSizeInBytes);
        testEntity.MarkdownTestField!.Value.Should().Be(text);
        testEntity.InternetDomainTestField!.Value.Should().BeEquivalentTo(internetDomain);
    }

    //[Fact]
    public void GeneratedRelationship_SqlServer_ZeroOrMany_OneOrMany()
    {
        var text = "TX";

        var newItem = new TestEntity()
        {
            Id = Text.From(text),
            TextTestField = Text.From(text),
        };
        DbContext.TestEntities.Add(newItem);
        DbContext.SaveChanges();

        var newItem2 = new SecondTestEntity()
        {
            Id = Text.From(text),
            TextTestField2 = Text.From(text),
        };

        newItem.SecondTestEntities.Add(newItem2);
        DbContext.SecondTestEntities.Add(newItem2);
        DbContext.SaveChanges();

        // Force the recreation of DBContext and ensure we have fresh data from database
        RecreateDbContext();

        var testEntity = DbContext.TestEntities.Include(x => x.SecondTestEntities).First();
        var secondTestEntity = DbContext.SecondTestEntities.Include(x => x.TestEntities).First();

        Assert.NotEmpty(testEntity.SecondTestEntities);
        Assert.NotEmpty(secondTestEntity.TestEntities);
    }

    //[Fact]
    public void GeneratedRelationship_SqlServer_OneOrMany_OneOrMany()
    {
        var text = "TX";

        var newItem = new TestEntityOneOrMany()
        {
            Id = Text.From(text),
            TextTestField = Text.From(text),
        };
        DbContext.TestEntityOneOrManies.Add(newItem);
        DbContext.SaveChanges();

        var newItem2 = new SecondTestEntityOneOrMany()
        {
            Id = Text.From(text),
            TextTestField2 = Text.From(text),
        };

        newItem.SecondTestEntityOneOrManies.Add(newItem2);
        DbContext.SecondTestEntityOneOrManies.Add(newItem2);
        DbContext.SaveChanges();

        // Force the recreation of DBContext and ensure we have fresh data from database
        RecreateDbContext();

        var testEntity = DbContext.TestEntityOneOrManies.Include(x => x.SecondTestEntityOneOrManies).First();
        var secondTestEntity = DbContext.SecondTestEntityOneOrManies.Include(x => x.TestEntityOneOrManies).First();

        Assert.NotEmpty(testEntity.SecondTestEntityOneOrManies);
        Assert.NotEmpty(secondTestEntity.TestEntityOneOrManies);
    }

    //[Fact]
    public void GeneratedRelationship_SqlServer_ExactlyOne_ExactlyOne()
    {
        var text = "TX";

        var newItem = new TestEntityExactlyOne()
        {
            Id = Text.From(text),
            TextTestField = Text.From(text),
        };
        var newItem2 = new SecondTestEntityExactlyOne()
        {
            Id = Text.From(text),
            TextTestField2 = Text.From(text),
        };

        newItem.SecondTestEntityExactlyOne = newItem2;
        DbContext.TestEntityExactlyOnes.Add(newItem);
        DbContext.SecondTestEntityExactlyOnes.Add(newItem2);
        DbContext.SaveChanges();

        // Force the recreation of DBContext and ensure we have fresh data from database
        RecreateDbContext();

        var testEntity = DbContext.TestEntityExactlyOnes.Include(x => x.SecondTestEntityExactlyOne).First();
        var secondTestEntity = DbContext.SecondTestEntityExactlyOnes.Include(x => x.TestEntityExactlyOne).First();

        Assert.NotNull(testEntity.SecondTestEntityExactlyOne);
        Assert.NotNull(secondTestEntity.TestEntityExactlyOne);
    }
}
