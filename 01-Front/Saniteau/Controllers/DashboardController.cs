using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Saniteau.Models;

namespace Saniteau.Controllers
{
    public class DashboardController : Controller
    {
        public DashboardController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
