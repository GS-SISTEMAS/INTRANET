<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmReporteVenta.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Reportes.ReporteVenta.frmReporteVenta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Reporte de ventas
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("ibExcel") >= 0)
                args.set_enableAjax(false);
        }

        function ShowReportbySalesman(ID_Vendedor, fechaInicial, fechaFinal) {
            window.radopen("frmReporteVenta_Clientes.aspx?ID_Vendedor=" + ID_Vendedor + "&fechaInicio=" + fechaInicial
                + "&fechaFinal=" + fechaFinal, "rwReporteVenta");
            return false;
        }

        $(document).ready(function () {
            var altura = $(document).height() - 132;
            $('#workplace').css("height", altura + "px");
        });

        $(window).resize(function () {
            var altura = $(document).height() - 132;
            $('#workplace').css("height", altura + "px");
        });

        function Resize()
        {
            var altura = $(document).height() - 142;
            $find("<%= ramReporteVenta.ClientID %>").ajaxRequest('ChangePageSize,' + altura);
            };
        window.onresize = window.onload = Resize;
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramReporteVenta" runat="server" OnAjaxRequest="ramReporteVenta_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlReporteVenta" LoadingPanelID="ralpReporteVenta"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ramReporteVenta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDocVenta" LoadingPanelID="ralpReporteVenta"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdDocVenta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlReporteVenta" LoadingPanelID="ralpReporteVenta"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpReporteVenta" runat="server">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmReporteVenta" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwReporteVenta" runat="server" Width="1030px" Height="575px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="pnlReporteVenta" runat="server" Width="100%" Height="100%" ClientEvents-OnRequestStart="requestStart">
        <div class="row">
            <div class="col-md-11">
                <asp:Label ID="lblTitulo" runat="server" Text="Reporte de Ventas" CssClass="titulo"></asp:Label>
            </div>
            <div class="col-md-1" style="text-align:right">
                <asp:ImageButton ID="ibExcel" runat="server" Height="20px" Width="20px" ImageUrl="~/Images/Icons/24_excel.png" OnClick="ibExcel_Click" 
                    AlternateText="ExcelML" ToolTip="Descargar Excel" />
            </div>
        </div>
        <div class="row">
            <div id="workplace" class="col-md-12">
                <telerik:RadSplitter RenderMode="Lightweight" ID="RadSplitter1" runat="server" Width="100%" Height="100%"
                    Orientation="Vertical">
                    <telerik:RadPane ID="RadPane1" runat="server" Width="22px" Scrolling="None">
                        <telerik:RadSlidingZone ID="RadSlidingZone1" runat="server" Width="22px">
                            <telerik:RadSlidingPane ID="RadSlidingPane1" runat="server" Width="250px" Title="Filtros de Busqueda"
                                EnableDock="false" MinWidth="225" MinHeight="225" Scrolling="None">
                                <div class="fila">
                                    <div class="colum5">
                                        <asp:Label ID="lblPeriodo" runat="server" Text="Periodo" CssClass="etiqueta"></asp:Label>
                                    </div>
                                    <div class="colum5">
                                        <telerik:RadMonthYearPicker ID="rmyReporte" runat="server" Width="100%">
                                            <DateInput runat="server" DateFormat="MM-yyyy"></DateInput>
                                        </telerik:RadMonthYearPicker>
                                    </div>
                                </div>
                                <div class="fila">
                                    <div class="colum6">
                                        <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click">
                                            <Icon PrimaryIconUrl="../../../Images/Icons/search-16.png"/>
                                        </telerik:RadButton>
                                    </div>
                                </div>
                            </telerik:RadSlidingPane>
                        </telerik:RadSlidingZone>
                    </telerik:RadPane>
                    <%--<telerik:RadSplitBar ID="RadSplitBar2" runat="server">
            </telerik:RadSplitBar>--%>
                    <telerik:RadPane ID="RadPane2" runat="server" Width="100%" Scrolling="None" Height="100%">
                        <div class="row">
                            <div class="col-md-12">
                                <telerik:RadGrid ID="grdDocVenta" runat="server" AutoGenerateColumns="false" Width="100%" Height="500px" 
                                    OnNeedDataSource="grdDocVenta_NeedDataSource" OnItemCommand="grdDocVenta_ItemCommand" ShowFooter="true" 
                                    OnItemDataBound="grdDocVenta_ItemDataBound">
                                    <ExportSettings Excel-Format="ExcelML" ExportOnlyData="true" OpenInNewWindow="true"/>
                                    <MasterTableView Height="100%" TableLayout="Fixed">
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderText="Zona" DataField="Zona">
                                                <HeaderStyle Width="80px"/>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Nombre vendedor" DataField="Vendedor">
                                                <HeaderStyle Width="80px"/>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Valor Venta" DataField="ValorVenta" DataFormatString="${0:#,0}" Aggregate="Sum">
                                                <HeaderStyle Width="80px"/>
                                                <FooterStyle Font-Bold="true"/>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Presupuesto" DataField="ValorPlanificado" DataFormatString="${0:#,0}" Aggregate="Sum">
                                                <HeaderStyle Width="80px"/>
                                                <FooterStyle Font-Bold="true"/>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Diferencia" DataField="Diferencia" DataFormatString="${0:#,0}" Aggregate="Sum">
                                                <HeaderStyle Width="80px"/>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="CostoVenta" DataField="CostoVenta" DataFormatString="${0:#,0}" Aggregate="Sum">
                                                <HeaderStyle Width="80px"/>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Avance" DataField="Avance" DataFormatString="{0:F0}%">
                                                <HeaderStyle Width="80px"/>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Rentab." DataField="Rentabilidad" DataFormatString="{0:F0}%">
                                                <HeaderStyle Width="80px"/>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Graf.">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibKPIs" runat="server" CommandArgument='<%# Eval("ID_Vendedor") %>' 
                                                        CommandName="Grafico" ImageUrl="~/Images/Icons/analytics-16.png"/>
                                                </ItemTemplate>
                                                <HeaderStyle Width="40px"/>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true"/>
                                        <Selecting AllowRowSelect="true"/>
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </div>
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            <asp:HiddenField ID="lblDatos"  runat="server" />
        </div>
    </div>
</asp:Content>
