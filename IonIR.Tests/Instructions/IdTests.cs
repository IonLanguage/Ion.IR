using Ion.IR.Constructs;
using NUnit.Framework;

namespace Ion.IR.Tests.Instructions
{
    [TestFixture]
    public class IdTests
    {
        [Test]
        [TestCase("test", "$test")]
        public void Emit(string input, string output)
        {
            // Create the construct.
            Id id = new Id(input);

            // Emit the construct.
            string result = id.Emit();

            // Compare result with expected output.
            Assert.AreEqual(output, result);
        }
    }
}
