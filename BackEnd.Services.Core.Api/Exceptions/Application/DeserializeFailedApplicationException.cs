using BackEnd.Common.Core.Exceptions;

namespace BackEnd.Services.Core.Api.Exceptions.Application
{
    public class DeserializeFailedApplicationException : ApplicationExceptionBase
    {
        public DeserializeFailedApplicationException(string message) : base(message, BoundedContextCodes.Core,
            (int)ApplicationExceptionFamilyCodes.DeserializeFailed)
        {
        }
    }
}