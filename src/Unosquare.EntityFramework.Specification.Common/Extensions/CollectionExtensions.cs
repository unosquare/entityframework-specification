using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace Unosquare.EntityFramework.Specification.Common.Extensions;

public static class CollectionExtensions
{
    public static IQueryable<T> Where<T>(this IQueryable<T> query, Specification<T>? specification)
    {
        if (specification == null) return query;

        var expression = specification
            .BuildExpression()
            .ResolveEmbedded();

        return query.Where(expression);
    }

    public static IQueryable<T> Where<T, TU>(this IQueryable<T> query, Specification<TU>? specification,
        Expression<Func<T, TU>>? selector)
    {
        if (specification == null) return query;
        if (selector == null) return query;

        var expression = selector
            .CombinePropertySelectorWithPredicate(specification.BuildExpression())
            .ResolveEmbedded();

        return query.Where(expression);
    }

    public static IQueryable<T> Where<T, TU>(this IQueryable<T> query, Specification<TU> specification,
        Selector<T, TU> selector)
    {
        return query.Where(specification, selector.BuildExpression());
    }

    public static IEnumerable<T> Where<T>(this IEnumerable<T> query, Specification<T>? specification)
    {
        if (specification == null) throw new ArgumentNullException(nameof(specification));

        return query.Where(specification.IsSatisfy);
    }

    public static IEnumerable<T> Where<T, TU>(this IEnumerable<T> query, Specification<TU>? specification,
        Func<T, TU>? selector)
    {
        if (specification == null) throw new ArgumentNullException(nameof(specification));
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        return query.Where(x => specification.IsSatisfy(selector(x)));
    }

    public static IEnumerable<T> Where<T, TU>(this IEnumerable<T> query, Specification<TU> specification,
        Selector<T, TU> selector)
    {
        return query.Where(specification, selector.BuildExpression().Compile());
    }

    public static int Count<T>(this IQueryable<T> query, Specification<T> specification)
    {
        if (specification == null) throw new ArgumentNullException(nameof(specification));

        var expression = specification
            .BuildExpression()
            .ResolveEmbedded();

        return query.Count(expression);
    }

    public static int Count<T, TU>(this IQueryable<T> query, Specification<TU> specification,
        Expression<Func<T, TU>> selector)
    {
        if (specification == null) throw new ArgumentNullException(nameof(specification));
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        var expression = selector
            .CombinePropertySelectorWithPredicate(specification.BuildExpression())
            .ResolveEmbedded();

        return query.Count(expression);
    }

    public static int Count<T, TU>(this IQueryable<T> query, Specification<TU> specification, Selector<T, TU> selector)
    {
        return query.Count(specification, selector.BuildExpression());
    }

    public static int Count<T>(this IEnumerable<T> query, Specification<T> specification)
    {
        if (specification == null) throw new ArgumentNullException(nameof(specification));

        return query.Count(specification.IsSatisfy);
    }

    public static int Count<T, TU>(this IEnumerable<T> query, Specification<TU> specification,
        Func<T, TU> selector)
    {
        if (specification == null) throw new ArgumentNullException(nameof(specification));
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        return query.Count(x => specification.IsSatisfy(selector(x)));
    }

    public static int Count<T, TU>(this IEnumerable<T> query, Specification<TU> specification, Selector<T, TU> selector)
    {
        return query.Count(specification, selector.BuildExpression().Compile());
    }

    public static bool Any<T>(this IQueryable<T> query, Specification<T> specification)
    {
        if (specification == null) throw new ArgumentNullException(nameof(specification));

        var expression = specification
            .BuildExpression()
            .ResolveEmbedded();

        return query.Any(expression);
    }

    public static bool Any<T, TU>(this IQueryable<T> query, Specification<TU> specification,
        Expression<Func<T, TU>> selector)
    {
        if (specification == null) throw new ArgumentNullException(nameof(specification));
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        var expression = selector
            .CombinePropertySelectorWithPredicate(specification.BuildExpression())
            .ResolveEmbedded();

        return query.Any(expression);
    }

    public static bool Any<T, TU>(this IQueryable<T> query, Specification<TU> specification,
        Selector<T, TU> selector)
    {
        return query.Any(specification, selector.BuildExpression());
    }

    public static bool Any<T>(this IEnumerable<T> query, Specification<T> specification)
    {
        if (specification == null) throw new ArgumentNullException(nameof(specification));

        return query.Any(specification.IsSatisfy);
    }

