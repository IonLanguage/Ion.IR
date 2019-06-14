using System.Collections.Generic;
using Ion.IR.Constants;
using Ion.IR.Misc;

namespace Ion.IR.Constructs
{
    public struct RoutineOptions
    {
        public string Name { get; set; }

        public (Kind, Reference)[] Args { get; set; }

        public Kind ReturnKind { get; set; }

        public Section[] Sections { get; set; }
    }

    public class Routine : Construct
    {
        public override ConstructType ConstructType => ConstructType.Routine;

        public static readonly RoutineOptions DefaultOptions = new RoutineOptions
        {
            Name = SpecialName.Unknown,
            Args = new (Kind, Reference)[] { },
            Sections = new Section[] { },
            ReturnKind = KindFactory.Void
        };

        public string Name { get; }

        public (Kind, Reference)[] Args { get; }

        public Kind ReturnKind { get; }

        public Section[] Sections { get; }

        public Routine(RoutineOptions options)
        {
            this.Name = options.Name;
            this.Args = options.Args;
            this.ReturnKind = options.ReturnKind;
            this.Sections = options.Sections;
        }

        public override string Emit()
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
            builder.Append($"{this.ReturnKind.Emit()} {Symbol.RoutinePrefix}{this.Name}({args})");

            // Emit sections.
            foreach (Section section in this.Sections)
            {
                // Emit and append the section.
                builder.Append(section.Emit());
            }

            // Trim and return the resulting string.
            return builder.ToString().Trim();
        }
    }
}
