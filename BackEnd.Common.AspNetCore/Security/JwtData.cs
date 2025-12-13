using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace BackEnd.Common.AspNetCore.Security
{
    public class JwtData
    {
        public JwtData()
        {
        }

        public JwtData(string country, string sub, string applicationUuid, string endUser, List<string> roles,
            string endUserTenantId, string iss, string mobile, string userType, string emailAddress, string userid,
            string version, string applicationName, string lastname, string aud, string apiName, string givenName,
            string organization, string applicationId, int exp, string username)
        {
            Country = country;
            Sub = sub;
            ApplicationUuid = applicationUuid;
            EndUser = endUser;
            Roles = roles;
            EndUserTenantId = endUserTenantId;
            Iss = iss;
            Mobile = mobile;
            UserType = userType;
            EmailAddress = emailAddress;
            Userid = userid;
            Version = version;
            ApplicationName = applicationName;
            Lastname = lastname;
            Aud = aud;
            ApiName = apiName;
            GivenName = givenName;
            Organization = organization;
            ApplicationId = applicationId;
            Exp = exp;
            Username = username;
        }

        [JsonPropertyName("country")] public string Country { get; set; }

        [JsonPropertyName("sub")] public string Sub { get; set; }

        [JsonPropertyName("addresses")] public string Addresses { get; set; }

        [JsonPropertyName("applicationUUId")] public string ApplicationUuid { get; set; }

        [JsonPropertyName("enduser")] public string EndUser { get; set; }

        [JsonPropertyName("roles")] public List<string> Roles { get; set; }

        [JsonPropertyName("enduserTenantId")] public string EndUserTenantId { get; set; }

        [JsonPropertyName("iss")] public string Iss { get; set; }

        [JsonPropertyName("mobile")] public string Mobile { get; set; }

        [JsonPropertyName("usertype")] public string UserType { get; set; }

        [JsonPropertyName("emailaddress")] public string EmailAddress { get; set; }

        [JsonPropertyName("avatar")] public string Avatar { get; set; }

        [JsonPropertyName("userid")] public string Userid { get; set; }

        [JsonPropertyName("version")] public string Version { get; set; }

        [JsonPropertyName("applicationname")] public string ApplicationName { get; set; }

        [JsonPropertyName("lastname")] public string Lastname { get; set; }

        [JsonPropertyName("aud")] public string Aud { get; set; }

        [JsonPropertyName("apiname")] public string ApiName { get; set; }

        [JsonPropertyName("givenname")] public string GivenName { get; set; }

        [JsonPropertyName("organization")] public string Organization { get; set; }

        [JsonPropertyName("applicationid")] public string ApplicationId { get; set; }

        [JsonPropertyName("exp")] public int Exp { get; set; }

        [JsonPropertyName("username")] public string Username { get; set; }
    }
}