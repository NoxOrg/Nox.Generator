// Generated

#nullable enable

using System;
using System.Collections.Generic;

using Nox.Types;
using Nox.Domain;

namespace TestWebApp.Domain;
public partial class SecondTestEntityOneOrMany:SecondTestEntityOneOrManyBase
{

}
/// <summary>
/// .
/// </summary>
public abstract class SecondTestEntityOneOrManyBase : AuditableEntityBase, IEntityConcurrent
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
    /// SecondTestEntityOneOrMany Test entity relationship to TestEntityOneOrMany OneOrMany TestEntityOneOrManies
    /// </summary>
    public virtual List<TestEntityOneOrMany> TestEntityOneOrManyRelationship { get; set; } = new();

    /// <summary>
    /// Entity tag used as concurrency token.
    /// </summary>
    public System.Guid Etag { get; set; } = System.Guid.NewGuid();
}