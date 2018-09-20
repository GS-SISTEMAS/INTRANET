<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPorcentajeAvanceLetras.aspx.cs" Inherits="GS.SISGEGS.Web.Contabilidad.Informacion.frmSeguimientoAvanceLetras" %>

 
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre:  Porcentaje Avance Letras
    
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <link type="text/css" href="https://intranet.gruposilvestre.com.pe/resources/css/toastr.min.css" rel="stylesheet" />
    
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/common/jquery.js"></script>
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/common/toastr.min.js"></script>
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/init.js"></script>
     


    <script type="text/javascript">
        
           
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramUsuario" runat="server"  >
       <AjaxSettings>
           <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapLetras" LoadingPanelID="ralpUsuario"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>


       </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpUsuario" runat="server">
    </telerik:RadAjaxLoadingPanel>
    
  

    <telerik:RadWindowManager ID="rwmUsuario" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwUsuario" runat="server" Width="410px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>


    <telerik:RadAjaxPanel ID="rapLetras" runat="server" Width="100%" Height="100%" ClientEvents-OnRequestStart="requestStart">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="Label2" runat="server" Text="Porcentaje de Avance de Letras" CssClass="titulo"></asp:Label>
            </div>
        </div>
     
            <div class="row">
                <div class="col-md-12">
                    <br />
                    </div>
             </div>

        <div class="row">
            <%--<div class="col-md-5">                   
                <telerik:RadAjaxPanel runat="server" ID="WeatherPanel" LoadingPanelID="WeatherLoadingPanel">
                    <div class="weatherBackgroundHolder" runat="server" id="weatherBackground">
                        <div class="weatherStationHolder">
                        <div class="qsf-ib barometerHolder">
                            <telerik:RadRadialGauge runat="server" ID="BarometerGauge" Width="480px" Height="380px" Style="z-index: 1000;" >
                                <Pointer Value="40" Color="#146794">
                                </Pointer>
                                <Scale Min="0" Max="100" MajorUnit="10" MinorUnit="5" StartAngle="-45" EndAngle="225">
                                    <MajorTicks Size="20" Color="#333399" />
                                    <MinorTicks Size="10" Color="#ff6600" />
                                    <Labels Color="#00111a" Font="bold 14px Arial,Verdana,Tahoma" />
                                </Scale>
                            </telerik:RadRadialGauge>
                        </div>
                        </div>
                    </div> 
                </telerik:RadAjaxPanel>
            </div>
             --%>

         <div class="col-md-4">
                <div class="demo-container size-thin"  >

                     <asp:panel ID="RadAug_02" runat="server" CssClass="text-center;">

                      </asp:panel>
                    <%--<telerik:RadRadialGauge runat="server" ID="RadRadialGauge1" Width="450px" Height="450px">
                        <Pointer Value="2.2">
                            <Cap Size="0.1" />
                        </Pointer>
                        <Scale Min="0" Max="100" MajorUnit="20">
                            <Labels Format="{0} %" />
                            <Ranges>
                                <telerik:GaugeRange Color="#8dcb2a" From="20" To="40" />
                                <telerik:GaugeRange Color="#ffc700" From="40" To="60" />
                                <telerik:GaugeRange Color="#ff7a00" From="60" To="80" />
                                <telerik:GaugeRange Color="#c20000" From="80" To="100" />
                            </Ranges>
                        </Scale>
                    </telerik:RadRadialGauge>--%>
                    <asp:Label ID="Label1" runat="server" Text="&nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Total Porcentaje Avanzado" CssClass="titulo"  ></asp:Label>
                </div>
                
                 
            </div>
             <%--
            <div class="col-md-2">
                <div class="demo-container size-thin">
                    <telerik:RadRadialGauge runat="server" ID="RadRadialGauge2" Width="200px" Height="200px" Scale-EndAngle="180"  Scale-StartAngle ="90" >
                        <Pointer Value="20">
                            <Cap Size="0.1" />
                        </Pointer>
                        <Scale Min="0" Max="100" MajorUnit="20">
                            <Labels Format="{0} %" />
                            <Ranges>
                                <telerik:GaugeRange Color="#8dcb2a" From="0" To="40" />
                                 <telerik:GaugeRange Color="#ffc700" From="40" To="60" />
                                <telerik:GaugeRange Color="#ff7a00" From="60" To="80" />
                                <telerik:GaugeRange Color="#c20000" From="80" To="100" />
                            </Ranges>
                        </Scale>
                    </telerik:RadRadialGauge>
                </div>
            </div>
        --%>

            <%-- <div class="col-md-2">
                <div class="demo-container size-thin">
                            <telerik:RadRadialGauge runat="server" ID="RadRadialGauge3" Width="150px" Height="150px" Scale-EndAngle="180"  Font-Size="5" Scale-StartAngle ="90" >
                                <Pointer Value= "32"  >
                                    <Cap Size="0.1" />
                                </Pointer>
                                <Scale Min="0" Max="100" MajorUnit="20" >
                                    <Labels Template="#=value# points" />
                                    <Labels Format="{0} %" />
                                    <Ranges>
                                        <telerik:GaugeRange Color="#8dcb2a" From="0" To="40" />
                                         
                                    </Ranges>
                                </Scale>
                            </telerik:RadRadialGauge>
             
                       </div>
            </div>--%>

             <div class="col-md-4">
                  <telerik:LayoutColumn Span="6" Height="100%">
                                    <telerik:RadTabStrip runat="server" ID="stripPedido" MultiPageID="rmpRepCliente" SelectedIndex="0">
                                        <Tabs>
                                            <telerik:RadTab Text="Avance Por Zona"></telerik:RadTab>
                                            <telerik:RadTab Text="Cantidad Por Zona"></telerik:RadTab>
                                            <%--<telerik:RadTab Text="Zona"></telerik:RadTab>--%>
                                        </Tabs>
                                    </telerik:RadTabStrip>

                            <telerik:RadMultiPage runat="server" ID="rmpRepCliente" SelectedIndex="0" Height="95%" CssClass="col-md-12" BorderStyle="Solid" BorderWidth="1px">                                   
                                <telerik:RadPageView runat="server" ID="pageCliente" Width="100%" Height="450px">
                                            <telerik:RadHtmlChart ID="rhcProducto" runat="server" Width="100%" Height="100%">
                                                <Legend>
                                                    <Appearance Position="Top" />
                                                </Legend>
                                                <PlotArea>
                                                    <Appearance>
                                                    </Appearance>
                                                    <YAxis>
                                                        <MajorGridLines Visible="true"></MajorGridLines>
                                                        <MinorGridLines Visible="false"></MinorGridLines>
                                                        <LabelsAppearance DataFormatString="{0} %">
                                                        </LabelsAppearance>
                                                    </YAxis>
                                                    <XAxis>
                                                        <LabelsAppearance DataFormatString="{0}"></LabelsAppearance>
                                                        <MajorGridLines Visible="false" />
                                                        <MinorGridLines Visible="false" />
                                                    </XAxis>
                                                    <Series>
                                                        <%--<telerik:BarSeries Name="Avance por Zona">
                                                            <Appearance>
                                                                <FillStyle BackgroundColor="#b1c85a"></FillStyle>
                                                            </Appearance>
                                                            <LabelsAppearance DataFormatString="{0} %">
                                                            </LabelsAppearance>
                                                            <TooltipsAppearance BackgroundColor="#b1c85a" ClientTemplate='Zona.: #=category#<br/>Avance de Zona al: #= kendo.toString(value)#'>
                                                            </TooltipsAppearance>
                                                        </telerik:BarSeries>--%>

                                                        <telerik:BarSeries Name="Avance Por Letra Electrónica">
                                                            <Appearance>
                                                                <FillStyle BackgroundColor="#93d6d8"></FillStyle>
                                                            </Appearance>
                                                            <LabelsAppearance DataFormatString="{0} %">
                                                            </LabelsAppearance>
                                                            <TooltipsAppearance BackgroundColor="#93d6d8" ClientTemplate='Letra Electrónica.: #=category#<br/>Porcentaje: #= kendo.toString(value)#'>
                                                            </TooltipsAppearance>
                                                        </telerik:BarSeries>

                                                        <telerik:BarSeries Name="Avance Por Letra Manual">
                                                            <Appearance>
                                                                <FillStyle BackgroundColor="#b1c85a"></FillStyle>
                                                            </Appearance>
                                                            <LabelsAppearance DataFormatString="{0} %">
                                                            </LabelsAppearance>
                                                            <TooltipsAppearance BackgroundColor="#b1c85a" ClientTemplate='Letra Manual.: #=category#<br/>Porcentaje: #= kendo.toString(value)#'>
                                                            </TooltipsAppearance>
                                                        </telerik:BarSeries>

                                                    </Series>
                                                </PlotArea>
                                            </telerik:RadHtmlChart>
                                        </telerik:RadPageView>


                            <telerik:RadPageView runat="server" ID="pageProducto" Width="100%" Height="450px">

                                <telerik:RadGrid ID="grdLetra" runat="server" AutoGenerateColumns="false"  Width="100%" Height="450px" ShowFooter="false" AllowMultiRowSelection="false"  OnItemDataBound="grdLetras_ItemDataBound" >
                                    <ClientSettings>
                                        <Scrolling   AllowScroll="true" ScrollHeight="500" ></Scrolling>
                                    </ClientSettings>
                                    <MasterTableView TableLayout="Fixed"  Height="500" >
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderText="Zona" DataField="Nombre" FooterStyle-Font-Bold="true" >
                                                <HeaderStyle Width="180px"/>

                                            </telerik:GridBoundColumn>
                          
                                             <telerik:GridBoundColumn HeaderText="Cantidad"   DataField="C_Total" >
                                                <HeaderStyle Width="100px"/>
                                                <FooterStyle Font-Bold="true"/>
                                            </telerik:GridBoundColumn>
                             
                                      <%--  <telerik:GridTemplateColumn HeaderText="Avance">
                                            <ItemTemplate>
                                            <telerik:RadRadialGauge runat="server" ID="RadRadialGauge3" Width="150px" Height="150px" Scale-EndAngle="180"  Font-Size="5" Scale-StartAngle ="90" >
                                                <Pointer Value= "32"  >
                                                    <Cap Size="0.1" />
                                                </Pointer>
                                                <Scale Min="0" Max="100" MajorUnit="20" >
                                                    <Labels Template="#=value# points" />
                                                    <Labels Format="{0} %" />
                                                    <Ranges>
                                                        <telerik:GaugeRange Color="#8dcb2a" From="0" To="40" />
                                         
                                                    </Ranges>
                                                </Scale>
                                            </telerik:RadRadialGauge>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>--%>

                             <%--               <telerik:GridTemplateColumn HeaderText="Avance">
                                            <ItemTemplate>
                                                <asp:panel ID="RadAug" runat="server">

                                                </asp:panel>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                             --%>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </telerik:RadPageView>
                        </telerik:RadMultiPage>
                    </telerik:LayoutColumn>
                 </div>


                <div class="col-md-4">
                <div class="demo-container size-thin"  >

                     <asp:panel ID="Panel1" runat="server">

                         </asp:panel>
                    <asp:Label ID="Label3" runat="server" Text="&nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Total Porcentaje Avanzado" CssClass="titulo"  ></asp:Label>
                </div>
                
                 
            </div>
                
            </div>
  
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">

</asp:Content>
