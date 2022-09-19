namespace Unosquare.EntityFramework.Specification.Common.Primitive;

public class NotSpecification<T> : Specification<T>
{
    private readonly Specification<T> _exp;

    public NotSpecification(Specification<T> exp)
    {
        _exp = exp;
    }

    public override Expression<Func<T, bool>> BuildExpression()
    {
        var exp = _exp.BuildExpression();
        if (exp.ToString() == ShowAll.ToString()) return ShowAll;

        var param = exp.Parameters[0];
        return Expression.Lambda<Func<T, bool>>(Expression.Not(exp.Body), param);
    }
}