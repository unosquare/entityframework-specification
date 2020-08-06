using System;
using System.Linq;
using System.Linq.Expressions;

namespace Unosquare.EntityFramework.Specification.Primitive
{
    public abstract class Selector
    {
        public abstract Expression GetExpression();
    }
    
    public abstract class Selector<T, TU> : Selector
    {
        public abstract Expression<Func<T, TU>> BuildExpression();

        public override Expression GetExpression() => BuildExpression();
    }
    
    public abstract class GroupBySelector<T, TU>: Selector
    {
        public abstract Expression<Func<T, TU>> GroupBy();
        
        public abstract Expression<Func<IGrouping<TU, T>, T>> BuildExpression();
        
        public override Expression GetExpression() => BuildExpression();
    }
    
    public abstract class GroupBySelector<T, TU, TV>: Selector
    {
        public abstract Expression<Func<T, TU>> GroupBy();
        
        public abstract Expression<Func<IGrouping<TU, T>, TV>> BuildExpression();
        
        public override Expression GetExpression() => BuildExpression();
    }
}