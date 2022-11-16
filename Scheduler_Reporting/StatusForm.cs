using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Scheduler_Reporting;
using Scheduler_Reporting.Models;

namespace Scheduler_Reporting
{
    public partial class StatusForm : Form
    {
        public string? userJson;
        public Login local_user;
        public StatusForm()
        {
            InitializeComponent();
            userJson = File.ReadAllText("user.json");
            local_user = JsonConvert.DeserializeObject<Login>(userJson);
        }

        private void btnChiudi_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StatusForm_Load(object sender, EventArgs e)
        {
            tBoxUltimoScambio.Text = local_user.last_sync.ToString();
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Sei sicuro di voler eseguire il reset?", "Reset message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                File.Delete(@"user.json"); Application.Restart(); Environment.Exit(0);
            }
        }

        private void btnTermina_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Sei sicuro di uscire dall'applicazione?", "Exit message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) Application.Exit();
        }
    }
}
