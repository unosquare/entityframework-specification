using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace Unosquare.EntityFramework.Specification.Common.Extensions
{
    public static class SpecificationExtensions
    {
        public static Specification<T> And<T>(this Specification<T> left, Specification<T> right)
        {
            return new AndSpecification<T>(left, right);
        }
        
        public static Specification<T> And<T, TU>(this Specification<T> left, Specification<TU> right, Expression<Func<T, TU>> selector)
        {
            return new AndSpecification<T, TU>(left, right, selector);
        }
        
        public static Specification<T> And<T, TU>(this Specification<T> left, Specification<TU> right, Selector<T, TU> selector)
        {
            return And(left, right, selector.BuildExpression());
        }

        public static Specification<T> Or<T>(this Specification<T> left, Specification<T> right)
        {
            return new OrSpecification<T>(left, right);
        }
        
        public static Specification<T> Or<T, TU>(this Specification<T> left, Specification<TU> right, Expression<Func<T, TU>> selector)
        {
            return new OrSpecification<T, TU>(left, right, selector);
        }
        
        public static Specification<T> Or<T, TU>(this Specification<T> left, Specification<TU> right, Selector<T, TU> selector)
        {
            return Or(left, right, selector.BuildExpression());
        }

        public static Specification<T> Not<T>(this Specification<T> exp)
        {
            return new NotSpecification<T>(exp);
        }
    }
}
