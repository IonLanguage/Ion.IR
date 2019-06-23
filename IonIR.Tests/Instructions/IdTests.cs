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
            Reference id = new Reference(input);

            // Emit the construct.
            string result = id.ToString();

            // Compare result with expected output.
            Assert.AreEqual(output, result);
        }
    }
}
