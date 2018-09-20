<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmRendGastoIndEnvio.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Gastos.frmRendGastoIndEnvio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Reporte presupuesto por zona
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

             <telerik:AjaxSetting AjaxControlID="grdPresupuesto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPresupuesto" LoadingPanelID="ralpReporte"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadWindowManager ID="rwmReporte" runat="server"></telerik:RadWindowManager>

    <telerik:RadAjaxLoadingPanel ID="ralpReporte" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="rapReporte" runat="server" Height="100%" Width="100%" ClientEvents-OnRequestStart="requestStart">
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Height="100%" Width="100%">
            <Rows>
                <telerik:LayoutRow>
                    <Content>
                        <div class="col-md-11">
                            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Reporte de envio de planillas"></asp:Label>
                        </div> 
                        <div class="col-md-1">
                            <telerik:RadButton ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click">
                                <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png"/>
                            </telerik:RadButton>
                        </div>                                               
                    </Content>
                </telerik:LayoutRow>
                <telerik:LayoutRow Height="95%">
                    <Columns>
                        <telerik:LayoutColumn Span="12" Height="100%">
                            <telerik:RadSplitter RenderMode="Lightweight" ID="RadSplitter1" runat="server" Width="100%" Height="100%"
                                Orientation="Vertical">
                               <%-- <telerik:RadPane ID="RadPane1" runat="server" Width="22px" Scrolling="None">
                                    <telerik:RadSlidingZone ID="RadSlidingZone1" runat="server" Width="22px">
                                        <telerik:RadSlidingPane ID="RadSlidingPane1" runat="server" Width="350px" Title="Filtros de Busqueda"
                                            EnableDock="false" MinWidth="280" MinHeight="300" Scrolling="None">
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
                                                    <asp:Label ID="Label2" runat="server" Text="Zona" CssClass="etiqueta"></asp:Label>
                                                </div>
                                                <div class="colum6">
                                                       <telerik:RadComboBox ID="cboZona" runat="server" AutoPostBack="true" Width="100%" OnSelectedIndexChanged="cboZona_SelectedIndexChanged" Enabled="true" >
                                                       </telerik:RadComboBox>
                                                </div>
                                            </div>

                                            <div class="fila">
                                                <div class="colum4">
                                                    <asp:Label ID="Label1" runat="server" Text="Vendedor" CssClass="etiqueta"></asp:Label>
                                                </div>
                                                <div class="colum6">
                                                     <telerik:RadComboBox ID="cboVendedor" runat="server" AutoPostBack="true" Width="100%"   >
                                                     </telerik:RadComboBox>
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
                                </telerik:RadPane>--%>
                                <telerik:RadPane ID="RadPane2" runat="server" Width="100%" Scrolling="Both" Height="100%">

                                    <telerik:RadPivotGrid ID="grdIndicador" runat="server" Width="100%" Height="100%" AllowFiltering="false" ShowFilterHeaderZone="false"
                                        ShowDataHeaderZone="false" ShowRowHeaderZone="false" ShowColumnHeaderZone="false" EnableConfigurationPanel="true">
                                    

                                        <ExportSettings Excel-Format="Xlsx" OpenInNewWindow="true"></ExportSettings>

                                        <ClientSettings EnableFieldsDragDrop="false" >
                                            <Scrolling AllowVerticalScroll="true"></Scrolling>
                                        </ClientSettings>

                                        <Fields>
                                            <telerik:PivotGridRowField DataField="Colaborador"   ZoneIndex="1" SortOrder="None">
                                                <CellStyle Width="200px" />
                                            </telerik:PivotGridRowField>
                                            <telerik:PivotGridColumnField DataField="Semana">
                                            </telerik:PivotGridColumnField>
                                           <telerik:PivotGridAggregateField  DataField="Contador"   Aggregate="Sum" CellStyle-Width="80px">
                                                <HeaderCellTemplate >
                                                    Planillas Enviadas.
                                                </HeaderCellTemplate>
                                           </telerik:PivotGridAggregateField>
                                        </Fields>
                                           
                                    </telerik:RadPivotGrid>
                      
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
    <div class="col-md-12">
        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
    </div>
</asp:Content>
