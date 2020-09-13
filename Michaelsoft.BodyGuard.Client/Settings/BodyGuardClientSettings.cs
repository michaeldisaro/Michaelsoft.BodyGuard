namespace Michaelsoft.BodyGuard.Client.Settings
{
    public class BodyGuardClientSettings : IBodyGuardClientSettings
    {

        public string BasePath { get; set; }

    }

    public interface IBodyGuardClientSettings
    {

        public string BasePath { get; set; }

    }
}