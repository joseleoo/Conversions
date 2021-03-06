﻿using CoinsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
namespace CoinsApi.Controllers
{
    [Route("cotizacion/")]
    [ApiController]
    public class ChangeController : ControllerBase
    {

        private readonly QuoContext _context;
        public ChangeController(QuoContext context)
        {
            _context = context;

            if (_context.Quotations.Count() == 0)
            {
                _context.Quotations.Add(new Quotation { moneda = "", precio = 0 });
                _context.SaveChanges();
            }

            #region ServiceMethods
        }
        /// <summary>
        /// get all quotations
        /// </summary>
        /// <returns></returns>
        // GET: cotizacion/change
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quotation>>> GetQuotations()
        {
            return await _context.Quotations.ToListAsync();
        }

        /// <summary>
        /// get quotation by moneda (coin)
        /// </summary>
        /// <param name="moneda"></param>
        /// <returns></returns>
        // GET: cotizacion/change/5
        [HttpGet("{moneda}")]
        public async Task<ActionResult<IEnumerable<Quotation>>> GetQuotation(string moneda)
        {

            var quotation = await _context.Quotations.Where(x => x.moneda == moneda).ToListAsync();

            if (quotation == null)
            {
                return NotFound();
            }

            return quotation;
        }

        /// <summary>
        /// save a new quotations
        /// </summary>
        /// <param name="moneda">coin's name</param>
        /// <returns></returns>
        // POST: cotizacion/change
        [HttpPost("{moneda}")]
        public async Task<ActionResult<Quotation>> PostQuotation(string moneda)
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("https://api.cambio.today/v1/quotes/" + moneda + "/ARS/json?quantity=1&key=2874|s~L^ud9o65CLVK6dnW30PLHCAUj0ZGiF");
            var change = JsonConvert.DeserializeObject<Change>(json);
            Quotation quotation = new Quotation
            {
                moneda = SetInitials(change.result.source),
                precio = change.result.amount
            };

            _context.Quotations.Add(quotation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetQuotation), new { id = quotation.Id }, quotation);
        }
        /// <summary>
        /// delete all quotations
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: cotizacion/change/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuotations()
        {
            var quotations = await _context.Quotations.ToListAsync();

            if (quotations == null)
            {
                return NotFound();
            }

            _context.Quotations.RemoveRange(quotations);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion


        #region Methods

        /// <summary>
        /// method that is used to show in views conversions
        /// </summary>
        /// <param name="Source">origin coin in api today format</param>
        /// <param name="Quantity">quantity to convert</param>
        /// <returns></returns>
        public async Task<ActionResult<Quotation>> SetQuotation(string Source, int Quantity)
        {
            if (_context.Quotations.Where(x => x.moneda == SetInitials(Source)).Count() == 0)
            {
                await PostQuotation(Source);
            }

            var httpClient = new HttpClient();
            var jsonApi = await httpClient.GetStringAsync("https://api.cambio.today/v1/quotes/" + Source + "/ARS/json?quantity=" + Quantity + "&key=2874|s~L^ud9o65CLVK6dnW30PLHCAUj0ZGiF");
            var jsonDeserialized = JsonConvert.DeserializeObject<Change>(jsonApi);
            Quotation quotation = new Quotation
            {
                moneda = jsonDeserialized.result.source,
                precio = jsonDeserialized.result.amount
            };
            return quotation;

        }
        /// <summary>
        /// convert the initials of the coins
        /// </summary>
        /// <param name="initial">initial to convert to complete description</param>
        /// <returns>string complete coin description</returns>
        private string SetInitials(string initial)
        {
            string coinName;
            switch (initial)
            {
                case "USD":
                    coinName = "dolar";
                    break;
                case "BRL":
                    coinName = "real";
                    break;
                case "EUR":
                    coinName = "euro";
                    break;
                default:
                    coinName = initial;
                    break;
            }
            return coinName;

        }
        #endregion








    }
}
