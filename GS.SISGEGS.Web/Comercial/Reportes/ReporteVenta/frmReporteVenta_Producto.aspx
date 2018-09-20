<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmReporteVenta_Producto.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Reportes.ReporteVenta.frmReporteVenta_Producto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Reporte de venta de productos
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            var altura = $(document).height() - 132;
            $('#rapRepProducto').css("height", altura + "px");
        });

        $(window).resize(function () {
            var altura = $(document).height() - 132;
            $('#rapRepProducto').css("height", altura + "px");
        });

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramRepProducto" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdProdCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapRepProducto" LoadingPanelID="ralRepProducto"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdProdVendedor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapRepProducto" LoadingPanelID="ralRepProducto"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdProducto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapRepProducto" LoadingPanelID="ralRepProducto"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapRepProducto" LoadingPanelID="ralRepProducto"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnVerTodo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapRepProducto" LoadingPanelID="ralRepProducto"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralRepProducto" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="rapRepProducto" runat="server" Width="100%" Height="95%">
        <telerik:RadPageLayout ID="RadPageLayout3" runat="server" Width="100%">
            <Rows>
                <telerik:LayoutRow>
                    <Columns>
                        <telerik:LayoutColumn Span="11" SpanMd="11" SpanXs="1">
                            <asp:Label ID="lblTitulo" runat="server" Text="Reporte de venta por producto" CssClass="titulo"></asp:Label>
                        </telerik:LayoutColumn>
                        <telerik:LayoutColumn Span="1" SpanMd="1" SpanXs="1">
                            <telerik:RadButton ID="btnVerTodo" runat="server" Text="VerTodo" OnClick="btnVerTodo_Click">
                            </telerik:RadButton>
                        </telerik:LayoutColumn>
                    </Columns>
                </telerik:LayoutRow>
            </Rows>
        </telerik:RadPageLayout>
        <telerik:RadSplitter RenderMode="Lightweight" ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Vertical">
            <telerik:RadPane ID="RadPane1" runat="server" Width="22px" Scrolling="None">
                <telerik:RadSlidingZone ID="RadSlidingZone1" runat="server" Width="22px">
                    <telerik:RadSlidingPane ID="RadSlidingPane1" runat="server" Width="250px" Title="Filtros de Busqueda"
                        EnableDock="false" MinWidth="225" MinHeight="225" Scrolling="None">
                        <telerik:RadPageLayout ID="RadPageLayout2" runat="server" Width="100%">
                            <Rows>
                                <telerik:LayoutRow>
                                    <Columns>
                                        <telerik:LayoutColumn Span="3">
                                            <asp:Label ID="lblPeriodo" runat="server" Text="Periodo" CssClass="etiqueta"></asp:Label>
                                        </telerik:LayoutColumn>
                                        <telerik:LayoutColumn Span="9">
                                            <telerik:RadMonthYearPicker ID="rmpPeriodo" runat="server" Width="100%">
                                                <DateInput runat="server" DateFormat="MM-yyyy"></DateInput>
                                            </telerik:RadMonthYearPicker>
                                        </telerik:LayoutColumn>
                                    </Columns>
                                </telerik:LayoutRow>
                                <telerik:LayoutRow>
                                    <Columns>
                                        <telerik:LayoutColumn Span="6">
                                            <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click">
                                                <Icon PrimaryIconUrl="../../../Images/Icons/search-16.png"/>
                                            </telerik:RadButton>
                                        </telerik:LayoutColumn>
                                    </Columns>
                                </telerik:LayoutRow>
                            </Rows>
                        </telerik:RadPageLayout>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
            <telerik:RadPane ID="RadPane2" runat="server" Width="100%" Height="100%" Scrolling="None">
                <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Width="100%" Height="100%">
                    <Rows>
                        <telerik:LayoutRow Height="100%">
                            <Columns>
                                <telerik:LayoutColumn Height="100%" Span="6">

                                    <telerik:RadGrid ID="grdProducto" runat="server" Width="100%" Height="100%" AutoGenerateColumns="false" 
                                        OnSelectedIndexChanged="grdProducto_SelectedIndexChanged" OnItemDataBound="grdProducto_ItemDataBound">
                                        <MasterTableView ShowFooter="true">
                                                 <GroupByExpressions>
                                                    <telerik:GridGroupByExpression>
                                                        <SelectFields>
                                                           <telerik:GridGroupByField FieldAlias="Tipo" FieldName="TipoVenta" />
                                                        </SelectFields>

                                                        <GroupByFields>
                                                            <telerik:GridGroupByField FieldName="TipoVenta"  />
                                                        </GroupByFields>

                                                    </telerik:GridGroupByExpression>
                                                </GroupByExpressions>

                                            <Columns>

                                                <telerik:GridBoundColumn DataField="Marca" UniqueName="Marca" HeaderText="Marca" Aggregate="Count">
                                                    <HeaderStyle Width="150px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ValorVenta" UniqueName="ValorVenta" HeaderText="Valor Venta" DataFormatString="${0:#,0}" Aggregate="Sum">
                                                    <HeaderStyle Width="95px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ValorPlanificado" UniqueName="ValorPlanificado" HeaderText="Valor Planif." DataFormatString="${0:#,0}" Aggregate="Sum">
                                                    <HeaderStyle Width="95px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Avance" UniqueName="Avance" HeaderText="Avance" DataFormatString="{0:#,0}%">
                                                    <HeaderStyle Width="60px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CostoVenta" UniqueName="CostoVenta" HeaderText="Costo Venta" DataFormatString="${0:#,0}" Aggregate="Sum">
                                                    <HeaderStyle Width="60px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Rentabilidad" UniqueName="Rentabilidad" HeaderText="Rentab." DataFormatString="{0:#,0}%">
                                                    <HeaderStyle Width="60px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="KgLt" UniqueName="KgLt" HeaderText="Kg / Lt" DataFormatString="{0:#,0}" Aggregate="Sum">
                                                    <HeaderStyle Width="60px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Id_Marca" UniqueName="Id_Marca" HeaderText="Id_Marca" Display="false">
                                                </telerik:GridBoundColumn>

                                            </Columns>
                                            <FooterStyle Font-Bold="true" Font-Size="12px" />
                                        </MasterTableView>
                                        <ClientSettings EnablePostBackOnRowClick="true">
                                            <Selecting AllowRowSelect="true" />
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                            <Resizing AllowRowResize="true" AllowColumnResize="true" />
                                        </ClientSettings>
                                        <GroupingSettings ShowUnGroupButton="false" /> 
                                    </telerik:RadGrid>


                                </telerik:LayoutColumn>
                                <telerik:LayoutColumn Span="6" Height="100%">
                                    <telerik:RadTabStrip runat="server" ID="stripPedido" MultiPageID="rmpRepCliente" SelectedIndex="0">
                                        <Tabs>
                                            <telerik:RadTab Text="Producto"></telerik:RadTab>
                                            <telerik:RadTab Text="Cliente"></telerik:RadTab>
                                            <telerik:RadTab Text="Zona"></telerik:RadTab>
                                        </Tabs>
                                    </telerik:RadTabStrip>

                                    <telerik:RadMultiPage runat="server" ID="rmpRepCliente" SelectedIndex="0" Height="95%" CssClass="col-md-12" BorderStyle="Solid" BorderWidth="1px">
                                        <telerik:RadPageView runat="server" ID="pageProducto" Width="100%" Height="100%">
                                            <telerik:RadHtmlChart ID="rhcProducto" runat="server" Width="100%" Height="100%">
                                                <Legend>
                                                    <Appearance Position="Top" />
                                                </Legend>
                                                <PlotArea>
                                                    <Appearance>
                                                    </Appearance>
                                                    <YAxis>
                                                        <MajorGridLines Visible="true"></MajorGridLines>
                                                        <MinorGridLines Visible="false"></MinorGridLines>
                                                        <LabelsAppearance DataFormatString="${0}K">
                                                        </LabelsAppearance>
                                                    </YAxis>
                                                    <XAxis>
                                                        <LabelsAppearance DataFormatString="{0}"></LabelsAppearance>
                                                        <MajorGridLines Visible="false" />
                                                        <MinorGridLines Visible="false" />
                                                    </XAxis>
                                                    <Series>
                                                        <telerik:BarSeries Name="Valor Venta">
                                                            <Appearance>
                                                                <FillStyle BackgroundColor="#b1c85a"></FillStyle>
                                                            </Appearance>
                                                            <LabelsAppearance DataFormatString="${0}K">
                                                            </LabelsAppearance>
                                                            <TooltipsAppearance BackgroundColor="#b1c85a" ClientTemplate='Emp.: #=category#<br/>Venta: #= "$"+kendo.toString(value)+"K"#'>
                                                            </TooltipsAppearance>
                                                        </telerik:BarSeries>
                                                        <telerik:BarSeries Name="Valor Planificado">
                                                            <Appearance>
                                                                <FillStyle BackgroundColor="#93d6d8"></FillStyle>
                                                            </Appearance>
                                                            <LabelsAppearance DataFormatString="${0}K">
                                                            </LabelsAppearance>
                                                            <TooltipsAppearance BackgroundColor="#93d6d8" ClientTemplate='Emp.: #=category#<br/>Planif.: #= "$"+kendo.toString(value)+"K"#'>
                                                            </TooltipsAppearance>
                                                        </telerik:BarSeries>
                                                    </Series>
                                                </PlotArea>
                                            </telerik:RadHtmlChart>
                                        </telerik:RadPageView>
                                        <telerik:RadPageView runat="server" ID="pageCliente" Width="100%" Height="100%">
                                            <telerik:RadPageLayout ID="RadPageLayout5" runat="server" Width="100%" Height="100%">
                                                <Rows>
                                                    <telerik:LayoutRow Height="60%">
                                                        <Content>
                                                            <telerik:RadGrid ID="grdCliente" runat="server" AutoGenerateColumns="false" Width="100%" Height="100%" OnItemDataBound="grdCliente_ItemDataBound" OnSelectedIndexChanged="grdCliente_SelectedIndexChanged">
                                                                <MasterTableView ShowFooter="true">
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn DataField="Cliente" UniqueName="Cliente" HeaderText="Cliente">
                                                                            <HeaderStyle Width="" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="ValorVenta" UniqueName="ValorVenta" HeaderText="Valor Venta" Aggregate="Sum" DataFormatString="${0:#,0}">
                                                                            <HeaderStyle Width="" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="ValorPlanificado" UniqueName="ValorPlanificado" HeaderText="Valor Planif." Aggregate="Sum" DataFormatString="${0:#,0}">
                                                                            <HeaderStyle Width="" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Avance" UniqueName="Avance" HeaderText="Avance" DataFormatString="{0:#,0}%">
                                                                            <HeaderStyle Width="" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="CostoVenta" UniqueName="CostoVenta" HeaderText="Costo Venta" Aggregate="Sum" DataFormatString="${0:#,0}">
                                                                            <HeaderStyle Width="" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Rentabilidad" UniqueName="Rentabilidad" HeaderText="Rentab." Aggregate="Sum" DataFormatString="{0:#,0}%">
                                                                            <HeaderStyle Width="" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="KgLt" UniqueName="KgLt" HeaderText="Kg / Lt" Aggregate="Sum" DataFormatString="{0:#,0}">
                                                                            <HeaderStyle Width="" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="ID_Marca" UniqueName="ID_Marca" Display="false">
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="ID_Cliente" UniqueName="ID_Cliente" Display="false">
                                                                        </telerik:GridBoundColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                                <ClientSettings EnablePostBackOnRowClick="true">
                                                                    <Scrolling AllowScroll="true" UseStaticHeaders="true"/>
                                                                    <Selecting AllowRowSelect="true" />
                                                                </ClientSettings>
                                                            </telerik:RadGrid>
                                                        </Content>
                                                    </telerik:LayoutRow>
                                                    <telerik:LayoutRow Height="40%">
                                                        <Content>
                                                            <telerik:RadHtmlChart ID="rhcProdCliente" runat="server" Width="100%" Height="90%">
                                                                <Legend>
                                                                    <Appearance Position="Top" />
                                                                </Legend>
                                                                <PlotArea>
                                                                    <Appearance>
                                                                    </Appearance>
                                                                    <YAxis>
                                                                        <MajorGridLines Visible="true"></MajorGridLines>
                                                                        <MinorGridLines Visible="false"></MinorGridLines>
                                                                        <LabelsAppearance DataFormatString="${0}K">
                                                                        </LabelsAppearance>
                                                                    </YAxis>
                                                                    <XAxis>
                                                                        <LabelsAppearance DataFormatString="{0}"></LabelsAppearance>
                                                                        <MajorGridLines Visible="false" />
                                                                        <MinorGridLines Visible="false" />
                                                                    </XAxis>
                                                                    <Series>
                                                                        <telerik:BarSeries Name="Valor Venta">
                                                                            <Appearance>
                                                                                <FillStyle BackgroundColor="#b1c85a"></FillStyle>
                                                                            </Appearance>
                                                                            <LabelsAppearance DataFormatString="${0}K">
                                                                            </LabelsAppearance>
                                                                            <TooltipsAppearance BackgroundColor="#b1c85a" ClientTemplate='Emp.: #=category#<br/>Venta: #= "$"+kendo.toString(value)+"K"#'>
                                                                            </TooltipsAppearance>
                                                                        </telerik:BarSeries>
                                                                        <telerik:BarSeries Name="Valor Planificado">
                                                                            <Appearance>
                                                                                <FillStyle BackgroundColor="#93d6d8"></FillStyle>
                                                                            </Appearance>
                                                                            <LabelsAppearance DataFormatString="${0}K">
                                                                            </LabelsAppearance>
                                                                            <TooltipsAppearance BackgroundColor="#93d6d8" ClientTemplate='Emp.: #=category#<br/>Planif.: #= "$"+kendo.toString(value)+"K"#'>
                                                                            </TooltipsAppearance>
                                                                        </telerik:BarSeries>
                                                                    </Series>
                                                                </PlotArea>
                                                            </telerik:RadHtmlChart>
                                                        </Content>
                                                    </telerik:LayoutRow>
                                                </Rows>
                                            </telerik:RadPageLayout>
                                        </telerik:RadPageView>
                                        <telerik:RadPageView runat="server" ID="pageZona" Width="100%" Height="100%">
                                            <telerik:RadPageLayout ID="RadPageLayout6" runat="server" Width="100%" Height="100%">
                                                <Rows>
                                                    <telerik:LayoutRow Height="60%">
                                                        <Content>
                                                            <telerik:RadGrid ID="grdVendedor" runat="server" AutoGenerateColumns="false" Width="100%" Height="100%" 
                                                                OnItemDataBound="grdVendedor_ItemDataBound" OnSelectedIndexChanged="grdVendedor_SelectedIndexChanged">
                                                                <MasterTableView ShowFooter="true">
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn DataField="Zona" UniqueName="Zona" HeaderText="Zona">
                                                                            <HeaderStyle Width="" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Vendedor" UniqueName="Vendedor" HeaderText="Vendedor">
                                                                            <HeaderStyle Width="" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="ValorVenta" UniqueName="ValorVenta" HeaderText="Valor Venta" Aggregate="Sum" DataFormatString="${0:#,0}">
                                                                            <HeaderStyle Width="" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="ValorPlanificado" UniqueName="ValorPlanificado" HeaderText="Valor Planif." Aggregate="Sum" DataFormatString="${0:#,0}">
                                                                            <HeaderStyle Width="" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Avance" UniqueName="Avance" HeaderText="Avance" DataFormatString="{0:#,0}%">
                                                                            <HeaderStyle Width="" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="CostoVenta" UniqueName="CostoVenta" HeaderText="Costo Venta" Aggregate="Sum" DataFormatString="${0:#,0}">
                                                                            <HeaderStyle Width="" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Rentabilidad" UniqueName="Rentabilidad" HeaderText="Rentab." Aggregate="Sum" DataFormatString="{0:#,0}%">
                                                                            <HeaderStyle Width="" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="KgLt" UniqueName="KgLt" HeaderText="Kg / Lt" Aggregate="Sum" DataFormatString="{0:#,0}">
                                                                            <HeaderStyle Width="" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="ID_Marca" UniqueName="ID_Marca" Display="false">
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="ID_Zona" UniqueName="ID_Zona" Display="false">
                                                                        </telerik:GridBoundColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                                <ClientSettings EnablePostBackOnRowClick="true">
                                                                    <Scrolling AllowScroll="true" UseStaticHeaders="true"/>
                                                                    <Selecting AllowRowSelect="true" />
                                                                </ClientSettings>
                                                            </telerik:RadGrid>
                                                        </Content>
                                                    </telerik:LayoutRow>
                                                    <telerik:LayoutRow Height="40%">
                                                        <Content>
                                                            <telerik:RadHtmlChart ID="rhcProdVendedor" runat="server" Width="100%" Height="90%">
                                                                <Legend>
                                                                    <Appearance Position="Top" />
                                                                </Legend>
                                                                <PlotArea>
                                                                    <Appearance>
                                                                    </Appearance>
                                                                    <YAxis>
                                                                        <MajorGridLines Visible="true"></MajorGridLines>
                                                                        <MinorGridLines Visible="false"></MinorGridLines>
                                                                        <LabelsAppearance DataFormatString="${0}K">
                                                                        </LabelsAppearance>
                                                                    </YAxis>
                                                                    <XAxis>
                                                                        <LabelsAppearance DataFormatString="{0}"></LabelsAppearance>
                                                                        <MajorGridLines Visible="false" />
                                                                        <MinorGridLines Visible="false" />
                                                                    </XAxis>
                                                                    <Series>
                                                                        <telerik:BarSeries Name="Valor Venta">
                                                                            <Appearance>
                                                                                <FillStyle BackgroundColor="#b1c85a"></FillStyle>
                                                                            </Appearance>
                                                                            <LabelsAppearance DataFormatString="${0}K">
                                                                            </LabelsAppearance>
                                                                            <TooltipsAppearance BackgroundColor="#b1c85a" ClientTemplate='Emp.: #=category#<br/>Venta: #= "$"+kendo.toString(value)+"K"#'>
                                                                            </TooltipsAppearance>
                                                                        </telerik:BarSeries>
                                                                        <telerik:BarSeries Name="Valor Planificado">
                                                                            <Appearance>
                                                                                <FillStyle BackgroundColor="#93d6d8"></FillStyle>
                                                                            </Appearance>
                                                                            <LabelsAppearance DataFormatString="${0}K">
                                                                            </LabelsAppearance>
                                                                            <TooltipsAppearance BackgroundColor="#93d6d8" ClientTemplate='Emp.: #=category#<br/>Planif.: #= "$"+kendo.toString(value)+"K"#'>
                                                                            </TooltipsAppearance>
                                                                        </telerik:BarSeries>
                                                                    </Series>
                                                                </PlotArea>
                                                            </telerik:RadHtmlChart>
                                                        </Content>
                                                    </telerik:LayoutRow>
                                                </Rows>
                                            </telerik:RadPageLayout>
                                        </telerik:RadPageView>
                                    </telerik:RadMultiPage>
                                </telerik:LayoutColumn>
                            </Columns>
                        </telerik:LayoutRow>
                    </Rows>
                </telerik:RadPageLayout>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
