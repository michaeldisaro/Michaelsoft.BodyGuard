using System;
using Michaelsoft.BodyGuard.Common.HttpModels.Authentication;
using Michaelsoft.BodyGuard.Common.HttpModels.Authorization;
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

        [HttpPost("[action]")]
        [Produces("application/json")]
        [Authorize]
        public CanResponse Can([FromBody]
                               CanRequest canRequest)
        {
            try
            {
                _userService.Can(canRequest.Id, canRequest.Roles, canRequest.Claims, canRequest.CanAll);
                return new CanResponse();
            }
            catch (Exception ex)
            {
                return new CanResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [HttpPut("[action]")]
        [Produces("application/json")]
        [Authorize(Roles = "root,admin")]
        public ManageRoleResponse AssignRole([FromBody]
                                             ManageRoleRequest manageRoleRequest)
        {
            try
            {
                _userService.AssignRole(manageRoleRequest.EmailAddress, manageRoleRequest.Role);
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
                _userService.RevokeRole(manageRoleRequest.EmailAddress, manageRoleRequest.Role);
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