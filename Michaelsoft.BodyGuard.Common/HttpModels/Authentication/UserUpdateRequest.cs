using Michaelsoft.BodyGuard.Common.Models;

namespace Michaelsoft.BodyGuard.Common.HttpModels.Authentication
{
    public class UserUpdateRequest
    {

        public string Id { get; set; }

        public User UserData { get; set; }

    }
}