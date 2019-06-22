namespace Ion.IR.Constructs
{
    public class Prototype
    {
        public string Identifier { get; }

        public (Kind, Reference)[] Arguments { get; }

        // TODO: Must verify return type to be a type emitter (either Type or PrimitiveType).
        public Kind ReturnKind { get; }

        public Prototype(string identifier, (Kind, Reference)[] arguments, Kind returnKind)
        {
            this.Identifier = identifier;
            this.Arguments = arguments;
            this.ReturnKind = returnKind;
        }
    }
}
