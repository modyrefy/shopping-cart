using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model.Dto.Configuration
{
    public class SettingConfiguration
    {
        public Logging Logging { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        public Jwt Jwt { get; set; }
        public string AllowedHosts { get; set; }

    }

    public class LogLevel
    {
        public string Default { get; set; }
        public string Microsoft { get; set; }
        public string MicrosoftHostingLifetime { get; set; }
    }
    public class Logging
    {
        public LogLevel LogLevel { get; set; }
    }
    public class ConnectionStrings
    {
        public string Redis { get; set; }
        public string DefaultConnectionString { get; set; }
        public string CacheConnectionString { get; set; }
    }
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
    }
}
