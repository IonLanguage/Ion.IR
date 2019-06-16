using Ion.IR.ErrorHandling;
using Ion.IR.Instructions;
using Ion.IR.Target;

namespace Ion.IR.Handling
{
    public class CallHandler : InstructionHandler<CallInstruction>
    {
        public override void Handle(IProvider<LlvmBuilder> provider, CallInstruction call)
        {
            // Ensure target function exists on the symbol table.
            if (!provider.SymbolTable.ContainsFunction(call.TargetIdentifier))
            {
                // Append an error.
                provider.NoticeJar.Put(new Error($"Call to undefined function named '{call.TargetIdentifier}' performed"));

                // Do not continue.
                return;
            }

            // Retrieve the function.
            LlvmFunction function = provider.SymbolTable.GetFunction(call.TargetIdentifier);

            // Create the call.
            provider.Target.CreateCall(function, call.Name, call.Arguments.AsLlvmValues());
        }
    }
}
