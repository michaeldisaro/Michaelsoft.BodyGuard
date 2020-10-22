using Michaelsoft.BodyGuard.Common.HttpModels.Authentication;

namespace Michaelsoft.BodyGuard.Client.Models
{
    public class RegistrationForm
    {

        public UserCreateRequest CreateRequest { get; set; }

        public string SuccessUrl { get; set; }

        public string FailureUrl { get; set; }

        public string SubmitLabel { get; set; } = "Login";

    }
}