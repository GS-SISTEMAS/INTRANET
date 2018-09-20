<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmReporteVenta_Clientes.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Reportes.ReporteVenta.frmReporteVenta_Clientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Reporte de ventas por cliente
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("ibExcel") >= 0)
                args.set_enableAjax(false);
        }

        function Resize()
        {
            var altura = $(document).height() - 142;
            $find("<%= ramRepCliente.ClientID %>").ajaxRequest('ChangePageSize,' + altura);
            };
        window.onresize = window.onload = Resize;
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramRepCliente" runat="server" OnAjaxRequest="ramRepCliente_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramRepCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlRepCliente" LoadingPanelID="ralpRepCliente"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpRepCliente" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="pnlRepCliente" runat="server" Width="100%" ClientEvents-OnRequestStart="requestStart" Height="100%">
        <telerik:RadPageLayout ID="RadPageLayout3" runat="server" Width="100%">
            <Rows>
                <telerik:LayoutRow>
                    <Columns>
                        <telerik:LayoutColumn Span="11" SpanMd="11" SpanXs="1">
                            <asp:Label ID="lblTitulo" runat="server" Text="Reporte de venta por cliente" CssClass="titulo"></asp:Label>
                        </telerik:LayoutColumn>
                        <telerik:LayoutColumn Span="1" SpanMd="1" SpanXs="1">
                            <%--<telerik:RadButton ID="btnVerTodo" runat="server" Text="VerTodo" OnClick="btnVerTodo_Click">
                            </telerik:RadButton>--%>
                        </telerik:LayoutColumn>
                    </Columns>
                </telerik:LayoutRow>
            </Rows>
        </telerik:RadPageLayout>
        <telerik:RadSplitter RenderMode="Lightweight" ID="RadSplitter1" runat="server" Width="100%" Height="96%" Orientation="Vertical">
            <telerik:RadPane ID="RadPane1" runat="server" Width="22px" Scrolling="None">
                <telerik:RadSlidingZone ID="RadSlidingZone1" runat="server" Width="22px">
                    <telerik:RadSlidingPane ID="RadSlidingPane1" runat="server" Width="250px" Title="Filtros de Busqueda"
                        EnableDock="false" MinWidth="225" MinHeight="225" Scrolling="None">
                        <telerik:RadPageLayout ID="RadPageLayout2" runat="server" Width="100%">
                            <Rows>
                                <telerik:LayoutRow>
                                    <Columns>
                                        <telerik:LayoutColumn Span="3">
                                            <asp:Label ID="lblPeriodo" runat="server" Text="Periodo" CssClass="etiqueta"></asp:Label>
                                        </telerik:LayoutColumn>
                                        <telerik:LayoutColumn Span="9">
                                            <telerik:RadMonthYearPicker ID="rmpPeriodo" runat="server" Width="100%">
                                                <DateInput runat="server" DateFormat="MM-yyyy"></DateInput>
                                            </telerik:RadMonthYearPicker>
                                        </telerik:LayoutColumn>
                                    </Columns>
                                </telerik:LayoutRow>
                                 <telerik:LayoutRow>
                                    <Columns>
                                        <telerik:LayoutColumn Span="3">
                                                <asp:Label ID="Label1" runat="server" Text="Estado crédito" CssClass="etiqueta"></asp:Label>
                                        </telerik:LayoutColumn>
                                        <telerik:LayoutColumn Span="9">
                                              <telerik:RadComboBox ID="cboEstadoCredito" runat="server" Width="120px"  >
                                                     </telerik:RadComboBox>
                                        </telerik:LayoutColumn>
                                    </Columns>
                                </telerik:LayoutRow>

                                <telerik:LayoutRow>
                                    <Columns>
                                        <telerik:LayoutColumn Span="6">
                                            <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" >
                                                <Icon PrimaryIconUrl="../../../Images/Icons/search-16.png"/>
                                            </telerik:RadButton>
                                        </telerik:LayoutColumn>
                                    </Columns>
                                </telerik:LayoutRow>
                            </Rows>
                        </telerik:RadPageLayout>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
            <telerik:RadPane ID="RadPane2" runat="server" Width="100%" Height="100%" Scrolling="None">
                <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Width="100%" Height="100%">
                    <Rows>
                        <telerik:LayoutRow Height="3%">
                            <Columns>
                                <telerik:LayoutColumn Span="11"></telerik:LayoutColumn>
                                <telerik:LayoutColumn Span="1">
                                    <telerik:RadButton ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click">
                                        <Icon PrimaryIconUrl="../../../Images/Icons/excel-16.png"/>
                                    </telerik:RadButton>
                                </telerik:LayoutColumn>
                            </Columns>
                        </telerik:LayoutRow>
                        <telerik:LayoutRow Height="96%">
                            <Columns>
                                <telerik:LayoutColumn Height="100%" Span="12">
                                    <telerik:RadGrid ID="grdCliente" runat="server" AutoGenerateColumns="false" Height="100%" Width="100%"
                                        ShowFooter="True">
                                        <MasterTableView>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="ID_Cliente" UniqueName="ID_Cliente" HeaderText="Código">
                                                    <HeaderStyle Width="" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Cliente" UniqueName="Cliente" HeaderText="Cliente">
                                                    <HeaderStyle Width="" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ValorVenta" UniqueName="ValorVenta" HeaderText="Valor Venta" DataFormatString="${0:F2}" Aggregate="Sum">
                                                    <HeaderStyle Width="" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ValorPlanificado" UniqueName="ValorPlanificado" HeaderText="Valor Planf." DataFormatString="${0:F2}" Aggregate="Sum">
                                                    <HeaderStyle Width="" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CostoVenta" UniqueName="CostoVenta" HeaderText="Costo Venta" DataFormatString="${0:F2}" Aggregate="Sum">
                                                    <HeaderStyle Width="" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Avance" UniqueName="Avance" HeaderText="Avance" DataFormatString="{0:F0}%" >
                                                    <HeaderStyle Width="" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Diferencia" UniqueName="Diferencia" HeaderText="Diferenc." DataFormatString="${0:F2}" Aggregate="Sum">
                                                    <HeaderStyle Width="" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Rentabilidad" UniqueName="Rentabilidad" HeaderText="Rentab." DataFormatString="{0:F0}%">
                                                    <HeaderStyle Width="" />
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings>
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true"/>
                                            <Selecting AllowRowSelect="True"></Selecting>
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                </telerik:LayoutColumn>
                            </Columns>
                        </telerik:LayoutRow>
                    </Rows>
                </telerik:RadPageLayout>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
