using System.Security.Cryptography;
using Michaelsoft.BodyGuard.Common.Encryption;
using Microsoft.AspNetCore.Mvc;

namespace Michaelsoft.BodyGuard.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EncryptionController
    {

        [HttpGet("[action]")]
        public string GenerateAesParameters()
        {
            var aes = new AesManaged {KeySize = 256};
            aes.GenerateKey();
            aes.GenerateIV();
            return EncodingHelper.ToSafeUrlBase64(aes.Key) + "\n" + EncodingHelper.ToSafeUrlBase64(aes.IV);
        }

    }
}