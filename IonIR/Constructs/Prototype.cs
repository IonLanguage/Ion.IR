using System.Collections.Generic;
using Ion.IR.Constants;
using Ion.IR.Handling;

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
            // Emit the arguments.
            List<string> argsBuffer = new List<string>();

            // Loop through all arguments.
            foreach ((Kind kind, Reference reference) in this.Arguments)
            {
                // Emit argument and store in the buffer.
                argsBuffer.Add($"{kind.ToString()} {reference.ToString()}");
            }

            // Convert buffer to an array.
            string[] argArray = argsBuffer.ToArray();

            // TODO: Comma is hard-coded.
            // Join arguments.
            string arguments = string.Join(", ", argArray).Trim();

            // TODO: Same hard-coded for parentheses.
            // Return the resulting string.
            return $"{this.ReturnKind.ToString()} {Symbol.RoutinePrefix}{this.Identifier}({arguments})";
        }

        public override Construct Accept(LlvmVisitor visitor)
        {
            return visitor.VisitPrototype(this);
        }
    }
}
