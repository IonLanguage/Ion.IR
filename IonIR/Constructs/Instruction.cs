using System.Collections.Generic;
using System.Text;

namespace Ion.IR.Constructs
{
    public struct Instruction : IConstruct
    {
        public ConstructType ConstructType => ConstructType.Instruction;

        public string Name { get; }

        public IConstruct[] Args { get; }

        public Instruction(string name, IConstruct[] args)
        {
            this.Name = name;
            this.Args = args;
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

            // Loop through all the arguments.
            foreach (IConstruct arg in this.Args)
            {
                // Emit and append the argument.
                cells.Add(arg.Emit());
            }

            // Join cells onto the resulting string, separated by a space.
            return string.Join(" ", cells);
        }
    }
}
