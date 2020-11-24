using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Interfaces;
using Michaelsoft.BodyGuard.Client.Models.Forms;
using Michaelsoft.BodyGuard.Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Michaelsoft.BodyGuard.Client.Areas.Authentication.Pages
{
    public class PasswordRecovery : PageModel
    {

        private readonly IBodyGuardAuthenticationApiService _authenticationApiService;

        public PasswordRecovery(IBodyGuardAuthenticationApiService authenticationApiService)
        {
            _authenticationApiService = authenticationApiService;
        }

        [BindProperty]
        public PasswordRecoveryForm PasswordRecoveryForm { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                TempData.Set("FormStatus", new FormStatus(ModelState));
                return RedirectToPage(PasswordRecoveryForm.FailurePage,
                                      new {Area = PasswordRecoveryForm.FailureArea});
            }
            
            var response = await _authenticationApiService.PasswordRecovery
                               (PasswordRecoveryForm.PasswordRecoveryRequest.EmailAddress,
                                PasswordRecoveryForm.PasswordRecoveryRequest.TtlSeconds,
                                PasswordRecoveryForm.PasswordRecoveryRequest.ValidateRecoveryUrl);

            if (response.Success)
            {
                TempData["Message"] = "Password recovery succeed!";
                return RedirectToPage(PasswordRecoveryForm.SuccessPage,
                                      new {Area = PasswordRecoveryForm.SuccessArea});
            }

            TempData["Message"] = "Password recovery failed.";
            return RedirectToPage(PasswordRecoveryForm.FailurePage,
                                  new {Area = PasswordRecoveryForm.FailureArea});
        }

    }
}