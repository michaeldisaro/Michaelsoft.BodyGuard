using System.ComponentModel.DataAnnotations;
using Michaelsoft.BodyGuard.Common.Attributes;
using Michaelsoft.BodyGuard.Common.Models;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Common.HttpModels.Authentication
{
    public class UserCreateRequest
    {

        [Required]
        [JsonRequired]
        [MinLength(5)]
        [MaxLength(320)]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [JsonRequired]
        [MinLength(6)]
        [MaxLength(64)]
        [ValidatePassword]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [JsonRequired]
        [MinLength(6)]
        [MaxLength(64)]
        [ValidatePassword]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string PasswordConfirm { get; set; }

        public User UserData { get; set; }
        
        public int TtlSeconds { get; set; } = 48 * 3600;

        public string ConfirmRegistrationUrl { get; set; } = "/Authentication/ConfirmRegistration?token={{token}}";

    }
}