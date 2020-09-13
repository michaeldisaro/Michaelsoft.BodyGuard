using System.Net;
using Michaelsoft.BodyGuard.Client.Services;
using Michaelsoft.BodyGuard.Client.Settings;
using Michaelsoft.BodyGuard.Common.Encryption;
using Michaelsoft.BodyGuard.Common.RequestModels;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Client.Interfaces
{
    public class ConnectionService : IConnectionService
    {

        private string _basePath;

        private string _publicServerXmlParams;

        private CommunicationPayload _communicationPayload;

        public ConnectionService(IBodyGuardClientSettings settings)
        {
            _basePath = settings.BasePath;
            Initialize();
        }

        public void Initialize()
        {
            _publicServerXmlParams = GetPublicRsaParameters();
            if (_communicationPayload != null) return;
            _communicationPayload = new CommunicationPayload();
        }

        private string GetPublicRsaParameters()
        {
            using var client = new WebClient();
            return client.DownloadString($"{_basePath}/Encryption/GetPublicCommunicationRsaParameters");
        }

        public string RegisterUser(string email,
                                   string password,
                                   dynamic userData)
        {
            using var client = new WebClient();

            var registerUser = new RegisterUser
            {
                EmailAddress = email,
                Password = password,
                UserData = userData
            };

            var encryptedRequestPayload = _communicationPayload.Encrypt(registerUser);

            var response = client.UploadString($"{_basePath}/Registration/Add",
                                               JsonConvert.SerializeObject(encryptedRequestPayload));

            return response;
        }

    }
}