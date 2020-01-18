using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Conversions.Models;
using System.Net.Http;
using Newtonsoft.Json;
using CoinsApi.Models;
using CoinsApi.Controllers;

namespace Conversions.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
          
            ChangeController change = new ChangeController();
            
            var quotation = await change.SetQuotation("USD",1);

            ViewData["moneda"] = quotation.Value.moneda;
            ViewData["precio"] = quotation.Value.precio;

            return View(quotation);

        }

        public async Task<IActionResult> Privacy()
        {
            ChangeController change = new ChangeController();

            var quotation = await change.SetQuotation("BRL", 1);

            ViewData["moneda"] = quotation.Value.moneda;
            ViewData["precio"] = quotation.Value.precio;

            return View(quotation);
           
        }

        public async Task<IActionResult> Euro()
        {
            ChangeController change = new ChangeController();

            var quotation = await change.SetQuotation("EUR", 1);

            ViewData["moneda"] = quotation.Value.moneda;
            ViewData["precio"] = quotation.Value.precio;

            return View(quotation);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

