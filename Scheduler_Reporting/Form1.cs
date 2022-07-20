using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http.Headers;
using System.IO;
using System.Text;
using System.Text.Json.Nodes;

namespace Scheduler_Reporting
{
    public partial class FormAccesso : Form
    {
        private const string token = "1|az4Hn5DzmxU5v6584CwK0cQzigEtOZHxa6EMXgpS";
        private const string connStringReporting = "Data Source=80.88.87.40; Database=w133edy7_reporting; User Id=w133edy7_rootreporting; Password=password;";
        private const string connStringDrVeto = "Data Source=(localDb)\\MSSQLLocalDB; Initial Catalog=DrVeto_avec_groupes_actes; Trusted_Connection=True";
        private string? query;

        string test = String.Format("http://reporting.alcyonsoluzionidigitali.it/api/v1/invoices/passive");

        //private const string url = "http://reporting.alcyonsoluzionidigitali.it/api/v1/invoices/active"; -> VECCHIO

        public FormAccesso()
        {
            InitializeComponent();
            ///CONNSSIONE CON DRVETO
            //ConnDrVeto();
            ///CONNESSIONE CON REPORTING
            ConnReporting2();
            //MySqlConnection conn = new MySqlConnection(connStringReporting);
            //conn.Open();
            //query = "select * from invoices";
            //MySqlCommand cmd = new MySqlCommand(query, conn);
            //MySqlDataReader reader = cmd.ExecuteReader();
            //while (reader.Read())
            //{
            //    MessageBox.Show(reader["id"].ToString());
            //}
        }



        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }


        /// <summary>
        /// FUNZIONE CHE SI SCATENA AL CLICK SU INIZIA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCodiceAccesso_Click(object sender, EventArgs e)
        {

            ///VAR PER SYSTEM TRAY (SE AVVIENE CON SUCCESSO TUTTO ALLORA TRUE)
            bool? success = null;
            /// PRENDO VALORE DALLA TXTBOX
            string nome = tBoxNome.Text; //-> Da vedere come gestire il login degli utenti

            try
            {
                ///QUI VERRA ESEGUITO IL COLLEGAMENTO AL DB, E ALL' API

            }
            catch (InvalidCastException err)
            {
                ///SE NON RIESCE
                MessageBox.Show(
                    "Codice sbagliato, o non inserito",
                    "Errore di collegamento",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }
            success = true;//PER PROVA
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
            }

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
        //private void ConnReporting()
        //{
        //    // Create a request for the URL. 		
        //    WebRequest request = WebRequest.Create("http://reporting.alcyonsoluzionidigitali.it/api/v1/invoices/passive");

        //    request.ContentType = "application/json";
        //    request.Headers["Authorization"] = "Bearer 1|az4Hn5DzmxU5v6584CwK0cQzigEtOZHxa6EMXgpS";
        //    // If required by the server, set the credentials.
        //    //request.Credentials = CredentialCache.DefaultCredentials;
        //    // Get the response.
        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //    response.ContentType = "application/json";
            
        //    // Display the status.
        //    MessageBox.Show(response.StatusDescription);
        //    // Get the stream containing content returned by the server.
        //    Stream dataStream = response.GetResponseStream();
        //    // Open the stream using a StreamReader for easy access.
        //    StreamReader reader = new StreamReader(dataStream);
        //    // Read the content.
        //    string responseFromServer = reader.ReadToEnd();
        //    // Display the content.
        //    MessageBox.Show(responseFromServer);

        //    reader.Close();
        //    dataStream.Close();
        //    response.Close();
        //}




        /// <summary>
        /// METODO CHE MI COLLEGA IL DB DI DRVETO. E MI PRENDE I DATI 
        /// </summary>
        private void ConnDrVeto()
        {
            SqlConnection connDrVeto = new SqlConnection(connStringDrVeto);
            connDrVeto.Open();
            query = "select * from actes";
            SqlCommand sqlcmd = new SqlCommand(query, connDrVeto);
            SqlDataReader reader = sqlcmd.ExecuteReader();
            while (reader.Read())
            {
                MessageBox.Show(reader["ACcod"].ToString()+ reader["AClib"].ToString()); //MI STAMPA SOLO IL CAMPO ACcode
            }
        }


        /// <summary>
        /// METODO CHE MI COLLEGA IL DB DI MYSQL. E MI DEVE SPEDIRE I DATI ELABORATI
        /// </summary>
        private async void ConnReporting2()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept
  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                HttpResponseMessage response = await client.GetAsync("http://reporting.alcyonsoluzionidigitali.it/api/v1/invoices");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                MessageBox.Show(responseBody);
            }
        }
    }
}