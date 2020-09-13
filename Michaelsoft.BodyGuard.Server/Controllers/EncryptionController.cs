using System.Security.Cryptography;
using Michaelsoft.BodyGuard.Common.Encryption;
using Michaelsoft.BodyGuard.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Michaelsoft.BodyGuard.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EncryptionController
    {

        private readonly PayloadEncryptionService _encryptionService;

        public EncryptionController(PayloadEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;

        }

        [HttpGet("[action]")]
        public string GetPublicCommunicationRsaParameters()
        {
            return _encryptionService.PublicParams;
        }

        [HttpGet("[action]")]
        public string GenerateAesParameters()
        {
            var aes = new AesManaged();
            aes.GenerateKey();
            aes.GenerateIV();
            return EncodingHelper.ToSafeUrlBase64(aes.Key) + "\n" + EncodingHelper.ToSafeUrlBase64(aes.IV);
        }

    }
}