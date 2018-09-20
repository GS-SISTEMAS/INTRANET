<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmReporteComision.aspx.cs" Inherits="GS.SISGEGS.Web.Comisiones.Reporte.frmReporteComision" %>


<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Reporte Comisiones</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function OnClientEntryAddingHandler(sender, eventArgs) {
            if (sender.get_entries().get_count() > 0) {
                eventArgs.set_cancel(true);
                alert("Solo se puede selecionar un elemento.");
            }
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
            if (args.get_eventTarget().indexOf("btnExcelGerencia") >= 0)
                args.set_enableAjax(false);
            if (args.get_eventTarget().indexOf("btnExcelVendedor") >= 0)
                args.set_enableAjax(false);
            if (args.get_eventTarget().indexOf("btnExcelPromotor") >= 0)
                args.set_enableAjax(false);
            if (args.get_eventTarget().indexOf("btnExcelSemilla") >= 0)
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

            <telerik:AjaxSetting AjaxControlID="btnBuscarGerencia">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpEstadoCuenta" ControlID="rapEstadoCuenta"/>
                </UpdatedControls>
            </telerik:AjaxSetting>

           <telerik:AjaxSetting AjaxControlID="btnBuscarPromotor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpEstadoCuenta" ControlID="rapEstadoCuenta"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
              <telerik:AjaxSetting AjaxControlID="btnBuscarSemilla">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpEstadoCuenta" ControlID="rapEstadoCuenta"/>
                </UpdatedControls>
            </telerik:AjaxSetting>

            


            <telerik:AjaxSetting AjaxControlID="grdGerentePivot">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdGerentePivot" LoadingPanelID="ralpEstadoCuenta"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdVendedor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdVendedor" LoadingPanelID="ralpEstadoCuenta"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdPromotores">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPromotores" LoadingPanelID="ralpEstadoCuenta"></telerik:AjaxUpdatedControl>
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
                <asp:Label ID="lblTitulo" runat="server" Text="Consultar Comisiones" CssClass="titulo"></asp:Label>
            </div>
        </div>
        <div class="row">
            <telerik:RadTabStrip runat="server" ID="stripPedido" MultiPageID="rmpRepEstadoCuenta" SelectedIndex="0" CssClass="col-md-12">
                <Tabs>
                    <telerik:RadTab Text="Promotores" Selected="True"></telerik:RadTab>
                    <telerik:RadTab Text="Vendedores"></telerik:RadTab>
                    <telerik:RadTab Text="Jefaturas"></telerik:RadTab>
                    <telerik:RadTab Text="Semillas"></telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage runat="server" ID="rmpRepEstadoCuenta" SelectedIndex="0" Height="100%" CssClass="col-md-12">

              <telerik:RadPageView runat="server" ID="pagePromotores" CssClass="col-md-12" Height="100%">
                <div class="row">
                <div class="col-md-12">

                    <table>
                    <tr>
                        <td class="auto-style1">
                             <asp:Label ID="Label1" runat="server" CssClass="etiqueta" Text="Periodo comisión:" Width="116px"></asp:Label>
                        </td>
                        <td class="auto-style1">   
                           <telerik:RadMonthYearPicker ID="rmyPromotores" runat="server" Width="100%">
                                            <DateInput runat="server" DateFormat="MM-yyyy"></DateInput>
                                        </telerik:RadMonthYearPicker>
                        </td>
                        <td colspan="3">
                            <telerik:RadButton ID="btnBuscarPromotor" runat="server" OnClick="btnBuscarPromotor_Click" style="top: 1px; left: 3px" Text="Buscar" Width="100px">
                                <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                            </telerik:RadButton>
                        </td>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style1">
                            <telerik:RadButton ID="btnExcelPromotor" runat="server" Text="Excel" OnClick="btnExcelPromotor_Click">
                                <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png"/>
                            </telerik:RadButton>

                       
                        </td>
                        <td class="auto-style1">
                             
                        </td>
                        <td class="auto-style1">
                           
                        </td>
                    </tr>

                </table>

                    <telerik:RadPivotGrid ID="grdPromotoresPivot" runat="server" Width="100%" Height="450px" AllowFiltering="false" 

                                        ShowFilterHeaderZone="false" OnPivotGridCellExporting="grdPromotoresPivot_PivotGridCellExporting"
                                        ShowDataHeaderZone="false" ShowRowHeaderZone="false" ShowColumnHeaderZone="false" EnableConfigurationPanel="true" 
                                        TotalsSettings-GrandTotalsVisibility="RowsOnly"  OnNeedDataSource="grdPromotoresPivot_NeedDataSource"
                                        AllowSorting="true"
                                        RowGroupsDefaultExpanded="false"
                                        >

                                        <ExportSettings Excel-Format="Xlsx" OpenInNewWindow="true"></ExportSettings>
                                        <ClientSettings EnableFieldsDragDrop="false" >
                                            <Scrolling AllowVerticalScroll="true"></Scrolling>
                                        </ClientSettings>

                                        <Fields>
                                            <telerik:PivotGridRowField DataField="Zona" ZoneIndex="0">
                                                <CellStyle Width="200px" />
                                            </telerik:PivotGridRowField>
                                            <telerik:PivotGridRowField DataField="agendanombre" ZoneIndex="1" >
                                                <CellStyle Width="250px" />
                                            </telerik:PivotGridRowField>
                                            <telerik:PivotGridColumnField DataField="Periodo">
                                            </telerik:PivotGridColumnField>
                               
                                           <telerik:PivotGridAggregateField CellStyle-Width="105px" DataField="CobradoIGV_D" Aggregate="Average" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    CobradoIGV
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                           <telerik:PivotGridAggregateField CellStyle-Width="105px" DataField="Cobrado_D" Aggregate="Average" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Cobrado
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                           <telerik:PivotGridAggregateField CellStyle-Width="105px" DataField="cobradoProvision_D" Aggregate="Average" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    cobradoProvision
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>


                                           <telerik:PivotGridAggregateField CellStyle-Width="105px"  DataField="cumplimiento" Aggregate="Average" DataFormatString="{0:F2}%">
                                                <HeaderCellTemplate>
                                                    Avance(%)
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                           <telerik:PivotGridAggregateField CellStyle-Width="105px" DataField="CobradoCumplimiento_D" Aggregate="Average" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    CobradoCumplimiento
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                            
                                        
                                            <telerik:PivotGridAggregateField CellStyle-Width="105px" DataField="CobradoCategoria_D" Aggregate="Average" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    CobradoCategoria
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>


                                            <telerik:PivotGridAggregateField CellStyle-Width="105px" DataField="CobradoComision_D" Aggregate="Average" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    CobradoComision
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                           <telerik:PivotGridAggregateField CellStyle-Width="105px"  DataField="porcentajePro" Aggregate="Sum" DataFormatString="{0:F0}%">
                                                <HeaderCellTemplate>
                                                    (%)Comisión
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                             <telerik:PivotGridAggregateField CellStyle-Width="105px" DataField="Comision_D" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    ComisionD
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>


                                              <telerik:PivotGridAggregateField CellStyle-Width="105px" DataField="Comision_Soles" Aggregate="Sum" DataFormatString="S/{0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    ComisionS
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>


                                        </Fields>
                               
                                    </telerik:RadPivotGrid>

              </div>
              </div>
            </telerik:RadPageView>

              <telerik:RadPageView runat="server" ID="pageVendedores" CssClass="col-md-12" Height="100%">
              <div class="row">
                <div class="col-md-12">

                <table>
                    <tr>
                        <td class="auto-style1">
                             <asp:Label ID="lblFechaEmision" runat="server" CssClass="etiqueta" Text="Periodo comisión:" Width="116px"></asp:Label>
                        </td>
                        <td class="auto-style1">   
                           <telerik:RadMonthYearPicker ID="rmyReporte" runat="server" Width="100%">
                                            <DateInput runat="server" DateFormat="MM-yyyy"></DateInput>
                                        </telerik:RadMonthYearPicker>
                        </td>
                        <td colspan="3">
                            <telerik:RadButton ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" style="top: 1px; left: 3px" Text="Buscar" Width="100px">
                                <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                            </telerik:RadButton>
                        </td>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style1">
                            <telerik:RadButton ID="btnExcelVendedor" runat="server" Text="Excel" OnClick="btnExcelVendedor_Click">
                                <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png"/>
                            </telerik:RadButton>

                       
                        </td>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style1">
                           
                        </td>
                    </tr>

                </table>

                 <telerik:RadGrid ID="grdVendedor" runat="server" AllowMultiRowSelection="false"
                    AutoGenerateColumns="False" Height="450px"  Width="100%"
                    OnNeedDataSource="grdVendedor_NeedDataSource" AllowSorting="True"  ShowFooter="true" >

                     <ExportSettings Excel-Format="ExcelML" ExportOnlyData="true" OpenInNewWindow="true"/>


                    <MasterTableView TableLayout="Fixed" DataKeyNames="idpersonal"
                        AllowMultiColumnSorting="true"  ShowGroupFooter="true"  >
                        <Columns>

                            <telerik:GridBoundColumn DataField="zona" Display="true" HeaderText="Zona" HeaderStyle-Width="150px" AllowSorting="false" Aggregate="Count" FooterText="Total Vendedores: "  >
                            </telerik:GridBoundColumn>


                            <telerik:GridBoundColumn DataField="agendanombre" Display="true" HeaderText="Vendedor" HeaderStyle-Width="150px" AllowSorting="false"  >
                            </telerik:GridBoundColumn>


                            <telerik:GridBoundColumn DataField="cobradoD" HeaderText="CobradoIGV" HeaderStyle-Width="80px" 
                                DataFormatString="${0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" 
                                AllowSorting="false"    FooterStyle-HorizontalAlign="Right" Aggregate="Sum" FooterText="Total Cobrado:">
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="PorcentajeZona" HeaderText="%Zona" HeaderStyle-Width="60px" 
                                DataFormatString="{0:#,##0.00}%" HeaderStyle-HorizontalAlign="Right"  Aggregate="Avg"
                                AllowSorting="false"    >
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="cobradoD_zona" HeaderText="CobradoIGV_Zona" HeaderStyle-Width="110px" 
                                DataFormatString="${0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" 
                                AllowSorting="false"    FooterStyle-HorizontalAlign="Right" Aggregate="Sum"  >
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>


                             <telerik:GridBoundColumn DataField="cobradoSinIGV_D" HeaderText="cobradoSinIGV" HeaderStyle-Width="100px" 
                                DataFormatString="${0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" 
                                AllowSorting="false"    FooterStyle-HorizontalAlign="Right" Aggregate="Sum" >
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>


                             <telerik:GridBoundColumn DataField="cobrado0a60_D" HeaderText="Cobrado0a60" HeaderStyle-Width="100px" 
                                DataFormatString="${0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" 
                                AllowSorting="false"    FooterStyle-HorizontalAlign="Right" Aggregate="Sum" FooterText="Total Cobrado:">
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>

                               <telerik:GridBoundColumn DataField="cobrado60a120_D" HeaderText="cobrado60a120" HeaderStyle-Width="100px" 
                                DataFormatString="${0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" 
                                AllowSorting="false"    FooterStyle-HorizontalAlign="Right" Aggregate="Sum" FooterText="Total Cobrado:">
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="cobrado120aMas_D" HeaderText="cobrado120aMas" HeaderStyle-Width="100px" 
                                DataFormatString="${0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" 
                                AllowSorting="false"    FooterStyle-HorizontalAlign="Right" Aggregate="Sum" FooterText="Total Cobrado:">
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="porcentajeVendedor" HeaderText="%Comisión" HeaderStyle-Width="70px" 
                                DataFormatString="{0:#,##0.00}%" HeaderStyle-HorizontalAlign="Right"  Aggregate="Avg"
                                AllowSorting="false"    >
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn DataField="Comision0a60_D" HeaderText="Comision0a60" HeaderStyle-Width="100px" 
                                DataFormatString="${0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" 
                                AllowSorting="false"    FooterStyle-HorizontalAlign="Right" Aggregate="Sum" FooterText="Total Comisión:">
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Comision60a120_D" HeaderText="Comision60a120" HeaderStyle-Width="100px" 
                                DataFormatString="${0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" 
                                AllowSorting="false"    FooterStyle-HorizontalAlign="Right" Aggregate="Sum" FooterText="Total Comisión:">
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>


                             <telerik:GridBoundColumn DataField="Comision_D" HeaderText="ComisiónD" HeaderStyle-Width="90px" 
                                DataFormatString="${0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" 
                                AllowSorting="false"    FooterStyle-HorizontalAlign="Right" Aggregate="Sum" FooterText="Total Comisión:">
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn DataField="Comision_Soles" HeaderText="ComisiónS" HeaderStyle-Width="90px" 
                                DataFormatString="S/{0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" 
                                AllowSorting="false"    FooterStyle-HorizontalAlign="Right" Aggregate="Sum" FooterText="Total Comisión:">
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>

                        </Columns>
                    </MasterTableView>

                <ClientSettings>
                    <Selecting AllowRowSelect="True"></Selecting>
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                    <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                </ClientSettings>
                    <GroupingSettings ShowUnGroupButton="false" /> 
                </telerik:RadGrid>
                 </div>
                </div>
             </telerik:RadPageView>

              <telerik:RadPageView runat="server" ID="pageJefaturas" CssClass="col-md-12" Height="100%">
                <div class="row">
                <div class="col-md-12">

                <table>
                    <tr>
                        <td class="auto-style1">
                            <asp:Label ID="Label2" runat="server" CssClass="etiqueta" Text="Periodo comisión:" Width="116px"></asp:Label>
                        </td>
                        <td class="auto-style1">   
                                       <telerik:RadMonthYearPicker ID="rmyPeriodoGerencia" runat="server" Width="100%">
                                            <DateInput runat="server" DateFormat="MM-yyyy"></DateInput>
                                        </telerik:RadMonthYearPicker>
                        </td>
                       <td class="auto-style1">
                          
                        </td>
                        <td class="auto-style1">   
                        </td>
                         <td class="auto-style1">   
                        </td>
                        <td class="auto-style1"></td>
                        
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style1">
           
                              <telerik:RadButton ID="btnBuscarGerencia" runat="server" OnClick="btnBuscarGerencia_Click" style="top: 1px; left: 3px" Text="Buscar" Width="100px">
                                  <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                              </telerik:RadButton>
           
                        </td>
                        <td class="auto-style1">&nbsp;&nbsp;</td>
                         <td colspan="2">
                            <telerik:RadButton ID="btnExcelGerencia" runat="server" Text="Excel" OnClick="btnExcelGerencia_Click">
                                <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png"/>
                            </telerik:RadButton>
                       </td>

                    </tr>
                </table>
                 <div style="height: 5px">
                 </div>

                    <telerik:RadPivotGrid ID="grdGerentePivot" runat="server" Width="100%" Height="500px" AllowFiltering="false" 
                                        OnPivotGridCellExporting="grdGerentePivot_PivotGridCellExporting"
                                        OnNeedDataSource="grdGerentePivot_NeedDataSource"

                                        ShowFilterHeaderZone="false" 
                                        ShowDataHeaderZone="false" ShowRowHeaderZone="false" ShowColumnHeaderZone="false" EnableConfigurationPanel="true" 
                                        TotalsSettings-GrandTotalsVisibility="RowsOnly"  
                                        AllowSorting="true"
                                        RowGroupsDefaultExpanded="false"
                                        >

                                        <ClientSettings EnableFieldsDragDrop="false" >
                                            <Scrolling AllowVerticalScroll="true"></Scrolling>
                                        </ClientSettings>

                                        <Fields>
                                            <telerik:PivotGridRowField DataField="AgendaNombreG"  ZoneIndex="0">
                                                <CellStyle Width="200px"/>
                                            </telerik:PivotGridRowField>
                                            <telerik:PivotGridRowField DataField="zona" ZoneIndex="1" >
                                                <CellStyle Width="150px" />
                                            </telerik:PivotGridRowField>

                                            <telerik:PivotGridColumnField DataField="Periodo">
                                            </telerik:PivotGridColumnField>

                                              <telerik:PivotGridAggregateField CellStyle-Width="105px" DataField="cobradoD" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    CobradoIGV
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>


                                              <telerik:PivotGridAggregateField CellStyle-Width="105px" DataField="cobradoSinIGV_D" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Cobrado
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                           <telerik:PivotGridAggregateField CellStyle-Width="105px" DataField="cobrado0a60_D" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Cobrado0a60
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                            
                                           <telerik:PivotGridAggregateField CellStyle-Width="105px" DataField="cobrado60a120_D" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Cobrado60a120
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                           <telerik:PivotGridAggregateField CellStyle-Width="105px" DataField="cobrado120aMas_D" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    cobrado120aMas
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>


                                             <telerik:PivotGridAggregateField CellStyle-Width="105px"  DataField="PorcentajeCom" Aggregate="Average" DataFormatString="{0:F2}%">
                                                <HeaderCellTemplate>
                                                    %Comisión
                                                </HeaderCellTemplate>
                                           </telerik:PivotGridAggregateField>

                                              <telerik:PivotGridAggregateField CellStyle-Width="120px" DataField="Comision0a60_D" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Comision0a60
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                             <telerik:PivotGridAggregateField CellStyle-Width="120px" DataField="Comision60a120_D" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Comision60a120
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                          
                                             <telerik:PivotGridAggregateField CellStyle-Width="105px" DataField="Comision_D" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    ComisionD
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>


                                              <telerik:PivotGridAggregateField CellStyle-Width="105px" DataField="Comision_Soles" Aggregate="Sum" DataFormatString="S/{0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    ComisionS
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                        </Fields>
                               
                                    </telerik:RadPivotGrid>

              </div>
              </div>
            </telerik:RadPageView>
            
              <telerik:RadPageView runat="server" ID="pageSemillas" CssClass="col-md-12" Height="100%">
                <div class="row">
                <div class="col-md-12">

                <table>
                    <tr>
                        <td class="auto-style1">
                            <asp:Label ID="Label3" runat="server" CssClass="etiqueta" Text="Periodo comisión:" Width="116px"></asp:Label>
                        </td>
                        <td class="auto-style1">   
                                       <telerik:RadMonthYearPicker ID="rmyPeriodoSemilla" runat="server" Width="100%">
                                            <DateInput runat="server" DateFormat="MM-yyyy"></DateInput>
                                        </telerik:RadMonthYearPicker>
                        </td>
                       <td class="auto-style1">
                          
                        </td>
                        <td class="auto-style1">   
                        </td>
                         <td class="auto-style1">   
                        </td>
                        <td class="auto-style1"></td>
                        
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style1">
           
                              <telerik:RadButton ID="btnBuscarSemilla" runat="server" OnClick="btnBuscarSemilla_Click" style="top: 1px; left: 3px" Text="Buscar" Width="100px">
                                  <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                              </telerik:RadButton>
           
                        </td>
                        <td class="auto-style1">&nbsp;&nbsp;</td>
                         <td colspan="2">
                            <telerik:RadButton ID="btnExcelSemilla" runat="server" Text="Excel" OnClick="btnExcelSemilla_Click">
                                <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png"/>
                            </telerik:RadButton>
                       </td>

                    </tr>
                </table>
                 <div style="height: 5px">
                 </div>

                    <telerik:RadPivotGrid ID="grdSemillaPivot" runat="server" Width="100%" Height="450px" AllowFiltering="false" 
                                        OnPivotGridCellExporting="grdSemillaPivot_PivotGridCellExporting"
                                        OnNeedDataSource="grdSemillaPivot_NeedDataSource"

                                        ShowFilterHeaderZone="false" 
                                        ShowDataHeaderZone="false" ShowRowHeaderZone="false" ShowColumnHeaderZone="false" EnableConfigurationPanel="true" 
                                        TotalsSettings-GrandTotalsVisibility="RowsOnly"  
                                        AllowSorting="true"
                                        RowGroupsDefaultExpanded="false"
                                        >

                                        <ExportSettings Excel-Format="Xlsx" OpenInNewWindow="true"></ExportSettings>
                                        <ClientSettings EnableFieldsDragDrop="false" >
                                            <Scrolling AllowVerticalScroll="true"></Scrolling>
                                        </ClientSettings>

                                        <Fields>
                                            <telerik:PivotGridRowField DataField="AgendaNombreG"  ZoneIndex="0">
                                                <CellStyle Width="250px"/>
                                            </telerik:PivotGridRowField>

                                            <telerik:PivotGridRowField DataField="zona" ZoneIndex="1" >
                                                <CellStyle Width="200px" />
                                            </telerik:PivotGridRowField>

                                            <telerik:PivotGridColumnField DataField="Periodo">
                                            </telerik:PivotGridColumnField>

                                              <telerik:PivotGridAggregateField CellStyle-Width="105px" DataField="cobradoD" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Cobrado
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                           <telerik:PivotGridAggregateField CellStyle-Width="120px" DataField="cobradoSinIGV_D" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    CobradoSinIGV
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                           <telerik:PivotGridAggregateField CellStyle-Width="120px" DataField="cobradoSinIGV_DSemilla" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    CobradoSemilla
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                            <telerik:PivotGridAggregateField CellStyle-Width="110px" DataField="cobrado0a60_D" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Cobrado0a60
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                            <telerik:PivotGridAggregateField CellStyle-Width="110px" DataField="cobrado60a120_D" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Cobrado60a120
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                            <telerik:PivotGridAggregateField CellStyle-Width="110px" DataField="cobrado120aMas_D" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Cobrado120aMas
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                           <telerik:PivotGridAggregateField CellStyle-Width="100px"  DataField="PorcentajeCom" Aggregate="Average" DataFormatString="{0:F2}%">
                                                <HeaderCellTemplate>
                                                    Porcentaje
                                                </HeaderCellTemplate>
                                           </telerik:PivotGridAggregateField>

                                             <telerik:PivotGridAggregateField CellStyle-Width="120px" DataField="Comision0a60_D" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Comision0a60
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                            <telerik:PivotGridAggregateField CellStyle-Width="120px" DataField="Comision60a120_D" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Comision60a120
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>


                                            <telerik:PivotGridAggregateField CellStyle-Width="120px" DataField="Comision_D" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Comision_Dolares
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                            
                                            <telerik:PivotGridAggregateField CellStyle-Width="120px" DataField="Comision_Soles" Aggregate="Sum" DataFormatString="S/{0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Comision_Soles
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>


                                        </Fields>
                               
                                    </telerik:RadPivotGrid>

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
             <asp:Label ID="lblPromotor" runat="server" Visible="false"></asp:Label>
             <asp:Label ID="lblVendedor" runat="server" Visible="false"></asp:Label>
             <asp:Label ID="lblGerente" runat="server" Visible="false"></asp:Label>
             <asp:Label ID="lblSemilla" runat="server" Visible="false"></asp:Label>
        </div>
    </div>
</asp:Content>
