<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmVendedor.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Reportes.ReporteGerencial.frmVendedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre - Reporte de vendedor por periodo
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">


    <script type="text/javascript">
             function requestStart(sender, args) {

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
        </AjaxSettings>
    </telerik:RadAjaxManager>

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
                            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Reporte de ventas por periodos"></asp:Label>
                        </telerik:LayoutColumn>
                         

                    </Columns>
                </telerik:LayoutRow>


                <telerik:LayoutRow Height="3%">
                                <Columns>
                                    <telerik:LayoutColumn Span="11">
                                    </telerik:LayoutColumn>
                                    <telerik:LayoutColumn Span="1" style="text-align:right">
                                        <telerik:RadButton ID="btnExcel" runat="server" Text="Excel"  OnClick="btnExcel_Click"  ToolTip="Descargar Excel">
                                            <Icon PrimaryIconUrl="../../../Images/Icons/excel-16.png" />
                                        </telerik:RadButton>
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
                                                <div class="colum4">
                                                    <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click">
                                                        <Icon PrimaryIconUrl="../../../Images/Icons/search-16.png" />
                                                    </telerik:RadButton>
                                                </div>
                                            </div>
                                        </telerik:RadSlidingPane>
                                    </telerik:RadSlidingZone>
                                </telerik:RadPane>
                                <telerik:RadPane ID="RadPane2" runat="server" Width="100%" Scrolling="None" Height="100%">
                                    <telerik:RadPivotGrid ID="grdVendedor" runat="server" Width="100%"  ColumnHeaderZoneText="ColumnHeaderZone"
                                        Height="100%" 
                                        AllowFiltering="false" 
                                        ShowFilterHeaderZone="false"
                                        ShowDataHeaderZone="false" 
                                        ShowRowHeaderZone="false" 
                                        ShowColumnHeaderZone="false" 
                                        EnableConfigurationPanel="true"
                                        


                                RowGroupsDefaultExpanded="False"
                                     TotalsSettings-GrandTotalsVisibility="RowsOnly"

                                     OnNeedDataSource="grdVendedor_NeedDataSource"
                                     OnCellDataBound="grdVendedor_CellDataBound"

                                     OnPreRender="grdVendedor_PreRender"
                                     RowHeaderCellStyle-Font-Size="X-Small"
                                     PagerStyle-Font-Size="X-Small" 
                                    OnPivotGridCellExporting="RadPivotGrid1_PivotGridCellExporting" 


                                        >
                                         
                                        <ExportSettings Excel-Format="Xlsx" OpenInNewWindow="true"></ExportSettings>


                                        <ClientSettings EnableFieldsDragDrop="false">
                                            <Scrolling AllowVerticalScroll="true"></Scrolling>
                                        </ClientSettings>

                                        <Fields>
                                            <telerik:PivotGridRowField DataField="Zona" ZoneIndex="0">
                                                <CellStyle Width="250px"/>
                                            </telerik:PivotGridRowField>
                                            <telerik:PivotGridRowField DataField="Vendedor" ZoneIndex="1">
                                                <CellStyle Width="250px"/>
                                            </telerik:PivotGridRowField>
                                            <%-- <telerik:PivotGridColumnField DataField="Año">
                                            </telerik:PivotGridColumnField>--%>
                                            <telerik:PivotGridColumnField DataField="Periodo">
                                            </telerik:PivotGridColumnField>
                                            <telerik:PivotGridAggregateField DataField="ValorVenta" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Valor Venta
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                            <telerik:PivotGridAggregateField DataField="ValorPlanificado" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Valor Planif.
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                            <telerik:PivotGridAggregateField DataField="Avance" CalculationDataFields="ValorVenta,ValorPlanificado" CalculationExpression="{0}/{1}*100" DataFormatString="{0:F0}%">
                                            </telerik:PivotGridAggregateField>
                                        </Fields>
                                        <SortExpressions>
                                            <telerik:PivotGridSortExpression FieldName="ValorVenta" SortOrder="Descending"></telerik:PivotGridSortExpression>
                                        </SortExpressions>
                                        <TotalsSettings RowsSubTotalsPosition="None"/>
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
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>

        <div class="col-md-12">
            <asp:HiddenField ID="lblReporte" runat="server"  />
        </div>

    </div>
</asp:Content>
