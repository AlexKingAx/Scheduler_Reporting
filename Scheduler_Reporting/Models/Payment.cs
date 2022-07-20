using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler_Reporting.Models
{
    public class payment
    {
        public ulong id { get; set; }
        public string? payment_number { get; set; }
        public DateTime? payment_date { get; set; }
        public string? notes { get; set; }
        public ulong? amount { get; set; }
        public string? unique_hash { get; set; }
        public uint? user_id { get; set; }
        public uint? invoice_id { get; set; }
        public uint? deadline_id { get; set; }
        public uint? company_id { get; set; }
        public uint? payment_method_id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public uint? creator_id { get; set; }
    }
}
