using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Telerik.Web.UI;
using System.Web.Services;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.LetrasEmitidasWCF;
using GS.SISGEGS.Web.PerfilWCF;
using GS.SISGEGS.DM;
using System.Data.Sql;
using System.Data.OleDb;
using System.Data;
using System.Drawing;

namespace GS.SISGEGS.Web.Contabilidad.Informacion
{
    public partial class frmCanjeAtomaticoLetras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            try
            {
                if (!Page.IsPostBack)
                {
                    txtLetras.Focus();
                    dpInicio.SelectedDate = DateTime.Now;
                    dpFinal.SelectedDate = DateTime.Now;
                    dpInicio.SelectedDate = DateTime.Now.AddDays(-DateTime.Now.Day + 1);
                    dpFinal.SelectedDate = DateTime.Now;
                    Estado_Cargar();

                    Letra_Cargar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, dpInicio.SelectedDate.Value, dpFinal.SelectedDate.Value, cboEstado.Text);

                }
            }

            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void Estado_Cargar()
        {
            LetrasEmitidasWCFClient objEstadoWCF = new LetrasEmitidasWCFClient();
            USP_SEL_Estado_LetrasResult objTipoDoc = new USP_SEL_Estado_LetrasResult();
            List<USP_SEL_Estado_LetrasResult> lst = objEstadoWCF.Estado_Letras_Listar(((Usuario_LoginResult)Session["Usuario"]).nombreComercial).ToList();

            lst.Insert(0, objTipoDoc);
            objTipoDoc.Estado = "Todos";
            objTipoDoc.ID_Estado = 0;
            cboEstado.DataSource = lst;
            cboEstado.DataTextField = "Estado";
            cboEstado.DataValueField = "ID_Estado";
            cboEstado.DataBind();
        }

        private void Letra_Cargar(int id_empresa, DateTime Fecha_Inicio, DateTime Fecha_Final, string Estado)
        {
            LetrasEmitidasWCFClient objUsuariosWCF = new LetrasEmitidasWCFClient();
            try
            {
                txtLetras.Text = "";
                txtLetras.Focus();

                List<USP_SEL_Canje_Automatico_LetrasResult> listUsuario = objUsuariosWCF.CanjeAutomaticoLetras_Listar(id_empresa, Fecha_Inicio, Fecha_Final, cboEstado.Text).ToList();

                //if (txtLetras.Text!="")
                //if (listUsuario.Where(be => be.CodigoLetra.ToString() == txtLetras.Text).Count() == 0)
                //{
                //    lblMensaje.Text = "ERROR: " + "El número de letra ingresada no existe";
                //    lblMensaje.CssClass = "mensajeError";
                //}

                grdLetras.DataSource = listUsuario;
                grdLetras.DataBind();
                Session["LstLetras"] = JsonHelper.JsonSerializer(listUsuario);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grdLetras_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    var IsActivo = false;
                    var estado = item["Estado"].Text;

                    if (estado == "StandBy")
                        IsActivo = true;

                    if (IsActivo)
                    {
                        ((System.Web.UI.WebControls.Image)e.Item.FindControl("ibDesactivar")).ImageUrl = "~/Images/Icons/circle-red-16.png";

                    }
                    else
                    {
                        ((System.Web.UI.WebControls.Image)e.Item.FindControl("ibDesactivar")).ImageUrl = "~/Images/Icons/circle-green-16.png";

                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                Letra_Cargar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, dpInicio.SelectedDate.Value, dpFinal.SelectedDate.Value, cboEstado.Text);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try

            {
                LetrasEmitidasWCFClient objItemWCF = new LetrasEmitidasWCFClient();
                var result = objItemWCF.CanjeAutomaticoLetras_Registrar(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).nombreComercial, int.Parse(txtLetras.Text), ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).nombres);
                if (result == "")
                {
                    Letra_Cargar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, dpInicio.SelectedDate.Value, dpFinal.SelectedDate.Value, cboEstado.Text);
                    txtLetras.Text = "";
                    txtLetras.Focus();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(btnRegistrar, btnRegistrar.GetType(), "mykey2", "setTimeout(function(){mostrarMensaje('ERROR: " + result + "');},3000);", true);
                    lblMensaje.Text = "ERROR: " + result;
                    lblMensaje.CssClass = "mensajeError";
                }


            }
            catch (Exception ex)
            {
                //rwmReporte.RadAlert(ex.Message, 300, 120, "Mensaje", "");
                lblMensaje.Text = "ERROR: " + ex.Message;
                //lblMensaje.CssClass = "mensajeError";
            }
        }

        [WebMethod]
        public static string Registrar(string letra)
        {
            if (((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]) == null)
            {
                return "";
            }
            try

            {
                LetrasEmitidasWCFClient objItemWCF = new LetrasEmitidasWCFClient();
                var result = objItemWCF.CanjeAutomaticoLetras_Registrar(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).nombreComercial, int.Parse(letra), ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).nombres);
                return result;
                //if (result == "")
                //{
                //    //Letra_Cargar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, dpInicio.SelectedDate.Value, dpFinal.SelectedDate.Value, cboEstado.Text);
                //    //txtLetras.Text = "";
                //    //txtLetras.Focus();
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(btnRegistrar, btnRegistrar.GetType(), "mykey2", "setTimeout(function(){mostrarMensaje('ERROR: " + result + "');},3000);", true);
                //    lblMensaje.Text = "ERROR: " + result;
                //    lblMensaje.CssClass = "mensajeError";
                //}


            }
            catch (Exception ex)
            {
                //rwmReporte.RadAlert(ex.Message, 300, 120, "Mensaje", "");
                return "ERROR: " + ex.Message;
                //lblMensaje.CssClass = "mensajeError";
            }
        }


        protected void btnFinanciamiento_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                LetrasEmitidasWCFClient objItemWCF = new LetrasEmitidasWCFClient();
                objItemWCF.Financiamiento_CA_Letras_General(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).nombreComercial);
                Letra_Cargar(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, dpInicio.SelectedDate.Value, dpFinal.SelectedDate.Value, cboEstado.Text);
                txtLetras.Text = "";
                txtLetras.Focus();
            }
            catch (Exception ex)
            {
                rwmReporte.RadAlert(ex.Message, 300, 120, "Mensaje", "");
                //lblMensaje.Text = "ERROR: " + ex.Message;
                //lblMensaje.CssClass = "mensajeError";
            }
        }


        protected void btnExcel_Click(object sender, EventArgs e)
        {

            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (grdLetras.Items.Count == 0)
                {
                    throw new System.ArgumentException("No se puede descargar el reporte porque no hay registros en el listado");
                }


                List<USP_SEL_Canje_Automatico_LetrasResult> lista = new List<USP_SEL_Canje_Automatico_LetrasResult>();
                lista = JsonHelper.JsonDeserialize<List<USP_SEL_Canje_Automatico_LetrasResult>>((string)Session["LstLetras"]);
                if (cboEstado.Text == "Todos")
                {
                    grdLetras.DataSource = lista.ToList();
                }
                else
                {
                    grdLetras.DataSource = lista.Where(x => x.Estado == cboEstado.Text).ToList();
                }

                grdLetras.DataBind();

                grdLetras.ExportSettings.FileName = "ReporteLetras_" + DateTime.Now.ToString("yyyyMMddHmm");
                grdLetras.ExportSettings.ExportOnlyData = true;
                grdLetras.ExportSettings.IgnorePaging = true;
                grdLetras.ExportSettings.OpenInNewWindow = true;
                grdLetras.MasterTableView.ExportToExcel();
                Page.Response.ClearHeaders();
                Page.Response.ClearContent();
            }
            catch (ArgumentException ex1)
            {
                rwmReporte.RadAlert(ex1.Message, 300, 120, "Mensaje", "");
            }
            catch (Exception ex)
            {
                this.lblMensaje.Text = ex.Message + "- btnExcel_Click ";
                lblMensaje.CssClass = "mensajeError";
                rwmReporte.RadAlert(ex.Message, 500, 100, "Error al descargar el excel", "");
            }

        }




    }
}