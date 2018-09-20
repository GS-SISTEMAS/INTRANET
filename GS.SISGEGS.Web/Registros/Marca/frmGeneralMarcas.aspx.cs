using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.DM;
using GS.SISGEGS.Web.MarcasWCF;
using GS.SISGEGS.Web.EmpresaWCF;
using Telerik.Web.UI;
using GS.SISGEGS.Web.Helpers;

namespace GS.SISGEGS.Web.Contratos.Reportes
{
    public partial class frmGeneralMarcas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (!Page.IsPostBack)
                {
                    Empresa_Cargar();
                    //Marca_Cargar();
                    Tipo_Cargar();
                    Titular_Cargar();
                    Pais_Cargar();

                    dpFechaDesde.SelectedDate = DateTime.Now.Date;
                    dpFechaHasta.SelectedDate = DateTime.Now.AddYears(10).Date;

                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);
                }

            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }

        }

        private void Empresa_Cargar()
        {
            try
            {
                EmpresaWCFClient objEmpresaWCF = new EmpresaWCFClient();
                Empresa_ComboBoxResult objEmpresa = new Empresa_ComboBoxResult();
                List<Empresa_ComboBoxResult> lstEmpresa;

                lstEmpresa = objEmpresaWCF.Empresa_ComboBox().ToList();

                lstEmpresa.Insert(0, objEmpresa);
                objEmpresa.idEmpresa = 0;
                objEmpresa.nombreComercial = "Todos";


                var lstEmpresas = from x in lstEmpresa
                                  select new
                                  {
                                      x.idEmpresa,
                                      DisplayID = String.Format("{0}", x.idEmpresa),
                                      DisplayField = String.Format("{0}", x.nombreComercial)
                                  };

                cboEmpresa.DataSource = lstEmpresas;
                cboEmpresa.DataTextField = "DisplayField";
                cboEmpresa.DataValueField = "DisplayID";
                cboEmpresa.DataBind();

            }
            catch (Exception ex)
            {
            }

        }

        private void Tipo_Cargar()
        {
            try
            {
                MarcasWCFClient objMarcaWCF = new MarcasWCFClient();
                TipoMarca_ListarResult objTipo = new TipoMarca_ListarResult();
                List<TipoMarca_ListarResult> lstTipo;

                lstTipo = objMarcaWCF.TipoMarca_Listar().ToList();

                lstTipo.Insert(0, objTipo);
                objTipo.idTipo = 0;
                objTipo.AbrevTipo = "Todos";


                var lstTipos = from x in lstTipo
                               select new
                               {
                                   x.idTipo,
                                   DisplayID = String.Format("{0}", x.idTipo),
                                   DisplayField = String.Format("{0}", x.AbrevTipo)
                               };

                cboTipo.DataSource = lstTipos;
                cboTipo.DataTextField = "DisplayField";
                cboTipo.DataValueField = "DisplayID";
                cboTipo.DataBind();

            }
            catch (Exception ex)
            {
            }

        }
        private void Pais_Cargar()
        {
            try
            {
                MarcasWCFClient objMarcaWCF = new MarcasWCFClient();
                Pais_ListarResult objPais = new Pais_ListarResult();
                List<Pais_ListarResult> lstPais;

                lstPais = objMarcaWCF.Pais_Listar().ToList();

                lstPais.Insert(0, objPais);
                objPais.idPais = 0;
                objPais.nombrePais = "Todos";


                var lstPaises = from x in lstPais
                                select new
                                {
                                    x.idPais,
                                    DisplayID = String.Format("{0}", x.idPais),
                                    DisplayField = String.Format("{0}", x.nombrePais)
                                };

                cboPais.DataSource = lstPaises;
                cboPais.DataTextField = "DisplayField";
                cboPais.DataValueField = "DisplayID";
                cboPais.DataBind();

            }
            catch (Exception ex)
            {
            }

        }
        private void Titular_Cargar()
        {
            try
            {
                MarcasWCFClient objMarcaWCF = new MarcasWCFClient();
                TitularMarca_ListarResult objTitular = new TitularMarca_ListarResult();
                List<TitularMarca_ListarResult> lstTitular;

                lstTitular = objMarcaWCF.TitularMarca_Listar().ToList();

                lstTitular.Insert(0, objTitular);
                objTitular.idTitular = 0;
                objTitular.nombreTitular = "Todos";


                var lstTitulares = from x in lstTitular
                                   select new
                                   {
                                       x.idTitular,
                                       DisplayID = String.Format("{0}", x.idTitular),
                                       DisplayField = String.Format("{0}", x.nombreTitular)
                                   };

                cboTitular.DataSource = lstTitulares;
                cboTitular.DataTextField = "DisplayField";
                cboTitular.DataValueField = "DisplayID";
                cboTitular.DataBind();

            }
            catch (Exception ex)
            {
            }

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                var empresa = int.Parse(cboEmpresa.SelectedValue);
                var marca = txtMarca.Text.Trim();
                var tipo = int.Parse(cboTipo.SelectedValue);
                var pais = int.Parse(cboPais.SelectedValue);
                var titular = int.Parse(cboTitular.SelectedValue);
                var fechaInicio = dpFechaDesde.SelectedDate.Value;
                var fechaFin = dpFechaHasta.SelectedDate.Value;
                var clase = cboClase.SelectedValue;
                var todo = 0;

                if (checkTodasFechas.Checked) {
                    todo = 1;
                }

                ReporteGeneral_Marcas(empresa, marca, tipo, pais, titular, fechaInicio, fechaFin, clase, todo);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        private void ReporteGeneral_Marcas(int idEmpresa, string marca, int idTipo, int idPais, int idTitular, DateTime fechaInicio, DateTime fechaFin, string clase, int todo)
        {
            MarcasWCFClient objMarcaWCF = new MarcasWCFClient();

            try
            {
                List<RegistroMarca_Listar_v2Result> lst = objMarcaWCF.RegistroMarca_Listar(idEmpresa, marca, idTipo, idPais, idTitular, fechaInicio, fechaFin, clase, todo).ToList();
                grdGeneralMarcas.DataSource = lst;
                grdGeneralMarcas.DataBind();

                ViewState["lstMarcas"] = JsonHelper.JsonSerializer(lst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grdGeneralMarcas_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (ViewState["lstMarcas"] != null)
                {
                    grdGeneralMarcas.DataSource = JsonHelper.JsonDeserialize<List<RegistroMarca_Listar_v2Result>>((string)ViewState["lstMarcas"]);
                    //grdProducto.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {

            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                grdGeneralMarcas.ExportSettings.FileName = "ReporteMarcas_" + DateTime.Now.ToString("ddMMyyyyhhmm");
                grdGeneralMarcas.ExportSettings.ExportOnlyData = true;
                grdGeneralMarcas.ExportSettings.IgnorePaging = true;
                grdGeneralMarcas.ExportSettings.OpenInNewWindow = true;
                grdGeneralMarcas.MasterTableView.ExportToExcel();

                Page.Response.ClearHeaders();
                Page.Response.ClearContent();
            }
            catch (Exception ex)
            {
                //ra.RadAl

            }

        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowInsertForm(0);", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void grdGeneralMarcas_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            try
            {
                if (e.CommandName == "Editar")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowInsertForm(" + e.CommandArgument + ");", true);
                }
                else if (e.CommandName == "Historial")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowHistoryForm(" + e.CommandArgument + ");", true);
                }
                else if (e.CommandName == "AdjuntarDocumento")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "ShowDocumentsForm(" + e.CommandArgument + ");", true);
                }
                else if (e.CommandName == "Logo")
                {
                    if (e.CommandArgument.ToString().Length > 0)
                    {
                        var id = e.CommandArgument.ToString().Split('/')[0];
                        var imagen = e.CommandArgument.ToString().Split('/')[1];

                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "showLogo(" + id + ",'" + imagen + "');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }


        protected void grdGeneralMarcas_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    if (item["Logo"].Text != "&nbsp;")
                    {
                        ((ImageButton)e.Item.FindControl("ibLogo")).Visible = true;
                    }
                    else
                    {
                        ((ImageButton)e.Item.FindControl("ibLogo")).Visible = false;
                    }
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