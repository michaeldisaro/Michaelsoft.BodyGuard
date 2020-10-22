using System;
using Michaelsoft.BodyGuard.Client.Interfaces;

namespace Michaelsoft.BodyGuard.Client.Models
{
    public class UpdateForm
    {

        public UserData UserData { get; set; }

        public string SuccessUrl { get; set; }

        public string FailureUrl { get; set; }

        public string SubmitLabel { get; set; } = "Update";

    }
}