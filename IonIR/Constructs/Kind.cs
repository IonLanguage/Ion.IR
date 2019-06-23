using System;
using Ion.Engine.Llvm;
using Ion.Engine.Tracking;
using Ion.IR.Constants;
using Ion.IR.Handling;

namespace Ion.IR.Constructs
{
    public class Kind : Construct, INamed
    {
        public override ConstructType ConstructType => ConstructType.Type;

        public bool IsVoid => this.Type == KindType.Void;

        public KindType Type { get; }

        public string Name { get; }

        public bool IsPointer { get; }

        public Kind(KindType type, bool isPointer = false)
        {
            // Ensure type is regitered in the reverse type map.
            if (!TokenConstants.kindReverseTypeMap.ContainsKey(type))
            {
                throw new Exception($"Unrecognized type: {type}");
            }

            this.Type = type;
            this.Name = TokenConstants.kindReverseTypeMap[type];
            this.IsPointer = isPointer;
        }

        public Kind(string name, bool isPointer = false) : this(TokenConstants.kindTypeMap[name], isPointer)
        {
            //
        }

        public override string ToString()
        {
            // Create the resulting string.
            string result = $"{Symbol.TypePrefix}{this.Name}";

            // Append the pointer symbol if applicable.
            if (this.IsPointer)
            {
                result += Symbol.Pointer;
            }

            // Return the result.
            return result;
        }

        public override Construct Accept(LlvmVisitor visitor)
        {
            return visitor.VisitKind(this);
        }
    }
}
