namespace Ion.IR.Constructs
{
    public abstract class EmptyConstruct : IConstruct
    {
        public abstract ConstructType ConstructType { get; }

        public string Emit()
        {
            return string.Empty;
        }
    }
}
