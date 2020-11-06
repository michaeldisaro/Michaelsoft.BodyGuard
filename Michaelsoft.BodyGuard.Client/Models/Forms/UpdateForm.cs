using Michaelsoft.BodyGuard.Client.Models.Entities;

namespace Michaelsoft.BodyGuard.Client.Models.Forms
{
    public class UpdateForm
    {

        public User User { get; set; }

        public string SuccessUrl { get; set; }

        public string FailureUrl { get; set; }

        public string SubmitLabel { get; set; } = "Update";

    }
}