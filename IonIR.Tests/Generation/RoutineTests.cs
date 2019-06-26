using Ion.Engine.Llvm;
using Ion.IR.Constants;
using Ion.IR.Constructs;
using Ion.IR.Handling;
using Ion.IR.Misc;
using Ion.IR.Parsing;
using Ion.IR.Syntax;
using NUnit.Framework;
using Newtonsoft.Json;
using System;

namespace Ion.IR.Tests.Instructions
{
    [TestFixture]
    public class RoutineTests
    {
        [Test]
        public void ParseRoutine()
        {
            string input = TestUtil.GetInput("Routine");

            IrLexer lexer = new IrLexer(input);

            Token[] output = lexer.Tokenize();

            Token[] expected = TestUtil.DeserializeTokensFromOutput("RoutineTokens", "json");

            Assert.AreEqual(expected, JsonConvert.SerializeObject(output));
        }

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
            LlvmModule module = LlvmModule.Create();

            // Create the visitor.
            LlvmVisitor visitor = new LlvmVisitor(module);

            // Invoke the visitor
            visitor.VisitRoutine(routine);

            // Load the expected output.
            string expected = TestUtil.GetOutput("EmptyFunction");

            // Compare results.
            Assert.AreEqual(expected, module.ToString());
        }
    }
}
