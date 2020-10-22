using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Interfaces;
using Michaelsoft.BodyGuard.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Michaelsoft.BodyGuard.TestWebApp.Areas.Site.Pages
{
    public class IndexModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;

        private readonly IBodyGuardAuthenticationApiService _bodyGuardAuthenticationApiService;

        private readonly IBodyGuardUserApiService _bodyGuardUserApiService;

        public IndexModel(ILogger<IndexModel> logger,
                          IBodyGuardAuthenticationApiService bodyGuardAuthenticationApiService,
                          IBodyGuardUserApiService bodyGuardUserApiService)
        {
            _bodyGuardUserApiService = bodyGuardUserApiService;
            _bodyGuardAuthenticationApiService = bodyGuardAuthenticationApiService;
            _logger = logger;
        }

        public List<UserData> UsersData { get; set; } = new List<UserData>();

        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public UserCreate UserCreateForm { get; set; }

        private async Task LoadData()
        {
            try
            {
                var userDataResponse = await _bodyGuardUserApiService.GetUsers();
                UsersData = userDataResponse.UsersData;
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
        }

        public async Task<IActionResult> OnGet()
        {
            await LoadData();
            return Page();
        }

        public async Task<IActionResult> OnPostCreateUser()
        {
            var userCreateResponse = await _bodyGuardAuthenticationApiService.Register
                                         (UserCreateForm.EmailAddress,
                                          UserCreateForm.Password,
                                          new UserData
                                          {
                                              Name = UserCreateForm.Name,
                                              Surname = UserCreateForm.Surname,
                                              EmailAddress = UserCreateForm.EmailAddress
                                          });

            if (!userCreateResponse.Success) return Page();
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
    }
}