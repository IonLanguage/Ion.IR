using System.Collections.Generic;
using Ion.IR.Syntax;

namespace Ion.IR.Misc
{
    public static class Extension
    {
        public static Dictionary<string, TokenType> SortByKeyLength(this Dictionary<string, TokenType> map)
        {
            string[] keys = new string[map.Count];

            map.Keys.CopyTo(keys, 0);

            List<string> keyList = new List<string>(keys);

            // Sort the keys by length.
            keyList.Sort((a, b) =>
            {
                if (a.Length > b.Length) return -1;

                if (b.Length > a.Length) return 1;

                return 0;
            });

            Dictionary<string, TokenType> updated = new Dictionary<string, TokenType>();

            foreach (var item in keyList)
            {
                updated[item] = map[item];
            }

            return updated;
        }
    }
}
