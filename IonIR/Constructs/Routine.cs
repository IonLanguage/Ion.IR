using System.Collections.Generic;
using Ion.IR.Constants;
using Ion.IR.Misc;

namespace Ion.IR.Constructs
{
    public class Routine : Construct
    {
        public override ConstructType ConstructType => ConstructType.Routine;

        public Prototype Prototype { get; }

        public Section Body { get; }

        public Routine(Prototype prototype, Section body)
        {
            this.Prototype = prototype;
            this.Body = body;
        }

        public override string ToString()
        {
            // Create a new fixed string builder instance.
            FixedStringBuilder builder = new FixedStringBuilder();

            // Emit the arguments.
            List<string> argsBuffer = new List<string>();

            // ----------- TODO: Migrate emit code to Prototype class -------------
            // Loop through all arguments.
            foreach ((Kind kind, Reference reference) in this.Args)
            {
                // Emit argument and store in the buffer.
                argsBuffer.Add($"{kind.ToString()} {reference.ToString()}");
            }

            // Convert buffer to an array.
            string[] argArray = argsBuffer.ToArray();

            // TODO: Comma is hard-coded.
            // Join arguments.
            string args = string.Join(", ", argArray).Trim();

            // TODO: Same hard-coded for parentheses.
            // Append the routine's header.
            builder.Append($"{this.ReturnKind.ToString()} {Symbol.RoutinePrefix}{this.Name}({args})");

            // Emit and append the body.
            builder.Append(this.Body.ToString());

            // Trim and return the resulting string.
            return builder.ToString().Trim();
        }
    }
}
