using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Unosquare.EntityFramework.Specification.Primitive;

namespace Unosquare.EntityFramework.Specification.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class CollectionExtensions
    {
        public static IQueryable<T> Where<T>(this IQueryable<T> query, Specification<T> specification)
        {
            if (specification == null) return query;

            var expression = specification
                .BuildExpression()
                .ResolveEmbedded();
        
            return query.Where(expression);
        }
        
        public static IQueryable<T> Where<T, TU>(this IQueryable<T> query, Specification<TU> specification, 
            Selector<T, TU> selector)
        {
            if (specification == null) return query;
            if (selector == null) return query;
            
            var expression = selector
                .BuildExpression()
                .CombinePropertySelectorWithPredicate(specification.BuildExpression())
                .ResolveEmbedded();
            
            return query.Where(expression);
        }
        
        public static IQueryable<T> Where<T, TU>(this IQueryable<T> query, Specification<TU> specification, 
            Expression<Func<T, TU>> selector)
        {
            if (specification == null) return query;
            if (selector == null) return query;
            
            var expression = selector
                .CombinePropertySelectorWithPredicate(specification.BuildExpression())
                .ResolveEmbedded();
            
            return query.Where(expression);
        }
        
        public static IEnumerable<T> Where<T>(this IEnumerable<T> query, Specification<T> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            
            return query.Where(specification.IsSatisfy);
        }

        public static IEnumerable<T> Where<T, TU>(this IEnumerable<T> query, Specification<TU> specification, Func<T, TU> selector)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            
            return query.Where(x => specification.IsSatisfy(selector(x)));
        }

        public static Task<int> CountAsync<T>(this IQueryable<T> query, Specification<T> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            
            var expression = specification
                .BuildExpression()
                .ResolveEmbedded();
            
            return query.CountAsync(expression);
        }
        
        public static Task<int> CountAsync<T, TU>(this IQueryable<T> query, Specification<TU> specification, 
            Expression<Func<T, TU>> selector)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            
            var expression = selector
                .CombinePropertySelectorWithPredicate(specification.BuildExpression())
                .ResolveEmbedded();
            
            return query.CountAsync(expression);
        }
        
        public static Task<int> CountAsync<T, TU>(this IQueryable<T> query, Specification<TU> specification, Selector<T, TU> selector)
        {
            return query.CountAsync(specification, selector.BuildExpression());
        }

        public static int Count<T, TU>(this IQueryable<T> query, Specification<TU> specification, Expression<Func<T, TU>> selector)
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

        public static int Count<T>(this IQueryable<T> query, Specification<T> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            
            var expression = specification
                .BuildExpression()
                .ResolveEmbedded();

            return query.Count(expression);
        }
        
        public static int Count<T>(this IEnumerable<T> query, Specification<T> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));

            return query.Count(specification.IsSatisfy);
        }

        public static bool Any<T>(this IQueryable<T> query, Specification<T> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));

            var expression = specification
                .BuildExpression()
                .ResolveEmbedded();
            
            return query.Any(expression);
        }
        
        public static T FirstOrDefault<T>(this IQueryable<T> query, Specification<T> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));

            return query.FirstOrDefault(specification.BuildExpression().ResolveEmbedded());
        }
        
           public static IQueryable<TU> Select<T, TU>(this IQueryable<T> query, Selector<T, TU> selector)
        {
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            
            var expression = selector.BuildExpression().ResolveEmbedded();

            return query.Select(expression);
        }
        
        public static IQueryable<Tuu> Select<T, Tu, Tuu>(this IQueryable<T> query, Selector<Tu, Tuu> selector, Expression<Func<T, Tu>> additionalSelector)
        {
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            
            var expression = selector.BuildExpression().ResolveEmbedded();
            
            return query.Select(additionalSelector).Select(expression);
        }
        
        public static IEnumerable<TU> Select<T, TU>(this IEnumerable<T> query, Selector<T, TU> selector)
        {
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            
            var expression = selector.BuildExpression().ResolveEmbedded().Compile();

            return query.Select(expression);
        }

        public static Task<double> AverageAsync<T>(this IQueryable<T> query, Selector<T, int> selector)
        {
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            
            return query.AverageAsync(selector.BuildExpression().ResolveEmbedded());
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
        
        public static Task<Dictionary<TU, TV>> GroupByDictionaryAsync<T, TU, TV>(this IQueryable<T> query, GroupBySelector<T, TU, TV> selector)
        {
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            var grouping = selector.GroupBy().ResolveEmbedded();
            var expression = selector
                .BuildExpression()
                .ResolveEmbedded()
                .Compile();
            
            return query.GroupBy(grouping).ToDictionaryAsync(x => x.Key, expression);
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
        
        public static Task<Dictionary<TU, int>> GroupAndDictionaryForCountAsync<T, TU>(this IQueryable<T> query, Selector<T, TU> selector)
        {
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            var grouping = selector.BuildExpression().ResolveEmbedded();
            return query
                .GroupBy(grouping)
                .ToDictionaryAsync(x => x.Key, x => x.Count());
        }
        
        public static Task<Dictionary<TUU, int>> GroupAndDictionaryForCountAsync<T, TU, TUU>(this IQueryable<T> query, Selector<TU, TUU> selector, Expression<Func<T, TU>> additionalSelector)
        {
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            if (additionalSelector == null) throw new ArgumentNullException(nameof(additionalSelector));

            var grouping = selector.BuildExpression().ResolveEmbedded();
            return query
                .Select(additionalSelector)
                .GroupBy(grouping)
                .ToDictionaryAsync(x => x.Key, x => x.Count());
        }
    }
}