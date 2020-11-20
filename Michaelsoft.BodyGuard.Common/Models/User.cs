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

        [MaxLength(32)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [MaxLength(32)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [MaxLength(32)]
        [Display(Name = "Nickname")]
        public string Nickname { get; set; }

        [MaxLength(256)]
        [EmailAddress]
        [ExcludeFromInsert]
        [Display(Name = "Email Address")]
        
        public string EmailAddress { get; set; }

        [MaxLength(32)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [JsonIgnore]
        [ExcludeFromInsert]
        [ExcludeFromUpdate]
        public string FullName => $"{Surname} {Name}";

    }
}