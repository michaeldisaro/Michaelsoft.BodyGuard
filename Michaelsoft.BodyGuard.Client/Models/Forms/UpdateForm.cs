using Michaelsoft.BodyGuard.Common.Models;

namespace Michaelsoft.BodyGuard.Client.Models.Forms
{
    public class UpdateForm
    {

        public User User { get; set; }

        public string SuccessArea { get; set; } = "Result";

        public string SuccessPage { get; set; } = "/Success";

        public string FailureArea { get; set; } = "User";

        public string FailurePage { get; set; } = "/Update";

        public string SubmitLabel { get; set; } = "submit_update";

    }
}