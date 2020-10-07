using System.Collections.Generic;

namespace Michaelsoft.BodyGuard.Common.HttpModels.Authentication
{
    public class UsersDataResponse : BaseResponse
    {

        public Dictionary<string, string> UsersData { get; set; }

    }
}