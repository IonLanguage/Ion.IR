using System.Collections.Generic;
using System.Text;
using Ion.IR.Constants;
using Ion.IR.Misc;

namespace Ion.IR.Constructs
{
    public struct RoutineOptions
    {
        public string Name { get; set; }

        public (Kind, Reference)[] Args { get; set; }

        public Kind ReturnType { get; set; }

        public Instruction[] Instructions { get; set; }
    }

    public class Routine : IConstruct
    {
        public readonly ConstructType ConstructType => ConstructType.Routine;

        public static readonly RoutineOptions DefaultOptions = new RoutineOptions
        {
            Name = SpecialName.Unknown,
            Args = new (Kind, Reference)[] { },
            Instructions = new Instruction[] { },
            ReturnType = TypeFactory.Void
        };

        public string Name { get; }

        public (Kind, Reference)[] Args { get; }

        public Kind ReturnType { get; }

        public Instruction[] Instructions { get; }

        public Routine(RoutineOptions options)
        {
            this.Name = options.Name;
            this.Args = options.Args;
            this.ReturnType = options.ReturnType;
            this.Instructions = options.Instructions;
        }

        public string Emit()
        {
            // Create a new fixed string builder instance.
            FixedStringBuilder builder = new FixedStringBuilder();

            // Emit the arguments.
            List<string> argsBuffer = new List<string>();

            // Loop through all arguments.
            foreach ((Kind kind, Reference reference) in this.Args)
            {
                // Emit argument and store in the buffer.
                argsBuffer.Add($"{kind.Emit()} {reference.Emit()}");
            }

            // Convert buffer to an array.
            string[] argArray = argsBuffer.ToArray();

            // TODO: Comma is hard-coded.
            // Join arguments.
            string args = string.Join(", ", argArray).Trim();

            // TODO: Same hard-coded for parentheses.
            // Append the routine's header.
            builder.Append($"{this.ReturnType.Emit()} {Symbol.RoutinePrefix}{this.Name}({args})");

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
