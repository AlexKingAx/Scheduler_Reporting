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
using System.Linq;

namespace Scheduler_Reporting
{
    public partial class FormAccesso : Form
    {
        
        private const string token_prova = "8|6wYGw55gvAdvPlqColmWowjHLr1UgEO6UDEEMm36";        
        private const string connStringDrVeto = "Data Source=(localDb)\\MSSQLLocalDB; Initial Catalog=DrVeto; Trusted_Connection=True";
        //public string? local_token; // TOKEN PER ACCEDERE A REPOTING
        private string? query;
        private string? userJson;        
        private bool success = false;// VAR PER SYSTEM TRAY (SE AVVIENE CON SUCCESSO TUTTO ALLORA TRUE)

        // LISTA DI OGGETTI FATTURE CHE ARRIVANO DA DRV
        public List<Data> listFromDrVeto = new List<Data>();

        // LISTA DI OGGETTI FATTURE CHE ARRIVANO DA DRV
        public List<Data> listForReporting = new List<Data>();

        public Login local_user = new Login();


        public FormAccesso()
        {
            InitializeComponent();            
        }

        /// <summary>
        /// METODO CHE VIENE ESEGUITO DURANTE IL CARICAMENTO DEL FORM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormAccesso_Load(object sender, EventArgs e)
        {

            /// PROVO CON TRY E CATCH A VEDERE SE ESISTE UN FILE CON I DATI DELL'UTENTE(TOKEN, ETC...)
            /// SE ESISTE METTO SUBITO L'APP NEL SYSTEM TRY 
            /// 
            /// SE INVECE NON ESISTE APRO IL FORM NORMALMENTE NON FACENDO NULLA
            try
            {
                userJson = string.Empty;
                userJson = File.ReadAllText(@"user.json");
                local_user = JsonConvert.DeserializeObject<Login>(userJson);

                if (local_user.token != null || local_user.token != "") success = true;
            }
            catch (Exception)
            {
                success = false;
            }

            SystemTryAppTrasformation(success);// METTO L APP NEL SYSTEM TRY

        }

        /// <summary>
        /// FUNZIONE CHE SI SCATENA AL CLICK SU INIZIA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnCodiceAccesso_Click(object sender, EventArgs e)
        {            

            //token = "8|6wYGw55gvAdvPlqColmWowjHLr1UgEO6UDEEMm36";
            if (local_user.token == null || local_user.token == "" && !success)
            {
                /// PRENDO VALORE DALLA TXTBOX
                local_user.token = tBoxCodice.Text;

                /// MESSAGGIO DI ERRORE
                if (local_user.token == null || local_user.token == "")
                {
                    //MOSTRA MSG "manca token"
                    MessageBox.Show(
                        "Manca Il codice di accesso(token)",
                        "Errore di collegamento",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                        );
                    success = false;
                    return;
                }
                success = true;
            }

            success = await ConnectionTester(success);// TESTO LE CONN

            LoginDataStoring(success);// CREO FILE JSON PER LA PRIMA VOLTA CON I DATI LOGIN

            ///SE TUTTO E CORRETTO ALLORA METTO L'APP NEL SYSTEM TRAY
            SystemTryAppTrasformation(success);

        }

        //CHIUSURA
        private void OnCloseClicked(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Sei sicuro di uscire dall'applicazione?", "Exit message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) Application.Exit();
        }

