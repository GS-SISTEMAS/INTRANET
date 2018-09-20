using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GS.SISGEGS.DM;
using System.Data.Linq;
using System.Configuration;

namespace GS.SISGEGS.BL
{
    public interface IEmpresaBL {
        List<Empresa_ComboBoxResult> Empresa_ComboBox();
        List<Empresa_BuscarDetalleResult> Empresa_BuscarDetalle(int idEmpresa);
        List<Empresa_ListarDashboardResult> Empresa_ListarDashboard();
        List<Empresa_ListarResult> Empresa_Listar(int id_empresa, string detalle);
        void Empresa_Registrar(int id_empresa, decimal provision, int comision); 
    }

    public class EmpresaBL : IEmpresaBL
    {
        public List<Empresa_ComboBoxResult> Empresa_ComboBox()
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.Empresa_ComboBox().ToList();
                }
                catch (ChangeConflictException ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al obtener la lista de empresas.");
                }
            }
        }

        public List<Empresa_BuscarDetalleResult> Empresa_BuscarDetalle(int idEmpresa)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.Empresa_BuscarDetalle(idEmpresa).ToList();
                }
                catch (ChangeConflictException ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al obtener la lista de empresas.");
                }
            }
        }

        public List<Empresa_ListarDashboardResult> Empresa_ListarDashboard()
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.Empresa_ListarDashboard().ToList();
                }
                catch (ChangeConflictException ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw ex;
                }
            }
        }

        public List<Empresa_ListarResult> Empresa_Listar( int id_empresa, string detalle )
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.Empresa_Listar(id_empresa, detalle).ToList();
                }
                catch (ChangeConflictException ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw ex;
                }
            }
        }

        public void Empresa_Registrar(int id_empresa, decimal provision, int comision)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    dci.Empresa_Registrar(id_empresa, provision, comision); 
                }
                catch (ChangeConflictException ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw ex;
                }
            }
        }

    }
}
