using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler_Reporting.Models
{
    /// <summary>
    /// CLASSE DOVE INSERISCO I DATI UNA VOLTA PRESI DA DR VETO
    /// </summary>
    public class Data
    {
        public DateTime invoice_date { get; set; }
        public DateTime registration_date { get; set; }
        public string invoice_number { get; set; }
        public string document_type { get; set; }
        public string description { get; set; } = "";
        public string notes { get; set; } = "";
        public int ritenuta { get; set; }
        public string? status { get; set; } = null;
        public List<Item> Items { get; set; } = new List<Item>();
        public string? name { get; set; } = "";
        public string? phone { get; set; } = "";
        public string? website { get; set; } = "";
        public string? email { get; set; } = null;  
        public List<Address> Addresses { get; set; } = new List<Address>();



    }
}
