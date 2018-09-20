using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

using GS.SISGEGS.DM;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.AgendaWCF;
using GS.SISGEGS.Web.DocumentoWCF;
using GS.SISGEGS.Web.FormaPagoWCF;
using GS.SISGEGS.Web.OrdenVentaWCF;
using GS.SISGEGS.Web.LoginWCF;
using GS.SISGEGS.Web.ReporteSistemasWCF;
using GS.SISGEGS.Web.OrdenCompraWCF;


namespace GS.SISGEGS.Web.Compras.OC
{
    public partial class frmOcImpParcial : System.Web.UI.Page
    {
        #region PROCEDIMIENTOS
        private void CargarComboParcial(List<USP_Sel_Genesys_OC_ImpResult> lst_OCParcialOp)
        {

            
            cbonroparcial.Items.Clear();
            if (lst_OCParcialOp.Any())
            {

                for (int x = 1; x <= lst_OCParcialOp.Select(y => y.NroParcial).Distinct().Max(); x++)
                {
                    cbonroparcial.Items.Add(x.ToString());

                }
                cbonroparcial.Items.Add((cbonroparcial.Items.Count() + 1).ToString());

            }
            else
                cbonroparcial.Items.Add("1");
        }
        private void CargarKardex(int op)
        {
            OrdenCompraWCFClient objOrdenCompraWCF = new OrdenCompraWCFClient();
            List<USP_Sel_OC_OpLineaResult> lst_OcOplinea = new List<USP_Sel_OC_OpLineaResult>();
            lst_OcOplinea = objOrdenCompraWCF.Seleccionar_OC_OPLinea(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
               op).ToList();

            var lstkardex = from x in lst_OcOplinea
                            select new
                            {
                                ValueField = x.Kardex,
                                TextField = x.Nombre
                            };

            cbokardex.DataSource = lstkardex;
            cbokardex.DataTextField = "TextField";
            cbokardex.DataValueField = "ValueField";
            cbokardex.DataBind();


        }
        private void CargarDatos(int op, int nroparcial)
        {
            OrdenCompraWCFClient objOrdenCompraWCF = new OrdenCompraWCFClient();


            List<USP_Sel_OC_OpResult> lst_OcOp = new List<USP_Sel_OC_OpResult>();
            List<USP_Sel_OC_OpLineaResult> lst_OcOplinea = new List<USP_Sel_OC_OpLineaResult>();
            //List<USP_Sel_Genesys_OC_ImpLineaResult> lst_OcOpParcial = new List<USP_Sel_Genesys_OC_ImpLineaResult>();
            List<USP_Sel_OC_OpParcialResult> lst_OcOpParcialLinea = new List<USP_Sel_OC_OpParcialResult>();
            List<USP_Sel_Genesys_OC_ImpResult> lst_OCParcialOp = new List<USP_Sel_Genesys_OC_ImpResult>();

            lst_OcOp = objOrdenCompraWCF.Seleccionar_OC_OP(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
                op).ToList();

            lst_OcOplinea = objOrdenCompraWCF.Seleccionar_OC_OPLinea(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
                op).ToList();

            lst_OcOpParcialLinea = objOrdenCompraWCF.Seleccionar_OC_OpParcial(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
                op).ToList();



            gvwoc.DataSource = lst_OcOplinea;
            gvwoc.DataBind();

            gvwparcial.DataSource = lst_OcOpParcialLinea.Where(x => x.NroParcial == nroparcial && x.Op_OC == op).ToList();
            gvwparcial.DataBind();




            //var lstkardex = from x in lst_OcOplinea
            //               select new
            //               {
            //                   ValueField = x.Kardex,
            //                   TextField = x.Nombre
            //               };

            //cbokardex.DataSource = lstkardex;
            //cbokardex.DataTextField = "TextField";
            //cbokardex.DataValueField = "ValueField";
            //cbokardex.DataBind();

            txtop.Text = lst_OcOp.Select(x => x.Op.ToString()).First();
            txtagendanombre.Text = lst_OcOp.Select(x => x.AgendaNombre).First();
            txtidagenda.Text = lst_OcOp.Select(x => x.ID_Agenda).First();
            txtnrooc.Text = lst_OcOp.Select(x => x.NoRegistro).First();
            txtobservaciones.Text = lst_OcOp.Select(x => x.Observaciones).First();
            txtnroimpparcial.Text = lst_OcOp.Select(x => x.NoRegistro).First();
            dtpfechaoc.SelectedDate = lst_OcOp.Select(x => x.FechaOrden).First();

            USP_Sel_Genesys_OC_ImpResult eCab;
            foreach (int? det in lst_OcOpParcialLinea.Select(x => x.NroParcial).Distinct().ToList())
            {
                eCab = new USP_Sel_Genesys_OC_ImpResult();
                eCab.Op_OC = Convert.ToDecimal(txtop.Text);
                eCab.No_Registro = txtnroimpparcial.Text;
                eCab.NroParcial = det;
                eCab.OpParcial_OC = 0;
                eCab.No_RegistroParcial = txtnroimpparcial.Text + "." + det.ToString();
                eCab.EsParcial = 1;
                eCab.Neto = lst_OcOpParcialLinea.Where(x => x.NroParcial == det && x.Op_OC == op).ToList().Sum(x => x.Cantidad * x.Precio);
                eCab.Subtotal = lst_OcOpParcialLinea.Where(x => x.NroParcial == det && x.Op_OC == op).ToList().Sum(x => x.Cantidad * x.Precio);
                eCab.Impuestos = 0;
                eCab.Total = 0;
                eCab.Observaciones = txtobservaciones.Text;
                eCab.FechaIngresoAlm = (DateTime?)null;
                eCab.FlgOcProcesada = 0;
                eCab.Id_SegImp = 0;
                lst_OCParcialOp.Add(eCab);
            }



            CargarComboParcial(lst_OCParcialOp);

            Session["lstoc"] = JsonHelper.JsonSerializer(lst_OcOplinea);
            Session["lstparcial"] = JsonHelper.JsonSerializer(lst_OcOpParcialLinea);
            Session["lstparcialOp"] = JsonHelper.JsonSerializer(lst_OCParcialOp);
            
        }
        private void AgregarItemParcial(int op)
        {
            List<USP_Sel_OC_OpParcialResult> lstparcial = new List<USP_Sel_OC_OpParcialResult>();
            List<USP_Sel_OC_OpLineaResult> lst_OcOplinea = new List<USP_Sel_OC_OpLineaResult>();
            List<USP_Sel_Genesys_OC_ImpResult> lst_OCParcialOp = new List<USP_Sel_Genesys_OC_ImpResult>();

            OrdenCompraWCFClient objOrdenCompraWCF = new OrdenCompraWCFClient();
            
            //List<USP_Sel_OC_OpParcialResult> lst_OcOpParcialLinea = new List<USP_Sel_OC_OpParcialResult>();
            USP_Sel_OC_OpParcialResult ItemParcial = new USP_Sel_OC_OpParcialResult();
            double CantidadTotalKardex, PrecioUnitario;

            lst_OcOplinea = objOrdenCompraWCF.Seleccionar_OC_OPLinea(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
                op).ToList();

            Int32 kardex = Convert.ToInt32(cbokardex.SelectedValue.ToString() == string.Empty ? "0" : cbokardex.SelectedValue.ToString());
            double CantidadParcial = (Convert.ToDouble(txtcantidad2.Text.Trim() == string.Empty ? "0" : txtcantidad2.Text));

            if(Convert.ToInt32(hvid.Value.ToString()==string.Empty ? "0" : hvid.Value.ToString())==0)
                CantidadTotalKardex=Convert.ToDouble(lst_OcOplinea.Where(x => x.Kardex == kardex).Sum(x => x.Cantidad));
            else
                CantidadTotalKardex = Convert.ToDouble(lst_OcOplinea.Where(x => x.Kardex == kardex).Sum(x => x.Cantidad));

            PrecioUnitario = Convert.ToDouble(txtprecio2.Text == string.Empty ? "0" : txtprecio2.Text);


            //obtencion de lista de parciales
            lstparcial = JsonHelper.JsonDeserialize<List<USP_Sel_OC_OpParcialResult>>((string)Session["lstparcial"]);
            if (lstparcial.Where(x => x.Kardex == kardex && x.NroParcial == Convert.ToInt32(cbonroparcial.Text)).Any())
                throw new ArgumentException("ERROR: Solo puede existir un tipo de kardex por Parcial.");


            if (CantidadParcial > CantidadTotalKardex)
                throw new ArgumentException("ERROR: La cantidad que esta ingresando es superior a la cantidad disponible");
            else if (PrecioUnitario < 0)
                throw new ArgumentException("ERROR: El precio unitario debe ser positivo.");
            else if(CantidadParcial<=0)
                throw new ArgumentException("ERROR: La cantidad debe ser mayor a 0");
            else if(Math.Round(CantidadTotalKardex,2)< Math.Round(Convert.ToDouble(lstparcial.Where(x=> x.Kardex== kardex).Sum(x=> x.Cantidad))))
                throw new ArgumentException("ERROR: La cantidad total del kardex la OC no puede ser a la cantidad total del kardex de los parciales");
      
            else
            {
                

               

                ItemParcial = JsonHelper.JsonDeserialize<USP_Sel_OC_OpParcialResult>((string)Session["lstparcial"]);
                ItemParcial.Op_OC = Convert.ToInt32(txtop.Text);
                ItemParcial.NroParcial = Convert.ToInt32(cbonroparcial.Text);
                ItemParcial.No_RegistroParcial = txtnroimpparcial.Text + "." + cbonroparcial.Text;
                ItemParcial.Id = Convert.ToInt32(hvid.Value.ToString()==string.Empty ? "0" : hvid.Value.ToString());
                ItemParcial.ItemCodigo = lst_OcOplinea.Where(x => x.Kardex == kardex).Select(x => x.ItemCodigo).First();
                ItemParcial.Kardex = kardex;
                ItemParcial.NombreKardex = cbokardex.Text;
                ItemParcial.Id_UnidadInv = lst_OcOplinea.Where(x => x.Kardex == kardex).Select(x => x.ID_UnidadInv).First();
                ItemParcial.Cantidad = Convert.ToDecimal(txtcantidad2.Text);
                ItemParcial.Precio = Convert.ToDecimal(txtprecio2.Text);
                ItemParcial.Importe = ItemParcial.Cantidad * ItemParcial.Precio;
                

                if(ItemParcial.Id==0)
                    lstparcial.Add(ItemParcial);
                else
                {
                    lstparcial.Where(x => x.Id == ItemParcial.Id).ToList().ForEach(x => x.Cantidad = ItemParcial.Cantidad);
                    lstparcial.Where(x => x.Id == ItemParcial.Id).ToList().ForEach(x => x.Precio = ItemParcial.Precio);
                    lstparcial.Where(x => x.Id == ItemParcial.Id).ToList().ForEach(x => x.Importe = ItemParcial.Cantidad * ItemParcial.Precio);
                }

                lst_OCParcialOp = JsonHelper.JsonDeserialize<List<USP_Sel_Genesys_OC_ImpResult>>((string)Session["lstparcialOp"]);
                decimal? neto = lstparcial.Where(x => x.NroParcial == ItemParcial.NroParcial && x.Op_OC == op).ToList().Sum(x => x.Cantidad * x.Precio);

                if (lst_OCParcialOp.Where(x => x.NroParcial == Convert.ToInt32(ItemParcial.NroParcial)).ToList().Any())
                {
                    lst_OCParcialOp.Where(x => x.NroParcial == ItemParcial.NroParcial).ToList().ForEach(x => x.Neto = neto);
                    lst_OCParcialOp.Where(x => x.NroParcial == ItemParcial.NroParcial).ToList().ForEach(x => x.Subtotal = neto);
                    
                }
                else
                {
                    USP_Sel_Genesys_OC_ImpResult eCab;
                    eCab = new USP_Sel_Genesys_OC_ImpResult();
                    eCab.Op_OC = Convert.ToDecimal(txtop.Text);
                    eCab.No_Registro = txtnroimpparcial.Text;
                    eCab.OpParcial_OC = 0;
                    eCab.NroParcial = Convert.ToInt32(cbonroparcial.Text);
                    eCab.No_RegistroParcial = txtnroimpparcial.Text + "." + cbonroparcial.Text;
                    eCab.EsParcial = 1;
                    eCab.Neto = neto;
                    eCab.Subtotal = neto;
                    eCab.Impuestos = 0;
                    eCab.Total = 0;
                    eCab.Observaciones = txtobservaciones.Text;
                    eCab.FechaIngresoAlm = (DateTime?)null;
                    eCab.FlgOcProcesada = 0;
                    eCab.Id_SegImp = 0;
                    lst_OCParcialOp.Add(eCab);

                    
                    CargarComboParcial(lst_OCParcialOp);
                    

                }

                //gvwparcial.DataSource = lstparcial.Where(x => x.NroParcial == Convert.ToInt32(cbonroparcial.Text == string.Empty ? "0" : cbonroparcial.Text)).ToList();
                
                gvwparcial.DataSource = lstparcial.Where(x => x.NroParcial == ItemParcial.NroParcial).ToList();
                gvwparcial.DataBind();

                Session["lstparcial"] = JsonHelper.JsonSerializer(lstparcial);
                Session["lstparcialOp"] = JsonHelper.JsonSerializer(lst_OCParcialOp);

                //CargarComboParcial(lstparcial);

                hvid.Value = "0";
            }
            CargarCantidadDisponible();
                



        }