        /// <summary>
        /// ESEGUE METODO DI PER LA SINCRONIZZAZIONE DEI DATI
        /// </summary>
        private async void OnSyncClicked(object? sender, EventArgs e)
        {
            //GET TABELLA STRUTTURE
            await GetStructureTable();

            //APRO LA CONNESSIONE E GLI MANDO LA QUERY SQL
            SqlConnection connDrVeto = new SqlConnection(connStringDrVeto);
            connDrVeto.Open();
            query = "select FCdate as 'Data Fattura', FCdmaj as 'Data Aggiornamento', FCnumero as 'Numero Fattura', FCtyp as 'Tipologia Documento', FCnumero + ' - ' + CONVERT(VARCHAR, FCdate) as 'Descrizione', FCtauxRA as 'Ritenuta Acconto', FCsold as'Status', FLlib as 'Descrizione Riga' ,FAlib as 'Famiglia drv', FLqte as 'QTA', FLtht as 'Price', FLmttva as 'Tot IVA', FCtx1 as 'Perc IVA 1', FCtva1 as 'IVA 1', FCtx2 as 'Perc IVA 2', FCtva2 as 'IVA 2', FCtx3 as 'Perc IVA 3', FCtva3 as 'IVA 3', FCnom as 'Nome Cliente', FCprenom as 'Cognome', CLtelpor1 as 'Telefono', CLmail1 as 'Email', FCad1 as 'Indirizzo', FCad2 as 'Indirizzo 2', CLvil as 'Citta', PAYS_Nom as 'Nazione', CLcodeFiscal as 'CF Cliente', CLnumtva as 'P iva', CLdept as 'Provincia','Billing' as 'TipologiaIndiirizzo', CLnumtva as 'P.IVA', CLcp as 'CAP', Cabcode as 'Codice Struttura' from FACENT inner join FACLIG on FC_Uid = FL_FAC_Uid inner join CLIENTS on FCcli = CL_Uid inner join ACTES on AC_Uid = FL_ACT_Uid inner join FAMACTE on ACfam_uid = FA_Uid inner join PAYS on CLpays_uid = PAYS_Uid inner join CABINET on FCsite = Cab_Id where FCtyp = 'Facture'";
            SqlCommand sqlcmd = new SqlCommand(query, connDrVeto);
            SqlDataReader reader = sqlcmd.ExecuteReader();

            DataPicker(reader); // PRENDO E INSERISCO I DATI NELLA LISTA 1

            DatesTransformation(); // MODIFICO E INSERISCO I DATI NELLA LISTA 2 PER INVIO

            await DataSender();// MANDA I DATI AL WEBSERVICE

            // MESSAGGIO FINE SCAMBIO COMPLETATO
            MessageBox.Show("Scambio avvenuto con successo", "Sincronizzazione dati", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // SALVO LA DATA DELLO SCAMBIO            
            userJson = File.ReadAllText("user.json");
            local_user = JsonConvert.DeserializeObject<Login>(userJson);
            local_user.last_sync = DateTime.Now;
            userJson = JsonConvert.SerializeObject(local_user);
            File.WriteAllText("user.json", userJson);

            AddingMenuItems();// RICHIAMO IL METODO CHE SCRIVE GLI ITEMS DEL MENU COSI, MI AGGIORNA LO STATO ULTIMO SCAMBIO


            //CHIUSO LE CONNESSIONI DA DRVETO
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

        /// <summary>
        /// TESO LE CONNESSIONI A ENTRAMBI I DB 
        /// </summary>
        /// <param name="success"></param>
        /// <returns></returns>
        private async Task<bool> ConnectionTester(bool success)
        {
            try { success = await Connection_Test(); }
            catch (HttpRequestException err)
            {
                //MOSTRA MSG "manca token"
                MessageBox.Show(
                    "Assenza internet o problema di rete, l'errore " +
                    "si genera quando si prova a fare la connessione a reporting",
                    "Errore di collegamento",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                success = false;
            }

            return success;
        }

        /// <summary>
        /// METODO CHE MI MEMORIZZA IL TOKEN E IL DATI DELLO USER LOCAL IN UN FILE COSI DA NON DOVERLI PIU PRENDERE
        /// </summary>
        /// <param name="success"></param>
        private void LoginDataStoring(bool success)
        {
            /// SALVO I DATI DI ACCESSO IN UN FILE JSON CRIPTANDOLI IN MODO DA RENDERLI SICURI,
            /// PER RENDERLI DISPONIBILI AL PROSSIMO RIAVVIO
            if (success == true)
            {
                //var user = new Login() { token = local_token };
                userJson = JsonConvert.SerializeObject(local_user);
                File.WriteAllText(@"user.json", userJson);

            }
        }

        /// <summary>
        /// METODO CHE TRASFORMA IL FORM IN UN APPLICAZIONE 
        /// CHE GIRA SOLO NEL SYSTETRY, IN BASSO A DESTRA
        /// </summary>
        /// <param name="success"></param>
        private void SystemTryAppTrasformation(bool success)
        {
            if (success == true)
            {
                this.ShowInTaskbar = false;
                notifyIcon1.Visible = true;
                this.Visible = false;
                ///INIZIALIZZO IL MENU E E CREO GLI ELEMENTI
                notifyIcon1.ContextMenuStrip = new ContextMenuStrip();
                AddingMenuItems();

            }
        }

        /// <summary>
        /// QUESTO METODO MI AGGIUNGE GLI ITEMS NEL MENU DEL NOTIFFYICON
        /// IN BASSO A DESTRA
        /// </summary>
        private void AddingMenuItems()
        {
            notifyIcon1.ContextMenuStrip.Items.Clear();

            /// DATI DA INSERIRE NEL METODO ADD
            /// (string text, Image image, EventHandler onClick);
            /// LE FOTO VANNO NELLA CARTELLA BIN/DEBUG ALTRIMENTI NON LE PRENDE                
            notifyIcon1.ContextMenuStrip.Items.Add("Stato " + local_user.last_sync, Image.FromFile("icons/setting.ico"), OnStausClicked);
            notifyIcon1.ContextMenuStrip.Items.Add("Esegui sincronizzazione", Image.FromFile("icons/transfer-arrow.ico"), OnSyncClicked);
            notifyIcon1.ContextMenuStrip.Items.Add("Termina applicazione", Image.FromFile("icons/close.ico"), OnCloseClicked);
        }

        /// <summary>
        /// METODO CHE MI RITORNA LA TAB STRUTTURE
        /// </summary>
        /// <returns></returns>
        private async Task GetStructureTable()
        {
            string url = "http://reporting.alcyonsoluzionidigitali.it/api/v1/structures";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", local_user.token);

                client.DefaultRequestHeaders.ConnectionClose = true;
                                
                //System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var response = await client.GetAsync(url);

                string responseBody = response.Content.ReadAsStringAsync().Result;
            }
        }

        /// <summary>
        /// METODO CHE MANDA I DATI SU REPORTING
        /// </summary>
        /// <returns></returns>
        private async Task DataSender()
        {
            var url = "http://reporting.alcyonsoluzionidigitali.it/api/v1/invoices/import";
            string json;
            foreach (var item in listForReporting)
            {
                json = JsonConvert.SerializeObject(item);

                var data = new StringContent(json, Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", local_user.token);

                    client.DefaultRequestHeaders.ConnectionClose = true;

                    //System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    var response = await client.PostAsync(url, data);

                    string responseBody = response.Content.ReadAsStringAsync().Result;

                    Thread.Sleep(2000);
                }
            }
        }

        /// <summary>
        /// METODO CHE PRENDE I DATI DALLA QUERY E LI INSERISCE NELLA LISTA FROM DRVETO
        /// </summary>
        /// <param name="reader">DATI PRESI DALLA QUERY</param>
        private void DataPicker(SqlDataReader reader)
        {
            while (reader.Read())
            {
                var statusVar = reader.GetBoolean("Status"); // PRENDO IL VALORE CHE CE ALL'INTERNO DELLA RESPONDE DI STATUS

                // CLASSE PRINCIPALE PER RACCOLTA DATI
                var data = new Data();

                string? statusString = null; // CREO VARIABILE CHE MI SERVE PER PASSARE LO STATO DI PAGAMENTO NEL MODO CORRETTO  

                if (statusVar)
                {
                    statusString = "COMPLETED"; // ASSEGNO IL VALORE COMPLETED SE NELLA RESPONSE TROVO TRUE O 1
                    data.due_amount = 0;
                }

                // TOLGO LA VIRGOLA ALLA RITENUTA, SICCOME LO VUOLE COME INTERO
                var ritenutaVar = reader.GetDecimal("Ritenuta Acconto");
                ritenutaVar = ritenutaVar * 100;

                
                try
                {
                    //GESTIONE DEL CORRETTO FORMATO PER LA DATA DA INSERIRE
                    var datetime = reader.GetDateTime("Data Fattura");
                    string year = datetime.Year.ToString();
                    string month = datetime.Month.ToString();
                    string day = datetime.Day.ToString();
                    
                    if(datetime.Month < 10 && datetime.Day <10) data.invoice_date = year + "-0" + month + "-0" + day;

                    else if(datetime.Month < 10) data.invoice_date = year + "-0" + month + "-" + day;

                    else if (datetime.Day < 10) data.invoice_date = year + "-" + month + "-0" + day;

                    else data.invoice_date = year + "-" + month + "-" + day;

                }
                catch (Exception er)
                {

                }
                try
                {
                    //GESTIONE DEL CORRETTO FORMATO PER LA DATA DA INSERIRE
                    var datetime = reader.GetDateTime("Data Aggiornamento");
                    string year = datetime.Year.ToString();
                    string month = datetime.Month.ToString();
                    string day = datetime.Day.ToString();

                    if (datetime.Month < 10 && datetime.Day < 10) data.registration_date = year + "-0" + month + "-0" + day;

                    else if (datetime.Month < 10) data.registration_date = year + "-0" + month + "-" + day;

                    else if (datetime.Day < 10) data.registration_date = year + "-" + month + "-0" + day;

                    else data.registration_date = year + "-" + month + "-" + day;
                }
                catch (Exception er)
                {

                }
                try
                {
                    data.invoice_number = reader.GetString("Numero Fattura");
                }
                catch (Exception er)
                {

                }
                try
                {
                    var d_type = reader.GetString("Tipologia Documento");
                    if(d_type == "Facture") data.document_type = "TD01";

                }
                catch (Exception er)
                {

                }
                try
                {
                    data.description = reader.GetString("Descrizione");
                }
                catch (Exception er)
                {

                }
                try
                {
                    data.ritenuta = (int)ritenutaVar;
                }
                catch (Exception er)
                {

                }
                try
                {
                    data.status = statusString;
                }
                catch (Exception er)
                {

                }
                try
                {
                    data.name = reader.GetString("Nome Cliente") + " ";
                    data.name += reader.GetString("Cognome");
                }
                catch (Exception er)
                {

                }
                try
                {
                    data.phone = reader.GetString("Telefono");
                }
                catch (Exception er)
                {

                }
                try
                {
                    data.email = reader.GetString("Email");                    
                }
                catch (Exception er)
                {

                }

                // TOLGO LA VIRGOLA AL PREZZO, SICCOME LO VUOLE COME INTERO
                var priceVar = reader.GetDecimal("Price");
                priceVar = priceVar * 100;



                // PRENDO RIGA FATTURA
                var item = new Item();
                {
                    item.name = "Riga " + reader.GetString("Descrizione Riga");
                    //fctax
                    //indtax
                    //valid
                };
                try
                {
                    item.description = reader.GetString("Descrizione Riga");
                }
                catch (Exception er)
                {

                }
                try
                {
                    item.family = reader.GetString("Famiglia drv");
                }
                catch (Exception er)
                {

                }
                try
                {
                    item.quantity = (int)reader.GetDecimal("QTA");
                }
                catch (Exception er)
                {

                }
                try
                {
                    item.price = (int)priceVar;
                }
                catch (Exception er)
                {

                }
                try
                {
                    item.totalTax = (int)reader.GetDecimal("Tot IVA");
                }
                catch (Exception er)
                {

                }

                //PRENDO LE PERCENTUALI IVA DOVE NON SONO NULL
                try
                {
                    var tax = new Tax() { percent = (int)reader.GetDecimal("Perc IVA 1") };
                    item.taxes.Add(tax);

                }
                catch (Exception er)
                {

                }

                try
                {
                    var tax = new Tax() { percent = (int)reader.GetDecimal("Perc IVA 2") };
                    item.taxes.Add(tax);
                }
                catch (Exception er)
                {

                }

                try
                {
                    var tax = new Tax() { percent = (int)reader.GetDecimal("Perc IVA 3") };
                    item.taxes.Add(tax);
                }
                catch (Exception er)
                {

                }

                //AGGIUNGO RIGA ALLA FATTURA
                data.items.Add(item);

                //PRENDO GLI INDIRIZZI E I DATI DEL CLIENTE
                var address = new Address();
                try
                {
                    address.address_street_1 = reader.GetString("Indirizzo");
                }
                catch (Exception er)
                {

                }
                try
                {
                    address.address_street_2 = reader.GetString("Indirizzo 2");
                }
                catch (Exception er)
                {

                }
                try
                {
                    address.city = reader.GetString("Citta");
                }
                catch (Exception er)
                {

                }
                try
                {
                    var nation = reader.GetString("Nazione");
                    if (nation == "ITALIA")
                    {
                        address.country_id = 107;
                    }

                }
                catch (Exception er)
                {

                }
                try
                {
                    address.fiscalcode = reader.GetString("CF Cliente");
                }
                catch (Exception er)
                {

                }
                try
                {
                    address.state = reader.GetString("Provincia");
                }
                catch (Exception er)
                {

                }
                try
                {
                    address.vatnumber = reader.GetString("P iva");
                }
                catch (Exception er)
                {

                }
                try
                {
                    address.zip = reader.GetString("CAP");
                }
                catch (Exception er)
                {

                }


                //AGGIUNGO DATI CLIENTE IN GENERAL DATA
                data.addresses.Add(address);

                // AGGIUNGO RIGA DELLA QUERIY SCOMPOSTA NEI VARI DATI ALLA LISTA DATI FROM DRVETO
                listFromDrVeto.Add(data);

            }
        }

        //private T prova<T>(SqlDataReader reader, string s)
        //{
        //    try
        //    {
        //        switch (T.class)
        //        {
        //            default:
        //            break;
        //        }
        //        return reader.GetString
        //    }
        //    catch (Exception)
        //    {                
        //    }
        //}
        private void DatesTransformation()
        {
            foreach (var obj in listFromDrVeto)
            {


                // SE NON MI TROVA UN ELEMENTO NELLA LISTA CON IL NUMERO FATTURA UGUALE A QUELLO
                // DELL ALTRA LISTA ALLORA LO AGGIUNGE PER IL TRASFERIMENTO
                if (!listForReporting.Any(n => n.invoice_number == obj.invoice_number))
                {
                    listForReporting.Add(obj);
                    //MessageBox.Show(obj.ToString());
                }
                else
                {
                    var element = listForReporting.FirstOrDefault(x => x.invoice_number == obj.invoice_number);

                    if (element != null)
                    {
                        listForReporting.Remove(element); // LO RIMUOVO

                        // PASSO LE RIGHE FATTURE NEL CASO CI SIA UN DOPPIONE ALL'INTERNO
                        // DELLA LISTA FROM DR VETO. 
                        // DOPO DI CHE LO AGGIUNGO ALL'ELEMNTO CHE SARA MESSO NELLA LISTA FOR REPORTING. 
                        element.items.AddRange(obj.items);

                        listForReporting.Add(element);// LO REINSERISCO AGGIORNATO                   
                    }
                }
            }
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
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", local_user.token);

                client.DefaultRequestHeaders.ConnectionClose = true;

                //var content = new FormUrlEncodedContent(values);
                //HttpResponseMessage response = new HttpResponseMessage();
                //System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var response = await client.GetAsync("http://reporting.alcyonsoluzionidigitali.it/api/v1/invoices/passive");
                //response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
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
    }
}