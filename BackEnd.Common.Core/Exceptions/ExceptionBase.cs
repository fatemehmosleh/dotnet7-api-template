using System;

namespace BackEnd.Common.Core.Exceptions
{
    public abstract class ExceptionBase : ApplicationException
    {
        protected ExceptionBase(string message, ExceptionTypeCodes exceptionTypeCode,
            BoundedContextCodes boundedContextCode,
            int exceptionFamilyCode) : base(message)
        {
            HResult = (int)exceptionTypeCode + (int)boundedContextCode + exceptionFamilyCode;
        }
    }
}