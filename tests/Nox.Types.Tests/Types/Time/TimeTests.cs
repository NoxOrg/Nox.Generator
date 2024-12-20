using FluentAssertions;
using System.Globalization;

namespace Nox.Types.Tests.Types;

/// <summary>
/// The time tests.
/// </summary>
public class TimeTests
{
    [Fact]
    public void From_HourAndMinutes_WithValidInputs_ReturnsValue()
    {
        int hour = 11;
        int minute = 22;

        var time = Time.From(hour, minute);

        time.Value.Hour.Should().Be(hour);
        time.Value.Minute.Should().Be(minute);
        time.Value.Second.Should().Be(0);
        time.Value.Millisecond.Should().Be(0);
    }

    [Fact]
    public void From_DateTime_WithValidInputs_ReturnsValue()
    {
        int hour = 11;
        int minute = 22;
        var input = new TimeOnly();
        input = input.AddHours(hour);
        input = input.AddMinutes(minute);

        var time = Time.From(input);

        time.Value.Hour.Should().Be(hour);
        time.Value.Minute.Should().Be(minute);
        time.Value.Second.Should().Be(0);
        time.Value.Millisecond.Should().Be(0);
    }

    [Fact]
    public void From_HourMinutesSeconds_WithValidInputs_ReturnsValue()
    {
        int hour = 11;
        int minute = 22;
        int seconds = 35;

        var time = Time.From(hour, minute, seconds);

        time.Value.Hour.Should().Be(hour);
        time.Value.Minute.Should().Be(minute);
        time.Value.Second.Should().Be(seconds);
        time.Value.Millisecond.Should().Be(0);
    }

    [Fact]
    public void From_HourMinutesSecondsMilliseconds_WithValidInputs_ReturnsValue()
    {
        int hour = 11;
        int minute = 22;
        int seconds = 35;
        int milliseconds = 947;
        var time = Time.From(hour, minute, seconds, milliseconds);

        time.Value.Hour.Should().Be(hour);
        time.Value.Minute.Should().Be(minute);
        time.Value.Second.Should().Be(seconds);
        time.Value.Millisecond.Should().Be(milliseconds);
    }

    [Fact]
    public void From_Ticks_WithValidInputs_ReturnsValue()
    {
        int hour = 3;
        int minute = 5;
        int seconds = 52;
        int milliseconds = 500;
        long ticks = 111525000000;
        var time = Time.From(ticks);

        time.Value.Hour.Should().Be(hour);
        time.Value.Minute.Should().Be(minute);
        time.Value.Second.Should().Be(seconds);
        time.Value.Millisecond.Should().Be(milliseconds);
    }

    [Fact]
    public void TimeTypeOptions_Constructor_ReturnsDefaults()
    {
        var timeTypeOptions = new TimeTypeOptions();

        timeTypeOptions.MinTimeTicks.Should().Be(0);
        timeTypeOptions.MaxTimeTicks.Should().Be(863_999_999_999);
    }


    [Fact]
    public void From_WithInvalidMinTime_ThrowsValidationException()
    {
        void Test()
        {
            int hour = 0;
            int minute = 0;
            int seconds = 0;
            int milliseconds = 100;
            var exception = Assert.Throws<NoxTypeValidationException>(() => _ =
              Time.From(hour, minute, seconds, milliseconds, new TimeTypeOptions { MinTimeTicks = 25000000 })
            );

            exception.Errors.First().ErrorMessage.Should().BeEquivalentTo($"Could not create a Nox Time type as value 1000000 is less than than the minimum specified value of 25000000.");
        }

        TestUtility.RunInCulture(Test, "en-GB");
    }

    [Fact]
    public void From_WithInvalidMaxTime_ThrowsValidationException()
    {
        void Test()
        {
            int hour = 25;
            int minute = 22;
            int seconds = 35;
            int milliseconds = 947;
            Assert.Throws<ArgumentOutOfRangeException>(() => _ =
              Time.From(hour, minute, seconds, milliseconds)
            );
        }

        TestUtility.RunInCulture(Test, "en-GB");
    }

    [Fact]
    public void From_WithInvalidMinTicks_ThrowsValidationException()
    {
        void Test()
        {
            long ticks = 100;
            var exception = Assert.Throws<NoxTypeValidationException>(() => _ =
              Time.From(ticks, new TimeTypeOptions { MinTimeTicks = 1000})
            );

            exception.Errors.First().ErrorMessage.Should().BeEquivalentTo($"Could not create a Nox Time type as value 100 is less than than the minimum specified value of 1000.");
        }

        TestUtility.RunInCulture(Test, "en-GB");
    }

