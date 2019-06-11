namespace Ion.IR.Constructs
{
    public struct Value : IConstruct
    {
        public ConstructType Type => ConstructType.Value;

        public string Content { get; }

        public Value(string content)
        {
            this.Content = content;
        }

        public string Emit()
        {
            // TODO: Implement.
            return this.Content;
        }
    }
}
