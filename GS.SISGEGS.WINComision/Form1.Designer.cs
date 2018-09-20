namespace GS.SISGEGS.WINComision
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnProcesar = new System.Windows.Forms.Button();
            this.cboEmpresa = new System.Windows.Forms.ComboBox();
            this.lblEmpresa = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnProcesar
            // 
            this.btnProcesar.Location = new System.Drawing.Point(58, 177);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(142, 25);
            this.btnProcesar.TabIndex = 0;
            this.btnProcesar.Text = "Procesar";
            this.btnProcesar.UseVisualStyleBackColor = true;
            this.btnProcesar.Click += new System.EventHandler(this.button1_Click);
            // 
            // cboEmpresa
            // 
            this.cboEmpresa.FormattingEnabled = true;
            this.cboEmpresa.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cboEmpresa.Location = new System.Drawing.Point(114, 49);
            this.cboEmpresa.Name = "cboEmpresa";
            this.cboEmpresa.Size = new System.Drawing.Size(121, 21);
            this.cboEmpresa.TabIndex = 1;
            this.cboEmpresa.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.AutoSize = true;
            this.lblEmpresa.Location = new System.Drawing.Point(44, 52);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Size = new System.Drawing.Size(51, 13);
            this.lblEmpresa.TabIndex = 2;
            this.lblEmpresa.Text = "Empresa:";
            this.lblEmpresa.Click += new System.EventHandler(this.label1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.lblEmpresa);
            this.Controls.Add(this.cboEmpresa);
            this.Controls.Add(this.btnProcesar);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnProcesar;
        private System.Windows.Forms.ComboBox cboEmpresa;
        private System.Windows.Forms.Label lblEmpresa;
    }
}

