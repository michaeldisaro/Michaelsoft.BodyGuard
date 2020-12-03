using Michaelsoft.BodyGuard.Common.HttpModels.Authentication;

namespace Michaelsoft.BodyGuard.Client.Models.Forms
{
    public class RegistrationEmailForm
    {

        public RegistrationEmailRequest RegistrationEmailRequest { get; set; }

        public string SuccessArea { get; set; } = "Result";

        public string SuccessPage { get; set; } = "/Success";

        public string FailureArea { get; set; } = "Authentication";

        public string FailurePage { get; set; } = "/RegistrationEmail";

        public string SubmitLabel { get; set; } = "submit_confirmation_request";

    }
}