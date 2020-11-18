using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Interfaces;
using Michaelsoft.BodyGuard.Client.Models;
using Michaelsoft.BodyGuard.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Michaelsoft.BodyGuard.TestWebApp.Areas.Site.Pages
{
    public class IndexModel : PageModel
    {

        private readonly IBodyGuardUserApiService _bodyGuardUserApiService;

        public IndexModel(IBodyGuardUserApiService bodyGuardUserApiService)
        {
            _bodyGuardUserApiService = bodyGuardUserApiService;
        }

        public List<User> UsersData { get; set; } = new List<User>();

        [TempData]
        public string Message { get; set; }

        private async Task LoadData()
        {
            try
            {
                UsersData = (await _bodyGuardUserApiService.GetUsers()).UsersData;
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

    }
}