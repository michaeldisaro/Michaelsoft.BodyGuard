using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Services;
using Michaelsoft.BodyGuard.TestWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

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

        public void OnGet()
        {
            var result = _bodyGuardAuthenticationApiService.UserCreate("djmds@sdasda.asd", "bagigio", new User {Name = "asdasdasd"});
            return;
        }

    }
}