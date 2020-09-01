using System;
using System.Linq;
using System.Linq.Expressions;

namespace Unosquare.EntityFramework.Specification.Common.Primitive
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
    
    public class ExpressionSelector<T, TU> : Selector<T, TU>
    {
        private readonly Expression<Func<T, TU>> _exp;

        public ExpressionSelector(Expression<Func<T, TU>> exp = null)
        {
            _exp = exp;
        }
        public override Expression<Func<T, TU>> BuildExpression()
        {
            return _exp;
        }
    }
}