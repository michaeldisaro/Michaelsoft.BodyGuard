using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Interfaces;
using Michaelsoft.BodyGuard.Client.Models.Forms;
using Michaelsoft.BodyGuard.Common.HttpModels.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Michaelsoft.BodyGuard.Client.Areas.Authentication.Pages
{
    public class ValidateRecovery : PageModel
    {

        private readonly IBodyGuardAuthenticationApiService _authenticationApiService;

        public ValidateRecovery(IBodyGuardAuthenticationApiService authenticationApiService)
        {
            _authenticationApiService = authenticationApiService;
        }

        [BindProperty]
        public ValidateRecoveryForm ValidateRecoveryForm { get; set; }

        public void OnGet(string token)
        {
            ValidateRecoveryForm = new ValidateRecoveryForm
            {
                SuccessUrl = "/Authentication/Login",
                FailureUrl = "/Authentication/RecoveryRequest",
                ValidateRecoveryRequest = new ValidateRecoveryRequest
                {
                    Token = token
                }
            };
        }

        public async Task<IActionResult> OnPost()
        {
            var response = await _authenticationApiService.ValidateRecovery
                               (ValidateRecoveryForm.ValidateRecoveryRequest.EmailAddress,
                                ValidateRecoveryForm.ValidateRecoveryRequest.Token,
                                ValidateRecoveryForm.ValidateRecoveryRequest.NewPassword,
                                ValidateRecoveryForm.ValidateRecoveryRequest.NewPasswordConfirm);

            if (response.Success)
            {
                TempData["Message"] = "Recovery validation succeed!";
                return Redirect(ValidateRecoveryForm.SuccessUrl ?? "/Authentication/Login");
            }

            TempData["Message"] = "Recovery validation failed.";
            return Redirect(ValidateRecoveryForm.FailureUrl ?? "/Authentication/RecoveryRequest");
        }

    }
}