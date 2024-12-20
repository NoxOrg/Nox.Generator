using FluentAssertions;
using FluentAssertions.Execution;

using System.Globalization;

namespace Nox.Types.Tests.Types;

public class DateTimeRangeTests
{
    [Fact]
    public void DateTimeRangeTypeOptions_Constructor_ReturnsDefaultValues()
    {
        var typeOptions = new DateTimeRangeTypeOptions();

        typeOptions.MinStartValue.Should().Be(new DateTimeOffset(1800, 1, 1, 0, 0, 0, TimeSpan.Zero));
        typeOptions.MaxEndValue.Should().Be(new DateTimeOffset(3000, 12, 31, 0, 0, 0, TimeSpan.Zero));
    }

    [Fact]
    public void From_WithValidStartAndEnd_ReturnsValue()
    {
        var start = new DateTimeOffset(System.DateTime.UtcNow);
        var end = start.AddDays(1);

        var dateTimeRange = DateTimeRange.From(start, end);

        start.Should().Be(dateTimeRange.Start);
        dateTimeRange.End.Should().Be(end);
        (dateTimeRange.Start, dateTimeRange.End).Should().Be((start, end));
    }

    [Fact]
    public void From_WithSameStartAndEnd_ReturnsValue()
    {
        var start = new DateTimeOffset(System.DateTime.UtcNow);
        var end = start;

        var dateTimeRange = DateTimeRange.From(start, end);

        dateTimeRange.Start.Should().Be(start);
        dateTimeRange.End.Should().Be(end);
        (dateTimeRange.Start, dateTimeRange.End).Should().Be((start, end));
    }

    [Fact]
    public void From_WithTimeSpan_ReturnsValue()
    {
        var start = new DateTimeOffset(System.DateTime.UtcNow);
        var timeSpan = TimeSpan.FromDays(20);

        var dateTimeRange = DateTimeRange.From(start, timeSpan);

        dateTimeRange.Start.Should().Be(start);
        dateTimeRange.End.Should().Be(start + timeSpan);
        (dateTimeRange.Start, dateTimeRange.End).Should().Be((start, start + timeSpan));
    }

    [Theory]
    [InlineData("2023-05-02T00:00:00+00:00", "2023-05-01T00:00:00+00:00", "05/02/2023 00:00:00 +00:00", "05/01/2023 00:00:00 +00:00")] // Different date, no tz offset
    [InlineData("2023-05-01T00:00:00+01:00", "2023-05-01T00:00:00+02:00", "05/01/2023 00:00:00 +01:00", "05/01/2023 00:00:00 +02:00")] // Same date, end is in lower tz offset
    public void From_WithEndBeforeStart_ThrowsValidationException(string startDateTimeString, string endDateTimeString, string expectedStartStringOutput, string expectedEndStringOutput)
    {
        void Test()
        {
            var start = DateTimeOffset.Parse(startDateTimeString, CultureInfo.InvariantCulture);
            var end = DateTimeOffset.Parse(endDateTimeString, CultureInfo.InvariantCulture);

            var action = () => DateTimeRange.From(start, end);

            action.Should().Throw<NoxTypeValidationException>()
                .And.Errors.Should().BeEquivalentTo(new[] { new ValidationFailure("Value", $"Could not create a Nox DateTimeRange type with Start value {expectedStartStringOutput} and End value {expectedEndStringOutput} as start of the time range must be the same or before the end of the time range.") });
        }

        TestUtility.RunInCulture(Test, "en-GB");
    }

