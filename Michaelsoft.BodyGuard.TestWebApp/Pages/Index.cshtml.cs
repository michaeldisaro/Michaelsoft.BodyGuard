using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Services;
using Michaelsoft.BodyGuard.TestWebApp.Models;
using Microsoft.AspNetCore.Mvc;
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

        public User UserData { get; set; }

        [BindProperty]
        public UserCreate UserCreateForm { get; set; }

        [BindProperty]
        public UserUpdate UserUpdateForm { get; set; }

        public async Task<IActionResult> OnPostCreateUser()
        {
            var result = await _bodyGuardAuthenticationApiService.UserCreate
                             (UserCreateForm.EmailAddress,
                              UserCreateForm.Password,
                              new User
                              {
                                  Name = UserCreateForm.Name,
                                  Surname = UserCreateForm.Surname,
                                  EmailAddress = UserCreateForm.EmailAddress
                              });

            var data = await _bodyGuardAuthenticationApiService.GetUserData(result.Id);
            UserData = JsonConvert.DeserializeObject<User>(data.Data);
            UserUpdateForm.Id = result.Id;
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateUser()
        {
            var data = await _bodyGuardAuthenticationApiService.GetUserData(UserUpdateForm.Id);
            var user = JsonConvert.DeserializeObject<User>(data.Data);

            var result = await _bodyGuardAuthenticationApiService.UpdateUserData
                             (UserUpdateForm.Id,
                              new User
                              {
                                  Name = UserUpdateForm.Name,
                                  Surname = UserUpdateForm.Surname,
                                  EmailAddress = user.EmailAddress
                              });

            data = await _bodyGuardAuthenticationApiService.GetUserData(UserUpdateForm.Id);
            UserData = JsonConvert.DeserializeObject<User>(data.Data);

            return Page();
        }

        public class UserCreate
        {

            [Required]
            [EmailAddress]
            public string EmailAddress { get; set; }

            [Required]
            public string Password { get; set; }

            [Required]
            public string Name { get; set; }

            [Required]
            public string Surname { get; set; }

        }

        public class UserUpdate
        {

            [Required]
            public string Id { get; set; }

            [Required]
            public string Name { get; set; }

            [Required]
            public string Surname { get; set; }

        }

    }
}