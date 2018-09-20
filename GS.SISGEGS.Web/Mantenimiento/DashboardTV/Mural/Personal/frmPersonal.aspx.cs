using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.ReportesRRHHWCF;
using GS.SISGEGS.DM;
using System.IO;
using Telerik.Web.UI;

namespace GS.SISGEGS.Web.Mantenimiento.DashboardTV.Mural.Personal
{
    public partial class frmPersonal : System.Web.UI.Page
    {
        private void Personal_Cargar() {
            ReportesRRHHClient objRRHHWCF = new ReportesRRHHClient();
            try
            {
                int mes = int.Parse(cboMes.SelectedValue.ToString()); 
                List<Personal_ListarResult> lista = new List<Personal_ListarResult>();
                lista = objRRHHWCF.Personal_Listar(cboEmpresa.SelectedValue, txtBuscar.Text).ToList();



                if(mes>0)
                {
                    lista = lista.FindAll(x => x.Fecha.Value.Month == mes).ToList();
                }

                grdPersonal.DataSource = lista; 
                grdPersonal.DataBind();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        private void Empresa_ComboBox() {
            EmpresaWCFClient objEmpresaWCF = new EmpresaWCFClient();
            try {
                List<Empresa_ComboBoxResult> lstEmpresa = objEmpresaWCF.Empresa_ComboBox().ToList();
                cboEmpresa.DataSource = lstEmpresa;
                cboEmpresa.DataBind();

                cboEmpresa.SelectedValue = lstEmpresa.Find(x => x.idEmpresa == ((Usuario_LoginResult)Session["Usuario"]).idEmpresa).codigoRHPlus;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try {
                if (!Page.IsPostBack) {

                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    cboMes.SelectedValue = "0"; 
                    lblNroDocumento.Text = "NRO.DOC.";
                    lblNombres.Text = "NOMBRES DEL PERSONAL";
                    lblEmmprePersonal.Text = "EMPRESA";
                    Empresa_ComboBox();
                    Personal_Cargar();


                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try {
                Personal_Cargar();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void grdPersonal_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try {
                if (e.CommandName == "Editar") {
                    ViewState["nroDocumento"] = e.CommandArgument;
                    btnGuardar.Enabled = true;
                    btnCancelar.Enabled = true;
                    rauImagen.Enabled = true;
                    grdPersonal.Enabled = false;

                    GridDataItem item = (GridDataItem)e.Item;
                    lblNroDocumento.Text = "NRO.DOC. " + item["NroDocumento"].Text;
                    lblNombres.Text = item["ApPaterno"].Text + " " + item["ApMaterno"].Text + " " + item["Nombres"].Text;
                    lblEmmprePersonal.Text = item["Empresa"].Text;
                    imageURL.Text = item["ImagenURL"].Text;
                    imgPersonal.ImageUrl = "~/Images/Personal/" + item["ImagenURL"].Text + "?" + new Random().Next();
                }
            }
            catch (Exception ex) {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void rauImagen_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                String path = Server.MapPath("~/Images/Personal");
                if (e.IsValid)
                {
                    ViewState["fileName"] = (string)ViewState["nroDocumento"] + e.File.GetExtension();
                    string imgPath = Path.Combine(path, (string)ViewState["fileName"]);
                    e.File.InputStream.Dispose();
                    e.File.InputStream.Close();
                    e.File.SaveAs(imgPath, true);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            ReportesRRHHClient objRRHHWCF = new ReportesRRHHClient();
            int idPersonal = 0;
            try {
                if (imageURL.Text.ToUpper() != "SIN FOTO")
                    idPersonal = 1;
                objRRHHWCF.Personal_Registrar(idPersonal, (string)ViewState["nroDocumento"], 
                    (string)ViewState["fileName"], ((Usuario_LoginResult)Session["Usuario"]).idUsuario);
                imgPersonal.ImageUrl = "~/Images/Personal/" + (string)ViewState["fileName"] + "?" + new Random().Next();
                
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
                rauImagen.Enabled = false;
                grdPersonal.Enabled = true;

                grdPersonal.DataSource = objRRHHWCF.Personal_Listar(cboEmpresa.SelectedValue, txtBuscar.Text);
                grdPersonal.DataBind();
            }
            catch (Exception ex) {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                imgPersonal.Dispose();
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
                rauImagen.Enabled = false;
                grdPersonal.Enabled = true;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}