using System.ComponentModel.DataAnnotations;
using Michaelsoft.BodyGuard.Common.Attributes;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Common.HttpModels.Authentication
{
    public class ValidateRecoveryRequest
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
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required]
        [JsonRequired]
        [MinLength(6)]
        [MaxLength(64)]
        [ValidatePassword]
        [Compare("NewPassword")]
        [Display(Name = "New Password Confirm")]
        public string NewPasswordConfirm { get; set; }

        [Required]
        [JsonRequired]
        public string Token { get; set; }

    }
}