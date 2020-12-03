using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Common.HttpModels.Authentication
{
    public class PasswordRecoveryRequest
    {
        [Required]
        [JsonRequired]
        [Display(Name = "email_address")]
        public string EmailAddress { get; set; }

        public int TtlSeconds { get; set; } = 1800;

        public string ValidateRecoveryUrl { get; set; } = "/Authentication/ValidateRecovery?token={{token}}";

    }
}