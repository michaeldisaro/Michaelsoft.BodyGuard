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
        [Display(Name = "email_address")]
        public string EmailAddress { get; set; }

        [Required]
        [JsonRequired]
        [MinLength(6)]
        [MaxLength(64)]
        [ValidatePassword]
        [Display(Name = "new_password")]
        public string NewPassword { get; set; }

        [Required]
        [JsonRequired]
        [MinLength(6)]
        [MaxLength(64)]
        [ValidatePassword]
        [Compare("NewPassword")]
        [Display(Name = "new_password_confirm")]
        public string NewPasswordConfirm { get; set; }

        [Required]
        [JsonRequired]
        public string Token { get; set; }

    }
}