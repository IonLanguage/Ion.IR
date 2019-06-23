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

            // Emit the prototype.
            builder.Append(this.Prototype.ToString());

            // Emit and append the body.
            builder.Append(this.Body.ToString());

            // Trim and return the resulting string.
            return builder.ToString().Trim();
        }
    }
}
