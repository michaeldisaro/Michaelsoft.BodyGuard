using System.Collections.Generic;
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

        public Dictionary<string, User> UsersData { get; set; } = new Dictionary<string, User>();

        [BindProperty]
        public UserCreate UserCreateForm { get; set; }

        [BindProperty]
        public UserUpdate UserUpdateForm { get; set; }

        private async Task LoadData()
        {
            var userDataResponse = await _bodyGuardAuthenticationApiService.GetUsersData();
            if (!userDataResponse.Success) return;
            foreach (var kvp in userDataResponse.UsersData)
                UsersData.Add(kvp.Key, JsonConvert.DeserializeObject<User>(kvp.Value));
        }

        public async Task<IActionResult> OnGet()
        {
            await LoadData();
            return Page();
        }

        public async Task<IActionResult> OnPostCreateUser()
        {
            var userCreateResponse = await _bodyGuardAuthenticationApiService.UserCreate
                                         (UserCreateForm.EmailAddress,
                                          UserCreateForm.Password,
                                          new User
                                          {
                                              Name = UserCreateForm.Name,
                                              Surname = UserCreateForm.Surname,
                                              EmailAddress = UserCreateForm.EmailAddress
                                          });

            if (!userCreateResponse.Success) return Page();
            await LoadData();
            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostUpdateUser()
        {
            var userUpdateResponse = await _bodyGuardAuthenticationApiService.UpdateUserData
                                         (UserUpdateForm.Id,
                                          new User
                                          {
                                              Name = UserUpdateForm.Name,
                                              Surname = UserUpdateForm.Surname,
                                              EmailAddress = UserUpdateForm.EmailAddress
                                          });

            if (!userUpdateResponse.Success) return Page();
            await LoadData();
            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnGetDeleteUser(string id)
        {
            var userDeleteResponse = await _bodyGuardAuthenticationApiService.UserDelete(id);
            if (!userDeleteResponse.Success) return Page();
            await LoadData();
            return RedirectToPage("Index");
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

            [Required]
            public string EmailAddress { get; set; }

        }

    }
}