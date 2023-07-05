using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System;
using System.Globalization;

namespace Nox.Types;

/// <summary>
/// Represents a Nox <see cref="MacAddress"/> type and value object.
/// </summary>
/// <remarks>Placeholder, needs to be implemented</remarks>
public sealed class MacAddress : ValueObject<string, MacAddress>
{
    private const int MacAddressLengthInBytes = 6;

    /// <summary>
    /// Creates a new instance of <see cref="MacAddress"/> object.
    /// </summary>
    /// <param name="value">The string value to create the <see cref="MacAddress"/> with</param>
    /// <returns></returns>
    /// <exception cref="TypeValidationException"></exception>
    public new static MacAddress From(string value)
    {
        TryParse(value, out string macAddressValue);

        var newObject = new MacAddress
        {
            Value = macAddressValue
        };

        var validationResult = newObject.Validate();

        if (!validationResult.IsValid)
        {
            throw new TypeValidationException(validationResult.Errors);
        }

        return newObject;
    }

    /// <summary>
    /// Creates a new instance of <see cref="MacAddress"/> object.
    /// </summary>
    /// <param name="value">The ulong value to create the <see cref="MacAddress"/> with</param>
    /// <returns></returns>
    /// <exception cref="TypeValidationException"></exception>
    public static MacAddress From(ulong value)
    {
        var macAddressValue = PadToMacAddressLength(value.ToString("X2", CultureInfo.InvariantCulture));
        return From(macAddressValue);
    }

    /// <summary>
    /// Creates a new instance of <see cref="MacAddress"/> object.
    /// </summary>
    /// <param name="value">The byte array value to create the <see cref="MacAddress"/> with</param>
    /// <returns></returns>
    /// <exception cref="TypeValidationException"></exception>
    public static MacAddress From(byte[] value)
    {
        var macAddressValue = FormatAsMacAddressHexString(value);
        return From(macAddressValue);
    }

    /// <summary>
    /// Converts the instance to string and formats it in <see cref="MacAddressFormat.ByteGroupWithColon"/> format
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public new string ToString()
        => ToString(MacAddressFormat.ByteGroupWithColon);

    /// <summary>
    /// Converts the instance to string in specified <see cref="MacAddressFormat"/> format.
    /// </summary>
    /// <param name="format">The format.</param>
    public string ToString(MacAddressFormat format)
        => format switch
        {
            MacAddressFormat.NoSeparator => Value.ToString(CultureInfo.InvariantCulture),
            MacAddressFormat.ByteGroupWithColon => string.Join(":", SplitAddress(2)),
            MacAddressFormat.ByteGroupWithDash => string.Join("-", SplitAddress(2)),
            MacAddressFormat.DoubleByteGroupWithColon => string.Join(":", SplitAddress(4)),
            MacAddressFormat.DoubleByteGroupWithDot => string.Join(".", SplitAddress(4)),
            _ => throw new NotImplementedException()
        };

    /// <summary>
    /// Validates a <see cref="MacAddress"/> object.
    /// </summary>
    /// <returns>true if the <see cref="MacAddress"/> value is valid.</returns>
    internal override ValidationResult Validate()
    {
        var result = base.Validate();

        if (!TryParse(Value, out var _))
        {
            result.Errors.Add(new ValidationFailure(nameof(Value), $"Could not create a Nox MAC Address type as value {Value} is not a valid MAC Address."));
        }

        return result;
    }

    private static bool TryParse(string inputValue, out string macAddressValue)
    {
        try
        {
            var macAddress = PhysicalAddress.Parse(inputValue);
            var bytes = macAddress.GetAddressBytes();
            macAddressValue = FormatAsMacAddressHexString(bytes);

            if (bytes.Length > MacAddressLengthInBytes)
            {
                return false;
            }

            return true;
        }
        catch (Exception)
        {
            macAddressValue = inputValue;
            return false;
        }
    }

    private IEnumerable<string> SplitAddress(int chunkSize)
        => Enumerable.Range(0, Value.Length / chunkSize).Select(i => Value.Substring(i * chunkSize, chunkSize));

    /// <summary>
    /// Formats byte array as upper case hexadecimal digits and pads it to 12 characters
    /// </summary>
    /// <param name="input">The input.</param>
    private static string FormatAsMacAddressHexString(byte[] input)
        => PadToMacAddressLength(string.Join(string.Empty, input.Select(x => x.ToString("X2", CultureInfo.InvariantCulture)).ToArray()).ToUpperInvariant());

    /// <summary>
    /// Pads the string to the length of to mac address.
    /// </summary>
    /// <param name="input">The input.</param>
    private static string PadToMacAddressLength(string input)
        => input.PadLeft(2 * MacAddressLengthInBytes, '0');
}
