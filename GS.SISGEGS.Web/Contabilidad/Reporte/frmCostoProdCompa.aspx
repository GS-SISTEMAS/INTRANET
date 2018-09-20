<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmCostoProdCompa.aspx.cs" Inherits="GS.SISGEGS.Web.Contabilidad.Reporte.frmCostoProdCompa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Costos de Produccion Por Producto
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
     <script>
         function requestStart(sender, args) {
             if (args.get_eventTarget().indexOf("btnExcel") >= 0)
                 args.set_enableAjax(false);
         }
    </script>
    
    

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpReporte" ControlID="rapReporte"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadWindowManager ID="rwmReporte" runat="server"></telerik:RadWindowManager>

    <telerik:RadAjaxLoadingPanel ID="ralpReporte" runat="server"></telerik:RadAjaxLoadingPanel>
    
    <telerik:RadAjaxPanel ID="rapReporte" runat="server" Height="90%" Width="100%" ClientEvents-OnRequestStart="requestStart">
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Height="100%" Width="100%">
            <Rows>
                <telerik:LayoutRow>
                    <Content>
                        <div class="col-md-11">
                            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Costo Comparativo mensual"></asp:Label>
                        </div> 
                        <div class="col-md-1">
                            <telerik:RadButton ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click">
                                <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png"/>
                            </telerik:RadButton>
                        </div>   
                        <div class="col-md-1">
                            <telerik:RadButton ID="btnGraph" runat="server" Text="Grafico" OnClick="btnGraph_Click">
                                <Icon PrimaryIconUrl="../../Images/Icons/analytics-16.png"/>
                            </telerik:RadButton>
                        </div>   
                        <div class="col-md-1">
                            <telerik:RadButton ID="btnBack" runat="server" Text="Regresar" OnClick="btnBack_Click" Visible="False">
                                <Icon PrimaryIconUrl="../../Images/Icons/arrowLeft-16.png"/>
                            </telerik:RadButton>
                        </div>                                          
                    </Content>
                </telerik:LayoutRow>
                <telerik:LayoutRow Height="95%" ID="PanelGrid" runat="server">
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
                                                <div class="colum4">
                                                    <asp:Label ID="lblFechaVariacion" runat="server" Text="Mes Evaluado" CssClass="etiqueta"></asp:Label>
                                                </div>
                                                <div class="colum6">
                                                    <telerik:RadMonthYearPicker ID="dpFecVariacion" runat="server" DateInput-DateFormat="MM-yyyy" Width="100%">
                                                    </telerik:RadMonthYearPicker>
                                                </div>
                                            </div>
                                            <div class="fila">
                                                <div class="colum4">
                                                    <asp:Label ID="lblMoneda" runat="server" Text="Moneda" CssClass="etiqueta"></asp:Label>
                                                </div>
                                                <div class="colum6">
                                                
                                                    <asp:DropDownList  ID="ddlMoneda" runat="server" Font-Size="8pt" Width="100px">
                                                        <asp:ListItem Value="0" Selected="True"> Soles</asp:ListItem>
                                                        <asp:ListItem Value="1"> Dolares</asp:ListItem>
                                                    </asp:DropDownList>
                                                
                                               </div>
                                            </div>
                                            <div class="fila">
                                                <div class="colum4">
                                                    <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click">
                                                        <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                                                    </telerik:RadButton>
                                                </div>
                                            </div>
                                        </telerik:RadSlidingPane>
                                    </telerik:RadSlidingZone>
                                </telerik:RadPane>
                                <telerik:RadPane ID="RadPane2" runat="server" Width="100%" Scrolling="None" Height="100%">
                                    <telerik:RadGrid ID="grdDocumentos01" runat="server" Height="100%" Width="100%" OnNeedDataSource="grdDocumentos01_NeedDataSource"
                                        AutoGenerateColumns="true">
                                        <ExportSettings Excel-Format="Html" OpenInNewWindow="true"/>
                                        <ClientSettings>
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true"/>
                                            <Selecting AllowRowSelect="true"/>
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                    
                                </telerik:RadPane>
                                
                            </telerik:RadSplitter>
                        </telerik:LayoutColumn>
                    </Columns>
                </telerik:LayoutRow>
                <telerik:LayoutRow Height="95%" id="PanelGraph" Visible="False" runat="server">
                    <Columns>
                        <telerik:LayoutColumn Span="12" Height="100%">
                            <telerik:RadSplitter RenderMode="Lightweight" ID="RadSplitter2" runat="server" Width="100%" Height="100%"
                                Orientation="Vertical">
                         <telerik:RadPane ID="RadPane3" runat="server" Width="100%" Scrolling="None" Height="100%">
                                    <telerik:RadHtmlChart runat="server" ID="RadHtmlChart1" Width="700px" Height="400px" Visible="False">
                                        <PlotArea>
                                                <Series>
                                                    <telerik:ColumnSeries  Name="Wooden Table" Stacked="false" Gap="1.5" Spacing="0.4" DataFieldY="costoUnitario">
                                                        <Appearance>
                                                            <FillStyle BackgroundColor="#d5a2bb"></FillStyle>
                                                        </Appearance>
                                                        <LabelsAppearance DataFormatString="{0}" Position="OutsideEnd"></LabelsAppearance>
                                                        <TooltipsAppearance DataFormatString="{0}" Color="White"></TooltipsAppearance>
                        
                                                    </telerik:ColumnSeries>
                    
                                                </Series>
                                                <Appearance>
                                                    <FillStyle BackgroundColor="Transparent"></FillStyle>
                                                </Appearance>
                                                <XAxis AxisCrossingValue="0" Color="black" MajorTickType="Outside" MinorTickType="Outside"
                                                    Reversed="false" DataLabelsField="mes">
                    
                                                    <LabelsAppearance DataFormatString="{0}" RotationAngle="0" Skip="0" Step="1"></LabelsAppearance>
                                                    <TitleAppearance Position="Center" RotationAngle="0" Text="Periodos">
                                                    </TitleAppearance>
                                                </XAxis>
                                                <YAxis AxisCrossingValue="0" Color="black" MajorTickSize="1" MajorTickType="Outside"
                                                    MinorTickType="None" Reversed="false">
                                                    <LabelsAppearance DataFormatString="{0}" RotationAngle="0" Skip="0" Step="1"></LabelsAppearance>
                                                    <TitleAppearance Position="Center" RotationAngle="0" Text="Costo Unitario">
                                                    </TitleAppearance>
                                                </YAxis>
                                            </PlotArea>
                                        <Legend>
                                            <Appearance Visible="false"></Appearance>
                                        </Legend>
                                        <ChartTitle Text="Fluctuacion por Costo Unitario por Kardex"></ChartTitle>
                                    </telerik:RadHtmlChart>
                              
                                </telerik:RadPane> 
                                </telerik:RadSplitter>  
                        </telerik:LayoutColumn>
                    
                    </Columns>
                    
                </telerik:LayoutRow>
               
            </Rows>
        
      
        </telerik:RadPageLayout>
        <br />
        <br />
        <br />
        <div class="col-md-12">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
        
    </telerik:RadAjaxPanel>

    
</asp:Content>

