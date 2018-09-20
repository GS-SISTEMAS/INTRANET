<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmReporteVenta_Items.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Reportes.ReporteBI.frmReporteVenta_Items" %>
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

            <telerik:AjaxSetting AjaxControlID="gsReporteVentas_Item">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gsReporteVentas_Item" LoadingPanelID="ralpVendedor"></telerik:AjaxUpdatedControl>
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
                            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Reporte de Ventas - Margen por Producto"></asp:Label>
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
                                                <telerik:RadPivotGrid  Height="520px"
                                            ID="gsReporteVentas_Item" runat="server" ColumnHeaderZoneText="ColumnHeaderZone"
                                            
                                            AllowFiltering="False" 

                                            ShowFilterHeaderZone="False"
                                            ShowDataHeaderZone="False" 

                                            ShowRowHeaderZone="False" 
                                            ShowColumnHeaderZone="False" 

                                            EnableConfigurationPanel="false"

                                            RowGroupsDefaultExpanded="true"

                                            TotalsSettings-GrandTotalsVisibility="RowsOnly" 
                                            OnNeedDataSource="gsReporteVentas_Item_NeedDataSource"
                                
                                            OnCellDataBound="gsReporteVentas_Item_CellDataBound"
                                            OnPreRender="gsReporteVentas_Item_PreRender"

                                            DataCellStyle-Height="10px"
                                            RowHeaderCellStyle-Font-Size="X-Small"
                                            RowHeaderCellStyle-Height="10px"
                                            OnItemNeedCalculation="gsReporteVentas_Item_ItemNeedCalculation"
                                            PagerStyle-Font-Size="X-Small"
                                            OnPivotGridCellExporting="RadPivotGrid1_PivotGridCellExporting"
                                            Skin="Silk"
                                            >

                                            <ClientSettings EnableFieldsDragDrop="false" >
                                                <Scrolling AllowVerticalScroll="true"></Scrolling>
                                               
                                            </ClientSettings>


                                            <Fields >
                                                <telerik:PivotGridRowField DataField="Tipo" ZoneIndex="0">
                                                      <CellStyle  Width="100px"  Font-Size="X-Small"  />
                                                </telerik:PivotGridRowField>

                                                <telerik:PivotGridRowField DataField="Marca" ZoneIndex="1">
                                                      <CellStyle Width="100px" Font-Size="X-Small" />
                                                </telerik:PivotGridRowField>

                                                <telerik:PivotGridRowField DataField="Nombre_Item" ZoneIndex="2">
                                                      <CellStyle Width="120px" Font-Size="X-Small" />
                                                </telerik:PivotGridRowField>

                                                <telerik:PivotGridColumnField DataField="Año">
                                                    <CellStyle Font-Size="X-Small"  Height="5px"/>
                                                </telerik:PivotGridColumnField>

                                                <telerik:PivotGridColumnField DataField="Mes">
                                                    <CellStyle Font-Size="X-Small"  Height="5px"/>
                                                </telerik:PivotGridColumnField>

                                          

                                                 <telerik:PivotGridAggregateField DataField="Cantidad" Aggregate="Sum" DataFormatString="{0:##,###0}">
                                                       <HeaderCellTemplate >
                                                   Cant
                                                </HeaderCellTemplate>
                                                  <CellStyle Font-Size="X-Small"  Height="5px" />
                                                </telerik:PivotGridAggregateField>

                                                <telerik:PivotGridAggregateField DataField="CantidadPPTO" Aggregate="Sum" DataFormatString="{0:##,###0}">
                                                   <HeaderCellTemplate >
                                                   Cant_PPTO
                                                </HeaderCellTemplate>

                                                     <CellStyle Font-Size="X-Small"  Height="5px" />
                                           
                                                </telerik:PivotGridAggregateField>

                                                <telerik:PivotGridAggregateField DataField="SaldoDolares" Aggregate="Sum" DataFormatString="{0:##,###0}">
                                                  <HeaderCellTemplate>
                                                    Venta
                                                </HeaderCellTemplate>

                                                      <CellStyle Font-Size="X-Small"  Height="5px"   />
                                                </telerik:PivotGridAggregateField>

                                                  <telerik:PivotGridAggregateField DataField="VentaPPTO_Dolares" Aggregate="Sum" DataFormatString="{0:##,###0}">
                                                  <HeaderCellTemplate>
                                                    PPTO
                                                </HeaderCellTemplate>
                                                       <CellStyle Font-Size="X-Small"  Height="5px" />
                                                </telerik:PivotGridAggregateField>

                                                
                                                <telerik:PivotGridAggregateField DataField="CostoDolares" Aggregate="Sum" DataFormatString="{0:##,###0}">
                                                  <HeaderCellTemplate>
                                                    Costo
                                                </HeaderCellTemplate>
                                                       <CellStyle Font-Size="X-Small"  Height="5px" />
                                                </telerik:PivotGridAggregateField>

                                                  <telerik:PivotGridAggregateField DataField="UtilidadDolares" Aggregate="Sum" DataFormatString="{0:##,###0}">
                                                  <HeaderCellTemplate>
                                                    Util. 
                                                </HeaderCellTemplate>
                                                       <CellStyle Font-Size="X-Small"  Height="5px" />
                                                </telerik:PivotGridAggregateField>
 

                                                <telerik:PivotGridAggregateField DataField="PUnitario_c"  UniqueName="PUnitario_c" 
                                                   CalculationDataFields="SaldoDolares,Cantidad" CalculationExpression="(({0}/{1}))"
                                                   DataFormatString="{0:##,###0.##}">
                                                  <HeaderCellTemplate>
                                                      P.U.
                                                </HeaderCellTemplate>
                                                         <CellStyle Font-Size="X-Small"  />
                                                </telerik:PivotGridAggregateField>  

                                                <telerik:PivotGridAggregateField DataField="PUnitarioPPT_c"  UniqueName="PUnitarioPPT_c" 
                                                   CalculationDataFields="VentaPPTO_Dolares,CantidadPPTO" CalculationExpression="(({0}/{1}))"
                                                   DataFormatString="{0:##,###0.##}">
                                                  <HeaderCellTemplate>
                                                      P.U.PPTO.
                                                </HeaderCellTemplate>
                                                         <CellStyle Font-Size="X-Small"  />
                                                </telerik:PivotGridAggregateField>  

                                               <telerik:PivotGridAggregateField DataField="PUnitarioAnterior_c"  UniqueName="PUnitarioAnterior_c" 
                                                   CalculationDataFields="SaldoAnoMesAnterior_Dolares,Cant_AnoMesAnterior" CalculationExpression="(({0}/{1}))"
                                                   DataFormatString="{0:##,###0.##}">
                                                         <HeaderCellTemplate>
                                                          P.U.2016
                                                         </HeaderCellTemplate>
                                                         <CellStyle Font-Size="X-Small"  />
                                                </telerik:PivotGridAggregateField>  


                                                <telerik:PivotGridAggregateField DataField="CUnitario_c"  UniqueName="CUnitario_c" 
                                                   CalculationDataFields="CostoDolares,Cantidad" CalculationExpression="(({0}/{1}))"
                                                   DataFormatString="{0:##,###0.##}">
                                                               <HeaderCellTemplate>
                                                                 C.U.
                                                               </HeaderCellTemplate>
                                                         <CellStyle Font-Size="X-Small"  />
                                                </telerik:PivotGridAggregateField>  
 
 
                                                <telerik:PivotGridAggregateField DataField="CUnitarioAnterior_c"  UniqueName="CUnitarioAnterior_c" 
                                                   CalculationDataFields="CostoAnoMesAnterior_Dolares,Cant_AnoMesAnterior" CalculationExpression="(({0}/{1}))"
                                                   DataFormatString="{0:##,###0.##}">
                                                          <HeaderCellTemplate>
                                                             C.U.2016
                                                          </HeaderCellTemplate>
                                                         <CellStyle Font-Size="X-Small"  />
                                                </telerik:PivotGridAggregateField>  
 
                                                <telerik:PivotGridAggregateField DataField="Margen_2017_c"  UniqueName="Margen_2017_c" 
                                                   CalculationDataFields="CostoDolares,Cantidad,SaldoDolares" CalculationExpression="((((({0}/{1})/({2}/{1}))-1)*100)*(-1))"
                                                   DataFormatString="{0:F0}%" >
                                                               <HeaderCellTemplate>
                                                                  %Mg17
                                                               </HeaderCellTemplate>
                                                         <CellStyle Font-Size="X-Small"  />
                                                </telerik:PivotGridAggregateField>

                                                <telerik:PivotGridAggregateField DataField="Margen_2016_c"  UniqueName="Margen_2016_c" 
                                                   CalculationDataFields="CostoAnoMesAnterior_Dolares,Cant_AnoMesAnterior,SaldoAnoMesAnterior_Dolares" 
                                                    CalculationExpression="((((({0}/{1})/({2}/{1}))-1)*100)*(-1))"
                                                   DataFormatString="{0:F0}%" >
                                                               <HeaderCellTemplate>
                                                                  %Mg16
                                                               </HeaderCellTemplate>
                                                         <CellStyle Font-Size="X-Small"  />
                                                </telerik:PivotGridAggregateField>  
 

                                               <telerik:PivotGridAggregateField DataField="VPU_2016_c"  UniqueName="VPU_2016_c" 
                                                   CalculationDataFields="SaldoDolares,Cantidad,SaldoAnoMesAnterior_Dolares,Cant_AnoMesAnterior" 
                                                   CalculationExpression="((({0}/{1})/({2}/{3}))-1)*100"
                                                    DataFormatString="{0:F1}%" >
                                                  <HeaderCellTemplate>
                                                       %Vpu16
                                                </HeaderCellTemplate>
                                                         <CellStyle Font-Size="X-Small"  />
                                                </telerik:PivotGridAggregateField>  

                                               <telerik:PivotGridAggregateField DataField="VPU_PPTO_c"  UniqueName="VPU_PPTO_c" 
                                                   CalculationDataFields="SaldoDolares,Cantidad,VentaPPTO_Dolares,CantidadPPTO" 
                                                   CalculationExpression="((({0}/{1})/({2}/{3}))-1)*100"
                                                    DataFormatString="{0:F1}%" >
                                                  <HeaderCellTemplate>
                                                        %VpuPPTO
                                                </HeaderCellTemplate>
                                                         <CellStyle Font-Size="X-Small"  />
                                                </telerik:PivotGridAggregateField>  


                                                  <telerik:PivotGridAggregateField DataField="VMg17_16_c"  UniqueName="VMg17_16_c" 
                                                   CalculationDataFields="CostoDolares,Cantidad,SaldoDolares,CostoAnoMesAnterior_Dolares,Cant_AnoMesAnterior,SaldoAnoMesAnterior_Dolares" 
                                                   CalculationExpression="((((((({0}/{1})/({2}/{1}))-1)*100)*(-1))/((((({3}/{4})/({5}/{4}))-1)*100)*(-1)))-1)*100"
                                                   DataFormatString="{0:F1}%" >
                                                               <HeaderCellTemplate>
                                                                 Var.Margen
                                                               </HeaderCellTemplate>
                                                         <CellStyle Font-Size="X-Small"  />
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
