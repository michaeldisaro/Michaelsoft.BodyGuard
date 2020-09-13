using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Michaelsoft.BodyGuard.Common.RequestModels;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Common.Encryption
{
    /// <summary>
    /// Encrypt/Decrypt communication payload with RSA encryption algorithm
    /// Keys are on BodyGuard.Server and only public key is disclosed
    /// </summary>
    public class CommunicationPayload
    {

        private readonly RSACryptoServiceProvider _rsa;

        public string PublicKeyParams => _rsa.ToXmlString(false);

        public CommunicationPayload()
        {
            var cspParams = new CspParameters {KeyContainerName = "bodyguard"};
            _rsa = new RSACryptoServiceProvider(2048, cspParams);
        }

        public EncryptedPayload Encrypt<T>(T data,
                                           bool encryptWithPrivateKey = true,
                                           string xmlParams = null) where T : class
        {
            var json = JsonConvert.SerializeObject(data);

            var aes = new AesManaged();
            aes.GenerateKey();
            aes.GenerateIV();

            xmlParams ??= _rsa.ToXmlString(encryptWithPrivateKey);

            var encryptedKey = RsaHelper.EncryptStringToBytes_Rsa
                (EncodingHelper.ToSafeUrlBase64(aes.Key), xmlParams, false);

            var encryptedIv = RsaHelper.EncryptStringToBytes_Rsa
                (EncodingHelper.ToSafeUrlBase64(aes.IV), xmlParams, false);

            var encryptedData = AesHelper.EncryptStringToBytes_Aes(json, aes.Key, aes.IV);

            return new EncryptedPayload
            {
                AsymmetricEncryptedKey = EncodingHelper.ToSafeUrlBase64(encryptedKey),
                AsymmetricEncryptedIv = EncodingHelper.ToSafeUrlBase64(encryptedIv),
                SymmetricEncryptedData = EncodingHelper.ToSafeUrlBase64(encryptedData)
            };
        }

        public T Decrypt<T>(EncryptedPayload payload,
                            bool decryptWithPrivateKey = false,
                            string xmlParams = null) where T : class
        {
            xmlParams ??= _rsa.ToXmlString(decryptWithPrivateKey);

            var decryptedKey =
                EncodingHelper
                    .FromSafeUrlBase64(RsaHelper
                                           .DecryptStringFromBytes_Rsa(EncodingHelper.FromSafeUrlBase64(payload.AsymmetricEncryptedKey),
                                                                       xmlParams, false));

            var decryptedIv =
                EncodingHelper
                    .FromSafeUrlBase64(RsaHelper
                                           .DecryptStringFromBytes_Rsa(EncodingHelper.FromSafeUrlBase64(payload.AsymmetricEncryptedIv),
                                                                       xmlParams, false));

            var decryptedData =
                AesHelper.DecryptStringFromBytes_Aes(EncodingHelper.FromSafeUrlBase64(payload.SymmetricEncryptedData),
                                                     decryptedKey, decryptedIv);

            return JsonConvert.DeserializeObject<T>(decryptedData);
        }

    }
}