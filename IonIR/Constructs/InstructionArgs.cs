namespace Ion.IR.Constructs
{
    public struct InstructionArgs
    {
        public Value Left { get; }

        public Value? Right { get; }

        public InstructionArgs(Value left, Value? right)
        {
            this.Left = left;
            this.Right = right;
        }
    }
}
