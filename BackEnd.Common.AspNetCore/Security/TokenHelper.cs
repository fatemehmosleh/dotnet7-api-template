using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Common.AspNetCore.Security
{
    public class TokenHelper
    {
        private const string SecurityKey = "---";

        public static string CreateAccessToken(UserData user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.UserData, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.GivenName, user.Name ?? ""),
                new Claim(ClaimTypes.Surname, user.Surname ?? ""),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber ?? ""),
                //new Claim(ClaimTypes.Role, user.Role),
                //new Claim("tenantId", (user.TenantId ?? Guid.Empty).ToString()),
                //new Claim("teamIds", string.Join(',', user.TeamIds)),
                //new Claim("ownedTeamIds", string.Join(',', user.OwnedTeamIds)),
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = creds
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public static void AddJwtAuthentication(IServiceCollection services)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecurityKey))
                    };
                });
        }

        public static UserData GetUserData(ClaimsPrincipal user)
        {
            var claimsIdentity = user.Identity as ClaimsIdentity;
            Guid tenantId = Guid.Empty;
            var teamIds = new List<Guid>();
            var ownedTeamIds = new List<Guid>();

            Guid.TryParse(claimsIdentity.FindFirst("tenantId")?.Value, out tenantId);
            var teamIdsString = claimsIdentity.FindFirst("teamIds")?.Value;
            if (!string.IsNullOrWhiteSpace(teamIdsString))
                teamIds = teamIdsString.Split(',').Select(t => Guid.Parse(t)).ToList();

            var ownedTeamIdsString = claimsIdentity.FindFirst("ownedTeamIds")?.Value;
            if (!string.IsNullOrWhiteSpace(ownedTeamIdsString))
                ownedTeamIds = ownedTeamIdsString.Split(',').Select(t => Guid.Parse(t)).ToList();

            return new UserData(
                Guid.Parse(claimsIdentity.FindFirst(ClaimTypes.UserData)?.Value),
                claimsIdentity.FindFirst(ClaimTypes.Name)?.Value,
                claimsIdentity.FindFirst(ClaimTypes.Email)?.Value,
                claimsIdentity.FindFirst(ClaimTypes.MobilePhone)?.Value,
                claimsIdentity.FindFirst(ClaimTypes.GivenName)?.Value,
                claimsIdentity.FindFirst(ClaimTypes.Surname)?.Value
                //claimsIdentity.FindFirst(ClaimTypes.Role)?.Value,
                //tenantId,
                //teamIds,
                //ownedTeamIds
                );
        }

        public static string GetRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
