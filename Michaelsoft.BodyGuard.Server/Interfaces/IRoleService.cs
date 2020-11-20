using System.Collections.Generic;

namespace Michaelsoft.BodyGuard.Server.Interfaces
{
    public interface IRoleService
    {

        string this[string index] { get; set; }

        List<string> Roles { get; set; }
    }
}