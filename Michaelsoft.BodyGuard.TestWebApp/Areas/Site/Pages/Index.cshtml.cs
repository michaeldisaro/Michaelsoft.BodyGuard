using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Interfaces;
using Michaelsoft.BodyGuard.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Michaelsoft.BodyGuard.TestWebApp.Areas.Site.Pages
{
    public class IndexModel : PageModel
    {

        [TempData]
        public string Message { get; set; }

        public void OnGet()
        {
        }

    }
}