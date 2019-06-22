using System.Collections.Generic;
using Ion.IR.Constructs;

namespace Ion.IR.Visitor
{
    public class Prototype : Construct
    {
        public override ConstructType ConstructType => ConstructType.Prototype;

        public string Identifier { get; }

        public List<string> Arguments { get; }

        public Prototype(string identifier, List<string> arguments)
        {
            this.Identifier = identifier;
            this.Arguments = arguments;
        }

        public override string ToString()
        {
            // TODO: Implement.
            throw new System.NotImplementedException();
        }
    }
}
