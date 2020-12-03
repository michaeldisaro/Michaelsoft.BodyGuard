using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Interfaces;
using Michaelsoft.BodyGuard.Common.HttpModels.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Michaelsoft.BodyGuard.TestWebApp.Areas.Site.Pages
{
    public class ConfirmRegistration : PageModel
    {

        private readonly IBodyGuardAuthenticationApiService _authenticationApiService;

        public ConfirmRegistration(IBodyGuardAuthenticationApiService authenticationApiService)
        {
            _authenticationApiService = authenticationApiService;
        }

        [TempData]
        public string Message { get; set; }

        public async Task<IActionResult> OnGet(string token)
        {
            var response = await _authenticationApiService.ConfirmRegistration(token);
            if (response.Success)
                TempData["Message"] = "Account confermato!";
            else
                TempData["Message"] = "Conferma account non riuscita!";
            return Page();
        }

    }
}