using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinsApi.Models
{
    public class Result
    {
        public DateTime updated { get; set; }
        public string source  { get; set; }
        public string target  { get; set; }
        public double value { get; set; }
        public double quantity { get; set; }
        public double amount { get; set; }
    }
}
