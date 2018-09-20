using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GS.SISGEGS.WINAplicaciones.GuiaWCF;
using System.Net;
using System.Drawing.Printing;
using System.IO;
using System.Drawing.Imaging;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;

namespace GS.SISGEGS.WINAplicaciones
{
    public partial class frmimpresionqr : Form
    {
        const int WM_SYSCOMMAND = 0x112;
        const int SC_MINIMIZE = 0xF020;
        const int SC_CLOSE = 0xF060;
        GuiaWCFClient objGuiaWCF;
        List<USP_SEL_Guia_VentaImpresaQRResult> _lstguiasimpresas = new List<USP_SEL_Guia_VentaImpresaQRResult>();
        List<USP_SEL_IMPRESORASQRResult> _lstimpresoras = new List<USP_SEL_IMPRESORASQRResult>();
        List<USP_SEL_GUIAS_PENDIENTESIMPRESIONResult> _lstpendientes = new List<USP_SEL_GUIAS_PENDIENTESIMPRESIONResult>();
        PrintDocument objprintdocument;
        string _strqr = string.Empty;
        string _nombreempresa = string.Empty;
        string _nroguia = string.Empty;
        public frmimpresionqr()
        {
            InitializeComponent();
            this.Closing += new CancelEventHandler(frmimpresionqr_Closing);
        }

        #region Procedimientos

        private void CargarGuias()
        {
            objGuiaWCF = new GuiaWCFClient();
            _lstguiasimpresas = objGuiaWCF.GuiaVentaQR_SeleccionarDocumentos(1, 1, dtpfechai.Value, dtpfechaf.Value, txtnombre.Text.Trim(), chkimpreso.Checked).ToList();
            gvwguiaventa.DataSource = _lstguiasimpresas;


        }

        private void ImprimirCodigosQR()
        {
            objGuiaWCF = new GuiaWCFClient();


            _lstpendientes = objGuiaWCF.GuiaVentaQR_SeleccionarGuiasPendientesImrpesion(1, 1).ToList();
            PrinterSettings propiedades = new PrinterSettings();

            foreach (USP_SEL_GUIAS_PENDIENTESIMPRESIONResult p in _lstpendientes)
            {
                _strqr = p.Empresa + "|" + p.RucEmpresa + "|" + p.NombreEmpresa + "|" + p.OPOV.ToString() + "|" + p.AgendaNombre + "|" + p.NroGuia + "|" + p.NombreAgencia.Trim();
                _nombreempresa = p.NombreEmpresa.Trim();
                _nroguia = p.NroGuia;
                objprintdocument = new PrintDocument();
                propiedades.DefaultPageSettings.Landscape = false;
                propiedades.DefaultPageSettings.Margins.Left = 2;
                propiedades.DefaultPageSettings.Margins.Top = 2;
                //propiedades.PrinterName = "ETIQUETERA";
                //propiedades.PrinterName = "Microsoft Print to PDF";
                propiedades.PrinterName = _lstimpresoras.Where(x => x.NombrePC.ToUpper().Trim() == Dns.GetHostName().ToUpper()).Select(x => x.RutaImpresora).First().ToString(); //@"Microsoft Print to PDF";

                propiedades.DefaultPageSettings.PaperSize = new PaperSize("210 x 297 mm", 800, 800);
                propiedades.DefaultPageSettings.PaperSize.RawKind = new PaperSize("210 x 297 mm", 800, 800).RawKind;
                //propiedades.PrinterName= "Microsoft Print to PDF";
                objprintdocument.PrinterSettings = propiedades;



                objprintdocument.PrintPage += new PrintPageEventHandler(Datos_Documento);



                objprintdocument.Print();
                objGuiaWCF.GuiaVentaQR_ActualizarFlagImpresion(1, 1, p.Empresa, Convert.ToInt32(p.OpGuia), true);
            }


        }

