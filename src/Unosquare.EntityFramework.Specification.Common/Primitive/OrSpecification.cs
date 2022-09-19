using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace Unosquare.EntityFramework.Specification.Common.Primitive;

public class OrSpecification<T> : Specification<T>
{
    private readonly Specification<T> _left;
    private readonly Specification<T> _right;

    protected OrSpecification() { }

    public OrSpecification(Specification<T> left, Specification<T> right)
    {
        _left = left;
        _right = right;
    }

    public override Expression<Func<T, bool>> BuildExpression() =>
        ApplyOr(_left.BuildExpression(), _right.BuildExpression());

    protected Expression<Func<T, bool>> ApplyOr(Expression<Func<T, bool>> leftExp, Expression<Func<T, bool>> rightExp)
    {
        if (IsShowAll(leftExp)) return rightExp;
        if (IsShowAll(rightExp)) return leftExp;

        var leftParameter = leftExp.Parameters[0];
        var rightParameter = rightExp.Parameters[0];

        var rightWithChangedParam = rightExp.Body.Replace(rightParameter, leftParameter);

        return Expression.Lambda<Func<T, bool>>(Expression.OrElse(leftExp.Body, rightWithChangedParam), leftParameter);
    }

    private static bool IsShowAll(Expression<Func<T, bool>> exp) =>
        exp.Body.Type == typeof(bool) && exp.Body.NodeType == ExpressionType.Constant;
}

public class OrSpecification<T, TU> : OrSpecification<T>
{
    private readonly Specification<T> _left;
    private readonly Specification<TU> _right;
    private readonly Expression<Func<T, TU>> _selector;

    public OrSpecification(Specification<T> left, Specification<TU> right, Expression<Func<T, TU>> selector)
    {
        _left = left;
        _right = right;
        _selector = selector;
    }

    public override Expression<Func<T, bool>> BuildExpression() =>
        ApplyOr(_left.BuildExpression(), _selector.CombinePropertySelectorWithPredicate(_right.BuildExpression()));
}