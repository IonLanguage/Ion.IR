using Ion.IR.Constructs;
using Ion.IR.Instructions;

namespace Ion.IR.Parsing
{
    public class CallSubparser : ISubInstructionParser
    {
        public void SubParse(string name, IConstruct[] inputs)
        {
            // Create the call instruction.
            CallInstruction call = new CallInstruction();
        }
    }
}
