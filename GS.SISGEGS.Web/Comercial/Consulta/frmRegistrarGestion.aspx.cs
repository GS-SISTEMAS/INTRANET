using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.FinanzasWCF;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.PlanificacionWCF;
using GS.SISGEGS.Web.ItemWCF;
using Telerik.Web.UI;



namespace GS.SISGEGS.Web.Comercial.Consulta
{
    public partial class frmRegistrarGestion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (!Page.IsPostBack)
                {
                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    if (!string.IsNullOrEmpty(Request.QueryString["Kardex"]))
                    {
                        Title = "Gestión Stock";
 
                        int Kardex = JsonHelper.JsonDeserialize<int>(Request.QueryString["Kardex"]);
                        int ID_Almacen = JsonHelper.JsonDeserialize<int>(Request.QueryString["ID_Almacen"]);
 
                        Item_GestionStock(0, ID_Almacen, Kardex, 0, ""); 

                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            ItemWCFClient objItemWCF = new ItemWCFClient();


            string mensaje = "";
            lblMensaje.Text = ""; 
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                int ID = 0; 
                int Operacion = 2;
                int Kardex = JsonHelper.JsonDeserialize<int>(Request.QueryString["Kardex"]);
                int ID_Almacen = JsonHelper.JsonDeserialize<int>(Request.QueryString["ID_Almacen"]);
                float Cantidad = 0;
                string Observacion = txtObservacion.Text ; 
                if(txtImporte.Text.Length>0)
                {
                    Cantidad = float.Parse(txtImporte.Text);
                    objItemWCF.Item_Mantenimiento_GestionStock(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                   ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, ID, ID_Almacen, Kardex, Cantidad, Observacion, Operacion);
                    lblMensaje.Text = "Se registro con exitó.";
                    lblMensaje.CssClass = "mensajeExito";

                    Item_Limpiar(); 
                    Item_GestionStock(0, ID_Almacen, Kardex, 0, "");
                }
                else
                {
                    lblMensaje.Text = "ERROR: " + "Por favor, ingresar cantidad.";
                    lblMensaje.CssClass = "mensajeError";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {

            lblMensaje.Text = "";
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind(" + 100 + ");", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void Item_GestionStock(int ID, int Id_AgendaAnexo, int Id_Item, float Cantidad, string Observacion)
        {
            ItemWCFClient objItemWCF = new ItemWCFClient();
            try
            {
 
                int Operacion=1; 
                
                List<sp_GestionStock_ListarResult> Lista = new List<sp_GestionStock_ListarResult>();

                Lista = objItemWCF.Item_Listar_GestionStock(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, ID, Id_AgendaAnexo, Id_Item, Cantidad, Observacion, Operacion ).ToList();

                Lista = Lista.OrderByDescending(x => x.id).ToList(); 

                grdGestionStock.DataSource = Lista;
                grdGestionStock.DataBind();
 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Item_Limpiar ()
        {
             
            try
            {
                txtImporte.Text = "";
                txtObservacion.Text = ""; 

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grdGestionStock_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            if (!ConnectionHelpers.CheckForInternetConnection())
                throw new ArgumentException("ERROR: Revisar su conexión a internet.");

            lblMensaje.Text = "";

            try
            {
                if (e.CommandName == "Eliminar")
                {

 
                   int ID = 0;
                   ID = int.Parse(e.CommandArgument.ToString());
 

                    ItemWCFClient objItemWCF = new ItemWCFClient();
 
                    int Operacion = 3;
                    int Kardex = JsonHelper.JsonDeserialize<int>(Request.QueryString["Kardex"]);
                    int ID_Almacen = JsonHelper.JsonDeserialize<int>(Request.QueryString["ID_Almacen"]);
                    float Cantidad = 0;
                    string Observacion = txtObservacion.Text;

                    objItemWCF.Item_Mantenimiento_GestionStock(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                                          ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario, ID, ID_Almacen, Kardex, Cantidad, Observacion, Operacion);


                    Item_Limpiar();
                    Item_GestionStock(0, ID_Almacen, Kardex, 0, "");

                    lblMensaje.Text = "Se eliminó el registro.";
                    lblMensaje.CssClass = "mensajeExito";
         


                }
            }
            catch (Exception ex)
            {

                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";

            }
        }
    }
}