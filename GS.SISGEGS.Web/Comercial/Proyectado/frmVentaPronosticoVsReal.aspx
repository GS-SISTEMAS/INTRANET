<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmVentaPronosticoVsReal.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Proyectado.frmVentaPronosticoVsReal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
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
                        <telerik:AjaxUpdatedControl LoadingPanelID="ralpReporte" ControlID="pnlGeneralContratos"/>
                    </UpdatedControls>
                </telerik:AjaxSetting>

             <telerik:AjaxSetting AjaxControlID="grdComparativoPivot">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdComparativoPivot" LoadingPanelID="ralpReporte"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

       <telerik:RadWindowManager ID="rwmReporte" runat="server"></telerik:RadWindowManager>
     <telerik:RadAjaxLoadingPanel ID="ralpReporte" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="pnlGeneralContratos" runat="server" Width="100%" Height="700px"  ClientEvents-OnRequestStart="requestStart">
        <div class="row">
            <div class="col-md-11">
                <asp:Label ID="lblTitulo" runat="server" Text="Seguimiento del pronóstico vs venta real en US$" CssClass="titulo"></asp:Label>
            </div>
            <div class="col-md-1" style="text-align:right">

            </div>
        </div>

        <div class="row">
             <div class="col-md-12">
                <div class="col-md-1">
                    <asp:Label ID="lblZona" runat="server" Text="Zona" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="col-md-2">
                        <telerik:RadComboBox ID="cboZona" runat="server"  Width="100%"  Enabled="true" >
                        </telerik:RadComboBox>
                </div>

                  <div class="col-md-1">
                    <asp:Label ID="lblExpandir" runat="server" Text="Expandir" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="col-md-2">
                        <asp:CheckBox runat="server" ID="chkExpandir" />
                </div>

            </div>
            <div class="col-md-12">
                 <div class="col-md-1">
                    <asp:Label runat="server" ID="lblPeriodo" Text="Periodo Inicio" CssClass="etiqueta"></asp:Label>
                </div>
                
                <div class="col-md-1">
                    <telerik:RadMonthYearPicker ID="dpPeriodoInicio" runat="server" DateInput-DateFormat="MM-yyyy" Width="100%">
                                                    </telerik:RadMonthYearPicker>
                </div>

                <div class="col-md-1">
                    <asp:Label runat="server" ID="lblPeriodoFinal" Text="Periodo Final" CssClass="etiqueta"></asp:Label>
                </div>

                <div class="col-md-1">
                    <telerik:RadMonthYearPicker ID="dpPeriodoFinal" runat="server" DateInput-DateFormat="MM-yyyy" Width="100%">
                                                    </telerik:RadMonthYearPicker>
                </div>
                
                <div class ="col-md-1">
                    <telerik:LayoutColumn Span="6">
                        <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" >
                            <Icon PrimaryIconUrl="../../Images/Icons/search-16.png"/>
                        </telerik:RadButton>
                    </telerik:LayoutColumn>
                </div>
                <div class ="col-md-1">
                    <telerik:LayoutColumn Span="6">
                            <telerik:RadButton ID="btnExcel" runat="server" Text="Excel" AlternateText="ExcelML" OnClick="btnExcel_Click">
                                <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png"/>
                            </telerik:RadButton>
                    </telerik:LayoutColumn>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
               
                <telerik:RadPivotGrid ID="grdComparativoPivot" runat="server" Width="99%" Height="700px" AllowFiltering="false" 
                                    OnNeedDataSource="grdComparativoPivot_NeedDataSource"
                                        ShowFilterHeaderZone="false" 
                                        ShowDataHeaderZone="false" ShowRowHeaderZone="false" ShowColumnHeaderZone="false" 
                                        TotalsSettings-GrandTotalsVisibility="RowsOnly"  
                                        AllowSorting="true"
                                        RowGroupsDefaultExpanded="false"
                                        AllowNaturalSort="true"
                                        RenderMode="Lightweight"
                                        >

                            <ExportSettings Excel-Format="Xlsx" OpenInNewWindow="true"></ExportSettings>

                            <ClientSettings EnableFieldsDragDrop="false" >
                                <Scrolling AllowVerticalScroll="true"></Scrolling>
                            </ClientSettings>

                            <Fields >
                                <telerik:PivotGridRowField DataField="Zona" ZoneIndex="0" SortOrder="None">
                                    <CellStyle Width="200px" />
                                </telerik:PivotGridRowField>
       
                                <telerik:PivotGridRowField DataField="Marca" ZoneIndex="1" SortOrder="None">
                                    <CellStyle Width="200px" />
                                </telerik:PivotGridRowField>
                                <telerik:PivotGridRowField DataField="SKU_Nombre" ZoneIndex="2" SortOrder="None">
                                    <CellStyle Width="200px" />
                                </telerik:PivotGridRowField>

                                <telerik:PivotGridAggregateField  DataField="VentaReal" DataFormatString="{0:##,###0.##}"  CellStyle-Width="80px">
                                    <HeaderCellTemplate >
                                        Venta Real US$
                                    </HeaderCellTemplate> 
                                </telerik:PivotGridAggregateField>

                                <telerik:PivotGridAggregateField  DataField="VentaPronosticada"  DataFormatString="${0:##,###0.##}"  CellStyle-Width="80px">
                                    <HeaderCellTemplate >
                                        Venta Pronosticada US$
                                    </HeaderCellTemplate>
                                </telerik:PivotGridAggregateField>

                                <telerik:PivotGridAggregateField  DataField="VentaPresupuestada"  DataFormatString="${0:##,###0.##}" CellStyle-Width="80px">
                                    <HeaderCellTemplate >
                                        Venta Presupuestada US$
                                    </HeaderCellTemplate>
                                </telerik:PivotGridAggregateField>

                                <telerik:PivotGridAggregateField  DataField="VentaAnterior"  DataFormatString="${0:##,###0.##}" CellStyle-Width="80px">
                                    <HeaderCellTemplate >
                                        Venta Año Anterior US$
                                    </HeaderCellTemplate>
                                </telerik:PivotGridAggregateField>

                                <telerik:PivotGridAggregateField  DataField="CumplimientoPronostico"  DataFormatString="{0:##,###0.##}%"  CellStyle-Width="80px"
                                     CalculationDataFields="VentaReal,VentaPronosticada" CalculationExpression="({0}/{1})*100">
                                    <HeaderCellTemplate >
                                        % Cumplimiento (Real / Pronostico)
                                    </HeaderCellTemplate>
                                </telerik:PivotGridAggregateField>

                                <telerik:PivotGridAggregateField  DataField="CumplimientoPresupuesto"  DataFormatString="{0:##,###0.##}%"  CellStyle-Width="80px"
                                     CalculationDataFields="VentaReal,VentaPresupuestada" CalculationExpression="({0}/{1})*100">
                                    <HeaderCellTemplate >
                                        % Cumplimiento (Real / Presupuesto)
                                    </HeaderCellTemplate>
                                </telerik:PivotGridAggregateField>

                                <telerik:PivotGridAggregateField  DataField="CumplimientoAnterior" DataFormatString="{0:##,###0.##}%"  CellStyle-Width="80px"
                                     CalculationDataFields="VentaReal,VentaAnterior" CalculationExpression="({0}/{1})*100">
                                    <HeaderCellTemplate>
                                        % Cumplimiento (Real / Año Anterior)
                                    </HeaderCellTemplate>
                                </telerik:PivotGridAggregateField>

                            </Fields>
                            
                </telerik:RadPivotGrid>
            </div>
        </div>
        
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
     <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
