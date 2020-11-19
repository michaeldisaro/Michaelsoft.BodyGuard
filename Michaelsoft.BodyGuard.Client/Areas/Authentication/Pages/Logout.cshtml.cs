using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Interfaces;
using Michaelsoft.BodyGuard.Client.Models.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Michaelsoft.BodyGuard.Client.Areas.Authentication.Pages
{
    public class Logout : PageModel
    {

        private readonly IBodyGuardAuthenticationApiService _authenticationApiService;

        public Logout(IBodyGuardAuthenticationApiService authenticationApiService)
        {
            _authenticationApiService = authenticationApiService;
        }

        [BindProperty]
        public AuthenticationForm AuthenticationForm { get; set; }

        public async Task<IActionResult> OnPost()
        {
            var response = await _authenticationApiService.Logout();

            if (response.Success)
            {
                TempData["Message"] = "Logout succeed!";
                return Redirect(AuthenticationForm.LogoutSuccessUrl ?? "/Authentication/Logout");
            }

            TempData["Message"] = "Logout failed.";
            return Redirect(AuthenticationForm.LogoutFailureUrl ?? "/Authentication/Logout");
        }

    }
}