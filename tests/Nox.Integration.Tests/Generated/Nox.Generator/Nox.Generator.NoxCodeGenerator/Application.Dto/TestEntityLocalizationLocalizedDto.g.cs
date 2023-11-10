﻿// Generated

#nullable enable

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Nox.Abstractions;
using Nox.Domain;
using Nox.Solution;
using Nox.Types;

namespace TestWebApp.Application.Dto;

/// <summary>
/// Entity created for testing localization Localized DTO.
/// </summary>
public partial class TestEntityLocalizationLocalizedDto
{
    /// <summary>
    ///  (Required).
    /// </summary>
    
    public System.String Id { get; set; } = default!;

    public System.String CultureCode { get; set; } = default!;

    /// <summary>
    ///  (Required).
    /// </summary>
    public System.String TextFieldToLocalize { get; set; } = default!;

    [JsonPropertyName("@odata.etag")]
    public System.Guid Etag { get; init; }
}

/// <summary>
/// Record for TestEntityLocalization Localized Key DTO.
/// </summary>
public record TestEntityLocalizationLocalizedKeyDto(System.String Id, System.String CultureCode);