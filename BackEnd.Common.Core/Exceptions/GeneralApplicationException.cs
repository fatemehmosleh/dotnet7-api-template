namespace BackEnd.Common.Core.Exceptions
{
    public class GeneralApplicationException : ApplicationExceptionBase
    {
        public GeneralApplicationException(string message)
            : base(message, BoundedContextCodes.Global, 0)
        {

        }
    }
}
