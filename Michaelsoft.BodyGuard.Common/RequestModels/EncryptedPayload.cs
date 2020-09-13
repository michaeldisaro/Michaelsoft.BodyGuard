namespace Michaelsoft.BodyGuard.Common.RequestModels
{
    public class EncryptedPayload
    {

        public string SymmetricEncryptedData { get; set; }

        public string AsymmetricEncryptedKey { get; set; }

        public string AsymmetricEncryptedIv { get; set; }

    }
}