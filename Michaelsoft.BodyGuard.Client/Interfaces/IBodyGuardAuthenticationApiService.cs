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
        
    }
}