using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Common.HttpModels.Authentication;
using Michaelsoft.BodyGuard.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpPost("[action]")]
        [Produces("application/json")]
        public UserLoginResponse Login([FromBody]
                                       UserLoginRequest request)
        {
            try
            {
                var user = _userService.Access(request.EmailAddress, request.Password);

                var claims = new List<Claim>
                {
                    new Claim("sub", user.Id)
                };
/*
                var roles = await _userManager.GetRolesAsync(user);
                claims.AddRange(roles.Select(r => new Claim("roles", r)));
*/
                var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
                identity.AddClaims(claims);

                HttpContext.User = new ClaimsPrincipal(identity);
                
                return new UserLoginResponse
                {
                    UserId = user.Id
                };
            }
            catch (Exception ex)
            {
                return new UserLoginResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

    }
}