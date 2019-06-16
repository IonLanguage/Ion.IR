using System;
using Ion.IR.Target;

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

        public override string Emit()
        {
            // TODO: Hard-coded symbols.
            return $"({this.Kind.Emit()}){this.Content}";
        }

        public LlvmValue AsLlvmValue()
        {
            // TODO: Implement.
            throw new NotImplementedException();
        }
    }
}
