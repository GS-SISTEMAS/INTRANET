using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using GS.SISGEGS.DM;
using System.Transactions;

namespace GS.SISGEGS.BL
{
    public interface IRRHHBL
    {
        List<Ingreso_PersonalResult> Ingreso_Personal(DateTime fecha);
        List<Ingreso_Personal_DetalleResult> Ingreso_PersonalDetalle(DateTime fecha, string ccosto);
        List<Ingreso_Personal_PermisosResult> Ingreso_PersonalPermisos(DateTime fecha, string ccosto);
        List<Personal_ListarCumpleanhosResult> Personal_ListarCumpleanhos(DateTime fecha);
        List<Personal_ListarResult> Personal_Listar(string codEmpresa, string texto);
        void Personal_Registrar(int idPersonal, string nroDocumento, string imageURL, int idUsuarioRegistro);

    }
    public class RRHHBL : IRRHHBL
    {
        public List<Ingreso_PersonalResult> Ingreso_Personal(DateTime fecha)
        {
            //dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString);            
            dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys"));
                try {
                return
                dci.Ingreso_Personal(fecha).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }

        public List<Ingreso_Personal_DetalleResult> Ingreso_PersonalDetalle(DateTime fecha, string ccosto)
        {
            dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString);

            try
            {
                return
                dci.Ingreso_Personal_Detalle(fecha, ccosto).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }

        public List<Ingreso_Personal_PermisosResult> Ingreso_PersonalPermisos(DateTime fecha, string ccosto)
        {
            //dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString);
            dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys"));

            try
            {
                return
                dci.Ingreso_Personal_Permisos(fecha,ccosto).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }

        public List<Personal_ListarResult> Personal_Listar(string codEmpresa, string texto)
        {
            //dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString);
            dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys"));
            try
            {
                return
                dci.Personal_Listar(codEmpresa, texto).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }

        public List<Personal_ListarCumpleanhosResult> Personal_ListarCumpleanhos(DateTime fecha)
        {
            //dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString);
            dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys"));
            try
            {
                return
                dci.Personal_ListarCumpleanhos(fecha).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }

        public void Personal_Registrar(int idPersonal, string nroDocumento, string imageURL, int idUsuarioRegistro)
        {
            //dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString);
            dmIntranetDataContext dci = new dmIntranetDataContext(GS.configuration.Init.GetValue(Constant.sistema, Constant.key, "genesys"));
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    dci.Personal_Registrar(idPersonal, nroDocumento, imageURL, idUsuarioRegistro);
                    dci.SubmitChanges();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw ex;
                }
            }
        }
    }
}
