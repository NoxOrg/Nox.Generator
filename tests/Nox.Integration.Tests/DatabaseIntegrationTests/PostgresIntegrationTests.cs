using FluentAssertions;
using Nox.Types;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using TestWebApp.Domain;

using DayOfWeek = Nox.Types.DayOfWeek;
using Guid = Nox.Types.Guid;
using Nox.Integration.Tests.Fixtures;

namespace Nox.Integration.Tests.DatabaseIntegrationTests;

[Collection("Sequential")]
public class PostgresIntegrationTests : NoxIntegrationTestBase<NoxTestPostgreContainerFixture>
{
    public PostgresIntegrationTests(NoxTestPostgreContainerFixture containerFixture) : base(containerFixture)
    {
    }

    [Fact]
    public void GeneratedEntity_Postgres_CanSaveAndReadFields_AllTypes()
    {
        // TODO:
        // array
        // colour
        // collection
        // entity
        // formula
        // image
        // imagePng
        // imageJpg
        // imageSvg
        // object
        // languageCode
        // yaml
        // uri
        // dateTimeSchedule
        // json

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
            CountryId = Enum.Parse<CountryCode>(countryCode2),
            PostalCode = "61135"
        };
        var languageCode = "en";
        var year = (ushort)2023;
        var area = 198_090M;
        var areaPersistUnitAs = AreaTypeUnit.SquareMeter;
        var volume = 198d;
        var persistVolumeUnitAs = VolumeTypeUnit.CubicMeter;
        var cultureCode = "de-CH";
        var macAddress = "A1B2C3D4E5F6";
        var url = "http://example.com/";
        var guid = System.Guid.NewGuid();
        var password = "Test123.";
        ushort dayOfWeek = 1;
        byte month = 7;
        var currencyNumber = (short)970;
        var dateTimeDurationInHours = 30.5;
        var color = new byte[] { 255, 255, 0, 0 };
        var date = new DateOnly(2023, 7, 14);
        var time = new TimeOnly(11152500000);
        var percentage = 0.5f;
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
        var phoneNumber = "38761000000";

        using var aesAlgorithm = System.Security.Cryptography.Aes.Create();
        var encryptedTextTypeOptions = new EncryptedTextTypeOptions
        {
            PublicKey = Convert.ToBase64String(aesAlgorithm.Key),
            EncryptionAlgorithm = EncryptionAlgorithm.Aes,
            Iv = Convert.ToBase64String(aesAlgorithm.IV)
        };

        var internetDomain = "nox.org";
        var temperatureFahrenheit = 88;
        var temperaturePersistUnitAs = TemperatureTypeUnit.Celsius;
        var length = 314_598M;
        var persistLengthUnitAs = LengthTypeUnit.Meter;
        var sampleUri = "https://user:password@www.contoso.com:80/Home/Index.htm?q1=v1&q2=v2#FragmentName";
        var latitude = 47.376934;
        var longitude = 8.541287;

        var jwtToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";

        var weight = 20.58M;
        var persistWeightUnitAs = WeightTypeUnit.Kilogram;

        var distance = 80.481727;
        var persistDistanceUnitAs = DistanceTypeUnit.Kilometer;

        var dateTimeRangeStart = new DateTimeOffset(2023, 4, 12, 0, 0, 0, TimeSpan.FromHours(3));
        var dateTimeRangeEnd = new DateTimeOffset(2023, 7, 10, 0, 0, 0, TimeSpan.FromHours(5));
        var cronJobExpression = "0 0 12 ? * 2,3,4,5,6 *";
        var dateTime = new DateTimeOffset(2023, 7, 10, 0, 0, 0, TimeSpan.FromHours(5));

        var html = @"
<html>
    <body>
    Plain text
    <p> Paragraph text </p>
    </body>
</html>";

        var imageUrl = "https://example.com/image.png";
        var imagePrettyName = "Image";
        var imageSizeInBytes = 128;

