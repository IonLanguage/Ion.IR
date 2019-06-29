using Ion.IR.Constructs;

namespace Ion.IR.Constants
{
    public static class KindFactory
    {
        public static Kind Void => new Kind(KindType.Void);

        public static Kind Int32 => new Kind(KindType.Integer32);

        public static Kind Int8 => new Kind(KindType.Integer8);
    }
}
