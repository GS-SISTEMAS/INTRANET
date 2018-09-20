<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmFacturasInafectas.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.Financiamientos.LetrasEmitidas.frmFacturasInafectas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Letras Emitidas
</asp:Content>

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
        }
       function refreshGrid(arg) {
            if (!arg) {
                $find("<%= ramLetrasEmitidas.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%=  ramLetrasEmitidas.ClientID %>").ajaxRequest("RebindAndNavigate(" + arg + ")");
            }
       }
        function ShowInsertForm(id) {
            window.radopen("frmFacturasInafectas.aspx?idOperacion=" + id, "rwVidaLey");
            return false;
        }
        function ShowInsertFormImprimir(id) {
            window.radopen("frmExportarPDF.aspx?idOperacion=" + id, "rwVidaLey");
            return false;
        }

    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramLetrasEmitidas" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpLetrasEmitidas" ControlID="rapLetrasEmitidas"/>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdLetrasEmitidas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdLetrasEmitidas" LoadingPanelID="ralpLetrasEmitidas"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdLetrasEmitidas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapLetrasEmitidas" LoadingPanelID="ralpLetrasEmitidas"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>


        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpLetrasEmitidas" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmVidaLey" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwVidaLey" runat="server" Width="850px" Height="550px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="rapLetrasEmitidas" runat="server" ClientEvents-OnRequestStart="requestStart" Height="100%" >
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblTitulo" runat="server" Text="Reporte Facturas Inafectas" CssClass="titulo"></asp:Label>
            </div>
        </div>
        <div class="row">
            
            <div class="col-md-12">

                <table>
                    <tr>
                       

                        <td class="auto-style1">
                            <asp:Label ID="lblFinalEmision" runat="server" CssClass="etiqueta" Text="Desde:" Width="40px"></asp:Label>
                        </td>
                        <td class="auto-style1">   
                            <telerik:RadDatePicker ID="dpInicio" runat="server" DateInput-ReadOnly="true" Width="200px">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker></td>
                      <td class="auto-style1">
                            <asp:Label ID="lblFinalEmision0" runat="server" CssClass="etiqueta" Text="Hasta: " Width="40px"></asp:Label>
                        </td>
                        <td class="auto-style1">
                            <telerik:RadDatePicker ID="dpFinal" runat="server" DateInput-ReadOnly="true" Width="200px">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                        </td>
                        <td class="auto-style1"></td>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style1"></td>
                        <td class="auto-style1">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCliente" runat="server" CssClass="etiqueta" Text="Cliente:"></asp:Label>
                        </td>
                        <td colspan="4">
                            <telerik:RadAutoCompleteBox ID="acbCliente" runat="server" AllowCustomEntry="true" DropDownHeight="150px" 
                                DropDownWidth="300px" EmptyMessage="Selec. cliente" InputType="Text" 
                                OnClientEntryAdding="OnClientEntryAddingHandler" TextSettings-SelectionMode="Single" Width="300px">
                                <WebServiceSettings Method="Agenda_BuscarCliente" Path="frmFacturasInafectas.aspx" />
                            </telerik:RadAutoCompleteBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                  
                    <tr>
                        <td class="auto-style1">
                             <asp:Label ID="Label2" runat="server" CssClass="etiqueta" Text="Tipo Documento: " Width="116px"></asp:Label>
                        </td>

                        <td >
                            <telerik:RadComboBox ID="cboTipoDoc" Runat="server" Culture="es-ES" >
                                <Items>
                                    <telerik:RadComboBoxItem runat="server" Text="Factura (Venta)" Value="101" />
                                    <telerik:RadComboBoxItem runat="server" Text="Boleta (Venta)" Value="102" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                       <td class="auto-style1">
                            &nbsp;</td>
                        <td class="auto-style1">   
                            <telerik:RadButton ID="RadButton1" runat="server" OnClick="btnBuscar_Click" style="top: 1px; left: 3px" Text="Buscar" Width="100px">
                                <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                            </telerik:RadButton></td>

                        <td colspan="3"  class="auto-style1">
                            &nbsp;</td>

                        <td class="auto-style1">
                            &nbsp;</td>
                        <td class="auto-style1">
                            &nbsp;</td>
                        <td class="auto-style1">
                            &nbsp;</td>
                    </tr>
                </table>
                 <div style="height: 5px">
                 </div>

                <telerik:RadGrid ID="grdFacturasInafectas" runat="server" AllowMultiRowSelection="false"
                    AutoGenerateColumns="False" Height="280px"  Width="90%"
                    OnNeedDataSource="grdFacturasInafectas_NeedDataSource" AllowSorting="True"
                    OnItemDataBound="grdFacturasInafectas_ItemDataBound"  >

                    <MasterTableView TableLayout="Fixed" DataKeyNames="Cliente"
                        AllowMultiColumnSorting="true"    >
                        <Columns>

                            <telerik:GridHyperLinkColumn DataTextField="Cliente" HeaderText="Cliente" HeaderStyle-Width="20px" 
                                HeaderStyle-HorizontalAlign="Center" 
                                AllowSorting="false"    FooterStyle-HorizontalAlign="Left">
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridHyperLinkColumn>
                             <telerik:GridBoundColumn DataField="FechaDoc" HeaderText="Fecha" UniqueName="Fecha" AllowFiltering="false"
                                                DataFormatString="{0:dd/MM/yyyy}">
                                                <HeaderStyle Width="10px" />
                                            </telerik:GridBoundColumn>

                             <telerik:GridHyperLinkColumn DataTextField="TipoDoc" HeaderText="TipoDoc" HeaderStyle-Width="10px" 
                                HeaderStyle-HorizontalAlign="Center" 
                                AllowSorting="false"    FooterStyle-HorizontalAlign="Left">
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridHyperLinkColumn>

                             <telerik:GridHyperLinkColumn DataTextField="NumeroDocumento" HeaderText="NumeroDocumento" HeaderStyle-Width="10px" 
                                 HeaderStyle-HorizontalAlign="Center" 
                                AllowSorting="false"    FooterStyle-HorizontalAlign="Left">
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridHyperLinkColumn>

                            <telerik:GridHyperLinkColumn DataTextField="Importe" HeaderText="Importe" HeaderStyle-Width="10px" 
                                 DataTextFormatString="{0:$ #,##0.00}"
                                 HeaderStyle-HorizontalAlign="Center" 
                                AllowSorting="false"    FooterStyle-HorizontalAlign="Left">
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridHyperLinkColumn>


                            <telerik:GridHyperLinkColumn DataTextField="Cobrado" HeaderText="Cobrado" HeaderStyle-Width="10px" 
                                 DataTextFormatString="{0:$ #,##0.00}"
                                 HeaderStyle-HorizontalAlign="Center" 
                                AllowSorting="false"    FooterStyle-HorizontalAlign="Left">
                                <ItemStyle  HorizontalAlign="Right"/> 
                            </telerik:GridHyperLinkColumn>

                

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
    </telerik:RadAjaxPanel>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
        <div class="col-md-12">
            <asp:Label ID="lblDate" runat="server" Visible="false"></asp:Label>
        </div>
    </div>
</asp:Content>
