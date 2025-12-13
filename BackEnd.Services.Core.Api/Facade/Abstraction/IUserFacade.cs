using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Services.Core.Api.Domain.Common;
using User = BackEnd.Services.Core.Api.Domain.Common.User;

namespace BackEnd.Services.Core.Api.Facade.Abstraction
{
    public interface IUserFacade
    {
        User AuthenticateByEmail(string email, string password);
    }
}