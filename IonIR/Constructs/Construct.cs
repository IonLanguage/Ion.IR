using Ion.IR.Handling;
using Ion.IR.Misc;

namespace Ion.IR.Constructs
{
    public interface IConstruct
    {
        ConstructType ConstructType { get; }
    }

    public abstract class Construct : Taggable, IVisitable<Construct, LlvmVisitor>
    {
        public abstract ConstructType ConstructType { get; }

        public override abstract string ToString();

        public virtual Construct Accept(LlvmVisitor visitor)
        {
            return visitor.VisitExtension(this);
        }

        public virtual Construct VisitChildren(LlvmVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
