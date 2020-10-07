using System;
using System.Collections.Generic;
using System.Net.Http;
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

        public AuthenticationController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("[action]")]
        [Produces("application/json")]
        public UserCreateResponse UserCreate([FromBody]
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

        [HttpGet("[action]")]
        [Produces("application/json")]
        public UsersDataResponse GetUsersData()
        {
            try
            {
                var usersData = new Dictionary<string, string>();
                var users = _userService.GetAll();
                foreach (var user in users)
                {
                    var data = _userService.GetData(user.Id);
                    usersData.Add(user.Id, data);
                }
                return new UsersDataResponse
                {
                    UsersData = usersData
                };
            }
            catch (Exception ex)
            {
                return new UsersDataResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [HttpGet("[action]/{id}")]
        [Produces("application/json")]
        public UserDataResponse GetUserData(string id)
        {
            try
            {
                var data = _userService.GetData(id);
                return new UserDataResponse
                {
                    Id = id,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new UserDataResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [HttpPut("[action]/{id}")]
        [Produces("application/json")]
        public UserUpdateResponse UpdateUserData(string id,
                                                 [FromBody]
                                                 UserUpdateRequest userUpdateRequest)
        {
            try
            {
                if (!id.Equals(userUpdateRequest.Id))
                    throw new HttpRequestException("User id in path is not equal to user id in body.");
                _userService.UpdateData(
                                        userUpdateRequest.Id,
                                        userUpdateRequest.UserData);
                return new UserUpdateResponse();
            }
            catch (Exception ex)
            {
                return new UserUpdateResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [HttpDelete("[action]/{id}")]
        [Produces("application/json")]
        public UserDeleteResponse UserDelete(string id)
        {
            try
            {
                _userService.Delete(id);
                return new UserDeleteResponse();
            }
            catch (Exception ex)
            {
                return new UserDeleteResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

    }
}