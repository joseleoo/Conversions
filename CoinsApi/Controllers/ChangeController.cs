using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoinsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CoinsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangeController : ControllerBase
    {




        public async Task<ActionResult<Quotation>> SetQuotation(string Source, int Quantity)
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("https://api.cambio.today/v1/quotes/"+Source+"/USD/json?quantity="+Quantity+"&key=2874|s~L^ud9o65CLVK6dnW30PLHCAUj0ZGiF");
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
