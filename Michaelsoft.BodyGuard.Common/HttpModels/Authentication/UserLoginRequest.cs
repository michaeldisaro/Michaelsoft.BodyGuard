using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Common.HttpModels.Authentication
{
    public class UserLoginRequest
    {

        [Required]
        [JsonRequired]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [JsonRequired]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
}