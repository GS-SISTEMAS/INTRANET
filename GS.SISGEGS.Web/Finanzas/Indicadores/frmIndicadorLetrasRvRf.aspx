<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmIndicadorLetrasRvRf.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.Indicadores.frmIndicadorLetrasRvRf" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Indicador Letras renovadas y refinanciadas
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
    <telerik:RadAjaxManager ID="radj1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar" EventName="Click">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ajaxpanel1" LoadingPanelID="radajaxloader" />
                    <telerik:AjaxUpdatedControl ControlID="pnlMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="width: 100%; height: 100%;">
        <telerik:RadAjaxLoadingPanel ID="radajaxloader" runat="server"></telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxPanel ID="ajaxpanel1" runat="server" Width="100%" Height="100%" ClientEvents-OnRequestStart="requestStart">
            <telerik:RadPageLayout ID="radpage1" runat="server" Width="100%" Height="100%">
                <Rows>
                    <telerik:LayoutRow>
                        <Content>
                            <div class="col-md-12">
                                <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Indicador de letras renovadas y refinanciadas"></asp:Label>
                            </div>
                        </Content>
                    </telerik:LayoutRow>
                    <telerik:LayoutRow>
                        <Content>
                            <div class="row" style="width: 600px; margin-top: 20px;">
                                <div class="col-md-2">
                                    <asp:Label ID="lblMes" runat="server" CssClass="etiqueta" Text="Mes"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadMonthYearPicker ID="dpFechaMes" runat="server" DateInput-DateFormat="MM/yyyy" Width="100%"></telerik:RadMonthYearPicker>
                                </div>
                                <div class="col-md-3">
                                    <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click">
                                        <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                                    </telerik:RadButton>
                                </div>
                                <div class="col-md-3">
                                    <telerik:RadButton ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click">
                                        <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png" />
                                    </telerik:RadButton>
                                </div>
                            </div>
                        </Content>
                    </telerik:LayoutRow>
                    <telerik:LayoutRow>
                        <Columns>
                            <telerik:LayoutColumn Span="12" Height="100%">
                                <div style="margin-top: 15px;">
                                    <telerik:RadPivotGrid ID="pivotIndicador" runat="server" Height="460px" AllowFiltering="false"
                                        ShowFilterHeaderZone="false" ShowDataHeaderZone="false" ShowRowHeaderZone="false" ShowColumnHeaderZone="false"
                                        EnableConfigurationPanel="True" TotalsSettings-GrandTotalsVisibility="RowsOnly" RowGroupsDefaultExpanded="false"
                                        OnNeedDataSource="pivotIndicador_NeedDataSource" OnItemNeedCalculation="pivotIndicador_ItemNeedCalculation">
                                        <ExportSettings Excel-Format="Xlsx" OpenInNewWindow="true" FileName="IndicarRfRv"></ExportSettings>
                                        <ClientSettings EnableFieldsDragDrop="false">
                                            <Scrolling AllowVerticalScroll="true"></Scrolling>
                                        </ClientSettings>
                                        <Fields>
                                            <telerik:PivotGridRowField DataField="ZonaCobranza" ZoneIndex="0">
                                                <CellStyle Width="200px" />
                                            </telerik:PivotGridRowField>
                                            <telerik:PivotGridRowField DataField="ClienteNombre" ZoneIndex="1">
                                                <CellStyle Width="200px" />
                                            </telerik:PivotGridRowField>
                                            <%-- <telerik:PivotGridAggregateField DataField="ImportePorVencer" Aggregate="Sum" DataFormatString="{0:##,###0.##}">
                                                <HeaderCellTemplate>Deuda por vencer</HeaderCellTemplate>
                                                <CellStyle Width="80px" />
                                            </telerik:PivotGridAggregateField>--%>
                                            <telerik:PivotGridAggregateField DataField="TotalRenovada" Aggregate="Sum" DataFormatString="{0:##,###0.##}">
                                                <HeaderCellTemplate>Renovaciones</HeaderCellTemplate>
                                                <CellStyle Width="80px" />
                                            </telerik:PivotGridAggregateField>
                                            <telerik:PivotGridAggregateField DataField="TotalRefinanciada" Aggregate="Sum" DataFormatString="{0:##,###0.##}">
                                                <HeaderCellTemplate>Refinanciamientos</HeaderCellTemplate>
                                                <CellStyle Width="80px" />
                                            </telerik:PivotGridAggregateField>
                                            <telerik:PivotGridAggregateField DataField="DeudaTotal" Aggregate="Sum" DataFormatString="{0:##,###0.##}">
                                                <HeaderCellTemplate>Deuda Total</HeaderCellTemplate>
                                                <CellStyle Width="80px" />
                                            </telerik:PivotGridAggregateField>
                                            <telerik:PivotGridAggregateField DataField="Indicador" CalculationDataFields="TotalRenovada, TotalRefinanciada, DeudaTotal" DataFormatString="{0:##,###0.##}%">
                                                <HeaderCellTemplate>Indicador</HeaderCellTemplate>
                                                <CellStyle Width="80px" />
                                            </telerik:PivotGridAggregateField>
                                        </Fields>
                                    </telerik:RadPivotGrid>
                                </div>

                            </telerik:LayoutColumn>
                        </Columns>
                    </telerik:LayoutRow>
                </Rows>
            </telerik:RadPageLayout>
        </telerik:RadAjaxPanel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <telerik:RadAjaxPanel ID="pnlMensaje" runat="server">
        <telerik:RadWindowManager ID="rwmMensaje" runat="server"></telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
</asp:Content>
