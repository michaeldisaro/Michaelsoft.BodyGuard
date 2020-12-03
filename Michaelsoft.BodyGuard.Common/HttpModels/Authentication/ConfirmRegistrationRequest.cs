using System.ComponentModel.DataAnnotations;
using Michaelsoft.BodyGuard.Common.Attributes;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Common.HttpModels.Authentication
{
    public class ConfirmRegistrationRequest
    {

        [Required]
        [JsonRequired]
        public string Token { get; set; }

    }
}