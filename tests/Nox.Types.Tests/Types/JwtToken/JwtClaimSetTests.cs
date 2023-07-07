﻿using FluentAssertions;
using System.Security.Claims;

namespace Nox.Types.Tests.Types;

public class JwtClaimSetTests
{
    [Fact]
    public void JwtClaimSet_Constructor_ReturnsValue()
    {
        var encodedPayload = "eyJzdWIiOiIxMjM0NTY3ODkxIiwibmFtZSI6IkpvaG4gRG9lIiwicm9sZXMiOlt7Im5hbWUiOiJPd25lciIsImFzQXQiOiIyMDIzLTA3LTA1VDAwOjAwOjAwIn0seyJuYW1lIjoiQ29udHJpYnV0b3IiLCJhc0F0IjoiMjAyMy0wNy0wMVQwMDowMDowMCJ9XSwiaWF0IjoxNTE2MjM5MDIyfQ";

        var claims = new JwtClaimSet(encodedPayload);

        claims.Should().BeEquivalentTo(new[]
        {
            new Claim("sub", "1234567891", "http://www.w3.org/2001/XMLSchema#string"),
            new Claim("name", "John Doe", "http://www.w3.org/2001/XMLSchema#string"),
            new Claim("roles", "{\"name\":\"Owner\",\"asAt\":\"2023-07-05T00:00:00\"}", "JSON"),
            new Claim("roles", "{\"name\":\"Contributor\",\"asAt\":\"2023-07-01T00:00:00\"}", "JSON"),
            new Claim("iat", "1516239022", "http://www.w3.org/2001/XMLSchema#integer"),
        });
    }
}