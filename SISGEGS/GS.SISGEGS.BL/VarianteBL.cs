using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GS.SISGEGS.DM;
using System.Configuration;

namespace GS.SISGEGS.BL
{
    public interface IVarianteBL {
        Variante_BuscarResult Variante_Buscar(int idEmpresa, string codigoVariante);
    }

    public class VarianteBL : IVarianteBL
    {
        public Variante_BuscarResult Variante_Buscar(int idEmpresa, string codigoVariante)
        {
            using (dmIntranetDataContext dci = new dmIntranetDataContext(ConfigurationManager.ConnectionStrings["genesys"].ConnectionString))
            {
                try
                {
                    return dci.Variante_Buscar(idEmpresa, codigoVariante).Single();
                }
                catch (Exception ex)
                {
                    dci.Excepcion_Registrar(ex.Message, ex.TargetSite.Name);
                    dci.SubmitChanges();
                    throw new ArgumentException("No se pudo obtener la variante de la página.");
                }
            }
        }
    }
}
