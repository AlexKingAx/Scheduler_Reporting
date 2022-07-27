using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler_Reporting.Models
{
    public class Login
    {
       public string? token { get; set; }
       public DateTime? last_sync { get; set; }
    }
}
