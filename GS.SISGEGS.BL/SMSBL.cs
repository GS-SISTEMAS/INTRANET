using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GS.SISGEGS.DM;
using System.Configuration;

namespace GS.SISGEGS.BL
{

    public interface ISMSnBL
    {
        List<SP_PerfilesEmpresaResult> Lista_PerfilesEmpresa(int idEmpresa);

        void Registro_SMS(string text, string id_perfil);

    }
    public class SMSBL
    {
        public List<SP_PerfilesEmpresaResult> Lista_PerfilesEmpresa(int idEmpresa)
        {
            ////using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            //using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "gs0genesys")))

            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    return dci.SP_PerfilesEmpresa(idEmpresa).ToList();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error consultar el reporte general de registro de SMS en la base de datos.");
                }
            }
        }


        public void Registro_SMS(string text, string id_perfil)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys")))
            {
                try
                {
                    dci.USP_INS_SMS(text, id_perfil);
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("Error al registrar/actualizar la marca en la base de datos.");
                }
            }

        }

    }

}

