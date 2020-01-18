using CoinsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
namespace CoinsApi.Controllers
{
    [Route("cotizacion/[controller]")]
    [ApiController]
    public class ChangeController : ControllerBase
    {

        private readonly QuoContext _context;  

        // GET: cotizacion/change
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quotation>>> GetQuotations()
        {
            return await _context.Quotations.ToListAsync();
        }

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

        // POST: cotizacion/change
        [HttpPost("{moneda}")]
        public async Task<ActionResult<Quotation>> PostQuotation(string moneda)
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("https://api.cambio.today/v1/quotes/"+ moneda + "/ARS/json?quantity=1&key=2874|s~L^ud9o65CLVK6dnW30PLHCAUj0ZGiF");
            var change = JsonConvert.DeserializeObject<Change>(json);
            Quotation quotation= new Quotation
            {
                moneda = change.result.source,
                precio = change.result.amount
            };

            _context.Quotations.Add(quotation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetQuotation), new { id = quotation.Id }, quotation);
        }

        // DELETE: cotizacion/change/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.Quotations.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.Quotations.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// method that is used to show in views cconversions
        /// </summary>
        /// <param name="Source">origin coin</param>
        /// <param name="Quantity">quantity to convert</param>
        /// <returns></returns>
        public async Task<ActionResult<Quotation>> SetQuotation(string Source, int Quantity)
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("https://api.cambio.today/v1/quotes/" + Source + "/USD/json?quantity=" + Quantity + "&key=2874|s~L^ud9o65CLVK6dnW30PLHCAUj0ZGiF");
            var dataConversion = JsonConvert.DeserializeObject<Change>(json);
            Quotation quotation = new Quotation
            {
                moneda = dataConversion.result.source,
                precio = dataConversion.result.amount
            };
            return quotation;

        }








    }
}
