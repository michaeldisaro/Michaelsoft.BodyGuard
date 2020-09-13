using Michaelsoft.BodyGuard.Common.Encryption;
using Michaelsoft.BodyGuard.Server.Settings;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Server.Encryption
{
    /// <summary>
    /// Encrypt/Decrypt DB data with AES encryption algorithm
    /// Keys are on BodyGuard.Server and never disclosed
    /// </summary>
    public class Data
    {

        private readonly byte[] _aesKey;

        private readonly byte[] _aesIv;

        public Data(IEncryptionSettings encryptionSettings)
        {
            _aesKey = EncodingHelper.FromSafeUrlBase64(encryptionSettings.DataEncryptionKey);
            _aesIv = EncodingHelper.FromSafeUrlBase64(encryptionSettings.DataEncryptionIv);
        }

        public string Encrypt<T>(T data) where T : class
        {
            var json = JsonConvert.SerializeObject(data);
            var encrypted = AesHelper.EncryptStringToBytes_Aes(json, _aesKey, _aesIv);
            return EncodingHelper.ToSafeUrlBase64(encrypted);
        }

        public T Decrypt<T>(string payload) where T : class
        {
            var encrypted = EncodingHelper.FromSafeUrlBase64(payload);
            var json = AesHelper.DecryptStringFromBytes_Aes(encrypted, _aesKey, _aesIv);
            return JsonConvert.DeserializeObject<T>(json);
        }

    }
}