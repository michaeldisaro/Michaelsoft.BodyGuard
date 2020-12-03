using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Common.HttpModels.Authentication
{
    public class RegistrationEmailRequest
    {

        [Required]
        [JsonRequired]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        public int TtlSeconds { get; set; } = 48 * 3600;

        public string ConfirmRegistrationUrl { get; set; } = "/Authentication/ConfirmRegistration?token={{token}}";

    }
}