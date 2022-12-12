using System.Diagnostics.CodeAnalysis;

namespace Unosquare.EntityFramework.Specification.Common.Extensions;

[ExcludeFromCodeCoverage]
internal class CombineWithSelectorVisitor : ExpressionVisitor
{
    private readonly ParameterExpression _paramExpr;
    private readonly Expression _memberExpr;

    internal CombineWithSelectorVisitor(ParameterExpression paramExpr, Expression memberExpr) 
    {
        _paramExpr = paramExpr;
        _memberExpr = memberExpr;
    }

    public override Expression? Visit(Expression? p) => base.Visit(p == _paramExpr ? _memberExpr : p);
}