using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Common.RequestModels;
using Michaelsoft.BodyGuard.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Michaelsoft.BodyGuard.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : Controller
    {

        private readonly PayloadEncryptionService _encryptionService;

        public RegistrationController(PayloadEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
        }

        [HttpPost("[action]")]
        [Produces("application/json")]
        public async Task<EncryptedPayload> Add([FromBody]
                                                EncryptedPayload ePayload)
        {
            var registerUser = _encryptionService.Decrypt<RegisterUser>(ePayload);
            return new EncryptedPayload();
        }

    }
}