namespace Ion.IR.Constructs
{
    public interface IConstruct
    {
        ConstructType ConstructType { get; }

        string Emit();
    }
}
