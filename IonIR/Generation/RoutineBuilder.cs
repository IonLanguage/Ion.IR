using System.Collections.Generic;
using Ion.IR.Constructs;

namespace Ion.IR.Generation
{
    public class RoutineBuilder : IBuilder<Routine>
    {
        public string Name { get; set; }

        public List<Instruction> Instructions { get; }

        public Routine Build()
        {
            return new Routine(this.Name, this.Instructions);
        }
    }
}
