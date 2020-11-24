using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Interfaces;
using Michaelsoft.BodyGuard.Client.Models.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Michaelsoft.BodyGuard.Client.Areas.User.Pages
{
    public class Delete : PageModel
    {

        private readonly IBodyGuardUserApiService _userApiService;

        public Delete(IBodyGuardUserApiService userApiService)
        {
            _userApiService = userApiService;
        }

        [BindProperty]
        public DeleteForm DeleteForm { get; set; }

        public void OnGet(string id)
        {
            DeleteForm = new DeleteForm
            {
                Id = id
            };
        }

        public async Task<IActionResult> OnPost(string id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage(DeleteForm.FailurePage,
                                      new {Area = DeleteForm.FailureArea, Id = DeleteForm.Id});
            }

            var response = await _userApiService.DeleteUser(id);

            if (response.Success)
            {
                TempData["Message"] = "Delete succeed!";
                return RedirectToPage(DeleteForm.SuccessPage,
                                      new {Area = DeleteForm.SuccessArea, Id = DeleteForm.Id});
            }

            TempData["Message"] = "Delete failed.";
            return RedirectToPage(DeleteForm.FailurePage,
                                  new {Area = DeleteForm.FailureArea, Id = DeleteForm.Id});
        }

    }
}