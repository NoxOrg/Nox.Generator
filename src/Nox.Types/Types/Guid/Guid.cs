﻿using System.Collections.Generic;

namespace Nox.Types;

/// <summary>
/// Represents a Nox <see cref="Guid"/> type and value object.
/// </summary>
public sealed class Guid : ValueObject<System.Guid, Guid>
{
    /// <summary>
    /// A instance of the <see cref="Guid"/> object whose Value is all zeros.
    /// </summary>
    public static Guid Empty => new() { Value = System.Guid.Empty };

    /// <summary>
    /// Creates a <see cref="Guid"/> object from a <see cref="System.String"/>.
    /// </summary>
    /// <param name="value">String to be parsed to <see cref="Guid"/>.</param>
    /// <returns>New instance of <see cref="Guid"/>.</returns>
    /// <exception cref="TypeValidationException">In case the <paramref name="value"/> contains an invalid Guid.</exception>
    public static Guid From(string value)
    {
        if (!System.Guid.TryParse(value, out var parsedGuid))
        {
            throw new TypeValidationException(
                new List<ValidationFailure>
                {
                    new(nameof(value),
                        $"Could not create a Nox {nameof(Guid)} type from the string '{value}' provided, because it is not a valid Guid.")
                });
        }

        var newObject = new Guid
        {
            Value = parsedGuid
        };

        var validationResult = newObject.Validate();

        if (!validationResult.IsValid)
        {
            throw new TypeValidationException(validationResult.Errors);
        }

        return newObject;
    }

    /// <summary>
    /// Initialize a new instance of <see cref="Guid"/> with an autogenerated Guid in its <see cref="ValueObject{T,TValueObject}.Value"/>.
    /// </summary>
    /// <returns>A new <see cref="Guid"/> object.</returns>
    public static Guid NewGuid() => new() { Value = System.Guid.NewGuid() };


    /// <summary>
    /// Validates a <see cref="Guid"/> object.
    /// </summary>
    /// <returns>true if the <see cref="Guid"/> value is valid .</returns>
    internal override ValidationResult Validate()
    {
        var result = base.Validate();

        if (Value == System.Guid.Empty)
        {
            result.Errors.Add(new ValidationFailure(nameof(Value),
                $"Could not create a Nox {nameof(Guid)} type as empty Guid is not allowed."));
        }

        return result;
    }
}