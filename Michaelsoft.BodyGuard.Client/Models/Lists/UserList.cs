using System.Collections.Generic;
using Michaelsoft.BodyGuard.Client.Models.Entities;

namespace Michaelsoft.BodyGuard.Client.Models.Lists
{
    public class UserList
    {

        public string SuccessUrl { get; set; }

        public string FailureUrl { get; set; }

        public List<User> UsersData { get; set; }

        public string SubmitLabel { get; set; } // usefull for inline update
    }
}