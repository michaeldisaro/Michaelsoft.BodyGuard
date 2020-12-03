using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Interfaces;
using Michaelsoft.BodyGuard.Client.Models.Forms;
using Michaelsoft.BodyGuard.Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Michaelsoft.BodyGuard.Client.Areas.Authentication.Pages
{
    public class RegistrationEmail : PageModel
    {

        private readonly IBodyGuardAuthenticationApiService _authenticationApiService;

        public RegistrationEmail(IBodyGuardAuthenticationApiService authenticationApiService)
        {
            _authenticationApiService = authenticationApiService;
        }

        [BindProperty]
        public RegistrationEmailForm RegistrationEmailForm { get; set; }

        public void OnGet()
        {
            RegistrationEmailForm = new RegistrationEmailForm();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                TempData.Set("FormStatus", new FormStatus(ModelState));
                return RedirectToPage(RegistrationEmailForm.FailurePage,
                                      new {Area = RegistrationEmailForm.FailureArea});
            }

            var response = await _authenticationApiService.SendRegistrationEmail
                               (RegistrationEmailForm.RegistrationEmailRequest.EmailAddress,
                                RegistrationEmailForm.RegistrationEmailRequest.TtlSeconds,
                                RegistrationEmailForm.RegistrationEmailRequest.ConfirmRegistrationUrl);

            if (response.Success)
            {
                TempData["Message"] = "Registration email succeed!";
                return RedirectToPage(RegistrationEmailForm.SuccessPage,
                                      new {Area = RegistrationEmailForm.SuccessArea});
            }

            TempData["Message"] = "Registration email failed.";
            return RedirectToPage(RegistrationEmailForm.FailurePage,
                                  new {Area = RegistrationEmailForm.FailureArea});
        }

    }
}