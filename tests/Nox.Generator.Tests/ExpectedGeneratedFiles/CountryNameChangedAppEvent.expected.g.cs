﻿// Generated

#nullable enable

// Generated by DtoGenerator::GenerateEvent

using Nox.Abstractions;
using Nox.Types;
using System.Collections.Generic;

namespace SampleWebApp.Application.Events;

/// <summary>
/// An application event raised when the name of a country changes.
/// </summary>
public partial class CountryNameChangedAppEvent : Nox.Application.IIntegrationEvent
{
    
    /// <summary>
    /// The identifier of the country. The Iso alpha 2 code.
    /// </summary>
    public CountryCode2? CountryId { get; set; } = null!;
    
    /// <summary>
    /// The new name of the country.
    /// </summary>
    public Text? CountryName { get; set; } = null!;
}
