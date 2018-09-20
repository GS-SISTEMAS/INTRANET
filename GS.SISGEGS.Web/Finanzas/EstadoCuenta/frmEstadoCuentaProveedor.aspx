<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmEstadoCuentaProveedor.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.EstadoCuenta.frmEstadoCuentaProveedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Pendientes Proveedores
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
                            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Deuda pendiente a proveedores"></asp:Label>
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

                                    <telerik:RadPivotGrid ID="grdDocumentos" runat="server" Width="100%" Height="100%" AllowFiltering="false" 

                                        ShowFilterHeaderZone="false" OnPivotGridCellExporting="grdDocumentos_PivotGridCellExporting"
                                        ShowDataHeaderZone="false" ShowRowHeaderZone="false" ShowColumnHeaderZone="false" EnableConfigurationPanel="true" 
                                        TotalsSettings-GrandTotalsVisibility="RowsOnly"  OnNeedDataSource="grdDocumentos_NeedDataSource"
                                        AllowSorting="true"
                                        RowGroupsDefaultExpanded="false"
                                        >

                                        <ExportSettings Excel-Format="Xlsx" OpenInNewWindow="true"></ExportSettings>
                                        <ClientSettings EnableFieldsDragDrop="false" >
                                            <Scrolling AllowVerticalScroll="true"></Scrolling>
                                        </ClientSettings>

                                        <Fields>
                                            <telerik:PivotGridRowField DataField="Documento" ZoneIndex="0">
                                                <CellStyle Width="150px" />
                                            </telerik:PivotGridRowField>
                                          <%--  <telerik:PivotGridRowField DataField="Kardex" ZoneIndex="1" >
                                                <CellStyle Width="50px" />
                                            </telerik:PivotGridRowField>--%>
                                            <telerik:PivotGridRowField DataField="Agenda" ZoneIndex="1">
                                                <CellStyle Width="280px" />
                                            </telerik:PivotGridRowField>
                                            <telerik:PivotGridColumnField DataField="EstadoDeuda">
                                            </telerik:PivotGridColumnField>

                                           <telerik:PivotGridAggregateField  DataField="DeudaPorVencer1a30" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    PorVencer1a30
                                                </HeaderCellTemplate>
                                           </telerik:PivotGridAggregateField>
                                         
                                           <telerik:PivotGridAggregateField DataField="DeudaPorVencer31a60" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    PorVencer31a60
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                            <telerik:PivotGridAggregateField DataField="DeudaPorVencer61a90" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    PorVencer61a90
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                           <telerik:PivotGridAggregateField DataField="DeudaPorVencer91a120" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    PorVencer91a120
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                            <telerik:PivotGridAggregateField DataField="DeudaPorVencer121a180" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    PorVencer121a180
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                                 <telerik:PivotGridAggregateField DataField="NoVencido" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Total NoVencido
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>



                                            <telerik:PivotGridAggregateField DataField="Vencido01a30" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Vencido01a30
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                            <telerik:PivotGridAggregateField DataField="Vencido31a60" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Vencido31a60
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                            <telerik:PivotGridAggregateField DataField="Vencido61a90" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Vencido61a90
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                            <telerik:PivotGridAggregateField DataField="Vencido91a120" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Vencido91a120
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                            <telerik:PivotGridAggregateField DataField="Vencido121a180" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Vencido121a180
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                            <telerik:PivotGridAggregateField DataField="Vencido181amas" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                   Vencido181aMás
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                           <telerik:PivotGridAggregateField DataField="Vencido" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Total Vencido
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>


                                           <telerik:PivotGridAggregateField CellStyle-Font-Bold="true"  DataField="Total" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate >
                                                   Grand Total
                                                </HeaderCellTemplate>
                                              
                                              
                                               <CellStyle BackColor="LightGreen" />
                                            </telerik:PivotGridAggregateField>

                                        </Fields>
                               
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
    <div class="col-md-12">
        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
    </div>
</asp:Content>
