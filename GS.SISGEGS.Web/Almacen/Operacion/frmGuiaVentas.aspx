<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmGuiaVentas.aspx.cs" Inherits="GS.SISGEGS.Web.Almacen.Operacion.frmGuiaVentas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Guía de Ventas
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
                $find("<%= ramGuiaVentas.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%=  ramGuiaVentas.ClientID %>").ajaxRequest("RebindAndNavigate(" + arg + ")");
            }
       }


        function ShowInsertForm(id) {
            window.radopen("frmGuiaVentasMng.aspx?idOperacion=" + id, "rwVidaLey");
            return false;
        }

    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramGuiaVentas" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpGuiaVentas" ControlID="rapGuiaVentas"/>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdGuiaVentas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdGuiaVentas" LoadingPanelID="ralpGuiaVentas"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdGuiaVentas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapGuiaVentas" LoadingPanelID="ralpGuiaVentas"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>


        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpGuiaVentas" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmVidaLey" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwVidaLey" runat="server" Width="560px" Height="560px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="rapGuiaVentas" runat="server" ClientEvents-OnRequestStart="requestStart" Height="100%" >
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblTitulo" runat="server" Text="Consultar guías de ventas" CssClass="titulo"></asp:Label>
            </div>
        </div>
        <div class="row">
            
            <div class="col-md-12">

                <table>
                    <tr>
                        <td class="auto-style1">
                             <asp:Label ID="lblFechaEmision" runat="server" CssClass="etiqueta" Text="Fecha" Width="57px"></asp:Label>
                        </td>

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
                                <WebServiceSettings Method="Agenda_BuscarCliente" Path="frmGuiaVentas.aspx" />
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
                        <td>
                            <asp:Label ID="lblItem0" runat="server" CssClass="etiqueta" Text="Almacén"></asp:Label>
                        </td>
                        <td colspan="4">
                            <telerik:RadComboBox ID="cboAlmacen" runat="server" Width="301px">
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            <telerik:RadButton ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" style="top: 1px; left: 3px" Text="Buscar" Width="100px">
                                <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                            </telerik:RadButton>
                        </td>

                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                 <div style="height: 5px">
                 </div>

                <telerik:RadGrid ID="grdGuiasVentas" runat="server" AllowMultiRowSelection="false"
                    AutoGenerateColumns="False" Height="520px"  Width="100%"
                    AllowFilteringByColumn="true" AllowSorting="True"
                    OnNeedDataSource="grdGuiasVentas_NeedDataSource"  ShowFooter="true" 
                    OnItemDataBound="grdGuiasVentas_ItemDataBound" OnItemCommand="grdGuiasVentas_ItemCommand">
                     <GroupingSettings CaseSensitive="false"></GroupingSettings>

                    <MasterTableView DataKeyNames="Op" ClientDataKeyNames="Op" 
                        AutoGenerateColumns="false" AllowFilteringByColumn="true"
                                 ShowFooter="True" >
                        <Columns>
                          <telerik:GridTemplateColumn HeaderText="Edit." AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibEditar" runat="server" ImageUrl="~/Images/Icons/pencil-16.png" CommandArgument='<%# Eval("Op") %>' CommandName="Editar" />
                                </ItemTemplate>
                                <HeaderStyle Width="40px"/>
                            </telerik:GridTemplateColumn>

                           <telerik:GridMaskedColumn DataField="Op" HeaderText="#Op" FilterControlWidth="45px" AutoPostBackOnFilter="false" AllowSorting="true"
                                        CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false" Mask="#######" UniqueName="Op">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle Height="50px" />
                            </telerik:GridMaskedColumn>

                            <telerik:GridBoundColumn DataField="ID_Almacen" HeaderText="Id_Almacen" AllowSorting="false" Visible="false" AllowFiltering="false" >
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn DataField="Almacen" HeaderText="Almacén" HeaderStyle-Width="150px" AllowSorting="false" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="ID_AlmacenAnexo" Visible="false" HeaderText="Id_AlmacenAnexo" HeaderStyle-Width="30px" AllowSorting="false" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="AlmacenAnexo" HeaderText="AlmacenAnexo" HeaderStyle-Width="260px" AllowSorting="false" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha" HeaderStyle-Width="100px" AllowSorting="true" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                               <telerik:GridBoundColumn FilterControlWidth="150px" DataField="Transaccion" HeaderText="Transacción"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <HeaderStyle Width="200px" />
                               </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn DataField="ID_Agenda" HeaderText="Id_Agenda" HeaderStyle-Width="90px" AllowSorting="false"  AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Agenda" HeaderText="Agenda" HeaderStyle-Width="260px" AllowSorting="true" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="ID_Transportista" HeaderText="ID_Transportista" HeaderStyle-Width="100px" AllowSorting="false" AllowFiltering="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Transportista" HeaderText="Transportista" HeaderStyle-Width="150px" AllowSorting="false" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Id_vehiculo1" HeaderText="Vehiculo" HeaderStyle-Width="150px" AllowSorting="false" AllowFiltering="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TransportistaMarca" HeaderText="Vehiculo_Marca" HeaderStyle-Width="150px" AllowSorting="false" AllowFiltering="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TransportistaModelo" HeaderText="Vehiculo_Modelo" HeaderStyle-Width="150px" AllowSorting="false" AllowFiltering="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TransportistaPlaca" HeaderText="Vehiculo_Placa" HeaderStyle-Width="100px" AllowSorting="false" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Id_Chofer" HeaderText="Id_Chofer" HeaderStyle-Width="150px" AllowSorting="false" AllowFiltering="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TransportistaChofer" HeaderText="Chofer" HeaderStyle-Width="300px" AllowSorting="false" AllowFiltering="false">
                            </telerik:GridBoundColumn>




                        </Columns>
                    </MasterTableView>

                     <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="3"></Scrolling>
                                <Selecting AllowRowSelect="True"></Selecting>
                            </ClientSettings>
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
