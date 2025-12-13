using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using BackEnd.Services.Core.Api.Context;
using BackEnd.Services.Core.Api.Domain.Common;
using BackEnd.Services.Core.Api.Facade.Abstraction;
namespace BackEnd.Services.Core.Api.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private const string adminrole = "Admin";
        private readonly CoreContext _context;
        private readonly IUserFacade _userFacade;
        public UserController( CoreContext context,IUserFacade userFacade)
        {
            _userFacade = userFacade;
            _context = context;
        }         

        [HttpPost("Login")]        
        [OpenApiOperation("Login", "Login User", "")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Login(User user)
        {
            var finaluser = _userFacade.AuthenticateByEmail(user.UserName, user.Password);
            if (finaluser == null)
            {
                return NotFound();
            }
            
            return Ok(finaluser);
        }

        
    }
}
