using System;
using Ion.IR.Handling;

namespace Ion.IR.Constructs
{
    public class Extern : Construct
    {
        public override ConstructType ConstructType => ConstructType.Extern;

        public Prototype Prototype { get; }

        public Extern(Prototype prototype)
        {
            this.Prototype = prototype;
        }

        public override string ToString()
        {
            // TODO
            throw new NotImplementedException();
        }

        public override Construct Accept(LlvmVisitor visitor)
        {
            return visitor.VisitExtern(this);
        }
    }
}