    [Fact]
    public void From_WithInvalidMaxTicks_ThrowsArgumentOutOfRangeException()
    {
        void Test()
        {
            long ticks = 11111525000000;
            Assert.Throws<ArgumentOutOfRangeException>(() => _ =
                Time.From(ticks)
            );
        }

        TestUtility.RunInCulture(Test, "en-GB");
    }

    [Fact]
    public void From_WithNegativeTicks_ThrowsArgumentOutOfRangeException()
    {
        void Test()
        {
            long ticks = -11111525000000;
            Assert.Throws<ArgumentOutOfRangeException>(() => _ =
                Time.From(ticks)
            );
        }

        TestUtility.RunInCulture(Test, "en-GB");
    }

    [Fact]
    public void Equality_WithSameTimeConstructor_ShouldBeEquivalent()
    {
        int hour = 15;
        int minute = 22;
        int seconds = 35;
        int milliseconds = 947;
        var time1 = Time.From(hour, minute, seconds, milliseconds);

        var time2 = Time.From(hour, minute, seconds, milliseconds);

        time1.Should().BeEquivalentTo(time2);
    }

    [Fact]
    public void Equality_WithSameTickConstructor_ShouldBeEquivalent()
    {
        long ticks = 111525000000;
        var time1 = Time.From(ticks);

        var time2 = Time.From(ticks);

        time1.Should().BeEquivalentTo(time2);
    }

    [Fact]
    public void Equality_WithRegularAndTickConstructor_ShouldBeEquivalent()
    {
        int hour = 3;
        int minute = 5;
        int second = 52;
        int millisecond = 500;
        long ticks = 111525000000;
        var time1 = Time.From(hour, minute, second, millisecond);

        var time2 = Time.From(ticks);

        time1.Should().BeEquivalentTo(time2);
    }

    [Fact]
    public void ToString_WithTickConstructor_ReturnsNoFormattedStringInInvariantCulture()
    {
        var expectedResult = "03:05:52";
        void Test()
        {
            long ticks = 111525000000;

            var time = Time.From(ticks);

            var timeString = time.ToString();

            timeString.Should().BeEquivalentTo(expectedResult);
        }

        TestUtility.RunInInvariantCulture(Test);

    }

    [Theory]
    [InlineData("en-GB")]
    [InlineData("en-US")]
    public void ToString_WithTickConstructor_ReturnsDefaultFormattedStringInProvidedCulture(string culture)
    {
        void Test()
        {
            long ticks = 111525000000;

            var time = Time.From(ticks);

            var nativeFormattingOutput = new System.TimeOnly(ticks).ToString("T", new CultureInfo(culture));

            var timeString = time.ToString(new CultureInfo(culture));

            timeString.Should().BeEquivalentTo(nativeFormattingOutput);
        }

        TestUtility.RunInInvariantCulture(Test);

    }

    [Theory]
    [InlineData("en-US", "h:mm", "3:05")]
    [InlineData("en-US", "hh:mm", "03:05")]
    [InlineData("en-US", "HH:mm", "03:05")]
    [InlineData("en-US", "h:mm tt", "3:05 AM")]
    [InlineData("en-US", "hh:mm tt", "03:05 AM")]
    [InlineData("en-US", "HH:mm tt", "03:05 AM")]
    [InlineData("en-US", "HH:mm:ss", "03:05:52")]
    [InlineData("en-US", "hh:mm:ss", "03:05:52")]
    [InlineData("en-US", "hh:mm:ss fff", "03:05:52 500")]
    [InlineData("en-US", "HH:mm:ss fff", "03:05:52 500")]
    [InlineData("en-US", "hh:mm:ss fff tt", "03:05:52 500 AM")]
    [InlineData("en-US", "HH:mm:ss fff tt", "03:05:52 500 AM")]
    [InlineData("en-US", "t", "03:05")]
    [InlineData("en-US", "T", "03:05:52")]
    [InlineData("en-GB", "h:mm", "3:05")]
    [InlineData("en-GB", "hh:mm", "03:05")]
    [InlineData("en-GB", "HH:mm", "03:05")]
    [InlineData("en-GB", "h:mm tt", "3:05 AM")]
    [InlineData("en-GB", "hh:mm tt", "03:05 AM")]
    [InlineData("en-GB", "HH:mm tt", "03:05 AM")]
    [InlineData("en-GB", "HH:mm:ss", "03:05:52")]
    [InlineData("en-GB", "hh:mm:ss", "03:05:52")]
    [InlineData("en-GB", "hh:mm:ss fff", "03:05:52 500")]
    [InlineData("en-GB", "HH:mm:ss fff", "03:05:52 500")]
    [InlineData("en-GB", "hh:mm:ss fff tt", "03:05:52 500 AM")]
    [InlineData("en-GB", "HH:mm:ss fff tt", "03:05:52 500 AM")]
    [InlineData("en-GB", "t", "03:05")]
    [InlineData("en-GB", "T", "03:05:52")]
    public void ToString_WithTickConstructor_ReturnsFormattedStringInInvariantCulture(string culture, string format, string expectedResult)
    {
        void Test()
        {
            long ticks = 111525000000;

            var time = Time.From(ticks);

            var timeString = time.ToString(format);

            timeString.Should().BeEquivalentTo(expectedResult);
        }

        TestUtility.RunInCulture(Test, culture);
    }

