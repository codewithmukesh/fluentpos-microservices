using BuildingBlocks.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BuildingBlocks.Specification;
public static class Extensions
{
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        ParameterExpression p = left.Parameters.First();
        SubstExpressionVisitor visitor = new SubstExpressionVisitor
        {
            Subst = { [right.Parameters.First()] = p }
        };

        Expression body = Expression.AndAlso(left.Body, visitor.Visit(right.Body));
        return Expression.Lambda<Func<T, bool>>(body, p);
    }

    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {

        ParameterExpression p = left.Parameters.First();
        SubstExpressionVisitor visitor = new SubstExpressionVisitor
        {
            Subst = { [right.Parameters.First()] = p }
        };

        Expression body = Expression.OrElse(left.Body, visitor.Visit(right.Body));
        return Expression.Lambda<Func<T, bool>>(body, p);
    }

    public static IQueryable<T> Specify<T>(this IQueryable<T> query, ISpecification<T> spec) where T : class, IEntityBase
    {
        var queryableResultWithIncludes = spec.Includes
            .Aggregate(query,
                (current, include) => current.Include(include));
        var secondaryResult = spec.IncludeStrings
            .Aggregate(queryableResultWithIncludes,
                (current, include) => current.Include(include));
        return secondaryResult.Where(spec.Criteria!);
    }
}
internal class SubstExpressionVisitor : ExpressionVisitor
{
    public Dictionary<Expression, Expression> Subst = new();

    protected override Expression VisitParameter(ParameterExpression node)
    {
        if (Subst.TryGetValue(node, out var newValue))
        {
            return newValue;
        }
        return node;
    }
}