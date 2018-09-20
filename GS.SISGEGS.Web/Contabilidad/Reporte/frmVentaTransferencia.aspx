<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmVentaTransferencia.aspx.cs" Inherits="GS.SISGEGS.Web.Contabilidad.Reporte.frmVentaTransferencia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Ventas de tipo transferencia
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
    
    <telerik:RadAjaxPanel ID="rapReporte" runat="server" Height="90%" Width="100%" ClientEvents-OnRequestStart="requestStart">
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Height="100%" Width="100%">
            <Rows>
                <telerik:LayoutRow>
                    <Content>
                        <div class="col-md-11">
                            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Ventas de tipo transferencia"></asp:Label>
                        </div> 
                        <div class="col-md-1">
                            <telerik:RadButton ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_OnClick">
                                <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png"/>
                            </telerik:RadButton>
                        </div>   
                        <div class="col-md-1">
                            <telerik:RadButton ID="btnGraph" runat="server" Text="Grafico" OnClick="btnGraph_OnClick">
                                <Icon PrimaryIconUrl="../../Images/Icons/analytics-16.png"/>
                            </telerik:RadButton>
                        </div>   
                        <div class="col-md-1">
                            <telerik:RadButton ID="btnBack" runat="server" Text="Regresar" OnClick="btnBack_OnClick" Visible="False">
                                <Icon PrimaryIconUrl="../../Images/Icons/arrowLeft-16.png"/>
                            </telerik:RadButton>
                        </div>                                          
                    </Content>
                </telerik:LayoutRow>
                <telerik:LayoutRow Height="95%" ID="PanelGrid" runat="server">
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
                                                    <asp:Label ID="lblMoneda" runat="server" Text="Moneda" CssClass="etiqueta"></asp:Label>
                                                </div>
                                                <div class="colum6">
                                                
                                                    <asp:DropDownList  ID="ddlMoneda" runat="server" Font-Size="8pt" Width="100px">
                                                        <asp:ListItem Value="0" Selected="True"> Dolares</asp:ListItem>
                                                        <asp:ListItem Value="1"> Soles</asp:ListItem>
                                                    </asp:DropDownList>
                                                
                                               </div>
                                            </div>
                                            <div class="fila">
                                                <div class="colum4">
                                                    <asp:Label ID="lblFormaPago" runat="server" Text="Forma Pago" CssClass="etiqueta"></asp:Label>
                                                </div>
                                                <div class="colum6">
                                                    <asp:DropDownList  ID="ddlFormaPago" runat="server" Font-Size="8pt" Width="100px">
                                                        <asp:ListItem Value="0" Selected="True">Contado</asp:ListItem>
                                                        <asp:ListItem Value="1">Crédito</asp:ListItem>
                                                        <asp:ListItem Value="2">Transferencia Gratuita</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="fila">
                                                <div class="colum4">
                                                    <asp:Label ID="lblKardex" runat="server" Text="Kardex" CssClass="etiqueta"></asp:Label>
                                                </div>
                                                <div class="colum6">
                                                    <asp:TextBox runat="server" ID="txtKardex" Width="100px"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="fila">
                                                <div class="colum4">
                                                    <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_OnClick">
                                                        <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                                                    </telerik:RadButton>
                                                </div>
                                            </div>
                                        </telerik:RadSlidingPane>
                                    </telerik:RadSlidingZone>
                                </telerik:RadPane>
                                <telerik:RadPane ID="RadPane2" runat="server" Width="100%" Scrolling="None" Height="100%">
                                    
                                    <telerik:RadGrid ID="grdVentas" runat="server" Height="100%" Width="100%" OnNeedDataSource="grdVentas_OnNeedDataSource"
                                        AllowPaging="True" VirtualItemCount="100000" ShowStatusBar="true" AllowCustomPaging="True">
                                        <PagerStyle Mode="NextPrevAndNumeric" Position="Bottom" ></PagerStyle>
                                        <MasterTableView TableLayout="Fixed">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Kardex" HeaderText="Kardex" UniqueName="Kardex">
                                                    <HeaderStyle Width="100px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripcion" UniqueName="Descripcion">
                                                    <HeaderStyle Width="100px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Cantidad" HeaderText="Cantidad" UniqueName="Cantidad">
                                                    <HeaderStyle Width="100px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ValorVenta" HeaderText="Valor Venta" UniqueName="ValorVenta">
                                                    <HeaderStyle Width="100px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CostoVenta" HeaderText="Costo Venta" UniqueName="CostoVenta">
                                                    <HeaderStyle Width="100px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridCalculatedColumn DataFields="CostoVentaUnidad,PrecioVentaUnidad" Expression="iif({1} = 0, 0, 1-({0}/{1}))" HeaderText="Margen">
                                                    <HeaderStyle Width="100px"/>
                                                </telerik:GridCalculatedColumn>
                                                <telerik:GridBoundColumn DataField="Zona" HeaderText="Zona" UniqueName="Zona">
                                                    <HeaderStyle Width="100px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PrecioVentaUnidad" HeaderText="Precio Unitario" UniqueName="PrecioVentaUnidad">
                                                    <HeaderStyle Width="100px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CostoVentaUnidad" HeaderText="Costo Unitario" UniqueName="CostoVentaUnidad">
                                                    <HeaderStyle Width="100px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Moneda" HeaderText="Moneda" UniqueName="Moneda">
                                                    <HeaderStyle Width="100px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Año" HeaderText="Año" UniqueName="Año">
                                                    <HeaderStyle Width="100px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Mes" HeaderText="Mes" UniqueName="Mes">
                                                    <HeaderStyle Width="100px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Cliente" HeaderText="Cliente" UniqueName="Cliente">
                                                    <HeaderStyle Width="100px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Vendedor" HeaderText="Vendedor" UniqueName="Vendedor">
                                                    <HeaderStyle Width="100px"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Codigo" HeaderText="Codigo Kardex" UniqueName="Codigo">
                                                    <HeaderStyle Width="100px"/>
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <ExportSettings Excel-Format="Html" OpenInNewWindow="true"/>
                                        <ClientSettings>
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true"/>
                                            <Selecting AllowRowSelect="true"/>
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                    
                                </telerik:RadPane>
                                
                            </telerik:RadSplitter>
                        </telerik:LayoutColumn>
                    </Columns>
                </telerik:LayoutRow>
                <telerik:LayoutRow Height="95%" id="PanelGraph" Visible="False" runat="server">
                    <Columns>
                        <telerik:LayoutColumn Span="12" Height="100%">
                            <telerik:RadSplitter RenderMode="Lightweight" ID="RadSplitter2" runat="server" Width="100%" Height="100%"
                                Orientation="Vertical">
                         <telerik:RadPane ID="RadPane3" runat="server" Width="90%" Scrolling="None" Height="100%">
                                    <div class="row">
                                        <asp:Label ID="Label1" runat="server" Text="11     Empresa:  " CssClass="etiqueta"></asp:Label>
                                        <asp:Label ID="Label2" runat="server" Text="" CssClass="etiqueta"></asp:Label>
                                    </div>
                                    <div class="row">
                                        <asp:Label ID="Label3" runat="server" Text="11     Cuadro Comparativo:  " CssClass="etiqueta"></asp:Label>
                                        <asp:Label ID="Label4" runat="server" Text="" CssClass="etiqueta"></asp:Label>
                                    </div>
                                    <div class="row">
                                        <asp:Label ID="Label5" runat="server" Text="11     Moneda:  " CssClass="etiqueta"></asp:Label>
                                        <asp:Label ID="Label6" runat="server" Text="" CssClass="etiqueta"></asp:Label>
                                    </div>
                                    <div class="row">
                                    <telerik:RadPivotGrid ID="grdGraph" runat="server" Visible="False" Width="95%" Height="95%" AllowFiltering="false" ShowFilterHeaderZone="false"
                                        ShowDataHeaderZone="false" ShowRowHeaderZone="True" ShowColumnHeaderZone="false" EnableConfigurationPanel="true" TotalsSettings-GrandTotalsVisibility="None">
                                        <ClientSettings EnableFieldsDragDrop="false">
                                            <Scrolling AllowVerticalScroll="true"></Scrolling>
                                        </ClientSettings>
                                        <Fields>
                                            <telerik:PivotGridRowField DataField="Mes" ZoneIndex="0">
                                                <CellStyle Width="100px" />
                                            </telerik:PivotGridRowField>
                                            
                                            <telerik:PivotGridColumnField DataField="Año">
                                            </telerik:PivotGridColumnField>
                                            <telerik:PivotGridAggregateField DataField="ValorVenta" Aggregate="Sum" DataFormatString="{0:F0}">
                                                <HeaderCellTemplate>
                                                    Venta
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                            <telerik:PivotGridAggregateField DataField="CostoVenta" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Costo Venta
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                            
                                        </Fields>
                                       <%-- <SortExpressions>
                                            <telerik:PivotGridSortExpression FieldName="ValorVenta" SortOrder="Descending"></telerik:PivotGridSortExpression>
                                        </SortExpressions>
                                        <TotalsSettings RowsSubTotalsPosition="None" />--%>
                                    </telerik:RadPivotGrid>
                                    </div>
                                </telerik:RadPane> 
                                </telerik:RadSplitter>  
                        </telerik:LayoutColumn>
                    
                    </Columns>
                    
                </telerik:LayoutRow>
               
            </Rows>
        
      
        </telerik:RadPageLayout>
        <br />
        <br />
        <br />
        <div class="col-md-12">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
        
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
</asp:Content>
