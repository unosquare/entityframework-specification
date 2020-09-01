using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Unosquare.EntityFramework.Specification.Common.Extensions;
using Unosquare.EntityFramework.Specification.Common.Primitive;
using Microsoft.EntityFrameworkCore;

namespace Unosquare.EntityFramework.Specification.EFCore.Extensions
{
    public static class EntityFrameworkExtensions
    {
        public static Task<double> AverageAsync<T>(this IQueryable<T> query, Selector<T, int> selector)
        {
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            
            return query.AverageAsync(selector.BuildExpression().ResolveEmbedded());
        }
        
        public static Task<bool> AnyAsync<T>(this IQueryable<T> query, Specification<T> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            
            var expression = specification
                .BuildExpression()
                .ResolveEmbedded();
            
            return query.AnyAsync(expression);
        }
        
        public static Task<bool> AnyAsync<T, TU>(this IQueryable<T> query, Specification<TU> specification, Selector<T, TU> selector)
        {
            return query.AnyAsync(specification, selector.BuildExpression());
        }
        
        public static Task<bool> AnyAsync<T, TU>(this IQueryable<T> query, Specification<TU> specification, Expression<Func<T, TU>> selector)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            
            var expression = selector
                .CombinePropertySelectorWithPredicate(specification.BuildExpression())
                .ResolveEmbedded();
            
            return query.AnyAsync(expression);
        }
        
        public static Task<int> CountAsync<T>(this IQueryable<T> query, Specification<T> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            
            var expression = specification
                .BuildExpression()
                .ResolveEmbedded();
            
            return query.CountAsync(expression);
        }
        
        public static Task<int> CountAsync<T, TU>(this IQueryable<T> query, Specification<TU> specification, Selector<T, TU> selector)
        {
            return query.CountAsync(specification, selector.BuildExpression());
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
        
        public static Task<T> FirstOrDefaultAsync<T>(this IQueryable<T> query, Specification<T> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            
            var expression = specification
                .BuildExpression()
                .ResolveEmbedded();
            
            return query.FirstOrDefaultAsync(expression);
        }
        
        public static Task<T> FirstOrDefaultAsync<T, TU>(this IQueryable<T> query, Specification<TU> specification, Selector<T, TU> selector)
        {
            return query.FirstOrDefaultAsync(specification, selector.BuildExpression());
        }
        
        public static Task<T> FirstOrDefaultAsync<T, TU>(this IQueryable<T> query, Specification<TU> specification, Expression<Func<T, TU>> selector)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            
            var expression = selector
                .CombinePropertySelectorWithPredicate(specification.BuildExpression())
                .ResolveEmbedded();
            
            return query.FirstOrDefaultAsync(expression);
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
        
        public static Task<Dictionary<TU, int>> GroupAndDictionaryForCountAsync<T, TU>(this IQueryable<T> query, Selector<T, TU> selector)
        {
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            var grouping = selector.BuildExpression().ResolveEmbedded();
            return query
                .GroupBy(grouping)
                .ToDictionaryAsync(x => x.Key, x => x.Count());
        }
        
        public static Task<Dictionary<TUu, int>> GroupAndDictionaryForCountAsync<T, TU, TUu>(this IQueryable<T> query, Selector<TU, TUu> selector, Expression<Func<T, TU>> additionalSelector)
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