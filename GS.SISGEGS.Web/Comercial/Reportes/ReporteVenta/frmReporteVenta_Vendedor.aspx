<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmReporteVenta_Vendedor.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Reportes.ReporteVenta.frmReporteVenta_Vendedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Reporte de ventas por cliente
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("btnExcel") >= 0)
                args.set_enableAjax(false);
        }

        function Resize()
        {
            var altura = $(document).height() - 142;
            $find("<%= ramRepCliente.ClientID %>").ajaxRequest('ChangePageSize,' + altura);
            };
        window.onresize = window.onload = Resize;
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramRepCliente" runat="server" OnAjaxRequest="ramRepCliente_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramRepCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlRepCliente" LoadingPanelID="ralpRepCliente"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdProducto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlRepCliente" LoadingPanelID="ralpRepCliente"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpRepCliente" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="pnlRepCliente" runat="server" Width="100%" ClientEvents-OnRequestStart="requestStart">
        <div class="row">
            <div class="col-md-11">
                <asp:Label ID="lblTitulo" runat="server" CssClass="titulo"></asp:Label>
            </div>
            <div class="col-md-1">
                <telerik:RadButton ID="btnRegresar" runat="server" Text="Regresar" OnClick="btnRegresar_Click" CssClass="btn-success">
                    <Icon PrimaryIconUrl="../../../Images/Icons/arrowLeft-16.png"/>
                </telerik:RadButton>
            </div>
        </div>

        <div class="row">
            <telerik:RadTabStrip runat="server" ID="stripPedido" MultiPageID="rmpRepCliente" SelectedIndex="0" CssClass="col-md-12">
                <Tabs>
                    <telerik:RadTab Text="Resumen"></telerik:RadTab>
                    <telerik:RadTab Text="Venta 80%"></telerik:RadTab>
                    <telerik:RadTab Text="Venta 20%"></telerik:RadTab>
                    <telerik:RadTab Text="Cliente"></telerik:RadTab>
                    <telerik:RadTab Text="Sin Venta"></telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>

            <telerik:RadMultiPage runat="server" ID="rmpRepCliente" SelectedIndex="0" Height="100%" CssClass="col-md-12">
                <telerik:RadPageView runat="server" ID="pageResumen" CssClass="col-md-12" Height="100%">
                    <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Width="100%" Height="100%">
                        <Rows>
                            <telerik:LayoutRow Height="100%">
                                <Columns>
                                    <telerik:LayoutColumn Span="3" SpanMd="6" SpanXs="12" Height="100%">
                                        <div class="row">
                                            <telerik:RadHtmlChart ID="rhcParticipacion" Transitions="true" runat="server" Height="320px" Skin="Bootstrap">
                                                <Appearance>
                                                    <FillStyle />
                                                </Appearance>
                                                <Legend>
                                                    <Appearance Visible="false">
                                                    </Appearance>
                                                </Legend>
                                                <PlotArea>
                                                    <Series>
                                                        <telerik:DonutSeries HoleSize="20">
                                                            <LabelsAppearance DataFormatString="{0}%" Position="InsideEnd" Color="Black">
                                                            </LabelsAppearance>
                                                            <TooltipsAppearance ClientTemplate="#= category #"></TooltipsAppearance>
                                                        </telerik:DonutSeries>
                                                    </Series>
                                                </PlotArea>
                                            </telerik:RadHtmlChart>
                                        </div>
                                        <div class="row">
                                            <telerik:RadRadialGauge runat="server" ID="rrgAvance" Height="180px" Skin="Bootstrap">
                                                <Pointer Value="0">
                                                    <Cap Size="0.1" />
                                                </Pointer>
                                                <Scale Min="0" Max="100" MajorUnit="25">
                                                    <Labels Format="{0}%" />
                                                    <Ranges>
                                                        <telerik:GaugeRange Color="#c20000" From="0" To="75" />
                                                        <telerik:GaugeRange Color="#ffc700" From="75" To="90" />
                                                        <telerik:GaugeRange Color="#75af1d" From="90" To="100" />
                                                    </Ranges>
                                                </Scale>
                                            </telerik:RadRadialGauge>
                                        </div>
                                        <div class="row" style="text-align:center">
                                            <asp:Label ID="lblAvance" runat="server" CssClass="etiqueta" Width="100%"></asp:Label>
                                        </div>
                                    </telerik:LayoutColumn>
                                    <telerik:LayoutColumn Span="9" SpanMd="6" SpanXs="12" Height="100%">
                                        <div class="row" style="border: solid; border-color: black; background-color: black">
                                            <div class="col-md-2">
                                                <asp:Label ID="lblValor" runat="server" Font-Size="Medium" Text="Valor Venta" ForeColor="White" Width="100%"></asp:Label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblPronos" runat="server" Font-Size="Medium" Text="Valor Planif." ForeColor="White" Width="100%"></asp:Label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblDif" runat="server" Font-Size="Medium" Text="Diferencia" ForeColor="White" Width="100%"></asp:Label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblCTotal" runat="server" Font-Size="Medium" Text="Cli.Total" ForeColor="White" Width="100%"></asp:Label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblCVenta" runat="server" Font-Size="Medium" Text="Cli.Venta" ForeColor="White" Width="100%"></asp:Label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblCNoVenta" runat="server" Font-Size="Medium" Text="Cli.Sin Venta" ForeColor="White" Width="100%"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row" style="border: solid">
                                            <div class="col-md-2">
                                                <asp:Label ID="lblValorVenta" runat="server" Font-Size="Large" CssClass="etiqueta" Width="100%"></asp:Label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblPronostico" runat="server" Font-Size="Large" CssClass="etiqueta" Width="100%"></asp:Label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblDiferencia" runat="server" Font-Size="Large" CssClass="etiqueta" Width="100%"></asp:Label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblCantTotal" runat="server" Font-Size="Large" CssClass="etiqueta" Width="100%"></asp:Label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblCantVenta" runat="server" Font-Size="Large" CssClass="etiqueta" Width="100%"></asp:Label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblCantNoVenta" runat="server" Font-Size="Large" CssClass="etiqueta" Width="100%"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <telerik:RadHtmlChart runat="server" ID="rhcDiario" Width="100%" Height="470px" Transitions="true" Skin="Bootstrap">
                                                    <Appearance>
                                                        <FillStyle BackgroundColor="Transparent"></FillStyle>
                                                    </Appearance>
                                                    <ChartTitle Text="Venta diaria">
                                                        <Appearance Align="Center" BackgroundColor="Transparent" Position="Top">
                                                        </Appearance>
                                                    </ChartTitle>
                                                    <Legend>
                                                        <Appearance BackgroundColor="Transparent" Position="Bottom">
                                                        </Appearance>
                                                    </Legend>
                                                    <PlotArea>
                                                        <Appearance>
                                                            <FillStyle BackgroundColor="Transparent"></FillStyle>
                                                        </Appearance>
                                                        <XAxis AxisCrossingValue="0" Color="black" MajorTickType="Outside" MinorTickType="Outside"
                                                            Reversed="false">
                                                            <LabelsAppearance DataFormatString="dd/MM/yyyy" RotationAngle="60" Skip="0" Step="1">
                                                            </LabelsAppearance>
                                                            <TitleAppearance Position="Center" RotationAngle="0" Text="Días">
                                                            </TitleAppearance>
                                                            <AxisCrossingPoints>
                                                                <telerik:AxisCrossingPoint Value="0" />
                                                                <telerik:AxisCrossingPoint Value="31" />
                                                            </AxisCrossingPoints>
                                                        </XAxis>
                                                        <YAxis AxisCrossingValue="0" Color="black" MajorTickSize="1" MajorTickType="Outside"
                                                            MinorTickSize="1" MinorTickType="Outside" MinValue="0" Reversed="false">
                                                            <LabelsAppearance DataFormatString="${0}K" RotationAngle="0" Skip="0" Step="1">
                                                            </LabelsAppearance>
                                                            <TitleAppearance Position="Center" RotationAngle="0" Text="Monto">
                                                            </TitleAppearance>
                                                        </YAxis>
                                                        <AdditionalYAxes>
                                                            <telerik:AxisY Name="AdditionalAxis">
                                                                <TitleAppearance Text="Porcentaje de avance">
                                                                    <TextStyle Color="Black" />
                                                                </TitleAppearance>
                                                                <LabelsAppearance DataFormatString="{0}%">
                                                                    <TextStyle Color="Black"/>
                                                                </LabelsAppearance>
                                                            </telerik:AxisY>
                                                        </AdditionalYAxes>
                                                        <Series>
                                                            <telerik:LineSeries Name="Venta">
                                                                <%--<Appearance>
                                                                    <FillStyle BackgroundColor="#5ab7de"></FillStyle>
                                                                </Appearance>--%>
                                                                <LabelsAppearance DataFormatString="${0}K" Position="Above">
                                                                </LabelsAppearance>
                                                                <%--<LineAppearance Width="1" />--%>
                                                                <MarkersAppearance MarkersType="Circle"></MarkersAppearance>
                                                                <TooltipsAppearance BackgroundColor="#b1c85a" ClientTemplate='Fecha.: #=kendo.format(\"{0:dd/MM/yyyy}\", category)#<br/>Venta: #= "$"+kendo.toString(value) + "K"#'>
                                                                </TooltipsAppearance>
                                                            </telerik:LineSeries>
                                                            <telerik:LineSeries Name="Avance" AxisName="AdditionalAxis">
                                                                <Appearance>
                                                                    <FillStyle BackgroundColor="Red"></FillStyle>
                                                                </Appearance>
                                                                <LabelsAppearance DataFormatString="{0}%" Position="Above">
                                                                </LabelsAppearance>
                                                                <%--<LineAppearance Width="1" />--%>
                                                                <MarkersAppearance MarkersType="Square"></MarkersAppearance>
                                                                <TooltipsAppearance BackgroundColor="#b1c85a" ClientTemplate='Fecha.: #=kendo.format(\"{0:dd/MM/yyyy}\", category)#<br/>Avance: #= kendo.toString(value) + "%"#'>
                                                                </TooltipsAppearance>
                                                            </telerik:LineSeries>
                                                        </Series>
                                                    </PlotArea>
                                                </telerik:RadHtmlChart>
                                            </div>
                                        </div>
                                    </telerik:LayoutColumn>
                                </Columns>
                            </telerik:LayoutRow>
                        </Rows>
                    </telerik:RadPageLayout>
                </telerik:RadPageView>
                <telerik:RadPageView runat="server" ID="pageVenta80" CssClass="col-md-12" Height="100%">
                    <telerik:RadPageLayout ID="RadPageLayout2" runat="server" Width="100%" Height="100%">
                        <Rows>
                            <telerik:LayoutRow Height="100%">
                                <Columns>
                                    <telerik:LayoutColumn Span="12" SpanMd="12" SpanXs="12" Height="100%">
                                        <telerik:RadHtmlChart ID="rhcCliente80" runat="server" Width="100%" Height="100%">
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
                                    </telerik:LayoutColumn>
                                </Columns>
                            </telerik:LayoutRow>
                        </Rows>
                    </telerik:RadPageLayout>
                </telerik:RadPageView>
                <telerik:RadPageView runat="server" ID="pageVenta20" CssClass="col-md-12" Height="100%">
                    <telerik:RadPageLayout ID="RadPageLayout3" runat="server" Width="100%" Height="100%">
                        <Rows>
                            <telerik:LayoutRow Height="100%">
                                <Columns>
                                    <telerik:LayoutColumn Span="12" SpanMd="12" SpanXs="12" Height="100%">
                                        <telerik:RadHtmlChart ID="rhcCliente20" runat="server" Width="100%" Height="100%">
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
                                                        <TooltipsAppearance BackgroundColor="#b1c85a" ClientTemplate='Emp.: #=category#<br/>Venta: #= "$"+kendo.toString(value) + "K"#'>
                                                        </TooltipsAppearance>
                                                    </telerik:BarSeries>
                                                    <telerik:BarSeries Name="Valor Planificado">
                                                        <Appearance>
                                                            <FillStyle BackgroundColor="#93d6d8"></FillStyle>
                                                        </Appearance>
                                                        <LabelsAppearance DataFormatString="${0}K">
                                                        </LabelsAppearance>
                                                        <TooltipsAppearance BackgroundColor="#93d6d8" ClientTemplate='Emp.: #=category#<br/>Planif.: #= "$"+kendo.toString(value) + "K"#'>
                                                        </TooltipsAppearance>
                                                    </telerik:BarSeries>
                                                </Series>
                                            </PlotArea>
                                        </telerik:RadHtmlChart>
                                    </telerik:LayoutColumn>
                                </Columns>
                            </telerik:LayoutRow>
                        </Rows>
                    </telerik:RadPageLayout>
                </telerik:RadPageView>
                <telerik:RadPageView runat="server" ID="pageProducto" CssClass="col-md-12" Height="100%">
                    <telerik:RadPageLayout ID="RadPageLayout4" runat="server" Width="100%" Height="100%">
                        <Rows>
                            <telerik:LayoutRow Height="3%">
                                <Columns>
                                    <telerik:LayoutColumn Span="11">
                                    </telerik:LayoutColumn>
                                    <telerik:LayoutColumn Span="1" style="text-align:right">
                                        <telerik:RadButton ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click" ToolTip="Descargar Excel">
                                            <Icon PrimaryIconUrl="../../../Images/Icons/excel-16.png" />
                                        </telerik:RadButton>
                                    </telerik:LayoutColumn>
                                </Columns>
                            </telerik:LayoutRow>
                            <telerik:LayoutRow Height="96%">
                                <Columns>
                                    <telerik:LayoutColumn Span="12" SpanMd="12" SpanXs="12" Height="100%">
                                        <telerik:RadGrid ID="grdProducto" runat="server" Width="100%" Height="100%" AutoGenerateColumns="false" 
                                            OnItemDataBound="grdProducto_ItemDataBound" AllowFilteringByColumn="true" OnNeedDataSource="grdProducto_NeedDataSource">
                                            <ExportSettings Excel-Format="ExcelML" ExportOnlyData="true" OpenInNewWindow="true"/>
                                            <GroupingSettings CaseSensitive="false"/>
                                            <MasterTableView Width="1300px" ShowFooter="true" ShowGroupFooter="true" AllowMultiColumnSorting="true">
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="Cliente" UniqueName="Cliente" HeaderText="Cliente" ShowFilterIcon="false" CurrentFilterFunction="Contains" FilterDelay="2000" AutoPostBackOnFilter="true">
                                                        <HeaderStyle Width="300px"/>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Codigo" UniqueName="Codigo" HeaderText="Código" ShowFilterIcon="false" CurrentFilterFunction="Contains" FilterDelay="2000" AutoPostBackOnFilter="true">
                                                        <HeaderStyle Width="100px"/>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Descripcion" UniqueName="Descripcion" HeaderText="Descripción" ShowFilterIcon="false" CurrentFilterFunction="Contains" FilterDelay="2000" AutoPostBackOnFilter="true">
                                                        <HeaderStyle Width="350px"/>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Categoria" UniqueName="Categoria" HeaderText="Categoría" ShowFilterIcon="false" CurrentFilterFunction="Contains" FilterDelay="2000" AutoPostBackOnFilter="true">
                                                        <HeaderStyle Width="200px"/>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="ValorVenta" UniqueName="ValorVenta" HeaderText="Valor Venta" DataFormatString="${0:#,0}" Aggregate="Sum" AllowFiltering="false">
                                                        <HeaderStyle Width="100px"/>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Cantidad" UniqueName="Cantidad" HeaderText="Cant." DataFormatString="{0:F0}" Aggregate="Sum" AllowFiltering="false">
                                                        <HeaderStyle Width="80px"/>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="ValorPlanificado" UniqueName="ValorPlanificado" HeaderText="Valor Planif." DataFormatString="${0:#,0}" Aggregate="Sum" AllowFiltering="false">
                                                        <HeaderStyle Width="80px"/>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Diferencia" UniqueName="Diferencia" HeaderText="Diferencia" DataFormatString="${0:#,0}" Aggregate="Sum" AllowFiltering="false">
                                                        <HeaderStyle Width="80px"/>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="ValorUnitario" UniqueName="ValorUnitario" HeaderText="Valor Uni." DataFormatString="${0:##,##0.00}" AllowFiltering="false">
                                                        <HeaderStyle Width="80px"/>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="CostoVenta" UniqueName="CostoVenta" HeaderText="Costo Venta" DataFormatString="${0:#,0}" Aggregate="Sum" AllowFiltering="false">
                                                        <HeaderStyle Width="80px"/>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="CostoUnitario" UniqueName="CostoUnitario" HeaderText="Costo Uni." DataFormatString="${0:##,##0.00}" AllowFiltering="false">
                                                        <HeaderStyle Width="80px"/>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Avance" UniqueName="Avance" HeaderText="Avance" DataFormatString="{0:F0}%" Aggregate="Custom" AllowFiltering="false">
                                                        <HeaderStyle Width="80px"/>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Rentabiliad" UniqueName="Rentabiliad" HeaderText="Rentabiliad" DataFormatString="{0:F0}%" AllowFiltering="false">
                                                        <HeaderStyle Width="80px"/>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="KilosLitros" UniqueName="KilosLitros" HeaderText="Kg/Lt" DataFormatString="{0:F1}" Aggregate="Sum" AllowFiltering="false">
                                                        <HeaderStyle Width="80px"/>
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                            <ClientSettings>
                                                <Scrolling AllowScroll="true" UseStaticHeaders="true"/>
                                                <Selecting AllowRowSelect="true"/>
                                                <Resizing AllowRowResize="true" AllowResizeToFit="true"/>
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                    </telerik:LayoutColumn>
                                </Columns>
                            </telerik:LayoutRow>
                        </Rows>
                    </telerik:RadPageLayout>
                </telerik:RadPageView>
                <telerik:RadPageView runat="server" ID="pageNoVenta" CssClass="col-md-12" Height="100%">
                    <telerik:RadPageLayout ID="RadPageLayout5" runat="server" Width="100%" Height="100%">
                        <Rows>
                            <telerik:LayoutRow Height="100%">
                                <Columns>
                                    <telerik:LayoutColumn Span="12" SpanMd="12" SpanXs="12" Height="100%">
                                        <telerik:RadGrid ID="grdCliente" runat="server" Width="100%" Height="100%" AutoGenerateColumns="false" ShowFooter="True">
                                            <MasterTableView Width="790px">
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="ID_Cliente" HeaderText="Código">
                                                        <HeaderStyle Width="80px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Cliente" HeaderText="Nombre">
                                                        <HeaderStyle Width="575px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="ValorPlanificado" HeaderText="Valor Planif." DataFormatString="${0:#,0}" Aggregate="Sum">
                                                        <FooterStyle Font-Bold="true"></FooterStyle>
                                                        <HeaderStyle Width="100px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="ValorVenta" HeaderText="Valor Venta" DataFormatString="${0:#,0}" Aggregate="Sum">
                                                        <FooterStyle Font-Bold="true"></FooterStyle>
                                                        <HeaderStyle Width="100px" />
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                            <ClientSettings>
                                                <Scrolling AllowScroll="true" UseStaticHeaders="True" SaveScrollPosition="true" />
                                                <Selecting AllowRowSelect="true" />
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                    </telerik:LayoutColumn>
                                </Columns>
                            </telerik:LayoutRow>
                        </Rows>
                    </telerik:RadPageLayout>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
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