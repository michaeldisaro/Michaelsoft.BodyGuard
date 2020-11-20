using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Common.HttpModels.Authorization;

namespace Michaelsoft.BodyGuard.Client.Interfaces
{
    public interface IBodyGuardAuthorizationApiService
    {

        Task<ManageRoleResponse> AssignRole(string emailAddress,
                                            string role);

        Task<ManageRoleResponse> RevokeRole(string emailAddress,
                                            string role);

    }
}