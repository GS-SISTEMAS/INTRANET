<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmReporteCobranzas_Clientes.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.Cobranzas.ReporteCobranza.frmReporteCobranzas_Clientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Reporte de cobranzas por cliente
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("ibExcel") >= 0)
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
                        <telerik:RadTab Text="Gestión diaria"></telerik:RadTab>

                    <telerik:RadTab Text="Cobro 80%"></telerik:RadTab>
                    <telerik:RadTab Text="Cobro 20%"></telerik:RadTab>
                 
                     
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
                                            <telerik:RadRadialGauge runat="server" ID="rrgAvanceReal" Height="180px" Skin="Bootstrap">
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
                                            <asp:Label ID="lblAvanceReal" runat="server" CssClass="etiqueta" Width="100%"></asp:Label>
                                        </div>

                                       <div class="row" style="text-align:center">
                                            <asp:Label ID="Label1" runat="server" CssClass="etiqueta" Width="100%"></asp:Label>
                                        </div>

                                         <div class="row">
                                           <telerik:RadRadialGauge runat="server" ID="rrgAvanceEsperado" Height="180px" Skin="Bootstrap">
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
                                            <asp:Label ID="lblAvanceEsperado" runat="server" CssClass="etiqueta" Width="100%"></asp:Label>
                                        </div>


                                    </telerik:LayoutColumn>

                                    <telerik:LayoutColumn Span="9" SpanMd="11" SpanXs="11" Height="100%">

                                          
                                                     <div class="row"  >
                                                        <asp:Label ID="Label2" runat="server" Text="Resumen Deuda" ForeColor="Black"   CssClass="subTitulo" Width="100%"></asp:Label>
                                                    </div>
                                           

                                              <div class="row">
                                        <telerik:RadGrid ID="grdCuadro1" runat="server" 
                                        AutoGenerateColumns="false" Height="230px" Width="100%"
                                            OnNeedDataSource="grdCuadro1_NeedDataSource"
                                            OnItemDataBound="grdCuadro1_ItemDataBound"
                                       AllowSorting="True"
                                       Skin="Glow" 
                                        >
                                        <MasterTableView >

                                            <Columns>
                                              
                                                <telerik:GridBoundColumn DataField="Periodo" UniqueName="Periodo" HeaderText="Periodo"   >
                                                    <HeaderStyle Width="10%" />
                                                </telerik:GridBoundColumn>

                                               <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" DataField="ImportePendiente" UniqueName="ImportePendiente" HeaderText="ImporteTotal" DataFormatString="{0:#,##0.00}"  >
                                                    <HeaderStyle Width="8%" />
                                                </telerik:GridBoundColumn>

                                               <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" DataField="Importe_NoVencidoMenor30" UniqueName="Importe_NoVencidoMenor30" HeaderText="NoVencidoMenor30" DataFormatString="{0:#,##0.00}"  >
                                                    <HeaderStyle Width="15%" />
                                                </telerik:GridBoundColumn>

                                                       <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" DataField="Importe_NoVencido30a0" UniqueName="Importe_NoVencido30a0" HeaderText="NoVencido30a0" DataFormatString="{0:#,##0.00}"  >
                                                    <HeaderStyle Width="13%" />
                                                </telerik:GridBoundColumn>
                                            

                                                <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" DataField="Importe_01a15" UniqueName="Importe_01a15" HeaderText="Vencido01a15" DataFormatString="{0:#,##0.00}"  >
                                                    <HeaderStyle Width="10%" />
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" DataField="Importe_16a30" UniqueName="Importe_16a30" HeaderText="Vencido16a30" DataFormatString="{0:#,##0.00}" >
                                                    <HeaderStyle Width="10%" />
                                                </telerik:GridBoundColumn>

                                                     <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" DataField="Importe_31a60" UniqueName="Importe_31a60" HeaderText="Vencido31a60" DataFormatString="{0:#,##0.00}"  >
                                                    <HeaderStyle Width="10%" />
                                                </telerik:GridBoundColumn>
                                                     <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" DataField="Importe_61a90" UniqueName="Importe_61a90" HeaderText="Vencido61a90" DataFormatString="{0:#,##0.00}"  >
                                                    <HeaderStyle Width="10%" />
                                                </telerik:GridBoundColumn>
                                                     <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" DataField="Importe_91a120" UniqueName="Importe_91a120" HeaderText="Vencido91a120" DataFormatString="{0:#,##0.00}"  >
                                                    <HeaderStyle Width="10%" />
                                                </telerik:GridBoundColumn>
                                                     <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" DataField="Importe_121a240" UniqueName="Importe_121a240" HeaderText="Vencido121a240" DataFormatString="{0:#,##0.00}"  >
                                                    <HeaderStyle Width="10%" />
                                                </telerik:GridBoundColumn>
                                                     <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" DataField="Importe_240aMas" UniqueName="Importe_240aMas" HeaderText="Vencido240aMas" DataFormatString="{0:#,##0.00}"  >
                                                    <HeaderStyle Width="10%" />
                                                </telerik:GridBoundColumn>

                                            </Columns>

                                            
                                        </MasterTableView>
                                        <ClientSettings>
                                             <Scrolling AllowScroll="True"  ></Scrolling>
                                             <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                        </div>

                                            <div class="row">
                                                <asp:Label ID="Label4" runat="server" Text="&nbsp;&nbsp;&nbsp;" CssClass="subTitulo" Width="100%"></asp:Label>
                                           </div>

                                    
                                                     <div class="row" >
                                                         <asp:Label ID="Label3" runat="server" Text="Resumen Clientes" ForeColor="Black" CssClass="subTitulo" Width="100%"></asp:Label>
                                                    </div>
                   

                                        <div class="row" >
                                            
                                      <telerik:RadGrid ID="grdCuadro2" runat="server" 
                                        AutoGenerateColumns="false" Height="150px" Width="100%"
                                            OnNeedDataSource="grdCuadro2_NeedDataSource"
                                          
                                       AllowSorting="True"
                                       Skin="Glow" 
                                        >
                                        <MasterTableView >
                                            <Columns>
                                              
                                                <telerik:GridBoundColumn DataField="Periodo" UniqueName="Periodo" HeaderText="Periodo"   >
                                                    <HeaderStyle Width="10%" />
                                                </telerik:GridBoundColumn>

                                               <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Center" 
                                                   DataField="ClientesPendiente" UniqueName="ClientesPendiente" 
                                                   HeaderText="TotalClientes" DataFormatString="{0:#,##0}"  >
                                                    <HeaderStyle Width="8%" />
                                                </telerik:GridBoundColumn>

                                               <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Center" 
                                                   DataField="ClientesNoVencido" UniqueName="ClientesNoVencido" 
                                                   HeaderText="NoVencidos" DataFormatString="{0:#,##0}"  >
                                                    <HeaderStyle Width="15%" />
                                                </telerik:GridBoundColumn>

                                                       <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Center" 
                                                           DataField="Clientes01a70" UniqueName="Clientes01a70" 
                                                           HeaderText="Vencido01a70" DataFormatString="{0:#,##0}"  >
                                                    <HeaderStyle Width="13%" />
                                                </telerik:GridBoundColumn>
                                            

                                                <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Center" 
                                                    DataField="Clientes71aMas" UniqueName="Clientes71aMas" 
                                                    HeaderText="Vencido70aMas" DataFormatString="{0:#,##0}"  >
                                                    <HeaderStyle Width="10%" />
                                                </telerik:GridBoundColumn>
 
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings>
                                             <Scrolling AllowScroll="True"  ></Scrolling>
                                             <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                        </div>

                                    </telerik:LayoutColumn>
                                </Columns>
                            </telerik:LayoutRow>
                        </Rows>
                    </telerik:RadPageLayout>
                </telerik:RadPageView>

                <telerik:RadPageView runat="server" ID="pageGestion" CssClass="col-md-12" Height="100%">
                    <telerik:RadPageLayout ID="RadPageLayout4" runat="server" Width="100%" Height="100%">
                        <Rows>
                            <telerik:LayoutRow Height="100%">
                                <Columns>
                                   
                                   <telerik:LayoutColumn Span="12" SpanMd="9" SpanXs="18" Height="100%">
                                     <div class="row">
                                            <div class="col-md-12">
                                                <telerik:RadHtmlChart runat="server" ID="rhcDiario" Width="100%" Height="430px" Transitions="true" Skin="Bootstrap">
                                                    <Appearance>
                                                        <FillStyle BackgroundColor="Transparent"></FillStyle>
                                                    </Appearance>
                                                    <ChartTitle Text="Cobranza diaria">
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
                                                            <telerik:LineSeries Name="Cobranza">
                                                                <Appearance>
                                                                    <FillStyle BackgroundColor="#0000ff"></FillStyle>
                                                                </Appearance>
                                                                <LabelsAppearance DataFormatString="${0}K" Position="Above">
                                                                </LabelsAppearance>
                                                                <%--<LineAppearance Width="1" />--%>
                                                                <MarkersAppearance MarkersType="Circle"></MarkersAppearance>
                                                                <TooltipsAppearance BackgroundColor="#b1c85a" ClientTemplate='Fecha.: #=kendo.format(\"{0:dd/MM/yyyy}\", category)#<br/>Cobranza: #= "$"+kendo.toString(value) + "K"#'>
                                                                </TooltipsAppearance>
                                                            </telerik:LineSeries>

                                                            <telerik:LineSeries Name="AvanceReal" AxisName="AdditionalAxis">
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

                                                            <telerik:LineSeries Name="AvanceEsperado" >
                                                                <Appearance>
                                                                    <FillStyle BackgroundColor="#00cc00"></FillStyle>
                                                                </Appearance>
                                                                 <LabelsAppearance DataFormatString="{0}%" Position="Above">
                                                                </LabelsAppearance>
                                                                <%--<LineAppearance Width="1" />--%>
                                                                <MarkersAppearance MarkersType="Triangle"></MarkersAppearance>
                                                                <TooltipsAppearance BackgroundColor="#00cc00" ClientTemplate='Fecha.: #=kendo.format(\"{0:dd/MM/yyyy}\", category)#<br/>Esperado: #= kendo.toString(value) + "%"#'>
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
                                                    <telerik:BarSeries Name="Valor Cobrado">
                                                        <Appearance>
                                                            <FillStyle BackgroundColor="#0033cc"></FillStyle>
                                                        </Appearance>
                                                        <LabelsAppearance DataFormatString="${0}K">
                                                        </LabelsAppearance>
                                                        <TooltipsAppearance BackgroundColor="#0033cc" ClientTemplate='Emp.: #=category#<br/>Venta: #= "$"+kendo.toString(value)+"K"#'>
                                                        </TooltipsAppearance>
                                                    </telerik:BarSeries>
                                                    <telerik:BarSeries Name="Valor Planificado">
                                                        <Appearance>
                                                            <FillStyle BackgroundColor="#ff0000"></FillStyle>
                                                        </Appearance>
                                                        <LabelsAppearance DataFormatString="${0}K">
                                                        </LabelsAppearance>
                                                        <TooltipsAppearance BackgroundColor="#ff0000" ClientTemplate='Emp.: #=category#<br/>Planif.: #= "$"+kendo.toString(value)+"K"#'>
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
                                                   <telerik:BarSeries Name="Valor Cobrado">
                                                        <Appearance>
                                                            <FillStyle BackgroundColor="#0033cc"></FillStyle>
                                                        </Appearance>
                                                        <LabelsAppearance DataFormatString="${0}K">
                                                        </LabelsAppearance>
                                                        <TooltipsAppearance BackgroundColor="#0033cc" ClientTemplate='Emp.: #=category#<br/>Venta: #= "$"+kendo.toString(value) + "K"#'>
                                                        </TooltipsAppearance>
                                                    </telerik:BarSeries>
                                                    <telerik:BarSeries Name="Valor Planificado">
                                                        <Appearance>
                                                            <FillStyle BackgroundColor="#ff0000"></FillStyle>
                                                        </Appearance>
                                                        <LabelsAppearance DataFormatString="${0}K">
                                                        </LabelsAppearance>
                                                        <TooltipsAppearance BackgroundColor="#ff0000" ClientTemplate='Emp.: #=category#<br/>Planif.: #= "$"+kendo.toString(value) + "K"#'>
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
 
                

            </telerik:RadMultiPage>
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
              <asp:HiddenField ID="lblGrilla" runat="server" />
              <asp:HiddenField ID="lblGrilla2" runat="server" />
        </div>
    </div>
</asp:Content>
