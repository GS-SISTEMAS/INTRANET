using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GS.SISGEGS.DM;
using System.Configuration;

namespace GS.SISGEGS.BL
{
    public interface IVoucherBL {
        List<gsVoucher_fechContaVSfechaAplicResult> Voucher_FechContaVSFechaAplic(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal);
    }

    public class VoucherBL : IVoucherBL
    {
        public List<gsVoucher_fechContaVSfechaAplicResult> Voucher_FechContaVSFechaAplic(int idEmpresa, int codigoUsuario, DateTime fechaInicio, DateTime fechaFinal)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    ////dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(ConfigurationManager.ConnectionStrings[dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos].ConnectionString, "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    dmGenesysDataContext dcg = new dmGenesysDataContext(string.Format(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, dci.Empresa.SingleOrDefault(x => x.idEmpresa == idEmpresa).baseDatos), "usrGEN" + (10000 + codigoUsuario).ToString().Substring(1, 4)));
                    return dcg.gsVoucher_fechContaVSfechaAplic(fechaInicio, fechaFinal).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se puede realizar la consulta de los vouchers en la base de datos");
                }
            }
        }
    }
}
