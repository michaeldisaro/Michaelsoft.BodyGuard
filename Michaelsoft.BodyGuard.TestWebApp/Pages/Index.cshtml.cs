using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Interfaces;
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

        private readonly ConnectionService _connectionService;

        public IndexModel(ILogger<IndexModel> logger,
                          ConnectionService connectionService)
        {
            _connectionService = connectionService;
            _logger = logger;
        }

        public void OnGet()
        {
            _connectionService.RegisterUser("djmds@sdasda.asd", "bagigio", new User {Name = "asdasdasd"});
        }

    }
}