        var newItem = new TestEntityForTypes()
        {
            Id = Text.From(countryCode2),
            TextTestField = Text.From(text),
            NumberTestField = Number.From(number),
            MoneyTestField = Money.From(money, currencyCode),
            CountryCode2TestField = CountryCode2.From(countryCode2),
            AreaTestField = Area.From(area, new AreaTypeOptions() { Units = AreaTypeUnit.SquareFoot, PersistAs = areaPersistUnitAs }),
            VolumeTestField = Volume.From(volume, new VolumeTypeOptions { Unit = VolumeTypeUnit.CubicMeter, PersistAs = persistVolumeUnitAs }),
            StreetAddressTestField = StreetAddress.From(addressItem),
            CurrencyCode3TestField = CurrencyCode3.From(currencyCode3),
            LanguageCodeTestField = LanguageCode.From(languageCode),
            YearTestField = Year.From(year),
            CultureCodeTestField = CultureCode.From(cultureCode),
            TranslatedTextTestField = TranslatedText.From((CultureCode.From("ur-PK"), "شادی مبارک")),
            CountryCode3TestField = CountryCode3.From(countryCode3),
            CountryNumberTestField = CountryNumber.From(242),
            TimeZoneCodeTestField = TimeZoneCode.From("utc"),
            MacAddressTestField = MacAddress.From(macAddress),
            UrlTestField = Url.From(url),
            UserTestField = User.From(email),
            GuidTestField = Guid.From(guid),
            HashedTextTestField = HashedText.From(text),
            PasswordTestField = Password.From(password),
            DayOfWeekTestField = DayOfWeek.From(1),
            MonthTestField = Month.From(month),
            CurrencyNumberTestField = CurrencyNumber.From(currencyNumber),
            DateTimeDurationTestField = DateTimeDuration.FromHours(dateTimeDurationInHours),
            TimeTestField = Time.From(time.Ticks),
            JsonTestField = Json.From(addressJsonPretty),
            BooleanTestField = Types.Boolean.From(boolean),
            EmailTestField = Email.From(email),
            YamlTestField = Yaml.From(switzerlandCitiesCountiesYaml),
            TemperatureTestField = Temperature.From(temperatureFahrenheit, new TemperatureTypeOptions() { Units = TemperatureTypeUnit.Fahrenheit, PersistAs = temperaturePersistUnitAs }),
            EncryptedTextTestField = EncryptedText.FromPlainText(text, encryptedTextTypeOptions),
            ColorTestField = Color.From(color[0], color[1], color[2], color[3]),
            PercentageTestField = Percentage.From(percentage),
            DateTestField = Date.From(date),
            MarkdownTestField = Markdown.From(text),
            FileTestField = Types.File.From(fileUrl, fileName, fileSizeInBytes),
            InternetDomainTestField = InternetDomain.From(internetDomain),
            LengthTestField = Length.From(length, new LengthTypeOptions() { Units = LengthTypeUnit.Foot, PersistAs = persistLengthUnitAs }),
            JwtTokenTestField = JwtToken.From(jwtToken),
            WeightTestField = Weight.From(weight, new WeightTypeOptions() { Units = WeightTypeUnit.Pound, PersistAs = persistWeightUnitAs }),
            DistanceTestField = Distance.From(distance, new DistanceTypeOptions() { Units = DistanceTypeUnit.Mile, PersistAs = persistDistanceUnitAs }),
            UriTestField = Types.Uri.From(sampleUri),
            GeoCoordTestField = LatLong.From(latitude, longitude),
            DateTimeRangeTestField = DateTimeRange.From(dateTimeRangeStart, dateTimeRangeEnd),
            HtmlTestField = Html.From(html),
            ImageTestField = Image.From(imageUrl, imagePrettyName, imageSizeInBytes),
            PhoneNumberTestField = PhoneNumber.From(phoneNumber),
            DateTimeScheduleTestField = DateTimeSchedule.From(cronJobExpression),
            DateTimeTestField = Types.DateTime.From(dateTime),
        };
        var temperatureCelsius = newItem.TemperatureTestField.ToCelsius();
        DataContext.TestEntityForTypes.Add(newItem);
        DataContext.SaveChanges();

        // Force the recreation of DBContext and ensure we have fresh data from database
        RecreateDataContext();

