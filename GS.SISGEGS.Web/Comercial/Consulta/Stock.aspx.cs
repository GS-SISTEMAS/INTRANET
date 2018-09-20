using GS.SISGEGS.DM;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.ItemWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace GS.SISGEGS.Web.Comercial.Consulta
{
    public partial class Stock : System.Web.UI.Page
    {
        private void Almacen_Cargar()
        {
            try
            {
                AgendaWCFClient objAgendaWCF = new AgendaWCFClient();
                VBG00746Result objAlmacen = new VBG00746Result();
                List<VBG00746Result> lstAlmacen;

                lstAlmacen = objAgendaWCF.AgendaAnexo_ListarAlmacen(((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idEmpresa, 
                    ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).codigoUsuario).ToList();
                lstAlmacen.Insert(0, objAlmacen);
                objAlmacen.AlmacenAnexo = "Todos";
                objAlmacen.ID_Almacen = "";

                cboAlmacen.DataSource = lstAlmacen;
                cboAlmacen.DataTextField = "AlmacenAnexo";
                cboAlmacen.DataValueField = "ID_AlmacenAnexo";
                cboAlmacen.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Item_ListarStock(string nombre, decimal? ID_AnexoAlmacen) {
            ItemWCFClient objItemWCF = new ItemWCFClient();
            try {
                if (ID_AnexoAlmacen == 0)
                    ID_AnexoAlmacen = null;
                if (string.IsNullOrEmpty(nombre))
                    nombre = null;
                grdStock.DataSource = objItemWCF.Item_ListarStock(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, nombre, ID_AnexoAlmacen);
                grdStock.DataBind();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (!Page.IsPostBack) {
                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);
                    Almacen_Cargar();
                }
            }
            catch (Exception ex) {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                Item_ListarStock(txtDescripcion.Text, decimal.Parse(cboAlmacen.SelectedValue));
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}