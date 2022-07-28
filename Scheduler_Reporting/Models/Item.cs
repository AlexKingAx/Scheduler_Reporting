using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler_Reporting.Models
{
    public class Item
    {
        public string name { get; set; }
        public string? description { get; set; } = "";
        public string family { get; set; }
        public int quantity { get; set; }
        /// <summary>
        /// PREZZO SENZA VIRGOLA
        /// </summary>
        public int price { get; set; }
        public int totalTax { get; set; }
        public List<Tax> taxes { get; set; } = new List<Tax>();
        public bool fctax { get; set; } = false;
        public bool indtax { get; set; } = false;
        public int? enpav { get; set; }
        public bool? valid { get; set; } = true;


    }
}
