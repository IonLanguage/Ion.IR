namespace Ion.IR.Constructs
{
    public interface IConstruct
    {
        ConstructType Type { get; }

        string Emit();
    }
}
