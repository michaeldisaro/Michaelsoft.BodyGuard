using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Common.HttpModels.Authentication;

namespace Michaelsoft.BodyGuard.Client.Interfaces
{
    public interface IBodyGuardAuthenticationApiService
    {

        public Task<UserCreateResponse> Register(string email,
                                                 string password,
                                                 dynamic userData = null);

        public Task<UserLoginResponse> Login(string email,
                                             string password);

        public Task<UserLogoutResponse> Logout();

        public Task<ValidateRecoveryResponse> ValidateRecovery(string emailAddress,
                                                               string token,
                                                               string newPassword,
                                                               string newPasswordConfirm);

        public Task<PasswordRecoveryResponse> PasswordRecovery(string emailAddress,
                                                               int ttlSeconds,
                                                               string validateRecoveryUrl);

    }
}