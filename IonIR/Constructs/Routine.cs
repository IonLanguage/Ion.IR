using System.Collections.Generic;

namespace Ion.IR.Constructs
{
    public class Routine
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
    }
}
