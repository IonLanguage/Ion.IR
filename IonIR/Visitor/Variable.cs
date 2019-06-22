using Ion.IR.Constructs;

namespace Ion.IR.Visitor
{
    public class Variable : Construct
    {
        public override ConstructType ConstructType => ConstructType.Variable;

        public string Name { get; }

        public Variable(string name)
        {
            this.Name = name;
        }

        public override string ToString()
        {
            // TODO: Implement.
            throw new System.NotImplementedException();
        }
    }
}
