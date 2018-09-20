<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmCostoMensual.aspx.cs" Inherits="GS.SISGEGS.Web.Contabilidad.Reporte.frmCostoMensual" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Costos Mensuales
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

    <telerik:RadAjaxPanel ID="rapReporte" runat="server" Height="100%" Width="100%" ClientEvents-OnRequestStart="requestStart">
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Height="100%" Width="100%">
            <Rows>
                <telerik:LayoutRow>
                    <Content>
                        <div class="col-md-11">
                            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Costos mensuales"></asp:Label>
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
                                    <telerik:RadPivotGrid ID="grdDocumentos" runat="server" Width="100%" Height="100%" AllowFiltering="false" ShowFilterHeaderZone="false" OnPivotGridCellExporting="grdDocumentos_PivotGridCellExporting"
                                        ShowDataHeaderZone="false" ShowRowHeaderZone="false" ShowColumnHeaderZone="false" EnableConfigurationPanel="true" TotalsSettings-GrandTotalsVisibility="None" OnNeedDataSource="grdDocumentos_NeedDataSource">
                                        <ExportSettings Excel-Format="Xlsx" OpenInNewWindow="true"></ExportSettings>
                                        <ClientSettings EnableFieldsDragDrop="false">
                                            <Scrolling AllowVerticalScroll="true"></Scrolling>
                                        </ClientSettings>
                                        <Fields>
                                            <telerik:PivotGridRowField DataField="Marca" ZoneIndex="0">
                                                <CellStyle Width="250px" />
                                            </telerik:PivotGridRowField>
                                          <%--  <telerik:PivotGridRowField DataField="Kardex" ZoneIndex="1" >
                                                <CellStyle Width="50px" />
                                            </telerik:PivotGridRowField>--%>
                                            <telerik:PivotGridRowField DataField="Descripcion" ZoneIndex="1">
                                                <CellStyle Width="250px" />
                                            </telerik:PivotGridRowField>
                                            <telerik:PivotGridColumnField DataField="PeriodoReal">
                                            </telerik:PivotGridColumnField>
                                            <telerik:PivotGridAggregateField DataField="Cantidad" Aggregate="Sum" DataFormatString="{0:F0}">
                                                <HeaderCellTemplate>
                                                    Cantidad
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                            <telerik:PivotGridAggregateField DataField="ValorVenta" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Valor Venta
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                            <telerik:PivotGridAggregateField DataField="PrecioUnitario" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Precio Unit.
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                            <telerik:PivotGridAggregateField DataField="CostoVenta" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Costo Venta
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                            <telerik:PivotGridAggregateField DataField="CostoVentaReal" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    C.V.Unit.
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                            <telerik:PivotGridAggregateField DataField="CostoVentaEstandar" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    C.E.Unit.
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                            <telerik:PivotGridAggregateField DataField="CostoVentaCalculado" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    C.C.Unit.
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                        </Fields>
                                       <%-- <SortExpressions>
                                            <telerik:PivotGridSortExpression FieldName="ValorVenta" SortOrder="Descending"></telerik:PivotGridSortExpression>
                                        </SortExpressions>
                                        <TotalsSettings RowsSubTotalsPosition="None" />--%>
                                    </telerik:RadPivotGrid>
                                    <%--<telerik:RadGrid ID="grdDocumentos" runat="server" Height="100%" Width="100%" OnNeedDataSource="grdDocumentos_NeedDataSource"
                                        AutoGenerateColumns="false">
                                        <ExportSettings Excel-Format="ExcelML" ExportOnlyData="true" OpenInNewWindow="true"/>
                                        <MasterTableView>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Op" HeaderText="Op" UniqueName="Op">
                                                    <HeaderStyle Width="60px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Nombre" HeaderText="Nombre" UniqueName="Nombre">
                                                    <HeaderStyle Width="100px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Transaccion" HeaderText="Transaccion" UniqueName="Transaccion">
                                                    <HeaderStyle Width="80px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Fecha" HeaderText="Fecha" UniqueName="Fecha" DataFormatString="{0:dd/MM/yyyy}">
                                                    <HeaderStyle Width="80px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="AgendaNombre" HeaderText="AgendaNombre" UniqueName="AgendaNombre">
                                                    <HeaderStyle Width="180px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TablaOrigen" HeaderText="Tb.Origen" UniqueName="TablaOrigen" >
                                                    <HeaderStyle Width="80px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TransaccionOrigen" HeaderText="Trans.Origen" UniqueName="TransaccionOrigen">
                                                    <HeaderStyle Width="80px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Item_ID" HeaderText="Item_ID" UniqueName="Item_ID">
                                                    <HeaderStyle Width="80px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ID_Item" HeaderText="ID_Item" UniqueName="ID_Item">
                                                    <HeaderStyle Width="100px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ItemNombre" HeaderText="ItemNombre" UniqueName="ItemNombre">
                                                    <HeaderStyle Width="150px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Cantidad" HeaderText="Cantidad" UniqueName="Cantidad" DataFormatString="{0:F0}">
                                                    <HeaderStyle Width="80px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ValorVenta" HeaderText="Venta" UniqueName="ValorVenta" DataFormatString="{0:F2}">
                                                    <HeaderStyle Width="80px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CostoVentaReal" HeaderText="Cos.Real.Tot" UniqueName="CostoVentaReal" DataFormatString="{0:F2}">
                                                    <HeaderStyle Width="80px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CostoCalculadoUnitario" HeaderText="Cos.Cal.Uni" UniqueName="CostoCalculadoUnitario" DataFormatString="{0:F2}">
                                                    <HeaderStyle Width="80px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CostoVentaCalculado" HeaderText="Cos.Cal.Tot" UniqueName="CostoVentaCalculado" DataFormatString="{0:F2}">
                                                    <HeaderStyle Width="100px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CostoEstandarUnitario" HeaderText="Cos.Est.Uni" UniqueName="CostoEstandarUnitario" DataFormatString="{0:F2}">
                                                    <HeaderStyle Width="80px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CostoVentaEstandar" HeaderText="Cos.Est.Tot" UniqueName="CostoVentaEstandar" DataFormatString="{0:F2}">
                                                    <HeaderStyle Width="80px"/>
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings>
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true"/>
                                            <Selecting AllowRowSelect="true"/>
                                        </ClientSettings>
                                    </telerik:RadGrid>--%>
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
