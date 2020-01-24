using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Conversions.Controllers
{
    public class PersonalStudyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Map()
        {
            return View();
        }
        public IActionResult Closure()
        {
            return View();
        }

    }
}