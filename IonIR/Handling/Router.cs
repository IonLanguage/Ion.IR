using System;
using Ion.Engine.Llvm;
using Ion.IR.Constructs;

namespace Ion.IR.Handling
{
    public class Router<T>
    {
        protected readonly LlvmModule module;

        public Router(LlvmModule module)
        {
            // TODO: Ensure T is a derived LlvmWrapper instance.
            // if (typeof(T) != typeof(LlvmWrapper<object>))
            // {

            // }

            this.module = module;
        }

        public void Route(T wrapper, Construct construct)
        {
            // Create the provider.
            Provider<T> provider = new Provider<T>(this.module, wrapper);

            // Wrapper cannot be a module.
            if (wrapper is LlvmModule)
            {
                throw new Exception("Wrapper cannot be a module");
            }
            // Must be placed before value handler check.
            else if (wrapper is LlvmFunction && construct is Routine)
            {
                Provider<LlvmFunction> refinedProvider = provider as Provider<LlvmFunction>;

                // Routine handler.
                if (construct is Routine)
                {
                    new RoutineHandler().Handle(refinedProvider, construct as Routine);
                }
                // Section handler.
                else if (construct is Section)
                {
                    new SectionHandler().Handle(refinedProvider, construct as Section);
                }
                // Invalid construct provided.
                else
                {
                    throw new Exception("Invalid construct provided, expected either routine or section");
                }
            }
            // Unrecognized wrapper.
            else
            {
                throw new Exception("Unrecognized wrapper");
            }
        }
    }
}
