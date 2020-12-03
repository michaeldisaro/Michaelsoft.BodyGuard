using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Common.Settings
{
    public class CommonSettings
    {
        [JsonProperty("BaseApplicationPath")]
        public string BaseApplicationPath { get; set; }

        public IdentitySettings IdentitySettings { get; set; } = new IdentitySettings();

        public PasswordSettings PasswordSettings { get; set; } = new PasswordSettings();

    }
}