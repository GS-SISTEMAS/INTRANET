<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmResumAfiliadas.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.Reportes.frmResumAfiliadas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Reporte resumen de afiliadas
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("btnExcel") >= 0)
                args.set_enableAjax(false);
            if (args.get_eventTarget().indexOf("btnExportar1") >= 0)
                args.set_enableAjax(false);
            if (args.get_eventTarget().indexOf("btnDetalleAnti") >= 0)
                args.set_enableAjax(false);
            if (args.get_eventTarget().indexOf("btnEXportAnti") >= 0)
                args.set_enableAjax(false);
            //btnExportar1
            
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <%--<telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="true" />--%>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpReporte" ControlID="rapReporte" />
                    <telerik:AjaxUpdatedControl ControlID="ajaxMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGraph1">
                <UpdatedControls>
                    <%--<telerik:AjaxUpdatedControl ControlID="rwmReporte" LoadingPanelID="ralpReporte" />--%>
                    <telerik:AjaxUpdatedControl ControlID="ajaxMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGraph2">
                <UpdatedControls>
                    <%--<telerik:AjaxUpdatedControl ControlID="rwmReporte" />--%>
                    <telerik:AjaxUpdatedControl ControlID="ajaxMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdCobrar" EventName="ItemCommand">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rwmReporte" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdPagar" EventName="ItemCommand">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rwmReporte" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="PivotAgrupamiento">
                <UpdatedControls>                    
                    <telerik:AjaxUpdatedControl ControlID="pnlAgrupamiento1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDetalleMov" />
                    <telerik:AjaxUpdatedControl ControlID="pnlAnticuamiento" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDetalleAntiMov" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnRegresar" EventName="Click">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlAgrupamiento1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDetalleMov" />
                    <telerik:AjaxUpdatedControl ControlID="pnlAnticuamiento" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDetalleAntiMov" />
                </UpdatedControls>
            </telerik:AjaxSetting>   
            <telerik:AjaxSetting AjaxControlID="btnRegresarDetAnti" EventName="Click">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlAgrupamiento1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDetalleMov" />
                    <telerik:AjaxUpdatedControl ControlID="pnlAnticuamiento" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDetalleAntiMov" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadPivotGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlAgrupamiento1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDetalleMov" />
                    <telerik:AjaxUpdatedControl ControlID="pnlAnticuamiento" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDetalleAntiMov" />
                </UpdatedControls>
            </telerik:AjaxSetting>  
            <telerik:AjaxSetting AjaxControlID="ddlFamilias">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlAgrupamiento1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDetalleMov" />
                    <telerik:AjaxUpdatedControl ControlID="pnlAnticuamiento" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDetalleAntiMov" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadDropDownList1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlAgrupamiento1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDetalleMov" />
                    <telerik:AjaxUpdatedControl ControlID="pnlAnticuamiento" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDetalleAntiMov" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <%--<telerik:AjaxSetting AjaxControlID="btnExportar1" EventName="Click">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlAgrupamiento1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDetalleMov" />
                    <telerik:AjaxUpdatedControl ControlID="pnlAnticuamiento" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDetalleAntiMov" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <div style="width: 100%; height: 100%">
        <%-- LOADER --%>
        <telerik:RadAjaxLoadingPanel ID="ralpReporte" runat="server"></telerik:RadAjaxLoadingPanel>
        <%-- FIN LOADER --%>
        <%-- LAYOUT --%>
        <telerik:RadAjaxPanel ID="rapReporte" runat="server" Height="100%" Width="100%" ClientEvents-OnRequestStart="requestStart">
            <telerik:RadPageLayout ID="radPageL1" runat="server" Width="100%" Height="100%">
                <Rows>
                    <telerik:LayoutRow>
                        <Content>
                            <div class="col-md-9">
                                <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Resumen de afiliadas"></asp:Label>
                            </div>
                            <div class="col-md-3" style="text-align: right;">
                                <telerik:RadButton ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click">
                                    <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png" />
                                </telerik:RadButton>
                            </div>
                        </Content>
                    </telerik:LayoutRow>
                    <telerik:LayoutRow runat="server" Height="94%">
                        <Columns>
                            <telerik:LayoutColumn Span="12" Height="100%">
                                <telerik:RadSplitter RenderMode="Lightweight" ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Vertical">
                                    <telerik:RadPane ID="RadPane1" runat="server" Width="22px" Scrolling="None">
                                        <telerik:RadSlidingZone ID="RadSlidingZone1" runat="server" Width="22px">
                                            <telerik:RadSlidingPane ID="RadSlidingPane1" runat="server" Width="250px" Title="Filtros de Busqueda"
                                                EnableDock="false" MinWidth="225" MinHeight="225" Scrolling="None">
                                                <div class="fila">
                                                    <div class="colum4">
                                                        <asp:Label ID="lblFechaCorte" runat="server" Text="Fecha de corte" CssClass="etiqueta"></asp:Label>
                                                    </div>
                                                    <div class="colum6">
                                                        <telerik:RadMonthYearPicker ID="dpFechaCorte" runat="server" DateInput-DateFormat="MM/yyyy" Width="100%"></telerik:RadMonthYearPicker>
                                                    </div>
                                                </div>
                                                <div class="fila">
                                                    <div class="colum4">
                                                        <asp:Label ID="lblMoneda" runat="server" Text="Fecha de corte" CssClass="etiqueta"></asp:Label>
                                                    </div>
                                                    <div class="colum6">
                                                        <asp:DropDownList ID="ddlMoneda" runat="server" Font-Size="8pt" Width="100px">
                                                            <asp:ListItem Value="1" Selected="True"> Soles</asp:ListItem>
                                                            <asp:ListItem Value="0"> Dólares</asp:ListItem>
                                                        </asp:DropDownList>
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
                                    <telerik:RadPane ID="radPane2" runat="server" Scrolling="X" Height="100%">
                                        <div style="/*background-color: azure; */ width: 100%; /*height: 50%; */" class="row">
                                            <div class="col-md-12" style="padding-left: 20px;">
                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-md-8">
                                                        <asp:Label ID="Label1" runat="server" CssClass="titulo" Text="Cuentas por cobrar"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4" style="text-align: right; padding-top: 10px;">
                                                        <telerik:RadButton ID="btnGraph1" runat="server" Text="Grafico" OnClick="btnGraph1_Click">
                                                            <Icon PrimaryIconUrl="../../Images/Icons/analytics-16.png" />
                                                        </telerik:RadButton>
                                                    </div>
                                                </div>
                                                <telerik:RadGrid ID="grdCobrar" runat="server" AutoGenerateColumns="false" Width="100%" Height="100%" OnItemDataBound="grdCobrar_ItemDataBound"
                                                    OnItemCommand="grdCobrar_ItemCommand">
                                                    <ExportSettings Excel-Format="Html" OpenInNewWindow="true"></ExportSettings>
                                                    <MasterTableView TableLayout="Fixed">
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="nombreEmpresa" HeaderText="Empresa"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="montoInicio" HeaderText="" DataType="System.Decimal"
                                                                DataFormatString="{0:#,00.00}">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="montoPendiente" HeaderText="" DataType="System.Decimal"
                                                                DataFormatString="{0:#,00.00}">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="cargos" HeaderText="" DataType="System.Decimal"
                                                                DataFormatString="{0:#,00.00}">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="abonos" HeaderText="" DataType="System.Decimal"
                                                                DataFormatString="{0:#,00.00}">
                                                            </telerik:GridBoundColumn>
                                                            <%--<telerik:GridBoundColumn DataField="cumplimiento" HeaderText="Nivel de cumplimiento" DataType="System.Decimal" 
                                                                DataFormatString="{0:#,00.00}"></telerik:GridBoundColumn>--%>
                                                            <telerik:GridTemplateColumn HeaderText="Nivel de cumplimiento" DataField="cumplimiento">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNivelCumple" runat="server"></asp:Label>
                                                                    <span style="margin-left: 5px;">
                                                                        <asp:Image ID="imgSemaforo" runat="server" Width="16" /></span>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkAgrupamiento" runat="server" Text="Agrupación" Font-Size="Smaller" ForeColor="#0000ff" CommandName="Agrupamiento" CommandArgument='<%# Eval("id_agenda") %>'></asp:LinkButton>
                                                                    <span style="margin-left: 10px;">
                                                                        <asp:LinkButton ID="lnkAnticuamiento" runat="server" Text="Anticuamiento" CommandName="Anticuamiento" CommandArgument='<%# Eval("id_agenda") %>' Font-Size="Smaller" ForeColor="#0000ff"></asp:LinkButton></span>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </div>
                                        </div>
                                        <div style="/*background-color: brown; */ width: 100%; /*height: 50%; */" class="row">
                                            <div class="col-md-12" style="padding-left: 20px; margin-top: 20px;">
                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-md-8">
                                                        <asp:Label ID="Label2" runat="server" CssClass="titulo" Text="Cuentas por pagar"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4" style="text-align: right; padding-top: 10px;">
                                                        <telerik:RadButton ID="btnGraph2" runat="server" Text="Grafico" OnClick="btnGraph2_Click">
                                                            <Icon PrimaryIconUrl="../../Images/Icons/analytics-16.png" />
                                                        </telerik:RadButton>
                                                    </div>
                                                </div>
                                                <telerik:RadGrid ID="grdPagar" runat="server" AutoGenerateColumns="false" Width="100%" Height="100%" OnItemDataBound="grdPagar_ItemDataBound"
                                                    OnItemCommand="grdPagar_ItemCommand">
                                                    <ExportSettings Excel-Format="Html" OpenInNewWindow="true"></ExportSettings>
                                                    <MasterTableView TableLayout="Fixed">
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="nombreEmpresa" HeaderText="Empresa"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="montoInicio" HeaderText="" DataType="System.Decimal"
                                                                DataFormatString="{0:#,00.00}">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="montoPendiente" HeaderText="" DataType="System.Decimal"
                                                                DataFormatString="{0:#,00.00}">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="cargos" HeaderText="" DataType="System.Decimal"
                                                                DataFormatString="{0:#,00.00}">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="abonos" HeaderText="" DataType="System.Decimal"
                                                                DataFormatString="{0:#,00.00}">
                                                            </telerik:GridBoundColumn>
                                                            <%--<telerik:GridBoundColumn DataField="cumplimiento" HeaderText="Nivel de cumplimiento" DataType="System.Decimal" 
                                                                DataFormatString="{0:#,00.00}"></telerik:GridBoundColumn>--%>
                                                            <telerik:GridTemplateColumn HeaderText="Nivel de cumplimiento" DataField="cumplimiento">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNivelCumple" runat="server"></asp:Label>
                                                                    <span style="margin-left: 5px;">
                                                                        <asp:Image ID="imgSemaforo" runat="server" Width="16" /></span>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkAgrupamiento" runat="server" Text="Agrupación" Font-Size="Smaller" ForeColor="#0000ff" CommandName="Agrupamiento" CommandArgument='<%# Eval("id_agenda") %>'></asp:LinkButton>
                                                                    <span style="margin-left: 10px;">
                                                                        <asp:LinkButton ID="lnkAnticuamiento" runat="server" Text="Anticuamiento" CommandName="Anticuamiento" CommandArgument='<%# Eval("id_agenda") %>' Font-Size="Smaller" ForeColor="#0000ff"></asp:LinkButton></span>
                                                                </ItemTemplate>                                                               
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </div>
                                        </div>
                                    </telerik:RadPane>
                                </telerik:RadSplitter>
                            </telerik:LayoutColumn>
                        </Columns>
                    </telerik:LayoutRow>
                </Rows>
            </telerik:RadPageLayout>

        </telerik:RadAjaxPanel>
        <%-- FIN LAYOUT --%>
    </div>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <telerik:RadAjaxPanel ID="ajaxMensaje" runat="server">
        <asp:Label ID="lblRegistros" runat="server"></asp:Label>

        <telerik:RadWindowManager ID="rwmReporte" runat="server" >
            <Windows>
                <telerik:RadWindow ID="modalGrafico" runat="server" Title="" Width="600px" Height="500px" Modal="true" RenderMode="Lightweight"
                    ShowContentDuringLoad="false">
                    <ContentTemplate>
                        <div style="padding-right: 15px; font-weight: bolder;">
                            <div class="row">
                                <div class="col-md-8">
                                    <asp:Label ID="lblTituloGraph" runat="server" Font-Size="Medium"></asp:Label>
                                </div>
                                <div class="col-md-4" style="text-align: right;">
                                    <asp:Label ID="lblMontoTotal" runat="server" Font-Size="Medium"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <telerik:RadHtmlChart runat="server" ID="GraficoPie" Transitions="true" CssClass="fb-sized">
                            <PlotArea>
                                <Series>
                                    <telerik:PieSeries StartAngle="90" DataFieldY="valorp" NameField="NombreEmpresa"
                                        ExplodeField="MayorValor" ColorField="colorColumn">
                                        <LabelsAppearance Position="OutsideEnd" DataFormatString="{0} %" />
                                        <TooltipsAppearance DataFormatString="{0} %" />
                                    </telerik:PieSeries>
                                </Series>
                            </PlotArea>
                            <ChartTitle Text="">
                            </ChartTitle>
                        </telerik:RadHtmlChart>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow ID="ModalDetalle" runat="server" Title="" Width="1024px" Height="550px" RenderMode="Lightweight" Modal="true">
                    <ContentTemplate>

                        <telerik:RadAjaxPanel ID="pnlAgrupamiento1" runat="server" Visible="false" ClientEvents-OnRequestStart="requestStart">

                            <div class="row" style="width:1000px;">
                            <div class="col-md-6">
                                <asp:Label ID="lbltituloPopupDetalle" runat="server" Text="" CssClass="titulo"></asp:Label>
                            </div>
                            <div class="col-md-6" style="text-align:right;">
                                <telerik:RadButton ID="btnExportar1" runat="server" Text="Excel" OnClick="btnExportar1_Click"><%--btnExcelPivot_Click--%>
                                    <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png" />
                                </telerik:RadButton>
                            </div>
                        </div>
                        <div class="row" style="width:1000px;">
                            <div class="col-md-12">
                                <asp:Label ID="lblTituloEmpresaAEmpresa" runat="server" Text="Texto empresa a empresa" CssClass="titulo"></asp:Label>
                            </div>
                        </div>
                        <div class="row" style="padding-right: 0px; padding-left: 5px; height: 350px; width: 100%;">
                            <div class="col-md-12" style="height: 100%;">
                                <telerik:RadPivotGrid ID="PivotAgrupamiento" runat="server" Width="100%" Height="400px" AllowFiltering="false"
                                    ShowFilterHeaderZone="false" ShowDataHeaderZone="false" ShowRowHeaderZone="false" ShowColumnHeaderZone="false" EnableConfigurationPanel="True"
                                    TotalsSettings-GrandTotalsVisibility="RowsOnly" RowGroupsDefaultExpanded="false" OnNeedDataSource="PivotAgrupamiento_NeedDataSource" OnCellDataBound="PivotAgrupamiento_CellDataBound"
                                    OnItemCommand="PivotAgrupamiento_ItemCommand">
                                    <ExportSettings Excel-Format="Xlsx" OpenInNewWindow="true" FileName="Agrupamiento"></ExportSettings>
                                    <ClientSettings EnableFieldsDragDrop="false">
                                        <Scrolling AllowVerticalScroll="true"></Scrolling>
                                    </ClientSettings>
                                    <Fields>
                                        <telerik:PivotGridRowField DataField="Familia" ZoneIndex="0">                                            
                                            <CellTemplate>
                                                <asp:LinkButton ID="lnkFamilia" runat="server" Text='<%# GetDataItem().ToString().Split(new string[] {"|"},StringSplitOptions.None)[1] %>' CommandName="VerDetalleFam" CommandArgument='<%# GetDataItem().ToString().Split(new string[] {"|"},StringSplitOptions.None)[0] %>' ></asp:LinkButton>                                                
                                            </CellTemplate>
                                            <TotalHeaderCellTemplate>                                                
                                                <asp:Label ID="lblTotalFamilia" runat="server" Text='<%# GetDataItem().ToString().Split(new string[] {"|"},StringSplitOptions.None)[1] %>'></asp:Label>                                                
                                            </TotalHeaderCellTemplate>
                                            <CellStyle Width="150px" />
                                        </telerik:PivotGridRowField>
                                        <telerik:PivotGridRowField DataField="Documento" ZoneIndex="1">
                                            <CellTemplate>
                                                <asp:LinkButton ID="lnkDocumento" runat="server" Text='<%# GetDataItem().ToString().Split(new string[] {"|"},StringSplitOptions.None)[1] %>' CommandName="VerDetalleDoc" CommandArgument='<%# GetDataItem().ToString().Split(new string[] {"|"},StringSplitOptions.None)[0] %>' ></asp:LinkButton>                                                
                                            </CellTemplate>                                            
                                            <CellStyle Width="200px" />
                                        </telerik:PivotGridRowField>

                                        <telerik:PivotGridColumnField DataField="Anio">
                                        </telerik:PivotGridColumnField>

                                        <telerik:PivotGridAggregateField DataField="MontoInicio" Aggregate="Sum" DataFormatString="{0:##,###0.##}">
                                            <HeaderCellTemplate>
                                                Deuda al inicio
                                            </HeaderCellTemplate>
                                            <CellStyle Width="100px" />
                                        </telerik:PivotGridAggregateField>

                                        <telerik:PivotGridAggregateField DataField="MontoAlCorte" Aggregate="Sum" DataFormatString="{0:##,###0.##}">
                                            <HeaderCellTemplate>
                                                Pendiente
                                            </HeaderCellTemplate>
                                            <CellStyle Width="100px" />
                                        </telerik:PivotGridAggregateField>

                                        <telerik:PivotGridAggregateField DataField="Cargos" Aggregate="Sum" DataFormatString="{0:##,###0.##}">
                                            <HeaderCellTemplate>
                                                Cargos
                                            </HeaderCellTemplate>
                                            <CellStyle Width="100px" />
                                        </telerik:PivotGridAggregateField>

                                        <telerik:PivotGridAggregateField DataField="Abonos" Aggregate="Sum" DataFormatString="{0:##,###0.##}">
                                            <HeaderCellTemplate>
                                                Abonos
                                            </HeaderCellTemplate>
                                            <CellStyle Width="100px" />
                                        </telerik:PivotGridAggregateField>

                                        <telerik:PivotGridAggregateField DataField="Cumplimiento" Caption="Cumplimiento" Aggregate="Average" DataFormatString="{0:##,###0.##}%">
                                            <HeaderCellTemplate>
                                                Cumplimiento
                                            </HeaderCellTemplate>
                                            <CellStyle Width="100px" />                                             
                                            <CellTemplate>
                                                <asp:Label ID="lblCumplimiento" runat="server" Width="40px"></asp:Label>
                                                <span style="margin-left: 5px;">
                                                    <asp:Image ID="imgSemaforo" runat="server" Width="16" />
                                                </span>
                                            </CellTemplate>
                                            <RowTotalCellTemplate>
                                                <asp:Label ID="lblCumplimiento" runat="server" Text="" Width="40px"></asp:Label>
                                                <span style="margin-left: 5px;">
                                                    <asp:Image ID="imgSemaforo" runat="server" Width="16" />
                                                </span>
                                            </RowTotalCellTemplate>
                                            <RowGrandTotalCellTemplate>
                                                <asp:Label ID="lblCumplimiento" runat="server" Text="" Width="40px"></asp:Label>
                                                <span style="margin-left: 5px;">
                                                    <asp:Image ID="imgSemaforo" runat="server" Width="16" />
                                                </span>
                                            </RowGrandTotalCellTemplate>
                                        </telerik:PivotGridAggregateField> 
                                        <%--<telerik:PivotGridAggregateField DataField="IdDocumento" >
                                            <HeaderCellTemplate></HeaderCellTemplate>
                                            <CellTemplate>
                                                <asp:LinkButton ID="lnkDetalle" runat="server" CommandName="VerDetalle" Text="Ver detalle" CommandArgument='<%# Container.DataItem.ToString() %>' ></asp:LinkButton>
                                            </CellTemplate>
                                            <RowGrandTotalCellTemplate></RowGrandTotalCellTemplate>
                                            <RowTotalCellTemplate></RowTotalCellTemplate>                                            
                                            <CellStyle Width="100px" /> 
                                        </telerik:PivotGridAggregateField>--%>
                                    </Fields>
                                </telerik:RadPivotGrid>
                                <asp:Label ID="lblmensaje2" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        </telerik:RadAjaxPanel>
                        <telerik:RadAjaxPanel id="pnlDetalleMov" runat="server" Visible="false" ClientEvents-OnRequestStart="requestStart">
                            <div class="row" style="width:1000px;">
                                <div class="col-md-6">
                                    <asp:Label ID="lblTituloDetalle" runat="server" Text="" CssClass="titulo"></asp:Label>
                                </div>
                                <div class="col-md-4">                                    
                                    <telerik:RadDropDownList ID="ddlFamilias" runat="server" AutoPostBack="true" Width="150px" OnSelectedIndexChanged="ddlFamilias_SelectedIndexChanged"></telerik:RadDropDownList>
                                </div>
                                <div class="col-md-1">
                                    <telerik:RadButton ID="btnExcelDetalle" runat="server" Text="Excel" OnClick="btnExcelDetalle_Click">
                                        <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png" />
                                    </telerik:RadButton>
                                </div>
                                <div class="col-md-1">
                                    <telerik:RadButton ID="btnRegresar" runat="server" Text="Regresar" OnClick="btnRegresar_Click">
                                        <Icon PrimaryIconUrl="../../Images/Icons/arrowLeft-16.png" />
                                    </telerik:RadButton>
                                </div>
                            </div>   
                            <div class="row" style="width:1000px;">
                                <div class="col-md-12">
                                    <telerik:RadGrid ID="rdDetalle" runat="server" AutoGenerateColumns="false" Width="100%" Height="100%">
                                        <ExportSettings Excel-Format="Xlsx" OpenInNewWindow="true"></ExportSettings>
                                        <MasterTableView TableLayout="Fixed">
                                            <Columns>
                                                <%--<telerik:GridBoundColumn DataField="montoInicio" HeaderText="" DataType="System.Decimal"
                                                                DataFormatString="{0:#,00.00}">
                                                            </telerik:GridBoundColumn>--%>
                                                <telerik:GridBoundColumn DataField="NroOperacion" HeaderText="Ope.">
                                                    <ItemStyle Width="50px" />
                                                    <HeaderStyle Width="50px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Documento" HeaderText="">
                                                    <ItemStyle Width="200px" />
                                                    <HeaderStyle Width="200px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FechaVencimiento" HeaderText="Vencimiento" DataType="System.DateTime"
                                                    DataFormatString="{0:dd/MM/yyyy}">
                                                    <ItemStyle Width="120px" />
                                                    <HeaderStyle Width="120px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DifDias" HeaderText="Días atraso" DataType="System.Int32">
                                                    <ItemStyle Width="80px" />
                                                    <HeaderStyle Width="80px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Glosa" HeaderText="Referencia" DataType="System.String">                                                    
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="MontoSoles" HeaderText="Soles" DataType="System.Decimal" DataFormatString="{0:#,00.00}">
                                                    <ItemStyle Width="80px" />
                                                    <HeaderStyle Width="80px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="MontoDolares" HeaderText="Dólares" DataType="System.Decimal" DataFormatString="{0:#,00.00}">
                                                    <ItemStyle Width="80px" />
                                                    <HeaderStyle Width="80px" />
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </div>
                        </telerik:RadAjaxPanel>
                        
                        <telerik:RadAjaxPanel ID="pnlAnticuamiento" runat="server" Visible="false" ClientEvents-OnRequestStart="requestStart">

                            <div class="row" style="width:1000px;">
                            <div class="col-md-6">
                                <asp:Label ID="Label3" runat="server" Text="" CssClass="titulo"></asp:Label>
                            </div>
                            <div class="col-md-6" style="text-align:right;">
                                <telerik:RadButton ID="btnEXportAnti" runat="server" Text="Excel" OnClick="btnEXportAnti_Click">
                                    <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png" />
                                </telerik:RadButton>
                            </div>
                        </div>
                        <div class="row" style="width:1000px;">
                            <div class="col-md-12">
                                <asp:Label ID="Label4" runat="server" Text="Texto empresa a empresa" CssClass="titulo"></asp:Label>
                            </div>
                        </div>
                        <div class="row" style="padding-right: 0px; padding-left: 5px; height: 350px; width: 100%;">
                            <div class="col-md-12" style="height: 100%;">
                                <telerik:RadPivotGrid ID="RadPivotGrid1" runat="server" Width="100%" Height="400px" AllowFiltering="false"
                                    ShowFilterHeaderZone="false" ShowDataHeaderZone="false" ShowRowHeaderZone="false" ShowColumnHeaderZone="false" EnableConfigurationPanel="True"
                                    TotalsSettings-GrandTotalsVisibility="RowsOnly" RowGroupsDefaultExpanded="false" OnNeedDataSource="RadPivotGrid1_NeedDataSource" OnCellDataBound="RadPivotGrid1_CellDataBound"
                                    OnItemCommand="RadPivotGrid1_ItemCommand">
                                    <ExportSettings Excel-Format="Xlsx" OpenInNewWindow="true" FileName="Anticuamiento"></ExportSettings>
                                    <ClientSettings EnableFieldsDragDrop="false">
                                        <Scrolling AllowVerticalScroll="true"></Scrolling>
                                    </ClientSettings>
                                    <Fields>
                                        <telerik:PivotGridRowField DataField="Familia" ZoneIndex="0">
                                            <CellTemplate>
                                                <asp:LinkButton ID="lnkFamilia" runat="server" Text='<%# GetDataItem().ToString().Split(new string[] {"|"},StringSplitOptions.None)[1] %>' CommandName="VerDetalleFam" CommandArgument='<%# GetDataItem().ToString().Split(new string[] {"|"},StringSplitOptions.None)[0] %>' ></asp:LinkButton>                                                
                                            </CellTemplate>
                                            <TotalHeaderCellTemplate>                                                
                                                <asp:Label ID="lblTotalFamilia" runat="server" Text='<%# GetDataItem().ToString().Split(new string[] {"|"},StringSplitOptions.None)[1] %>'></asp:Label>                                                
                                            </TotalHeaderCellTemplate>                                            
                                            <CellStyle Width="150px" />
                                        </telerik:PivotGridRowField>
                                        <telerik:PivotGridRowField DataField="Documento" ZoneIndex="1">
                                            <CellTemplate>
                                                <asp:LinkButton ID="lnkDocumento" runat="server" Text='<%# GetDataItem().ToString().Split(new string[] {"|"},StringSplitOptions.None)[1] %>' CommandName="VerDetalleDoc" CommandArgument='<%# GetDataItem().ToString().Split(new string[] {"|"},StringSplitOptions.None)[0] %>' ></asp:LinkButton>                                                
                                            </CellTemplate>
                                            <CellStyle Width="200px" />
                                        </telerik:PivotGridRowField>

                                        <telerik:PivotGridColumnField DataField="Anio">
                                        </telerik:PivotGridColumnField>

                                        <telerik:PivotGridAggregateField DataField="Vence1a30" Aggregate="Sum" DataFormatString="{0:##,###0.##}">
                                            <HeaderCellTemplate>
                                                Por vencer<br /> 1 a 30
                                            </HeaderCellTemplate>
                                            <CellStyle Width="100px" />
                                        </telerik:PivotGridAggregateField>

                                        <telerik:PivotGridAggregateField DataField="Vence31a60" Aggregate="Sum" DataFormatString="{0:##,###0.##}">
                                            <HeaderCellTemplate>
                                                Por vencer<br /> 31 a 60
                                            </HeaderCellTemplate>
                                            <CellStyle Width="100px" />
                                        </telerik:PivotGridAggregateField>

                                        <telerik:PivotGridAggregateField DataField="Vence61a90" Aggregate="Sum" DataFormatString="{0:##,###0.##}">
                                            <HeaderCellTemplate>
                                                Por vencer<br /> 61 a 90
                                            </HeaderCellTemplate>
                                            <CellStyle Width="100px" />
                                        </telerik:PivotGridAggregateField>

                                        <telerik:PivotGridAggregateField DataField="Vence91a120" Aggregate="Sum" DataFormatString="{0:##,###0.##}">
                                            <HeaderCellTemplate>
                                                Por vencer<br /> 91 a 120
                                            </HeaderCellTemplate>
                                            <CellStyle Width="100px" />
                                        </telerik:PivotGridAggregateField>

                                        <telerik:PivotGridAggregateField DataField="Vencido" Aggregate="Sum" DataFormatString="{0:##,###0.##}">
                                            <HeaderCellTemplate>
                                                Vencido
                                            </HeaderCellTemplate>
                                            <CellStyle Width="100px" />
                                        </telerik:PivotGridAggregateField>

                                        <%--<telerik:PivotGridAggregateField DataField="IdEmpresa">
                                            <HeaderCellTemplate></HeaderCellTemplate>
                                            <CellTemplate>
                                                <asp:LinkButton ID="lnkDetalle" runat="server" CommandName="VerDetalle" Text="Ver Detalle" ></asp:LinkButton>
                                            </CellTemplate>
                                            <RowGrandTotalCellTemplate></RowGrandTotalCellTemplate>
                                            <RowTotalCellTemplate></RowTotalCellTemplate>
                                            <CellStyle Width="100px" /> 
                                        </telerik:PivotGridAggregateField>--%>
                                    </Fields>
                                </telerik:RadPivotGrid>
                            </div>
                        </div>

                        </telerik:RadAjaxPanel>
                        <telerik:RadAjaxPanel id="pnlDetalleAntiMov" runat="server" Visible="false" ClientEvents-OnRequestStart="requestStart">
                            <div class="row" style="width:1000px;">
                                <div class="col-md-6">
                                    <asp:Label ID="Label5" runat="server" Text="" CssClass="titulo"></asp:Label>
                                </div>
                                <div class="col-md-4">                                    
                                    <telerik:RadDropDownList ID="RadDropDownList1" runat="server" AutoPostBack="true" Width="150px" OnSelectedIndexChanged="RadDropDownList1_SelectedIndexChanged"></telerik:RadDropDownList>
                                </div>
                                <div class="col-md-1">
                                    <telerik:RadButton ID="btnDetalleAnti" runat="server" Text="Excel" OnClick="btnDetalleAnti_Click">
                                        <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png" />
                                    </telerik:RadButton>
                                </div>
                                <div class="col-md-1">
                                    <telerik:RadButton ID="btnRegresarDetAnti" runat="server" Text="Regresar" OnClick="btnRegresarDetAnti_Click">
                                        <Icon PrimaryIconUrl="../../Images/Icons/arrowLeft-16.png" />
                                    </telerik:RadButton>
                                </div>
                            </div>   
                            <div class="row" style="width:1000px;">
                                <div class="col-md-12">
                                    <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="false" Width="100%" Height="100%">
                                        <ExportSettings Excel-Format="Xlsx" OpenInNewWindow="true"></ExportSettings>
                                        <MasterTableView TableLayout="Fixed">
                                            <Columns>
                                                <%--<telerik:GridBoundColumn DataField="montoInicio" HeaderText="" DataType="System.Decimal"
                                                                DataFormatString="{0:#,00.00}">
                                                            </telerik:GridBoundColumn>--%>
                                                <telerik:GridBoundColumn DataField="NroOperacion" HeaderText="Ope.">
                                                    <ItemStyle Width="50px" />
                                                    <HeaderStyle Width="50px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Documento" HeaderText="">
                                                    <ItemStyle Width="200px" />
                                                    <HeaderStyle Width="200px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FechaVencimiento" HeaderText="Vencimiento" DataType="System.DateTime"
                                                    DataFormatString="{0:dd/MM/yyyy}">
                                                    <ItemStyle Width="120px" />
                                                    <HeaderStyle Width="120px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DifDias" HeaderText="Días atraso" DataType="System.Int32">
                                                    <ItemStyle Width="80px" />
                                                    <HeaderStyle Width="80px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Glosa" HeaderText="Referencia" DataType="System.String">                                                    
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="MontoSoles" HeaderText="Soles" DataType="System.Decimal" DataFormatString="{0:#,00.00}">
                                                    <ItemStyle Width="80px" />
                                                    <HeaderStyle Width="80px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="MontoDolares" HeaderText="Dólares" DataType="System.Decimal" DataFormatString="{0:#,00.00}">
                                                    <ItemStyle Width="80px" />
                                                    <HeaderStyle Width="80px" />
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </div>
                        </telerik:RadAjaxPanel>

                    </ContentTemplate>
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
</asp:Content>
