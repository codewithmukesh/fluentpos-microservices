using MassTransit;

namespace BuildingBlocks.Domain;
public abstract class EntityBase : IEntity
{
    public Guid Id { get; set; } = NewId.Next().ToGuid();
}
public interface IEntity
{
    Guid Id { get; protected set; }
}