using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Interfaces;
using Michaelsoft.BodyGuard.Client.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Michaelsoft.BodyGuard.Client.Areas.User.Pages
{
    public class List : PageModel
    {

        private readonly IBodyGuardUserApiService _userApiService;

        public List(IBodyGuardUserApiService userApiService)
        {
            _userApiService = userApiService;
        }

        public UserList UserList { get; set; }

        public async Task OnGet()
        {
            var userDataResponse = await _userApiService.GetUsers();
            UserList = new UserList
            {
                UsersData = userDataResponse.UsersData
            };
        }

    }
}