        private void ValidarCantidades(List<USP_Sel_OC_OpParcialResult> lstparcial)
        {
            OrdenCompraWCFClient objOrdenCompraWCF = new OrdenCompraWCFClient();
            List<USP_Sel_OC_OpLineaResult> lst_OcOplinea = new List<USP_Sel_OC_OpLineaResult>();
            lst_OcOplinea = objOrdenCompraWCF.Seleccionar_OC_OPLinea(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
                Convert.ToInt32(txtop.Text)).ToList();

            
            if(Math.Round(Convert.ToDecimal(lstparcial.Sum(x=>x.Cantidad)),2)!= Math.Round(Convert.ToDecimal(lst_OcOplinea.Sum(x=>x.Cantidad)),2))
                throw new Exception("Las cantidad total de la OC es distinta a la Cantidad de los parciales");
            
        }
        private void GuardarOcParcial()
        {
            OrdenCompraWCFClient objOrdenCompraWCF = new OrdenCompraWCFClient();
            List<USP_Sel_Genesys_OC_ImpResult> CabOcParcial = new List<USP_Sel_Genesys_OC_ImpResult>();
            List<USP_Sel_Genesys_OC_ImpLineaResult> DetOcParcial = new List<USP_Sel_Genesys_OC_ImpLineaResult>();
            USP_Sel_Genesys_OC_ImpLineaResult item = new USP_Sel_Genesys_OC_ImpLineaResult();

            List<USP_Sel_OC_OpParcialResult> lstparcial = new List<USP_Sel_OC_OpParcialResult>();
            
            lstparcial = JsonHelper.JsonDeserialize<List<USP_Sel_OC_OpParcialResult>>((string)Session["lstparcial"]);
            CabOcParcial = JsonHelper.JsonDeserialize<List<USP_Sel_Genesys_OC_ImpResult>>((string)Session["lstparcialOp"]);

            ValidarCantidades(lstparcial);

            
            foreach (USP_Sel_Genesys_OC_ImpResult cab in CabOcParcial)
            {
                foreach (USP_Sel_OC_OpParcialResult e in lstparcial.Where(x=> x.NroParcial==cab.NroParcial).ToList())
                {
                    item = new USP_Sel_Genesys_OC_ImpLineaResult();
                    item.Id = e.Id;
                    item.NroParcial = e.NroParcial;
                    item.Op_OC = Convert.ToDecimal(txtop.Text);
                    item.OpParcial_OC = 0;
                    item.No_RegistroParcial = cab.No_RegistroParcial;
                    item.Linea = 0;
                    item.Id_Item = e.ItemCodigo;
                    item.Item_ID = e.Kardex;
                    item.Id_UnidadInv = e.Id_UnidadInv;
                    item.Cantidad = e.Cantidad;
                    item.Precio = e.Precio;
                    item.Importe = e.Cantidad * e.Precio;
                    item.Observaciones = string.Empty;
                    DetOcParcial.Add(item);
                }
            }
            
            objOrdenCompraWCF.Registrar_Oc_Parcial(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario,
                CabOcParcial.ToArray(), DetOcParcial.ToArray());

            lblMensaje.Text = "Se registró correctamente la OC Parcial";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "AvisoOk('Se registró correctamente la OC Parcial');", true);
            CargarDatos(Convert.ToInt32(txtop.Text),Convert.ToInt32(cbonroparcial.Text==string.Empty ? "0" : cbonroparcial.Text));
        }

