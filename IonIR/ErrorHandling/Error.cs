namespace Ion.IR.ErrorHandling
{
    public class Error
    {
        public string Message { get; }

        public Error(string message)
        {
            this.Message = message;
        }
    }
}
