namespace Nox.Types.Tests.Types;
using FluentAssertions;
using System.Globalization;

public class DateTimeTests
{
    [Fact]
    public void From_WithValidDate_ReturnsValue()
    {
        var datetimeExpected = DateTimeOffset.Now;
        var datetime = DateTime.From(new DateTimeOffset(datetimeExpected.Year, datetimeExpected.Month, datetimeExpected.Day, 0, 0, 0, 0, TimeSpan.Zero));

        datetime.Value.Year.Should().Be(datetimeExpected.Year);
        datetime.Value.Month.Should().Be(datetimeExpected.Month);
        datetime.Value.Day.Should().Be(datetimeExpected.Day);
    }

    [Fact]
    public void From_AllowFutureOnly_Past_ThrowsException()
    {
        DateTimeTypeOptions dateTimeTypeOptions = new() { AllowFutureOnly = true };
        Action comparison = () => DateTime.From(new DateTimeOffset(2000, 01, 01, 0, 0, 0, 0, TimeSpan.Zero), dateTimeTypeOptions);

        comparison.Should().Throw<NoxTypeValidationException>();
    }

    [Fact]
    public void From_AllowFutureOnly_Now_ReturnsValue()
    {
        var datetimeNow = System.DateTime.Now.AddMinutes(1);
        DateTimeTypeOptions dateTimeTypeOptions = new() { AllowFutureOnly = true };
        var datetime = DateTime.From(new DateTimeOffset(datetimeNow), dateTimeTypeOptions);

        datetime.Value.Year.Should().Be(datetimeNow.Year);
        datetime.Value.Month.Should().Be(datetimeNow.Month);
        datetime.Value.Day.Should().Be(datetimeNow.Day);
        datetime.Value.Hour.Should().Be(datetimeNow.Hour);
        datetime.Value.Minute.Should().Be(datetimeNow.Minute);
        datetime.Value.Second.Should().Be(datetimeNow.Second);
    }

    [Fact]
    public void From_AllowFutureOnly_Future_ReturnsValue()
    {
        var datetimeNow = System.DateTime.Now.AddDays(1);
        DateTimeTypeOptions dateTimeTypeOptions = new() { AllowFutureOnly = true };
        var datetime = DateTime.From(new DateTimeOffset(datetimeNow), dateTimeTypeOptions);

        datetime.Value.Year.Should().Be(datetimeNow.Year);
        datetime.Value.Month.Should().Be(datetimeNow.Month);
        datetime.Value.Day.Should().Be(datetimeNow.Day);
        datetime.Value.Hour.Should().Be(datetimeNow.Hour);
        datetime.Value.Minute.Should().Be(datetimeNow.Minute);
        datetime.Value.Second.Should().Be(datetimeNow.Second);
    }

    [Fact]
    public void From_WithYearTypeOptions_InvalidValue_ThrowsException()
    {
        DateTimeTypeOptions dateTimeTypeOptions = new() { MaxValue = new DateTimeOffset(2022, 01, 01, 0, 0, 0, 0, TimeSpan.Zero) };
        Action comparison = () => DateTime.From(new DateTimeOffset(2023, 01, 01, 0, 0, 0, 0, TimeSpan.Zero), dateTimeTypeOptions);

        comparison.Should().Throw<NoxTypeValidationException>();
    }

    [Fact]
    public void From_WithValidDateTime_ReturnsValue()
    {
        var datetimeExpected = DateTimeOffset.Now;
        var datetime = DateTime.From(new DateTimeOffset(datetimeExpected.Year, datetimeExpected.Month, datetimeExpected.Day, datetimeExpected.Hour, datetimeExpected.Minute, datetimeExpected.Second, TimeSpan.Zero));

        datetime.Value.Year.Should().Be(datetimeExpected.Year);
        datetime.Value.Month.Should().Be(datetimeExpected.Month);
        datetime.Value.Day.Should().Be(datetimeExpected.Day);
        datetime.Value.Hour.Should().Be(datetimeExpected.Hour);
        datetime.Value.Minute.Should().Be(datetimeExpected.Minute);
        datetime.Value.Second.Should().Be(datetimeExpected.Second);
    }

    [Fact]
    public void From_SystemDateTime_ReturnsValue()
    {
        var datetimeExpected = DateTimeOffset.Now;
        var datetime = DateTime.From(datetimeExpected);

        datetime.Value.Year.Should().Be(datetimeExpected.Year);
        datetime.Value.Month.Should().Be(datetimeExpected.Month);
        datetime.Value.Day.Should().Be(datetimeExpected.Day);
        datetime.Value.Hour.Should().Be(datetimeExpected.Hour);
        datetime.Value.Minute.Should().Be(datetimeExpected.Minute);
        datetime.Value.Second.Should().Be(datetimeExpected.Second);
    }

