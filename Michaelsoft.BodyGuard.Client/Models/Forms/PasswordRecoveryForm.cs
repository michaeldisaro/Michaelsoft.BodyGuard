using Michaelsoft.BodyGuard.Common.HttpModels.Authentication;

namespace Michaelsoft.BodyGuard.Client.Models.Forms
{
    public class PasswordRecoveryForm
    {

        public PasswordRecoveryRequest PasswordRecoveryRequest { get; set; }

        public string SuccessUrl { get; set; }

        public string FailureUrl { get; set; }

        public string SubmitLabel { get; set; } = "Ask to recover password";

    }
}