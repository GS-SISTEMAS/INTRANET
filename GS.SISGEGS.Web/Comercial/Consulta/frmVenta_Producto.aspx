<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmVenta_Producto.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Consulta.frmVenta_Producto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">.
    Grupo Silvestre - Consultar productos vendidos
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
    <telerik:RadAjaxManager ID="ramProducto" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapProducto" LoadingPanelID="ralpProducto"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdProducto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapProducto" LoadingPanelID="ralpProducto"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpProducto" runat="server">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="rapProducto" runat="server" Width="100%" Height="95%" ClientEvents-OnRequestStart="requestStart">
        <telerik:RadPageLayout ID="RadPageLayout2" runat="server" Width="100%" Height="100%">
            <Rows>
                <telerik:LayoutRow>
                    <Columns>
                        <telerik:LayoutColumn Span="11">
                            <asp:Label ID="lblTitulo" runat="server" Text="Consultar productos vendidos" CssClass="titulo"></asp:Label>
                        </telerik:LayoutColumn>
                        <telerik:LayoutColumn Span="1">
                            <telerik:RadButton ID="btnExcel" runat="server" Text="Excel" AlternateText="ExcelML" OnClick="btnExcel_Click">
                                <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png"/>
                            </telerik:RadButton>
                        </telerik:LayoutColumn>
                    </Columns>
                </telerik:LayoutRow>
                <telerik:LayoutRow Height="100%">
                    <Content>
                        <telerik:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
                            <telerik:RadPane ID="RadPane1" runat="server" Width="22px" Scrolling="None">
                                <telerik:RadSlidingZone ID="RadSlidingZone1" runat="server" Width="22px">
                                    <telerik:RadSlidingPane ID="RadSlidingPane1" runat="server" Width="250px" Title="Filtros de Busqueda" EnableDock="false" 
                                        MinWidth="225" MinHeight="225" Scrolling="None">
                                        <div class="fila">
                                            <div class="colum3">
                                                <asp:Label ID="lblPeriodoInicio" runat="server" Text="Desde" CssClass="etiqueta"></asp:Label>
                                            </div>
                                            <div class="colum7">
                                                <telerik:RadMonthYearPicker ID="dpPeriodoInicio" runat="server" DateInput-DateFormat="MM-yyyy">
                                                </telerik:RadMonthYearPicker>
                                            </div>
                                        </div>
                                        <div class="fila">
                                            <div class="colum3">
                                                <asp:Label ID="lblPeriodoFinal" runat="server" Text="Hasta" CssClass="etiqueta"></asp:Label>
                                            </div>
                                            <div class="colum7">
                                                <telerik:RadMonthYearPicker ID="dpPeriodoFinal" runat="server" DateInput-DateFormat="MM-yyyy">
                                                </telerik:RadMonthYearPicker>
                                            </div>
                                        </div>
                                        <div class="fila">
                                            <div class="colum3">
                                                <asp:Label ID="lblCliente" runat="server" Text="Cliente" CssClass="etiqueta"></asp:Label>
                                            </div>
                                            <div class="colum7">
                                                <telerik:RadAutoCompleteBox ID="acbCliente" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text"
                                                    DropDownHeight="150px" EmptyMessage="Buscar cliente" AllowCustomEntry="true" DropDownWidth="350px">
                                                    <WebServiceSettings Method="Agenda_BuscarCliente" Path="frmVenta_Producto.aspx" />
                                                </telerik:RadAutoCompleteBox>
                                            </div>
                                        </div>
                                        <div class="fila">
                                            <div class="colum7">
                                                <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click">
                                                    <Icon PrimaryIconUrl="../../Images/Icons/search-16.png"/>
                                                </telerik:RadButton>
                                            </div>
                                        </div>
                                    </telerik:RadSlidingPane>
                                </telerik:RadSlidingZone>
                            </telerik:RadPane>
                            <telerik:RadPane ID="RadPane2" runat="server" Width="100%" Height="100%">
                                <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Height="100%">
                                    <Rows>
                                        <telerik:LayoutRow Height="100%">
                                            <Content>
                                                <telerik:RadGrid ID="grdProducto" runat="server" Width="100%" Height="100%" AutoGenerateColumns="false" 
                                                    OnNeedDataSource="grdProducto_NeedDataSource">
                                                    <GroupingSettings CaseSensitive="false"/>
                                                    <ExportSettings Excel-Format="ExcelML" ExportOnlyData="true" OpenInNewWindow="true"/>
                                                    <MasterTableView ShowFooter="true" AllowFilteringByColumn="true">
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="ZonaVendedor" HeaderText="Zona" UniqueName="ZonaVendedor" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterDelay="2000">
                                                                <HeaderStyle Width="100px"/>
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Vendedor" HeaderText="Vendedor" UniqueName="Vendedor" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterDelay="2000">
                                                                <HeaderStyle Width="100px"/>
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Cliente" HeaderText="Cliente" UniqueName="Cliente" AllowFiltering="false">
                                                                <HeaderStyle Width="100px"/>
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Marca" HeaderText="Marca" UniqueName="Marca" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterDelay="2000">
                                                                <HeaderStyle Width="100px"/>
                                                            </telerik:GridBoundColumn>
                                                            <%--<telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripcion" UniqueName="">
                                                                <HeaderStyle Width="100px"/>
                                                            </telerik:GridBoundColumn>--%>
                                                            <%--<telerik:GridBoundColumn DataField="Cantidad" HeaderText="Cantidad" UniqueName="Cantidad"  DataFormatString="{0:#,0}" Aggregate="Sum">
                                                                <HeaderStyle Width="100px"/>
                                                            </telerik:GridBoundColumn>--%>
                                                            <telerik:GridBoundColumn DataField="ValorVenta" HeaderText="ValorVenta" UniqueName="ValorVenta" DataFormatString="${0:#,0.00}" Aggregate="Sum" AllowFiltering="false">
                                                                <HeaderStyle Width="100px"/>
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="ValorKgLt" HeaderText="Valor/KgLt" UniqueName="ValorKgLt" DataFormatString="${0:#,0.00}" AllowFiltering="false">
                                                                <HeaderStyle Width="100px"/>
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="KgLt" HeaderText="Kg/Lt" UniqueName="KgLt"  DataFormatString="{0:#,0.00}" Aggregate="Sum" AllowFiltering="false">
                                                                <HeaderStyle Width="100px"/>
                                                            </telerik:GridBoundColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings>
                                                        <Scrolling UseStaticHeaders="true" AllowScroll="true"/>
                                                    </ClientSettings>
                                                </telerik:RadGrid>
                                            </Content>
                                        </telerik:LayoutRow>
                                    </Rows>
                                </telerik:RadPageLayout>
                            </telerik:RadPane>
                        </telerik:RadSplitter>
                    </Content>
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
    </div>
</asp:Content>
