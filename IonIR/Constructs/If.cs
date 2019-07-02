using System;
using Ion.IR.Constructs;
using Ion.IR.Handling;

namespace Ion.Generation
{
    public class If : Construct
    {
        public override ConstructType ConstructType => ConstructType.If;

        public Construct Condition { get; }

        public Section Action { get; }

        public Section Otherwise { get; }

        public If(Construct condition, Section action, Section otherwise = null)
        {
            // Ensure condition and action are set.
            if (condition == null || action == null)
            {
                throw new ArgumentNullException("Neither condition nor action argument may be null");
            }

            // Populate properties.
            this.Condition = condition;
            this.Action = action;
            this.Otherwise = otherwise;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public override Construct Accept(LlvmVisitor visitor)
        {
            return visitor.VisitIf(this);
        }
    }
}
