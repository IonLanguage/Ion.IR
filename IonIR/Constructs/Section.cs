#nullable enable

using Ion.IR.Handling;
using Ion.IR.Misc;

namespace Ion.IR.Constructs
{
    public class Section : Construct
    {
        public override ConstructType ConstructType => ConstructType.Section;

        public string Identifier { get; }

        public Instruction[] Instructions { get; }

        public Section(string identifier, Instruction[] instructions)
        {
            this.Identifier = identifier;
            this.Instructions = instructions;
        }

        public Section(string name) : this(name, new Instruction[] { })
        {
            //
        }

        public override string ToString()
        {
            // Create a new fixed string builder instance.
            FixedStringBuilder builder = new FixedStringBuilder();

            // Emit the header.
            builder.Append($"{this.Identifier}:");

            // Loop through all instructions.
            foreach (Instruction instruction in this.Instructions)
            {
                // Emit instruction to the builder.
                builder.Append(instruction.ToString());
            }

            // Return the resulting string.
            return builder.ToString();
        }

        public override Construct Accept(LlvmVisitor visitor)
        {
            return visitor.VisitSection(this);
        }
    }
}
