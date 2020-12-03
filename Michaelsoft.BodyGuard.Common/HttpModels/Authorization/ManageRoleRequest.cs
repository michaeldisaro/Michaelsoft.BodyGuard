using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Common.HttpModels.Authorization
{
    public class ManageRoleRequest
    {
        [Required]
        [JsonRequired]
        [Display(Name = "email_address")]
        public string EmailAddress { get; set; }
        
        [Required]
        [JsonRequired]
        [Display(Name = "role")]
        public string Role { get; set; }

    }
}