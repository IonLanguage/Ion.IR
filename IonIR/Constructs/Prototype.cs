namespace Ion.IR.Constructs
{
    public class Prototype : Construct
    {
        public override ConstructType ConstructType => ConstructType.Prototype;

        public string Identifier { get; }

        public (Kind, Reference)[] Arguments { get; }

        // TODO: Must verify return type to be a type emitter (either Type or PrimitiveType).
        public Kind ReturnKind { get; }

        public bool HasInfiniteArguments { get; }

        public Prototype(string identifier, (Kind, Reference)[] arguments, Kind returnKind, bool hasInfiniteArguments)
        {
            this.Identifier = identifier;
            this.Arguments = arguments;
            this.ReturnKind = returnKind;
            this.HasInfiniteArguments = hasInfiniteArguments;
        }

        public override string ToString()
        {
            // TODO: Implement.
            throw new System.NotImplementedException();
        }
    }
}
