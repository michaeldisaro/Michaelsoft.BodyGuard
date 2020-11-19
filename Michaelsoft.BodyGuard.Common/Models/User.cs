using System.ComponentModel.DataAnnotations;
using Michaelsoft.BodyGuard.Common.Attributes;
using Michaelsoft.BodyGuard.Common.Interfaces;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Common.Models
{
    public class User : IUser
    {
        [ExcludeFromForm]
        public string Id { get; set; }

        [MaxLength(32)]
        public string Name { get; set; }

        [MaxLength(32)]
        public string Surname { get; set; }

        [MaxLength(32)]
        public string Nickname { get; set; }

        [MaxLength(256)]
        [EmailAddress]
        [ExcludeFromForm]
        public string EmailAddress { get; set; }

        [MaxLength(32)]
        public string PhoneNumber { get; set; }

        [JsonIgnore]
        [ExcludeFromForm]
        public string FullName => $"{Surname} {Name}";

    }
}