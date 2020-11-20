using System.ComponentModel.DataAnnotations;
using Michaelsoft.BodyGuard.Common.Models;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Common.HttpModels.Authentication
{
    public class UserCreateRequest
    {

        [Required]
        [JsonRequired]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [JsonRequired]
        [Display(Name = "Password")]
        public string Password { get; set; }
        
        [Required]
        [JsonRequired]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string PasswordConfirm { get; set; }

        public User UserData { get; set; }

    }
}