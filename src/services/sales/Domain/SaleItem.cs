using BuildingBlocks.Domain;

namespace Sales.Domain;
public class SaleItem : AuditableEntity
{
    public Sale? Sale { get; set; }
    public Guid SaleId { get; set; }
    public Guid ProductId { get; set; }
    public string? ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal MaximumRetailPrice { get; set; }
    public decimal SubTotal { get; set; }
}
