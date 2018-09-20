<%@ Page Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmContadorAccesos.aspx.cs" Inherits="GS.SISGEGS.Web.Sistemas.Reportes.frmContadorAccesos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Gestion de Acceso Diario - Intranet
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
     
     <script type="text/javascript">
             function requestStart(sender, args) {

                 if (args.get_eventTarget().indexOf("btnExcel") >= 0)
                     args.set_enableAjax(false);
             }
     </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramContadorAccesos" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapContadorAccesos" LoadingPanelID="ralpContadorAccesos"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBuscarS">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapContadorAccesos" LoadingPanelID="ralpContadorAccesos"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>

             <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpContadorAccesos" ControlID="rapContadorAccesos"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="gsContadorAccesos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gsContadorAccesos" LoadingPanelID="ralpContadorAccesos"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="gsContadorSinAccesos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gsContadorSinAccesos" LoadingPanelID="ralpContadorAccesos"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="false" />
    <telerik:RadAjaxLoadingPanel ID="ralpContadorAccesos" runat="server">
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

    <telerik:RadAjaxPanel ID="rapContadorAccesos" runat="server" Width="100%" Height="100%" ClientEvents-OnRequestStart="requestStart">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="Label1" runat="server" Text="Consultar Cantidad de Accesos por Menu" CssClass="titulo"></asp:Label>
                </div>
            </div>
            <div class="row" style="height:100%">
                <telerik:RadTabStrip runat="server" ID="stripMenu" MultiPageID="rmpRepMenus" SelectedIndex="0" CssClass="col-md-12">
                    <Tabs>
                        <telerik:RadTab Text="Menus Accedidos" Selected="True"></telerik:RadTab>
                        <telerik:RadTab Text="Menus No Accedidos"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage runat="server" ID="rmpRepMenus" SelectedIndex="0" Height="100%" CssClass="col-md-12">
                   <telerik:RadPageView runat="server" ID="pageMenu" CssClass="col-md-12" Height="100%">
                         <div class="row">
                             <div class="col-md-12">
                                 <table>
                                     <tr>
                                         <td class="auto-style1">
                                             <asp:Label ID="Label3" runat="server" CssClass="etiqueta" Text="Fechas:" Width="116px"></asp:Label>
                                         </td>
                                         <td class="auto-style1">
                                             <asp:Label ID="Label4" runat="server" CssClass="etiqueta" Text="Inicial: " Width="40px"></asp:Label>
                                         </td>
                                         <td class="auto-style1">
                                             <telerik:RadDatePicker ID="dpFechaInicial" runat="server" DateInput-ReadOnly="true" Width="150px">
                                                 <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                                 </DateInput>
                                             </telerik:RadDatePicker>
                                         </td>
                                         <td class="auto-style1">
                                             <asp:Label ID="Label5" runat="server" CssClass="etiqueta" Text="Final: " Width="40px"></asp:Label>
                                         </td>
                                         <td class="auto-style1">
                                             <telerik:RadDatePicker ID="dpFechaFinal" runat="server" DateInput-ReadOnly="true" Width="150px">
                                                 <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                                 </DateInput>
                                             </telerik:RadDatePicker>
                                         </td>
                                         <td class="auto-style1"></td>
                                         <td class="auto-style1">&nbsp;</td>
                                         <td>
                                             <asp:CheckBox ID="chkexpandido" Text="Mostrar Expandido" runat="server" CssClass="chkSmall" Visible="false" />
                                         </td>
                                         <td class="auto-style1"></td>
                                         <td class="auto-style1">&nbsp;</td>
                                         <td class="auto-style1">
                                             <telerik:RadButton ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Style="top: 1px; left: 3px" Text="Buscar" Width="100px">
                                                 <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                                             </telerik:RadButton>
                                         </td>
                                         <td class="auto-style1">&nbsp;&nbsp;</td>
                                         <td class="auto-style1">
                                             <telerik:RadButton ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click" Style="top: 1px; left: 3px" Width="100px">
                                                 <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png" />
                                             </telerik:RadButton>
                                         </td>

                                     </tr>
                                 </table>
                                 <div style="height: 5px">
                                 </div>
                                 <div>
                                    <h6><strong>Resumen:</strong></h6>
                                 </div>

                                 <telerik:RadPivotGrid ID="gsContadorAccesos" runat="server" ColumnHeaderZoneText="ColumnHeaderZone"
                                     Height="450"
                                     AllowFiltering="False"
                                     ShowFilterHeaderZone="False"
                                     ShowDataHeaderZone="False"
                                     ShowRowHeaderZone="False"
                                     ShowColumnHeaderZone="False"
                                     EnableConfigurationPanel="false"
                                     RowGroupsDefaultExpanded="False"
                                     TotalsSettings-GrandTotalsVisibility="RowsOnly"
                                     OnNeedDataSource="gsContadorAccesos_NeedDataSource"
                                     OnCellDataBound="gsContadorAccesos_CellDataBound"
                                     OnPreRender="gsContadorAccesos_PreRender"
                                     RowHeaderCellStyle-Font-Size="X-Small"
                                     PagerStyle-Font-Size="X-Small"
                                     OnPivotGridCellExporting="RadPivotGrid1_PivotGridCellExporting">
                                    

                                     <ExportSettings Excel-Format="Xlsx" OpenInNewWindow="true"></ExportSettings>
                                     <ClientSettings EnableFieldsDragDrop="false">
                                         <Scrolling AllowVerticalScroll="true"></Scrolling>
                                     </ClientSettings>
                                     

                                     <Fields>
                                         <telerik:PivotGridRowField DataField="nombreMenu" ZoneIndex="0">
                                             <CellStyle Width="100" Font-Size="X-Small" />
                                         </telerik:PivotGridRowField>

                                         <telerik:PivotGridRowField DataField="NombreUsuario" ZoneIndex="1">
                                             <CellStyle Width="250" Font-Size="X-Small" />
                                         </telerik:PivotGridRowField>


                                         <%--COLUMNAS DEL PIVOT--%>
                                         <telerik:PivotGridColumnField DataField="NombreTrimestre">
                                             <CellStyle Font-Size="X-Small" />
                                         </telerik:PivotGridColumnField>

                                         <telerik:PivotGridColumnField DataField="NombreMes">
                                             <CellStyle Font-Size="X-Small" />
                                         </telerik:PivotGridColumnField>


                                         <telerik:PivotGridAggregateField DataField="idMenu" Aggregate="Count" DataFormatString="{0:##,###0}">
                                             <HeaderCellTemplate>Total</HeaderCellTemplate>
                                             <CellStyle Font-Size="X-Small" />
                                         </telerik:PivotGridAggregateField>


                                     </Fields>
                                     <SortExpressions>
                                         <telerik:PivotGridSortExpression FieldName="nombreMenu" SortOrder="Ascending"></telerik:PivotGridSortExpression>
                                     </SortExpressions>
                                 </telerik:RadPivotGrid>
                             </div>
                        </div>
                   </telerik:RadPageView>
                   <telerik:RadPageView runat="server" ID="pageMenuSinAcceso" CssClass="col-md-12" Height="100%">
                       <div class="row">
                           <div class="col-md-12">
                               <table>
                                   <tr>
                                       <td class="auto-style1">
                                           <asp:Label ID="lblfechas" runat="server" CssClass="etiqueta" Text="Fechas:" Width="116px"></asp:Label>
                                       </td>
                                       <td class="auto-style1">
                                           <asp:Label ID="lblfechainicials" runat="server" CssClass="etiqueta" Text="Inicial: " Width="40px"></asp:Label>
                                       </td>
                                       <td class="auto-style1">
                                           <telerik:RadDatePicker ID="dpFechaInicialS" runat="server" DateInput-ReadOnly="true" Width="150px">
                                               <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                               </DateInput>
                                           </telerik:RadDatePicker>
                                       </td>
                                       <td class="auto-style1">
                                           <asp:Label ID="Label9" runat="server" CssClass="etiqueta" Text="Final: " Width="40px"></asp:Label>
                                       </td>
                                       <td class="auto-style1">
                                           <telerik:RadDatePicker ID="dpFechaFinalS" runat="server" DateInput-ReadOnly="true" Width="150px">
                                               <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                               </DateInput>
                                           </telerik:RadDatePicker>
                                       </td>
                                       <td class="auto-style1"></td>
                                       <td class="auto-style1">&nbsp;</td>
                                       <td>
                                           <asp:CheckBox ID="chkexpandidoS" Text="Mostrar Expandido" runat="server" CssClass="chkSmall" Font-Bold="false" Visible="false" />
                                       </td>
                                       <td class="auto-style1">&nbsp;</td>
                                       <td class="auto-style1">&nbsp;</td>
                                       <td class="auto-style1">
                                           <telerik:RadButton ID="btnBuscarS" runat="server" OnClick="btnBuscarS_Click" Style="top: 1px; left: 3px" Text="Buscar" Width="100px">
                                               <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                                           </telerik:RadButton>
                                       </td>

                                       <td class="auto-style1">&nbsp;&nbsp;</td>

                                       <td class="auto-style1">
                                           <telerik:RadButton ID="btnExcelS" runat="server" Text="Excel" OnClick="btnExcelS_Click" Style="top: 1px; left: 3px" Width="100px">
                                               <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png" />
                                           </telerik:RadButton>
                                       </td>
                                   </tr>
                               </table>
                               <div style="height: 5px">
                               </div>
                               <div>
                                    <h6><strong>Resumen:</strong></h6>
                               </div>
                               <telerik:RadPivotGrid
                                   ID="gsContadorSinAccesos" runat="server" ColumnHeaderZoneText="ColumnHeaderZone"
                                   Height="480"
                                   AllowFiltering="False"
                                   ShowFilterHeaderZone="False"
                                   ShowDataHeaderZone="False"
                                   ShowRowHeaderZone="False"
                                   ShowColumnHeaderZone="False"
                                   EnableConfigurationPanel="false"
                                   RowGroupsDefaultExpanded="False"
                                   TotalsSettings-GrandTotalsVisibility="RowsOnly"
                                   OnNeedDataSource="gsContadorSinAccesos_NeedDataSource"
                                   OnCellDataBound="gsContadorSinAccesos_CellDataBound"
                                   OnPreRender="gsContadorSinAccesos_PreRender"
                                   OnItemNeedCalculation="gsContadorSinAccesos_ItemNeedCalculation"
                                   RowHeaderCellStyle-Font-Size="X-Small"
                                   PagerStyle-Font-Size="X-Small"
                                   OnPivotGridCellExporting="RadPivotGrid1_PivotGridCellExporting">

                                   <ClientSettings EnableFieldsDragDrop="false">
                                       <Scrolling AllowVerticalScroll="true"></Scrolling>
                                   </ClientSettings>
                                   <Fields>

                                       <telerik:PivotGridRowField DataField="nombreMenu" ZoneIndex="0">
                                           <CellStyle Width="150" Font-Size="X-Small" />
                                       </telerik:PivotGridRowField>

                                       <telerik:PivotGridRowField DataField="nombrePerfil" ZoneIndex="1">
                                           <CellStyle Width="250" Font-Size="X-Small" />
                                       </telerik:PivotGridRowField>

                                       <telerik:PivotGridRowField DataField="Nombres" ZoneIndex="2">
                                           <CellStyle Width="250" Font-Size="X-Small" />
                                       </telerik:PivotGridRowField>



                                       <telerik:PivotGridColumnField DataField="Cantidad">
                                           <CellStyle Font-Size="X-Small" />
                                       </telerik:PivotGridColumnField>

                                       <telerik:PivotGridAggregateField DataField="idMenu" Aggregate="Count" DataFormatString="{0:##,###0}">
                                           <HeaderCellTemplate>Cantidad Usuarios</HeaderCellTemplate>
                                           <CellStyle Font-Size="X-Small" />
                                       </telerik:PivotGridAggregateField>
                                   </Fields>
                                   <SortExpressions>
                                       <telerik:PivotGridSortExpression FieldName="nombreMenu" SortOrder="Ascending"></telerik:PivotGridSortExpression>
                                   </SortExpressions>
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
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
        <div class="col-md-12">
            <asp:HiddenField ID="lblReporte" runat="server"  />
        </div>
        <div class="col-md-12">
            <asp:HiddenField ID="lblReporteS" runat="server"  />
        </div>
    </div>
</asp:Content>

