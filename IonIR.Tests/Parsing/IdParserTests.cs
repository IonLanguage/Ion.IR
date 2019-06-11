using Ion.IR.Constructs;
using Ion.IR.Misc;
using Ion.IR.Parsing;
using Ion.IR.Syntax;
using NUnit.Framework;

namespace Ion.IR.Tests.Parsing
{
    [TestFixture]
    public class IdParserTests
    {
        [Test]
        [TestCase("$test", "test")]
        public void Simple(string input, string expectedValue)
        {
            // Create the lexer.
            IrLexer lexer = new IrLexer(input);

            // Invoke the lexer and create the corresponding stream.
            TokenStream stream = new TokenStream(lexer.Tokenize());

            // Create the parser context.
            ParserContext context = new ParserContext(stream);

            // Invoke the parser.
            Reference result = new ReferenceParser().Parse(context);

            // Compare resulting value.
            Assert.AreEqual(expectedValue, result.Value);
        }
    }
}
