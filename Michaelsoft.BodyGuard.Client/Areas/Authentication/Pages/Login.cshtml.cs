using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Models;
using Michaelsoft.BodyGuard.Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Michaelsoft.BodyGuard.Client.Areas.Authentication.Pages
{
    public class Login : PageModel
    {

        private readonly BodyGuardAuthenticationApiService _authenticationApiService;

        public Login(BodyGuardAuthenticationApiService authenticationApiService)
        {
            _authenticationApiService = authenticationApiService;
        }

        [BindProperty]
        public LoginForm LoginForm { get; set; }

        public void OnGet()
        {
            LoginForm = new LoginForm
            {
                SuccessUrl = "/Authentication/Login"
            };
        }

        public async Task<IActionResult> OnPost()
        {
            var response = await _authenticationApiService.Login
                               (LoginForm.LoginRequest.EmailAddress, LoginForm.LoginRequest.Password);

            if (response.Success)
            {
                TempData["Message"] = "Login succeed!";
                return Redirect(LoginForm.SuccessUrl ?? "/Authentication/Login");
            }

            TempData["Message"] = "Login failed.";
            return Redirect(LoginForm.FailureUrl ?? "/Authentication/Login");
        }

    }
}