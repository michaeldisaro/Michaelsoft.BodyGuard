using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Michaelsoft.BodyGuard.Common.Enums;
using Michaelsoft.BodyGuard.Common.Extensions;
using Michaelsoft.BodyGuard.Common.HttpModels.Authentication;
using Michaelsoft.BodyGuard.Common.Models;
using Michaelsoft.BodyGuard.Common.Settings;
using Michaelsoft.BodyGuard.Server.Services;
using Michaelsoft.Mailer.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Server.Controllers
{
    [ApiController]
    [Route("/")]
    public class AuthenticationController : Controller
    {

        private readonly UserService _userService;

        private readonly string _baseApplicationPath;

        private readonly IdentitySettings _identitySettings;

        private readonly TokenService _tokenService;

        private readonly IMailer _mailer;

        public AuthenticationController(UserService userService,
                                        TokenService tokenService,
                                        IMailer mailer,
                                        IOptions<CommonSettings> commonSettings)
        {
            _mailer = mailer;
            _tokenService = tokenService;
            _baseApplicationPath = commonSettings.Value.BaseApplicationPath;
            _identitySettings = commonSettings.Value.IdentitySettings;
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

                var tokenValue = (user.Id + StringHelper.RandomString(10, "$*#+%") + DateTime.Now.ToString("O")).Sha1();
                var token = _tokenService.Create
                    (TokenTypes.ConfirmRegistration, tokenValue, user.Id, userCreateRequest.TtlSeconds);
                var confirmRegistrationUrl = _baseApplicationPath +
                                             userCreateRequest.ConfirmRegistrationUrl.Replace("{{token}}", token.Value);
                _mailer.SendMailUsingTemplateAsync
                    (
                     new Dictionary<string, string>
                         {{userCreateRequest.EmailAddress, userCreateRequest.EmailAddress}},
                     "Confirm Registration",
                     "ConfirmRegistration",
                     new Dictionary<string, string> {{"confirmRegistrationUrl", confirmRegistrationUrl}});

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

                do
                {
                    if (!_identitySettings.AdditionalClaims.Any())
                        break;

                    var decryptedData = _userService.GetData(user.Id);
                    if (decryptedData == null)
                        break;

                    var additionalClaims = _identitySettings.AdditionalClaims;
                    var userData = JsonConvert.DeserializeObject<User>(decryptedData);

                    foreach (var ac in additionalClaims)
                    {
                        if (!AdditionalClaims.ToUserProperty.ContainsKey(ac)) continue;
                        var property = AdditionalClaims.ToUserProperty[ac];
                        var value = userData.GetType().GetProperty(property)?.GetValue(userData, null)?.ToString();
                        if (!value.IsNullOrEmpty())
                            claims.Add(new Claim(ac, value));
                    }
                } while (false);

                var roles = user.Roles;
                if (roles != null)
                    claims.AddRange(roles.Select(r => new Claim("roles", r)));

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

        [Authorize]
        [HttpPost("[action]")]
        [Produces("application/json")]
        public UserLogoutResponse Logout()
        {
            try
            {
                HttpContext.User = null;

                return new UserLogoutResponse
                {
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new UserLogoutResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [Produces("application/json")]
        public PasswordRecoveryResponse PasswordRecovery([FromBody]
                                                         PasswordRecoveryRequest passwordRecoveryRequest)
        {
            try
            {
                var user = _userService.GetByEmail(passwordRecoveryRequest.EmailAddress);
                var tokenValue = (user.Id + StringHelper.RandomString(10, "$*#+%") + DateTime.Now.ToString("O")).Sha1();
                var token = _tokenService.Create
                    (TokenTypes.PasswordRecovery, tokenValue, user.Id, passwordRecoveryRequest.TtlSeconds);
                var validateRecoveryUrl = _baseApplicationPath +
                                          passwordRecoveryRequest.ValidateRecoveryUrl.Replace("{{token}}", token.Value);
                _mailer.SendMailUsingTemplateAsync
                    (
                     new Dictionary<string, string>
                         {{passwordRecoveryRequest.EmailAddress, passwordRecoveryRequest.EmailAddress}},
                     "Password Recovery",
                     "PasswordRecovery",
                     new Dictionary<string, string> {{"validateRecoveryUrl", validateRecoveryUrl}});
                return new PasswordRecoveryResponse();
            }
            catch (Exception ex)
            {
                return new PasswordRecoveryResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [Produces("application/json")]
        public ValidateRecoveryResponse ValidateRecovery([FromBody]
                                                         ValidateRecoveryRequest validateRecoveryRequest)
        {
            try
            {
                var user = _userService.GetByEmail(validateRecoveryRequest.EmailAddress);
                var token = _tokenService.GetTokenByTypeUserAndValue(TokenTypes.PasswordRecovery, user.Id,
                                                                     validateRecoveryRequest.Token);
                _userService.UpdatePassword(user.Id, validateRecoveryRequest.NewPassword,
                                            validateRecoveryRequest.NewPasswordConfirm);
                _tokenService.Delete(token.Id);
                return new ValidateRecoveryResponse();
            }
            catch (Exception ex)
            {
                return new ValidateRecoveryResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [Produces("application/json")]
        public RegistrationEmailResponse RegistrationEmail([FromBody]
                                                           RegistrationEmailRequest registrationEmailRequest)
        {
            try
            {
                var user = _userService.GetByEmail(registrationEmailRequest.EmailAddress);
                var tokenValue = (user.Id + StringHelper.RandomString(10, "$*#+%") + DateTime.Now.ToString("O")).Sha1();
                var token = _tokenService.Create
                    (TokenTypes.ConfirmRegistration, tokenValue, user.Id, registrationEmailRequest.TtlSeconds);
                var confirmRegistrationUrl = _baseApplicationPath +
                                             registrationEmailRequest.ConfirmRegistrationUrl.Replace("{{token}}",
                                                 token.Value);
                _mailer.SendMailUsingTemplateAsync
                    (
                     new Dictionary<string, string>
                         {{registrationEmailRequest.EmailAddress, registrationEmailRequest.EmailAddress}},
                     "Confirm Registration",
                     "ConfirmRegistration",
                     new Dictionary<string, string> {{"confirmRegistrationUrl", confirmRegistrationUrl}});
                return new RegistrationEmailResponse();
            }
            catch (Exception ex)
            {
                return new RegistrationEmailResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [Produces("application/json")]
        public ConfirmRegistrationResponse ConfirmRegistration([FromBody]
                                                               ConfirmRegistrationRequest confirmRegistrationRequest)
        {
            try
            {
                var token = _tokenService.GetTokenByTypeAndValue(TokenTypes.ConfirmRegistration,
                                                                 confirmRegistrationRequest.Token);
                _userService.ConfirmUser(token.UserId);
                _tokenService.Delete(token.Id);
                return new ConfirmRegistrationResponse();
            }
            catch (Exception ex)
            {
                return new ConfirmRegistrationResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

    }

}