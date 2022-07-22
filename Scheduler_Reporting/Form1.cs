using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http.Headers;
using System.IO;
using System.Text;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using System.Data;
using Scheduler_Reporting.Models;

namespace Scheduler_Reporting
{
    public partial class FormAccesso : Form
    {
        public string token = "8|6wYGw55gvAdvPlqColmWowjHLr1UgEO6UDEEMm36";
        private const string token_prova = "8|6wYGw55gvAdvPlqColmWowjHLr1UgEO6UDEEMm36";
        private const string connStringReporting = "Data Source=80.88.87.40; Database=w133edy7_reporting; User Id=w133edy7_rootreporting; Password=password;";
        private const string connStringDrVeto = "Data Source=(localDb)\\MSSQLLocalDB; Initial Catalog=DrVeto; Trusted_Connection=True";
        private string? query;

        // LISTA DI OGGETTI FATTURE CHE ARRIVANO DA DRV
        public List<Data> listFromDrVeto = new List<Data>();

        // LISTA DI OGGETTI FATTURE CHE ARRIVANO DA DRV
        public List<Data> dataForReporting = new List<Data>();

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
            if (token == null || token == "")
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
            if (MessageBox.Show("Sei sicuro di uscire dall'applicazione?", "Exit message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) Application.Exit();
        }

        /// <summary>
        /// ESEGUE METODO DI PER LA SINCRONIZZAZIONE DEI DATI
        /// </summary>
        private void OnSyncClicked(object? sender, EventArgs e)
        {
            SqlConnection connDrVeto = new SqlConnection(connStringDrVeto);
            connDrVeto.Open();
            query = "select FCdate as 'Data Fattura', FCdmaj as 'Data Aggiornamento', FCnumero as 'Numero Fattura', FCtyp as 'Tipologia Documento', FCnumero + ' - ' + CONVERT(VARCHAR, FCdate) as 'Descrizione', FCtauxRA as 'Ritenuta Acconto', FCsold as'Status', FLdes as 'Descrizione Riga' ,ACfam as 'Famiglia dr.veto', FLqte as 'QTA', FLmttva as 'Tot IVA', FCtx1 as 'Perc IVA 1', FCtva1 as 'IVA 1', FCtx2 as 'Perc IVA 2', FCtva2 as 'IVA 2', FCtx3 as 'Perc IVA 3', FCtva3 as 'IVA 3', FCnom as 'Nome Cliente', FCprenom as 'Cognome', CLtelpor1 as 'Telefono', CLmail1 as 'Email Cliente', FCad1 as 'Indirizzo', FCad2 as 'Indirizzo 2', CLvil as 'Citta', PAYS_Uid as 'Nazione', CLcodeFiscal as 'CF Cliente', CLnumtva as 'P.iVA', CLdept as 'PROVINCIA','Billing' as 'TipologiaIndiirizzo', CLnumtva as 'P.IVA', CLcp as 'CAP' from FACENT inner join FACLIG on FC_Uid = FL_FAC_Uid inner join CLIENTS on FCcli = CL_Uid inner join ACTES on AC_Uid = FL_ACT_Uid inner join FAMACTE on ACfam_uid = FA_Uid inner join PAYS on CLpays_uid = PAYS_Uid where FCtyp = 'Facture'";
            SqlCommand sqlcmd = new SqlCommand(query, connDrVeto);
            SqlDataReader reader = sqlcmd.ExecuteReader();
            while (reader.Read())
            {
                var statusVar = reader.GetBoolean("Status"); // PRENDO IL VALORE CHE CE ALL'INTERNO DELLA RESPONDE DI STATUS

                string? statusString = null; // CREO VARIABILE CHE MI SERVE PER PASSARE LO STATO DI PAGAMENTO NEL MODO CORRETTO  

                if (statusVar) statusString = "COMPLETED"; // ASSEGNO IL VALORE COMPLETED SE NELLA RESPONSE TROVO TRUE O 1

                listFromDrVeto.Add(new Data()
                {

                });


                //CREO OGGETTO INVOICE NELLA LISTA DELLE FATTURE
                //invoicesDataFromDrV.Add(new Invoice()
                //{
                //    invoice_date = reader.GetDateTime("Data Fattura"),
                //    registratio_date = reader.GetDateTime("Data Aggiornamento"),
                //    invoice_number = reader.GetString("Numero Fattura"),
                //    document_type = reader.GetString("Tipologia Documento"),
                //    description = reader.GetString("Descrizione"),
                //    notes = null,
                //    ritenuta = (ulong)reader.GetInt64("Ritenuta Acconto"),
                //    status = statusString
                //});

                ////CREO OGGETTO INVOICEITEMS NELLA LISTA DELLE RIGHE FATTURA
                //invoiceItemsDataFromDrV.Add(new InvoicesItem()
                //{
                //    name = reader.GetString("Nome Cliente"),
                //    description = reader.GetString("Descrizione Riga"),
                //    quantity = reader.GetInt32(""),

                //});

                ////CREO OGGETTO USER NELLA LISTA DELLE RIGHE FATTURA
                //usersDataFromDrV.Add(new User()
                //{
                //    name = reader.GetString("Nome Cliente"),  
                //    phone = reader.GetString("Telefono"),
                //    email = reader.GetString("Email Cliente"),
                //    role = "customer",
                //    contact_name = reader.GetString("Nome Cliente") + reader.GetString("Cognome"),
                    

                //});

                MessageBox.Show(reader.GetDateTime("Data Fattura").ToString());
            }
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