using Ion.IR.Constructs;

namespace Ion.IR.Parsing
{
    public interface ISubInstructionParser
    {
        void SubParse(string name, IConstruct[] inputs);
    }
}
