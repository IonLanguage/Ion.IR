using System.Collections.Generic;
using System.Text;
using Ion.IR.Constants;
using Ion.IR.Misc;

namespace Ion.IR.Constructs
{
    public struct RoutineOptions
    {
        public string Name { get; set; }

        public Type[] Args { get; set; }

        public Type ReturnType { get; set; }

        public Instruction[] Instructions { get; set; }
    }

    public class Routine : IConstruct
    {
        public static readonly RoutineOptions DefaultOptions = new RoutineOptions
        {
            Name = SpecialName.Unknown,
            Args = new Type[] { },
            Instructions = new Instruction[] { },
            ReturnType = TypeFactory.Void
        };

        public string Name { get; }

        public Type[] Args { get; }

        public Type ReturnType { get; }

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
            foreach (Type arg in this.Args)
            {
                // Emit argument and store in the buffer.
                argsBuffer.Add(arg.Emit());
            }

            // Convert buffer to an array.
            string[] argArray = argsBuffer.ToArray();

            // Join arguments.
            string args = string.Join(", ", argArray);

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
