using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Common.AspNetCore.Security
{
    public class UserData
    {
        internal UserData()
        {

        }

        public UserData(Guid id, string username, string email,
            string phoneNumber, string name, string surname
            
           )
        {
            Id = id;
            Username = username;
            Email = email ?? "";
            PhoneNumber = phoneNumber ?? "";
            Name = name ?? "";
            Surname = surname ?? "";
            
        }

        public Guid Id { get; internal set; }
        public string Username { get; internal set; }
        public string Email { get; internal set; }
        public string PhoneNumber { get; internal set; }
        public string Name { get; internal set; }
        public string Surname { get; internal set; }
        public string Role { get; internal set; }
    }
}
