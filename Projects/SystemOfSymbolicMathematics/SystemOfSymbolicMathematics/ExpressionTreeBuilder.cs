using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Irony.Parsing;

namespace SystemOfSymbolicMathematics
{
    [SuppressMessage("ReSharper", "SwitchStatementMissingSomeCases")]
    public class ExpressionTreeBuilder
    {
        private Binder Binder { get; }
        
        public ExpressionTreeBuilder()
        {
            Binder = new Binder();
        }

        public Expression<Func<double, double>> CreateFunction(ParseTreeNode root)
        {
            var parameter = Expression.Parameter(typeof(double), "x");
            Binder.RegisterParameter(parameter);
            var body = CreateExpression(root);
            var result = Expression.Lambda<Func<double, double>>(body, parameter);
    
            return result;
        }

        private Expression CreateExpression(ParseTreeNode root)
        {
            switch (root.Term.Name)
            {
                case "BinExpr":
                    return CreateBinaryExpression(root);
                case "identifier":
                    return Binder.Resolve(root.Token.Text);
                case "number":
                    return CreateLiteralExpression(Convert.ToDouble(root.Token.Value));
                case "FunctionCall":
                    return CreateCallExpression(root);
            }

            return null;
        }

        private Expression CreateCallExpression(ParseTreeNode root)
        {
            var functionName = root.ChildNodes[0].Token.Text;
            var argument = CreateExpression(root.ChildNodes[1]);
            var method = Binder.ResolveMethod(functionName);
            return Expression.Call(method, argument);
        }

        private static Expression CreateLiteralExpression(double argument)
        {
            return Expression.Constant(argument);
        }

        private Expression CreateBinaryExpression(ParseTreeNode node)
        {
            var left = CreateExpression(node.ChildNodes[0]);
            var right = CreateExpression(node.ChildNodes[2]);

            switch (node.ChildNodes[1].Term.Name)
            {
                case "+":
                    return Expression.Add(left, right);
                case "-":
                    return Expression.Subtract(left, right);
                case "*":
                    return Expression.Multiply(left, right);
                case "/":
                    return Expression.Divide(left, right);
                case "^":
                    return Expression.Power(left, right);
            }

            return null;
        }
    }
}