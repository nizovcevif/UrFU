using Irony.Parsing;

namespace SystemOfSymbolicMathematics
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Grammar grammar = new ExpressionGrammar(); 
            
            var parser = new Parser(grammar);
            var parseTree = parser.Parse("");
        }
    }
}