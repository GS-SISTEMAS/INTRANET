using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.NoticiasWCF;
using Telerik.Web.UI;
using System.IO;

namespace GS.SISGEGS.Web.Mantenimiento.DashboardTV.Mural.Noticia
{
    public partial class frmNoticiaMng : System.Web.UI.Page
    {
        private void Noticia_Cargar(int idNoticia)
        {
            
            NoticiasWCFClient objNoticiaWCF = new NoticiasWCFClient();
            NoticiaFoto_ListarResult[] lstFotos = null;
            Noticia_BuscarResult objNoticia;
            try
            {
                objNoticia = objNoticiaWCF.Noticia_Buscar(idNoticia, ref lstFotos);
                txtTexto.Text = objNoticia.texto;
                txtTitulo.Text = objNoticia.titulo;
                dpFechaPublicacion.SelectedDate = objNoticia.fechaPublicacion;
                dpFechaVencimiento.SelectedDate = objNoticia.fechaVencimiento;
                cboEmpresa.SelectedValue = objNoticia.idEmpresa.ToString();
                ckbActivo.Checked = objNoticia.activo;

                grdImagenes.Visible = true;
                grdImagenes.DataSource = lstFotos;
                grdImagenes.DataBind();

                ViewState["lstFotos"] = JsonHelper.JsonSerializer(lstFotos);
                rauArchivo.MaxFileInputsCount = 5 - lstFotos.Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<NoticiaFoto_ListarResult> NoticiaFoto_Obtener()
        {
            List<NoticiaFoto_ListarResult> lstNoticiaFoto = new List<NoticiaFoto_ListarResult>();
            try
            {
                foreach (UploadedFile file in rauArchivo.UploadedFiles)
                {
                    NoticiaFoto_ListarResult picture = new NoticiaFoto_ListarResult();
                    picture.urlImagen = file.FileName;
                    picture.idNoticiaFoto = 0;
                    picture.descripcion = "";
                    picture.activo = true;
                    System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream);
                    if (img.Width < img.Height)
                        picture.horizontal = false;
                    else
                        picture.horizontal = true;
                    lstNoticiaFoto.Add(picture);
                }
                return lstNoticiaFoto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Empresa_Cargar()
        {
            EmpresaWCFClient objEmpresaWCF = new EmpresaWCFClient();
            try
            {
                cboEmpresa.DataSource = objEmpresaWCF.Empresa_ComboBox();
                cboEmpresa.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            List<NoticiaFoto_ListarResult> lstFotos = new List<NoticiaFoto_ListarResult>();
            try
            {
                if (!Page.IsPostBack)
                {
                    if (ViewState["lstFotos"] == null)
                        ViewState["lstFotos"] = JsonHelper.JsonSerializer(lstFotos);

                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    dpFechaPublicacion.SelectedDate = DateTime.Now;
                    dpFechaVencimiento.SelectedDate = DateTime.Now.AddDays(1);
                    Empresa_Cargar();
                    cboEmpresa.SelectedValue = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();

                    if (Request.QueryString["idNoticia"] == "")
                    {
                        Title = "Registrar Noticias";
                        cboEmpresa.SelectedValue = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa.ToString();

                        lblMensaje.Text = "Listo para registrar Noticias";
                        lblMensaje.CssClass = "mensajeExito";
                    }
                    else
                    {
                        Title = "Modificar Noticias";
                        Noticia_Cargar(int.Parse(Request.QueryString["idNoticia"]));

                        lblMensaje.Text = "Listo para modificar Noticias";
                        lblMensaje.CssClass = "mensajeExito";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            NoticiasWCFClient objNoticiasWCF = new NoticiasWCFClient();
            int idNoticia = 0;
            List<NoticiaFoto_ListarResult> lstFotos = JsonHelper.JsonDeserialize<List<NoticiaFoto_ListarResult>>((string)ViewState["lstFotos"]);
            try
            {
                if (string.IsNullOrEmpty(txtTexto.Text))
                    throw new ArgumentException("Se debe ingresar un texto valido.");

                if (string.IsNullOrEmpty(txtTitulo.Text))
                    throw new ArgumentException("Se debe ingresar un título para la noticia");

                if (DateTime.Compare(dpFechaPublicacion.SelectedDate.Value, dpFechaVencimiento.SelectedDate.Value) > 0)
                    throw new ArgumentException("La fecha de publicación no puede ser despues de la fecha de vencimiento.");

                if (Request.QueryString["idNoticia"] != "")
                {
                    idNoticia = int.Parse(Request.QueryString["idNoticia"]);
                    foreach (GridDataItem row in grdImagenes.Items)
                    {
                        NoticiaFoto_ListarResult foto = lstFotos.Find(x => x.idNoticiaFoto.ToString() == row["idNoticiaFoto"].Text);
                        foto.activo = ((RadButton)row.FindControl("btnSeleccionar")).Checked;
                        lstFotos.RemoveAll(x => x.idNoticiaFoto.ToString() == row["idNoticiaFoto"].Text);
                        lstFotos.Add(foto);
                    }
                }

                if (lstFotos.FindAll(x => !x.elimino).Count <= 0)
                    throw new ArgumentException("Se debe guardar por lo menos un archivo valido.");

                objNoticiasWCF.Noticia_Registrar(idNoticia, txtTitulo.Text, txtTexto.Text, dpFechaPublicacion.SelectedDate.Value, dpFechaVencimiento.SelectedDate.Value, int.Parse(cboEmpresa.SelectedValue), ((Usuario_LoginResult)Session["Usuario"]).idUsuario,
                    ckbActivo.Checked, lstFotos.ToArray());

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind(" + cboEmpresa.SelectedValue + ");", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void rauArchivo_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                String path = Server.MapPath("~/Images/Noticia");
                if (e.IsValid)
                {
                    string imgPath = Path.Combine(path, e.File.FileName);
                    e.File.InputStream.Dispose();
                    e.File.InputStream.Close();
                    e.File.SaveAs(imgPath, true);

                    //NoticiaFoto_ListarResult imagen = new NoticiaFoto_ListarResult();
                    NoticiaFoto_ListarResult picture = new NoticiaFoto_ListarResult();
                    picture.urlImagen = e.File.FileName;
                    picture.idNoticiaFoto = 0;
                    picture.descripcion = "";
                    picture.activo = true;
                    picture.elimino = false;
                    //System.Drawing.Image img = System.Drawing.Image.FromStream(e.File.InputStream);
                    System.Drawing.Image img = System.Drawing.Image.FromFile(imgPath);
                    if (img.Width < img.Height)
                        picture.horizontal = false;
                    else
                        picture.horizontal = true;
                    picture.altura = img.Height;
                    picture.anchura = img.Width;
                    img.Dispose();

                    List<NoticiaFoto_ListarResult> lstFotos = JsonHelper.JsonDeserialize<List<NoticiaFoto_ListarResult>>((string)ViewState["lstFotos"]);
                    lstFotos.Add(picture);
                    ViewState["lstFotos"] = JsonHelper.JsonSerializer(lstFotos);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdImagenes_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            List<NoticiaFoto_ListarResult> lstImagenes;
            try
            {
                lstImagenes = JsonHelper.JsonDeserialize<List<NoticiaFoto_ListarResult>>((string)ViewState["lstFotos"]);
                lstImagenes.Find(x => x.idNoticiaFoto == Convert.ToInt32(((GridDataItem)e.Item).GetDataKeyValue("idNoticiaFoto"))).elimino = true;
                ViewState["lstFotos"] = JsonHelper.JsonSerializer(lstImagenes);
                grdImagenes.DataSource = lstImagenes.FindAll(x => !x.elimino);
                grdImagenes.DataBind();
                rauArchivo.MaxFileInputsCount = 5 - lstImagenes.FindAll(x => !x.elimino).Count;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
    }
}