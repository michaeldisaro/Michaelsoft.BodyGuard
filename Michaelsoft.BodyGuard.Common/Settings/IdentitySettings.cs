using System.Collections.Generic;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Common.Settings
{
    public class IdentitySettings
    {

        [JsonProperty("AdditionalClaims")]
        public List<string> AdditionalClaims { get; set; }

        [JsonProperty("CustomRoles")]
        public List<string> CustomRoles { get; set; }

        [JsonProperty("EnabledUserDataProperties")]
        public List<string> EnabledUserDataProperties { get; set; }

    }
}