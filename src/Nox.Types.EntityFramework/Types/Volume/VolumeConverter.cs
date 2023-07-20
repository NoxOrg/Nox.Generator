﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Nox.Types.EntityFramework.Types;

public class VolumeToCubicFeetConverter : ValueConverter<Volume, double>
{
    public VolumeToCubicFeetConverter() : base(volume => (double)volume.ToCubicFeet(), volumeValue => Volume.FromCubicFeet(volumeValue)) { }
}
public class VolumeToCubicMetersConverter : ValueConverter<Volume, double>
{
    public VolumeToCubicMetersConverter() : base(volume => (double)volume.ToCubicMeters(), volumeValue => Volume.FromCubicMeters(volumeValue)) { }
}
