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
            this.txtSql = new System.Windows.Forms.Label();
            this.tBoxSql = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
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
            // txtSql
            // 
            resources.ApplyResources(this.txtSql, "txtSql");
            this.txtSql.Name = "txtSql";
            // 
            // tBoxSql
            // 
            resources.ApplyResources(this.tBoxSql, "tBoxSql");
            this.tBoxSql.Name = "tBoxSql";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // FormAccesso
            // 
            this.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tBoxSql);
            this.Controls.Add(this.txtSql);
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
        private Label txtSql;
        private TextBox tBoxSql;
        private Label label1;
    }
}