using Michaelsoft.BodyGuard.Common.HttpModels.Authentication;

namespace Michaelsoft.BodyGuard.Client.Models.Forms
{
    public class AuthenticationForm
    {

        public UserLoginRequest LoginRequest { get; set; }

        public string LoginSuccessArea { get; set; } = "Result";

        public string LoginSuccessPage { get; set; } = "/Success";

        public string LoginFailureArea { get; set; } = "Result";

        public string LoginFailurePage { get; set; } = "/Failure";

        public string LoginLabel { get; set; } = "Login";

        public string LogoutSuccessArea { get; set; } = "Authentication";

        public string LogoutSuccessPage { get; set; } = "/Login";

        public string LogoutFailureArea { get; set; } = "Authentication";

        public string LogoutFailurePage { get; set; } = "/Logout";

        public string LogoutLabel { get; set; } = "Logout";

        public string UserClaim { get; set; }

        public string UserMessage { get; set; } = "Welcome:";

        public string RegistrationArea { get; set; } = "Authentication";

        public string RegistrationPage { get; set; } = "/Registration";

        public string RegistrationLabel { get; set; } = "Register";

        public string PasswordRecoveryArea { get; set; } = "Authentication";

        public string PasswordRecoveryPage { get; set; } = "/PasswordRecovery";

        public string PasswordRecoveryLabel { get; set; } = "Recover password";

    }
}