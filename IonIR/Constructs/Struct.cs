using Ion.IR.Handling;

namespace Ion.IR.Constructs
{
    public class Struct : Construct
    {
        // TODO
        public override ConstructType ConstructType => ConstructType.Struct;

        public override Construct Accept(LlvmVisitor visitor)
        {
            return visitor.VisitStruct(this);
        }

        public override string ToString()
        {
            // TODO
            throw new System.NotImplementedException();
        }
    }
}