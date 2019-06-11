using System.Collections.Generic;
using System.Text;

namespace Ion.IR.Constructs
{
    public struct Instruction : IConstruct
    {
        public ConstructType ConstructType => ConstructType.Instruction;

        public string Name { get; }

        public IConstruct[] Inputs { get; }

        public Instruction(string name, IConstruct[] inputs)
        {
            this.Name = name;
            this.Inputs = inputs;
        }

        public Instruction(string name) : this(name, new IConstruct[] { })
        {
            //
        }

        public string Emit()
        {
            // Create the cells buffer list.
            List<string> cells = new List<string>();

            // Append the name.
            cells.Add(this.Name);

            // Loop through all the inputs.
            foreach (IConstruct input in this.Inputs)
            {
                // Emit and append the argument.
                cells.Add(input.Emit());
            }

            // Join cells onto the resulting string, separated by a space.
            return string.Join(" ", cells);
        }
    }
}
