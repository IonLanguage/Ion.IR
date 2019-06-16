using System;
using Ion.IR.Instructions;
using Ion.IR.Target;

namespace Ion.IR.Handling
{
    public class CallHandler : InstructionHandler<CallInstruction>
    {
        public override void Handle(IProvider<LlvmBuilder> provider, CallInstruction instruction)
        {
            // TODO: Implement.
            throw new NotImplementedException();
        }
    }
}