    public static bool Any<T, TU>(this IEnumerable<T> query, Specification<TU> specification,
        Func<T, TU> selector)
    {
        if (specification == null) throw new ArgumentNullException(nameof(specification));
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        return query.Any(x => specification.IsSatisfy(selector(x)));
    }

    public static bool Any<T, TU>(this IEnumerable<T> query, Specification<TU> specification,
        Selector<T, TU> selector)
    {
        return query.Any(specification, selector.BuildExpression().Compile());
    }

    public static T FirstOrDefault<T>(this IQueryable<T> query, Specification<T> specification)
    {
        if (specification == null) throw new ArgumentNullException(nameof(specification));

        return query.FirstOrDefault(specification.BuildExpression().ResolveEmbedded());
    }

    public static T FirstOrDefault<T, TU>(this IQueryable<T> query, Specification<TU> specification,
        Expression<Func<T, TU>> selector)
    {
        if (specification == null) throw new ArgumentNullException(nameof(specification));
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        var expression = selector
            .CombinePropertySelectorWithPredicate(specification.BuildExpression())
            .ResolveEmbedded();

        return query.FirstOrDefault(expression);
    }

    public static T FirstOrDefault<T, TU>(this IQueryable<T> query, Specification<TU> specification,
        Selector<T, TU> selector)
    {
        return query.FirstOrDefault(specification, selector.BuildExpression());
    }

    public static T FirstOrDefault<T>(this IEnumerable<T> query, Specification<T> specification)
    {
        if (specification == null) throw new ArgumentNullException(nameof(specification));

        return query.FirstOrDefault(specification.IsSatisfy);
    }

    public static T FirstOrDefault<T, TU>(this IEnumerable<T> query, Specification<TU> specification,
        Func<T, TU> selector)
    {
        if (specification == null) throw new ArgumentNullException(nameof(specification));
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        return query.FirstOrDefault(x => specification.IsSatisfy(selector(x)));
    }

    public static T FirstOrDefault<T, TU>(this IEnumerable<T> query, Specification<TU> specification,
        Selector<T, TU> selector)
    {
        return query.FirstOrDefault(specification, selector.BuildExpression().Compile());
    }

    public static IQueryable<TU> Select<T, TU>(this IQueryable<T> query, Selector<T, TU> selector)
    {
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        var expression = selector.BuildExpression().ResolveEmbedded();

        return query.Select(expression);
    }

    public static IQueryable<TUu> Select<T, TU, TUu>(this IQueryable<T> query, Selector<TU, TUu> selector, Expression<Func<T, TU>> additionalSelector)
    {
        return query.Select(additionalSelector).Select(selector);
    }

    public static IQueryable<TUu> Select<T, TU, TUu>(this IQueryable<T> query, Selector<TU, TUu> selector, Selector<T, TU> additionalSelector)
    {
        return query.Select(additionalSelector).Select(selector);
    }

    public static IEnumerable<TU> Select<T, TU>(this IEnumerable<T> query, Selector<T, TU> selector)
    {
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        var expression = selector.BuildExpression().ResolveEmbedded().Compile();

        return query.Select(expression);
    }

    public static IQueryable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IQueryable<TSource> query,
        Selector<TSource, TKey> specification)
    {
        if (specification == null) throw new ArgumentNullException(nameof(specification));

        return query.GroupBy(specification.BuildExpression().ResolveEmbedded());
    }

    public static IQueryable<T> GroupAndSelect<T, TU>(this IQueryable<T> query, GroupBySelector<T, TU> selector)
    {
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        var grouping = selector.GroupBy().ResolveEmbedded();
        var expression = selector.BuildExpression().ResolveEmbedded();

        return query.GroupBy(grouping).Select(expression);
    }

    public static IQueryable<TV> GroupAndSelect<T, TU, TV>(this IQueryable<T> query, GroupBySelector<T, TU, TV> selector)
    {
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        var grouping = selector.GroupBy().ResolveEmbedded();
        var expression = selector.BuildExpression().ResolveEmbedded();

        return query.GroupBy(grouping).Select(expression);
    }

    public static IQueryable<KeyWithCount<TU>> GroupAndCount<T, TU>(this IQueryable<T> query, Selector<T, TU> selector)
    {
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        var grouping = selector.BuildExpression().ResolveEmbedded();

        return query.GroupBy(grouping).Select(x => new KeyWithCount<TU>
        {
            Key = x.Key,
            Count = x.Count()
        });
    }
}