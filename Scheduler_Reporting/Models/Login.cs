using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler_Reporting.Models
{
    public class Login
    {
        public string connStringDrVeto;
        public string? token { get; set; }
        public string? sql_server { get; set; } = "(localDb)\\MSSQLLocalDB";
        public DateTime? last_sync { get; set; }
        public void SetDrvetoString()
        {
            this.connStringDrVeto = "Data Source=" + this.sql_server + "; Initial Catalog=DrVeto; Trusted_Connection=True"; 
        }
    }
}
