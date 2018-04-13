using System;
using System.Linq.Expressions;
using Irony.Parsing;

namespace SystemOfSymbolicMathematics
{
    public class Compiler
    {
        public static Func<double, double> CompileFunction(string functionText)
        {
            var ast = ParserInstance.Parse(functionText);
            var builder = new ExpressionTreeBuilder();
            var expression = builder.CreateFunction(ast.Root);
            var function = expression.Compile();
            return function;
        }
        
        private static readonly Parser ParserInstance = new Parser(ExpressionGrammar.Instance);
    }
}