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

        public string LoginLabel { get; set; } = "login";

        public string LogoutSuccessArea { get; set; } = "Authentication";

        public string LogoutSuccessPage { get; set; } = "/Login";

        public string LogoutFailureArea { get; set; } = "Authentication";

        public string LogoutFailurePage { get; set; } = "/Logout";

        public string LogoutLabel { get; set; } = "logout";

        public string UserClaim { get; set; }

        public string UserMessage { get; set; } = "welcome_user";

        public string RegistrationArea { get; set; } = "Authentication";

        public string RegistrationPage { get; set; } = "/Registration";

        public string RegistrationLabel { get; set; } = "link_register";
        
        public string RegistrationEmailArea { get; set; } = "Authentication";

        public string RegistrationEmailPage { get; set; } = "/RegistrationEmail";

        public string RegistrationEmailLabel { get; set; } = "link_confirm_recovery";

        public string PasswordRecoveryArea { get; set; } = "Authentication";

        public string PasswordRecoveryPage { get; set; } = "/PasswordRecovery";

        public string PasswordRecoveryLabel { get; set; } = "link_password_recovery";

    }
}