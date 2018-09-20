using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using GS.SISGEGS.DM;
using GS.SISGEGS.BL;

namespace GS.SISGEGS.WCF
{
    public class EmpresaWCF : IEmpresaWCF
    {
        public List<Empresa_ComboBoxResult> Empresa_ComboBox()
        {
            EmpresaBL objEmpresaBL;
            try
            {
                objEmpresaBL = new EmpresaBL();
                return objEmpresaBL.Empresa_ComboBox();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Empresa_BuscarDetalleResult> Empresa_BuscarDetalle(int idEmmpresa)
        {
            EmpresaBL objEmpresaBL;
            try
            {
                objEmpresaBL = new EmpresaBL();
                return objEmpresaBL.Empresa_BuscarDetalle(idEmmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Empresa_ListarResult> Empresa_Listar(int idEmmpresa, string detalle)
        {
            EmpresaBL objEmpresaBL;
            try
            {
                objEmpresaBL = new EmpresaBL();
                return objEmpresaBL.Empresa_Listar(idEmmpresa, detalle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Empresa_Registrar(int id_empresa, decimal provision, int comision)
        {
            EmpresaBL objEmpresaBL;
            try
            {
                objEmpresaBL = new EmpresaBL();
                objEmpresaBL.Empresa_Registrar(id_empresa, provision,comision);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }
    }
}
