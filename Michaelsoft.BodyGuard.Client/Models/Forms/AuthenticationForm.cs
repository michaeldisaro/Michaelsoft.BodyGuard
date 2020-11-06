using Michaelsoft.BodyGuard.Common.HttpModels.Authentication;

namespace Michaelsoft.BodyGuard.Client.Models.Forms
{
    public class AuthenticationForm
    {

        public UserLoginRequest LoginRequest { get; set; }

        public string SuccessUrl { get; set; }

        public string FailureUrl { get; set; }

        public string LoginLabel { get; set; } = "Login";

        public string LogoutLabel { get; set; } = "Logout";

        public string UserClaim { get; set; }

        public string UserMessage { get; set; } = "Welcome:";

    }
}