#nullable enable

using System;
using Ion.IR.Handling;

namespace Ion.IR.Constructs
{
    public class Array : Construct
    {
        public override ConstructType ConstructType => ConstructType.Array;

        public Kind Kind { get; }

        public Value[] Values { get; }

        public Array(Kind kind, Value[] values)
        {
            this.Kind = kind;
            this.Values = values;
        }

        public override string ToString()
        {
            // TODO
            throw new NotImplementedException();
        }

        public override Construct Accept(LlvmVisitor visitor)
        {
            return visitor.VisitArray(this);
        }
    }
}
