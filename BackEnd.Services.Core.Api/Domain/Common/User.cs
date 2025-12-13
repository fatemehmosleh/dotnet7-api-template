using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Common.Core.Domain;

namespace BackEnd.Services.Core.Api.Domain.Common
{
    public class User : Entity
    {
        public User()
        {

        }

        public string Phone { get; set; }

        public string Address { get; set; }
        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public DateTime? RefreshTokenDate { get; set; }


        public string Role { get; set; }

        public string GivenName { get; set; }

        public string LastName { get; set; }

        public string Emailaddress { get; set; }



       
    }
}
