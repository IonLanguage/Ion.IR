#nullable enable

using System;
using Ion.IR.Handling;

namespace Ion.IR.Constructs
{
    public class Global : Construct
    {
        public override ConstructType ConstructType => ConstructType.Global;

        public Value? InitialValue { get; }

        public Kind Kind { get; }

        public string Identifier { get; }

        public Global(string identifier, Kind kind, Value? initialValue = null)
        {
            this.Identifier = identifier;
            this.Kind = kind;
            this.InitialValue = initialValue;
        }

        public override string ToString()
        {
            // TODO
            throw new NotImplementedException();
        }

        public override Construct Accept(LlvmVisitor visitor)
        {
            return visitor.VisitGlobal(this);
        }
    }
}