    [Theory]
    [InlineData("2023-05-02T00:00:00+00:00", "05/02/2023 00:00:00 +00:00")] // Date less than minimum, no tz offset
    [InlineData("2025-01-01T00:00:00+01:00", "01/01/2025 00:00:00 +01:00")] // Same date, but tz offset is before minimum
    public void From_WithStartLessThanMinStartValue_ThrowsValidationException(string startDateTimeString, string expectedEndStringOutput)
    {
        void Test()
        {
            var start = DateTimeOffset.Parse(startDateTimeString, CultureInfo.InvariantCulture);
            var action = () => DateTimeRange.From(
                start,
                new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero),
                new DateTimeRangeTypeOptions { MinStartValue = new DateTimeOffset(2025, 01, 01, 0, 0, 0, 0, TimeSpan.Zero)}
            );

            action.Should().Throw<NoxTypeValidationException>()
                .And.Errors.Should().BeEquivalentTo(new[] { new ValidationFailure("Start", $"Could not create a Nox DateTimeRange type as Start value {expectedEndStringOutput} is less than than the minimum specified value of 01/01/2025 00:00:00 +00:00.") });
        }

        TestUtility.RunInCulture(Test, "en-GB");
    }

    [Theory]
    [InlineData("2026-05-02T00:00:00+00:00", "05/02/2026 00:00:00 +00:00")] // Date greater than maximum, no tz offset
    [InlineData("2025-01-01T00:00:00-01:00", "01/01/2025 00:00:00 -01:00")] // Same date, but tz offset is after maximum
    public void From_WithEndGreaterThanMaxEndValue_ThrowsValidationException(string endDateTimeString, string expectedEndStringOutput)
    {
        void Test()
        {
            var end = DateTimeOffset.Parse(endDateTimeString, CultureInfo.InvariantCulture);
            var action = () => DateTimeRange.From(
                new DateTimeOffset(2021, 1, 1, 0, 0, 0, TimeSpan.Zero), 
                end,
                new DateTimeRangeTypeOptions { MaxEndValue = new DateTimeOffset(2025, 01, 01, 0, 0, 0, 0, TimeSpan.Zero) }
            );

            action.Should().Throw<NoxTypeValidationException>()
                .And.Errors.Should().BeEquivalentTo(new[] { new ValidationFailure("End", $"Could not create a Nox DateTimeRange type as End value {expectedEndStringOutput} is greater than than the maximum specified value of 01/01/2025 00:00:00 +00:00.") });
        }

        TestUtility.RunInCulture(Test, "en-GB");
    }

    [Theory]
    [InlineData("2023-05-01T00:00:00+00:00", "2023-06-01T00:00:00+00:00", "2023-05-01T00:00:00+00:00", "2023-06-01T00:00:00+00:00")] // Same date, no tz offset
    [InlineData("2023-05-01T01:00:00+02:00", "2023-06-01T00:00:00+00:00", "2023-05-01T00:00:00+01:00", "2023-06-01T00:00:00+00:00")] // Same date, start has different time, but compensated with different offset
    [InlineData("2023-05-01T00:00:00+00:00", "2023-06-01T01:00:00+02:00", "2023-05-01T00:00:00+00:00", "2023-06-01T00:00:00+01:00")] // Same date, end has different time, but compensated with different offset
    public void Equality_WithSameStartAndEnd_ShouldBeEquivalent(string startDateTimeString1, string endDateTimeString1, string startDateTimeString2, string endDateTimeString2)
    {
        var start1 = DateTimeOffset.Parse(startDateTimeString1, CultureInfo.InvariantCulture);
        var end1 = DateTimeOffset.Parse(endDateTimeString1, CultureInfo.InvariantCulture);

        var start2 = DateTimeOffset.Parse(startDateTimeString2, CultureInfo.InvariantCulture);
        var end2 = DateTimeOffset.Parse(endDateTimeString2, CultureInfo.InvariantCulture);

        var dateTimeRange1 = DateTimeRange.From(start1, end1);
        var dateTimeRange2 = DateTimeRange.From(start2, end2);

        AssertAreEquivalent(dateTimeRange1, dateTimeRange2);
    }

    [Fact]
    public void Equality_WithRegularAndWithTimeSpanConstructors_ShouldBeEquivalent()
    {
        var start = new DateTimeOffset(2023, 06, 15, 1, 2, 3, TimeSpan.Zero);
        var end = start.AddDays(1);
        var timeSpan = TimeSpan.FromDays(1);

        var dateTimeRange1 = DateTimeRange.From(start, end);
        var dateTimeRange2 = DateTimeRange.From(start, timeSpan);

        AssertAreEquivalent(dateTimeRange1, dateTimeRange2);
    }

    [Fact]
    public void Equality_WithRegularAndWithTupleConstructors_ShouldBeEquivalent()
    {
        var start = new DateTimeOffset(2023, 06, 15, 1, 2, 3, TimeSpan.Zero);
        var end = start.AddDays(1);

        var dateTimeRange1 = DateTimeRange.From(start, end);
        var dateTimeRange2 = DateTimeRange.From((start, end));

        AssertAreEquivalent(dateTimeRange1, dateTimeRange2);
    }

    [Theory]
    [InlineData("2021-01-01 00:00 +00:00", "2021-05-01 00:00 +00:00", "2023-01-01 00:00 +00:00", "2023-05-01 00:00 +00:00")] // Different dates
    [InlineData("2021-01-01 00:00 +02:00", "2021-05-01 00:00 +00:00", "2021-01-01 00:00 +03:00", "2021-05-01 00:00 +00:00")] // Same date and time, starts have different tz offsets
    [InlineData("2021-01-01 00:00 +00:00", "2021-05-01 00:00 +07:00", "2021-01-01 00:00 +00:00", "2021-05-01 00:00 +00:00")]// Same date and time, end have different tz offsets
    public void NonEquality_WithDifferentStartAndEnd_ShouldNotBeEquivalent(string startStr1, string endStr1, string startStr2, string endStr2)
    {
        var start1 = DateTimeOffset.Parse(startStr1, CultureInfo.InvariantCulture);
        var end1 = DateTimeOffset.Parse(endStr1, CultureInfo.InvariantCulture);

        var start2 = DateTimeOffset.Parse(startStr2, CultureInfo.InvariantCulture);
        var end2 = DateTimeOffset.Parse(endStr2, CultureInfo.InvariantCulture);

        var dateTimeRange1 = DateTimeRange.From(start1, end1);
        var dateTimeRange2 = DateTimeRange.From(start2, end2);

        AssertAreNotEquivalent(dateTimeRange1, dateTimeRange2);
    }

    [Fact]
    public void NonEquality_WithRegularAndWithTimeSpanConstructors_ShouldNotBeEquivalent()
    {
        var start = new DateTimeOffset(System.DateTime.UtcNow);
        var end = start.AddDays(1);
        var timeSpan = TimeSpan.FromDays(3);

        var dateTimeRange1 = DateTimeRange.From(start, end);
        var dateTimeRange2 = DateTimeRange.From(start, timeSpan);

        AssertAreNotEquivalent(dateTimeRange1, dateTimeRange2);
    }

    [Theory]
    [InlineData("2021-01-01 00:00 +00:00", "2021-05-01 00:00 +00:00", "2023-01-01 00:00 +00:00", "2023-05-01 00:00 +00:00")] // Different dates
    [InlineData("2021-01-01 00:00 +02:00", "2021-05-01 00:00 +00:00", "2021-01-01 00:00 +03:00", "2021-05-01 00:00 +00:00")] // Same date and time, starts have different tz offsets
    [InlineData("2021-01-01 00:00 +00:00", "2021-05-01 00:00 +07:00", "2021-01-01 00:00 +00:00", "2021-05-01 00:00 +00:00")] // Same date and time, end have different tz offsets
    public void NonEquality_WithRegularAndWithTupleConstructors_ShouldNotBeEquivalent(string startStr1, string endStr1, string startStr2, string endStr2)
    {
        var start1 = DateTimeOffset.Parse(startStr1, CultureInfo.InvariantCulture);
        var end1 = DateTimeOffset.Parse(endStr1, CultureInfo.InvariantCulture);

        var start2 = DateTimeOffset.Parse(startStr2, CultureInfo.InvariantCulture);
        var end2 = DateTimeOffset.Parse(endStr2, CultureInfo.InvariantCulture);

        var dateTimeRange1 = DateTimeRange.From(start1, end1);
        var dateTimeRange2 = DateTimeRange.From((start2, end2));

        AssertAreNotEquivalent(dateTimeRange1, dateTimeRange2);
    }

    [Fact]
    public void Duration_WithValidStartAndEnd_ReturnsValue()
    {
        var start = new DateTimeOffset(2023, 01, 01, 0, 0, 0, TimeSpan.Zero);
        var end = new DateTimeOffset(2023, 01, 20, 0, 0, 0, TimeSpan.FromHours(5));

        var dateTimeRange = DateTimeRange.From(start, end);

        dateTimeRange.Duration.TotalHours.Should().Be((19 * 24) - 5);
    }

    [Theory]
    [InlineData("2023-01-01T00:00Z")]
    [InlineData("2024-01-01T00:00Z")]
    [InlineData("2024-01-01T05:00+08:00")]
    [InlineData("2023-06-06T00:00Z")]
    public void Contains_WithDateWithinTheRange_ReturnsTrue(string dateTimeStr)
    {
        var dateTime = DateTimeOffset.Parse(dateTimeStr, CultureInfo.InvariantCulture);

        var start = new DateTimeOffset(2023, 01, 01, 0, 0, 0, TimeSpan.Zero);
        var end = new DateTimeOffset(2024, 01, 01, 0, 0, 0, TimeSpan.Zero);

        var dateTimeRange = DateTimeRange.From(start, end);

        dateTimeRange.Contains(dateTime).Should().BeTrue();
    }

    [Theory]
    [InlineData("2022-12-31T00:00Z")]
    [InlineData("2024-01-02T00:00Z")]
    [InlineData("2023-12-31T23:00-08:00")]
    [InlineData("2021-06-06T00:00Z")]
    public void Contains_WithDateOutsideTheRange_ReturnsFalse(string dateTimeStr)
    {
        var dateTime = DateTimeOffset.Parse(dateTimeStr, CultureInfo.InvariantCulture);

        var start = new DateTimeOffset(2023, 01, 01, 0, 0, 0, TimeSpan.Zero);
        var end = new DateTimeOffset(2024, 01, 01, 0, 0, 0, TimeSpan.Zero);

        var dateTimeRange = DateTimeRange.From(start, end);

        dateTimeRange.Contains(dateTime).Should().BeFalse();
    }

    [Theory]
    [InlineData("2023-01-01 00:00 +00:00", "2023-06-01 00:00 +00:00", "2023-02-01 00:00 +00:00", "2023-04-01 00:00 +00:00", "2023-02-01 00:00 +00:00", "2023-04-01 00:00 +00:00")]
    [InlineData("2023-01-01 00:00 +00:00", "2023-04-01 00:00 +00:00", "2023-02-01 00:00 +00:00", "2023-06-01 00:00 +00:00", "2023-02-01 00:00 +00:00", "2023-04-01 00:00 +00:00")]
    [InlineData("2023-02-01 00:00 +00:00", "2023-06-01 00:00 +00:00", "2023-01-01 00:00 +00:00", "2023-04-01 00:00 +00:00", "2023-02-01 00:00 +00:00", "2023-04-01 00:00 +00:00")]
    [InlineData("2023-01-01 00:00 +00:00", "2023-02-01 00:00 +00:00", "2023-02-01 00:00 +00:00", "2023-04-01 00:00 +00:00", "2023-02-01 00:00 +00:00", "2023-02-01 00:00 +00:00")]
    [InlineData("2023-01-01 00:00 +01:00", "2023-04-01 00:00 +02:00", "2023-02-01 00:00 +03:00", "2023-06-01 00:00 +04:00", "2023-02-01 00:00 +03:00", "2023-04-01 00:00 +02:00")]
    public void Intersect_WithOverlappingRanges_ReturnsIntersectedDateTimeRange(string startStr1, string endStr1, string startStr2, string endStr2, string expectedStartStr, string expectedEndStr)
    {
        var start1 = DateTimeOffset.Parse(startStr1, CultureInfo.InvariantCulture);
        var end1 = DateTimeOffset.Parse(endStr1, CultureInfo.InvariantCulture);

        var start2 = DateTimeOffset.Parse(startStr2, CultureInfo.InvariantCulture);
        var end2 = DateTimeOffset.Parse(endStr2, CultureInfo.InvariantCulture);

        var expectedStart = DateTimeOffset.Parse(expectedStartStr, CultureInfo.InvariantCulture);
        var expectedEnd = DateTimeOffset.Parse(expectedEndStr, CultureInfo.InvariantCulture);

        var dateTimeRange1 = DateTimeRange.From(start1, end1);
        var dateTimeRange2 = DateTimeRange.From(start2, end2);

        var intersection = dateTimeRange1.Intersect(dateTimeRange2);
        var intersection2 = dateTimeRange2.Intersect(dateTimeRange1);

        intersection2.Should().Be(intersection);
        intersection.Should().NotBeNull();
        intersection!.Start.Should().Be(expectedStart);
        intersection!.End.Should().Be(expectedEnd);
    }

    [Fact]
    public void Intersect_WithNonOverlappingRanges_ReturnsNull()
    {
        var start1 = new DateTimeOffset(2023, 01, 01, 0, 0, 0, TimeSpan.Zero);
        var end1 = new DateTimeOffset(2023, 02, 01, 0, 0, 0, TimeSpan.Zero);

        var start2 = new DateTimeOffset(2021, 03, 01, 0, 0, 0, TimeSpan.Zero);
        var end2 = new DateTimeOffset(2021, 04, 01, 0, 0, 0, TimeSpan.Zero);

        var dateTimeRange1 = DateTimeRange.From(start1, end1);
        var dateTimeRange2 = DateTimeRange.From(start2, end2);

        var intersection = dateTimeRange1.Intersect(dateTimeRange2);
        var intersection2 = dateTimeRange2.Intersect(dateTimeRange1);

        intersection2.Should().Be(intersection);
        intersection.Should().BeNull();
    }

    [Theory]
    [InlineData("en-GB", "06/20/2023 10:05:00 +08:00 - 08/20/2023 10:05:00 +00:00")]
    [InlineData("en-US", "06/20/2023 10:05:00 +08:00 - 08/20/2023 10:05:00 +00:00")]
    public void ToString_WithoutParameters_ReturnsFormattedStringInInvariantCulture(string culture, string expectedResult)
    {
        void Test()
        {
            var start = new DateTimeOffset(2023, 6, 20, 10, 5, 0, TimeSpan.FromHours(8));
            var end = new DateTimeOffset(2023, 8, 20, 10, 5, 0, TimeSpan.Zero);

            var dateTimeRange = DateTimeRange.From(start, end);
            var dateTimeRangeString = dateTimeRange.ToString();

            dateTimeRangeString.Should().Be(expectedResult);
        }

        TestUtility.RunInCulture(Test, culture);
    }

    [Theory]
    [InlineData("en-GB", "d", "06/20/2023 - 08/20/2023")]
    [InlineData("en-US", "d", "06/20/2023 - 08/20/2023")]
    [InlineData("en-GB", "dd/MM/yy", "20/06/23 - 20/08/23")]
    [InlineData("en-US", "dd/MM/yy", "20/06/23 - 20/08/23")]
    [InlineData("en-GB", "dd MMM HH:mm:ss", "20 Jun 10:05:00 - 20 Aug 10:05:00")]
    [InlineData("en-GB", "r", "Tue, 20 Jun 2023 02:05:00 GMT - Sun, 20 Aug 2023 10:05:00 GMT")]
    [InlineData("en-GB", "u", "2023-06-20 02:05:00Z - 2023-08-20 10:05:00Z")]
    [InlineData("en-GB", "s", "2023-06-20T10:05:00 - 2023-08-20T10:05:00")]
    public void ToString_WithFormatParameter_ReturnsFormattedStringInInvariantCulture(string culture, string format, string expectedResult)
    {
        void Test()
        {
            var start = new DateTimeOffset(2023, 6, 20, 10, 5, 0, TimeSpan.FromHours(8));
            var end = new DateTimeOffset(2023, 8, 20, 10, 5, 0, TimeSpan.Zero);

            var dateTimeRange = DateTimeRange.From(start, end);
            var dateTimeRangeString = dateTimeRange.ToString(format);

            dateTimeRangeString.Should().Be(expectedResult);
        }

        TestUtility.RunInCulture(Test, culture);
    }

    [Theory]
    [InlineData("en-GB")]
    [InlineData("en-US")]
    public void ToString_WithCultureParameter_ReturnsFormattedString(string culture)
    {
        void Test()
        {
            var start = new DateTimeOffset(2023, 6, 20, 10, 5, 0, TimeSpan.FromHours(8));
            var end = new DateTimeOffset(2023, 8, 20, 10, 5, 0, TimeSpan.Zero);

            var startNativeFormattingOutput = new DateTimeOffset(2023, 6, 20, 10, 5, 0, 0, new System.TimeSpan(8, 0, 0)).ToString(new CultureInfo(culture));
            var endNativeFormattingOutput = new DateTimeOffset(2023, 8, 20, 10, 5, 0, System.TimeSpan.Zero).ToString(new CultureInfo(culture));
            var nativeFormattingOutput = $"{startNativeFormattingOutput} - {endNativeFormattingOutput}";

            var dateTimeRange = DateTimeRange.From(start, end);
            var dateTimeRangeString = dateTimeRange.ToString(new CultureInfo(culture));

            dateTimeRangeString.Should().Be(nativeFormattingOutput);
        }

        TestUtility.RunInInvariantCulture(Test);
    }

    [Theory]
    [InlineData("en-GB", "d", "20/06/2023 - 20/08/2023")]
    [InlineData("en-US", "d", "6/20/2023 - 8/20/2023")]
    [InlineData("en-US", "dd/MM/yy", "20/06/23 - 20/08/23")]
    [InlineData("en-GB", "dd/MM/yy", "20/06/23 - 20/08/23")]
    [InlineData("en-GB", "dd MMM HH:mm:ss", "20 Jun 10:05:00 - 20 Aug 10:05:00")]
    [InlineData("en-GB", "r", "Tue, 20 Jun 2023 02:05:00 GMT - Sun, 20 Aug 2023 10:05:00 GMT")]
    [InlineData("en-GB", "u", "2023-06-20 02:05:00Z - 2023-08-20 10:05:00Z")]
    [InlineData("en-GB", "s", "2023-06-20T10:05:00 - 2023-08-20T10:05:00")]
    public void ToString_WithCultureAndFormatParameters_ReturnsFormattedString(string culture, string format, string expectedResult)
    {
        var start = new DateTimeOffset(2023, 6, 20, 10, 5, 0, TimeSpan.FromHours(8));
        var end = new DateTimeOffset(2023, 8, 20, 10, 5, 0, TimeSpan.Zero);

        var dateTimeRange = DateTimeRange.From(start, end);
        var dateTimeRangeString = dateTimeRange.ToString(format, new CultureInfo(culture));

        dateTimeRangeString.Should().Be(expectedResult);
    }

    private static void AssertAreEquivalent(DateTimeRange expected, DateTimeRange actual)
    {
        using var scope = new AssertionScope();

        actual.Should().Be(expected);
        expected.Equals(actual).Should().BeTrue();
        actual.Equals(expected).Should().BeTrue();
        (expected == actual).Should().BeTrue();
        (expected != actual).Should().BeFalse();
    }

    private static void AssertAreNotEquivalent(DateTimeRange expected, DateTimeRange actual)
    {
        using var scope = new AssertionScope();

        actual.Should().NotBe(expected);
        expected.Equals(actual).Should().BeFalse();
        actual.Equals(expected).Should().BeFalse();
        (expected == actual).Should().BeFalse();
        (expected != actual).Should().BeTrue();
    }
}