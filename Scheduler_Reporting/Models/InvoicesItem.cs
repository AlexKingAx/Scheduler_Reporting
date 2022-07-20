using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler_Reporting.Models
{
    public class InvoicesItem
    {
        public uint id { get; set; }
        public string? name { get; set; }
        public string? descrption { get; set; }
        public string? discount_type { get; set; }
        public ulong? price { get; set; }
        public decimal? quantity { get; set; }
        public decimal? discount { get; set; }
        public ulong? discount_val { get; set; }
        public ulong? tax { get; set; }
        public string? tax_motivation { get; set; }
        public bool fctax { get; set; } = false;
        public bool indtax { get; set; } = false;
        public ulong? total { get; set; }
        public uint? invoice_id { get; set; }
        public uint? category_id { get; set; }
        public uint? subcategory_id { get; set; }
        public uint? item_id { get; set; }
        public uint? company_id { get; set; }
        public ulong? structure_id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
