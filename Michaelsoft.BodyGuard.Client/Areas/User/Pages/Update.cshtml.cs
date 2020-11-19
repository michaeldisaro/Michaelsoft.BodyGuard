using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Interfaces;
using Michaelsoft.BodyGuard.Client.Models;
using Michaelsoft.BodyGuard.Client.Models.Forms;
using Michaelsoft.BodyGuard.Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Michaelsoft.BodyGuard.Client.Areas.User.Pages
{
    public class Update : PageModel
    {

        private readonly IBodyGuardUserApiService _userApiService;

        public Update(IBodyGuardUserApiService userApiService)
        {
            _userApiService = userApiService;
        }

        [BindProperty]
        public UpdateForm UpdateForm { get; set; }

        public async Task OnGet(string userId)
        {
            var user = await _userApiService.GetUser(userId);
            UpdateForm = new UpdateForm
            {
                SuccessUrl = "/User/Update",
                FailureUrl = "/User/Update",
                User = user
            };
        }

        public async Task<IActionResult> OnPost()
        {

            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Update failed.";
                TempData.Set("FormStatus", new FormStatus(ModelState));
                return Redirect(UpdateForm.FailureUrl ?? $"/User/Update?userId={UpdateForm.User.Id}");
            }

            var response = await _userApiService.UpdateUser(UpdateForm.User);

            if (response.Success)
            {
                TempData["Message"] = "Update succeed!";
                return Redirect(UpdateForm.SuccessUrl ?? $"/User/Update?userId={UpdateForm.User.Id}");
            }

            TempData["Message"] = "Update failed.";
            return Redirect(UpdateForm.FailureUrl ?? $"/User/Update?userId={UpdateForm.User.Id}");
        }

    }
}