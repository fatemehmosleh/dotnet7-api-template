using BackEnd.Common.Core.Exceptions;

namespace BackEnd.Services.Core.Api.Exceptions.Application
{
    public class EntityNotFoundApplicationException : ApplicationExceptionBase
    {
        public EntityNotFoundApplicationException(string message) : base(message, BoundedContextCodes.Core,
            (int) ApplicationExceptionFamilyCodes.EntityNotFound)
        {
        }
    }
}