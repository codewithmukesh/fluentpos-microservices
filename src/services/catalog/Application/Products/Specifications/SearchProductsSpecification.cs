using BuildingBlocks.Specification;
using FluentPOS.Catalog.Domain;

namespace FluentPOS.Catalog.Application.Products.Specifications;
internal class SearchProductsSpecification : SpecificationBase<Product>
{
    public SearchProductsSpecification(string searchString, decimal minimumPrice = decimal.Zero, decimal maximumPrice = decimal.MaxValue)
    {
        Criteria = p => p.Id != Guid.Empty;

        if (!string.IsNullOrEmpty(searchString))
        {
            Criteria = Criteria!.And(p => (p.Name!.ToLower().Contains(searchString.ToLower())
            || (p.Details!.ToLower().Contains(searchString.ToLower()))));
        }
        if (minimumPrice != decimal.Zero)
        {
            Criteria = Criteria!.And(p => p.Price >= minimumPrice);
        }
        if (maximumPrice != decimal.MaxValue)
        {
            Criteria = Criteria!.And(p => p.Price < maximumPrice);
        }
    }
}
