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
        public string EmailAddress { get; set; }

        [Required]
        [JsonRequired]
        public string Password { get; set; }

        public User UserData { get; set; }

    }
}