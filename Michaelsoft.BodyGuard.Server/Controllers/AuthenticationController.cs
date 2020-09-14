using System;
using System.Text.Json;
using Michaelsoft.BodyGuard.Common.Extensions;
using Michaelsoft.BodyGuard.Common.HttpModels.Authentication;
using Michaelsoft.BodyGuard.Server.DatabaseModels;
using Michaelsoft.BodyGuard.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Michaelsoft.BodyGuard.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {

        private readonly UserService _userService;

        private readonly DatabaseEncryptionService _encryptionService;

        public AuthenticationController(UserService userService,
                                        DatabaseEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
            _userService = userService;
        }

        [HttpGet("[action]/{id}")]
        [Produces("application/json")]
        public UserDataResponse UserData(string id)
        {
            var user = _userService.Get(id);

            var data = _encryptionService.Decrypt(user.EncryptedData);

            return new UserDataResponse
            {
                Id = id,
                Data = data
            };
        }

        [HttpPost("[action]")]
        [Produces("application/json")]
        public UserCreateResponse UserCreate([FromBody]
                                             UserCreateRequest userCreateRequest)
        {
            var user = new User
            {
                HashedEmail = userCreateRequest.EmailAddress.Sha1(),
                HashedPassword = userCreateRequest.Password.Sha1(),
                EncryptedData = _encryptionService.Encrypt(userCreateRequest.UserData.ToString()),
                Created = DateTime.Now,
                Updated = DateTime.Now
            };

            var inserted = _userService.Create(user);

            return new UserCreateResponse
            {
                Id = inserted.Id
            };
        }

    }
}