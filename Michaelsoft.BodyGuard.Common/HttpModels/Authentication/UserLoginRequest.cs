using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Common.HttpModels.Authentication
{
    public class UserLoginRequest
    {

        [Required]
        [JsonRequired]
        public string EmailAddress { get; set; }

        [Required]
        [JsonRequired]
        public string Password { get; set; }

    }
}