    [Fact]
    public void From_ValidDateTimeString_ReturnsValue()
    {
        var datetimeExpected = DateTimeOffset.Now;
        var datetime = DateTime.From(datetimeExpected.ToString(CultureInfo.InvariantCulture));

        datetime.Value.Year.Should().Be(datetimeExpected.Year);
        datetime.Value.Month.Should().Be(datetimeExpected.Month);
        datetime.Value.Day.Should().Be(datetimeExpected.Day);
        datetime.Value.Hour.Should().Be(datetimeExpected.Hour);
        datetime.Value.Minute.Should().Be(datetimeExpected.Minute);
        datetime.Value.Second.Should().Be(datetimeExpected.Second);
    }

    [Fact]
    public void From_InValidDateTimeString_ThrowsException()
    {
        Action comparison = () => DateTime.From("2023-31-31");

        comparison.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void From_InValidDateTime_ThrowsException()
    {
        Action comparison = () => DateTime.From("2023/12/32");

        comparison.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void From_InValidYear_ThrowsException()
    {
        Action comparison = () => DateTime.From("0/12/32");

        comparison.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void From_InValidMonth_ThrowsException()
    {
        Action comparison = () => DateTime.From("2022/18/32");

        comparison.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void From_UsingSystemDateTime_ReturnsValue()
    {
        var datetimeExpected = System.DateTime.UtcNow;
        var datetime = DateTime.From(datetimeExpected);

        datetime.Value.Year.Should().Be(datetimeExpected.Year);
        datetime.Value.Month.Should().Be(datetimeExpected.Month);
        datetime.Value.Day.Should().Be(datetimeExpected.Day);
        datetime.Value.Offset.Should().Be(TimeSpan.Zero);
    }

    [Fact]
    public void OperatorPlus_ReturnsValue_TestCopy()
    {
        var datetimeOriginalExpected = new DateTimeOffset(System.DateTime.UtcNow);
        var datetimeOriginal = DateTime.From(new DateTimeOffset(System.DateTime.UtcNow));
        var timeSpan = TimeSpan.FromDays(20);

        var datetimeExpected = datetimeOriginalExpected + timeSpan;
        var datetime = datetimeOriginal + timeSpan;

        datetime.Value.Year.Should().Be(datetimeExpected.Year);
        datetime.Value.Month.Should().Be(datetimeExpected.Month);
        datetime.Value.Day.Should().Be(datetimeExpected.Day);
        datetime.Value.Hour.Should().Be(datetimeExpected.Hour);
        datetime.Value.Minute.Should().Be(datetimeExpected.Minute);
        datetime.Value.Second.Should().Be(datetimeExpected.Second);

        datetimeOriginal.Value.Year.Should().Be(datetimeOriginalExpected.Year);
        datetimeOriginal.Value.Month.Should().Be(datetimeOriginalExpected.Month);
        datetimeOriginal.Value.Day.Should().Be(datetimeOriginalExpected.Day);
        datetimeOriginal.Value.Hour.Should().Be(datetimeOriginalExpected.Hour);
        datetimeOriginal.Value.Minute.Should().Be(datetimeOriginalExpected.Minute);
        datetimeOriginal.Value.Second.Should().Be(datetimeOriginalExpected.Second);
    }

    [Fact]
    public void OperatorMinus_ReturnsValue_TestCopy()
    {

        var systemDatetime = new DateTimeOffset(System.DateTime.UtcNow);
        var timeSpan = TimeSpan.FromDays(20);
        var datetimeOriginal = DateTime.From(systemDatetime);
        var datetimeDaysAdded = DateTime.From(systemDatetime.Add(timeSpan));

        var timeSpanActual = datetimeDaysAdded - datetimeOriginal;

        timeSpanActual.Should().Be(timeSpan);
    }

    [Fact]
    public void EquationSigns_NotEqual_ReturnsValue()
    {

        var systemDatetime = new DateTimeOffset(System.DateTime.UtcNow);
        var timeSpan = TimeSpan.FromDays(20);
        var datetime = DateTime.From(systemDatetime);
        var datetimeDaysAdded = DateTime.From(systemDatetime.Add(timeSpan));

        (datetime < datetimeDaysAdded).Should().BeTrue();
        (datetimeDaysAdded < datetime).Should().BeFalse();
        (datetime <= datetimeDaysAdded).Should().BeTrue();
        (datetimeDaysAdded <= datetime).Should().BeFalse();
        (datetime > datetimeDaysAdded).Should().BeFalse();
        (datetimeDaysAdded > datetime).Should().BeTrue();
        (datetime <= datetimeDaysAdded).Should().BeTrue();
        (datetimeDaysAdded <= datetime).Should().BeFalse();
        (datetime >= datetimeDaysAdded).Should().BeFalse();
        (datetimeDaysAdded >= datetime).Should().BeTrue();
        (datetime != datetimeDaysAdded).Should().BeTrue();
    }

    [Fact]
    public void EquationSigns_Equal_ReturnsValue()
    {

        var systemDatetime = new DateTimeOffset(System.DateTime.UtcNow);
        var datetime = DateTime.From(systemDatetime);
        var datetime2 = DateTime.From(systemDatetime);

        (datetime < datetime2).Should().BeFalse();
        (datetime <= datetime2).Should().BeTrue();
        (datetime > datetime2).Should().BeFalse();
        (datetime >= datetime2).Should().BeTrue();
        (datetime == datetime2).Should().BeTrue();
        (datetime != datetime2).Should().BeFalse();
    }

    [Theory]
    [InlineData("en-GB", "d", "20/06/2023")]
    [InlineData("en-US", "d", "6/20/2023")]
    [InlineData("en-US", "dd/MM/yy", "20/06/23")]
    [InlineData("en-GB", "dd/MM/yy", "20/06/23")]
    [InlineData("en-GB", "dd MMM HH:mm:ss", "20 Jun 10:05:00")]
    public void ToString_WithCultureAndFormatParameters_ReturnsFormattedString(string culture, string format, string expectedResult)
    {
        var datetime = DateTime.From(new DateTimeOffset(2023, 6, 20, 10, 5, 0, 0, TimeSpan.Zero));

        var dateTimeString = datetime.ToString(format, new CultureInfo(culture));

        Assert.Equal(expectedResult, dateTimeString);
    }

    [Theory]
    [InlineData("en-GB", "06/20/2023 10:05:00 +00:00")]
    [InlineData("en-US", "06/20/2023 10:05:00 +00:00")]
    public void ToString_WithoutParameters_ReturnsFormattedStringInInvariantCulture(string culture, string expectedResult)
    {
        void Test()
        {
            var dateTime = DateTime.From(new DateTimeOffset(2023, 6, 20, 10, 5, 0, 0, TimeSpan.Zero));

            var dateTimeString = dateTime.ToString();

            Assert.Equal(expectedResult, dateTimeString);
        }

        TestUtility.RunInCulture(Test, culture);
    }

    [Theory]
    [InlineData("en-GB", "d", "06/20/2023")]
    [InlineData("en-US", "d", "06/20/2023")]
    [InlineData("en-US", "dd/MM/yy", "20/06/23")]
    [InlineData("en-GB", "dd/MM/yy", "20/06/23")]
    [InlineData("en-GB", "dd MMM HH:mm:ss", "20 Jun 10:05:00")]
    public void ToString_WithFormatParameter_ReturnsFormattedStringInInvariantCulture(string culture, string format, string expectedResult)
    {
        void Test()
        {
            var dateTime = DateTime.From(new DateTimeOffset(2023, 6, 20, 10, 5, 0, 0, TimeSpan.Zero));
            var dateTimeString = dateTime.ToString(format);

            Assert.Equal(expectedResult, dateTimeString);
        }

        TestUtility.RunInCulture(Test, culture);
    }

    [Theory]
    [InlineData("en-GB")]
    [InlineData("en-US")]
    public void ToString_WithCultureParameter_ReturnsFormattedString(string culture)
    {
        var dateTime = DateTime.From(new DateTimeOffset(2023, 6, 20, 10, 5, 0, 0, new TimeSpan(1,0,0)));

        var nativeFormattingOutput = new DateTimeOffset(2023, 6, 20, 10, 5, 0, 0, new TimeSpan(1, 0, 0)).ToString(new CultureInfo(culture));
        
        var dateTimeString = dateTime.ToString(new CultureInfo(culture));

        Assert.Equal(nativeFormattingOutput, dateTimeString);
    }

    [Fact]
    public void Equals_ReturnsTrue()
    {

        var systemDatetime = new DateTimeOffset(System.DateTime.Now);
        var datetime = DateTime.From(systemDatetime);
        var datetime2 = DateTime.From(systemDatetime);

        (datetime.Equals(datetime2)).Should().BeTrue();
    }

    [Fact]
    public void Equals_ReturnsFalse()
    {

        var systemDatetime = new DateTimeOffset(System.DateTime.UtcNow);
        var datetime = DateTime.From(systemDatetime);
        var datetime2 = DateTime.From(systemDatetime.AddDays(20));

        (datetime.Equals(datetime2)).Should().BeFalse();
    }

    [Fact]
    public void From_WithTimeSpan_ReturnsValue()
    {
        var datetimeParam = new System.DateTime(2023, 5, 1, 3, 0, 0, DateTimeKind.Unspecified);
        var datetimeExpected = new DateTimeOffset(datetimeParam, TimeSpan.Zero);
        var datetime = DateTime.From(datetimeParam, TimeSpan.FromHours(5));

        datetime.Value.Year.Should().Be(datetimeExpected.Year);
        datetime.Value.Month.Should().Be(datetimeExpected.Month);
        datetime.Value.Day.Should().Be(datetimeExpected.Day);
        datetime.Value.Hour.Should().Be(datetimeExpected.Hour);
        datetime.Value.Minute.Should().Be(datetimeExpected.Minute);
        datetime.Value.Second.Should().Be(datetimeExpected.Second);
        datetime.Value.Offset.Should().Be(TimeSpan.FromHours(5));
    }
}

