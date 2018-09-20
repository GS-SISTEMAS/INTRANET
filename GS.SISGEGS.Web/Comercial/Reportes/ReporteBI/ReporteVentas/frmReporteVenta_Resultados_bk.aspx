<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmReporteVenta_Resultados_bk.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Reportes.ReporteBI.frmReporteVenta_Resultados_bk" %>
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

            <telerik:AjaxSetting AjaxControlID="gsReporteVentas_Familia">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gsReporteVentas_Familia" LoadingPanelID="ralpVendedor"></telerik:AjaxUpdatedControl>
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
                            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Estado Resultados - Zona"></asp:Label>
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
                                        <telerik:RadSlidingPane ID="RadSlidingPane1" runat="server" Width="350px" Title="Filtros de Busqueda"
                                            EnableDock="false" MinWidth="225" MinHeight="225" Scrolling="None">
                                            <div class="fila">
                                                <div class="colum4">
                                                    <asp:Label ID="lblFechaInicio" runat="server" Text="Periodo Inicio" CssClass="etiqueta" ></asp:Label>
                                                </div>
                                                <div class="colum6">
                                                    <telerik:RadMonthYearPicker ID="dpFecInicio" runat="server" DateInput-DateFormat="MM-yyyy" Width="100%" Enabled="false">
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
                                                <asp:Label ID="Label1" runat="server" Text="Zona: " CssClass="etiqueta" ></asp:Label>
                                            </div>
                                            <div class="colum6">
                                                <telerik:RadComboBox ID="cboZona" runat="server"   Width="100%"  >
                                                </telerik:RadComboBox>
                                            </div>

                                        </div>
                           
                                            <div class="fila">
                                                <div class="colum10">
                                                    <telerik:RadButton ID="btnBuscar"  Width="100%"  runat="server" Text="Buscar" OnClick="btnBuscar_Click">
                                                        <Icon PrimaryIconUrl="../../../Images/Icons/search-16.png" />
                                                    </telerik:RadButton>
                                                </div>
                                            </div>
                                        </telerik:RadSlidingPane>
                                    </telerik:RadSlidingZone>
                                </telerik:RadPane>
                                <telerik:RadPane ID="RadPane2" runat="server" Width="90%" Scrolling="None" Height="90%">

                                                      <telerik:RadPivotGrid  Height="560px" Width="95%"
                                            ID="gsReporteVentas_Familia" runat="server" ColumnHeaderZoneText="ColumnHeaderZone"
                                           
                                            AllowFiltering="False" 

                                            ShowFilterHeaderZone="False"
                                            ShowDataHeaderZone="False" 

                                            ShowRowHeaderZone="False" 
                                            ShowColumnHeaderZone="False" 

                                            EnableConfigurationPanel="false"

                                            RowGroupsDefaultExpanded="true"

                                            TotalsSettings-GrandTotalsVisibility="None"
                                                          TotalsSettings-ColumnGrandTotalsPosition="None"
                                                         TotalsSettings-ColumnsSubTotalsPosition="None"
                                                           TotalsSettings-RowsSubTotalsPosition="None"
                                                           TotalsSettings-RowGrandTotalsPosition="None"
                                                  
                                                    


                                            OnNeedDataSource="gsReporteVentas_Familia_NeedDataSource"
                                            
                                               OnCellDataBound="gsReporteVentas_Familia_CellDataBound"
                                            OnPreRender="gsReporteVentas_Familia_PreRender"


                                            DataCellStyle-Height="10px"
                                            RowHeaderCellStyle-Font-Size="Small"
                                            RowHeaderCellStyle-Height="10px"

                                             PagerStyle-Font-Size="Small"

                                            >

                                            <ClientSettings EnableFieldsDragDrop="false"
                                                 >
                                                <Scrolling AllowVerticalScroll="true"></Scrolling>
                                               
                                            </ClientSettings>


                                            <Fields >
                                                <telerik:PivotGridRowField DataField="Orden" ZoneIndex="0">
                                                      <CellStyle Width="60" Font-Size="Small"  Height="5px"  />
                                                </telerik:PivotGridRowField>

                                                <telerik:PivotGridRowField DataField="Columna" ZoneIndex="1">
                                                      <CellStyle Width="200" Font-Size="Small" Height="5px" /> 
                                                </telerik:PivotGridRowField>

                                                <telerik:PivotGridColumnField DataField="Presentacion">
                                                    <CellStyle Font-Size="Small"  Height="5px"/>
                                                </telerik:PivotGridColumnField>
                                                <telerik:PivotGridColumnField DataField="Año">
                                                    <CellStyle Font-Size="Small"  Height="5px"/>
                                                </telerik:PivotGridColumnField>
                                                <telerik:PivotGridColumnField DataField="Mes">
                                                    <CellStyle Font-Size="Small"  Height="5px"/>
                                                </telerik:PivotGridColumnField>

                                                 <telerik:PivotGridColumnField DataField="Tipo">
                                                    <CellStyle Font-Size="Small"  Height="5px"/>
                                                </telerik:PivotGridColumnField>


                                                <telerik:PivotGridAggregateField DataField="ImporteMes" Aggregate="Sum" DataFormatString="{0:##,###0}" CellStyle-Width="60%">
                                                   <HeaderCellTemplate>
                                                      $
                                                   </HeaderCellTemplate>
                                                   <CellStyle Font-Size="Small"  Height="5px" Width="60%"  />
                                                </telerik:PivotGridAggregateField>

                                                 <telerik:PivotGridAggregateField DataField="PorcentajeMes" 
                                                     Aggregate="Average" DataFormatString="{0:F0}%" CellStyle-Width="40%">
                                                    <HeaderCellTemplate>
                                                      %
                                                   </HeaderCellTemplate>
                                                     <CellStyle Font-Size="Small"  Height="5px" Width="40%"  />
                                                </telerik:PivotGridAggregateField>
 
                                            </Fields>
                                            <SortExpressions>
                                                <telerik:PivotGridSortExpression FieldName="Orden" SortOrder="Ascending"></telerik:PivotGridSortExpression>
                                                 <telerik:PivotGridSortExpression FieldName="Presentacion" SortOrder="Descending"></telerik:PivotGridSortExpression>
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
