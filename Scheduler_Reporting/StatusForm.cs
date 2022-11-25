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
            tBoxYear.Text = local_user.start_data_trans.Year.ToString() == "1" ? "ALL" : local_user.start_data_trans.Year.ToString();
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

        private void btnDataChange_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Stai cambiando la data dello scambio, sei sicuro? Questa modifica non ha effetto per i trasferimenti già in corso.", "Data change message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (tBoxYear.Text != "ALL" && Int32.TryParse(tBoxYear.Text, out int year))
                {
                    // 
                    local_user.start_data_trans = new DateTime(year, 1, 1);
                    local_user.end_data_trans = new DateTime(year, 12, 31);
                    local_user.SetOnlyInThisDataString();
                }
                else if (tBoxYear.Text == "ALL")
                {
                    // If ALL create empty DateTime
                    local_user.start_data_trans = new DateTime();
                    local_user.end_data_trans = new DateTime();
                    local_user.SetOnlyInThisDataString();
                }
                else
                {
                    MessageBox.Show("L'anno inserito non è corretto! Attenzione inserire solo l'anno, oppure ALL per tutto selezionare tutte le fatture presenti su DrVeto", "Data change message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    return;
                }

                // Deleteing old user.json file
                File.Delete(@"user.json");

                // Create new user.json file with new data
                userJson = JsonConvert.SerializeObject(local_user);
                File.WriteAllText(@"user.json", userJson);

                Application.Restart(); 
                Environment.Exit(0);
            }
        }
    }
}
