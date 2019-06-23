using System.Collections.Generic;
using Ion.Engine.Llvm;
using Ion.IR.Constructs;

namespace Ion.IR.Misc
{
    public static class Util
    {
        public static Reference AsReference(this string value)
        {
            return new Reference(value);
        }
    }
}
