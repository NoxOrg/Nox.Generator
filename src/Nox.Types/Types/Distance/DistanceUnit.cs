﻿using Nox.Types.Common;

namespace Nox.Types;

public class DistanceUnit : MeasurementUnit
{
    public static DistanceUnit Kilometer { get; } = new DistanceUnit(1, "Kilometer", "km");
    public static DistanceUnit Mile { get; } = new DistanceUnit(2, "Mile", "mi");

    protected DistanceUnit(int id, string name, string symbol) : base(id, name, symbol)
    {
    }
}
