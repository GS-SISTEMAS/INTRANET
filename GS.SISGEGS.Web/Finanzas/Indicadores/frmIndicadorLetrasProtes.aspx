<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmIndicadorLetrasProtes.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.Indicadores.frmIndicadorLetrasProtes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Indicador de Letras Protestadas
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

     <style type="text/css">
         .bm {
                margin: 0 0 0 15px;
                font-size:small;
                font-weight:bold;
            }

          .Sangri {
                margin: 0 0 0 15px;
   
            }

    </style>

    <script type="text/javascript">

        function requestStart(sender, args) {

            if (args.get_eventTarget().indexOf("btnExcel") >= 0)
                args.set_enableAjax(false);
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramEstadoCuenta" runat="server">
        <AjaxSettings>

            <telerik:AjaxSetting AjaxControlID="body">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpIndicadoresCobranzas" ControlID="rapIndicadoresCobranzas"/>
                </UpdatedControls>
            </telerik:AjaxSetting>

           <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpIndicadoresCobranzas" ControlID="rapIndicadoresCobranzas"/>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdIndicadores">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdIndicadores" LoadingPanelID="ralpIndicadoresCobranzas"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpIndicadoresCobranzas" runat="server" Visible="True"></telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmVidaLey" runat="server" EnableShadow="true">
        
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="rapIndicadoresCobranzas" runat="server" ClientEvents-OnRequestStart="requestStart" Height="100%" >
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblTitulo" runat="server" Text="Indicador de Letras Protestadas" CssClass="titulo"></asp:Label>
            </div>
        </div>

        <div class="row">
                   <div class="row">
                         <div class="col-md-1">
                                <asp:Label ID="Label4" runat="server"  CssClass="etiqueta, bm" Text="Fecha Hasta: " Width="100%" ></asp:Label>
                         </div>
                         <div class="col-md-2">
                                 <telerik:RadDatePicker ID="dpFechaHastaCliente" runat="server" DateInput-ReadOnly="true" Width="80%" >
                                        <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                        </DateInput>
                                  </telerik:RadDatePicker>
                              
                        </div>
                         
                          <div class="col-md-1">
                               <telerik:RadButton ID="btnBuscar" runat="server" OnClick="btnBuscar_OnClick"  style="top: 1px; left: 3px" Text="Buscar" Width="100%">
                                <Icon PrimaryIconUrl="~/Images/Icons/search-16.png" />
                                
                            </telerik:RadButton>
                         </div>
             
                            <div class="col-md-1">
                                  <telerik:RadButton ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_OnClick" Width="100%" >
                                <Icon PrimaryIconUrl="~/Images/Icons/excel-16.png"/>
                            </telerik:RadButton>
                          </div>
                          <div class="col-md-5">
                         </div>

                   </div>
                   <div class="row">
                         <div class="col-md-12">
                                 <asp:Label ID="Label5" runat="server" CssClass="etiqueta, bm" Text="Resumen: " Width="100%"></asp:Label>
                         </div>
                   </div>
                   <div class="row"> 
                          <div class="col-md-12">
                              <telerik:RadPivotGrid ID="grdIndicadores" runat="server" Width="95%" Height="430px" AllowFiltering="false"  CssClass="Sangri"

                                        ShowFilterHeaderZone="false" 
                                        ShowDataHeaderZone="false" ShowRowHeaderZone="false" ShowColumnHeaderZone="false" EnableConfigurationPanel="true" 
                                        TotalsSettings-GrandTotalsVisibility="RowsOnly"  OnNeedDataSource="grdIndicadores_OnNeedDataSource"
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

                                           <telerik:PivotGridAggregateField DataField="importeProtestado" Aggregate="Sum" DataFormatString="${0:##,###0.##}" CellStyle-Width="30%">
                                                <HeaderCellTemplate>
                                                    importeProtestado
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                           <telerik:PivotGridAggregateField  DataField="ImportePagado" Aggregate="Sum" DataFormatString="${0:##,###0.##}" CellStyle-Width="30%" >
                                                <HeaderCellTemplate>
                                                    ImportePagado
                                                </HeaderCellTemplate>
                                           </telerik:PivotGridAggregateField>
                            
                                              <telerik:PivotGridAggregateField DataField="IndicadorProtes_Cal" CalculationDataFields="importeProtestado,ImportePagado" CalculationExpression="({0}/{1})*100" DataFormatString="{0:##,###0.##} %">
                                                <HeaderCellTemplate>
                                                    IndicadorProtes
                                                </HeaderCellTemplate>
                                                <RowTotalCellTemplate></RowTotalCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                           
                                        </Fields>
                               
                                    </telerik:RadPivotGrid>
                          </div>
                 </div>

        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="row">
        <div class="col-md-12">
             <asp:Label ID="lblMensajeResumenCliente" runat="server"></asp:Label>
             <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
        <div class="col-md-12">
            <asp:Label ID="lblDate" runat="server" Visible="false"></asp:Label>
             <asp:Label ID="lblDate2" runat="server" Visible="false"></asp:Label>
        </div>
    </div>
</asp:Content>
