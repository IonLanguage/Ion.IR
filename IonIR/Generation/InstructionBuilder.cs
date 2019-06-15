#nullable enable

using System.Collections.Generic;
using Ion.IR.Constants;
using Ion.IR.Constructs;

namespace Ion.IR.Generation
{
    public class InstructionBuilder : IBuilder<Instruction>
    {
        public string Name { get; set; }

        public List<IConstruct> Inputs { get; set; }

        public InstructionBuilder()
        {
            this.Name = SpecialName.Unknown;
            this.Inputs = new List<IConstruct>();
        }

        public Instruction Build()
        {
            return new Instruction(this.Name, this.Inputs.ToArray());
        }
    }
}
