using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Common.HttpModels.Authentication
{
    public class ValidateRecoveryRequest
    {

        [Required]
        [JsonRequired]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [JsonRequired]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required]
        [JsonRequired]
        [Display(Name = "New Password Confirm")]
        public string NewPasswordConfirm { get; set; }

        [Required]
        [JsonRequired]
        public string Token { get; set; }

    }
}