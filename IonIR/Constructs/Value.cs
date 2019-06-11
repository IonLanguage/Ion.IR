namespace Ion.IR.Constructs
{
    public struct Value : IConstruct
    {
        public ConstructType ConstructType => ConstructType.Value;

        public Kind Kind { get; }

        public string Content { get; }

        public Value(Kind kind, string content)
        {
            this.Kind = kind;
            this.Content = content;
        }

        public string Emit()
        {
            // TODO: Hard-coded symbols.
            return $"({this.Kind.Emit()}){this.Content}";
        }
    }
}
