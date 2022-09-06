namespace Scheduler_Reporting
{
    partial class FormAccesso
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAccesso));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.txtCodice = new System.Windows.Forms.Label();
            this.tBoxCodice = new System.Windows.Forms.TextBox();
            this.btnCodiceAccesso = new System.Windows.Forms.Button();
            this.tBoxServerName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtServerName = new System.Windows.Forms.Label();
            this.txtDBName = new System.Windows.Forms.Label();
            this.tBoxDBName = new System.Windows.Forms.TextBox();
            this.txtUserId = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.Label();
            this.tBoxUserId = new System.Windows.Forms.TextBox();
            this.tBoxPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxTrustedConn = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            resources.ApplyResources(this.notifyIcon1, "notifyIcon1");
            // 
            // txtCodice
            // 
            resources.ApplyResources(this.txtCodice, "txtCodice");
            this.txtCodice.Name = "txtCodice";
            // 
            // tBoxCodice
            // 
            resources.ApplyResources(this.tBoxCodice, "tBoxCodice");
            this.tBoxCodice.Name = "tBoxCodice";
            // 
            // btnCodiceAccesso
            // 
            this.btnCodiceAccesso.BackColor = System.Drawing.SystemColors.ButtonFace;
            resources.ApplyResources(this.btnCodiceAccesso, "btnCodiceAccesso");
            this.btnCodiceAccesso.Name = "btnCodiceAccesso";
            this.btnCodiceAccesso.UseVisualStyleBackColor = false;
            this.btnCodiceAccesso.Click += new System.EventHandler(this.btnCodiceAccesso_Click);
            // 
            // tBoxServerName
            // 
            resources.ApplyResources(this.tBoxServerName, "tBoxServerName");
            this.tBoxServerName.Name = "tBoxServerName";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtServerName
            // 
            resources.ApplyResources(this.txtServerName, "txtServerName");
            this.txtServerName.Name = "txtServerName";
            // 
            // txtDBName
            // 
            resources.ApplyResources(this.txtDBName, "txtDBName");
            this.txtDBName.Name = "txtDBName";
            // 
            // tBoxDBName
            // 
            resources.ApplyResources(this.tBoxDBName, "tBoxDBName");
            this.tBoxDBName.Name = "tBoxDBName";
            // 
            // txtUserId
            // 
            resources.ApplyResources(this.txtUserId, "txtUserId");
            this.txtUserId.Name = "txtUserId";
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            // 
            // tBoxUserId
            // 
            resources.ApplyResources(this.tBoxUserId, "tBoxUserId");
            this.tBoxUserId.Name = "tBoxUserId";
            // 
            // tBoxPassword
            // 
            resources.ApplyResources(this.tBoxPassword, "tBoxPassword");
            this.tBoxPassword.Name = "tBoxPassword";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Name = "label2";
            // 
            // checkBoxTrustedConn
            // 
            resources.ApplyResources(this.checkBoxTrustedConn, "checkBoxTrustedConn");
            this.checkBoxTrustedConn.Name = "checkBoxTrustedConn";
            this.checkBoxTrustedConn.UseVisualStyleBackColor = true;
            // 
            // FormAccesso
            // 
            this.AllowDrop = true;
            this.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.checkBoxTrustedConn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tBoxPassword);
            this.Controls.Add(this.tBoxUserId);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(this.tBoxDBName);
            this.Controls.Add(this.txtDBName);
            this.Controls.Add(this.txtServerName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tBoxServerName);
            this.Controls.Add(this.btnCodiceAccesso);
            this.Controls.Add(this.tBoxCodice);
            this.Controls.Add(this.txtCodice);
            this.MaximizeBox = false;
            this.Name = "FormAccesso";
            this.Load += new System.EventHandler(this.FormAccesso_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NotifyIcon notifyIcon1;
        private Label txtCodice;
        private TextBox tBoxCodice;
        private Button btnCodiceAccesso;
        private TextBox tBoxServerName;
        private Label label1;
        private Label txtServerName;
        private Label txtDBName;
        private TextBox tBoxDBName;
        private Label txtUserId;
        private Label txtPassword;
        private TextBox tBoxUserId;
        private TextBox tBoxPassword;
        private Label label2;
        private CheckBox checkBoxTrustedConn;
    }
}