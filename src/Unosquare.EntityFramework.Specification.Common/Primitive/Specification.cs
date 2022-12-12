namespace Unosquare.EntityFramework.Specification.Common.Primitive;

public abstract class Specification
{
    public abstract Expression GetExpression();
}

public abstract class Specification<T> : Specification
{
    protected static Expression<Func<T, bool>> ShowAll => x => true;

    protected static Expression<Func<T, bool>> ShowNone => x => false;

    public abstract Expression<Func<T, bool>> BuildExpression();

    public bool IsSatisfy(T item) => BuildExpression().Compile()(item);

    public override Expression GetExpression() => BuildExpression();
}

public abstract class Specification<T, TU> : Specification
{
    protected static Expression<Func<T, TU, bool>> ShowAll => (x, y) => true;

    protected static Expression<Func<T, TU, bool>> ShowNone => (x, y) => false;

    public abstract Expression<Func<T, TU, bool>> BuildExpression();

    public bool IsSatisfy(T firstItem, TU secondItem) =>
        BuildExpression().Compile()(firstItem, secondItem);

    public override Expression GetExpression() =>
        BuildExpression();
}

public class ExpressionSpec<T> : Specification<T>
{
    private readonly Expression<Func<T, bool>>? _exp;

    public ExpressionSpec(Expression<Func<T, bool>>? exp = null) => _exp = exp;

    public override Expression<Func<T, bool>> BuildExpression() => _exp ?? ShowAll;
}