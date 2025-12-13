namespace BackEnd.Common.Core.Exceptions
{
    public abstract class ApplicationExceptionBase : ExceptionBase, IApplicationException
    {
        protected ApplicationExceptionBase(string message, BoundedContextCodes boundedContextCode, int exceptionFamilyCode)
            : base(message, ExceptionTypeCodes.ApplicationException, boundedContextCode, exceptionFamilyCode)
        {
        }
    }
}