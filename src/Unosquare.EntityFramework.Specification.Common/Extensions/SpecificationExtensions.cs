using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace Unosquare.EntityFramework.Specification.Common.Extensions;

public static class SpecificationExtensions
{
    public static Specification<T> And<T>(this Specification<T> left, Specification<T> right) =>
        new AndSpecification<T>(left, right);

    public static Specification<T> And<T, TU>(this Specification<T> left, Specification<TU> right,
        Expression<Func<T, TU>> selector) => new AndSpecification<T, TU>(left, right, selector);

    public static Specification<T> And<T, TU>(this Specification<T> left, Specification<TU> right,
        Selector<T, TU> selector)
    {
        if (selector == null)
            throw new ArgumentNullException(nameof(selector));

        return And(left, right, selector.BuildExpression());
    }

    public static Specification<T> Or<T>(this Specification<T> left, Specification<T> right) =>
        new OrSpecification<T>(left, right);

    public static Specification<T> Or<T, TU>(this Specification<T> left, Specification<TU> right,
        Expression<Func<T, TU>> selector) => new OrSpecification<T, TU>(left, right, selector);

    public static Specification<T> Or<T, TU>(this Specification<T> left, Specification<TU> right,
        Selector<T, TU> selector)
    {
        if (selector == null)
            throw new ArgumentNullException(nameof(selector));

        return Or(left, right, selector.BuildExpression());
    }

    public static Specification<T> Not<T>(this Specification<T> exp) => new NotSpecification<T>(exp);
}