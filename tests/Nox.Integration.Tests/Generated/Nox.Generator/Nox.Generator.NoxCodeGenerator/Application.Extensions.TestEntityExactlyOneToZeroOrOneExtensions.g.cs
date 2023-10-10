﻿// Generated

#nullable enable
using System;
using System.Linq;

using Nox.Extensions;

using TestWebApp.Domain;

namespace TestWebApp.Application.Dto;

internal static class TestEntityExactlyOneToZeroOrOneExtensions
{
    public static TestEntityExactlyOneToZeroOrOneDto ToDto(this TestEntityExactlyOneToZeroOrOne entity)
    {
        var dto = new TestEntityExactlyOneToZeroOrOneDto();
        dto.SetIfNotNull(entity?.Id, (dto) => dto.Id = entity!.Id.Value);
        dto.SetIfNotNull(entity?.TextTestField2, (dto) => dto.TextTestField2 =entity!.TextTestField2!.Value);
        dto.SetIfNotNull(entity?.TestEntityZeroOrOneToExactlyOneId, (dto) => dto.TestEntityZeroOrOneToExactlyOneId = entity!.TestEntityZeroOrOneToExactlyOneId!.Value);

        return dto;
    }
}