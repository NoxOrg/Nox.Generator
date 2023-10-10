﻿// Generated

#nullable enable
using System;
using System.Linq;

using Nox.Extensions;

using TestWebApp.Domain;

namespace TestWebApp.Application.Dto;

internal static class TestEntityExactlyOneToOneOrManyExtensions
{
    public static TestEntityExactlyOneToOneOrManyDto ToDto(this TestEntityExactlyOneToOneOrMany entity)
    {
        var dto = new TestEntityExactlyOneToOneOrManyDto();
        dto.SetIfNotNull(entity?.Id, (dto) => dto.Id = entity!.Id.Value);
        dto.SetIfNotNull(entity?.TextTestField, (dto) => dto.TextTestField =entity!.TextTestField!.Value);
        dto.SetIfNotNull(entity?.TestEntityOneOrManyToExactlyOneId, (dto) => dto.TestEntityOneOrManyToExactlyOneId = entity!.TestEntityOneOrManyToExactlyOneId!.Value);

        return dto;
    }
}