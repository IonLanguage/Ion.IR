using Ion.IR.Instructions;
using Ion.IR.Target;

namespace Ion.IR.Handling
{
    public class CreateHandler : InstructionHandler<CreateInstruction>
    {
        public override void Handle(IProvider<LlvmBuilder> context, CreateInstruction create)
        {
            // Create the alloca instruction.
            context.Target.CreateAlloca(create.Kind.AsLlvmType(), create.ResultIdentifier);
        }
    }
}
