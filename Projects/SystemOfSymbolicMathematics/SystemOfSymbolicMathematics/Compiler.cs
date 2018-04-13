using System;
using Irony.Parsing;

namespace SystemOfSymbolicMathematics
{
    public static class Compiler
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