using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Common.HttpModels.Authorization
{
    public class ManageRoleRequest
    {
        [Required]
        [JsonRequired]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        
        [Required]
        [JsonRequired]
        [Display(Name = "Role")]
        public string Role { get; set; }

    }
}