using Ion.IR.ErrorHandling;
using Ion.IR.Instructions;
using Ion.IR.Target;

namespace Ion.IR.Handling
{
    public class CallHandler : InstructionHandler<CallInstruction>
    {
        public override void Handle(IProvider<LlvmBuilder> provider, CallInstruction call)
        {
            // Create the call.
            provider.Target.CreateCall(call.Target, call.ResultIdentifier, call.Arguments.AsLlvmValues());
        }
    }
}
