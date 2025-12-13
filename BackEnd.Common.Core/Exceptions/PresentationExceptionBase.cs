namespace BackEnd.Common.Core.Exceptions
{
    public class PresentationExceptionBase : ExceptionBase, IPresentationException
    {
        public PresentationExceptionBase(string message, BoundedContextCodes boundedContextCode, int exceptionFamilyCode)
            : base(message, ExceptionTypeCodes.PresentationException, boundedContextCode, exceptionFamilyCode)
        {
        }
    }
}