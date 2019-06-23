using Ion.IR.Constants;
using Ion.IR.Constructs;
using Ion.IR.Handling;

namespace Ion.IR.Instructions
{
    public class CreateInst : Instruction
    {
        public string ResultIdentifier { get; }

        public Kind Kind { get; }

        public CreateInst(string resultIdentifier, Kind kind) : base(InstructionName.Create, new IConstruct[]
        {
            new Reference(resultIdentifier),
            kind
        })
        {
            this.ResultIdentifier = resultIdentifier;
            this.Kind = kind;
        }

        public override Construct Accept(LlvmVisitor visitor)
        {
            return visitor.VisitCreateInst(this);
        }
    }
}
