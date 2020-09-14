using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Services;
using Michaelsoft.BodyGuard.TestWebApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.TestWebApp.Pages
{
    public class IndexModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;

        private readonly BodyGuardAuthenticationApiService _bodyGuardAuthenticationApiService;

        public IndexModel(ILogger<IndexModel> logger,
                          BodyGuardAuthenticationApiService bodyGuardAuthenticationApiService)
        {
            _bodyGuardAuthenticationApiService = bodyGuardAuthenticationApiService;
            _logger = logger;
        }

        public User CreatedUser { get; set; }

        public async Task OnGetCreateUser()
        {
            var result = await _bodyGuardAuthenticationApiService.UserCreate
                ("djmds@sdasda.asd",
                 "bagigio",
                 new User
                 {
                     Name = "asdasdasd"
                 });

            var data = await _bodyGuardAuthenticationApiService.UserData(result.Id);

            CreatedUser = JsonConvert.DeserializeObject<User>(data.Data);
        }

    }
}