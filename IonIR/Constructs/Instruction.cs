using System;
using System.Collections.Generic;
using System.Linq;

namespace Ion.IR.Constructs
{
    public abstract class Instruction : Construct
    {
        public override ConstructType ConstructType => ConstructType.Instruction;

        public string ResultIdentifier { get; }

        public IConstruct[] Inputs { get; }

        public Instruction(string name, IConstruct[] inputs)
        {
            this.ResultIdentifier = name;
            this.Inputs = inputs;
        }

        public Instruction(string name) : this(name, new IConstruct[] { })
        {
            //
        }

        public override string ToString()
        {
            // Create the cells buffer list.
            List<string> cells = new List<string>();

            // Append the name.
            cells.Add(this.ResultIdentifier);

            // Loop through all the inputs.
            foreach (IConstruct input in this.Inputs)
            {
                // Emit and append the argument.
                cells.Add(input.ToString());
            }

            // Join cells onto the resulting string, separated by a space.
            return string.Join(" ", cells);
        }

        public IConstruct[] GetRange(int from, int amount)
        {
            return this.Inputs.Skip(from).Take(amount).ToArray();
        }

        public IConstruct[] GetFrom(int from)
        {
            return this.Inputs.Skip(from).ToArray();
        }

        public void VerifyArgumentCount(uint count)
        {
            if (this.Inputs.Length != count)
            {
                throw new Exception($"Expected {count} inputs but got {this.Inputs.Length}");
            }
        }
    }
}
