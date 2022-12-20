namespace BuildingBlocks.Domain;
public abstract class AuditableEntity : EntityBase, IAuditableEntity
{
    public string? CreatedBy { get; set; }
    public DateTime CreatedOn { get; private set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }

    protected AuditableEntity()
    {
        CreatedOn = DateTime.UtcNow;
        LastModifiedOn = DateTime.UtcNow;
    }
}
