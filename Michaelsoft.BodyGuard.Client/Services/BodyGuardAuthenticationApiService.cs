using System;
using System.Net.Http;
using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Interfaces;
using Michaelsoft.BodyGuard.Client.Settings;
using Michaelsoft.BodyGuard.Common.HttpModels.Authentication;
using Michaelsoft.BodyGuard.Common.Models;
using Microsoft.AspNetCore.Http;

namespace Michaelsoft.BodyGuard.Client.Services
{
    public class BodyGuardAuthenticationApiService : BodyGuardBaseApiService, IBodyGuardAuthenticationApiService
    {

        public BodyGuardAuthenticationApiService(IBodyGuardClientSettings settings,
                                                 IHttpClientFactory httpClientFactory,
                                                 IHttpContextAccessor httpContextAccessor) :
            base(settings, httpClientFactory, httpContextAccessor)
        {
        }

        public async Task<UserCreateResponse> Register(string email,
                                                       string password,
                                                       User userData = null)
        {
            var userCreateRequest = new UserCreateRequest
            {
                EmailAddress = email,
                Password = password,
                PasswordConfirm = password,
                UserData = userData
            };

            var baseApiResult = await PostRequest<UserCreateResponse>("Register", userCreateRequest);

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }

        public async Task<UserLoginResponse> Login(string email,
                                                   string password)
        {
            var userLoginRequest = new UserLoginRequest
            {
                EmailAddress = email,
                Password = password
            };

            var baseApiResult = await PostRequest<UserLoginResponse>("Login", userLoginRequest);

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }

        public async Task<UserLogoutResponse> Logout()
        {
            var baseApiResult = await PostRequest<UserLogoutResponse>("Logout");

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }

        public async Task<ValidateRecoveryResponse> ValidateRecovery(string emailAddress,
                                                                     string token,
                                                                     string newPassword,
                                                                     string newPasswordConfirm)
        {
            var validateRecoveryRequest = new ValidateRecoveryRequest
            {
                EmailAddress = emailAddress,
                Token = token,
                NewPassword = newPassword,
                NewPasswordConfirm = newPasswordConfirm
            };

            var baseApiResult =
                await PostRequest<ValidateRecoveryResponse>("ValidateRecovery", validateRecoveryRequest);

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }

        public async Task<PasswordRecoveryResponse> PasswordRecovery(string emailAddress,
                                                                     int ttlSeconds,
                                                                     string validateRecoveryUrl)
        {
            var passwordRecoveryRequest = new PasswordRecoveryRequest
            {
                EmailAddress = emailAddress,
                TtlSeconds = ttlSeconds,
                ValidateRecoveryUrl = ApplicationBasePath + validateRecoveryUrl
            };

            var baseApiResult =
                await PostRequest<PasswordRecoveryResponse>("PasswordRecovery", passwordRecoveryRequest);

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }

        public async Task<ConfirmRegistrationResponse> ConfirmRegistration(string token)
        {
            var confirmRegistrationRequest = new ConfirmRegistrationRequest
            {
                Token = token
            };

            var baseApiResult =
                await PostRequest<ConfirmRegistrationResponse>("ConfirmRegistration", confirmRegistrationRequest);

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }

        public async Task<RegistrationEmailResponse> SendRegistrationEmail(string emailAddress,
                                                                           int ttlSeconds,
                                                                           string confirmRegistrationUrl)
        {
            var registrationEmailRequest = new RegistrationEmailRequest
            {
                EmailAddress = emailAddress,
                TtlSeconds = ttlSeconds,
                ConfirmRegistrationUrl = ApplicationBasePath + confirmRegistrationUrl
            };

            var baseApiResult =
                await PostRequest<RegistrationEmailResponse>("RegistrationEmail", registrationEmailRequest);

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }

    }
}