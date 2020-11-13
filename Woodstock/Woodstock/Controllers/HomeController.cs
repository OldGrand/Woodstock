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
        private readonly ILogger<HomeController> _logger;
        private readonly WoodstockDbContext woodstockDbContext;

        public HomeController(ILogger<HomeController> logger, WoodstockDbContext woodstockDbContext)
        {
            _logger = logger;
            this.woodstockDbContext = woodstockDbContext;
        }

        public IActionResult Index()
        {
            woodstockDbContext.Users.ToList();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
