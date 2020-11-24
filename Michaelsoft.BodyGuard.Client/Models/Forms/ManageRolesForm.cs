using System.Collections.Generic;
using Michaelsoft.BodyGuard.Common.HttpModels.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Michaelsoft.BodyGuard.Client.Models.Forms
{
    public class ManageRolesForm
    {

        public ManageRoleRequest ManageRoleRequest { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }

        public string SuccessArea { get; set; } = "Result";

        public string SuccessPage { get; set; } = "/Success";

        public string FailureArea { get; set; } = "Result";

        public string FailurePage { get; set; } = "/Failure";

        public string AssignRoleLabel { get; set; } = "Assign role";

        public string RevokeRoleLabel { get; set; } = "Revoke role";

    }
}