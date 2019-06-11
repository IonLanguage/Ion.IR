using Ion.IR.Constants;
using Ion.IR.Constructs;
using NUnit.Framework;

namespace Ion.IR.Tests.Instructions
{
    [TestFixture]
    public class RoutineTests
    {
        [Test]
        [TestCase("test", ":void @test()")]
        public void Simple(string input, string output)
        {
            // Create the construct.
            Routine routine = new Routine(new RoutineOptions
            {
                Args = new Kind[] { },
                Instructions = new Instruction[] { },
                Name = input,
                ReturnType = TypeFactory.Void
            });

            // Emit the construct.
            string result = routine.Emit();

            // Compare result with expected output.
            Assert.AreEqual(output, result);
        }

        [Test]
        [TestCase("test", "inst", ":void @test()\ninst")]
        public void WithSimpleInstruction(string name, string instructionName, string output)
        {
            // Create the construct.
            Routine routine = new Routine(new RoutineOptions
            {
                Args = new Kind[] { },
                Name = name,
                ReturnType = TypeFactory.Void,

                Instructions = new Instruction[] {
                    new Instruction(instructionName)
                },
            });

            // Emit the construct.
            string result = routine.Emit();

            // Compare result with expected output.
            Assert.AreEqual(output, result);
        }
    }
}
