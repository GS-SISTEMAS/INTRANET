using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Telerik.Web.UI;
using System.Web.Services;
using GS.SISGEGS.Web.Helpers;
using GS.SISGEGS.Web.EmpresaWCF;
using GS.SISGEGS.Web.LetrasEmitidasWCF;
using GS.SISGEGS.Web.PerfilWCF;
using GS.SISGEGS.Web.LoginWCF;
using GS.SISGEGS.DM;
using System.Data.Sql;
using System.Data.OleDb;
using System.Data;
using System.Drawing;

namespace GS.SISGEGS.Web.Contabilidad.Informacion
{
    public partial class frmSeguimientoAvanceLetras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Letra_Cargar();
            ReporteLetra_Cargar();
        }
        public void Letra_Cargar()
        {
            if (Session["Usuario"] == null)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "mykey", "CancelEdit();", true);
            }
            else
            {

                LetrasEmitidasWCFClient objLetrasWCF = new LetrasEmitidasWCFClient();
                try
                {

                    List<USP_SEL_Porcentaje_Avance_Letras_ZonasResult> listLetras = objLetrasWCF.Porcentaje_Avance_Letras_Lista_x_Zonas(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList();

                    grdLetra.DataSource = listLetras;
                    grdLetra.DataBind();
                    Session["LstLetras"] = JsonHelper.JsonSerializer(listLetras);

                    List<USP_SEL_Porcentaje_Avance_LetrasResult> listLetras_total = objLetrasWCF.Porcentaje_Avance_Letras_Lista(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList();

                    Label1.Text = "Total Porcentaje Letras Electronicas " + listLetras_total[0].PORCENTAJE.ToString() + "%";
                    Label3.Text = "Total Porcentaje Letras Manuales " + listLetras_total[0].PORCENTAJE_MANUAL.ToString() + "%";

                    Label1.Font.Size = 12;
                    Label3.Font.Size = 12;
                    
                    

                    //Create new RadialGauge object
                    RadRadialGauge radialGauge = new RadRadialGauge();
                    radialGauge.ID = "radialGauge2";
                    radialGauge.Width = 350;
                    radialGauge.Height = 350;

                    //Set Pointer properties
                    radialGauge.Pointer.Value = Convert.ToDecimal(listLetras_total[0].PORCENTAJE.ToString());
                    radialGauge.Pointer.Cap.Size = (float)0.10;
                    //Set Min and Max values of the Scale
                    radialGauge.Scale.Min = 0;

                    //In order the Max value to be displayed it should be multiple of the MajorUnit      
                    radialGauge.Scale.Max = 100;
                    radialGauge.Scale.MajorUnit = 20;

                    //Set Scale Labels properties
                    radialGauge.Scale.Labels.Visible = true;
                    radialGauge.Scale.Labels.Font = "15px Arial,Helvetica,sans-serif";
                    radialGauge.Scale.Labels.Color = System.Drawing.Color.Black;
                    radialGauge.Scale.Labels.Format = "{0} %";
                    radialGauge.Scale.Labels.Position = Telerik.Web.UI.Gauge.ScaleLabelsPosition.Outside;

                    //Create new GaugeRange object
                    GaugeRange gr1 = new GaugeRange();

                    //Set the properties of the new object
                    gr1.From = 20;
                    gr1.To = 40;
                    gr1.Color = System.Drawing.Color.FromArgb(141, 203, 42);

                    GaugeRange gr2 = new GaugeRange();
                    gr2.From = 40;
                    gr2.To = 60;
                    gr2.Color = System.Drawing.Color.FromArgb(255, 199, 0);

                    GaugeRange gr3 = new GaugeRange();
                    gr3.From = 60;
                    gr3.To = 80;
                    gr3.Color = System.Drawing.Color.FromArgb(255, 117, 26);

                    GaugeRange gr4 = new GaugeRange();
                    gr4.From = 80;
                    gr4.To = 100;
                    gr4.Color = System.Drawing.Color.FromArgb(230, 46, 0);

                    radialGauge.Scale.Ranges.Add(gr1);
                    radialGauge.Scale.Ranges.Add(gr2);
                    radialGauge.Scale.Ranges.Add(gr3);
                    radialGauge.Scale.Ranges.Add(gr4);
                    //RadAug_02.Controls.Add(radialGauge);
                    //Panel1.Controls.Add(radialGauge);





                    //Create new RadialGauge object
                    RadRadialGauge radialGauge2 = new RadRadialGauge();
                    radialGauge2.ID = "radialGauge22";
                    radialGauge2.Width = 350;
                    radialGauge2.Height = 350;

                    //Set Pointer properties
                    radialGauge2.Pointer.Value = Convert.ToDecimal(listLetras_total[0].PORCENTAJE_MANUAL.ToString());
                    radialGauge2.Pointer.Cap.Size = (float)0.10;
                    //Set Min and Max values of the Scale
                    radialGauge2.Scale.Min = 0;

                    //In order the Max value to be displayed it should be multiple of the MajorUnit      
                    radialGauge2.Scale.Max = 100;
                    radialGauge2.Scale.MajorUnit = 20;

                    //Set Scale Labels properties
                    radialGauge2.Scale.Labels.Visible = true;
                    radialGauge2.Scale.Labels.Font = "15px Arial,Helvetica,sans-serif";
                    radialGauge2.Scale.Labels.Color = System.Drawing.Color.Black;
                    radialGauge2.Scale.Labels.Format = "{0} %";
                    radialGauge2.Scale.Labels.Position = Telerik.Web.UI.Gauge.ScaleLabelsPosition.Outside;


                    radialGauge2.Scale.Ranges.Add(gr1);
                    radialGauge2.Scale.Ranges.Add(gr2);
                    radialGauge2.Scale.Ranges.Add(gr3);
                    radialGauge2.Scale.Ranges.Add(gr4);
                    RadAug_02.Controls.Add(radialGauge);
                    Panel1.Controls.Add(radialGauge2);




                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        protected void grdLetras_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Security/frmCerrar.aspx");

            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;

                /*
                 
                RadRadialGauge radialGauge = new RadRadialGauge();
                radialGauge.ID = "radialGauge1";
                radialGauge.Width = 200;
                radialGauge.Height = 200;
                radialGauge.Scale.EndAngle = 180;
                radialGauge.Scale.StartAngle = 90;



                //Set Pointer properties
                radialGauge.Pointer.Value = point;
                radialGauge.Pointer.Cap.Size = (float)0.10;
                radialGauge.Pointer.Cap.Color = System.Drawing.Color.FromArgb(255, 51, 51);
                radialGauge.Pointer.Color = System.Drawing.Color.FromArgb(255, 51, 51);

                //Set Min and Max values of the Scale
                radialGauge.Scale.Min = 0;

                //In order the Max value to be displayed it should be multiple of the MajorUnit      
                radialGauge.Scale.Max = 100;
                //radialGauge.Scale.MinorUnit = 2;
                radialGauge.Scale.MajorUnit = 20;

                //Set Minor and Major Ticks properties
                //radialGauge.Scale.MinorTicks.Visible = false;
                //radialGauge.Scale.MajorTicks.Size = 2;

                //Set Scale Labels properties
                radialGauge.Scale.Labels.Visible = true;
                radialGauge.Scale.Labels.Font = "15px Arial,Helvetica,sans-serif";
                radialGauge.Scale.Labels.Color = System.Drawing.Color.Black;
                radialGauge.Scale.Labels.Format = "{0} %";
                radialGauge.Scale.Labels.Position = Telerik.Web.UI.Gauge.ScaleLabelsPosition.Outside;

                //Create new GaugeRange object
                GaugeRange gr1 = new GaugeRange();

                //Set the properties of the new object
                gr1.From = 0;
                gr1.To = point;
                gr1.Color = System.Drawing.Color.FromArgb(83, 198, 140);

                //GaugeRange gr2 = new GaugeRange();
                //gr2.From = 40;
                //gr2.To = 80;
                //gr2.Color = System.Drawing.Color.Yellow;

                //GaugeRange gr3 = new GaugeRange();
                //gr3.From = 80;
                //gr3.To = 100;
                //gr3.Color = System.Drawing.Color.FromArgb(225, 0, 0);

                //Add Gauge objects to the RadialGauge
                radialGauge.Scale.Ranges.Add(gr1);
                //radialGauge.Scale.Ranges.Add(gr2);
                //radialGauge.Scale.Ranges.Add(gr3);

    */

                //((System.Web.UI.WebControls.Panel)e.Item.FindControl("RadAug")).Controls.Add(radLinearGauge1);


            }



        }





        private void ReporteLetra_Cargar()
        {
            LetrasEmitidasWCFClient objLetrasWCF = new LetrasEmitidasWCFClient();
            int count = 0;
            rhcProducto.PlotArea.Series[0].Items.Clear();
            rhcProducto.PlotArea.Series[1].Items.Clear();
            rhcProducto.PlotArea.XAxis.Items.Clear();


            try
            {
                List<USP_SEL_Porcentaje_Avance_Letras_ZonasResult> lstLetra = objLetrasWCF.Porcentaje_Avance_Letras_Lista_x_Zonas(((Usuario_LoginResult)Session["Usuario"]).idEmpresa,
                    ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList();

                List<USP_SEL_Porcentaje_Avance_LetrasResult> listLetras_total = objLetrasWCF.Porcentaje_Avance_Letras_Lista(((Usuario_LoginResult)Session["Usuario"]).idEmpresa, ((Usuario_LoginResult)Session["Usuario"]).codigoUsuario).ToList();


                foreach (USP_SEL_Porcentaje_Avance_Letras_ZonasResult Letra in lstLetra)
                {
                    rhcProducto.PlotArea.XAxis.Items.Add(Letra.Nombre.Replace("'", string.Empty));
                    SeriesItem item = new SeriesItem();
                    //item = new SeriesItem();
                    //item.Name = Letra.Nombre.Replace("'", string.Empty);
                    //item.YValue = Letra.PORCENTAJE_ZONA;
                    //this.rhcProducto.PlotArea.Series[0].Items.Add(item);

                    item = new SeriesItem();
                    item.Name = Letra.Nombre.Replace("'", string.Empty);
                    item.YValue = Letra.PORCENTAJE;
                    this.rhcProducto.PlotArea.YAxis.MaxValue = Convert.ToDecimal(listLetras_total[0].PORCENTAJE.ToString());
                    this.rhcProducto.PlotArea.Series[0].Items.Add(item);


                    item = new SeriesItem();
                    item.Name = Letra.Nombre.Replace("'", string.Empty);
                    item.YValue = Letra.PORCENTAJE_MANUAL;
                    this.rhcProducto.PlotArea.YAxis.MaxValue = Convert.ToDecimal(listLetras_total[0].PORCENTAJE.ToString());
                    this.rhcProducto.PlotArea.Series[1].Items.Add(item);



                    count++;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}