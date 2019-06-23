using Ion.Engine.Llvm;
using Ion.IR.Constants;
using Ion.IR.Constructs;
using Ion.IR.Handling;
using NUnit.Framework;

namespace Ion.IR.Tests.Instructions
{
    [TestFixture]
    public class RoutineTests
    {
        [Test]
        public void VisitRoutine()
        {
            // Create the prototype.
            Prototype prototype = new Prototype("test", new (Kind, Reference)[] { }, KindFactory.Void, false);

            // Create the body.
            Section body = new Section("entry", new Instruction[] { });

            // Create the routine.
            Routine routine = new Routine(prototype, body);

            // Create the host module.
            LlvmModule module = LlvmModule.Create("test");

            // Create the visitor.
            LlvmVisitor visitor = new LlvmVisitor(module);

            // Invoke the visitor
            visitor.VisitRoutine(routine);

            // Load the expected output.
            string expected = TestUtil.GetOutput("EmptyRoutine");

            // Compare results.
            Assert.AreEqual(expected, module.ToString());
        }
    }
}
