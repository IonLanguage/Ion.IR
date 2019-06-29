using System;
using System.Collections.Generic;
using System.IO;
using Ion.IR.Syntax;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Ion.IR.Tests
{
    public static class TestUtil
    {
        public const string InputSubpath = "Input";

        public const string OutputSubpath = "Output";

        public const string InputExt = "iox";

        public const string OutputExt = "ll";

        public static readonly string BasePath = Path.GetFullPath("../../../Data");

        public static string ResolveSubpath(string subpath)
        {
            return Path.Join(TestUtil.BasePath, subpath);
        }

        public static string GetData(string subpath)
        {
            // Resolve the path.
            string path = TestUtil.ResolveSubpath(subpath);

            // Read the file.
            string result = File.ReadAllText(path).Trim();

            // Return the resulting content.
            return result;
        }

        public static string GetInput(string fileName, string extension = TestUtil.InputExt)
        {
            return TestUtil.GetData(Path.Join(TestUtil.InputSubpath, $"{fileName}.{extension}"));
        }

        public static string GetOutput(string fileName, string extension = TestUtil.OutputExt)
        {
            return TestUtil.GetData(Path.Join(TestUtil.OutputSubpath, $"{fileName}.{extension}"));
        }

        public static Token[] DeserializeTokens(string content)
        {
            return JsonConvert.DeserializeObject<List<Token>>(content).ToArray();
        }

        public static Token[] DeserializeTokensFromInput(string fileName, string extension = TestUtil.InputExt)
        {
            return TestUtil.DeserializeTokens(TestUtil.GetInput(fileName, extension));
        }

        public static Token[] DeserializeTokensFromOutput(string fileName, string extension = TestUtil.OutputExt)
        {
            return TestUtil.DeserializeTokens(TestUtil.GetOutput(fileName, extension));
        }

        public static void AssertTokens(Token[] expected, Token[] output)
        {
            Assert.AreEqual(expected.Length, output.Length);

            int i = 0;

            foreach (Token token in expected)
            {
                Assert.AreEqual(token.Value, output[i].Value);
                Assert.AreEqual(token.Type, output[i].Type);
                Assert.AreEqual(token.StartPos, output[i].StartPos);
                i++;
            }
        }
    }
}
