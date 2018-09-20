using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GS.SISGEGS.WINComision.ComisionWCF;
using GS.SISGEGS.WINComision.EmpresaWCF;

namespace GS.SISGEGS.WINComision
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            EmpresaWCFClient objEmpresaWCF = new EmpresaWCFClient();
            cboEmpresa.DataSource = objEmpresaWCF.Empresa_Listar(0, "");
            cboEmpresa.ValueMember = "idEmpresa";
            cboEmpresa.DisplayMember = "razonSocial";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ComisionWCFClient objComisionWCF = new ComisionWCFClient();
            var idEmpresa = int.Parse(cboEmpresa.SelectedValue.ToString());
            int idUsuario = 100;

            var list = objComisionWCF.gsReporteCanceladosVentasLista(idEmpresa, idUsuario);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
