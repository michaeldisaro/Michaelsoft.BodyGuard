namespace Michaelsoft.BodyGuard.Client.Settings
{
    public class BodyGuardClientSettings : IBodyGuardClientSettings
    {

        public string ServerBasePath { get; set; }
        
        public string ApplicationBasePath { get; set; }

    }

    public interface IBodyGuardClientSettings
    {

        public string ServerBasePath { get; set; }
        
        public string ApplicationBasePath { get; set; }

    }
}