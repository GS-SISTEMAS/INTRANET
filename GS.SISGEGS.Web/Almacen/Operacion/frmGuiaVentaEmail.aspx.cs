using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.GuiaWCF;
using GS.SISGEGS.Web.PerfilWCF;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.Web.Almacen.Operacion
{
    public partial class frmGuiaVentaEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GuiaWCFClient objUsuariosWCF = new GuiaWCFClient();
            try
            {
                int id_trama = int.Parse((Request.QueryString["id"]));
                int empresa = int.Parse((Request.QueryString["empresa"]));                
                List<USP_SEL_IdTrazabilidadDespachoResult> listTrazabilidad = objUsuariosWCF.IdTrazabilidadDespacho_Listar().ToList();
                

                DropDownList1.DataSource = listTrazabilidad;
                DropDownList1.DataTextField = "id";                          
                DropDownList1.DataValueField = "id";
                DropDownList1.DataBind();
                DropDownList1.SelectedIndex = id_trama;
                decimal id = Convert.ToDecimal(id_trama);
                List<USP_SEL_TrazabilidadDespachoXIDResult> listTrazabilidadId = objUsuariosWCF.IdTrazabilidadDespacho_Listar_ID(id, empresa).ToList();

                lblPedido.Text = listTrazabilidadId[0].NroPedido.ToString();
                lblCliente.Text = listTrazabilidadId[0].AgendaNombre.ToString();
                lblGuia.Text = listTrazabilidadId[0].Guia.ToString();
                lblFecha.Text = Convert.ToDateTime(listTrazabilidadId[0].FechaGuiaCaj.ToString()).ToString("dd/MM/yyyy");
                lblHora.Text = listTrazabilidadId[0].HoraGuiaCaj.ToString();
                Image1.ImageUrl = String.Format("data:image/jpeg;base64,{0}", listTrazabilidadId[0].Foto.ToString());
                lblEMpresa.Text = listTrazabilidadId[0].EmpresaName.ToString();
                DropDownList1.Visible = false;
                

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}