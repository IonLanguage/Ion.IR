using System;
using Ion.Engine.Tracking;
using LLVMSharp;

namespace Ion.IR.Target
{
    public class LlvmValue : LlvmWrapper<LLVMValueRef>, IPointerAware, INamed
    {
        public IntPtr Pointer => this.reference.Pointer;

        public bool IsNull => Util.IsPointerNull(this.Pointer);

        public LLVMValueKind Kind => LLVM.GetValueKind(this.reference);

        public string Name { get; }

        public LlvmValue(LLVMValueRef reference) : base(reference)
        {
            this.Name = LLVM.GetValueName(reference);
        }

        public void SetName(string name)
        {
            // Ensure name is not null nor empty.
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Name cannot be null nor empty");
            }

            // Set the name.
            LLVM.SetValueName(this.reference, name);
        }

        public new LlvmType GetType()
        {
            // Create the reference.
            LLVMTypeRef reference = LLVM.TypeOf(this.reference);

            // Wrap and return the reference.
            return new LlvmType(reference);
        }
    }
}
