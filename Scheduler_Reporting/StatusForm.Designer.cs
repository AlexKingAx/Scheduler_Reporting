namespace Scheduler_Reporting
{
    partial class StatusForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatusForm));
            this.btnTermina = new System.Windows.Forms.Button();
            this.txtUltimaModifica = new System.Windows.Forms.Label();
            this.tBoxUltimoScambio = new System.Windows.Forms.TextBox();
            this.btnChiudi = new System.Windows.Forms.Button();
            this.Reset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTermina
            // 
            this.btnTermina.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnTermina.Location = new System.Drawing.Point(230, 175);
            this.btnTermina.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnTermina.Name = "btnTermina";
            this.btnTermina.Size = new System.Drawing.Size(117, 63);
            this.btnTermina.TabIndex = 0;
            this.btnTermina.Text = "Termina";
            this.btnTermina.UseVisualStyleBackColor = true;
            // 
            // txtUltimaModifica
            // 
            this.txtUltimaModifica.AutoSize = true;
            this.txtUltimaModifica.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtUltimaModifica.Location = new System.Drawing.Point(17, 15);
            this.txtUltimaModifica.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtUltimaModifica.Name = "txtUltimaModifica";
            this.txtUltimaModifica.Size = new System.Drawing.Size(279, 32);
            this.txtUltimaModifica.TabIndex = 1;
            this.txtUltimaModifica.Text = "ULTIMO SCAMBIO DATI:";
            // 
            // tBoxUltimoScambio
            // 
            this.tBoxUltimoScambio.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tBoxUltimoScambio.Location = new System.Drawing.Point(17, 72);
            this.tBoxUltimoScambio.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tBoxUltimoScambio.Name = "tBoxUltimoScambio";
            this.tBoxUltimoScambio.ReadOnly = true;
            this.tBoxUltimoScambio.Size = new System.Drawing.Size(453, 39);
            this.tBoxUltimoScambio.TabIndex = 2;
            // 
            // btnChiudi
            // 
            this.btnChiudi.BackColor = System.Drawing.Color.Silver;
            this.btnChiudi.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnChiudi.Location = new System.Drawing.Point(356, 175);
            this.btnChiudi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnChiudi.Name = "btnChiudi";
            this.btnChiudi.Size = new System.Drawing.Size(117, 63);
            this.btnChiudi.TabIndex = 3;
            this.btnChiudi.Text = "Chiudi";
            this.btnChiudi.UseVisualStyleBackColor = false;
            this.btnChiudi.Click += new System.EventHandler(this.btnChiudi_Click);
            // 
            // Reset
            // 
            this.Reset.BackColor = System.Drawing.Color.MistyRose;
            this.Reset.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Reset.Location = new System.Drawing.Point(12, 175);
            this.Reset.Name = "Reset";
            this.Reset.Size = new System.Drawing.Size(117, 63);
            this.Reset.TabIndex = 4;
            this.Reset.Text = "Reset";
            this.Reset.UseVisualStyleBackColor = false;
            this.Reset.Click += new System.EventHandler(this.Reset_Click);
            // 
            // StatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(489, 258);
            this.Controls.Add(this.Reset);
            this.Controls.Add(this.btnChiudi);
            this.Controls.Add(this.tBoxUltimoScambio);
            this.Controls.Add(this.txtUltimaModifica);
            this.Controls.Add(this.btnTermina);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "StatusForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scheduler status";
            this.Load += new System.EventHandler(this.StatusForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnTermina;
        private Label txtUltimaModifica;
        private TextBox tBoxUltimoScambio;
        private Button btnChiudi;
        private Button Reset;
    }
}