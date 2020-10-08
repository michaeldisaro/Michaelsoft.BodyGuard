using System;
using Michaelsoft.BodyGuard.Common.HttpModels.Authentication;
using Michaelsoft.BodyGuard.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Michaelsoft.BodyGuard.Server.Controllers
{
    [ApiController]
    [Route("/")]
    public class AuthenticationController : Controller
    {

        private readonly UserService _userService;

        public AuthenticationController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("[action]")]
        [Produces("application/json")]
        public UserCreateResponse Register([FromBody]
                                           UserCreateRequest userCreateRequest)
        {
            try
            {
                var user = _userService.Create(
                                               userCreateRequest.EmailAddress,
                                               userCreateRequest.Password,
                                               userCreateRequest.UserData);
                return new UserCreateResponse
                {
                    Id = user.Id
                };
            }
            catch (Exception ex)
            {
                return new UserCreateResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

    }
}