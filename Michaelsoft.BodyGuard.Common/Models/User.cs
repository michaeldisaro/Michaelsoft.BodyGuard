using System.ComponentModel.DataAnnotations;
using Michaelsoft.BodyGuard.Common.Attributes;
using Michaelsoft.BodyGuard.Common.Interfaces;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Common.Models
{
    public class User : IUser
    {

        [ExcludeFromInsert]
        [ExcludeFromUpdate]
        public string Id { get; set; }

        [MinLength(5)]
        [MaxLength(320)]
        [EmailAddress]
        [ExcludeFromInsert]
        [Display(Name = "Email Address")]
        [ValidateEnabledUserDataProperty]
        public string EmailAddress { get; set; }

        [MinLength(2)]
        [MaxLength(64)]
        [Display(Name = "Name")]
        [ValidateEnabledUserDataProperty]
        public string Name { get; set; }

        [MinLength(2)]
        [MaxLength(64)]
        [Display(Name = "Surname")]
        [ValidateEnabledUserDataProperty]
        public string Surname { get; set; }

        [MinLength(5)]
        [MaxLength(64)]
        [Display(Name = "Nickname")]
        [ValidateEnabledUserDataProperty]
        public string Nickname { get; set; }

        [MinLength(5)]
        [MaxLength(16)]
        [Display(Name = "Phone Number")]
        [ValidateEnabledUserDataProperty]
        public string PhoneNumber { get; set; }

        [JsonIgnore]
        [ExcludeFromInsert]
        [ExcludeFromUpdate]
        public string FullName => $"{Surname} {Name}";

    }
}