    [Theory]
    [InlineData("en-US", "h:mm")]
    [InlineData("en-US", "hh:mm")]
    [InlineData("en-US", "HH:mm")]
    [InlineData("en-US", "h:mm tt")]
    [InlineData("en-US", "hh:mm tt")]
    [InlineData("en-US", "HH:mm tt")]
    [InlineData("en-US", "HH:mm:ss")]
    [InlineData("en-US", "hh:mm:ss")]
    [InlineData("en-US", "hh:mm:ss fff")]
    [InlineData("en-US", "HH:mm:ss fff")]
    [InlineData("en-US", "hh:mm:ss fff tt")]
    [InlineData("en-US", "HH:mm:ss fff tt")]
    [InlineData("en-US", "t")]
    [InlineData("en-US", "T")]
    [InlineData("en-GB", "h:mm")]
    [InlineData("en-GB", "hh:mm")]
    [InlineData("en-GB", "HH:mm")]
    [InlineData("en-GB", "h:mm tt")]
    [InlineData("en-GB", "hh:mm tt")]
    [InlineData("en-GB", "HH:mm tt")]
    [InlineData("en-GB", "HH:mm:ss")]
    [InlineData("en-GB", "hh:mm:ss")]
    [InlineData("en-GB", "hh:mm:ss fff")]
    [InlineData("en-GB", "HH:mm:ss fff")]
    [InlineData("en-GB", "hh:mm:ss fff tt")]
    [InlineData("en-GB", "HH:mm:ss fff tt")]
    [InlineData("en-GB", "t")]
    [InlineData("en-GB", "T")]
    public void ToString_WithTickConstructor_ReturnsFormattedStringInProvidedCulture(string culture, string format)
    {
        void Test()
        {
            long ticks = 111525000000;

            var time = Time.From(ticks);

            var nativeFormattingOutput = new System.TimeOnly(ticks).ToString(format, new CultureInfo(culture));

            var timeString = time.ToString(format, new CultureInfo(culture));

            timeString.Should().BeEquivalentTo(nativeFormattingOutput);
        }

        TestUtility.RunInCulture(Test, culture);
    }


    [Theory]
    [InlineData("en-GB", "03:05:52", 3)]
    [InlineData("en-US", "03:05:52", 3)]
    [InlineData("en-GB", "13:05:52", 13)]
    [InlineData("en-US", "13:05:52", 13)]
    public void ToString_WithoutParameters_ReturnsFormattedStringInInvariantCulture(string culture, string expectedResult, int hour)
    {
        void Test()
        {
            int minute = 5;
            int second = 52;
            int millisecond = 500;
            var time = Time.From(hour, minute, second, millisecond);

            var timeString = time.ToString();

            timeString.Should().BeEquivalentTo(expectedResult);
        }

        TestUtility.RunInCulture(Test, culture);
    }

    [Theory]
    [InlineData("en-GB")]
    [InlineData("en-US")]
    public void ToString_WithNoFormatParameter_ReturnsDefaultFormattedStringInProvidedCulture(string culture)
    {
        void Test()
        {
            int hour = 13;
            int minute = 5;
            int second = 52;
            int millisecond = 500;
            var time = Time.From(hour, minute, second, millisecond);

            var nativeFormattingOutput = new System.TimeOnly(hour, minute, second, millisecond).ToString("T", new CultureInfo(culture));

            var timeString = time.ToString(new CultureInfo(culture));

            timeString.Should().BeEquivalentTo(nativeFormattingOutput);
        }

        TestUtility.RunInCulture(Test, culture);
    }

