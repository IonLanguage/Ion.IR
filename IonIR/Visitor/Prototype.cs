using System.Collections.Generic;

namespace Ion.IR.Visitor
{
    public class Prototype : Expr
    {
        public string Identifier { get; }

        public List<string> Arguments { get; }

        public Prototype(string identifier, List<string> arguments)
        {
            this.Identifier = identifier;
            this.Arguments = arguments;
        }
    }
}
