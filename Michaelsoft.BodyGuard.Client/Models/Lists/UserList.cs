using System.Collections.Generic;
using Michaelsoft.BodyGuard.Common.Models;

namespace Michaelsoft.BodyGuard.Client.Models.Lists
{
    public class UserList
    {

        public string UpdatePage { get; set; } = "/Update";

        public string UpdateArea { get; set; } = "User";

        public string UpdateLabel { get; set; } = "link_update";
        
        public string SuccessArea { get; set; } = "Result";

        public string SuccessPage { get; set; } = "/Success";

        public string FailureArea { get; set; } = "Result";

        public string FailurePage { get; set; } = "/Failure";

    }
}