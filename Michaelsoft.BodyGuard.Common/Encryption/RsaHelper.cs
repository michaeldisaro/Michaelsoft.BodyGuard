using System.Security.Cryptography;
using System.Text;

namespace Michaelsoft.BodyGuard.Common.Encryption
{
    public static class RsaHelper
    {

        public static byte[] EncryptStringToBytes_Rsa(string plainText,
                                                      string xmlParams,
                                                      bool doOaepPadding)
        {
            try
            {
                var data = Encoding.Unicode.GetBytes(plainText);
                var rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlParams);
                return rsa.Encrypt(data, doOaepPadding);
            }
            catch (CryptographicException)
            {
                return null;
            }
        }

        public static string DecryptStringFromBytes_Rsa(byte[] data,
                                                        string xmlParams,
                                                        bool doOaepPadding)
        {
            try
            {
                var rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlParams);
                var decrypted = rsa.Decrypt(data, doOaepPadding);
                return Encoding.Unicode.GetString(decrypted);
            }
            catch (CryptographicException)
            {
                return null;
            }
        }

    }
}