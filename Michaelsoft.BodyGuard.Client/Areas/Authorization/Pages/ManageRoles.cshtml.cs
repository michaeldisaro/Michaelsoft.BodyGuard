using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Interfaces;
using Michaelsoft.BodyGuard.Client.Models.Forms;
using Michaelsoft.BodyGuard.Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Michaelsoft.BodyGuard.Client.Areas.Authorization.Pages
{
    public class ManageRoles : PageModel
    {

        private readonly IBodyGuardAuthorizationApiService _authorizationApiService;

        public ManageRoles(IBodyGuardAuthorizationApiService authorizationApiService)
        {
            _authorizationApiService = authorizationApiService;
        }

        [BindProperty]
        public ManageRolesForm ManageRolesForm { get; set; }

        public void OnGet()
        {
            ManageRolesForm = new ManageRolesForm
            {
                SuccessUrl = "/Authorization/ManageRoles",
                FailureUrl = "/Authorization/ManageRoles"
            };
        }

        public async Task<IActionResult> OnPostAssign()
        {
            if (!ModelState.IsValid)
            {
                TempData.Set("FormStatus", new FormStatus(ModelState));
                return Redirect(ManageRolesForm.FailureUrl ?? "/Authorization/ManageRoles");
            }

            var response = await _authorizationApiService.AssignRole(ManageRolesForm.ManageRoleRequest.EmailAddress,
                                                                     ManageRolesForm.ManageRoleRequest.Role);

            if (response.Success)
            {
                TempData["Message"] = "Assign role succeed!";
                return Redirect(ManageRolesForm.SuccessUrl ?? "/Authorization/ManageRoles");
            }

            TempData["Message"] = "Assign role failed.";
            return Redirect(ManageRolesForm.FailureUrl ?? "/Authorization/ManageRoles");
        }

        public async Task<IActionResult> OnPostRevoke()
        {
            if (!ModelState.IsValid)
            {
                TempData.Set("FormStatus", new FormStatus(ModelState));
                return Redirect(ManageRolesForm.FailureUrl ?? "/Authorization/ManageRoles");
            }

            var response = await _authorizationApiService.RevokeRole(ManageRolesForm.ManageRoleRequest.EmailAddress,
                                                                     ManageRolesForm.ManageRoleRequest.Role);

            if (response.Success)
            {
                TempData["Message"] = "Revoke role succeed!";
                return Redirect(ManageRolesForm.SuccessUrl ?? "/Authorization/ManageRoles");
            }

            TempData["Message"] = "Revoke role failed.";
            return Redirect(ManageRolesForm.FailureUrl ?? "/Authorization/ManageRoles");
        }

    }
}