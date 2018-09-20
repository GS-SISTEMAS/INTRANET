<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmReporteVenta_Variacion.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Reportes.ReporteBI.frmReporteVenta_Variacion" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="tk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre - Reporte de vendedor por periodo
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
 
  <style type="text/css">

         .paddingR{
         padding:1px 3px 1px 3px;
	}    
         .fontHeders{
         font-size: 9pt; font-weight:bold;
	}    .fontHeders2{
         text-align:center; font-size: 8pt; font-weight:bold;
	}    .fontHeders3{
         font-size: 7pt; font-weight:bold;
	}    .fontAlignR {
         text-align:right;
	}    .fontAlignC {
         text-align:center;
	}    .fontAlignL {
         text-align:left;
	}    .borderR {
         border:1px solid #000000;
	}    .BackColor1 {
         background-color:#F9CFAB;
	}    .BackColor2 {
         background-color:#C6C8FC;
	}    .BackColor3 {
         background-color:#DCB4F0;
	}    .BackColor4  {
         background-color:#A3FF5B;
	}    .BackColor5  {
         background-color:#3FE276;
	}    .BackColor6  {
         background-color:#F3FF5B;
	}    .BackColorRojo {
         background-color:#FF0000;
	}    .fontBlanco {
         color:#FFFFFF;
	}

.tableTB {
  table-layout: fixed; 
  width: 70%;
  *margin-left: -400px;  
}
 
.fix {
  
  position: absolute;
  *position: relative;  
  margin-left: -400px;
  width: 150px;
}

.fix2 {
  position: absolute;
  *position: relative;  
  margin-left: -250px;
  width: 250px;
}

.fix3 {
  position: absolute;
  *position: relative;  
  margin-left: -400px;
  width: 400px;
}

.outerTB {
  position: relative;
}

.innerTB {
  overflow-x: scroll;
  overflow-y: visible;
  width: 67%; 
  margin-left: 400px;
}


.borderTOP 
{  border-top:1px solid #000000; }
.borderBOTT 
{  border-bottom:1px solid #000000; }
.borderLEFT 
{  border-left:1px solid #000000; }
.borderRIGHT
{  border-right:1px solid #000000; }

.EspacioH{
    height:30px;
}

.EspacioH2{
    height:75px;
}

.CenterTEXT {
    display: table-cell;
    vertical-align: middle;
    text-align: center;
}

  </style>

 <script >
 
        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("ibExcel") >= 0)
                args.set_enableAjax(false);
            if (args.get_eventTarget().indexOf("btnExcel") >= 0)
                args.set_enableAjax(false);
        }

</script>
  


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramVendedor" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapVendedor" LoadingPanelID="ralpVendedor"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>

             <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpVendedor" ControlID="rapVendedor"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="gsReporteVentas_Zonas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gsReporteVentas_Zonas" LoadingPanelID="ralpVendedor"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

     <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="false" />

    <telerik:RadAjaxLoadingPanel ID="ralpVendedor" runat="server">
    </telerik:RadAjaxLoadingPanel>

     <telerik:RadWindowManager ID="rwmReporte" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwReporte" runat="server" Width="560px" Height="560px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>
            <telerik:RadWindow ID="rwExportarPDF" runat="server" Width="560px" Height="560px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="rapVendedor" runat="server" Width="100%" Height="100%" ClientEvents-OnRequestStart="requestStart">
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Width="100%" Height="100%">
            <Rows>
                <telerik:LayoutRow>
                    <Columns>
                        <telerik:LayoutColumn Span="12">
                            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Reporte de Ventas - Variación por Zonas"></asp:Label>
                        </telerik:LayoutColumn>
                    </Columns>
                </telerik:LayoutRow>
                <telerik:LayoutRow Height="96%">
                    <Columns>
                        <telerik:LayoutColumn Span="12" Height="100%">
                            <telerik:RadSplitter RenderMode="Lightweight" ID="RadSplitter1" runat="server" Width="100%" Height="100%"
                                Orientation="Vertical">
                                <telerik:RadPane ID="RadPane1" runat="server" Width="22px" Scrolling="None">
                                    <telerik:RadSlidingZone ID="RadSlidingZone1" runat="server" Width="22px">
                                        <telerik:RadSlidingPane ID="RadSlidingPane1" runat="server" Width="250px" Title="Filtros de Busqueda"
                                            EnableDock="false" MinWidth="225" MinHeight="225" Scrolling="None">
                                            <div class="fila">
                                                <div class="colum4">
                                                    <asp:Label ID="lblFechaInicio" runat="server" Text="Periodo Inicio" CssClass="etiqueta"></asp:Label>
                                                </div>
                                                <div class="colum6">
                                                    <telerik:RadMonthYearPicker ID="dpFecInicio" runat="server" DateInput-DateFormat="MM-yyyy" Width="100%">
                                                    </telerik:RadMonthYearPicker>
                                                </div>
                                            </div>
                                            <div class="fila">
                                                <div class="colum4">
                                                    <asp:Label ID="lblFechaFinal" runat="server" Text="Periodo Final" CssClass="etiqueta"></asp:Label>
                                                </div>
                                                <div class="colum6">
                                                    <telerik:RadMonthYearPicker ID="dpFecFinal" runat="server" DateInput-DateFormat="MM-yyyy" Width="100%">
                                                    </telerik:RadMonthYearPicker>
                                                </div>
                                            </div>
                                            <div class="fila">
                                                <div class="colum5">

                                                     <telerik:RadButton RenderMode="Lightweight" ID="rbContraer" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Radio"
                                                        Text="Contraer" Checked="true" Skin="Metro">
                                                    </telerik:RadButton>
                                                </div>
                                                 <div class="colum5">
                                                     <telerik:RadButton RenderMode="Lightweight" ID="rbExpandir" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Radio"
                                                            Text="Expandir" Skin="Metro">
                                                        </telerik:RadButton>
                                                </div>
                                            </div>

                                            <div class="fila">
                                                <div class="colum4">
                                                    <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click">
                                                        <Icon PrimaryIconUrl="../../../Images/Icons/search-16.png" />
                                                    </telerik:RadButton>
                                                </div>
                                            </div>
                                        </telerik:RadSlidingPane>
                                    </telerik:RadSlidingZone>
                                </telerik:RadPane>
                                <telerik:RadPane ID="RadPane2" runat="server" Width="100%" Scrolling="None" Height="100%" >
                                          <div class="row">
                                              <div class="col-md-11">

                                              </div>
                                              <div class="col-md-1">
                                                       <telerik:RadButton ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click">
                                                          <Icon PrimaryIconUrl="../../../../Images/Icons/excel-16.png"/>
                                                       </telerik:RadButton>
                                              </div>
                                          </div>

  

                                        <div class="outerTB">
                                            <div class="innerTB">
                                                    <asp:Table ID="tblReporte"  runat="server"   BorderWidth="1" >
                                                    </asp:Table>
                                            </div>
                                        </div>


                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:LayoutColumn>
                    </Columns>
                </telerik:LayoutRow>
            </Rows>
        </telerik:RadPageLayout>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
          <div class="col-md-12">

            <asp:HiddenField ID="hfGridHtml" runat="server" />
            <asp:HiddenField ID="lblReporte" runat="server"  />
        </div>
    </div>
</asp:Content>
