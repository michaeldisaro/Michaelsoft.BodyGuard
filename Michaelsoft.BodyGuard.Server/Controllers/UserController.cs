using System;
using System.Collections.Generic;
using System.Net.Http;
using Michaelsoft.BodyGuard.Common.HttpModels.Authentication;
using Michaelsoft.BodyGuard.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Michaelsoft.BodyGuard.Server.Controllers
{
    [ApiController]
    [Route("/")]
    [Authorize]
    public class UserController
    {

        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Users")]
        [Produces("application/json")]
        public UsersDataResponse List()
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

        [HttpGet("User/{id}")]
        [Produces("application/json")]
        public UserDataResponse Get(string id)
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

        [HttpPut("User/{id}")]
        [Produces("application/json")]
        public UserUpdateResponse Update(string id,
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

        [HttpDelete("User/{id}")]
        [Produces("application/json")]
        public UserDeleteResponse Delete(string id)
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