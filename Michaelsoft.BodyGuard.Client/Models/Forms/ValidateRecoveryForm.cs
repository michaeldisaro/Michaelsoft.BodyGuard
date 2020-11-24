using Michaelsoft.BodyGuard.Common.HttpModels.Authentication;

namespace Michaelsoft.BodyGuard.Client.Models.Forms
{
    public class ValidateRecoveryForm
    {

        public ValidateRecoveryRequest ValidateRecoveryRequest { get; set; }

        public string SuccessArea { get; set; } = "Result";

        public string SuccessPage { get; set; } = "/Success";

        public string FailureArea { get; set; } = "Authentication";

        public string FailurePage { get; set; } = "/ValidateRecovery";

        public string SubmitLabel { get; set; } = "Change your password";

    }
}