using System.Diagnostics.CodeAnalysis;

namespace Unosquare.EntityFramework.Specification.Common.Extensions;

[ExcludeFromCodeCoverage]
public static class ExpressionExtensions
{
    public static Expression? Replace(this Expression expression, ParameterExpression from, ParameterExpression to) =>
        new ReplaceParameterVisitor(from, to).Visit(expression);

    public static Expression<Func<T, bool>> CombinePropertySelectorWithPredicate<T, TU>(
        this Expression<Func<T, TU>> propertySelector,
        Expression<Func<TU, bool>> propertyPredicate)
    {
        var memberExpression = propertySelector.Body;
        var expr = Expression.Lambda<Func<T, bool>>(propertyPredicate.Body, propertySelector.Parameters);
        var reBinder = new CombineWithSelectorVisitor(propertyPredicate.Parameters[0], memberExpression);

        return (Expression<Func<T, bool>>) reBinder.Visit(expr);
    }
}