<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmCobranzas.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.Cobranzas.frmCobranzas" %>


<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Estado de cuenta</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function OnClientEntryAddingHandler(sender, eventArgs) {
            if (sender.get_entries().get_count() > 0) {
                eventArgs.set_cancel(true);
                alert("Solo se puede selecionar un elemento.");
            }
        }

        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("btnExpResumen") >= 0)
                args.set_enableAjax(false);
            if (args.get_eventTarget().indexOf("btnExpDetalle") >= 0)
                args.set_enableAjax(false);
            if (args.get_eventTarget().indexOf("btnExpPDFDetalle") >= 0)
                args.set_enableAjax(false);
        }

        function ShowInsertForm(variable) {
            window.radopen("frmExportarPDFPopup.aspx?strFileNombre=" + variable, "rwExportarPDF");
            return false;
        }

        function AbrirNuevoVentana(variable)
        {
            strCodigoSobre = codSobre;
            strCodigoPais = codPais;

            var surl = 'frmExportarPDFPopup.aspx?strFileNombre=' + variable
            window.open(surl, "", "left=0px,top=0px,height=730x,width=1160px,status=no,toolbar=no,menubar=no,location=no,resizable=yes");

        }

        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("btnExcel") >= 0)
                args.set_enableAjax(false);
        }

    </script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramEstadoCuenta" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpEstadoCuenta" ControlID="rapEstadoCuenta"/>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnBuscarResumen">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpEstadoCuenta" ControlID="rapEstadoCuenta"/>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdEstadoCuenta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEstadoCuenta" LoadingPanelID="ralpEstadoCuenta"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdResumenCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdResumenCliente" LoadingPanelID="ralpEstadoCuenta"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdCobranzasResumenPivot">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCobranzasResumenPivot" LoadingPanelID="ralpEstadoCuenta"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdCobranzas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCobranzas" LoadingPanelID="ralpEstadoCuenta"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpEstadoCuenta" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmVidaLey" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwVidaLey" runat="server" Width="560px" Height="560px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>
            <telerik:RadWindow ID="rwExportarPDF" runat="server" Width="560px" Height="560px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="rapEstadoCuenta" runat="server" ClientEvents-OnRequestStart="requestStart" Height="100%" >
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblTitulo" runat="server" Text="Consultar cobranzas" CssClass="titulo"></asp:Label>
            </div>
        </div>
        <div class="row">
            <telerik:RadTabStrip runat="server" ID="stripPedido" MultiPageID="rmpRepEstadoCuenta" SelectedIndex="0" CssClass="col-md-12">
                <Tabs>
                    <telerik:RadTab Text="Resumen" Selected="True"></telerik:RadTab>
                    <telerik:RadTab Text="Detalle"></telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage runat="server" ID="rmpRepEstadoCuenta" SelectedIndex="0" Height="100%" CssClass="col-md-12">

              <telerik:RadPageView runat="server" ID="pageResumen" CssClass="col-md-12" Height="100%">
                <div class="row">
                <div class="col-md-12">

                <table>
                    <tr>
                        <td class="auto-style1">
                             <asp:Label ID="Label1" runat="server" CssClass="etiqueta" Text="Fecha Cobranza:" Width="116px"></asp:Label>
                        </td>

                        <td class="auto-style1">
                            <asp:Label ID="Label4" runat="server" CssClass="etiqueta" Text="Desde: " Width="40px"></asp:Label>
                        </td>
                        <td class="auto-style1">   
                            <telerik:RadDatePicker ID="dpFechaDesdeResumen" runat="server" DateInput-ReadOnly="true" Width="150px">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker></td>
                       <td class="auto-style1">
                            <asp:Label ID="Label5" runat="server" CssClass="etiqueta" Text="Hasta: " Width="40px"></asp:Label>
                        </td>
                        <td class="auto-style1">   
                            <telerik:RadDatePicker ID="dpFechaHastaResumen" runat="server" DateInput-ReadOnly="true" Width="150px">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker></td>

                        <td class="auto-style1"></td>
                        
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style1">
           
                              <telerik:RadButton ID="btnBuscarResumen" runat="server" OnClick="btnBuscarResumen_Click" style="top: 1px; left: 3px" Text="Buscar" Width="100px">
                                  <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                              </telerik:RadButton>
           
                        </td>
                        <td class="auto-style1">&nbsp;&nbsp;</td>
                         <td colspan="2">
                            <telerik:RadButton ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click">
                                <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png"/>
                            </telerik:RadButton>
                       </td>

                    </tr>
                </table>
                 <div style="height: 5px">
                 </div>
                <table>
                   <tr>
                        <td>
                           <asp:Label ID="Label8" runat="server" CssClass="etiqueta" Text="Resumen: " Width="116px"></asp:Label>
                          

                        </td>
                        <td>
                            &nbsp;</td>
                         <td> 
                            
                             &nbsp;&nbsp;</td>
                        <td>
                            
                        </td>
                    </tr>
                </table>

                    <telerik:RadPivotGrid ID="grdCobranzasResumenPivot" runat="server" Width="100%" Height="100%" AllowFiltering="false" 

                                        ShowFilterHeaderZone="false" OnPivotGridCellExporting="grdCobranzasResumenPivot_PivotGridCellExporting"
                                        ShowDataHeaderZone="false" ShowRowHeaderZone="false" ShowColumnHeaderZone="false" EnableConfigurationPanel="true" 
                                        TotalsSettings-GrandTotalsVisibility="RowsOnly"  OnNeedDataSource="grdCobranzasResumenPivot_NeedDataSource"
                                        AllowSorting="true"
                                        RowGroupsDefaultExpanded="false"
                                        >

                                        <ExportSettings Excel-Format="Xlsx" OpenInNewWindow="true"></ExportSettings>
                                        <ClientSettings EnableFieldsDragDrop="false" >
                                            <Scrolling AllowVerticalScroll="true"></Scrolling>
                                        </ClientSettings>

                                        <Fields>
                                            <telerik:PivotGridRowField DataField="ZonaCobranza" ZoneIndex="0">
                                                <CellStyle Width="150px" />
                                            </telerik:PivotGridRowField>
                                            <telerik:PivotGridRowField DataField="Cliente" ZoneIndex="1" >
                                                <CellStyle Width="250px" />
                                            </telerik:PivotGridRowField>

                                            <telerik:PivotGridColumnField DataField="Periodo">
                                            </telerik:PivotGridColumnField>

                                           <telerik:PivotGridAggregateField CellStyle-Width="105px" DataField="PagadoDolares" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    PagadoDolares
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                           <telerik:PivotGridAggregateField   CellStyle-Width="105px" DataField="Importe_NoVencido" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    NoVencido
                                                </HeaderCellTemplate>
                                           </telerik:PivotGridAggregateField>
                                         

                                            <telerik:PivotGridAggregateField  CellStyle-Width="105px" DataField="Importe_01a08" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Importe_01a08
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                           <telerik:PivotGridAggregateField CellStyle-Width="105px" DataField="Importe_09a30" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Importe_09a30
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                            <telerik:PivotGridAggregateField CellStyle-Width="105px" DataField="Importe_31a60" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Importe_31a60
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>



                                            <telerik:PivotGridAggregateField CellStyle-Width="105px" DataField="Importe_61a120" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Importe_61a120
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                            <telerik:PivotGridAggregateField CellStyle-Width="105px" DataField="Importe_121a360" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Importe_121a360
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                            <telerik:PivotGridAggregateField CellStyle-Width="105px" DataField="Importe_361a720" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Importe_361a720
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                            <telerik:PivotGridAggregateField CellStyle-Width="105px" DataField="Importe_721amas" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Importe_721amas
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                        </Fields>
                               
                                    </telerik:RadPivotGrid>

                    <table>
                   <tr>
                        <td>
                           <asp:Label ID="Label6" runat="server" CssClass="etiqueta" Text="Resumen Vencidos: " Width="116px"></asp:Label>
                           <asp:Label ID="Label7" runat="server"></asp:Label></td>
                        <td>
                            &nbsp;</td>
                         <td> 
                             &nbsp;&nbsp;</td>
                        <td>
                            
                        </td>
                    </tr>
                </table>

                    <telerik:RadGrid ID="grdVencidosResumen" runat="server" AllowMultiRowSelection="false"
                    AutoGenerateColumns="False" Height="125px"  Width="100%"
                    AllowSorting="True"  ShowFooter="true" >

                    <MasterTableView TableLayout="Fixed" 
                        AllowMultiColumnSorting="true"  ShowGroupFooter="true"  >

                        <Columns>

                            <telerik:GridBoundColumn DataField="EstadoCliente" Display="true" HeaderText="Estado Cliente" HeaderStyle-Width="200px" AllowSorting="false"
                                Aggregate="Count" FooterText="Total : ">
                                   <FooterStyle Font-Bold="true" HorizontalAlign="left"/>
                            </telerik:GridBoundColumn>


                            <telerik:GridBoundColumn DataField="DeudaDolares" HeaderText="Deuda Dolares" HeaderStyle-Width="80px" 
                                DataFormatString="${0:#,##0.00}" HeaderStyle-HorizontalAlign="Right"   Aggregate="Sum" 
                                AllowSorting="false"    FooterStyle-HorizontalAlign="Right">
                                <ItemStyle  HorizontalAlign="Right"/> 
                                   <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="DeudaPorVencer" HeaderText="Deuda PorVencer" HeaderStyle-Width="70px" 
                             DataFormatString="${0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" AllowSorting="false" 
                                Aggregate="Sum"  FooterStyle-HorizontalAlign="Right" > 
                                <ItemStyle HorizontalAlign="Right"/> 
                                   <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                            </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn DataField="Vencido01a08" HeaderText="Vencido01a08" HeaderStyle-Width="70px" 
                             DataFormatString="${0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" AllowSorting="false" 
                                Aggregate="Sum"  FooterStyle-HorizontalAlign="Right" > 
                                <ItemStyle HorizontalAlign="Right"/> 
                                                               <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                            </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn DataField="Vencido09a30" HeaderText="Vencido09a30" HeaderStyle-Width="70px" 
                             DataFormatString="${0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" AllowSorting="false" 
                                Aggregate="Sum"  FooterStyle-HorizontalAlign="Right" > 
                                <ItemStyle HorizontalAlign="Right"/> 
                                                               <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                            </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn DataField="Vencido31a60" HeaderText="Vencido31a60" HeaderStyle-Width="70px" 
                             DataFormatString="${0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" AllowSorting="false" 
                                Aggregate="Sum"  FooterStyle-HorizontalAlign="Right" > 
                                <ItemStyle HorizontalAlign="Right"/> 
                                                               <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Vencido61a120" HeaderText="Vencido61a120" HeaderStyle-Width="70px" 
                             DataFormatString="${0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" AllowSorting="false" 
                                Aggregate="Sum"  FooterStyle-HorizontalAlign="Right" > 
                                <ItemStyle HorizontalAlign="Right"/> 
                                   <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Vencido121a360" HeaderText="Vencido121a360" HeaderStyle-Width="70px" 
                             DataFormatString="${0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" AllowSorting="false" 
                                Aggregate="Sum"  FooterStyle-HorizontalAlign="Right" > 
                                <ItemStyle HorizontalAlign="Right"/> 
                                   <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn DataField="Vencido361a720" HeaderText="Vencido361a720" HeaderStyle-Width="70px" 
                             DataFormatString="${0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" AllowSorting="false" 
                                Aggregate="Sum"  FooterStyle-HorizontalAlign="Right" > 
                                <ItemStyle HorizontalAlign="Right"/> 
                                    <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn DataField="Vencido721aMas" HeaderText="Vencido721aMas" HeaderStyle-Width="70px" 
                             DataFormatString="${0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" AllowSorting="false" 
                                Aggregate="Sum"  FooterStyle-HorizontalAlign="Right" > 
                                <ItemStyle HorizontalAlign="Right"/> 
                                    <FooterStyle Font-Bold="true" HorizontalAlign="Right"/>
                            </telerik:GridBoundColumn>



                        </Columns>
                    </MasterTableView>

                <ClientSettings>
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                    <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                </ClientSettings>
                    <GroupingSettings ShowUnGroupButton="false" /> 
                </telerik:RadGrid>
              </div>
              </div>
            </telerik:RadPageView>

              <telerik:RadPageView runat="server" ID="pageDetalle" CssClass="col-md-12" Height="100%">
              <div class="row">
                <div class="col-md-12" id="divDetalle" runat="server">

                <table>
                    <tr>
                        <td class="auto-style1">
                             <asp:Label ID="lblFechaEmision" runat="server" CssClass="etiqueta" Text="Fecha Cobranza:" Width="116px"></asp:Label>
                        </td>

                        <td class="auto-style1">
                            <asp:Label ID="lblFinalEmision" runat="server" CssClass="etiqueta" Text="Desde: " Width="40px"></asp:Label>
                        </td>
                        <td class="auto-style1">   
                            <telerik:RadDatePicker ID="dpDesde" runat="server" DateInput-ReadOnly="true" Width="150px">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker></td>
                       <td class="auto-style1">
                            <asp:Label ID="Label2" runat="server" CssClass="etiqueta" Text="Hasta: " Width="40px"></asp:Label>
                        </td>
                        <td class="auto-style1">   
                            <telerik:RadDatePicker ID="dpHasta" runat="server" DateInput-ReadOnly="true" Width="150px">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker></td>

                        <td class="auto-style1"></td>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style1">
                            <asp:ImageButton ID="btnExpDetalle" runat="server"  Height="30px" ImageUrl="~/Images/Icons/24_excel.png" OnClick="btnExpDetalle_Click" Width="30px" Visible="true" />
                        </td>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style1">
                            <%--<asp:ImageButton ID="btnExpPDFDetalle" runat="server"  Height="30px" ImageUrl="~/Images/Icons/24_pdf.png" OnClick="btnExpPDFDetalle_Click" Width="30px" Visible="true" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCliente" runat="server" CssClass="etiqueta" Text="Cliente:"></asp:Label>
                        </td>
                        <td colspan="4">
                            <telerik:RadAutoCompleteBox ID="acbCliente" runat="server" AllowCustomEntry="true" DropDownHeight="150px" 
                                DropDownWidth="300px" EmptyMessage="Selec. cliente" InputType="Text" 
                                OnClientEntryAdding="OnClientEntryAddingHandler" TextSettings-SelectionMode="Single" Width="400px">
                                <WebServiceSettings Method="Agenda_BuscarCliente" Path="frmCobranzas.aspx" />
                            </telerik:RadAutoCompleteBox>
                        </td>
                        <td >
                            &nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td >&nbsp;</td>
                        <td>&nbsp; &nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblVendedor" runat="server" CssClass="etiqueta" Text="Vendedor:"></asp:Label>
                        </td>
                        <td colspan="4">
                            <telerik:RadAutoCompleteBox ID="acbVendedor" runat="server" AllowCustomEntry="true" DropDownHeight="150px" DropDownWidth="300px" EmptyMessage="Selec. vendedor" 
                                InputType="Text" OnClientEntryAdding="OnClientEntryAddingHandler" TextSettings-SelectionMode="Single" Width="400px">
                                <WebServiceSettings Method="Agenda_BuscarVendedor" Path="frmCobranzas.aspx" />
                            </telerik:RadAutoCompleteBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td colspan="3">
                            <telerik:RadButton ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" style="top: 1px; left: 3px" Text="Buscar" Width="100px">
                                <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                            </telerik:RadButton>
                        </td>
                    </tr>
                </table>
                 <div style="height: 5px">
                 </div>
                 <table>
                   <tr>
                        <td>
                           <asp:Label ID="Label3" runat="server" CssClass="etiqueta" Text="Detalle: " Width="116px"></asp:Label>
                          

                        </td>
                        <td>
                            &nbsp;</td>
                         <td> 
                            
                             &nbsp;&nbsp;</td>
                        <td>
                            
                        </td>
                    </tr>
                </table>

                 <telerik:RadGrid ID="grdCobranzas" runat="server" AllowMultiRowSelection="false"
                    AutoGenerateColumns="False" Height="420px"  Width="95%"
                    OnNeedDataSource="grdCobranzas_NeedDataSource" AllowSorting="True"  ShowFooter="true">

                    <ExportSettings Excel-Format="Xlsx" OpenInNewWindow="true"></ExportSettings>
                      
                    <MasterTableView TableLayout="Fixed" DataKeyNames="Id_TipoDoc"
                        AllowMultiColumnSorting="true"  ShowGroupFooter="true" >


                         <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                   <telerik:GridGroupByField FieldAlias="CODIGO" FieldName="Id_Cliente" />
                                    <telerik:GridGroupByField FieldAlias="CLIENTE" FieldName="Cliente" />
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="Id_Cliente"  />
                                    <telerik:GridGroupByField FieldName="Cliente"/>
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                        </GroupByExpressions>
                        <Columns>
                            <telerik:GridBoundColumn DataField="TipoDoc" HeaderText="TipoDocumento" UniqueName="TipoDocumento" HeaderStyle-Width="100px" AllowSorting="false"  >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="NumeroDocumento" Display="true" HeaderText="NumDocumento" HeaderStyle-Width="120px" AllowSorting="false"
                                Aggregate="Count" FooterText="Total Documentos: ">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FechaEmision" HeaderText="FechaEmisión" HeaderStyle-Width="100px" DataFormatString="{0:dd/MM/yyyy}"   >
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="FechaVencimiento" HeaderText="FechaVencimiento" HeaderStyle-Width="100px" DataFormatString="{0:dd/MM/yyyy}"   >
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FechaCobranza" HeaderText="FechaCobranza" HeaderStyle-Width="100px" DataFormatString="{0:dd/MM/yyyy}"  >
                            </telerik:GridBoundColumn>



                            <telerik:GridBoundColumn DataField="DiasMora" HeaderText="DiasMora" HeaderStyle-Width="60px" >
                                <ItemStyle HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>
   
                            <telerik:GridBoundColumn DataField="Mon" HeaderText="Mon" HeaderStyle-Width="30px" AllowSorting="false" >
                            </telerik:GridBoundColumn>
                  
                            <telerik:GridBoundColumn DataField="ImporteCancelado" HeaderText="ImportePago" HeaderStyle-Width="80px" 
                                DataFormatString="{0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" 
                                AllowSorting="false"    FooterStyle-HorizontalAlign="Right">
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="TC" HeaderText="TC" HeaderStyle-Width="40px" 
                               HeaderStyle-HorizontalAlign="Right" AllowSorting="false" FooterText="Total:">
                                <ItemStyle HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="PagadoDolares" HeaderText="PagoDolares" HeaderStyle-Width="70px" 
                             DataFormatString="{0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" AllowSorting="false" 
                                Aggregate="Sum"  FooterStyle-HorizontalAlign="Right" > 
                                <ItemStyle HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Banco" HeaderText="Banco" HeaderStyle-Width="200px" AllowSorting="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CtaCte" HeaderText="CtaCte" HeaderStyle-Width="100px" AllowSorting="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NroCuenta" HeaderText="NroCuenta" HeaderStyle-Width="120px" AllowSorting="false">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>

                <ClientSettings>
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                    <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                </ClientSettings>
                    <GroupingSettings ShowUnGroupButton="false" /> 
                </telerik:RadGrid>
                 </div>
                </div>
             </telerik:RadPageView>
            
            
            </telerik:RadMultiPage>
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="row">
        <div class="col-md-12">
             <asp:Label ID="lblMesajeResumen" runat="server"></asp:Label>
             <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
        <div class="col-md-12">
             <asp:Label ID="lblDate" runat="server" Visible="false"></asp:Label>
             <asp:Label ID="lblDate2" runat="server" Visible="false"></asp:Label>
        </div>
    </div>
</asp:Content>
