<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmReporteDetracciones.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.Reportes.frmReporteDetracciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Reporte Detraccciones
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

            if (args.get_eventTarget().indexOf("btnExcel") >= 0)
                args.set_enableAjax(false);
        }

        function ShowEstadoCuenta(id, idagenda, serie, numero) {
            window.radopen("DetraccionAccionMng.aspx?op=" + id + '&idAgenda=' + idagenda + "&Serie=" + serie + "&Numero=" + numero , "rwDocumento");
            return false;
        }

        function ShowHistorialVoucher(op) {
            window.radopen("VouchersDetraccioMng.aspx?op=" + op, "rwVoucher");
            return false;
        }

        function refreshGrid(arg) {
            //console.log('ingresa a la funcion');
            <%--
            if (!arg) {
                $find("<%= ramDetracciones.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%= ramDetracciones.ClientID %>").ajaxRequest("Registro," + arg);
            }--%>

            $find("<%= btnBuscar.ClientID %>").click();
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramDetracciones" runat="server">
        <AjaxSettings>

            <telerik:AjaxSetting AjaxControlID="body">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpDetracciones" ControlID="rapDetracciones"/>
                     <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

           <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpDetracciones" ControlID="rapDetracciones"/>
                     <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdDetracciones">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDetracciones" LoadingPanelID="ralpDetracciones"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpDetracciones" runat="server" Visible="True"></telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmVidaLey" runat="server" EnableShadow="true">
       <Windows> 
        <telerik:RadWindow ID="rwDocumento" runat="server" Width="500px" Height="450px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
        </telerik:RadWindow>

        <telerik:RadWindow ID="rwVoucher" runat="server" Width="600px" Height="350px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
        </telerik:RadWindow>

       </Windows> 
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="rapDetracciones" runat="server" ClientEvents-OnRequestStart="requestStart" Height="100%" >
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblTitulo" runat="server" Text="Reporte de Detracciones" CssClass="titulo"></asp:Label>
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
                            <telerik:RadButton ID="btnBuscar" runat="server" OnClick="btnBuscar_OnClick"  style="top: 1px; left: 3px" Text="Buscar" Width="100px">
                                <Icon PrimaryIconUrl="~/Images/Icons/search-16.png" />
                                
                            </telerik:RadButton>
                        </td>
                            
                               
                        <td>&nbsp;</td>
                        <td>&nbsp; &nbsp;</td>
                        <td>
                            <%--<telerik:RadButton ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click">
                                <Icon PrimaryIconUrl="~/Images/Icons/excel-16.png"/>
                            </telerik:RadButton>--%>

                        </td>
                        
                        <td>
                            &nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCliente" runat="server" CssClass="etiqueta" Text="Cliente:"></asp:Label>
                        </td>
                        <td colspan="4">
                            <telerik:RadAutoCompleteBox ID="acbCliente" runat="server" AllowCustomEntry="true" DropDownHeight="150px" 
                                DropDownWidth="300px" EmptyMessage="Selec. cliente" InputType="Text" 
                                OnClientEntryAdding="OnClientEntryAddingHandler" TextSettings-SelectionMode="Single" Width="300px">
                                <WebServiceSettings Method="Agenda_BuscarCliente" Path="frmReporteDetracciones.aspx" />
                            </telerik:RadAutoCompleteBox>
                        </td>
                        <td colspan="2" >
<%--                            <telerik:RadButton ID="btnBuscarResumenCliente" runat="server" OnClick="btnBuscarResumenCliente_Click" style="top: 1px; left: 3px" Text="Buscar" Width="100px">
                                <Icon PrimaryIconUrl="~/Images/Icons/search-16.png" />
                                
                            </telerik:RadButton>--%>
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
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" CssClass="etiqueta" Text="Estatus:"></asp:Label>
                        </td>
                        <td colspan="4">
                            <asp:DropDownList runat="server" ID="ddlEstatus">

                                <asp:ListItem Selected="True" Value="0">Pendientes</asp:ListItem>
                                <asp:ListItem Value="1">Cancelado</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td colspan="2" >
<%--                            <telerik:RadButton ID="btnBuscarResumenCliente" runat="server" OnClick="btnBuscarResumenCliente_Click" style="top: 1px; left: 3px" Text="Buscar" Width="100px">
                                <Icon PrimaryIconUrl="~/Images/Icons/search-16.png" />
                                
                            </telerik:RadButton>--%>
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

                        <telerik:RadGrid ID="grdDetracciones" runat="server" AllowMultiRowSelection="false" AutoGenerateColumns="False" 
                                Height="440px"  Width="95%" AllowSorting="True" ShowFooter="true" 
                            OnItemCommand="grdDetracciones_OnItemCommand" 
                            OnItemDataBound="grdDetracciones_ItemDataBound"
                            >

                             <MasterTableView TableLayout="Fixed" DataKeyNames="Op">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ID_Cliente" HeaderText="RUC"  HeaderStyle-Width="100px" AllowSorting="false" >
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="Cliente" HeaderText="Cliente" HeaderStyle-Width="250px" AllowSorting="false" >
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="NroFactura" HeaderText="Factura"  HeaderStyle-Width="80px" AllowSorting="false"   >
                                        </telerik:GridBoundColumn>
                                        
                                        <telerik:GridBoundColumn DataField="Fecha" HeaderText="FechaEmision"  HeaderStyle-Width="80px" AllowSorting="false" DataFormatString="{0:dd/MM/yyyy}"   >
                                        </telerik:GridBoundColumn>
                                       
                                         <telerik:GridBoundColumn DataField="Total" HeaderText="Monto Total" HeaderStyle-Width="100px" DataFormatString="$ {0:#,##0.00}" HeaderStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right"/> 
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="saldo" HeaderText="Saldo Deudor" HeaderStyle-Width="100px" DataFormatString="$ {0:#,##0.00}" 
                                            HeaderStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right"/> 
                                        </telerik:GridBoundColumn>
                                        
                                        <telerik:GridBoundColumn DataField="NombreEstatus" HeaderText="Estatus"  HeaderStyle-Width="80px" AllowSorting="false"   >
                                        </telerik:GridBoundColumn>
                                        
                                        <telerik:GridBoundColumn DataField="Detraccion" HeaderText="Detraccion" HeaderStyle-Width="100px" DataFormatString="{0:#,##0.00}" HeaderStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right"/> 
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Accion">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibEditar" runat="server" ImageUrl="~/Images/Icons/search-16.png" CommandArgument='<%# Eval("Op") %>'  CommandName="Accion"/>
                                            </ItemTemplate>
                                            <HeaderStyle Width="40px"/>
                                        </telerik:GridTemplateColumn>

                                    </Columns>
                                </MasterTableView>
                             <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="2" ></Scrolling>
                                <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                            </ClientSettings>
                        </telerik:RadGrid>
                      

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
