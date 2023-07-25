﻿using Nox.Types;
using Nox.Types.EntityFramework.Types;

namespace Nox.EntityFramework.SqlServer;

public class SqlServerAreaDatabaseConfigurator : AreaDatabaseConfigurator, ISqlServerNoxTypeDatabaseConfigurator
{
    public override bool IsDefault => false;

    public override string? GetColumnType(AreaTypeOptions typeOptions)
    {
        var maxNumberOfIntegerDigits = Math.Round(typeOptions.MaxValue, Area.QuantityValueDecimalPrecision).ToString().Length;

        return $"DECIMAL({maxNumberOfIntegerDigits + Area.QuantityValueDecimalPrecision}, {Area.QuantityValueDecimalPrecision})";
    }
}
