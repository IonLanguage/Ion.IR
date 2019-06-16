namespace Ion.IR.ErrorHandling
{
    public class Error : Notice
    {
        public override NoticeType Type => NoticeType.Error;

        public Error(string message) : base(message)
        {
            //
        }
    }
}
