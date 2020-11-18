namespace Michaelsoft.BodyGuard.Common.HttpModels.Authentication
{
    public class ValidateRecoveryRequest
    {

        public string EmailAddress { get; set; }

        public string NewPassword { get; set; }

        public string NewPasswordConfirm { get; set; }

        public string Token { get; set; }

    }
}