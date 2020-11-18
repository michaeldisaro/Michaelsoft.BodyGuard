using Michaelsoft.BodyGuard.Common.HttpModels.Authentication;

namespace Michaelsoft.BodyGuard.Client.Models.Forms
{
    public class AuthenticationForm
    {

        public UserLoginRequest LoginRequest { get; set; }

        public string LoginSuccessUrl { get; set; }

        public string LoginFailureUrl { get; set; }

        public string LoginLabel { get; set; } = "Login";

        public string LogoutSuccessUrl { get; set; }

        public string LogoutFailureUrl { get; set; }

        public string LogoutLabel { get; set; } = "Logout";

        public string UserClaim { get; set; }

        public string UserMessage { get; set; } = "Welcome:";

        public string RegistrationUrl { get; set; } = "/Authentication/Registration";
        
        public string RegistrationLabel { get; set; } = "Register";

    }
}