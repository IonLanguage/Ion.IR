namespace Ion.IR.ErrorHandling
{
    public enum NoticeType
    {
        Warning,

        Error
    }

    public interface INotice
    {
        NoticeType Type { get; }

        string Message { get; }
    }

    public abstract class Notice
    {
        public abstract NoticeType Type { get; }

        public string Message { get; }

        public Notice(string message)
        {
            this.Message = message;
        }
    }
}
