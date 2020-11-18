using Michaelsoft.BodyGuard.Common.HttpModels.Authentication;

namespace Michaelsoft.BodyGuard.Client.Models.Forms
{
    public class ValidateRecoveryForm
    {

        public ValidateRecoveryRequest ValidateRecoveryRequest { get; set; }

        public string SuccessUrl { get; set; }

        public string FailureUrl { get; set; }

        public string SubmitLabel { get; set; } = "Change your password";

    }
}