// Generated

#nullable enable

using System;
using System.Collections.Generic;

using Nox.Types;
using Nox.Domain;

namespace TestWebApp.Domain;
public partial class TestEntityZeroOrManyToZeroOrOne:TestEntityZeroOrManyToZeroOrOneBase
{

}
/// <summary>
/// .
/// </summary>
public abstract class TestEntityZeroOrManyToZeroOrOneBase : AuditableEntityBase, IEntityConcurrent
{
    /// <summary>
    ///  (Required).
    /// </summary>
    public Text Id { get; set; } = null!;

    /// <summary>
    ///  (Required).
    /// </summary>
    public Nox.Types.Text TextTestField2 { get; set; } = null!;

    /// <summary>
    /// TestEntityZeroOrManyToZeroOrOne Test entity relationship to TestEntityZeroOrOneToZeroOrMany ZeroOrMany TestEntityZeroOrOneToZeroOrManies
    /// </summary>
    public virtual List<TestEntityZeroOrOneToZeroOrMany> TestEntityZeroOrOneToZeroOrMany { get; set; } = new();

    /// <summary>
    /// Entity tag used as concurrency token.
    /// </summary>
    public System.Guid Etag { get; set; } = System.Guid.NewGuid();
}