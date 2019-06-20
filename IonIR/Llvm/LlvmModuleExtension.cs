using Ion.Engine.Llvm;
using Ion.IR.Handling;

namespace Ion.IR.Llvm
{
    public static class LlvmModuleExtension
    {
        public static Router<T> CreateRouter<T>(this LlvmModule module)
        {
            return new Router<T>(module);
        }
    }
}
