using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Common.HttpModels.Authentication
{
    public class UserLoginRequest
    {

        [Required]
        [JsonRequired]
        [Display(Name = "email_address")]
        public string EmailAddress { get; set; }

        [Required]
        [JsonRequired]
        [Display(Name = "password")]
        public string Password { get; set; }

    }
}