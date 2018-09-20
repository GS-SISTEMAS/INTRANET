<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmControlLetrasSinCanje.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.Reportes.frmControlLetrasSinCanje" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Control de Letras sin Canje
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
             function requestStart(sender, args) {
                 
                 if (args.get_eventTarget().indexOf("btnExcel") >= 0)
                     args.set_enableAjax(false);
                 if (args.get_eventTarget().indexOf("btnExcelZona") >= 0)
                     args.set_enableAjax(false);
        }

        //function requestStart2(sender, args) {

        //         if (args.get_eventTarget().indexOf("btnExcelZona") >= 0)
        //             args.set_enableAjax(false);
        //     }
     </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    
    <telerik:RadAjaxManager ID="ramLetra" runat="server"> <%-- OnAjaxRequest="ramPedidoMng_AjaxRequest"--%>
        <AjaxSettings>
            <%--<telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlLetra" LoadingPanelID="ralpLetra" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>

            <telerik:AjaxSetting AjaxControlID="ramLetra">
                <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="rchardTorta" LoadingPanelID="ralpSeg" />
                </UpdatedControls>
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpSeg" runat="server">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="pnlLetra" runat="server" Width="100%" Height="100%" ClientEvents-OnRequestStart="requestStart" >
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Width="100%" Height="100%">
            <Rows>
                <telerik:LayoutRow >
                    <Columns>
                        <telerik:LayoutColumn Span="11">
                            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Control de Letras sin Canje"></asp:Label>
                        </telerik:LayoutColumn>
                    </Columns>
                </telerik:LayoutRow>
                
                <telerik:LayoutRow Height="85%">
                    <Columns>
                        <telerik:LayoutColumn Span="12" Height="100%">
                            <telerik:RadTabStrip runat="server" ID="stripLetra" MultiPageID="radmultipage" SelectedIndex="1" Style="position: relative; z-index: 1000" >
                                <Tabs>
                                    <telerik:RadTab Text="Resumen" Selected="True"></telerik:RadTab>
                                    <telerik:RadTab Text="Resumen Por Zona"></telerik:RadTab>
                                    <telerik:RadTab Text="Detalle"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>

                            <telerik:RadMultiPage runat="server" ID="radmultipage" SelectedIndex="0" Height="1100px" Width="100%"  CssClass="bordetab" BorderColor="DarkGray" BorderStyle="Solid"
                                BorderWidth="1px">
                                <telerik:RadPageView runat="server" ID="pageResumen" Height="100%">
                                    <telerik:RadPageLayout ID="RadPageLayout4" runat="server" Width="100%" Height="100%">
                                        <Rows>
                                            
                                            <telerik:LayoutRow >
                                                <Content>
                                                    
                                                    
                                                    <div class="col-md-3">
                                                        <telerik:RadDatePicker ID="dtpfechafinal" runat="server" Width="80%"  
                                                            DateInput-Label="Fecha Al" DateInput-DateFormat="dd/MM/yyyy" DateInput-LabelWidth="50%">
                                                        </telerik:RadDatePicker>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <telerik:RadComboBox  ID="cbzona" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Width="280" RenderMode="Lightweight" Label="Zona" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <telerik:RadNumericTextBox ID="txtCantidadDias" runat="server" Width="60%" Label="Días" LabelWidth="70%" Type="Number" NumberFormat-DecimalDigits="0" MaxLength="12" MinValue="0" ></telerik:RadNumericTextBox>
                                                    </div>
                                                    
                                                    <div class="col-md-1">
                                                        <telerik:RadButton ID="btnbuscar" runat="server" Text="Buscar" OnClick="btnbuscar_Click">
                                                            <Icon PrimaryIconUrl="../../Images/Icons/search-16.png"/>
                                                        </telerik:RadButton>
                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </Content>
                                            </telerik:LayoutRow>

                                            <telerik:LayoutRow CssClass="containerSubTitulo">
                                                <Content>
                                                    <div class="col-md-12">
                                                        <asp:Label ID="Label3" runat="server"  CssClass="subTitulo"></asp:Label>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>

                                            <telerik:LayoutRow >
                                                    <Content>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <asp:label Text="Saldos por Categoria" CssClass="etiqueta" runat="server" ID="lblTitulotarta" ></asp:label>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <center>
                                                                        <div class="demo-container size-wide">
                                                                        
                                                                            <%--<telerik:RadHtmlChart runat="server" ID="rchardTorta"  Transitions="true" OnDataBound="rchardTorta_DataBound" >
                                                                         
                                                                                <Legend>
                                                                                    <Appearance Position="Right" Visible="true">
                                                                                    </Appearance>
                                                                                </Legend>
                                                                              <PlotArea>
                                                                                      <Series>
                                                                                          <telerik:PieSeries StartAngle="90" DataFieldY="Cantidad" NameField="TipoPlazo">
                                                                                          
                                                                                              <LabelsAppearance DataFormatString="$#,##.00" Position="OutsideEnd"></LabelsAppearance>
                                                                                              <TooltipsAppearance DataFormatString="{0}" Color="White"></TooltipsAppearance>
                                                                                          
                                                                                          </telerik:PieSeries>
                                                                                      </Series>
                                                                                  </PlotArea>
                                                                          </telerik:RadHtmlChart>--%>
                                                                          <asp:Panel runat="server" ID="paneltorta" Width="100%" Height="100%">

                                                                          </asp:Panel>
                                                                        </div>
                                                                    </center>
                                                                </div>
                                                            </div>
                                                            
                                                           
                                                        </div>
                                                    </Content>
                                            </telerik:LayoutRow>

                                            <telerik:LayoutRow CssClass="containerSeparador">
                                                <Content>
                                                    <div class="col-md-12">
                                                        <asp:Label ID="Label1" runat="server"  CssClass="subTitulo"></asp:Label>
                                                    </div>
                                                    
                                                </Content>
                                            </telerik:LayoutRow>

                                            <telerik:LayoutRow >
                                                    <Content>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <asp:label Text="Saldos por Zonas" CssClass="etiqueta" runat="server" ></asp:label>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                            <telerik:RadHtmlChart runat="server" ID="rchardLineas" Width="1200" Height="500" Transitions="true">
                                                                                <Appearance>
                                                                                    <FillStyle BackgroundColor="Transparent"></FillStyle>
                                                                                </Appearance>
                                                                                <Legend>
                                                                                    <Appearance Visible="true">
                                                                                    </Appearance>
                                                                                </Legend>
                                                                                <PlotArea>
                                                                                    <Appearance>
                                                                                    </Appearance>
                                                                                    <YAxis>
                                                                                        <MajorGridLines Visible="true"></MajorGridLines>
                                                                                        <MinorGridLines Visible="false"></MinorGridLines>
                                                                                        <LabelsAppearance DataFormatString="#,##0.00">
                                                                                        </LabelsAppearance>
                                                                                    </YAxis>
                                                                                    <XAxis>
                                                                                        <LabelsAppearance RotationAngle="45"></LabelsAppearance>
                                                                                        <MajorGridLines Visible="false" />
                                                                                        <MinorGridLines Visible="false" />
                                                                                    </XAxis>
                                                                                    <Series>
                                                                                        <%--<telerik:BarSeries Name="Saldo">
                                                                                
                                                                                            <TooltipsAppearance BackgroundColor="#b1c85a">
                                                                                            </TooltipsAppearance>
                                                                                        </telerik:BarSeries>--%>
                                                                                        <telerik:LineSeries Name="Saldo" MissingValues="Interpolate" >
                                                                                            <Appearance>
                                                                                                <FillStyle BackgroundColor="Blue" />
                                                                                            </Appearance>
                                                                                            <TooltipsAppearance ClientTemplate="#= category #"></TooltipsAppearance>
                                                                                            <LabelsAppearance Position="Above" DataFormatString="#,##0.00" Color="DarkRed" />
                                                                                            <MarkersAppearance MarkersType="Square" BackgroundColor="Blue" />
                                                                                            <%--<TooltipsAppearance DataFormatString="#,##0.00"></TooltipsAppearance>--%>
                                                                                        </telerik:LineSeries>
                                                                                  
                                                                                    </Series>
                                                                                </PlotArea>

                                                                             
                                                                            </telerik:RadHtmlChart>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </Content>
                                            </telerik:LayoutRow>
                                        </Rows>
                                    </telerik:RadPageLayout>
                                </telerik:RadPageView>

                                <telerik:RadPageView runat="server" ID="RadPageView1" CssClass="col-md-12" Height="100%" >
                                    <telerik:RadPageLayout ID="pageResumenZona" runat="server" Height="100%" Width="100%">
                                        <Rows>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="col-md-11 containerSubTitulo">
                                                        <asp:Label ID="Label2" runat="server" Text="Total Resumen por Zonas" CssClass="subTitulo"></asp:Label>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <telerik:RadButton ID="btnExcelZona" runat="server" Text="Excel" OnClick="btnExcelZona_Click" Style="top: 1px; left: 3px" Width="100px">
                                                            <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png" />
                                                        </telerik:RadButton>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>

                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <telerik:RadPivotGrid RenderMode="Lightweight" AllowPaging="true" PageSize="100" Height="600px"
                                                                ID="rpgResumenZona" runat="server" ColumnHeaderZoneText="ColumnHeaderZone" RowGroupsDefaultExpanded="false" 
                                                                Skin="Default" OnPivotGridCellExporting="rpgResumenZona_PivotGridCellExporting" OnCellDataBound="rpgResumenZona_CellDataBound" OnNeedDataSource="rpgResumenZona_NeedDataSource" >
                                                                <ClientSettings EnableFieldsDragDrop="false">
                                                                    <Scrolling AllowVerticalScroll="true"></Scrolling>
                                                                    
                                                                </ClientSettings>
                                                                <Fields>
                                                                    <telerik:PivotGridRowField DataField="NombreZona" ZoneIndex="0" CellStyle-Width="250px">
                                                                        <CellStyle  Width="150px"  Font-Size="X-Small"  />
                                                                    </telerik:PivotGridRowField>

                                                                    <telerik:PivotGridColumnField DataField="TipoPlazoVencimiento">
                                                                        <CellStyle  Width="100px"  Font-Size="X-Small"  />
                                                                    </telerik:PivotGridColumnField>

                                                                    

                                                                    <%--<telerik:PivotGridAggregateField DataField="Importe_USD" Aggregate="Sum" DataFormatString="${0:N}" Caption="Importe $" GrandTotalAggregateFormatString="${0:N}">
                                                                        <CellStyle  Width="100px"  Font-Size="X-Small"  />
                                                                        <HeaderCellTemplate>
                                                                            Total Importe
                                                                        </HeaderCellTemplate>
                                                                        <ColumnGrandTotalHeaderCellTemplate>
                                                                            Total Importe
                                                                        </ColumnGrandTotalHeaderCellTemplate>
                                                                    </telerik:PivotGridAggregateField>--%>

                                                                    <telerik:PivotGridAggregateField DataField="Saldo" Aggregate="Sum" DataFormatString="${0:N}" Caption="Saldo $" GrandTotalAggregateFormatString="${0:N}">
                                                                        <CellStyle  Width="100px"  Font-Size="X-Small"  />
                                                                        <HeaderCellTemplate>
                                                                            Total Saldo
                                                                        </HeaderCellTemplate>
                                                                        <ColumnGrandTotalHeaderCellTemplate>
                                                                            Total Saldo
                                                                        </ColumnGrandTotalHeaderCellTemplate>
                                                                    </telerik:PivotGridAggregateField>
                                                                </Fields>
                                                                <%--<SortExpressions>
                                                                    <telerik:PivotGridSortExpression FieldName="NombreZona" SortOrder="Descending"></telerik:PivotGridSortExpression>
                                                                </SortExpressions>--%>
                                                            </telerik:RadPivotGrid>
                                                        </div>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>


                                        </Rows>
                                    </telerik:RadPageLayout>
                                </telerik:RadPageView>

                                <telerik:RadPageView runat="server" ID="pageDetalle" CssClass="col-md-12" Height="100%" >
                                    <telerik:RadPageLayout ID="RadPageLayout5" runat="server" Height="100%" Width="100%">
                                        <Rows>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="col-md-11 containerSubTitulo">
                                                        <asp:Label ID="Label4" runat="server" Text="Detalle de las letras sin Canje: Saldos Expresados en Dolares" CssClass="subTitulo"></asp:Label>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <telerik:RadButton ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click" Style="top: 1px; left: 3px" Width="100px">
                                                            <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png" />
                                                        </telerik:RadButton>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <telerik:RadPivotGrid RenderMode="Lightweight" AllowPaging="true" PageSize="100" Height="600px"
                                                                ID="rpgDetalle" runat="server" ColumnHeaderZoneText="ColumnHeaderZone" RowGroupsDefaultExpanded="false" OnNeedDataSource="rpgDetalle_NeedDataSource"
                                                                OnCellDataBound="rpgDetalle_CellDataBound" OnPivotGridCellExporting="rpgDetalle_PivotGridCellExporting" Skin="Default"  >
                                                                <ClientSettings EnableFieldsDragDrop="true">
                                                                    <Scrolling AllowVerticalScroll="true"></Scrolling>
                                                                    
                                                                </ClientSettings>
                                                                
                                                                <Fields>
                                                                    <telerik:PivotGridRowField DataField="NombreZona" ZoneIndex="0" CellStyle-Width="250px">
                                                                        <CellStyle  Width="150px"  Font-Size="X-Small"  />
                                                                    </telerik:PivotGridRowField>

                                                                    <telerik:PivotGridRowField DataField="NombreVendedor" ZoneIndex="1" CellStyle-Width="250px">
                                                                        <CellStyle  Width="150px"  Font-Size="X-Small"  />
                                                                    </telerik:PivotGridRowField>

                                                                    <telerik:PivotGridRowField DataField="Aceptante" ZoneIndex="2" CellStyle-Width="250px">
                                                                        <CellStyle  Width="190px"  Font-Size="X-Small"  />
                                                                    </telerik:PivotGridRowField>

                                                                    <telerik:PivotGridRowField DataField="ID_Letra" ZoneIndex="3" CellStyle-Width="250px">
                                                                        <CellStyle  Width="190px"  Font-Size="X-Small"  />
                                                                    </telerik:PivotGridRowField>



                                                                    <telerik:PivotGridColumnField DataField="TipoPlazoVencimiento">
                                                                        <CellStyle  Width="100px"  Font-Size="X-Small"  />
                                                                    </telerik:PivotGridColumnField>

                                                                    

                                                       

                                                                    <telerik:PivotGridAggregateField DataField="Saldo" Aggregate="Sum" DataFormatString="${0:N}" Caption="Saldo $" GrandTotalAggregateFormatString="${0:N}">
                                                                        <CellStyle  Width="100px"  Font-Size="X-Small"  />
                                                                        <HeaderCellTemplate>
                                                                            Total Saldo
                                                                        </HeaderCellTemplate>
                                                                        <ColumnGrandTotalHeaderCellTemplate>
                                                                            Total Saldo
                                                                        </ColumnGrandTotalHeaderCellTemplate>
                                                                    </telerik:PivotGridAggregateField>
                                                                </Fields>
                                                                <%--<SortExpressions>
                                                                    <telerik:PivotGridSortExpression FieldName="NombreZona" SortOrder="Descending"></telerik:PivotGridSortExpression>
                                                                </SortExpressions>--%>
                                                            </telerik:RadPivotGrid>
                                                        </div>
                                                    </div>
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
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="fila">
        <div class="colum10">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
