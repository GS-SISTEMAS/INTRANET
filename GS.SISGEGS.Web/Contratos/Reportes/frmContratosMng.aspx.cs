using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using GS.SISGEGS.Web.Helpers;
using System.Web.UI.WebControls;
using GS.SISGEGS.Web.ContratosWCF;
using GS.SISGEGS.Web.MateriaContratoWCF;
using GS.SISGEGS.Web.TipoContratoWCF;
using GS.SISGEGS.Web.EstadoContratoWCF;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.Web.Contratos.Reportes
{
    public partial class frmContratosMng : System.Web.UI.Page
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
                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    Area_Cargar();
                    MateriaContrato_Cargar();
                    TipoContrato_Cargar();
                    EstadoContrato_Cargar();
                    Proveedor_Cargar();

                    var idContrato = int.Parse((Request.QueryString["idContrato"]));

                    if (idContrato == 0)
                        Page.Title = "Registrar Contrato";
                    else
                    {
                        Page.Title = "Actualizar Contrato";
                        Contrato_Cargar(idContrato);
                    }
                        
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
         }

        private void Contrato_Cargar(int idContrato) {
            try {
                ContratosWCFClient objContratoWCF = new ContratosWCFClient();
                Contrato_ObtenerResult objContrato = new Contrato_ObtenerResult();
                objContrato = objContratoWCF.Contrato_Obtener(idContrato);

                txtCodigoContrato.Text = objContrato.CodigoContrato;
                cboAreaResponsable.SelectedValue = objContrato.idAreaResponsable.ToString();
                cboMateria.SelectedValue = objContrato.idMateriaContrato.ToString();
                cboTipo.SelectedValue = objContrato.idTipoContrato.ToString();
                txtRenovar.Text = objContrato.Renovar;
                cboCliente.SelectedValue = objContrato.idProveedor.ToString();
                txtContratante.Text = objContrato.Contratante;
                dtpFechaSuscripcion.SelectedDate = DateTime.Parse(objContrato.FechaSuscripcion);
                txtRenovación.Text = objContrato.Renovacion;
                dtpFechaVencimiento.SelectedDate = DateTime.Parse(objContrato.FechaVencimiento);
                txtMonto.Text = objContrato.Monto;
                cboEstado.SelectedValue = objContrato.idEstadoContrato.ToString();
                txtObjeto.Text = objContrato.ObjetoContrato; 
            }
            catch (Exception ex)
            {
            }
        }
        private void Area_Cargar()
        {
            try
            {
                ContratosWCFClient objContratoWCF = new ContratosWCFClient();
                AreaResponsable_ListarResult objArea = new AreaResponsable_ListarResult();
                List<AreaResponsable_ListarResult> lstArea;

                lstArea = objContratoWCF.AreaResponsable_Listar().ToList();

                lstArea.Insert(0, objArea);
                objArea.IdAreaResponsable = 0;
                objArea.nombreAreaResponsable = "Seleccionar";


                var lstAreas = from x in lstArea
                               select new
                               {
                                   x.IdAreaResponsable,
                                   DisplayID = String.Format("{0}", x.IdAreaResponsable ),
                                   DisplayField = String.Format("{0}", x.nombreAreaResponsable)
                                   //DisplayField = String.Format("{0} {1} {2} {3}", x.Placa, x.Modelo, x.Marca, x.Agenda)
                               };

                cboAreaResponsable.DataSource = lstAreas;
                cboAreaResponsable.DataTextField = "DisplayField";
                cboAreaResponsable.DataValueField = "DisplayID";
                cboAreaResponsable.DataBind();

            }
            catch (Exception ex)
            {
            }

        }

        private void MateriaContrato_Cargar()
        {
            try
            {
                MateriaContratoWCFClient objMateriaWCF = new MateriaContratoWCFClient();
                MateriaContrato_ListarResult objMateria = new MateriaContrato_ListarResult();
                List<MateriaContrato_ListarResult> lstMateria;

                lstMateria = objMateriaWCF.MateriaContrato_Listar().ToList();

                lstMateria.Insert(0, objMateria);
                objMateria.nombreMateria = "Seleccionar";
                objMateria.idMateriaContrato = 0;

                var lstMaterias = from x in lstMateria
                                  select new
                                  {
                                      x.idMateriaContrato,
                                      DisplayID = String.Format("{0}", x.idMateriaContrato),
                                      DisplayField = String.Format("{0}", x.nombreMateria)
                                      //DisplayField = String.Format("{0} {1} {2} {3}", x.Placa, x.Modelo, x.Marca, x.Agenda)
                                  };

                cboMateria.DataSource = lstMaterias;
                cboMateria.DataTextField = "DisplayField";
                cboMateria.DataValueField = "DisplayID";
                cboMateria.DataBind();

            }
            catch (Exception ex)
            {
            }
        }

        private void TipoContrato_Cargar()
        {
            try
            {
                TipoContratoWCFClient objTipoWCF = new TipoContratoWCFClient();
                TipoContrato_ListarResult objTipo = new TipoContrato_ListarResult();
                List<TipoContrato_ListarResult> lstTipo;

                lstTipo = objTipoWCF.TipoContrato_Listar().ToList();

                lstTipo.Insert(0, objTipo);
                objTipo.nombreTipo = "Seleccionar";
                objTipo.idTipoContrato = 0;

                var lstTipos = from x in lstTipo
                               select new
                               {
                                   x.idTipoContrato,
                                   DisplayID = String.Format("{0}", x.idTipoContrato),
                                   DisplayField = String.Format("{0}", x.nombreTipo)
                                   //DisplayField = String.Format("{0} {1} {2} {3}", x.Placa, x.Modelo, x.Marca, x.Agenda)
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

        private void EstadoContrato_Cargar()
        {
            try
            {
                EstadoContratoWCFClient objEstadoWCF = new EstadoContratoWCFClient();
                EstadoContrato_ListarResult objEstado = new EstadoContrato_ListarResult();
                List<EstadoContrato_ListarResult> lstEstado;

                lstEstado = objEstadoWCF.EstadoContrato_Listar().ToList();

                lstEstado.Insert(0, objEstado);
                objEstado.nombreEstado = "Seleccionar";
                objEstado.idEstadoContrato = 0;

                var lstEstados = from x in lstEstado
                                 select new
                                 {
                                     x.idEstadoContrato,
                                     DisplayID = String.Format("{0}", x.idEstadoContrato),
                                     DisplayField = String.Format("{0}", x.nombreEstado)
                                     //DisplayField = String.Format("{0} {1} {2} {3}", x.Placa, x.Modelo, x.Marca, x.Agenda)
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

        private void Proveedor_Cargar() {
            try
            {
                ContratosWCFClient objContratoWCF = new ContratosWCFClient();
                ProveedorContrato_ListarResult objProveedor = new ProveedorContrato_ListarResult();
                List<ProveedorContrato_ListarResult> lstProveedor;

                lstProveedor = objContratoWCF.ProveedorContrato_Listar().ToList();

                lstProveedor.Insert(0, objProveedor);
                objProveedor.nombreProveedor = "Seleccionar";
                objProveedor.idProveedor = 0;


                var lstProveedores = from x in lstProveedor
                                     select new
                               {
                                   x.idProveedor,
                                   DisplayID = String.Format("{0}", x.idProveedor),
                                   DisplayField = String.Format("{0}", x.nombreProveedor)
                                   //DisplayField = String.Format("{0} {1} {2} {3}", x.Placa, x.Modelo, x.Marca, x.Agenda)
                               };

                cboCliente.DataSource = lstProveedores;
                cboCliente.DataTextField = "DisplayField";
                cboCliente.DataValueField = "DisplayID";
                cboCliente.DataBind();

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
                ContratosWCFClient objContratoWCF = new ContratosWCFClient();
                if (cboAreaResponsable.SelectedIndex < 0)
                    throw new ArgumentException("Se debe seleccionar un área responsable.");

                if (txtCodigoContrato.Text.Length <= 0)
                    throw new ArgumentException("Se debe especificar el código del contrato.");

                if (DateTime.Compare(dtpFechaSuscripcion.SelectedDate.Value, dtpFechaVencimiento.SelectedDate.Value) > 0)
                    throw new ArithmeticException("La fecha de suscripción no debe ser mayor a la fecha de vencimiento.");

                var idContrato = int.Parse((Request.QueryString["idContrato"]));
                int codigo = int.Parse(txtCodigoContrato.Text);
                int materia = int.Parse(cboMateria.SelectedValue);
                int tipo = int.Parse(cboTipo.SelectedValue);
                int area = int.Parse(cboAreaResponsable.SelectedValue);
                var renovar = txtRenovar.Text;
                var proveedor = int.Parse(cboCliente.SelectedValue);
                var contratante = txtContratante.Text;
                var fechaSuscripcion = dtpFechaSuscripcion.SelectedDate.Value.ToShortDateString();
                var fechaVencimiento = dtpFechaVencimiento.SelectedDate.Value.ToShortDateString();
                var objeto = txtObjeto.Text;
                var renovacion = txtRenovación.Text;
                var monto = txtMonto.Text;
                var estado = int.Parse(cboEstado.SelectedValue);
                var usuario = ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario;

                if(idContrato==0)
                    objContratoWCF.Contrato_Registrar(codigo,materia,tipo,area,renovar,proveedor,contratante,fechaSuscripcion,fechaVencimiento,objeto,renovacion,monto,estado,usuario);
                else
                    objContratoWCF.Contrato_Actualizar(idContrato,codigo, materia, tipo, area, renovar, proveedor, contratante, fechaSuscripcion, fechaVencimiento, objeto, renovacion, monto, estado, usuario);

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