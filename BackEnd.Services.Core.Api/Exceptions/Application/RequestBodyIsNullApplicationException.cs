using BackEnd.Common.Core.Exceptions;

namespace BackEnd.Services.Core.Api.Exceptions.Application
{
    public class RequestBodyIsNullApplicationException : ApplicationExceptionBase
    {
        public RequestBodyIsNullApplicationException(string message) : base(message, BoundedContextCodes.Core,
            (int)ApplicationExceptionFamilyCodes.RequestBodyIsNull)
        {
        }
    }
}