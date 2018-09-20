using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.SISGEGS.Web.ReportesRRHHWCF;
using GS.SISGEGS.DM;

namespace GS.SISGEGS.Web.RRHH.Reportes
{
    public partial class frmIngresoPersonal : System.Web.UI.Page
    {
        private void IngresoPersonal_Detalle (DateTime fecha, string ccosto)
        {
            ReportesRRHHClient objreportesrrhh = new ReportesRRHHClient();
            List<Ingreso_Personal_DetalleResult> lstdetalle;
            Ingreso_Personal_PermisosResult[] lstpermisos=null;

            try
            {
                lstdetalle=objreportesrrhh.Ingreso_PersonalDetalle(fecha,ccosto,ref lstpermisos) .ToList();
                grdAsistenciaDetalle.DataSource = lstdetalle;
                grdAsistenciaDetalle.DataBind();
                grdAsistenciaPermisos.DataSource = lstpermisos;
                grdAsistenciaPermisos.DataBind();

            }
            catch (Exception ex) { throw ex; }
        }
        private void IngresoPersonal(DateTime fecha)
        {
            ReportesRRHHClient objreportesrrhh = new ReportesRRHHClient();
            try
            {
                List<Ingreso_PersonalResult> lst= objreportesrrhh.Ingreso_Personal(fecha).ToList();
                grdAsistencia.DataSource = objreportesrrhh.Ingreso_Personal(fecha);
                grdAsistencia.DataBind();
                ViewState["fecha"] = fecha;
            }
            catch (Exception ex) { throw ex; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    dpFecha.SelectedDate = DateTime.Now;
                    IngresoPersonal(dpFecha.SelectedDate.Value);

                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)HttpContext.Current.Session["Usuario"]).idUsuario);
                }
            }
            catch (Exception ex) { throw ex; }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                IngresoPersonal(dpFecha.SelectedDate.Value);
                grdAsistenciaDetalle.DataSource = null;
                grdAsistenciaDetalle.DataBind();
                grdAsistenciaPermisos.DataSource = null;
                grdAsistenciaPermisos.DataBind();
            }
            catch (Exception ex) { throw ex; }
        }
        protected void grdAsistencia_selectedindexchanged(object sender, EventArgs e)
        {
            try
            {
                IngresoPersonal_Detalle((DateTime) ViewState["fecha"],grdAsistencia.SelectedItems[0].Cells[2].Text);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}