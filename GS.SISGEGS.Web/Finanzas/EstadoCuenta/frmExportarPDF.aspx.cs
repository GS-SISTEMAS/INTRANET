using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GS.SISGEGS.Web.Finanzas.EstadoCuenta
{
    public partial class frmExportarPDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string FileName;
            FileName = Convert.ToString(Request.QueryString["strFileNombre"]);
            Response.Redirect("~/Finanzas/EstadoCuenta/tempArchivos/" + FileName);
        }
    }
}