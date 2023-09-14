// Generated

#nullable enable

using System;
using System.Collections.Generic;

using Nox.Types;
using Nox.Domain;

namespace TestWebApp.Domain;
public partial class TestEntityOneOrManyToZeroOrOne:TestEntityOneOrManyToZeroOrOneBase
{

}
/// <summary>
/// .
/// </summary>
public abstract class TestEntityOneOrManyToZeroOrOneBase : AuditableEntityBase, IEntityConcurrent
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
    /// TestEntityOneOrManyToZeroOrOne Test entity relationship to TestEntityZeroOrOneToOneOrMany OneOrMany TestEntityZeroOrOneToOneOrManies
    /// </summary>
    public virtual List<TestEntityZeroOrOneToOneOrMany> TestEntityZeroOrOneToOneOrMany { get; set; } = new();

    /// <summary>
    /// Entity tag used as concurrency token.
    /// </summary>
    public System.Guid Etag { get; set; } = System.Guid.NewGuid();
}