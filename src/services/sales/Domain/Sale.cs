using BuildingBlocks.Domain;
namespace Sales.Domain;

public class Sale : AuditableEntity
{
    public Guid CustomerId { get; set; }
    public ICollection<SaleItem>? SaleItems { get; set; }
    public string? CustomerName { get; set; }
    public string? ReferenceNumber { get; set; }
    public int TotalItems { get; set; }
    public string? Note { get; set; }
    public decimal Total { get; set; }
    public int DiscountId { get; set; }
    public decimal Discount { get; set; }
    public int TaxId { get; set; }
    public decimal Tax { get; set; }
    public decimal Shipping { get; set; }
    public decimal GrandTotal { get; set; }
    public SaleStatus SalesStatus { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public PaymentMode PaymentMode { get; set; }

}

public enum SaleStatus
{
    Completed,
    Pending
}

public enum PaymentStatus
{
    Paid,
    PartiallyPaid,
    Pending
}

public enum PaymentMode
{
    Cash,
    CreditCard,
    DebitCard,
    UPI
}