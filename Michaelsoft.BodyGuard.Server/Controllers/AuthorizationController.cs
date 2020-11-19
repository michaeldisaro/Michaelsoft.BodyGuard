using System;
using Michaelsoft.BodyGuard.Common.HttpModels.Authentication;
using Michaelsoft.BodyGuard.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Michaelsoft.BodyGuard.Server.Controllers
{
    [ApiController]
    [Route("/")]
    [Authorize]
    public class AuthorizationController : Controller
    {

        private readonly UserService _userService;

        public AuthorizationController(UserService userService)
        {
            _userService = userService;

        }

        [HttpPut("[action]")]
        [Produces("application/json")]
        [Authorize(Roles = "root,admin")]
        public ManageRoleResponse AssignRole([FromBody]
                                             ManageRoleRequest manageRoleRequest)
        {
            try
            {
                _userService.AssignRole(manageRoleRequest.UserId, manageRoleRequest.Role);
                return new ManageRoleResponse();
            }
            catch (Exception ex)
            {
                return new ManageRoleResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [HttpPut("[action]")]
        [Produces("application/json")]
        [Authorize(Roles = "root,admin")]
        public ManageRoleResponse RevokeRole([FromBody]
                                             ManageRoleRequest manageRoleRequest)
        {
            try
            {
                _userService.RevokeRole(manageRoleRequest.UserId, manageRoleRequest.Role);
                return new ManageRoleResponse();
            }
            catch (Exception ex)
            {
                return new ManageRoleResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

    }
}