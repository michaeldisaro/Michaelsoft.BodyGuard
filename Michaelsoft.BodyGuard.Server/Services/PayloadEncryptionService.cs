using Michaelsoft.BodyGuard.Common.Encryption;
using Michaelsoft.BodyGuard.Common.RequestModels;

namespace Michaelsoft.BodyGuard.Server.Services
{
    public class PayloadEncryptionService
    {

        private readonly CommunicationPayload _communicationPayload;

        public PayloadEncryptionService()
        {
            _communicationPayload = new CommunicationPayload();
        }

        public string PublicParams => _communicationPayload.PublicKeyParams;
        
        public EncryptedPayload Encrypt(dynamic data)
        {
            return _communicationPayload.Encrypt(data);
        }

        public T Decrypt<T>(EncryptedPayload payload) where T : class
        {
            return _communicationPayload.Decrypt<T>(payload);
        }

    }
}