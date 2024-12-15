namespace CleverAlbumDesigner.Exceptions
{
    public class OperationException : Exception
    {
        public OperationException(string message, Exception innerException)
       : base($"{message} Error Details: {innerException.Message}", innerException) { }

        public OperationException(string message) : base(message) { }
    }
}
