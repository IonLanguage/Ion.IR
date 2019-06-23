using System;
using System.IO;

namespace Ion.IR.Tests
{
    public static class TestUtil
    {
        public const string InputSubpath = "Input";

        public const string OutputSubpath = "Output";

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

        public static string GetInput(string fileName)
        {
            return TestUtil.GetData(Path.Join(TestUtil.InputSubpath, fileName));
        }

        public static string GetOutput(string fileName)
        {
            return TestUtil.GetData(Path.Join(TestUtil.OutputSubpath, $"{fileName}.{TestUtil.OutputExt}"));
        }
    }
}
