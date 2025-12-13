using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace BackEnd.Common.AspNetCore.Security
{
    public static class HttpContextExtension
    {
        public static JwtData GetUserDataFromJwtToken(this HttpContext context)
        {
            string authorization = context.Request.Headers["X-JWT-Assertion"];

            if (string.IsNullOrEmpty(authorization)) return null;



            var handler = new JwtSecurityTokenHandler();
            //var jsonToken = handler.ReadToken(authorization.Substring("Bearer ".Length).Trim());
            var jsonToken = handler.ReadToken(authorization);
            if (jsonToken is JwtSecurityToken tokens)
            {
                var country = tokens.Claims.FirstOrDefault(s => s.Type.Equals("country"))?.Value;
                var sub = tokens.Claims.FirstOrDefault(s => s.Type.Equals("sub"))?.Value;
                var applicationUuId = tokens.Claims
                    .FirstOrDefault(s => s.Type.Equals("applicationUUId"))?.Value;
                var endUser = tokens.Claims.FirstOrDefault(s => s.Type.Equals("enduser"))?.Value;
                var roles = tokens.Claims.Where(s => s.Type.Equals("roles")).Select(s => s.Value)
                    .ToList();
                var endUserTenantId = tokens.Claims
                    .FirstOrDefault(s => s.Type.Equals("enduserTenantId"))?.Value;
                var iss = tokens.Claims.FirstOrDefault(s => s.Type.Equals("iss"))?.Value;
                var mobile = tokens.Claims.FirstOrDefault(s => s.Type.Equals("mobile"))?.Value;
                var userType = tokens.Claims.FirstOrDefault(s => s.Type.Equals("usertype"))?.Value;
                var emailAddress = tokens.Claims.FirstOrDefault(s => s.Type.Equals("emailaddress"))
                    ?.Value;
                var userid = tokens.Claims.FirstOrDefault(s => s.Type.Equals("userid"))?.Value;
                var version = tokens.Claims.FirstOrDefault(s => s.Type.Equals("version"))?.Value;
                var applicationName = tokens.Claims
                    .FirstOrDefault(s => s.Type.Equals("applicationname"))?.Value;
                var lastname = tokens.Claims.FirstOrDefault(s => s.Type.Equals("lastname"))?.Value;
                var aud = tokens.Claims.FirstOrDefault(s => s.Type.Equals("aud"))?.Value;
                var apiName = tokens.Claims.FirstOrDefault(s => s.Type.Equals("apiname"))?.Value;
                var givenName = tokens.Claims.FirstOrDefault(s => s.Type.Equals("givenname"))?.Value;
                var organization = tokens.Claims.FirstOrDefault(s => s.Type.Equals("organization"))
                    ?.Value;
                var applicationId = tokens.Claims.FirstOrDefault(s => s.Type.Equals("applicationid"))
                    ?.Value;
                var exp = tokens.Claims.FirstOrDefault(s => s.Type.Equals("exp"))?.Value;
                var username = tokens.Claims.FirstOrDefault(s => s.Type.Equals("username"))?.Value;


                return new JwtData(country, sub, applicationUuId, endUser, roles,
                    endUserTenantId, iss, mobile, userType, emailAddress, userid, version,
                    applicationName, lastname, aud, apiName, givenName, organization, applicationId,
                    int.Parse(exp), username);
            }

            return null;
        }
    }
}