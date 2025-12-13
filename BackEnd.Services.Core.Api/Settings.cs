using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Services.Core.Api
{
    public class Settings
    {
        public string NotificationServiceUrl { get; set; }

        public string NotificationInternalApiKey { get; set; }
        public int PolicySyncFrequencyInSec { get; set; }
        public bool BypassWso2ForConnectingToThingsboard { get; set; }
        public string Wso2TenantKey { get; set; }
        public string DevelopmentThingsBoardUrl { get; set; }
        public string ProductionThingsBoardUrl { get; set; }
        public string ThingsboardAdminUsername { get; set; }
        public string ThingsboardAdminPassword { get; set; }
        public string DevelopmentTenantId { get; set; }
        public string DevelopmentUserId { get; set; }
        
        public string DevelopmentAccessToken { get; set; }
        public string AccessToken { get; set; }

        public IdentitySetting IdentitySetting { get; set; }


        public string BrandName { get; set; }
    }
    public class SmtpSettings
    {
        public string SmtpServer { get; set; }
        public int SmtpServerPort { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FromEmailAddress { get; set; }

    }
    public class IdentitySetting
    {
        public string KeyId { get; set; }
        public string Alg { get; set; }
        public string E { get; set; }
        public string N { get; set; }
        public string Kty { get; set; }
        public string Use { get; set; }
    }
}