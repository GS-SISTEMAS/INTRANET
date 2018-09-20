<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmIndicadorDeudaVcdCreditoAct.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.Indicadores.frmIndicadorDeudaVcdCreditoAct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        fieldset {
                  margin: 2px;
                    border: 1px solid silver;
                    padding: 8px;    
                  font:80%/1 sans-serif;
                  }
        legend {
                    padding: 2px;    
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

           <telerik:AjaxSetting AjaxControlID="btnBuscarResumenCliente">
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
                <asp:Label ID="lblTitulo" runat="server" Text="Indicador de Creditos y Cobranzas" CssClass="titulo"></asp:Label>
            </div>
        </div>

        <div class="row">

                      <div class="row">
                         <div class="col-md-12">
                             <table>
                           <tr>
                        <td>
                             <asp:Label ID="Label2" runat="server" CssClass="etiqueta" Text="Fecha Inicio: " Width="93px"></asp:Label>
                        </td>

                        <td>
                            <telerik:RadDatePicker ID="dpFechaDesdeCliente" runat="server" DateInput-ReadOnly="true" Width="150px">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                        </td>
                        <td>   
                            <asp:Label ID="Label4" runat="server" CssClass="etiqueta" Text="Fecha Fin: " Width="93px"></asp:Label>
                        </td>
                      <td>
                           <telerik:RadDatePicker ID="dpFechaHastaCliente" runat="server" DateInput-ReadOnly="true" Width="150px">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>

                      </td>
                               <td>
                            &nbsp;</td>
                        <td colspan="2" >
                            <telerik:RadButton ID="btnBuscarResumenCliente" runat="server" OnClick="btnBuscarResumenCliente_Click" style="top: 1px; left: 3px" Text="Buscar" Width="100px">
                                <Icon PrimaryIconUrl="~/Images/Icons/search-16.png" />
                                
                            </telerik:RadButton>
                        </td>
                            
                               
                        <td>&nbsp;</td>
                        <td>&nbsp; &nbsp;</td>
                        <td>
                            <telerik:RadButton ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click">
                                <Icon PrimaryIconUrl="~/Images/Icons/excel-16.png"/>
                            </telerik:RadButton>

                        </td>
                        
                        <td>
                            &nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
          
                        <td colspan="2">
                            <asp:CheckBox runat="server" ID="chkClientes" Text=". Ver Todos los Clientes" CssClass="etiqueta"/>

                        </td>

                        <td colspan="2" >
                            <asp:CheckBox CssClass="etiqueta" ID="chkCartera" runat="server" Text=". Incluir Zonas Afiliadas" />
                        </td>
                        <td>
                            &nbsp;</td>
                        <td >
                              </td>
                        <td>&nbsp;</td>
                        <td>
                            <%--<telerik:RadButton ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click">
                                <Icon PrimaryIconUrl="~/Images/Icons/excel-16.png"/>
                            </telerik:RadButton>--%>
                       </td>
                    </tr>
                </table>
                             <div style="height: 5px">
                             </div>
                             <table>
                            <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" CssClass="etiqueta" Text="Resumen: " Width="116px"></asp:Label>
                          

                        </td>
                        <td>
                            &nbsp;</td>
                         <td> 
                            
                             &nbsp;&nbsp;</td>
                        <td>
                          
                        </td>
                    </tr>
                            </table>

                                    <telerik:RadPivotGrid ID="grdIndicadores" runat="server" Width="100%" Height="100%" AllowFiltering="false" 

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

                                           <telerik:PivotGridAggregateField DataField="numeroVenc30Mas" Aggregate="Sum" DataFormatString="{0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    numeroVenc30Mas   
                                                </HeaderCellTemplate>
                                            </telerik:PivotGridAggregateField>
                                           <telerik:PivotGridAggregateField  DataField="CreditoDisponible" Aggregate="Max" DataFormatString="{0:##,###0.##}">
                                                <HeaderCellTemplate>
                                                    Credito Activo
                                                </HeaderCellTemplate>
                                           </telerik:PivotGridAggregateField>
                                            
                                            <telerik:PivotGridAggregateField  DataField="indDeudaCliente" Aggregate="Sum"  DataFormatString="{0:##,###0.##} %">
                                                <HeaderCellTemplate>
                                                    Ind.ClinenteVenc.Credito
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