        private void Datos_Documento(object obj, PrintPageEventArgs ev)
        {
            float pos_x = 20;
            float pos_y = 15;



            Font fuente = new Font("Arial", 8);


            ev.Graphics.DrawImage(GenerarCodigoQr(_strqr), pos_x, pos_y, 80, 80);
            ev.Graphics.DrawString(_nombreempresa, fuente, Brushes.Black, pos_x + 100, pos_y, new
            StringFormat());
            ev.Graphics.DrawString("Nro Guia:", fuente, Brushes.Black, pos_x + 100, pos_y + 10, new
            StringFormat());
            ev.Graphics.DrawString(_nroguia, fuente, Brushes.Black, pos_x + 100, pos_y + 20, new
            StringFormat());


            ev.Graphics.DrawImage(GenerarCodigoQr(_strqr), pos_x + 210, pos_y, 80, 80); //220
            ev.Graphics.DrawString(_nombreempresa, fuente, Brushes.Black, pos_x + 320, pos_y, new
            StringFormat());
            ev.Graphics.DrawString("Nro Guia:", fuente, Brushes.Black, pos_x + 320, pos_y + 10, new
            StringFormat());
            ev.Graphics.DrawString(_nroguia, fuente, Brushes.Black, pos_x + 320, pos_y + 20, new
            StringFormat());


            _strqr = string.Empty;
            _nombreempresa = string.Empty;
            _nroguia = string.Empty;
        }

        private Image GenerarCodigoQr(string strqr)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            qrEncoder.TryEncode(strqr, out qrCode);

            GraphicsRenderer renderer = new GraphicsRenderer(new FixedCodeSize(350, QuietZoneModules.Zero), Brushes.Black, Brushes.White);

            MemoryStream ms = new MemoryStream();

            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
            var imageTemporal = new Bitmap(ms);
            var imagen = new Bitmap(imageTemporal, new Size(new Point(350, 350)));


            MemoryStream msimagen = new MemoryStream();
            imagen.Save(msimagen, ImageFormat.Png);
            return imagen;
        }
        private void ObtenerImpresoras()
        {
            objGuiaWCF = new GuiaWCFClient();
            _lstimpresoras = objGuiaWCF.GuiaVentaQR_SeleccionarImpresoras(1, 1).ToList();
        }
        #endregion

        void frmimpresionqr_Closing(object sender, CancelEventArgs e)
        {
            //this.Hide();
            this.Visible = false;
            this.WindowState = FormWindowState.Minimized;
            notifyIcon1.Visible = true;
            e.Cancel = true;
        }
        private void frmimpresionqr_Resize(object sender, EventArgs e)
        {


            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = false;
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            this.Show();
            this.WindowState = FormWindowState.Normal;

            notifyIcon1.Visible = false;
        }

        private void frmimpresionqr_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnconsultar_Click(object sender, EventArgs e)
        {
            try
            {
                CargarGuias();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Impresion de Guia de Remisión", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //MessageBox.Show("hola");
            try
            {
                ImprimirCodigosQR();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Impresion de Guia de Remisión", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void frmimpresionqr_Load(object sender, EventArgs e)
        {
            ObtenerImpresoras();
            timer1.Start();
            //ImprimirCodigosQR();

        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.AppStarting;

                if (gvwguiaventa.RowCount <= 0) return;

                gvwguiaventa.ContextMenuStrip.Visible = false;
                DataGridViewRow row = new DataGridViewRow();
                row = gvwguiaventa.SelectedRows[0];

                switch (e.ClickedItem.Name)
                {
                    case "mnureimprimir":
                        if (MessageBox.Show("Desea reimprimir la guia seleccionada?", "Modulo de Impresion de Codigos QR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            objGuiaWCF = new GuiaWCFClient();

                            objGuiaWCF.GuiaVentaQR_ActualizarFlagImpresion(1, 1, row.Cells["Empresa"].Value.ToString().Substring(0, 1), row.Cells["OpGuia"].Value.ToString() == string.Empty ? 0 : Convert.ToInt32(row.Cells["OpGuia"].Value.ToString()), false);
                        }
                        break;

                }
                //Cargar_devoluciones(string.Empty);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString(), "Modulo de Impresion de Codigos QR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        private void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.AppStarting;

                if (gvwguiaventa.RowCount <= 0) return;

                gvwguiaventa.ContextMenuStrip.Visible = false;
                DataGridViewRow row = new DataGridViewRow();
                row = gvwguiaventa.SelectedRows[0];

                switch (e.ClickedItem.Name)
                {
                    case "mnureimprimir":
                        if (MessageBox.Show("Desea reimprimir la guia seleccionada?", "Modulo de Impresion de Codigos QR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            objGuiaWCF = new GuiaWCFClient();

                            objGuiaWCF.GuiaVentaQR_ActualizarFlagImpresion(1, 1, row.Cells["Empresa"].Value.ToString().Substring(0, 1), row.Cells["OpGuia"].Value.ToString() == string.Empty ? 0 : Convert.ToInt32(row.Cells["OpGuia"].Value.ToString()), false);
                        }
                        break;

                }
                //Cargar_devoluciones(string.Empty);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString(), "Modulo de Impresion de Codigos QR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }
    }
}
