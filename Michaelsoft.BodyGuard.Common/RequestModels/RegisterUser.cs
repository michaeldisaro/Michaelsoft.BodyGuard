namespace Michaelsoft.BodyGuard.Common.RequestModels
{
    public class RegisterUser
    {

        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public dynamic UserData { get; set; }

    }
}