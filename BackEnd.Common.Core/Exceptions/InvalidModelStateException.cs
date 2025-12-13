namespace BackEnd.Common.Core.Exceptions
{
    public class InvalidModelStateException : PresentationExceptionBase
    {
        public InvalidModelStateException(string message = "Invalid Request") : base(message, BoundedContextCodes.Global, 1)
        {
        }
    }
}