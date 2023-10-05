﻿// Generated

#nullable enable
using System;
using System.Linq;

using TestWebApp.Domain;

namespace TestWebApp.Application.Dto;

internal static class SecondTestEntityZeroOrOneExtensions
{
    public static SecondTestEntityZeroOrOneDto ToDto(this SecondTestEntityZeroOrOne entity)
    {
        var dto = new SecondTestEntityZeroOrOneDto();
        SetIfNotNull(entity?.Id, () => dto.Id = entity!.Id.Value);
        SetIfNotNull(entity?.TextTestField2, () => dto.TextTestField2 =entity!.TextTestField2!.Value);

        return dto;
    }

    private static void SetIfNotNull(object? value, Action setPropertyAction)
    {
        if (value is not null)
        {
            setPropertyAction();
        }
    }
}