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
            this.txtNome = new System.Windows.Forms.Label();
            this.tBoxNome = new System.Windows.Forms.TextBox();
            this.btnCodiceAccesso = new System.Windows.Forms.Button();
            this.tBoxPassword = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            resources.ApplyResources(this.notifyIcon1, "notifyIcon1");
            // 
            // txtNome
            // 
            resources.ApplyResources(this.txtNome, "txtNome");
            this.txtNome.Name = "txtNome";
            // 
            // tBoxNome
            // 
            resources.ApplyResources(this.tBoxNome, "tBoxNome");
            this.tBoxNome.Name = "tBoxNome";
            // 
            // btnCodiceAccesso
            // 
            resources.ApplyResources(this.btnCodiceAccesso, "btnCodiceAccesso");
            this.btnCodiceAccesso.Name = "btnCodiceAccesso";
            this.btnCodiceAccesso.UseVisualStyleBackColor = true;
            this.btnCodiceAccesso.Click += new System.EventHandler(this.btnCodiceAccesso_Click);
            // 
            // tBoxPassword
            // 
            resources.ApplyResources(this.tBoxPassword, "tBoxPassword");
            this.tBoxPassword.Name = "tBoxPassword";
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            // 
            // FormAccesso
            // 
            this.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.tBoxPassword);
            this.Controls.Add(this.btnCodiceAccesso);
            this.Controls.Add(this.tBoxNome);
            this.Controls.Add(this.txtNome);
            this.MaximizeBox = false;
            this.Name = "FormAccesso";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NotifyIcon notifyIcon1;
        private Label txtNome;
        private TextBox tBoxNome;
        private Button btnCodiceAccesso;
        private TextBox tBoxPassword;
        private Label txtPassword;
    }
}