using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Interfaces;
using Michaelsoft.BodyGuard.Client.Models.Forms;
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
            PasswordRecoveryForm = new PasswordRecoveryForm
            {
                SuccessUrl = "/Authentication/RecoveryRequest",
                FailureUrl = "/Authentication/RecoveryRequest"
            };
        }

        public async Task<IActionResult> OnPost()
        {
            var response = await _authenticationApiService.PasswordRecovery
                               (PasswordRecoveryForm.PasswordRecoveryRequest.EmailAddress,
                                PasswordRecoveryForm.PasswordRecoveryRequest.TtlSeconds,
                                PasswordRecoveryForm.PasswordRecoveryRequest.ValidateRecoveryUrl);

            if (response.Success)
            {
                TempData["Message"] = "Password recovery succeed!";
                return Redirect(PasswordRecoveryForm.SuccessUrl ?? "/Authentication/RecoveryRequest");
            }

            TempData["Message"] = "Password recovery failed.";
            return Redirect(PasswordRecoveryForm.FailureUrl ?? "/Authentication/RecoveryRequest");
        }

    }
}