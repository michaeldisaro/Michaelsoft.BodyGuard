namespace Michaelsoft.BodyGuard.Server.Settings
{
    public class TokenStoreDatabaseSettings : ITokenStoreDatabaseSettings
    {

        public string TokensCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

    }

    public interface ITokenStoreDatabaseSettings
    {

        public string TokensCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

    }
}