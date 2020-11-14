using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Woodstock.DAL;

namespace Woodstock.Controllers
{
    public class HomeController : Controller
    {
        private readonly WoodstockDbContext woodstockDbContext;

        public HomeController(WoodstockDbContext woodstockDbContext)
        {
            this.woodstockDbContext = woodstockDbContext;
        }

        public IActionResult Index()
        {
            //woodstockDbContext.Users.ToList();
            return View();
        }
    }
}
