using System.Collections.Generic;
using System.Text;
using Ion.IR.Constants;
using Ion.IR.Misc;

namespace Ion.IR.Constructs
{
    public class Routine : IConstruct
    {
        public string Name { get; }

        public Instruction[] Instructions { get; }

        public Routine(string name, Instruction[] instructions)
        {
            this.Name = name;
            this.Instructions = instructions;
        }

        public Routine(string name, List<Instruction> instructions) : this(name, instructions.ToArray())
        {
            //
        }

        public Routine(string name) : this(name, new Instruction[] { })
        {
            //
        }

        public string Emit()
        {
            // Create a new fixed string builder instance.
            FixedStringBuilder builder = new FixedStringBuilder();

            // Append the routine's header.
            builder.Append($"{Symbol.RoutinePrefix}{this.Name}");

            // Emit instructions.
            foreach (Instruction instruction in this.Instructions)
            {
                // Emit and append the instruction.
                builder.Append(instruction.Emit());
            }

            // Trim and return the resulting string.
            return builder.ToString().Trim();
        }
    }
}
