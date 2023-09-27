// Generated

#nullable enable

using System;
using System.Collections.Generic;

using Nox.Abstractions;
using Nox.Domain;
using Nox.Types;

namespace TestWebApp.Domain;
public partial class SecondTestEntityTwoRelationshipsOneToOne:SecondTestEntityTwoRelationshipsOneToOneBase
{

}
/// <summary>
/// Record for SecondTestEntityTwoRelationshipsOneToOne created event.
/// </summary>
public record SecondTestEntityTwoRelationshipsOneToOneCreated(SecondTestEntityTwoRelationshipsOneToOne SecondTestEntityTwoRelationshipsOneToOne) : IDomainEvent;
/// <summary>
/// Record for SecondTestEntityTwoRelationshipsOneToOne updated event.
/// </summary>
public record SecondTestEntityTwoRelationshipsOneToOneUpdated(SecondTestEntityTwoRelationshipsOneToOne SecondTestEntityTwoRelationshipsOneToOne) : IDomainEvent;
/// <summary>
/// Record for SecondTestEntityTwoRelationshipsOneToOne deleted event.
/// </summary>
public record SecondTestEntityTwoRelationshipsOneToOneDeleted(SecondTestEntityTwoRelationshipsOneToOne SecondTestEntityTwoRelationshipsOneToOne) : IDomainEvent;

/// <summary>
/// .
/// </summary>
public abstract class SecondTestEntityTwoRelationshipsOneToOneBase : EntityBase, IEntityConcurrent
{
    /// <summary>
    ///  (Required).
    /// </summary>
    public Nox.Types.Text Id { get; set; } = null!;

    /// <summary>
    ///  (Required).
    /// </summary>
    public Nox.Types.Text TextTestField2 { get; set; } = null!;

    /// <summary>
    /// SecondTestEntityTwoRelationshipsOneToOne First relationship to the same entity on the other side ZeroOrOne TestEntityTwoRelationshipsOneToOnes
    /// </summary>
    public virtual TestEntityTwoRelationshipsOneToOne? TestRelationshipOneOnOtherSide { get; private set; } = null!;

    public virtual void CreateRefToTestRelationshipOneOnOtherSide(TestEntityTwoRelationshipsOneToOne relatedTestEntityTwoRelationshipsOneToOne)
    {
        TestRelationshipOneOnOtherSide = relatedTestEntityTwoRelationshipsOneToOne;
    }

    public virtual void DeleteRefToTestRelationshipOneOnOtherSide(TestEntityTwoRelationshipsOneToOne relatedTestEntityTwoRelationshipsOneToOne)
    {
        TestRelationshipOneOnOtherSide = null;
    }

    public virtual void DeleteAllRefToTestRelationshipOneOnOtherSide()
    {
        TestRelationshipOneOnOtherSide = null;
    }

    /// <summary>
    /// SecondTestEntityTwoRelationshipsOneToOne Second relationship to the same entity on the other side ZeroOrOne TestEntityTwoRelationshipsOneToOnes
    /// </summary>
    public virtual TestEntityTwoRelationshipsOneToOne? TestRelationshipTwoOnOtherSide { get; private set; } = null!;

    public virtual void CreateRefToTestRelationshipTwoOnOtherSide(TestEntityTwoRelationshipsOneToOne relatedTestEntityTwoRelationshipsOneToOne)
    {
        TestRelationshipTwoOnOtherSide = relatedTestEntityTwoRelationshipsOneToOne;
    }

    public virtual void DeleteRefToTestRelationshipTwoOnOtherSide(TestEntityTwoRelationshipsOneToOne relatedTestEntityTwoRelationshipsOneToOne)
    {
        TestRelationshipTwoOnOtherSide = null;
    }

    public virtual void DeleteAllRefToTestRelationshipTwoOnOtherSide()
    {
        TestRelationshipTwoOnOtherSide = null;
    }

    /// <summary>
    /// Entity tag used as concurrency token.
    /// </summary>
    public System.Guid Etag { get; set; } = System.Guid.NewGuid();
}