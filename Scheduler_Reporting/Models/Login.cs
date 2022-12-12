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
        public string OnlyInThisData;
        public string? token { get; set; }
        public string? server_name { get; set; } = "(localDb)\\MSSQLLocalDB";
        public string? db_name { get; set; } = "";
        public string? user_id { get; set; } = "";
        public string? password { get; set; } = "";

        // Used for login with windows user credential
        public bool trusted_connection { get; set; } = true;
        public DateTime? last_sync { get; set; }

        // Used for change date of data transfer 
        public DateTime start_data_trans { get; set; } = new DateTime();
        public DateTime end_data_trans { get; set; } = new DateTime();
        public void SetDrvetoString()
        {
            // For connection with windows credential use trusted connection
            if (this.trusted_connection == true) this.connStringDrVeto = "Data Source=" + this.server_name + "; Initial Catalog=" + this.db_name + "; Trusted_Connection=True; ";

            // Without
            else this.connStringDrVeto = "Data Source=" + this.server_name + "; Initial Catalog=" + this.db_name + "; User Id="+ this.user_id+"; Password="+ this.password+";";


        }
        public void SetOnlyInThisDataString()
        {
            if (this.start_data_trans.Year.ToString().ToString() == "1") this.OnlyInThisData = "";
            else this.OnlyInThisData = " and FCdcrea >= '" + this.start_data_trans.ToString("yyyy/MM/dd") + "' and FCdcrea <= '" + this.end_data_trans.ToString("yyyy/MM/dd") + "'";

        }
    }
}