        var testEntity = DataContext.TestEntityForTypes.First();

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
        testEntity.VolumeTestField!.ToCubicMeters().Should().Be(volume);
        testEntity.VolumeTestField!.Unit.Should().Be(persistVolumeUnitAs);
        testEntity.CurrencyCode3TestField!.Value.Should().Be(currencyCode3);
        testEntity.LanguageCodeTestField!.Value.Should().Be(languageCode);
        testEntity.YearTestField!.Value.Should().Be(year);
        testEntity.CultureCodeTestField!.Value.Should().Be(cultureCode);
        testEntity.TranslatedTextTestField!.Value.Phrase.Should().BeEquivalentTo("شادی مبارک");
        testEntity.CountryCode3TestField!.Value.Should().Be(countryCode3);
        testEntity.CountryNumberTestField!.Value.Should().Be(242);
        testEntity.TimeZoneCodeTestField!.Value.Should().Be("UTC");
        testEntity.MacAddressTestField!.Value.Should().Be(macAddress);
        testEntity.UrlTestField!.Value.AbsoluteUri.Should().Be(url);
        testEntity.UserTestField!.Value.Should().Be(email);
        testEntity.GuidTestField!.Value.Should().Be(guid);
        testEntity.HashedTextTestField!.HashText.Should().Be(newItem.HashedTextTestField?.HashText);
        testEntity.HashedTextTestField!.Salt.Should().Be(newItem.HashedTextTestField?.Salt);
        testEntity.PasswordTestField!.HashedPassword.Should().Be(newItem.PasswordTestField.HashedPassword);
        testEntity.PasswordTestField!.Salt.Should().Be(newItem.PasswordTestField.Salt);
        testEntity.DayOfWeekTestField!.Value.Should().Be(dayOfWeek);
        testEntity.MonthTestField!.Value.Should().Be(month);
        testEntity.CurrencyNumberTestField!.Value.Should().Be(currencyNumber);
        testEntity.DateTimeDurationTestField!.TotalHours.Should().Be(dateTimeDurationInHours);
        testEntity.TimeTestField!.ToString("hh:mm").Should().Be(time.ToString("hh:mm"));
        testEntity.JsonTestField!.Value.Should().Be(addressJsonMinified);
        testEntity.JsonTestField!.ToString(string.Empty).Should().Be(addressJsonPretty);
        testEntity.JsonTestField!.ToString("p").Should().Be(addressJsonPretty);
        testEntity.JsonTestField!.ToString("m").Should().Be(addressJsonMinified);
        testEntity.BooleanTestField!.Value.Should().Be(boolean);
        testEntity.EmailTestField!.Value.Should().Be(email);
        testEntity.YamlTestField!.Value.Should().BeEquivalentTo(Yaml.From(switzerlandCitiesCountiesYaml).Value);
        testEntity.TemperatureTestField!.Value.Should().Be(temperatureCelsius);
        testEntity.TemperatureTestField!.ToFahrenheit().Should().Be(temperatureFahrenheit);
        testEntity.TemperatureTestField!.Unit.Should().Be(temperaturePersistUnitAs);
        testEntity.EncryptedTextTestField!.DecryptText(encryptedTextTypeOptions).Should().Be(text);
        testEntity.ColorTestField!.Value.Should().Be("#FFFF0000");
        testEntity.PercentageTestField!.Value.Should().Be(percentage);
        testEntity.DateTestField!.Value.Should().Be(date);
        testEntity.FileTestField!.Value.Url.Should().Be(fileUrl);
        testEntity.FileTestField!.Value.PrettyName.Should().Be(fileName);
        testEntity.FileTestField!.Value.SizeInBytes.Should().Be(fileSizeInBytes);
        testEntity.MarkdownTestField!.Value.Should().Be(text);
        testEntity.InternetDomainTestField!.Value.Should().Be(internetDomain);
        testEntity.LengthTestField!.ToFeet().Should().Be(length);
        testEntity.LengthTestField!.Unit.Should().Be(persistLengthUnitAs);
        testEntity.JwtTokenTestField!.Value.Should().Be(jwtToken);
        testEntity.WeightTestField!.Unit.Should().Be(persistWeightUnitAs);
        testEntity.WeightTestField!.ToPounds().Should().Be(weight);
        testEntity.DistanceTestField!.ToMiles().Should().Be(distance);
        testEntity.DistanceTestField!.Unit.Should().Be(persistDistanceUnitAs);
        testEntity.AutoNumberTestField!.Value.Should().BeGreaterThan(0);
        testEntity.UriTestField!.Value.Should().BeEquivalentTo(new System.Uri(sampleUri));
        testEntity.GeoCoordTestField!.Latitude.Should().Be(latitude);
        testEntity.GeoCoordTestField!.Longitude.Should().Be(longitude);
        testEntity.DateTimeRangeTestField!.Start.Should().Be(dateTimeRangeStart);
        testEntity.DateTimeRangeTestField!.End.Should().Be(dateTimeRangeEnd);
        testEntity.DateTimeRangeTestField!.Start.ToString().Should().Be(dateTimeRangeStart.ToString());
        testEntity.DateTimeRangeTestField!.End.ToString().Should().Be(dateTimeRangeEnd.ToString());
        testEntity.DateTimeRangeTestField!.Start.Offset.Should().Be(dateTimeRangeStart.Offset);
        testEntity.DateTimeRangeTestField!.End.Offset.Should().Be(dateTimeRangeEnd.Offset);
        testEntity.HtmlTestField!.Value.Should().Be(html);
        testEntity.ImageTestField!.Url.Should().Be(imageUrl);
        testEntity.ImageTestField!.PrettyName.Should().Be(imagePrettyName);
        testEntity.ImageTestField!.SizeInBytes.Should().Be(imageSizeInBytes);
        testEntity.PhoneNumberTestField!.Value.Should().Be(phoneNumber);
        testEntity.DateTimeScheduleTestField!.Value.Should().Be(cronJobExpression);
        //PostGres is always UTC
        testEntity.DateTimeTestField!.Value.Should().Be(dateTime.UtcDateTime);
        testEntity.DateTimeTestField!.Value.Offset.Should().Be(TimeSpan.Zero);
    }

    [Fact]
    public void UniqueConstraints_SameValue_ShouldThrowException()
    {
        const string countryCode2 = "UA";
        const string secondCountryCode2 = "TR";
        const string thirdCountryCode2 = "DE";
        const string currencyCode3 = "USD";
        const string secondCurrencyCode3 = "TRY";
        const int number = 123;
        const int secondNumber = 456;
        var testEntity1 = new TestEntityForUniqueConstraints()
        {
            Id = Text.From(countryCode2),
            TextField = Text.From("TestTextValue"),
            NumberField = Number.From(123),
            UniqueNumberField = Number.From(number),
            UniqueCountryCode = CountryCode2.From(countryCode2),
            UniqueCurrencyCode = CurrencyCode3.From(currencyCode3),
        };

        var testEntityWithSameUniqueNumber = new TestEntityForUniqueConstraints()
        {
            Id = Text.From(secondCountryCode2),
            TextField = Text.From("TestTextValue"),
            NumberField = Number.From(123),
            UniqueNumberField = Number.From(number),
            UniqueCountryCode = CountryCode2.From(secondCountryCode2),
            UniqueCurrencyCode = CurrencyCode3.From(secondCurrencyCode3),
        };

        var testEntityWithSameUniqueCountryCodeAndCurrencyCode = new TestEntityForUniqueConstraints()
        {
            Id = Text.From(thirdCountryCode2),
            TextField = Text.From("TestTextValue"),
            NumberField = Number.From(123),
            UniqueNumberField = Number.From(secondNumber),
            UniqueCountryCode = CountryCode2.From(countryCode2),
            UniqueCurrencyCode = CurrencyCode3.From(currencyCode3),
        };

        DataContext.TestEntityForUniqueConstraints.Add(testEntity1);
        DataContext.SaveChanges();

        DataContext.TestEntityForUniqueConstraints.Add(testEntityWithSameUniqueNumber);
        //save should throw exception
        Action act = () => DataContext.SaveChanges();
        act.Should().Throw<DbUpdateException>();

        DataContext.TestEntityForUniqueConstraints.Add(testEntityWithSameUniqueCountryCodeAndCurrencyCode);
        //save should throw exception
        Action act2 = () => DataContext.SaveChanges();
        act2.Should().Throw<DbUpdateException>();
    }
}