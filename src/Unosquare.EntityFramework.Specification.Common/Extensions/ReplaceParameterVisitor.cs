using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Unosquare.EntityFramework.Specification.Common.Extensions;

[ExcludeFromCodeCoverage]
internal class ReplaceParameterVisitor : ExpressionVisitor
{
    private readonly ParameterExpression _from;
    private readonly ParameterExpression _to;

    public ReplaceParameterVisitor(ParameterExpression from, ParameterExpression to)
    {
        _from = from;
        _to = to;
    }
        
    public override Expression? Visit(Expression? node) =>
        node == _from
            ? _to
            : base.Visit(node);
}