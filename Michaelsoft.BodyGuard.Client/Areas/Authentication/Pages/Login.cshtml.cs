using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Interfaces;
using Michaelsoft.BodyGuard.Client.Models.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Michaelsoft.BodyGuard.Client.Areas.Authentication.Pages
{
    public class Login : PageModel
    {

        private readonly IBodyGuardAuthenticationApiService _authenticationApiService;

        public Login(IBodyGuardAuthenticationApiService authenticationApiService)
        {
            _authenticationApiService = authenticationApiService;
        }

        [BindProperty]
        public AuthenticationForm AuthenticationForm { get; set; }

        public void OnGet()
        {
            AuthenticationForm = new AuthenticationForm
            {
                LoginSuccessUrl = "/Authentication/Login",
                LoginFailureUrl = "/Authentication/Login"
            };
        }

        public async Task<IActionResult> OnPost()
        {
            var response = await _authenticationApiService.Login
                               (AuthenticationForm.LoginRequest.EmailAddress, AuthenticationForm.LoginRequest.Password);

            if (response.Success)
            {
                TempData["Message"] = "Login succeed!";
                return Redirect(AuthenticationForm.LoginSuccessUrl ?? "/Authentication/Login");
            }

            TempData["Message"] = "Login failed.";
            return Redirect(AuthenticationForm.LoginFailureUrl ?? "/Authentication/Login");
        }

    }
}