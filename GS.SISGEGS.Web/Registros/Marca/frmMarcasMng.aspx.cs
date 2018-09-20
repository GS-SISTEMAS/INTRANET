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
    public partial class frmMarcasMng : System.Web.UI.Page
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
                    Empresa_Cargar();
                    Tipo_Cargar();
                    Pais_Cargar();
                    Titular_Cargar();
                    Estado_Cargar();
                    var idMarca = int.Parse((Request.QueryString["idMarca"]));

                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    if (idMarca == 0) {
                        lblTitulo.Text = "Nueva Marca";
                            Page.Title = "Registrar Marca";
                    }
                        

                    else
                    {
                        lblTitulo.Text = "Modificar Marca";
                        Page.Title = "Modificar Marca";
                        Marca_Cargar(idMarca);
                    }
                        
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

        private void Estado_Cargar()
        {
            try
            {
                MarcasWCFClient objMarcaWCF = new MarcasWCFClient();
                EstadoMarca_ListarResult objEstado = new EstadoMarca_ListarResult();
                List<EstadoMarca_ListarResult> lstEstado;

                lstEstado = objMarcaWCF.EstadoMarca_Listar().ToList();

                lstEstado.Insert(0, objEstado);
                objEstado.idEstadoMarca = 0;
                objEstado.nombreEstado = "Todos";


                var lstEstados = from x in lstEstado
                                   select new
                                   {
                                       x.idEstadoMarca,
                                       DisplayID = String.Format("{0}", x.idEstadoMarca),
                                       DisplayField = String.Format("{0}", x.nombreEstado)
                                   };

                cboEstado.DataSource = lstEstados;
                cboEstado.DataTextField = "DisplayField";
                cboEstado.DataValueField = "DisplayID";
                cboEstado.DataBind();

            }
            catch (Exception ex)
            {
            }

        }
        private void Marca_Cargar(int idMarca) {
            try {
                MarcasWCFClient objMarcaWCF = new MarcasWCFClient();
                RegistroMarca_ObtenerResult objMarca = new RegistroMarca_ObtenerResult();
                objMarca = objMarcaWCF.RegistroMarca_Obtener(idMarca);

                cboEmpresa.SelectedValue = objMarca.idEmpresa.ToString();
                txtMarca.Text = objMarca.MARCA;
                cboTipo.SelectedValue = objMarca.idTipo.ToString();
                txtCertificado.Text = objMarca.certificado;
                cboClase.SelectedValue= objMarca.clase.ToString();
                cboPais.SelectedValue = objMarca.idPais.ToString();
                dtpVencimiento.SelectedDate = objMarca.fechaVencimiento;
                cboTitular.SelectedValue = objMarca.idTitular.ToString();
                cboEstado.SelectedValue = objMarca.idEstadoMarca.ToString();
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                MarcasWCFClient objMarcasWCF = new MarcasWCFClient();
                //if (cboAreaResponsable.SelectedIndex < 0)
                //    throw new ArgumentException("Se debe seleccionar un área responsable.");

                //if (txtCodigoContrato.Text.Length <= 0)
                //    throw new ArgumentException("Se debe especificar el código del contrato.");

                //if (DateTime.Compare(dtpFechaSuscripcion.SelectedDate.Value, dtpFechaVencimiento.SelectedDate.Value) > 0)
                //    throw new ArithmeticException("La fecha de suscripción no debe ser mayor a la fecha de vencimiento.");

                var idMarca = int.Parse((Request.QueryString["idMarca"]));
                var idEmpresa = ((Usuario_LoginResult)Session["Usuario"]).idEmpresa;
                var marca = txtMarca.Text;
                var idPais = int.Parse(cboPais.SelectedValue);
                var idTipo = int.Parse(cboTipo.SelectedValue);
                var idTitular = int.Parse(cboTitular.SelectedValue);
                var clase = cboClase.SelectedValue;
                var certificado = txtCertificado.Text; 
                var fechaVencimiento = dtpVencimiento.SelectedDate.Value;
                var estado = int.Parse(cboEstado.SelectedValue);
                var observacion = "";
                var usuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;

                objMarcasWCF.RegistroMarca_Registrar(idMarca,idEmpresa,marca,idPais,idTipo,idTitular,clase,certificado,fechaVencimiento,estado,observacion,usuario);

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CloseAndRebind();", true);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR: " + ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
}
}