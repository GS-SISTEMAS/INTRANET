using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using GS.SISGEGS.Web.Helpers;
using System.Web.UI.WebControls;
using GS.SISGEGS.Web.ContratosWCF;
using GS.SISGEGS.Web.MarcasWCF;
using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.Web.Contratos.Reportes
{
    public partial class frmMarcaLogo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            if (!ConnectionHelpers.CheckForInternetConnection())
                throw new ArgumentException("ERROR: Revisar su conexión a internet.");

            try
            {
                if (!Page.IsPostBack)
                {

                    var idMarca = (Request.QueryString["idMarca"]);
                    var imagen = (Request.QueryString["imagen"]);
                    Page.Title = "Logo Marca";
                    imgLogo.ImageUrl = "Documentos/" + idMarca + "/" + imagen;

                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                }
            }
            catch (Exception ex)
            {
                
            }
         }

}
}