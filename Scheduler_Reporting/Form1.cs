using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http.Headers;
using System.IO;
using System.Text;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;

namespace Scheduler_Reporting
{
    public partial class FormAccesso : Form
    {
        public string token = "";
        private const string token_prova = "8|6wYGw55gvAdvPlqColmWowjHLr1UgEO6UDEEMm36";
        private const string connStringReporting = "Data Source=80.88.87.40; Database=w133edy7_reporting; User Id=w133edy7_rootreporting; Password=password;";
        private const string connStringDrVeto = "Data Source=(localDb)\\MSSQLLocalDB; Initial Catalog=DrVeto; Trusted_Connection=True";
        private string? query;

        string test = String.Format("http://reporting.alcyonsoluzionidigitali.it/api/v1/invoices/passive");


        public FormAccesso()
        {
            InitializeComponent();

        }



        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }


        /// <summary>
        /// FUNZIONE CHE SI SCATENA AL CLICK SU INIZIA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnCodiceAccesso_Click(object sender, EventArgs e)
        {

            ///VAR PER SYSTEM TRAY (SE AVVIENE CON SUCCESSO TUTTO ALLORA TRUE)
            bool? success = null;            
            /// PRENDO VALORE DALLA TXTBOX C
            token = tBoxCodice.Text;
            if(token==null || token == "")
            {
                //MOSTRA MSG "manca token"
                MessageBox.Show(
                    "Manca Il codice di accesso(token)",
                    "Errore di collegamento",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                return;
            }

           success = await Connection_Test();

            ///SE TUTTO E CORRETTO ALLORA METTO L'APP NEL SYSTEM TRAY
            if (success == true)
            {
                this.ShowInTaskbar = false;
                notifyIcon1.Visible = true;
                this.Visible = false;
                ///INIZIALIZZO IL MENU E E CREO GLI ELEMENTI
                notifyIcon1.ContextMenuStrip = new ContextMenuStrip();
                /// DATI DA INSERIRE NEL METODO ADD
                /// (string text, Image image, EventHandler onClick);
                /// LE FOTO VANNO NELLA CARTELLA BIN/DEBUG ALTRIMENTI NON LE PRENDE                
                notifyIcon1.ContextMenuStrip.Items.Add("Stato", Image.FromFile("icons/setting.ico"), OnStausClicked);
                notifyIcon1.ContextMenuStrip.Items.Add("Esegui sincronizzazione", Image.FromFile("icons/transfer-arrow.ico"), OnSyncClicked);
                notifyIcon1.ContextMenuStrip.Items.Add("Termina applicazione", Image.FromFile("icons/close.ico"), OnCloseClicked);

            }

        }

        //CHIUSURA
        private void OnCloseClicked(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Sei sicuro di uscire dall'applicazione?", "Exit message", MessageBoxButtons.YesNo, MessageBoxIcon.Question)== DialogResult.Yes) Application.Exit(); 
        }

        /// <summary>
        /// ESEGUE METODO DI PER LA SINCRONIZZAZIONE DEI DATI
        /// </summary>
        private void OnSyncClicked(object? sender, EventArgs e)
        {
            SqlConnection connDrVeto = new SqlConnection(connStringDrVeto);
            connDrVeto.Open();
            query = "select * from actes";
            SqlCommand sqlcmd = new SqlCommand(query, connDrVeto);
            SqlDataReader reader = sqlcmd.ExecuteReader();
            sqlcmd.Dispose();
            connDrVeto.Dispose();
        }


        /// <summary>
        /// METODO CH VIENE ESEGUITO AL CLICK SU STATO NEL 
        /// SYSTEM TRAY
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void OnStausClicked(object? sender, EventArgs e)
        {
            StatusForm statusForm = new StatusForm();
            statusForm.ShowDialog();
        }

        private async Task<bool> Connection_Test()
        {
            try
            {
                ///CONNSSIONE CON DRVETO
                bool conDrVSuc = ConnDrVeto();
                ///CONNESSIONE CON REPORTING
                bool conRepSuc = await ConnReporting();
                if (conDrVSuc && conRepSuc)
                {                    
                    return true;  //UNA VOLTA COMPLETATO TUTTO SE NON HA DATO ERRORI MI IMPOSTA SUCCES SU TRUE
                }

                //GENERA UN ERRORE CHE MI MANDA NEL CATCH
                throw new NotImplementedException();


            }
            catch (NotImplementedException err)
            {
                ///SE NON RIESCE
                MessageBox.Show(
                    "Codice sbagliato, o non inserito",
                    "Errore di collegamento",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                return false;
            }
        }

        /// <summary>
        /// METODO CHE MI COLLEGA IL DB DI DRVETO. E MI PRENDE I DATI 
        /// </summary>
        private bool ConnDrVeto()
        {
            SqlConnection connDrVeto = new SqlConnection(connStringDrVeto);
            connDrVeto.Open();
            query = "select * from actes";
            SqlCommand sqlcmd = new SqlCommand(query, connDrVeto);
            SqlDataReader reader = sqlcmd.ExecuteReader();
            if (reader.Read())
            {
                sqlcmd.Dispose();
                connDrVeto.Dispose();
                return true;
            }
            MessageBox.Show(
            "Connessione a DrVeto fallita",
            "Errore di collegamento",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
            );
            sqlcmd.Dispose();
            connDrVeto.Dispose();
            return false;

            //while (reader.Read())
            //{
            //    MessageBox.Show(reader["ACcod"].ToString() + reader["AClib"].ToString()); //MI STAMPA SOLO IL CAMPO ACcode
            //}
        }


        /// <summary>
        /// METODO CHE MI COLLEGA IL DB DI MYSQL. E MI DEVE SPEDIRE I DATI ELABORATI
        /// </summary>
        private async Task<bool> ConnReporting()
        {
            var values = new Dictionary<string, string>();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

                client.DefaultRequestHeaders.ConnectionClose = true;

                //var content = new FormUrlEncodedContent(values);
                //HttpResponseMessage response = new HttpResponseMessage();
                //System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var response2 = await client.GetAsync("http://reporting.alcyonsoluzionidigitali.it/api/v1/invoices/passive");
                //response.EnsureSuccessStatusCode();
                string responseBody = await response2.Content.ReadAsStringAsync();
                //MessageBox.Show(responseBody);
                int stringErr = responseBody.IndexOf("Unauthenticated");
                if ((responseBody != "" || responseBody != null) && stringErr == -1)
                {
                    //PER CHIUDERE LA CONNESSIONE DOPO CHE LA RICHEISTA E STATA MANDATA
                    return true;
                }                
            }
            return false;
        }

        private void FormAccesso_Load(object sender, EventArgs e)
        {

        }
    }
}