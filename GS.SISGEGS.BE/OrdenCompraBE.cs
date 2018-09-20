using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GS.SISGEGS.BE
{
    [Serializable]
    public class OrdenCompraSeguimientoBE
    {
        public Int32 Op_OC { get; set; }
        public string No_RegistroParcial { get; set; }

        public int Id_SegImp { get; set; }

        public OrdenCompraSeguimientoBE(Int32 op_oc, string no_registroparcial, int id_segimp)
        {
            this.Op_OC = op_oc;
            this.No_RegistroParcial = no_registroparcial;
            this.Id_SegImp = id_segimp;
        }
    }
}
