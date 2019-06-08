using Ion.IR.Constructs;
using NUnit.Framework;

namespace Ion.IR.Tests.Instructions
{
    [TestFixture]
    public class RoutineTests
    {
        [TestCase("test", "@test")]
        public void Simple(string input, string output)
        {
            // Create the construct.
            Routine routine = new Routine(input);

            // Emit the construct.
            string result = routine.Emit();

            // Compare result with expected output.
            Assert.AreEqual(output, result);
        }

        [TestCase("test", "inst", "@test\ninst")]
        public void WithSimpleInstruction(string name, string instructionName, string output)
        {
            // Create the construct.
            Routine routine = new Routine(name, new Instruction[] {
                new Instruction(instructionName)
            });

            // Emit the construct.
            string result = routine.Emit();

            // Compare result with expected output.
            Assert.AreEqual(output, result);
        }
    }
}
