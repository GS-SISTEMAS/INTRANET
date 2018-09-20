<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmFecCt_FecOp.aspx.cs" Inherits="GS.SISGEGS.Web.Contabilidad.Reporte.frmFecCt_FecOp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Fecha Contable vs Fecha Operativo
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
            <telerik:AjaxSetting AjaxControlID="">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="" ControlID=""/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadWindowManager ID="rwmReporte" runat="server"></telerik:RadWindowManager>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Height="100%" Width="100%" ClientEvents-OnRequestStart="requestStart">
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Height="100%" Width="100%">
            <Rows>
                <telerik:LayoutRow>
                    <Content>
                        <div class="col-md-11">
                            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Fecha contable vs fecha operación"></asp:Label>
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
                                                    <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click">
                                                        <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                                                    </telerik:RadButton>
                                                </div>
                                            </div>
                                        </telerik:RadSlidingPane>
                                    </telerik:RadSlidingZone>
                                </telerik:RadPane>
                                <telerik:RadPane ID="RadPane2" runat="server" Width="100%" Scrolling="None" Height="100%">
                                    <telerik:RadGrid ID="grdDocumentos" runat="server" Height="100%" Width="100%" OnNeedDataSource="grdDocumentos_NeedDataSource"
                                        AutoGenerateColumns="false">
                                        <ExportSettings Excel-Format="ExcelML" ExportOnlyData="true" OpenInNewWindow="true"/>
                                        <MasterTableView>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="ID_Voucher" HeaderText="Voucher" UniqueName="ID_Voucher">
                                                    <HeaderStyle Width="60px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="OrigenNombre" HeaderText="Tipo Doc." UniqueName="OrigenNombre">
                                                    <HeaderStyle Width="150px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Op" HeaderText="Op" UniqueName="Op">
                                                    <HeaderStyle Width="50px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Glosa" HeaderText="Glosa" UniqueName="Glosa">
                                                    <HeaderStyle Width="400px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Moneda" HeaderText="Moneda" UniqueName="Moneda">
                                                    <HeaderStyle Width="100px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FechaOperacion" HeaderText="Fec.Operación" UniqueName="FechaOperacion" DataFormatString="{0:dd/MM/yyyy}">
                                                    <HeaderStyle Width="100px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FechaConta" HeaderText="Fec.Contable" UniqueName="FechaConta" DataFormatString="{0:dd/MM/yyyy}">
                                                    <HeaderStyle Width="100px"/>
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
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
            </Rows>
        </telerik:RadPageLayout>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="col-md-12">
        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
    </div>
</asp:Content>
