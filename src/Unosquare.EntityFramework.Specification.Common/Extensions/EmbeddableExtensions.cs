using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace Unosquare.EntityFramework.Specification.Common.Extensions;

public static class EmbeddableExtensions
{
    public static Func<T, TU> Embed<T, TU>(this Selector<T, TU> selector)
    {
        // This is purely for unit testing and is never invoked otherwise.
        return selector.BuildExpression().Compile();
    }

    public static Func<T, bool> Embed<T>(this Specification<T> specification)
    {
        // This is purely for unit testing and is never invoked otherwise.
        return specification.BuildExpression().Compile();
    }

    public static Func<T, TU, bool> Embed<T, TU>(this Specification<T, TU> specification)
    {
        // This is purely for unit testing and is never invoked otherwise.
        return specification.BuildExpression().Compile();
    }

    public static Expression<T> ResolveEmbedded<T>(this Expression<T> exp)
    {
        var visitor = new ResolveEmbeddedVisitor();
        return (Expression<T>)visitor.Visit(exp);
    }

    public static Expression ResolveEmbedded(this MethodCallExpression exp)
    {
        var arguments = new List<Expression>();
        var visitor = new ResolveEmbeddedVisitor();
        foreach (var argument in exp.Arguments)
        {
            arguments.Add(visitor.Visit(argument));
        }

        return Expression.Call(exp.Method, arguments);
    }

    private class MultiParamReplaceVisitor : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, Expression> _replacements;
        private readonly LambdaExpression _expressionToVisit;

        public MultiParamReplaceVisitor(Expression[] parameterValues, LambdaExpression expressionToVisit)
        {
            if (parameterValues.Length != expressionToVisit.Parameters.Count)
                throw new ArgumentException(
                    $"The parameter values count ({parameterValues.Length}) does not match the expression parameter count ({expressionToVisit.Parameters.Count})");

            _replacements = expressionToVisit.Parameters
                .Select((parameter, idx) => new { Idx = idx, Parameter = parameter })
                .ToDictionary(x => x.Parameter, x => parameterValues[x.Idx]);

            _expressionToVisit = expressionToVisit;
        }

        protected override Expression VisitParameter(ParameterExpression node) =>
            (_replacements.TryGetValue(node, out var replacement)
                ? Visit(replacement)
                : base.VisitParameter(node)) ?? Expression.Empty();

        public Expression Replace() =>
            Visit(_expressionToVisit.Body);
    }

    private class ResolveEmbeddedVisitor : ExpressionVisitor
    {
        private const string EmbedMethod = "Embed";

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (!DoesExpressionMatchMethod(node, EmbedMethod)) return base.VisitMethodCall(node);

            var specificationExpression = ExtractEmbeddedExpression(node).Body;
            return Visit(specificationExpression) ?? Expression.Empty();
        }

        protected override Expression VisitInvocation(InvocationExpression node)
        {
            if (node.Expression.NodeType != ExpressionType.Call ||
                !DoesExpressionMatchMethod((MethodCallExpression)node.Expression, EmbedMethod))
                return base.VisitInvocation(node);

            var targetLambda = ExtractEmbeddedExpression((MethodCallExpression)node.Expression);
            var replaceParamsVisitor = new MultiParamReplaceVisitor(node.Arguments.ToArray(), targetLambda);

            return Visit(replaceParamsVisitor.Replace()) ?? Expression.Empty();
        }

        private static bool DoesExpressionMatchMethod(MethodCallExpression node, string methodName) =>
            node.Method.IsGenericMethod && node.Method.Name == methodName;

        private static LambdaExpression ExtractEmbeddedExpression(MethodCallExpression node)
        {
            var embeddedAction = Expression.Lambda(node.Arguments[0]).Compile().DynamicInvoke();
            return RetrieveEmbeddedExpression(embeddedAction);
        }

        private static LambdaExpression RetrieveEmbeddedExpression(object source)
        {
            var expression = source switch
            {
                Primitive.Specification specification => specification.GetExpression(),
                Selector selector => selector.GetExpression(),
                _ => null
            };

            return (LambdaExpression)(expression ?? Expression.Empty());
        }
    }
}