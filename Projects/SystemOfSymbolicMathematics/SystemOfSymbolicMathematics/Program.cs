using Irony.Parsing;

namespace SystemOfSymbolicMathematics
{
    internal static class Program
    {
        public static void Main()
        {
            Grammar grammar = new ExpressionGrammar(); 
            
            var parser = new Parser(grammar);
            var parseTree = parser.Parse("");
            
        }
    }
}