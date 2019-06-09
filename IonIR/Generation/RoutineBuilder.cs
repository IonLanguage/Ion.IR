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
            // Create the default routine options.
            RoutineOptions options = Routine.DefaultOptions;

            // Modify the routine's name.
            options.Name = this.Name;

            // Create and return the routine.
            return new Routine(options);
        }
    }
}
