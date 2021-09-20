using System.Collections.Generic;
using MyYamlParser;

namespace MarketingBox.AuthApi.Settings
{
    public class TenantSettings
    {
        [YamlProperty("TenantHostMapping", null)]
        public Dictionary<string, string> TenantHostMapping { get; set; }
    }
}