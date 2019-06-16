using Ion.IR.Misc;

namespace Ion.IR.Constructs
{
    public interface IConstruct
    {
        ConstructType ConstructType { get; }

        string Emit();
    }

    public abstract class Construct : Taggable, IConstruct
    {
        public abstract ConstructType ConstructType { get; }

        public abstract string Emit();

        public override string ToString()
        {
            return this.Emit();
        }
    }
}
