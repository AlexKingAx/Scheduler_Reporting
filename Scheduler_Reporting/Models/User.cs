using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler_Reporting.Models
{
    public class User
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? password { get; set; }
        public string? role { get; set; }
        public string? remember_token { get; set; }
        public bool? active { get; set; }
        public string? facebook_id { get; set; }
        public string? google_id { get; set; }
        public string? github_id { get; set; }
        public string? contact_name { get; set; }
        public string? website { get; set; }
        public bool? enable_portal { get; set; }
        public uint? curreny_id { get; set; }
        public uint? company_id { get; set; }       
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public uint? creator_id { get; set; }
    }
}
