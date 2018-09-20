<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmReporteVenta_ZonasKL_bk.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Reportes.ReporteBI.frmReporteVenta_ZonasKL_bk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre - Reporte de vendedor por periodo
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

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



    <telerik:RadAjaxPanel ID="rapVendedor" runat="server" Width="100%" Height="100%">
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Width="100%" Height="100%">
            <Rows>
                <telerik:LayoutRow>
                    <Columns>
                        <telerik:LayoutColumn Span="12">
                            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Reporte de Ventas - Variación por Zonas"></asp:Label>
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
                                                        <Icon PrimaryIconUrl="../../../Images/Icons/search-16.png" />
                                                    </telerik:RadButton>
                                                </div>
                                            </div>
                                        </telerik:RadSlidingPane>
                                    </telerik:RadSlidingZone>
                                </telerik:RadPane>
                                <telerik:RadPane ID="RadPane2" runat="server" Width="100%" Scrolling="None" Height="100%">

                                    <telerik:RadPivotGrid  Height="520px"
                                            ID="gsReporteVentas_Zonas" runat="server" 
                                            ColumnHeaderZoneText="ColumnHeaderZone"
                                            AllowFiltering="False" 

                                            ShowFilterHeaderZone="False"
                                            ShowDataHeaderZone="False" 

                                            ShowRowHeaderZone="False" 
                                            ShowColumnHeaderZone="False" 

                                            EnableConfigurationPanel="false"
                                            RowGroupsDefaultExpanded="False"
                                            TotalsSettings-GrandTotalsVisibility="RowsOnly" 

                                            OnNeedDataSource="gsReporteVentas_Zonas_NeedDataSource"
                                            OnCellDataBound="gsReporteVentas_Zonas_CellDataBound"
                                            OnPreRender="gsReporteVentas_Zonas_PreRender"
                                            OnItemNeedCalculation="gsReporteVentas_Zonas_ItemNeedCalculation"
                                        
                                            RowHeaderCellStyle-Font-Size="X-Small"
                                            PagerStyle-Font-Size="X-Small"

                                            >

                                            <ClientSettings EnableFieldsDragDrop="false" >
                                                <Scrolling AllowVerticalScroll="true"></Scrolling>
                                            </ClientSettings>

                                            <Fields >

                                                  <telerik:PivotGridRowField DataField="Tipo" ZoneIndex="0"  >
                                                      <CellStyle Width="100px"/>
                                                      <CellStyle Font-Size="X-Small"/>
                                                </telerik:PivotGridRowField>

                                                <telerik:PivotGridRowField DataField="Nombre_Zona" ZoneIndex="1"  >
                                                      <CellStyle Width="200px"/>
                                                      <CellStyle Font-Size="X-Small"/>
                                                </telerik:PivotGridRowField>
                                           

                                                <telerik:PivotGridColumnField DataField="Año">
                                                    <CellStyle Font-Size="X-Small"   />
                                                </telerik:PivotGridColumnField>

                                                <telerik:PivotGridColumnField DataField="Mes">
                                                    <CellStyle Font-Size="X-Small"   />
                                                </telerik:PivotGridColumnField>

                                             
                                                 <telerik:PivotGridAggregateField DataField="SaldoDolares" Aggregate="Sum" DataFormatString="{0:##,###0}">
                                                     <HeaderCellTemplate>
                                                        Venta
                                                     </HeaderCellTemplate>
                                                     <CellStyle Font-Size="X-Small"   />
                                                 </telerik:PivotGridAggregateField>

                                                <telerik:PivotGridAggregateField  Aggregate="Sum" DataField="Venta_P" 
                                                             CalculationDataFields="SaldoDolares" CalculationExpression="{0}"  >
                                                     <TotalFormat TotalFunction="PercentOfColumnTotal" Axis="Columns"  Level="0"   />   
                                                     <CellStyle Font-Size="X-Small" />
                                                      <HeaderCellTemplate>
                                                       %Venta
                                                      </HeaderCellTemplate>
                                                  </telerik:PivotGridAggregateField>

                                                <telerik:PivotGridAggregateField DataField="VentaPPTO_Dolares" Aggregate="Sum" DataFormatString="{0:##,###0}">
                                                      <HeaderCellTemplate>
                                                        Venta_PPTO
                                                      </HeaderCellTemplate>
                                                       <CellStyle Font-Size="X-Small"   />
                                                </telerik:PivotGridAggregateField>

                                                    <telerik:PivotGridAggregateField DataField="Cumplimiento" 
                                                        CalculationDataFields="SaldoDolares,VentaPPTO_Dolares" CalculationExpression="{0}/{1}*100" DataFormatString="{0:F0}%" >
                                                    <HeaderCellTemplate>
                                                    %Cumpl.
                                                    </HeaderCellTemplate>
                                                         <CellStyle Font-Size="X-Small"   />
                                                   </telerik:PivotGridAggregateField>

                                                  <telerik:PivotGridAggregateField DataField="SaldoAnoMesAnterior_Dolares" Aggregate="Sum" DataFormatString="{0:##,###0.##}">
                                                       <HeaderCellTemplate>
                                                        Venta_2016
                                                       </HeaderCellTemplate>
                                                       <CellStyle Font-Size="X-Small"   />
                                                </telerik:PivotGridAggregateField>

                                                     <telerik:PivotGridAggregateField DataField="Crecimiento" CalculationDataFields="SaldoDolares,SaldoAnoMesAnterior_Dolares" 
                                                         CalculationExpression="((({0}/{1})-1)*100)" DataFormatString="{0:F0}%" >
                                                    <HeaderCellTemplate>
                                                    %Crecim.
                                                    </HeaderCellTemplate>
                                                          <CellStyle Font-Size="X-Small"   />
                                                   </telerik:PivotGridAggregateField>



                                                 <telerik:PivotGridAggregateField DataField="KgLt" Aggregate="Sum" DataFormatString="{0:##,###0.##}">
                                                <HeaderCellTemplate >
                                                   KgLt
                                                </HeaderCellTemplate>
                                                      <CellStyle Font-Size="X-Small"   />
                                                </telerik:PivotGridAggregateField>

                                                  <telerik:PivotGridAggregateField  Aggregate="Sum" DataField="Kglt_P" CalculationDataFields="KgLt" CalculationExpression="{0}" >
                                                       <TotalFormat TotalFunction="PercentOfColumnTotal" />     
                                                       <CellStyle Font-Size="X-Small"   />
                                                        <HeaderCellTemplate >
                                                         %KgLt
                                                        </HeaderCellTemplate>
                                                  </telerik:PivotGridAggregateField>


                                                <telerik:PivotGridAggregateField DataField="KgLtPPTO" Aggregate="Sum" DataFormatString="{0:##,###0.##}">
                                                   <HeaderCellTemplate >
                                                   KgLt_PPTO
                                                </HeaderCellTemplate>
                                                     <CellStyle Font-Size="X-Small"   />
                                                </telerik:PivotGridAggregateField>

                                                   <telerik:PivotGridAggregateField DataField="CumplimientoKL" CalculationDataFields="KgLt,KgLtPPTO" 
                                                       CalculationExpression="{0}/{1}*100" DataFormatString="{0:F2}%" >
                                                    <HeaderCellTemplate>
                                                    %CumplKL.
                                                    </HeaderCellTemplate>
                                                        <CellStyle Font-Size="X-Small"   />
                                                   </telerik:PivotGridAggregateField>

                                                <telerik:PivotGridAggregateField DataField="KgLt_AnoMesAnterior" Aggregate="Sum" DataFormatString="{0:##,###0.##}">
                                                   <HeaderCellTemplate >
                                                   KgLt_2016
                                                </HeaderCellTemplate>
                                                     <CellStyle Font-Size="X-Small"   />
                                                </telerik:PivotGridAggregateField>

                                                
                                                   <telerik:PivotGridAggregateField DataField="CrecimientoKL" CalculationDataFields="KgLt,KgLt_AnoMesAnterior" 
                                                         CalculationExpression="((({0}/{1})-1)*100)" DataFormatString="{0:F2}%" >
                                                    <HeaderCellTemplate>
                                                    %CrecimKL.
                                                    </HeaderCellTemplate>
                                                        <CellStyle Font-Size="X-Small"   />
                                                   </telerik:PivotGridAggregateField>



                                               <telerik:PivotGridAggregateField DataField="PrecioUnitario" CalculationDataFields="SaldoDolares,KgLt" 
                                                    CalculationExpression="{0}/{1}"  DataFormatString="{0:##,###0.##}"  Caption="PrecioUnitario"
                                                   >
                                                    <HeaderCellTemplate>
                                                    P.U.
                                                </HeaderCellTemplate>
                                                    <CellStyle Font-Size="X-Small"   />
                                                </telerik:PivotGridAggregateField>

                                                 <telerik:PivotGridAggregateField DataField="PrecioUnitario2016" CalculationDataFields="SaldoAnoMesAnterior_Dolares,KgLt_AnoMesAnterior" 
                                                    CalculationExpression="{0}/{1}"  DataFormatString="{0:##,###0.##}"  
                                                   >
                                                    <HeaderCellTemplate>
                                                    P.U.2016
                                                </HeaderCellTemplate>
                                                      <CellStyle Font-Size="X-Small"   />
                                                </telerik:PivotGridAggregateField>

                                                <telerik:PivotGridAggregateField 
                                                      DataField="PrecioUnitarioPPTO" CalculationDataFields="VentaPPTO_Dolares,KgLtPPTO" 
                                                    CalculationExpression="{0}/{1}"  DataFormatString="{0:##,###0.##}">
                                                    <HeaderCellTemplate>
                                                    P.U.PPTO.
                                                </HeaderCellTemplate>
                                                     <CellStyle Font-Size="X-Small"   />
                                               </telerik:PivotGridAggregateField>


                                           <telerik:PivotGridAggregateField DataField="VP_Real" 
                                                     CalculationDataFields="SaldoDolares,KgLt,SaldoAnoMesAnterior_Dolares,KgLt_AnoMesAnterior"  
                                                     CalculationExpression="((({0}/{1})-({2}/{3})))" DataFormatString="{0:##,###0.##}"  >
                                                 <HeaderCellTemplate>
                                                    Var.Real
                                                </HeaderCellTemplate>
                                                <CellStyle Font-Size="X-Small"   />
                                            </telerik:PivotGridAggregateField>

                                            <telerik:PivotGridAggregateField DataField="VP_PPTO" CalculationDataFields="SaldoDolares,KgLt,VentaPPTO_Dolares,KgLtPPTO"  
                                                     CalculationExpression="((({0}/{1})-({2}/{3})))"  DataFormatString="{0:##,###0.##}" >
                                                 <HeaderCellTemplate>
                                                    Var.PPTO
                                                </HeaderCellTemplate>
                                                 <CellStyle Font-Size="X-Small"   />
                                            </telerik:PivotGridAggregateField>

                                         
                                           <telerik:PivotGridAggregateField DataField="EfectoPr_2017x2016" 
                                                     CalculationDataFields="SaldoDolares,KgLt,SaldoAnoMesAnterior_Dolares,KgLt_AnoMesAnterior"  
                                                     CalculationExpression="((({0}/{1})-({2}/{3}))*({3}))" DataFormatString="{0:##,###0.##}"  >
                                                 <HeaderCellTemplate>
                                                    EP_2017x2016
                                                </HeaderCellTemplate>
                                                <CellStyle Font-Size="X-Small"   />
                                            </telerik:PivotGridAggregateField>

                                            <telerik:PivotGridAggregateField DataField="EfectoPr_2017xPPTO" CalculationDataFields="SaldoDolares,KgLt,VentaPPTO_Dolares,KgLtPPTO"  
                                                     CalculationExpression="((({0}/{1})-({2}/{3}))*({3}))"  DataFormatString="{0:##,###0.##}" >
                                                 <HeaderCellTemplate>
                                                    EP_2017xPPTO
                                                </HeaderCellTemplate>
                                                 <CellStyle Font-Size="X-Small"   />
                                            </telerik:PivotGridAggregateField>

                                            <telerik:PivotGridAggregateField DataField="EfectoVol_Real" CalculationDataFields="KgLt,KgLt_AnoMesAnterior,SaldoDolares"  
                                                     CalculationExpression="(({0}-{1})*({2}/{0}))"  DataFormatString="{0:##,###0.##}" >
                                                 <HeaderCellTemplate>
                                                    EV_Real
                                                </HeaderCellTemplate> 
                                                 <CellStyle Font-Size="X-Small" />
                                            </telerik:PivotGridAggregateField>

                                             <telerik:PivotGridAggregateField DataField="EfectoVol_PPTO" CalculationDataFields="KgLt,KgLtPPTO,SaldoDolares"  
                                                     CalculationExpression="(({0}-{1})*({2}/{0}))"  DataFormatString="{0:##,###0.##}" >
                                                 <HeaderCellTemplate>
                                                    EV_PPTO
                                                </HeaderCellTemplate>
                                                  <CellStyle Font-Size="X-Small"   />
                                            </telerik:PivotGridAggregateField>


                                            </Fields>
                                            <SortExpressions>
                                                <telerik:PivotGridSortExpression FieldName="SaldoDolares" SortOrder="Descending"></telerik:PivotGridSortExpression>
                                            </SortExpressions>
                                      
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
