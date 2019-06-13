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
                Args = new (Kind, Reference)[] { },
                Sections = new Section[] { },
                Name = input,
                ReturnKind = KindFactory.Void
            });

            // Emit the construct.
            string result = routine.Emit();

            // Compare result with expected output.
            Assert.AreEqual(output, result);
        }

        [Test]
        [TestCase("test", "inst", ":void @test()\ninst:")]
        public void WithSimpleInstruction(string name, string sectionName, string output)
        {
            // Create the construct.
            Routine routine = new Routine(new RoutineOptions
            {
                Args = new (Kind, Reference)[] { },
                Name = name,
                ReturnKind = KindFactory.Void,

                Sections = new Section[] {
                    new Section(sectionName)
                },
            });

            // Emit the construct.
            string result = routine.Emit();

            // Compare result with expected output.
            Assert.AreEqual(output, result);
        }
    }
}