        private void CargarCantidadDisponible()
        {
            List<USP_Sel_OC_OpLineaResult> lst_OcOpLinea = new List<USP_Sel_OC_OpLineaResult>();
            List<USP_Sel_OC_OpParcialResult> lst_OcOpParcialLinea = new List<USP_Sel_OC_OpParcialResult>();
            lst_OcOpParcialLinea = JsonHelper.JsonDeserialize<List<USP_Sel_OC_OpParcialResult>>((string)Session["lstparcial"]);
            lst_OcOpLinea = JsonHelper.JsonDeserialize<List<USP_Sel_OC_OpLineaResult>>((string)Session["lstoc"]);
            decimal? cantidaddisponible = 0;

            if (lst_OcOpLinea.Any())
            {
                txtprecio2.Text = lst_OcOpLinea.Where(x => x.Kardex == Convert.ToInt32(cbokardex.SelectedValue.ToString())).Select(x => x.Precio.ToString()).First();
                txtcantidad2.Text = "0";
                cantidaddisponible = lst_OcOpLinea.Where(x => x.Kardex == Convert.ToInt32(cbokardex.SelectedValue.ToString())).Select(x => x.Cantidad).First();

                if (lst_OcOpParcialLinea.Any())
                    cantidaddisponible = cantidaddisponible - lst_OcOpParcialLinea.Where(x => x.Kardex == Convert.ToInt32(cbokardex.SelectedValue.ToString())).Select(x => x.Cantidad).Sum();


                txtcantidaddisponible.Text = cantidaddisponible.ToString();
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                if (!Page.IsPostBack)
                {
                    LoginWCF.LoginWCFClient objLoginWCF = new LoginWCF.LoginWCFClient();
                    objLoginWCF.AuditoriaMenu_Registrar(System.Web.HttpContext.Current.Request.Url.AbsolutePath, Environment.MachineName,
                        ((Usuario_LoginResult)System.Web.HttpContext.Current.Session["Usuario"]).idUsuario);

                    int Op = int.Parse((Request.QueryString["Op"]));
                    Session["Op"] = Op;
                    if (Op==0)
                    {
                        Title = "Orden de Compra no Valido";
                        lblMensaje.Text = "Orden de comrpa no valido";
                    }
                    else
                    {
                        Session["lstparcialOp"] = null;
                        Session["lstparcial"] = null;
                        Session["lstoc"] = null;
                        Session["lstkardex"] = null;
                        //Session["lstnroparcial"] = null;


                        //cbonroparcial.Items.Add("1");

                        //cbonroparcial.DataSource = lstnroparcial;
                        //cbonroparcial.DataBind();

                        CargarKardex(Op);
                        CargarDatos(Op,1);
                        CargarCantidadDisponible();
                        
                        lblMensaje.Text = "Orden de compra en modificación: " + Convert.ToString(Op);

                    }
                    lblMensaje.CssClass = "mensajeExito";
                }
            }
            catch (Exception ex)
            {
                rwmOC.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                
                Session.Remove("lstoc");
                Session.Remove("lstparcial");
                Session.Remove("lstparcialOp");
                Session.Remove("lstkardex");
                Response.Redirect("~/Compras/OC/frmOcImportacion.aspx");

                //if (string.IsNullOrEmpty(Request.QueryString["Op"]))
                //    Response.Redirect("~/Comercial/Pedido/frmOrdenVenta.aspx");
                //else
                //    Response.Redirect("~/Comercial/Pedido/frmOrdenVenta.aspx?fechaInicial=" + Request.QueryString["fechaInicial"] +
                //        "&fechaFinal=" + Request.QueryString["fechafinal"]);

            }
            catch (Exception ex)
            {
                rwmOC.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
        }
        
        //protected void btnagregarparcial_Click(object sender, EventArgs e)
        //{

        //}
        protected void gvwoc_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                //grdMaximo.DataSource = JsonHelper.JsonDeserialize<List<USP_Sel_ControlFacturasMaximoResult>>((string)ViewState["lstfacturas"]);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void gvwoc_ItemCommand(object sender, GridCommandEventArgs e)
        {
        }
        protected void gvwoc_ItemDataBound(object sender, GridItemEventArgs e)
        {
            
        }
        protected void gvwparcial_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                //grdMaximo.DataSource = JsonHelper.JsonDeserialize<List<USP_Sel_ControlFacturasMaximoResult>>((string)ViewState["lstfacturas"]);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }

        protected void gvwparcial_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                List<USP_Sel_OC_OpParcialResult> lst_OcOpParcialLinea = new List<USP_Sel_OC_OpParcialResult>();
                List<USP_Sel_OC_OpLineaResult> lst_OcOpLinea = new List<USP_Sel_OC_OpLineaResult>();

                lst_OcOpParcialLinea = JsonHelper.JsonDeserialize<List<USP_Sel_OC_OpParcialResult>>((string)Session["lstparcial"]);
                lst_OcOpLinea = JsonHelper.JsonDeserialize<List<USP_Sel_OC_OpLineaResult>>((string)Session["lstoc"]);

                Int32 id = 0;
                //decimal? cantidaddisponible = 0;
                string nroparcial = string.Empty;
                string kardex = string.Empty;
                GridDataItem item = (GridDataItem)e.Item;
                //Int32 nroparcial = Convert.ToInt32(DataBinder.Eval(item.DataItem, "NroParcial"));



                if (lst_OcOpParcialLinea.Any())
                {
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    id = Convert.ToInt32(commandArgs[0]);
                    nroparcial = commandArgs[1];
                    kardex= commandArgs[2];

                    hvid.Value = id.ToString();// e.CommandArgument.ToString();
                    

                    if (e.CommandName == "EliminarItem")
                    {
                        

                        OrdenCompraWCFClient objOrdenCompraWCF = new OrdenCompraWCFClient();
             

                        lst_OcOpParcialLinea.Remove(lst_OcOpParcialLinea.Where(x => x.Id == id && x.NroParcial == Convert.ToInt32(nroparcial) && x.Kardex == Convert.ToDecimal(kardex)).First());

                        

                    
                        Session["lstparcial"] = JsonHelper.JsonSerializer(lst_OcOpParcialLinea);

                        List<USP_Sel_Genesys_OC_ImpResult> CabOcParcial = new List<USP_Sel_Genesys_OC_ImpResult>();
                        CabOcParcial = JsonHelper.JsonDeserialize<List<USP_Sel_Genesys_OC_ImpResult>>((string)Session["lstparcialOp"]);

                        if (!lst_OcOpParcialLinea.Where(x=>x.NroParcial== Convert.ToInt32(nroparcial)).Any())
                        {
                            CabOcParcial.Remove(CabOcParcial.Where(x => x.NroParcial == Convert.ToInt32(nroparcial)).First());
                            Session["lstparcialOp"] = JsonHelper.JsonSerializer(CabOcParcial);
                        }

                        hvid.Value = "0";
                        
                        //CargarDatos(Convert.ToInt32(txtop.Text),1); no habilitar
                        CargarCantidadDisponible();
                        lblMensaje.CssClass = "Se eliminó el registro seleccionado";
                        gvwparcial.DataSource = lst_OcOpParcialLinea.Where(x => x.NroParcial == Convert.ToInt32(cbonroparcial.Text.ToString() == string.Empty ? "0" : cbonroparcial.Text.ToString())).ToList();
                        gvwparcial.DataBind();


                    }
                }
                
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.CssClass = "mensajeError";
            }
        }
        protected void gvwparcial_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //if (Session["Usuario"] == null)
            //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            //try
            //{
            //    if (e.Item is GridDataItem)
            //    {
            //        GridDataItem dataItem = e.Item as GridDataItem;
            //        if (decimal.Parse(dataItem["Stock"].Text) <= 0)
            //            e.Item.ForeColor = System.Drawing.Color.Red;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    rwmPedidoMng.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            //}
        }

        

        protected void btnagregaritem_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {
                AgregarItemParcial(Convert.ToInt32(txtop.Text));
                //CargarDatos(Convert.ToInt32(txtop.Text), Convert.ToInt32(cbonroparcial.Text == string.Empty ? "0" : cbonroparcial.Text));
            }
            catch (Exception ex)
            {
                rwmOC.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
            }
            
        }

        protected void cbokardex_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            CargarCantidadDisponible();
            

        }

        protected void btnguardaritem_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);

            try
            {

                GuardarOcParcial();
            }
            catch (Exception ex)
            {
                rwmOC.RadAlert(ex.Message, 400, null, "Mensaje de error", null);
                CargarCantidadDisponible();
            }
        }

        protected void btneditaritem_Click(object sender, ImageClickEventArgs e)
        {
          
        }

        protected void cbonroparcial_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            List<USP_Sel_OC_OpLineaResult> lst_OcOpLinea = new List<USP_Sel_OC_OpLineaResult>();
            List<USP_Sel_OC_OpParcialResult> lst_OcOpParcialLinea = new List<USP_Sel_OC_OpParcialResult>();
            lst_OcOpParcialLinea = JsonHelper.JsonDeserialize<List<USP_Sel_OC_OpParcialResult>>((string)Session["lstparcial"]);
            lst_OcOpLinea = JsonHelper.JsonDeserialize<List<USP_Sel_OC_OpLineaResult>>((string)Session["lstoc"]);

            if (lst_OcOpParcialLinea.Any())
            {
                gvwparcial.DataSource = lst_OcOpParcialLinea.Where(x => x.NroParcial == Convert.ToInt32(cbonroparcial.Text.ToString() == string.Empty ? "0" : cbonroparcial.Text.ToString())).ToList();
                gvwparcial.DataBind();
            }
            CargarCantidadDisponible();
        }
    }
}