using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace Unosquare.EntityFramework.Specification.Common.Primitive;

public class AndSpecification<T> : Specification<T>
{
    private readonly Specification<T> _left;
    private readonly Specification<T> _right;

    protected AndSpecification() { }
        
    public AndSpecification(Specification<T> left, Specification<T> right)
    {
        _left = left;
        _right = right;
    }
        
    public override Expression<Func<T, bool>> BuildExpression() => 
        ApplyAnd(_left.BuildExpression(), _right.BuildExpression());
        
    protected Expression<Func<T, bool>> ApplyAnd(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        var leftParameter = left.Parameters[0];
        var rightParameter = right.Parameters[0];

        var rightWithChangedParam = right.Body.Replace(rightParameter, leftParameter);

        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left.Body, rightWithChangedParam), leftParameter);
    }
}
    
public class AndSpecification<T, TU> : AndSpecification<T>
{
    private readonly Expression<Func<T, TU>> _selector;
    private readonly Specification<T> _left;
    private readonly Specification<TU> _right;

    public AndSpecification(Specification<T> left, Specification<TU> right, Expression<Func<T, TU>> selector)
    {
        _left = left;
        _right = right;
        _selector = selector;
    }

    public override Expression<Func<T, bool>> BuildExpression() =>
        ApplyAnd(_left.BuildExpression(), _selector.CombinePropertySelectorWithPredicate(_right.BuildExpression()));
}