using System.Collections.Generic;
using System.Text;

namespace Ion.IR.Constructs
{
    public struct Instruction : IConstruct
    {
        public string Name { get; }

        public InstructionArgs? Args { get; }

        public Instruction(string name, InstructionArgs? args = null)
        {
            this.Name = name;
            this.Args = args;
        }

        public string Emit()
        {
            // Create the cells buffer list.
            List<string> cells = new List<string>();

            // Append the name.
            cells.Add(this.Name);

            // Check for existing arguments.
            if (this.Args.HasValue)
            {
                // Emit and append left argument.
                cells.Add(this.Args.Value.Left.Emit());

                // Append right argument if applicable.
                if (this.Args.Value.Right.HasValue)
                {
                    // Emit and append right argument.
                    cells.Add(this.Args.Value.Right.Value.Emit());
                }
            }

            // Join cells onto the resulting string, separated by a space.
            return string.Join(" ", cells);
        }
    }
}
