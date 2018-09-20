<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmReporteVenta_Rentabilidad.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Reportes.ReporteBI.frmReporteVenta_Rentabilidad" %>
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

             <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpVendedor" ControlID="rapVendedor"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="gsReporteVentas_Zonas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gsReporteVentas_Zonas" LoadingPanelID="ralpVendedor"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

     <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="false" />

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
                            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Reporte de Ventas - Rentabilidad por Zonas"></asp:Label>
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
                                                <div class="colum5">

                                                     <telerik:RadButton RenderMode="Lightweight" ID="rbContraer" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Radio"
                                                        Text="Contraer" Checked="true" Skin="Metro">
                                                    </telerik:RadButton>
                                                </div>
                                                 <div class="colum5">
                                                     <telerik:RadButton RenderMode="Lightweight" ID="rbExpandir" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Radio"
                                                            Text="Expandir" Skin="Metro">
                                                        </telerik:RadButton>
                                                </div>
                                            </div>

                                            <div class="fila">
                                                <div class="colum4">
                                                    <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click">
                                                        <Icon PrimaryIconUrl="~/Images/Icons/search-16.png" />
                                                    </telerik:RadButton>
                                                </div>
                                            </div>
                                        </telerik:RadSlidingPane>
                                    </telerik:RadSlidingZone>
                                </telerik:RadPane>
                                <telerik:RadPane ID="RadPane2" runat="server" Width="100%" Scrolling="None" Height="100%">

                                       <div class="row">
                                             <div class="col-md-11">
                                                 
                                              </div>
                                             <div class="col-md-1">
                                                <telerik:RadButton ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click">
                                                    <Icon PrimaryIconUrl="~/Images/Icons/excel-16.png"/>
                                                </telerik:RadButton>
                                              </div>
                                       </div>
                                   

                                       <div class="row">
                                            <div class="col-md-12">
                                                 <telerik:RadPivotGrid  
                                            Height="520px" ID="gsReporteVentas_Zonas" runat="server" 
                                            ColumnHeaderZoneText="ColumnHeaderZone"  Skin="Silk"  AllowFiltering="False" 

                                            ShowFilterHeaderZone="False" ShowDataHeaderZone="False"   ShowRowHeaderZone="False" 
                                            ShowColumnHeaderZone="False" EnableConfigurationPanel="false" RowGroupsDefaultExpanded="False"

                                            TotalsSettings-GrandTotalsVisibility="RowsOnly" 
                                            OnNeedDataSource="gsReporteVentas_Zonas_NeedDataSource"
                                            OnCellDataBound="gsReporteVentas_Zonas_CellDataBound"
                                            OnPreRender="gsReporteVentas_Zonas_PreRender"
                                            OnItemNeedCalculation="gsReporteVentas_Zonas_ItemNeedCalculation"

                                            DataCellStyle-Height="10px"   RowHeaderCellStyle-Font-Size="X-Small"
                                            RowHeaderCellStyle-Height="10px" PagerStyle-Font-Size="X-Small"

                                            OnPivotGridCellExporting="RadPivotGrid1_PivotGridCellExporting"
                                                  
                                            >

                                         
                                            <ClientSettings EnableFieldsDragDrop="false" >
                                                <Scrolling AllowVerticalScroll="true"></Scrolling>
                                               
                                            </ClientSettings>


                                            <Fields >

                                                <telerik:PivotGridRowField DataField="Tipo" ZoneIndex="0">
                                                      <CellStyle Width="100" Font-Size="X-Small" />
                                                </telerik:PivotGridRowField>

                                                <telerik:PivotGridRowField DataField="Nombre_Zona" ZoneIndex="1">
                                                      <CellStyle Width="240" Font-Size="X-Small"  />
                                                </telerik:PivotGridRowField>
 

                                                <telerik:PivotGridColumnField DataField="Año">
                                                    <CellStyle Font-Size="X-Small"/>
                                                </telerik:PivotGridColumnField>

                                               <telerik:PivotGridColumnField DataField="Mes">
                                                      <CellStyle Font-Size="X-Small"/>
                                                </telerik:PivotGridColumnField>
                

                                                 <telerik:PivotGridAggregateField DataField="SaldoDolares" Aggregate="Sum" DataFormatString="{0:##,###0}">
                                                   <HeaderCellTemplate >
                                                      Ventas
                                                   </HeaderCellTemplate>
                                                  <CellStyle Font-Size="X-Small"/>
                                                </telerik:PivotGridAggregateField>

                                                
                                                 <telerik:PivotGridAggregateField DataField="CostoDolares" Aggregate="Sum" DataFormatString="{0:##,###0}">
                                                   <HeaderCellTemplate >
                                                      Costos
                                                   </HeaderCellTemplate>
                                                   <CellStyle Font-Size="X-Small"/>
                                                </telerik:PivotGridAggregateField>
                                               

                                              <telerik:PivotGridAggregateField DataField="Margen"  Aggregate="Sum"   DataFormatString="{0:##,###0.##}"  >
                                                    <HeaderCellTemplate>
                                                      Margen
                                                    </HeaderCellTemplate>
                                               <CellStyle Font-Size="X-Small"/>
                                              </telerik:PivotGridAggregateField>

                               
                                           <telerik:PivotGridAggregateField DataField="MargenPorcentaje"  DataFormatString="{0:F0}%" CalculationDataFields="SaldoDolares,Margen" 
                                                   CalculationExpression="({1}/{0})*100">
                                                    <HeaderCellTemplate>
                                                   %Margen
                                                </HeaderCellTemplate>
                                               <CellStyle Font-Size="X-Small"/>
                                            </telerik:PivotGridAggregateField>


                                          <telerik:PivotGridAggregateField DataField="GastoVentaDirecto" Aggregate="Sum" 
                                              DataFormatString="{0:##,###0}">
                                                <HeaderCellTemplate >
                                                    G.V.Dir.
                                                </HeaderCellTemplate>
                                                 <CellStyle Font-Size="X-Small"/>
                                            </telerik:PivotGridAggregateField>

                                            <telerik:PivotGridAggregateField DataField="MargenGASTOD"  DataFormatString="{0:F0}%" CalculationDataFields="SaldoDolares,GastoVentaDirecto" 
                                                   CalculationExpression="({1}/{0})*100">
                                                    <HeaderCellTemplate>
                                                   %GD
                                                </HeaderCellTemplate>
                                                 <CellStyle Font-Size="X-Small"/>
                                            </telerik:PivotGridAggregateField>

                                       
                                          <telerik:PivotGridAggregateField DataField="GastoVentaIndirecto" Aggregate="Sum" 
                                        
                                              DataFormatString="{0:##,###0}">
                                                <HeaderCellTemplate >
                                                    G.V.Ind.
                                                </HeaderCellTemplate>
                                                    <CellStyle Font-Size="X-Small"/>
                                            </telerik:PivotGridAggregateField>

                                                  <telerik:PivotGridAggregateField DataField="MargenGASTOI"  DataFormatString="{0:F0}%" CalculationDataFields="SaldoDolares,GastoVentaIndirecto" 
                                                   CalculationExpression="({1}/{0})*100">
                                                    <HeaderCellTemplate>
                                                   %GI
                                                </HeaderCellTemplate>
                                                   <CellStyle Font-Size="X-Small"/>
                                            </telerik:PivotGridAggregateField>


                                            <telerik:PivotGridAggregateField DataField="GastoVentaDolares" Aggregate="Sum" DataFormatString="{0:##,###0}">
                                                <HeaderCellTemplate >
                                                    G.V.
                                                </HeaderCellTemplate>
                                                  <CellStyle Font-Size="X-Small"/>
                                            </telerik:PivotGridAggregateField>
 
                                             <telerik:PivotGridAggregateField DataField="MargenGASTO"  DataFormatString="{0:F0}%" CalculationDataFields="SaldoDolares,GastoVentaDolares" 
                                                   CalculationExpression="({1}/{0})*100">
                                                    <HeaderCellTemplate>
                                                   %G
                                                </HeaderCellTemplate>
                                                  <CellStyle Font-Size="X-Small"/>
                                            </telerik:PivotGridAggregateField>
 

                                           <telerik:PivotGridAggregateField DataField="UtilidadOperativa" Aggregate="Sum" DataFormatString="{0:##,###0}">
                                                <HeaderCellTemplate >
                                                    Ut.Op.Zona
                                                </HeaderCellTemplate>
                                                   <CellStyle Font-Size="X-Small"/>
                                            </telerik:PivotGridAggregateField>

  
                                              <telerik:PivotGridAggregateField DataField="UtilidadOperativaP"  DataFormatString="{0:F0}%" CalculationDataFields="SaldoDolares,UtilidadOperativa" 
                                                    CalculationExpression="({1}/{0})*100">
                                                     <HeaderCellTemplate>
                                                      %Ut.Op.Zona
                                                     </HeaderCellTemplate>
                                                       <CellStyle Font-Size="X-Small"/>
                                              </telerik:PivotGridAggregateField>



                                                <telerik:PivotGridAggregateField DataField="GastoAdministrativoD" Aggregate="Sum" DataFormatString="{0:##,###0}">
                                                   <HeaderCellTemplate >
                                                      G.A.
                                                   </HeaderCellTemplate>
                                                      <CellStyle Font-Size="X-Small"/>
                                                </telerik:PivotGridAggregateField>

                                                   <telerik:PivotGridAggregateField DataField="GastoAdministrativoD_P"  DataFormatString="{0:F0}%" CalculationDataFields="SaldoDolares,GastoAdministrativoD" 
                                                    CalculationExpression="({1}/{0})*100">
                                                     <HeaderCellTemplate>
                                                      %G.A.
                                                     </HeaderCellTemplate>
                                                       <CellStyle Font-Size="X-Small"/>
                                              </telerik:PivotGridAggregateField>

                                               <telerik:PivotGridAggregateField DataField="UtilidadGA" Aggregate="Sum" DataFormatString="{0:##,###0}">
                                                    <HeaderCellTemplate >
                                                        Ut.Op.GA
                                                    </HeaderCellTemplate>
                                                     <CellStyle Font-Size="X-Small"/>
                                                </telerik:PivotGridAggregateField>


                                                <telerik:PivotGridAggregateField DataField="UtilidadGAP"  DataFormatString="{0:F0}%" CalculationDataFields="SaldoDolares,UtilidadGA" 
                                                    CalculationExpression="({1}/{0})*100">
                                                     <HeaderCellTemplate>
                                                      %Ut.Op.GA
                                                     </HeaderCellTemplate>
                                                       <CellStyle Font-Size="X-Small"/>
                                              </telerik:PivotGridAggregateField>
 

                                               <telerik:PivotGridAggregateField DataField="GastoFinanDolares" Aggregate="Sum" DataFormatString="{0:##,###0}">
                                                   <HeaderCellTemplate >
                                                      G.Finan.
                                                   </HeaderCellTemplate>
                                                     <CellStyle Font-Size="X-Small"/>
                                                </telerik:PivotGridAggregateField>

                                                <telerik:PivotGridAggregateField DataField="GastoFinanDolares_P"  DataFormatString="{0:F0}%" CalculationDataFields="SaldoDolares,GastoFinanDolares" 
                                                    CalculationExpression="({1}/{0})*100">
                                                        <HeaderCellTemplate>
                                                        %GFian.
                                                        </HeaderCellTemplate>
                                                         <CellStyle Font-Size="X-Small"/>
                                                </telerik:PivotGridAggregateField>

                                                <telerik:PivotGridAggregateField DataField="GastoOtrosDolares" Aggregate="Sum" DataFormatString="{0:##,###0}">
                                                   <HeaderCellTemplate >
                                                      G.Otros.
                                                   </HeaderCellTemplate>
                                                     <CellStyle Font-Size="X-Small"/>
                                                </telerik:PivotGridAggregateField>

                                                <telerik:PivotGridAggregateField DataField="GastoOtrosDolares_P"  DataFormatString="{0:F0}%" CalculationDataFields="SaldoDolares,GastoOtrosDolares" 
                                                    CalculationExpression="({1}/{0})*100">
                                                        <HeaderCellTemplate>
                                                        %GOtros
                                                        </HeaderCellTemplate>
                                                        <CellStyle Font-Size="X-Small"/>
                                                </telerik:PivotGridAggregateField>


                                                <telerik:PivotGridAggregateField DataField="UtilidadGF" Aggregate="Sum" DataFormatString="{0:##,###0}">
                                                   <HeaderCellTemplate >
                                                      Util.Ejerc.
                                                   </HeaderCellTemplate>
                                                    <CellStyle Font-Size="X-Small"/>
                                                </telerik:PivotGridAggregateField>

                                         
                                                   <telerik:PivotGridAggregateField DataField="Participacion" Aggregate="Sum" DataFormatString="{0:##,###0}">
                                                   <HeaderCellTemplate >
                                                     Participación
                                                   </HeaderCellTemplate>
                                                    <CellStyle Font-Size="X-Small"/>
                                                </telerik:PivotGridAggregateField>

                                               <telerik:PivotGridAggregateField DataField="Impuestos" Aggregate="Sum" DataFormatString="{0:##,###0}">
                                                   <HeaderCellTemplate >
                                                     Impuestos
                                                   </HeaderCellTemplate>
                                                   <CellStyle Font-Size="X-Small"/>
                                                </telerik:PivotGridAggregateField>

                                                   <telerik:PivotGridAggregateField DataField="UtilidadNeta" Aggregate="Sum" DataFormatString="{0:##,###0}">
                                                   <HeaderCellTemplate >
                                                     Util.Neta
                                                   </HeaderCellTemplate>
                                                     <CellStyle Font-Size="X-Small"/>
                                                </telerik:PivotGridAggregateField>
  

                                                <telerik:PivotGridAggregateField DataField="UtilidadNeta_P"  DataFormatString="{0:F0}%" CalculationDataFields="SaldoDolares,UtilidadNeta" 
                                                    CalculationExpression="({1}/{0})*100">
                                                        <HeaderCellTemplate>
                                                        %Util.Neta
                                                        </HeaderCellTemplate>
                                                          <CellStyle Font-Size="X-Small"/>
                                                </telerik:PivotGridAggregateField>

      
                                            </Fields>
                                            <SortExpressions>
                                                <telerik:PivotGridSortExpression FieldName="SaldoDolares" SortOrder="Descending"></telerik:PivotGridSortExpression>
                                            </SortExpressions>
                                      
                                        </telerik:RadPivotGrid>
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