    [Theory]
    [InlineData("en-US", "h:mm", "1:05")]
    [InlineData("en-US", "hh:mm", "01:05")]
    [InlineData("en-US", "HH:mm", "13:05")]
    [InlineData("en-US", "h:mm tt", "1:05 PM")]
    [InlineData("en-US", "hh:mm tt", "01:05 PM")]
    [InlineData("en-US", "HH:mm tt", "13:05 PM")]
    [InlineData("en-US", "HH:mm:ss", "13:05:52")]
    [InlineData("en-US", "hh:mm:ss", "01:05:52")]
    [InlineData("en-US", "hh:mm:ss fff", "01:05:52 500")]
    [InlineData("en-US", "HH:mm:ss fff", "13:05:52 500")]
    [InlineData("en-US", "hh:mm:ss fff tt", "01:05:52 500 PM")]
    [InlineData("en-US", "HH:mm:ss fff tt", "13:05:52 500 PM")]
    [InlineData("en-US", "t", "13:05")]
    [InlineData("en-US", "T", "13:05:52")]
    [InlineData("en-GB", "h:mm", "1:05")]
    [InlineData("en-GB", "hh:mm", "01:05")]
    [InlineData("en-GB", "HH:mm", "13:05")]
    [InlineData("en-GB", "h:mm tt", "1:05 PM")]
    [InlineData("en-GB", "hh:mm tt", "01:05 PM")]
    [InlineData("en-GB", "HH:mm tt", "13:05 PM")]
    [InlineData("en-GB", "HH:mm:ss", "13:05:52")]
    [InlineData("en-GB", "hh:mm:ss", "01:05:52")]
    [InlineData("en-GB", "hh:mm:ss fff", "01:05:52 500")]
    [InlineData("en-GB", "HH:mm:ss fff", "13:05:52 500")]
    [InlineData("en-GB", "hh:mm:ss fff tt", "01:05:52 500 PM")]
    [InlineData("en-GB", "HH:mm:ss fff tt", "13:05:52 500 PM")]
    [InlineData("en-GB", "t", "13:05")]
    [InlineData("en-GB", "T", "13:05:52")]
    public void ToString_WithFormatParameter_ReturnsFormattedStringInInvariantCulture(string culture, string format, string expectedResult)
    {
        void Test()
        {
            int hour = 13;
            int minute = 5;
            int second = 52;
            int millisecond = 500;
            var time = Time.From(hour, minute, second, millisecond);

            var timeString = time.ToString(format);

            timeString.Should().BeEquivalentTo(expectedResult);
        }

        TestUtility.RunInCulture(Test, culture);
    }

    [Theory]
    [InlineData("en-US", "h:mm")]
    [InlineData("en-US", "hh:mm")]
    [InlineData("en-US", "HH:mm")]
    [InlineData("en-US", "h:mm tt")]
    [InlineData("en-US", "hh:mm tt")]
    [InlineData("en-US", "HH:mm tt")]
    [InlineData("en-US", "HH:mm:ss")]
    [InlineData("en-US", "hh:mm:ss")]
    [InlineData("en-US", "hh:mm:ss fff")]
    [InlineData("en-US", "HH:mm:ss fff")]
    [InlineData("en-US", "hh:mm:ss fff tt")]
    [InlineData("en-US", "HH:mm:ss fff tt")]
    [InlineData("en-US", "t")]
    [InlineData("en-US", "T")]
    [InlineData("en-GB", "h:mm")]
    [InlineData("en-GB", "hh:mm")]
    [InlineData("en-GB", "HH:mm")]
    [InlineData("en-GB", "h:mm tt")]
    [InlineData("en-GB", "hh:mm tt")]
    [InlineData("en-GB", "HH:mm tt")]
    [InlineData("en-GB", "HH:mm:ss")]
    [InlineData("en-GB", "hh:mm:ss")]
    [InlineData("en-GB", "hh:mm:ss fff")]
    [InlineData("en-GB", "HH:mm:ss fff")]
    [InlineData("en-GB", "hh:mm:ss fff tt")]
    [InlineData("en-GB", "HH:mm:ss fff tt")]
    [InlineData("en-GB", "t")]
    [InlineData("en-GB", "T")]
    public void ToString_WithFormatParameter_ReturnsFormattedStringInProvidedCulture(string culture, string format)
    {
        void Test()
        {
            int hour = 13;
            int minute = 5;
            int second = 52;
            int millisecond = 500;
            var time = Time.From(hour, minute, second, millisecond);

            var nativeFormattingOutput = new System.TimeOnly(hour, minute, second, millisecond).ToString(format, new CultureInfo(culture));

            var timeString = time.ToString(format, new CultureInfo(culture));

            timeString.Should().BeEquivalentTo(nativeFormattingOutput);
        }

        TestUtility.RunInCulture(Test, culture);
    }
}
