using System.Collections.Generic;
using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Common.HttpModels.Authorization;

namespace Michaelsoft.BodyGuard.Client.Interfaces
{
    public interface IBodyGuardAuthorizationApiService
    {

        Task<GetRolesResponse> GetRoles();

        Task<CanResponse> Can(string id,
                              List<string> roles,
                              Dictionary<string, string> claims,
                              bool canAll);

        Task<ManageRoleResponse> AssignRole(string emailAddress,
                                            string role);

        Task<ManageRoleResponse> RevokeRole(string emailAddress,
                                            string role);

    }
}