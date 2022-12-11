using BuildingBlocks.Specification;
using FluentPOS.Catalog.Products.Models;

namespace FluentPOS.Catalog.Products.Specifications;
internal class SearchProductsSpecification : SpecificationBase<Product>
{
    public SearchProductsSpecification(string searchString, decimal minimumPrice = decimal.Zero, decimal maximumPrice = decimal.MaxValue)
    {
        Criteria = p => p.Id > 0;

        if (!string.IsNullOrEmpty(searchString))
        {
            Criteria = Criteria!.And(p => (p.Name!.ToLower().Contains(searchString.ToLower())
            || (p.Description!.ToLower().Contains(searchString.ToLower()))));
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
