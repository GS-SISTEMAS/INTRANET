<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmTransGratuitas.aspx.cs" Inherits="GS.SISGEGS.Web.Contabilidad.Reporte.frmTransGratuitas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Transferencias gratuitas
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
                            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Transferencias gratuitas"></asp:Label>
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
                                                     <telerik:RadDatePicker ID="dpFechaInicio" runat="server" DateInput-ReadOnly="true" Width="150px">
                                                            <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                                            </DateInput>
                                                        </telerik:RadDatePicker>
                                                </div>
                                            </div>
                                            <div class="fila">
                                                <div class="colum4">
                                                    <asp:Label ID="lblFechaFinal" runat="server" Text="Periodo Final" CssClass="etiqueta"></asp:Label>
                                                </div>
                                                <div class="colum6">
                                                   <telerik:RadDatePicker ID="dpFechaFin" runat="server" DateInput-ReadOnly="true" Width="150px">
                                                        <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                                        </DateInput>
                                                    </telerik:RadDatePicker>
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
                                    <telerik:RadGrid ID="grdDocumentos" runat="server" Height="100%" Width="100%" 
                                        OnNeedDataSource="grdDocumentos_NeedDataSource"
                                        AutoGenerateColumns="false"
                                        ShowFooter="true"
                                        >
                                        <ExportSettings Excel-Format="ExcelML" ExportOnlyData="true" OpenInNewWindow="true"/>
                                        <MasterTableView>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Op" HeaderText="Op" UniqueName="Op" Aggregate="Count">
                                                    <HeaderStyle Width="80px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Nombre_Documento" HeaderText="Documento" UniqueName="Nombre_Documento">
                                                    <HeaderStyle Width="100px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Transaccion" HeaderText="Transaccion" UniqueName="Transaccion">
                                                    <HeaderStyle Width="80px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Fecha" HeaderText="Fecha" UniqueName="Fecha" DataFormatString="{0:dd/MM/yyyy}">
                                                    <HeaderStyle Width="80px"/>
                                                </telerik:GridBoundColumn>

                                                   <telerik:GridBoundColumn DataField="Nombre_Zona" HeaderText="Zona" UniqueName="Nombre_Zona">
                                                    <HeaderStyle Width="180px"/>
                                                </telerik:GridBoundColumn>

                                                   <telerik:GridBoundColumn DataField="Nombre_Vendedor" HeaderText="Vendedor" UniqueName="Nombre_Vendedor">
                                                    <HeaderStyle Width="180px"/>
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="Nombre_Cliente" HeaderText="Cliente" UniqueName="Nombre_Cliente">
                                                    <HeaderStyle Width="180px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TablaOrigen" HeaderText="Tb.Origen" UniqueName="TablaOrigen" >
                                                    <HeaderStyle Width="80px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TransaccionOrigen" HeaderText="Trans.Origen" UniqueName="TransaccionOrigen">
                                                    <HeaderStyle Width="80px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Kardex" HeaderText="Kardex" UniqueName="Kardex">
                                                    <HeaderStyle Width="80px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ID_Item" HeaderText="ID_Item" UniqueName="ID_Item">
                                                    <HeaderStyle Width="100px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ItemNombre" HeaderText="ItemNombre" UniqueName="ItemNombre">
                                                    <HeaderStyle Width="150px"/>
                                                </telerik:GridBoundColumn>

                                                   <telerik:GridBoundColumn DataField="NombreFKNiv01" HeaderText="NombreFKNiv01" UniqueName="NombreFKNiv01">
                                                    <HeaderStyle Width="150px"/>
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="Cantidad" HeaderText="Cantidad" UniqueName="Cantidad" DataFormatString="{0:#,##0.00}"
                                                    Aggregate="Sum"  >
                                                    <HeaderStyle Width="80px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ValorVenta" HeaderText="Venta" UniqueName="ValorVenta" DataFormatString="{0:#,##0.00}"
                                                    Aggregate="Sum"    >
                                                    <HeaderStyle Width="80px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CostoVentaReal" HeaderText="Cos.Real.Tot" 
                                                    UniqueName="CostoVentaReal" DataFormatString="{0:#,##0.00}" Aggregate="Sum">
                                                    <HeaderStyle Width="80px"/>
                                                </telerik:GridBoundColumn>
                                      
                                                 <telerik:GridBoundColumn DataField="PrecioUnitario" HeaderText="PU" UniqueName="PrecioUnitario"
                                                     DataFormatString="{0:#,##0.00}"
                                                    Aggregate="Avg"    >
                                                    <HeaderStyle Width="80px"/>
                                                </telerik:GridBoundColumn>
                                                 <telerik:GridBoundColumn DataField="Costounitario" HeaderText="CU" UniqueName="Costounitario" DataFormatString="{0:#,##0.00}"
                                                   Aggregate="Avg"    >
                                                    <HeaderStyle Width="80px"/>
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
