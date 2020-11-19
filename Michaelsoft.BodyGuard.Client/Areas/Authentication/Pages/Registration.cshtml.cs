using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Interfaces;
using Michaelsoft.BodyGuard.Client.Models.Forms;
using Michaelsoft.BodyGuard.Common.Extensions;
using Michaelsoft.BodyGuard.Common.HttpModels.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Michaelsoft.BodyGuard.Client.Areas.Authentication.Pages
{
    public class Registration : PageModel
    {

        private readonly IBodyGuardAuthenticationApiService _authenticationApiService;

        public Registration(IBodyGuardAuthenticationApiService authenticationApiService)
        {
            _authenticationApiService = authenticationApiService;
        }

        [BindProperty]
        public RegistrationForm RegistrationForm { get; set; }

        public void OnGet()
        {
            RegistrationForm = new RegistrationForm
            {
                SuccessUrl = "/Authentication/Registration",
                FailureUrl = "/Authentication/Registration",
                CreateRequest = new UserCreateRequest
                {
                    UserData = new Common.Models.User()
                }
            };
        }

        public async Task<IActionResult> OnPost()
        {

            if (!ModelState.IsValid)
            {
                TempData.Set("FormStatus", new FormStatus(ModelState));
                return Redirect(RegistrationForm.FailureUrl ?? "/Authentication/Registration");
            }

            var response = await _authenticationApiService.Register
                               (RegistrationForm.CreateRequest.EmailAddress,
                                RegistrationForm.CreateRequest.Password,
                                RegistrationForm.CreateRequest.UserData);

            if (response.Success)
            {
                TempData["Message"] = "Registration succeed!";
                return Redirect(RegistrationForm.SuccessUrl ?? "/Authentication/Registration");
            }

            TempData["Message"] = "Registration failed.";
            return Redirect(RegistrationForm.FailureUrl ?? "/Authentication/Registration");
        }

    }
}