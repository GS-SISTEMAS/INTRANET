<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmEstadoCuenta.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.EstadoCuenta.frmEstadoCuenta" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="tk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Estado de cuenta</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var loadingSign = null;
        var contentCell = null;
        function OnClientShow(sender, args) {
            loadingSign = $get("loading");
            contentCell = sender._contentCell;
            if (contentCell && loadingSign) {
                contentCell.appendChild(loadingSign);
                contentCell.style.verticalAlign = "middle";
                loadingSign.style.display = "";
            }
        }
        function OnClientPageLoad(sender, args) {
            if (contentCell && loadingSign) {
                contentCell.removeChild(loadingSign);
                contentCell.style.verticalAlign = "";
                loadingSign.style.display = "none";
            }
        }

        function OnClientEntryAddingHandler(sender, eventArgs) {
            if (sender.get_entries().get_count() > 0) {
                eventArgs.set_cancel(true);
                alert("Solo se puede selecionar un elemento.");
            }
        }

        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("ibExcel") >= 0)
                args.set_enableAjax(false);

            if (args.get_eventTarget().indexOf("btnPDFDetalle") >= 0)
                args.set_enableAjax(false);

            if (args.get_eventTarget().indexOf("btnExcelDetalle") >= 0)
                args.set_enableAjax(false);

            if (args.get_eventTarget().indexOf("btnExcel") >= 0)
                args.set_enableAjax(false);
            if (args.get_eventTarget().indexOf("btnExpResumen") >= 0)
                args.set_enableAjax(false);
            if (args.get_eventTarget().indexOf("btnExpDetalle") >= 0)
                args.set_enableAjax(false);
            if (args.get_eventTarget().indexOf("btnExpPDFDetalle") >= 0)
                args.set_enableAjax(false);
            if (args.get_eventTarget().indexOf("btnPdf") >= 0)
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

        function ShowHistorial(objHistorial) {
            window.radopen("frmTrazabilidadMng.aspx?objHistorial=" + objHistorial, "rwTrazabilidad");
            return false;
        }

        function ShowGraficoPie(objCliente) {
            window.radopen("EstadoLetrasPieMng.aspx?objCliente=" + objCliente, "rwGraficos");
            return false;
        }

        function ShowVencidosPie(objCliente) {
            window.radopen("VencPorPlazoLetrasPieMng.aspx?objCliente=" + objCliente, "rwGraficos");
            return false;
        }

        function ShowVencidosMayorPlazoPie(objCliente) {
            window.radopen("VencPorPlazoFacturasPieMng.aspx?objCliente=" + objCliente, "rwGraficos");
            return false;
        }

        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= ramEstadoCuenta.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%= ramEstadoCuenta.ClientID %>").ajaxRequest("Registro," + arg);
            }
        }


    </script>

   <style type="text/css">
        .Rojo {
            background-color:red; 
        }

    </style>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramEstadoCuenta" runat="server">
        <AjaxSettings>

            <telerik:AjaxSetting AjaxControlID="body">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpEstadoCuenta" ControlID="rapEstadoCuenta"/>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpEstadoCuenta" ControlID="rapEstadoCuenta"/>
                       <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

             <telerik:AjaxSetting AjaxControlID="btnBuscarProvision">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpEstadoCuenta" ControlID="rapEstadoCuenta"/>
                       <telerik:AjaxUpdatedControl ControlID="lblMensajeProvision" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            

           <telerik:AjaxSetting AjaxControlID="btnBuscarResumenCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpEstadoCuenta" ControlID="rapEstadoCuenta"/>
                </UpdatedControls>
            </telerik:AjaxSetting>

               <telerik:AjaxSetting AjaxControlID="btnBuscarLegal">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpEstadoCuenta" ControlID="rapEstadoCuenta"/>
                     <telerik:AjaxUpdatedControl ControlID="lblMensajeLegal" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdEstadoCuenta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEstadoCuenta" LoadingPanelID="ralpEstadoCuenta"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>


            <telerik:AjaxSetting AjaxControlID="grdEstadoCuenta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEstadoCuenta" LoadingPanelID="ralpEstadoCuenta"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdEstadoCuentaCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEstadoCuentaCliente" LoadingPanelID="ralpEstadoCuenta"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

               <telerik:AjaxSetting AjaxControlID="grdEstadoCuenta_Legal">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEstadoCuenta_Legal" LoadingPanelID="ralpEstadoCuenta"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

                  <telerik:AjaxSetting AjaxControlID="grdEstadoCuenta_Provision">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEstadoCuenta_Provision" LoadingPanelID="ralpEstadoCuenta"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>


        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpEstadoCuenta" runat="server" Visible="True"></telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmVidaLey" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwVidaLey" runat="server" Width="100%" Height="100%" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>
            <telerik:RadWindow ID="rwExportarPDF" runat="server" Width="100%" Height="100%" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>
            <telerik:RadWindow ID="rwTrazabilidad" runat="server" Width="650px" Height="500px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true" OnClientShow="OnClientShow" OnClientPageLoad="OnClientPageLoad">
            </telerik:RadWindow>
            <telerik:RadWindow ID="rwGraficos" runat="server" Width="1050px" Height="500px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true" OnClientShow="OnClientShow" OnClientPageLoad="OnClientPageLoad">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="rapEstadoCuenta" runat="server" ClientEvents-OnRequestStart="requestStart" Height="100%" >
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblTitulo" runat="server" Text="Consultar estado de cuenta" CssClass="titulo"></asp:Label>
            </div>
        </div>

        <div class="row">
            <telerik:RadTabStrip runat="server" ID="stripPedido" MultiPageID="rmpRepEstadoCuenta" SelectedIndex="0" CssClass="col-md-12">
                <Tabs>
                    <telerik:RadTab Text="Resumen" Selected="True"></telerik:RadTab>
                    <telerik:RadTab Text="Legal"></telerik:RadTab>
                    <telerik:RadTab Text="Provisión"></telerik:RadTab>
                    <telerik:RadTab Text="Detalle"></telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
             <telerik:RadMultiPage runat="server" ID="rmpRepEstadoCuenta" SelectedIndex="0" Height="100%" CssClass="col-md-12">

                <telerik:RadPageView runat="server" ID="pageResumen" CssClass="col-md-12" Height="100%" Width="100%"> 
                        <div class="row">
                            <div class="col-md-1">
                                 <asp:Label ID="Label10" runat="server" CssClass="etiqueta" Text="Fecha Hasta: "></asp:Label>
                            </div>
                               <div class="col-md-2">
                                   <telerik:RadDatePicker ID="dpFechaHastaCliente" runat="server" DateInput-ReadOnly="true" >
                                        <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                        </DateInput>
                                  </telerik:RadDatePicker>
                            </div>
                               <div class="col-md-1">
                                     <asp:RadioButtonList ID="rbtResumenCliente" runat="server" Font-Size="8pt" >
                                        <asp:ListItem Selected="True" Value="0"> Todos</asp:ListItem>
                                        <asp:ListItem Value="1"> Vencidos</asp:ListItem>
                                    </asp:RadioButtonList>
                            </div>
                               <div class="col-md-1">
                                   <telerik:RadButton ID="btnBuscarResumenCliente" runat="server" OnClick="btnBuscarResumenCliente_Click" style="top: 1px; left: 3px" Text="Buscar" Width="100px">
                                      <Icon PrimaryIconUrl="~/Images/Icons/search-16.png" />
                                   </telerik:RadButton>
                              </div>
                               <div class="col-md-1">
                                    <telerik:RadButton ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click">
                                        <Icon PrimaryIconUrl="~/Images/Icons/excel-16.png"/>
                                    </telerik:RadButton>
                              </div>
                               <div class="col-md-6">
                               </div>
                        </div>
                  
                        <div class="row">
                           <div class="col-md-12">
                                 <asp:Label ID="Label5" runat="server" CssClass="etiqueta" Text="Resumen: " Width="116px"></asp:Label>
                           </div>
                        </div>

                        <div class="row">
                           <div class="col-md-12">
                               <telerik:RadPivotGrid ID="grdEstadoCuentaCliente" runat="server" Width="100%" Height="400px" AllowFiltering="false" 

                                        ShowFilterHeaderZone="false" OnPivotGridCellExporting="grdEstadoCuentaCliente_PivotGridCellExporting"
                                        ShowDataHeaderZone="false" ShowRowHeaderZone="false" ShowColumnHeaderZone="false" EnableConfigurationPanel="true" 
                                        TotalsSettings-GrandTotalsVisibility="RowsOnly"  OnNeedDataSource="grdEstadoCuentaCliente_NeedDataSource"
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
                                            <telerik:PivotGridRowField DataField="ClienteNombre" ZoneIndex="1" >
                                                <CellStyle Width="250px" />
                                            </telerik:PivotGridRowField>

                                            <telerik:PivotGridColumnField DataField="Periodo">
                                            </telerik:PivotGridColumnField>

                                           <telerik:PivotGridAggregateField DataField="DeudaPorVencer" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    DeudaPorVencer
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                           <telerik:PivotGridAggregateField  DataField="DeudaPorVencer30" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    PorVencer30
                                                </HeaderCellTemplate>
                                           </telerik:PivotGridAggregateField>
                                         

                                            <telerik:PivotGridAggregateField DataField="Vencido01a08" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Vencido01a08
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                           <telerik:PivotGridAggregateField DataField="Vencido09a30" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Vencido09a30
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                            <telerik:PivotGridAggregateField DataField="Vencido31a60" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Vencido31a60
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>



                                            <telerik:PivotGridAggregateField DataField="Vencido61a120" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Vencido61a120
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                            <telerik:PivotGridAggregateField DataField="Vencido121a360" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Vencido121a360
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                            <telerik:PivotGridAggregateField DataField="Vencido361a720" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Vencido361a720
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                            <telerik:PivotGridAggregateField DataField="Vencido721aMas" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Vencido721aMas
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
											
											<telerik:PivotGridAggregateField DataField="LimiteCredito" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Límete de Crédito
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>											
											
											<telerik:PivotGridAggregateField DataField="CreditoDisponible" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Crédito Disponible
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                           <telerik:PivotGridAggregateField CellStyle-Font-Bold="true"  DataField="DeudaVencida" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate >
                                                   Grand Total
                                                </HeaderCellTemplate>            
                                               <CellStyle BackColor="LightGreen" />
                                           </telerik:PivotGridAggregateField>

                                        </Fields>
                               
                                    </telerik:RadPivotGrid>
                           </div>
                        </div>
                  </telerik:RadPageView>

                <telerik:RadPageView runat="server" ID="pageLegal" CssClass="col-md-12" Height="100%"> 
                      <div class="row">
                         <div class="col-md-12">
                             <div class="row">
                               <div class="col-md-1">
                                 <asp:Label ID="Label2" runat="server" CssClass="etiqueta" Text="Fecha Hasta: "></asp:Label>
                            </div>
                               <div class="col-md-2">
                                   <telerik:RadDatePicker ID="dpFechaLegal" runat="server" DateInput-ReadOnly="true" >
                                        <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                        </DateInput>
                                  </telerik:RadDatePicker>
                            </div>
                               <div class="col-md-1">
                                     <asp:RadioButtonList ID="rbtVencidosLegal" runat="server" Font-Size="8pt" >
                                        <asp:ListItem Selected="True" Value="0"> Todos</asp:ListItem>
                                        <asp:ListItem Value="1"> Vencidos</asp:ListItem>
                                    </asp:RadioButtonList>
                            </div>
                               <div class="col-md-1">
                                   <telerik:RadButton ID="btnBuscarLegal" runat="server" OnClick="btnBuscarLegal_Click" style="top: 1px; left: 3px" Text="Buscar" Width="100px">
                                      <Icon PrimaryIconUrl="~/Images/Icons/search-16.png" />
                                   </telerik:RadButton>
                              </div>
                               <div class="col-md-1">
                                    <telerik:RadButton ID="btnExcelLegal" runat="server" Text="Excel" OnClick="btnExcel_Click">
                                        <Icon PrimaryIconUrl="~/Images/Icons/excel-16.png"/>
                                    </telerik:RadButton>
                              </div>
                               <div class="col-md-6">
                               </div>
                             </div>
 
                             <div class="row">
                                <div class="col-md-12">
                                        <asp:Label ID="Label6" runat="server" CssClass="etiqueta" Text="Resumen: " Width="116px"></asp:Label>
                                </div>
                            </div>

                           <div class="row">
                               <div class="col-md-12">
                                 <telerik:RadPivotGrid ID="grdEstadoCuenta_Legal" runat="server" Width="100%" Height="400px" AllowFiltering="false" 

                                        ShowFilterHeaderZone="false" OnPivotGridCellExporting="grdEstadoCuenta_Legal_PivotGridCellExporting"
                                        ShowDataHeaderZone="false" ShowRowHeaderZone="false" ShowColumnHeaderZone="false" EnableConfigurationPanel="true" 
                                        TotalsSettings-GrandTotalsVisibility="RowsOnly"  OnNeedDataSource="grdEstadoCuenta_Legal_NeedDataSource"
                                        AllowSorting="true"
                                        RowGroupsDefaultExpanded="false"
                                        >

                                        <ExportSettings Excel-Format="Xlsx" OpenInNewWindow="true"></ExportSettings>
                                        <ClientSettings EnableFieldsDragDrop="false" >
                                            <Scrolling AllowVerticalScroll="true"></Scrolling>
                                        </ClientSettings>

                                        <Fields>
                                           <telerik:PivotGridRowField DataField="PropiedadLegal" ZoneIndex="1">
                                                <CellStyle Width="150px" />
                                            </telerik:PivotGridRowField>

                                             <telerik:PivotGridRowField DataField="ZonaNombre" ZoneIndex="2">
                                                <CellStyle Width="150px" />
                                            </telerik:PivotGridRowField>

                                            <telerik:PivotGridRowField DataField="ClienteNombre" ZoneIndex="3" >
                                                <CellStyle Width="250px" />
                                            </telerik:PivotGridRowField>


                                            <telerik:PivotGridColumnField DataField="Periodo">
                                            </telerik:PivotGridColumnField>

                                           <telerik:PivotGridAggregateField DataField="DeudaPorVencer" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    DeudaPorVencer
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                           <telerik:PivotGridAggregateField  DataField="DeudaPorVencer30" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    PorVencer30
                                                </HeaderCellTemplate>
                                           </telerik:PivotGridAggregateField>
                                         

                                            <telerik:PivotGridAggregateField DataField="Vencido01a08" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Vencido01a08
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                           <telerik:PivotGridAggregateField DataField="Vencido09a30" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Vencido09a30
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                            <telerik:PivotGridAggregateField DataField="Vencido31a60" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Vencido31a60
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>



                                            <telerik:PivotGridAggregateField DataField="Vencido61a120" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Vencido61a120
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                            <telerik:PivotGridAggregateField DataField="Vencido121a360" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Vencido121a360
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                            <telerik:PivotGridAggregateField DataField="Vencido361a720" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Vencido361a720
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                            <telerik:PivotGridAggregateField DataField="Vencido721aMas" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Vencido721aMas
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
											
											<telerik:PivotGridAggregateField DataField="LimiteCredito" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Límete de Crédito
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>											
											
											<telerik:PivotGridAggregateField DataField="CreditoDisponible" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Crédito Disponible
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                           <telerik:PivotGridAggregateField CellStyle-Font-Bold="true"  DataField="DeudaVencida" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate >
                                                   Grand Total
                                                </HeaderCellTemplate>            
                                               <CellStyle BackColor="LightGreen" />
                                           </telerik:PivotGridAggregateField>

                                        </Fields>
                               
                                    </telerik:RadPivotGrid>
                               </div>
                            </div>

                            </div>
                        </div>
                  </telerik:RadPageView>

                <telerik:RadPageView runat="server" ID="pageProvision" CssClass="col-md-12" Height="100%"> 
                      <div class="row">
                         <div class="col-md-12">
                             <div class="row">
                               <div class="col-md-1">
                                 <asp:Label ID="Label4" runat="server" CssClass="etiqueta" Text="Fecha Hasta: "></asp:Label>
                            </div>
                               <div class="col-md-2">
                                   <telerik:RadDatePicker ID="dpFechaProvision" runat="server" DateInput-ReadOnly="true" >
                                        <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                        </DateInput>
                                  </telerik:RadDatePicker>
                            </div>
                               <div class="col-md-1">
                                     <asp:RadioButtonList ID="rbtVencidosProvision" runat="server" Font-Size="8pt" >
                                        <asp:ListItem Selected="True" Value="0"> Todos</asp:ListItem>
                                        <asp:ListItem Value="1"> Vencidos</asp:ListItem>
                                    </asp:RadioButtonList>
                            </div>
                               <div class="col-md-1">
                                   <telerik:RadButton ID="btnBuscarProvision" runat="server" OnClick="btnBuscarProvision_Click" style="top: 1px; left: 3px" Text="Buscar" Width="100px">
                                      <Icon PrimaryIconUrl="~/Images/Icons/search-16.png" />
                                   </telerik:RadButton>
                              </div>
                               <div class="col-md-1">
                                    <telerik:RadButton ID="btnExcelProvision" runat="server" Text="Excel" OnClick="btnExcel_Click">
                                        <Icon PrimaryIconUrl="~/Images/Icons/excel-16.png"/>
                                    </telerik:RadButton>
                              </div>
                               <div class="col-md-6">
                               </div>
                             </div>
 
                             <div class="row">
                                <div class="col-md-12">
                                        <asp:Label ID="Label7" runat="server" CssClass="etiqueta" Text="Resumen: " Width="116px"></asp:Label>
                                </div>
                            </div>

                           <div class="row">
                               <div class="col-md-12">
                                 <telerik:RadPivotGrid ID="grdEstadoCuenta_Provision" runat="server" Width="100%" Height="400px"  AllowFiltering="false" 

                                        ShowFilterHeaderZone="false" OnPivotGridCellExporting="grdEstadoCuenta_Provision_PivotGridCellExporting"
                                        ShowDataHeaderZone="false" ShowRowHeaderZone="false" ShowColumnHeaderZone="false" EnableConfigurationPanel="true" 
                                        TotalsSettings-GrandTotalsVisibility="RowsOnly"  OnNeedDataSource="grdEstadoCuenta_Provision_NeedDataSource"
                                        AllowSorting="true"
                                        RowGroupsDefaultExpanded="false"  >

                                        <ExportSettings Excel-Format="Xlsx" OpenInNewWindow="true"></ExportSettings>
                                        <ClientSettings EnableFieldsDragDrop="false" >
                                            <Scrolling AllowVerticalScroll="true"></Scrolling>
                                        </ClientSettings>

                                        <Fields>
                                           <telerik:PivotGridRowField DataField="ZonaCobranza" ZoneIndex="0">
                                                <CellStyle Width="150px" />
                                            </telerik:PivotGridRowField>

                                                 <telerik:PivotGridRowField DataField="PropiedadCliente" ZoneIndex="1">
                                                <CellStyle Width="150px" />
                                            </telerik:PivotGridRowField>

                                             <telerik:PivotGridRowField DataField="ZonaNombre" ZoneIndex="2">
                                                <CellStyle Width="150px" />
                                            </telerik:PivotGridRowField>

                                            <telerik:PivotGridRowField DataField="ClienteNombre" ZoneIndex="3" >
                                                <CellStyle Width="250px" />
                                            </telerik:PivotGridRowField>


                                            <telerik:PivotGridColumnField DataField="Periodo">
                                            </telerik:PivotGridColumnField>

                                           <telerik:PivotGridAggregateField DataField="DeudaPorVencer" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    DeudaPorVencer
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                           <telerik:PivotGridAggregateField  DataField="DeudaPorVencer30" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    PorVencer30
                                                </HeaderCellTemplate>
                                           </telerik:PivotGridAggregateField>
                                         

                                            <telerik:PivotGridAggregateField DataField="Vencido01a08" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Vencido01a08
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                           <telerik:PivotGridAggregateField DataField="Vencido09a30" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Vencido09a30
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                            <telerik:PivotGridAggregateField DataField="Vencido31a60" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Vencido31a60
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>



                                            <telerik:PivotGridAggregateField DataField="Vencido61a120" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Vencido61a120
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                            <telerik:PivotGridAggregateField DataField="Vencido121a360" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Vencido121a360
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                            <telerik:PivotGridAggregateField DataField="Vencido361a720" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Vencido361a720
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                            <telerik:PivotGridAggregateField DataField="Vencido721aMas" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Vencido721aMas
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
											
											<telerik:PivotGridAggregateField DataField="LimiteCredito" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Límete de Crédito
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>											
											
											<telerik:PivotGridAggregateField DataField="CreditoDisponible" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Crédito Disponible
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>

                                           <telerik:PivotGridAggregateField CellStyle-Font-Bold="true"  DataField="DeudaVencida" Aggregate="Sum" DataFormatString="${0:##,###0.##}">
                                                <HeaderCellTemplate >
                                                   Grand Total
                                                </HeaderCellTemplate>            
                                               <CellStyle BackColor="LightGreen" />
                                           </telerik:PivotGridAggregateField>

                                        </Fields>
                               
                                    </telerik:RadPivotGrid>
                               </div>
                            </div>

                            </div>
                        </div>
                  </telerik:RadPageView>

               <telerik:RadPageView runat="server" ID="pageDetalle" CssClass="col-md-12" Height="100%">
               <div class="col-md-12">
                    <div class="row">
                     <div class="col-md-4" >
                             <div class="row">
                                  <div class="col-md-2">
                                           <asp:Label ID="Label8" runat="server" CssClass="etiqueta" Text="Fecha Hasta: " Width="73px"></asp:Label>
                                  </div>
                                  <div class="col-md-10">
                                            <telerik:RadDatePicker ID="dpFinalEmision" runat="server" DateInput-ReadOnly="true" Width="200px">
                                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                                </DateInput>
                                            </telerik:RadDatePicker>
                                  </div>
                             </div>
                             <div class="row">
                                  <div class="col-md-2">
                                             <asp:Label ID="lblCliente" runat="server" CssClass="etiqueta" Text="Cliente:"></asp:Label>
                                      </div>
                                  <div class="col-md-10">
                                              <telerik:RadAutoCompleteBox ID="acbCliente" runat="server" AllowCustomEntry="true" DropDownHeight="150px" 
                                                DropDownWidth="300px" EmptyMessage="Selec. cliente" InputType="Text" 
                                                OnClientEntryAdding="OnClientEntryAddingHandler" TextSettings-SelectionMode="Single" Width="300px">
                                                <WebServiceSettings Method="Agenda_BuscarCliente" Path="frmEstadoCuenta.aspx" />
                                            </telerik:RadAutoCompleteBox>
                                      </div>
                             </div>
                             <div class="row">
                                  <div class="col-md-2">
                                            <asp:Label ID="lblVendedor" runat="server" CssClass="etiqueta" Text="Vendedor:"></asp:Label>
                                  </div>
                                  <div class="col-md-10">     
                                            <telerik:RadAutoCompleteBox ID="acbVendedor" runat="server" AllowCustomEntry="true" DropDownHeight="150px" DropDownWidth="300px" EmptyMessage="Selec. vendedor" 
                                                InputType="Text" OnClientEntryAdding="OnClientEntryAddingHandler" TextSettings-SelectionMode="Single" Width="300px">
                                                <WebServiceSettings Method="Agenda_BuscarVendedor" Path="frmEstadoCuenta.aspx" />
                                            </telerik:RadAutoCompleteBox>
                                  </div>
                            </div>
                             <div class="row">
                                  <div class="col-md-2">
                                            <asp:Label ID="Label9" runat="server" CssClass="etiqueta" Text="ZonaCliente:"></asp:Label>
                                  </div>
                                  <div class="col-md-10">     
                                              <telerik:RadComboBox ID="cboZona" runat="server" Width="300px" Enabled="true" DropDownWidth="300px" >
                                              </telerik:RadComboBox>
                                  </div>
                            </div>
                      </div>
                     <div class="col-md-1" >
                          <div class="row">
                               <div class="col-md-12">
                                    <asp:RadioButtonList ID="rbtTipo" runat="server" Font-Size="8pt" Width="80px">
                                        <asp:ListItem Value="0" Selected="True"> Todos</asp:ListItem>
                                        <asp:ListItem Value="1"> Vencidos</asp:ListItem>
                                    </asp:RadioButtonList>
                               </div>
                          </div>
                     </div>
                     <div class="col-md-1" >
                           <div class="row">
                               <div class="col-md-12">
                                    <telerik:RadButton ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Text="Buscar" Width="100%">
                                        <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                                    </telerik:RadButton>
                               </div>
                          </div>
                           <div class="row">
                               <div class="col-md-12">
                                    <telerik:RadButton ID="btnExcelDetalle" runat="server" Text="Excel" OnClick="btnExcelDetalle_Click" Width="100%">
                                            <Icon PrimaryIconUrl="../../Images/Icons/excel-16.png"/>
                                     </telerik:RadButton>
                               </div>
                          </div>
                           <div class="row">
                               <div class="col-md-12">
                                     <telerik:RadButton ID="btnPDFDetalle" runat="server" Text="PDF" OnClick="btnPDFDetalle_Click" Width="100%">
                                        <Icon PrimaryIconUrl="../../Images/Icons/pdf-16.png"/>
                                    </telerik:RadButton>
                               </div>
                          </div>
                     </div>
                     <div class="col-md-2" >
                          <div class="row">
                                <div class="col-md-12">
                                    <telerik:RadButton ID="RadButton1" runat="server" Text="Doc Estados" OnClick="RadButton1_OnClick" Width="100%">
                                        <Icon PrimaryIconUrl="../../Images/Icons/analytics-16.png"/>
                                    </telerik:RadButton>
                               </div>
                         </div>
                           <div class="row">
                                <div class="col-md-12">
                                       <telerik:RadButton ID="rdbVencidosPlazo" runat="server" Text="Venc. en Plazo" OnClick="rdbVencidosPlazo_OnClick" Width="100%">
                                            <Icon PrimaryIconUrl="../../Images/Icons/analytics-16.png"/>
                                        </telerik:RadButton>
                               </div>
                         </div>
                             <div class="row">
                                <div class="col-md-12">
                                        <telerik:RadButton ID="rabVencidosMayorPlazo" runat="server" Text="Venc. Fuera Plazo" OnClick="rabVencidosMayorPlazo_OnClick" Width="100%">
                                        <Icon PrimaryIconUrl="../../Images/Icons/analytics-16.png"/>
                                    </telerik:RadButton>
                               </div>
                         </div>
                     </div>
                     <div class="col-md-4" >
                             <telerik:RadGrid ID="grdGarantia" runat="server" Width="100%" Height="120px"
                            AllowSorting="false" AllowMultiRowSelection="false" ShowGroupPanel="false"
                            AutoGenerateColumns="False" Visible="false">
                            <MasterTableView TableLayout="Fixed" DataKeyNames="ID_Agenda"
                                AllowMultiColumnSorting="true" ShowGroupFooter="true">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="TipoGarantia" HeaderText="Tipo Garantia"
                                        HeaderStyle-Width="130px" AllowSorting="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FechaVencimiento" HeaderText="F.Venc."
                                        HeaderStyle-Width="90px" AllowSorting="false" DataFormatString="{0:dd/MM/yyyy}">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridHyperLinkColumn DataTextField="Valor" HeaderText="Valor" HeaderStyle-Width="80px"
                                        DataTextFormatString="{0:$ #,##0.00}"
                                        HeaderStyle-HorizontalAlign="Left"
                                        AllowSorting="false" FooterStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridHyperLinkColumn>
                                       <telerik:GridBoundColumn DataField="Observaciones" HeaderText="Observaciones"
                                        HeaderStyle-Width="180px" AllowSorting="false">
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings  >
                                      <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                                      <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                                      <Selecting AllowRowSelect="true"/>
                            </ClientSettings>

                        </telerik:RadGrid>
                         </div>
                 </div>
                    

                  <div class="row">
                        <div class="col-md-12"> 
                            <div id="loading" style="border: solid 1px Red; width: 100px; height: 50px; display: none;
                                 text-align: center; margin: auto;">
                                  Custom<br />
                                  loading....
                            </div>
                       </div>
                  </div>



                    <div class="row">
                       <div class="row">
                          <div class="col-md-12">
                                <div class="col-md-4">
                                    <asp:Label ID="Label1" runat="server" CssClass="etiqueta" Text="Resumen: " Width="116px"></asp:Label>
                                </div>
                               
                               <div class="col-md-8">
                                    <div class="row">
                                          <div class="col-md-12">


                                         
                                                <asp:Label ID="lbleMail" runat="server" CssClass="etiqueta" Text="" ForeColor="Blue" ></asp:Label>
                                       <asp:Label ID="Label11" runat="server" CssClass="etiqueta" Text="&nbsp;"   ></asp:Label>
                                        
                                                <asp:Label ID="lblCelular" runat="server" CssClass="etiqueta" Text="" ForeColor="Blue"></asp:Label>
                                     
                                       <asp:Label ID="Label12" runat="server" CssClass="etiqueta" Text="&nbsp;"   ></asp:Label>
                                                <asp:Label ID="lblOficina" runat="server" CssClass="etiqueta" Text="" ForeColor="Blue"></asp:Label>
                                    
                                      <asp:Label ID="Label13" runat="server" CssClass="etiqueta" Text="&nbsp;"   ></asp:Label>
                                                <asp:Label ID="lblContacto" runat="server" CssClass="etiqueta" Text="" ForeColor="Blue"></asp:Label>
                                   

                                         
                                      </div> 
                                </div>
                              </div>
                        
                            
                         </div>
                       </div>

 
                   <div class="row">
                            <div class="col-md-6">
                                <telerik:RadGrid ID="rdg_Observaciones"
                                    runat="server"
                                    Width="100%" Height="80px"
                                    AutoGenerateColumns="false"
                                    OnNeedDataSource="rdg_Observaciones_NeedDataSource">
                                    <MasterTableView TableLayout="Fixed">
                                        <Columns>
                                            <tk:GridBoundColumn DataField="FechaObservacion" HeaderText="Fecha" HeaderStyle-Width="30%" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}"></tk:GridBoundColumn>
                                            <tk:GridBoundColumn DataField="Observacion" HeaderText="Observación" HeaderStyle-Width="70%" DataType="System.String"></tk:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                                        <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                            <div class="col-md-6">
                                <telerik:RadGrid ID="rdg_LineaCredito" 
                                    runat="server"
                                    Width="100%" Height="80px"
                                    AutoGenerateColumns="false"
                                    OnNeedDataSource="rdg_LineaCredito_NeedDataSource">
                                    <MasterTableView TableLayout="Fixed">
                                        <Columns>
                                            <tk:GridBoundColumn DataField="FechaHora" HeaderText="Fecha" HeaderStyle-Width="30%" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy hh:mm:ss}"></tk:GridBoundColumn>
                                            <tk:GridBoundColumn DataField="loginUsuario" HeaderText="Usuario" HeaderStyle-Width="30%" DataType="System.String"></tk:GridBoundColumn>
                                            <tk:GridBoundColumn DataField="LineaAnterior" HeaderText="Linea Anterior" HeaderStyle-Width="20%" DataType="System.Decimal" DataFormatString="$ {0:#,##0.00}"></tk:GridBoundColumn>
                                            <tk:GridBoundColumn DataField="LineaActual" HeaderText="Linea Actual" HeaderStyle-Width="20%" DataType="System.Decimal" DataFormatString="$ {0:#,##0.00}"></tk:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                                        <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                    </div>

                            <div class="row">
                       <div class="col-md-12">
                           <telerik:RadGrid ID="grdResumenCliente" runat="server" AutoGenerateColumns="False"
                               Height="120px" Width="100%"
                               OnNeedDataSource="grdResumenCliente_NeedDataSource"  
                                Culture="es-ES" GroupPanelPosition="Top"  
                                ShowFooter="true"
                               >

                               <ClientSettings AllowDragToGroup="false">
                               </ClientSettings>

                               <MasterTableView TableLayout="Fixed" DataKeyNames="id_agenda"  >
                            <Columns>
                            <telerik:GridBoundColumn DataField="id_agenda" HeaderText="Código"  HeaderStyle-Width="85px" AllowSorting="false"   >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="AgendaNombre" HeaderText="Cliente" HeaderStyle-Width="250px" AllowSorting="false"
                                Aggregate="Count" FooterText="Total Cliente: " >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="EstadoDes" HeaderText="Estado"  HeaderStyle-Width="55px" AllowSorting="false"   >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="DiasCredito" HeaderText="Díascrédito "  HeaderStyle-Width="80px" AllowSorting="false"   >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="BloqueoLineaCredito" HeaderText="DíasGracia"  HeaderStyle-Width="70px" AllowSorting="false" Visible="false"   >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="FechaVCMTLinea" HeaderText="VcmtLínea"  HeaderStyle-Width="70px" AllowSorting="false" DataFormatString="{0:dd/MM/yyyy}"   >
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn DataField="AprobadoDes" HeaderText="LíneaAprob."  HeaderStyle-Width="100px" AllowSorting="false"  HeaderTooltip="Línea Credito Aprobada"  >
                                   <ItemStyle HorizontalAlign="Center"/> 
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="LineaCredito" HeaderText="LineaCredito" HeaderStyle-Width="80px" 
                               Aggregate="Sum"   DataFormatString="{0:#,##0.00}"  FooterStyle-HorizontalAlign="Right"  >
                                 <ItemStyle HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>


                                <telerik:GridBoundColumn DataField="XFacturar"  HeaderText="xFacturar" HeaderStyle-Width="80px" AllowSorting="false" 
                             Aggregate="Sum"  FooterStyle-HorizontalAlign="Right"  DataFormatString="{0:#,##0.00}"  >
                                 <ItemStyle HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn DataField="NoVencido"  HeaderText="NoVencido" HeaderStyle-Width="80px" AllowSorting="false" DataFormatString="{0:#,##0.00}"
                                Aggregate="Sum"  FooterStyle-HorizontalAlign="Right" > 
                                 <ItemStyle HorizontalAlign="Right" /> 
                            </telerik:GridBoundColumn>
                            
                           <telerik:GridBoundColumn DataField="DeudaVencida"  HeaderText="DeudaVencida" HeaderStyle-Width="95px" AllowSorting="false" 
                             Aggregate="Sum"  FooterStyle-HorizontalAlign="Right"  DataFormatString="{0:#,##0.00}"  >
                                 <ItemStyle HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>


                            <telerik:GridBoundColumn DataField="DeudaTotal"  HeaderText="DeudaTotal" HeaderStyle-Width="90px" AllowSorting="false" 
                              Aggregate="Sum"  FooterStyle-HorizontalAlign="Right"   DataFormatString="{0:#,##0.00}"  >
                                 <ItemStyle HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="CreditoDisponible" HeaderText="Cred.Disp" HeaderStyle-Width="90px"   HeaderStyle-HorizontalAlign="Right"   
                                Aggregate="Sum"   FooterStyle-HorizontalAlign="Right" DataFormatString="{0:#,##0.00}" >
                                <ItemStyle HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>


                            <telerik:GridBoundColumn DataField="PorVencer30"  HeaderText="PorVencer30" HeaderStyle-Width="80px" AllowSorting="false" DataFormatString="{0:#,##0.00}"
                                Aggregate="Sum"  FooterStyle-HorizontalAlign="Right" > 
                                 <ItemStyle HorizontalAlign="Right" /> 
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Vencido01a30" HeaderText="Vencido01a30" HeaderStyle-Width="90px"   DataFormatString="{0:#,##0.00}"  
                                Aggregate="Sum"  FooterStyle-HorizontalAlign="Right"  > 
                                <ItemStyle HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Vencido31a60" HeaderText="Vencido31a60" HeaderStyle-Width="100px"   DataFormatString="{0:#,##0.00}"
                                 Aggregate="Sum"  FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="Vencido61a120" HeaderText="Vencido61a120" HeaderStyle-Width="100px"  DataFormatString="{0:#,##0.00}" 
                                 Aggregate="Sum"  FooterStyle-HorizontalAlign="Right"  HeaderStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>

                              <telerik:GridBoundColumn DataField="Vencido121a360" HeaderText="Vencido121a360" HeaderStyle-Width="100px" 
                              Aggregate="Sum"  FooterStyle-HorizontalAlign="Right"  DataFormatString="{0:#,##0.00}"  HeaderStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Vencido361amas" HeaderText="Vencido361aMás" HeaderStyle-Width="100px"  
                                Aggregate="Sum"  FooterStyle-HorizontalAlign="Right" DataFormatString="{0:#,##0.00}" HeaderStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>

                        </Columns>
                    </MasterTableView>
                 <ClientSettings>
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="2" ></Scrolling>
                    <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                      <Selecting AllowRowSelect="true"/>
                </ClientSettings>
                </telerik:RadGrid>
                       </div>
                  </div>


                  <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="Label3" runat="server" CssClass="etiqueta" Text="Detalle: " Width="116px"></asp:Label></td>
                        </div>
                  </div>

                  <div class="row">
                       <div class="col-md-12">
                          <telerik:RadGrid ID="grdEstadoCuenta" runat="server" AllowMultiRowSelection="false"
                    AutoGenerateColumns="False" Height="250px"  Width="100%"
                    OnNeedDataSource="grdEstadoCuenta_NeedDataSource"   
                    OnItemCommand="grdEstadoCuenta_OnItemCommand"
                               ShowFooter="true"
                     >
  
                    <ExportSettings Excel-Format="ExcelML" ExportOnlyData="true" OpenInNewWindow="true"/>
                    <MasterTableView TableLayout="Fixed" DataKeyNames="TipoDocumento"
                        AllowMultiColumnSorting="true"  ShowGroupFooter="true"  >
                         <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                   <telerik:GridGroupByField FieldAlias="CODIGO" FieldName="id_agenda" />
                                    <telerik:GridGroupByField FieldAlias="CLIENTE" FieldName="ClienteNombre" />
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="id_agenda"  />
                                    <telerik:GridGroupByField FieldName="ClienteNombre"/>
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                        </GroupByExpressions>
                        <Columns>
                            <telerik:GridTemplateColumn>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibEditar" runat="server" ImageUrl="~/Images/Icons/analytics-16.png" CommandArgument='<%# Eval("ID_Financiamiento") %>'  CommandName="Traza"/>
                                </ItemTemplate>
                                <HeaderStyle Width="40px"/>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="TipoDocumento" HeaderText="TipoDocumento" UniqueName="TipoDocumento" HeaderStyle-Width="160px" AllowSorting="false"  >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="NroDocumento" Display="true" HeaderText="NumDocumento" HeaderStyle-Width="140px" AllowSorting="false"
                                Aggregate="Count" FooterText="Total Documentos: ">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="numeroLetra" Display="true" HeaderText="NumLetra" HeaderStyle-Width="95px" AllowSorting="false">
                                
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Referencia"  HeaderText="Referencia" HeaderStyle-Width="150px" AllowSorting="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Fecha" HeaderText="F.Emisión" HeaderStyle-Width="80px" DataFormatString="{0:dd/MM/yyyy}"  >
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FechaVencimiento" HeaderText="F.Vcmto" HeaderStyle-Width="80px" DataFormatString="{0:dd/MM/yyyy}"   >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="DiasMora" HeaderText="DiasMora" HeaderStyle-Width="60px" >
                                <ItemStyle HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>
   
                            <telerik:GridBoundColumn DataField="monedasigno" HeaderText="Mon" HeaderStyle-Width="45px" AllowSorting="false"
                               >
                            </telerik:GridBoundColumn>
                  
                            <telerik:GridBoundColumn DataField="Importe" HeaderText="Importe" HeaderStyle-Width="65px" 
                                DataFormatString="{0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" AllowSorting="false"  FooterText="Total:"  FooterStyle-HorizontalAlign="Right">
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DeudaSoles" HeaderText="Deuda(S/)" HeaderStyle-Width="60px" 
                                DataFormatString="{0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" AllowSorting="false"
                                Aggregate="Sum"  FooterStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DeudaDolares" HeaderText="Deuda($)" HeaderStyle-Width="60px" 
                             DataFormatString="{0:#,##0.00}" HeaderStyle-HorizontalAlign="Right" AllowSorting="false" 
                                Aggregate="Sum"  FooterStyle-HorizontalAlign="Right" > 
                                <ItemStyle HorizontalAlign="Right"/> 
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="EstadoDoc" HeaderText="Estado Doc." HeaderStyle-Width="100px" AllowSorting="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Banco" HeaderText="Banco" HeaderStyle-Width="200px" AllowSorting="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NumeroUnico" HeaderText="NumeroUnico" HeaderStyle-Width="100px" AllowSorting="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_Financiamiento" HeaderText="ID_Financiamiento" HeaderStyle-Width="100px" AllowSorting="false">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>

                <ClientSettings>
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                    <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                     <Selecting AllowRowSelect="true"/>
                </ClientSettings>
                    <GroupingSettings ShowUnGroupButton="false" /> 
                </telerik:RadGrid>
                       </div>
                  </div>



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
              <asp:Label ID="lblDateResumen1" runat="server" Visible="false"></asp:Label>
              <asp:Label ID="lblDateLegal1" runat="server" Visible="false"></asp:Label>
              <asp:Label ID="lblDateProvision1" runat="server" Visible="false"></asp:Label>
              <asp:Label ID="lblDateDetalle1" runat="server" Visible="false"></asp:Label>
        </div>
    </div>
</asp:Content>
