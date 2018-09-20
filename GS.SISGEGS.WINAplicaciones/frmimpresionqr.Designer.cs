namespace GS.SISGEGS.WINAplicaciones
{
    partial class frmimpresionqr
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmimpresionqr));
            this.gvwguiaventa = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Empresa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpGuia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumeroGuia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AgendaNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FlgImpreso = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtnombre = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkimpreso = new System.Windows.Forms.CheckBox();
            this.btnconsultar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpfechaf = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpfechai = new System.Windows.Forms.DateTimePicker();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnureimprimir = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.gvwguiaventa)).BeginInit();
            this.panel1.SuspendLayout();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // gvwguiaventa
            // 
            this.gvwguiaventa.AllowUserToAddRows = false;
            this.gvwguiaventa.AllowUserToDeleteRows = false;
            this.gvwguiaventa.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvwguiaventa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvwguiaventa.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.Empresa,
            this.OpGuia,
            this.NumeroGuia,
            this.Fecha,
            this.AgendaNombre,
            this.FlgImpreso});
            this.gvwguiaventa.ContextMenuStrip = this.menu;
            this.gvwguiaventa.Location = new System.Drawing.Point(2, 77);
            this.gvwguiaventa.Name = "gvwguiaventa";
            this.gvwguiaventa.ReadOnly = true;
            this.gvwguiaventa.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvwguiaventa.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvwguiaventa.Size = new System.Drawing.Size(799, 378);
            this.gvwguiaventa.TabIndex = 0;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "Id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 50;
            // 
            // Empresa
            // 
            this.Empresa.DataPropertyName = "Empresa";
            this.Empresa.HeaderText = "Empresa";
            this.Empresa.Name = "Empresa";
            this.Empresa.ReadOnly = true;
            // 
            // OpGuia
            // 
            this.OpGuia.DataPropertyName = "OpGuia";
            this.OpGuia.HeaderText = "OpGuia";
            this.OpGuia.Name = "OpGuia";
            this.OpGuia.ReadOnly = true;
            // 
            // NumeroGuia
            // 
            this.NumeroGuia.DataPropertyName = "NumeroGuia";
            this.NumeroGuia.HeaderText = "NumeroGuia";
            this.NumeroGuia.Name = "NumeroGuia";
            this.NumeroGuia.ReadOnly = true;
            // 
            // Fecha
            // 
            this.Fecha.DataPropertyName = "Fecha";
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            // 
            // AgendaNombre
            // 
            this.AgendaNombre.DataPropertyName = "AgendaNombre";
            this.AgendaNombre.HeaderText = "Cliente";
            this.AgendaNombre.Name = "AgendaNombre";
            this.AgendaNombre.ReadOnly = true;
            this.AgendaNombre.Width = 200;
            // 
            // FlgImpreso
            // 
            this.FlgImpreso.DataPropertyName = "FlgImpreso";
            this.FlgImpreso.HeaderText = "Impreso";
            this.FlgImpreso.Name = "FlgImpreso";
            this.FlgImpreso.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Fecha Inicial";
            // 
            // txtnombre
            // 
            this.txtnombre.Location = new System.Drawing.Point(91, 40);
            this.txtnombre.Name = "txtnombre";
            this.txtnombre.Size = new System.Drawing.Size(286, 20);
            this.txtnombre.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.chkimpreso);
            this.panel1.Controls.Add(this.btnconsultar);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtnombre);
            this.panel1.Controls.Add(this.dtpfechaf);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dtpfechai);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(547, 69);
            this.panel1.TabIndex = 3;
            // 
            // chkimpreso
            // 
            this.chkimpreso.AutoSize = true;
            this.chkimpreso.Location = new System.Drawing.Point(393, 13);
            this.chkimpreso.Name = "chkimpreso";
            this.chkimpreso.Size = new System.Drawing.Size(68, 17);
            this.chkimpreso.TabIndex = 7;
            this.chkimpreso.Text = "Impresos";
            this.chkimpreso.UseVisualStyleBackColor = true;
            // 
            // btnconsultar
            // 
            this.btnconsultar.Location = new System.Drawing.Point(480, 12);
            this.btnconsultar.Name = "btnconsultar";
            this.btnconsultar.Size = new System.Drawing.Size(54, 24);
            this.btnconsultar.TabIndex = 6;
            this.btnconsultar.Text = "Buscar";
            this.btnconsultar.UseVisualStyleBackColor = true;
            this.btnconsultar.Click += new System.EventHandler(this.btnconsultar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Nombre Cliente";
            // 
            // dtpfechaf
            // 
            this.dtpfechaf.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfechaf.Location = new System.Drawing.Point(274, 12);
            this.dtpfechaf.Name = "dtpfechaf";
            this.dtpfechaf.Size = new System.Drawing.Size(103, 20);
            this.dtpfechaf.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(206, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Fecha Final";
            // 
            // dtpfechai
            // 
            this.dtpfechai.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfechai.Location = new System.Drawing.Point(91, 12);
            this.dtpfechai.Name = "dtpfechai";
            this.dtpfechai.Size = new System.Drawing.Size(103, 20);
            this.dtpfechai.TabIndex = 2;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // timer1
            // 
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnureimprimir});
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(153, 26);
            this.menu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menu_ItemClicked);
            // 
            // mnureimprimir
            // 
            this.mnureimprimir.Image = global::GS.SISGEGS.WINAplicaciones.Properties.Resources.imprimir_32_x_32png;
            this.mnureimprimir.Name = "mnureimprimir";
            this.mnureimprimir.Size = new System.Drawing.Size(152, 22);
            this.mnureimprimir.Text = "Reimprimir QR";
            // 
            // frmimpresionqr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(808, 459);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gvwguiaventa);
            this.Name = "frmimpresionqr";
            this.Text = "Modulo de Impresion de Guias de Venta con Codigo QR";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmimpresionqr_FormClosed);
            this.Load += new System.EventHandler(this.frmimpresionqr_Load);
            this.Resize += new System.EventHandler(this.frmimpresionqr_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.gvwguiaventa)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gvwguiaventa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtnombre;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnconsultar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpfechaf;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpfechai;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.CheckBox chkimpreso;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Empresa;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpGuia;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroGuia;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn AgendaNombre;
        private System.Windows.Forms.DataGridViewCheckBoxColumn FlgImpreso;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem mnureimprimir;
    }
}

