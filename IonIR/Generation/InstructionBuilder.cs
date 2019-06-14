using System.Collections.Generic;
using Ion.IR.Constructs;

namespace Ion.IR.Generation
{
    public class InstructionBuilder : IBuilder<Instruction>
    {
        public string Name { get; set; }

        public List<IConstruct> Inputs { get; set; }

        public Instruction Build()
        {
            return new Instruction(this.Name, this.Inputs.ToArray());
        }
    }
}
