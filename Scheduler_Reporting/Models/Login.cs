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
        public string? server_name { get; set; } = "(localDb)\\MSSQLLocalDB";
        public string? db_name { get; set; } = "";
        public string? user_id { get; set; } = "";
        public string? password { get; set; } = "";

        // Used for login with windows user credential
        public bool trusted_connection { get; set; } = true;
        public DateTime? last_sync { get; set; }
        public void SetDrvetoString()
        {
            // For connection with windows credential use trusted connection
            if (this.trusted_connection == true) this.connStringDrVeto = "Data Source=" + this.server_name + "; Initial Catalog=" + this.db_name + "; Trusted_Connection=True; ";

            // Without
            else this.connStringDrVeto = "Data Source=" + this.server_name + "; Initial Catalog=" + this.db_name + "; User Id="+ this.user_id+"; Password="+ this.password+";";


        }
    }
}
