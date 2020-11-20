using Michaelsoft.BodyGuard.Common.HttpModels.Authorization;

namespace Michaelsoft.BodyGuard.Client.Models.Forms
{
    public class ManageRolesForm
    {

        public ManageRoleRequest ManageRoleRequest { get; set; }

        public string SuccessUrl { get; set; }

        public string FailureUrl { get; set; }

        public string AssignRoleLabel { get; set; } = "Assign role";

        public string RevokeRoleLabel { get; set; } = "Revoke role";

    }
}