using System.ComponentModel.DataAnnotations;
using Michaelsoft.BodyGuard.Client.Interfaces;

namespace Michaelsoft.BodyGuard.Client.Models.Entities
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

    }
}