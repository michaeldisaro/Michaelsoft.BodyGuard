namespace Michaelsoft.BodyGuard.Common.HttpModels.Authentication
{
    public class PasswordRecoveryRequest
    {

        public string EmailAddress { get; set; }

        public int TtlSeconds { get; set; } = 1800;

        public string ValidateRecoveryUrl { get; set; } = "/Authentication/ValidateRecovery?token={{token}}";

    }
}