using Ion.IR.Handling;

namespace Ion.IR.Constructs
{
    public class Value : Construct
    {
        public override ConstructType ConstructType => ConstructType.Value;

        public Kind Kind { get; }

        public string Content { get; }

        public Value(Kind kind, string content)
        {
            this.Kind = kind;
            this.Content = content;
        }

        public override string ToString()
        {
            // TODO: Hard-coded symbols.
            return $"({this.Kind.ToString()}){this.Content}";
        }

        public override Construct Accept(LlvmVisitor visitor)
        {
            return visitor.VisitValue(this);
        }
    }
}
