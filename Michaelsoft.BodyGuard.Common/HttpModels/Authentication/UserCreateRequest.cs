namespace Michaelsoft.BodyGuard.Common.HttpModels.Authentication
{
    public class UserCreateRequest
    {

        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public dynamic UserData { get; set; }

    }
}