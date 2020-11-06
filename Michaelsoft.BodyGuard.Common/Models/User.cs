using System.ComponentModel.DataAnnotations;
using Michaelsoft.BodyGuard.Common.Interfaces;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Common.Models
{
    public class User : IUser
    {

        public string Id { get; set; }

        [MaxLength(32)]
        public string Nickname { get; set; }

        [MaxLength(32)]
        public string Name { get; set; }

        [MaxLength(32)]
        public string Surname { get; set; }

        [MaxLength(256)]
        [EmailAddress]
        public string EmailAddress { get; set; }
        
        [MaxLength(32)]
        public string PhoneNumber { get; set; }

        [JsonIgnore]
        public string FullName => $"{Surname} {Name}";

    }
}