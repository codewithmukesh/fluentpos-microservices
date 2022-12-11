namespace BuildingBlocks.Domain;
public abstract class EntityBase : IEntityBase
{
    public int Id { get; set; }
}

public interface IEntityBase
{
    public int Id { get; set; }
}