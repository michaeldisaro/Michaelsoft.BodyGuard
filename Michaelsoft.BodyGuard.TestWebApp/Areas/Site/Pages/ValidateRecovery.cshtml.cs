using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Michaelsoft.BodyGuard.TestWebApp.Areas.Site.Pages
{
    public class ValidateRecovery : PageModel
    {

        public string Token { get; set; }
        
        public void OnGet(string token)
        {
            Token = token;
        }

    }
}