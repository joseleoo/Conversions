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
using CoinsApi;
using CoinsApi.Controllers;

namespace Conversions.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QuoContext _context;

        public HomeController(ILogger<HomeController> logger, QuoContext context)
        {
            _logger = logger;
            _context = context;
          
            if (_context.Quotations.Count() == 0)
            {


                _context.Quotations.Add(new Quotation { moneda = "", precio = 0 });
                _context.SaveChanges();
            }
        }

        public async Task<IActionResult> Dolar()
        {
        

        ChangeController change = new ChangeController(_context);
            
            var quotation = await change.SetQuotation("USD",1);

            ViewData["moneda"] = quotation.Value.moneda;
            ViewData["precio"] = quotation.Value.precio;
            ViewData["url"] = "https://localhost:" + Request.Host.Port + "/cotizacion/dolar";

            return View(quotation);

        }

        public async Task<IActionResult> Real()
        {
            ChangeController change = new ChangeController(_context);

            var quotation = await change.SetQuotation("BRL", 1);

            ViewData["moneda"] = quotation.Value.moneda;
            ViewData["precio"] = quotation.Value.precio;
            ViewData["url"] = "https://localhost:" + Request.Host.Port + "/cotizacion/real";
            return View(quotation);
           
        }

        public async Task<IActionResult> Euro()
        {
            ChangeController change = new ChangeController(_context);

            var quotation = await change.SetQuotation("EUR", 1);

            ViewData["moneda"] = quotation.Value.moneda;
            ViewData["precio"] = quotation.Value.precio;
            ViewData["url"] = "https://localhost:"+ Request.Host.Port + "/cotizacion/euro" ;
            return View(quotation);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

