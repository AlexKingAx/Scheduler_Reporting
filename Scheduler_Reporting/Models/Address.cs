using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler_Reporting.Models
{
    public class Address
    {
        public string address_street_1 { get; set; }
        public string? address_street_2 { get; set; } = "";
        public string? city { get; set; } = "";
        public int? country_id { get; set; } = null;
        public string? fiscalcode { get; set; } = "";
        public string? state { get; set; } = "";

        //SEMPRE COSI NON DEVE CAMBIARE
        public const string type = "billing";
        /// <summary>
        /// p.iva CLIENTE
        /// </summary>
        public string? vatnumber { get; set; } = "";

        /// <summary>
        /// CAP, CODICE POSTALE
        /// </summary>
        public string? zip { get; set; } = "";



    }
}
