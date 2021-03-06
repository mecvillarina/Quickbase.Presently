﻿namespace Presently.Common.Models
{
    public class AppSettings
    {
        public string JwtSecret { get; set; }
        public string JwtAudience { get; set; }
        public string JwtIssuer { get; set; }
        public string QuickbaseUserToken { get; set; }
        public string QuickbaseRealmHostname { get; set; }
        public string QuickbaseApiBaseUrl { get; set; }
        public string LocationIqApiKey { get; set; }
    }
}
