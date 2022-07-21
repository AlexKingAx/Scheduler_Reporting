using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler_Reporting.Models
{
    public class Invoice
    {
        public uint id { get; set; }
        public DateTime invoice_date { get; set; }
        public DateTime registratio_date { get; set; }
        public DateTime due_date { get; set; }
        public string? invoice_number { get; set; }
        public string? reference_number { get; set; }
        public string? description { get; set; }
        public string? status { get; set; }
        public string? paid_status { get; set; }
        public string? document_type { get; set; }
        public string? invoice_type { get; set; }
        public string? aruba_id { get; set; }
        public string? tax_per_item { get; set; }
        public string? discount_per_item { get; set; }
        public string? notes { get; set; }
        public string? discount_type { get; set; }
        public decimal? discount { get; set; }
        public ulong? discount_val { get; set; }
        public ulong? sub_total { get; set; }
        public ulong? total { get; set; }
        public ulong? tax { get; set; }
        public bool? reverse_charge { get; set; }
        public ulong? ritenuta { get; set; }
        public ulong? due_amount { get; set; }
        public bool? sent { get; set; }
        public bool? viewed { get; set; }
        public string? unique_hash { get; set; }
        public uint? invoice_template_id { get; set; }
        public uint? user_id { get; set; }
        public uint? company_id { get; set; }        
        public DateTime? created_at { get; set; }
        public uint? creator_id { get; set; }
        public uint? structure_id { get; set; }

        /// <summary>
        /// metodo per la generazione del file json da inviare
        /// </summary>
        public void GenerateJSON()
        {

        }
    }
}
