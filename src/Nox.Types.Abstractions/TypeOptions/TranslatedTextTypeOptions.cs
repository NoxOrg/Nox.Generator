﻿namespace Nox.Types;

/// <summary>
/// The translated text type options.
/// </summary>

public class TranslatedTextTypeOptions : INoxTypeOptions
{
    /// <summary>
    /// Gets the min length.
    /// </summary>
    public int MinLength { get; internal set; } = 0;

    /// <summary>
    /// Gets the max length.
    /// </summary>
    public int MaxLength { get; internal set; } = 511;

    /// <summary>
    /// Gets the character casing.
    /// </summary>
    public TextTypeCasing CharacterCasing { get; internal set; } = TextTypeCasing.Normal;
}