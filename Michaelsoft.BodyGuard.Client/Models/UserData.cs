using Michaelsoft.BodyGuard.Client.Interfaces;

namespace Michaelsoft.BodyGuard.Client.Models
{
    public class UserData : IUserData
    {

        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public string EmailAddress { get; set; }

    }
}