namespace BackEnd.Common.Core.Exceptions
{
    public abstract class DomainExceptionBase : ExceptionBase, IDomainException
    {
        protected DomainExceptionBase(
            string message,
            BoundedContextCodes boundedContextCode,
            int exceptionFamilyCode)
            : base(message, ExceptionTypeCodes.DomainException, boundedContextCode, exceptionFamilyCode)
        {
        }
    }
}