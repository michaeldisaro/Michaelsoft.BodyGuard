using System.Collections.Generic;

namespace Michaelsoft.BodyGuard.Common.HttpModels.Authorization
{
    public class CanRequest
    {

        public string Id { get; set; }
        
        public List<string> Roles { get; set; } = new List<string>();

        public Dictionary<string, string> Claims { get; set; } = new Dictionary<string, string>();

        public bool CanAll { get; set; } = false;

    }
}