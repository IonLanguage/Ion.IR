namespace Ion.IR.Constructs
{
    public abstract class EmptyConstruct : Construct
    {
        public override string Emit()
        {
            return string.Empty;
        }
    }
}
