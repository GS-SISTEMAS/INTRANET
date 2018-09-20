<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpL.Master" AutoEventWireup="true" CodeBehind="frmPedidoMng.aspx.cs" Inherits="GS.SISGEGS.Web.Commercial.Pedido.frmPedidoMng" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function OnClientEntryAddingHandler(sender, eventArgs) {
            if (sender.get_entries().get_count() > 0) {
                eventArgs.set_cancel(true);
                alert("Solo se puede selecionar un elemento.");
            }
        }

        function TextChanged(sender, e) {
            dateVar = new Date();

            var dpEmision = $find("<%= dpFechaEmision.ClientID %>");
            dpEmision.set_selectedDate(dateVar);

            if (sender.value != "")
                dateVar.setDate(dateVar.getDate() + parseInt(sender.value));

            var dpVencimiento = $find("<%= dpFechaVencimiento.ClientID %>");
            dpVencimiento.set_selectedDate(dateVar);
        }

        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= ramPedidoMng.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%= ramPedidoMng.ClientID %>").ajaxRequest("RebindAndNavigate(" + arg + ")");
            }
        }

        function CloseAndRebind(args) {
            GetRadWindow().BrowserWindow.refreshGrid(args);
            GetRadWindow().close();
        }

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)

            return oWindow;
        }

        function CancelEdit() {
            GetRadWindow().close();
        }

        function RowDblClick(sender, eventArgs) {
            var combo = $find('<%=cboAlmacen.ClientID %>');
            window.radopen("frmPedidoItem.aspx?idItem=" + eventArgs.getDataKeyValue("Item_ID") + "&idCliente=" + document.getElementById('<%= lblCodigoCliente.ClientID%>').value +
                "&nuevo=" + 0 + "&idAlmacen=" + combo.get_selectedItem().get_value(), "rwPedidoMng");
        }

        function ShowInsertForm(idItem, idCliente, nuevo, idAlmacen) {
            window.radopen("frmPedidoItem.aspx?idItem=" + idItem + "&idCliente=" + idCliente + "&nuevo=" + nuevo + "&idAlmacen=" + idAlmacen, "rwPedidoMng");
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramPedidoMng" runat="server" OnAjaxRequest="ramPedidoMng_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlPedidoMng" LoadingPanelID="ralpPedidoMng" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdGlosa" LoadingPanelID="ralpPedidoMng"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ramPedidoMng">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" LoadingPanelID="ralpPedidoMng"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="grdGlosa"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cbFormaPago">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlPedidoMng" LoadingPanelID="ralpPedidoMng" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBuscarCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlPedidoMng" LoadingPanelID="ralpPedidoMng" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlPedidoMng" LoadingPanelID="ralpPedidoMng" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="acbCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlPedidoMng" LoadingPanelID="ralpPedidoMng" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpPedidoMng" runat="server" ZIndex="9999" IsSticky="true">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmPedidoMng" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwPedidoMng" runat="server" Width="400px" Height="400px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="pnlPedidoMng" runat="server" Width="100%">
        <telerik:RadTabStrip runat="server" ID="stripPedido" MultiPageID="pagesPedido" SelectedIndex="0" CssClass="fila">
            <Tabs>
                <telerik:RadTab Text="Datos Principales" Selected="True"></telerik:RadTab>
                <telerik:RadTab Text="Productos"></telerik:RadTab>
                <telerik:RadTab Text="Datos Adicionales"></telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>

        <telerik:RadMultiPage runat="server" ID="pagesPedido" SelectedIndex="0" Height="415px" ScrollBars="Vertical" CssClass="multicontainer">
            <telerik:RadPageView runat="server" ID="pagePrincipal" CssClass="tabcontainer">
                <div class="fila containerSubTitulo">
                    <div class="colum6">
                        <asp:Label ID="lblSubTitulo" runat="server" Text="Datos del Cliente" CssClass="subTitulo"></asp:Label>
                    </div>
                    <div class="colum4">
                        <div class="colum9">
                            <telerik:RadAutoCompleteBox ID="acbCliente" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text"
                                DropDownHeight="150px" EmptyMessage="Buscar cliente" AllowCustomEntry="true">
                                <WebServiceSettings Method="Agenda_BuscarCliente" Path="frmPedidoMng.aspx" />
                            </telerik:RadAutoCompleteBox>
                        </div>
                        <div class="colum1">
                            <telerik:RadButton ID="btnBuscarCliente" runat="server" OnClick="btnBuscarCliente_Click" Text="Image Button" Width="16px" Height="16px">
                                <Image ImageUrl="../../Images/Icons/search-16.png" />
                            </telerik:RadButton>
                        </div>
                    </div>
                </div>

                <div class="fila">
                    <div class="colum2">
                        <div class="colum2">
                            <asp:Label ID="lblRUC" runat="server" Text="RUC" CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="colum7">
                            <telerik:RadTextBox ID="txtRUC" runat="server" ReadOnly="true" Width="100%" EmptyMessage="Número de RUC"></telerik:RadTextBox>
                        </div>
                        <div class="colum1">
                            <asp:HiddenField ID="lblCodigoCliente" runat="server" />
                        </div>
                    </div>
                    <div class="colum5">
                        <div class="colum2">
                            <asp:Label ID="lblNombre" runat="server" Text="Nombre" CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="colum8">
                            <telerik:RadTextBox ID="txtCliente" runat="server" ReadOnly="true" Width="100%" EmptyMessage="Nombre del cliente"></telerik:RadTextBox>
                        </div>
                    </div>
                    <div class="colum3">
                        <div class="colum3">
                            <asp:Label ID="lblSucursal" runat="server" Text="Sucursal" CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="colum7">
                            <telerik:RadComboBox ID="cboSucursal" runat="server" Width="100%"></telerik:RadComboBox>
                        </div>
                    </div>
                </div>
                <div class="fila">
                    <div class="colum10">
                        <div class="colum1">
                            <asp:Label ID="lblFacturacion" runat="server" Text="Factura" CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="colum9">
                            <telerik:RadComboBox ID="cboFacturacion" runat="server" Width="100%"></telerik:RadComboBox>
                        </div>
                    </div>
                </div>
                <div class="fila">
                    <div class="colum10">
                        <div class="colum1">
                            <asp:Label ID="lblDespacho" runat="server" Text="Despacho" CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="colum9">
                            <telerik:RadComboBox ID="cboDespacho" runat="server" Width="100%"></telerik:RadComboBox>
                        </div>
                    </div>
                </div>
                <div class="fila containerSubTitulo">
                    <div class="colum7">
                        <asp:Label ID="lblDatosPedido" runat="server" Text="Datos del Pedido" CssClass="subTitulo"></asp:Label>
                    </div>
                </div>
                <div class="fila">
                    <div class="colum3">
                        <div class="colum4">
                            <asp:Label ID="lblNroRegistro" runat="server" Text="Nro.Registro" CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="colum6">
                            <telerik:RadTextBox ID="txtNroRegistro" runat="server" Width="100%" Enabled="false"></telerik:RadTextBox>
                        </div>
                    </div>
                    <div class="colum2">
                        <div class="colum3">
                            <asp:Label ID="lblTEA" runat="server" Text="TEA" CssClass="etiqueta" Visible="false"></asp:Label>
                        </div>
                        <div class="colum3">
                            <telerik:RadTextBox ID="txtTEA" runat="server" Width="100%" Enabled="false" Visible="false"></telerik:RadTextBox>
                        </div>
                    </div>
                    <div class="colum3">
                        <div class="colum4">
                            <asp:Label ID="lblTipoEnvio" runat="server" Text="Tipo Envio" CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="colum6">
                            <telerik:RadComboBox ID="cboTipoEnvio" runat="server" Width="100%"></telerik:RadComboBox>
                        </div>
                    </div>
                    <div class="colum2">
                        <div class="colum2">
                            <br />
                        </div>
                        <div class="colum5">
                            <asp:Label ID="lblPrioridad" runat="server" Text="Prioridad" CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="colum3">
                            <telerik:RadComboBox ID="cboPrioridad" runat="server" Width="100%">
                                <Items>
                                    <telerik:RadComboBoxItem Value="1" Text="1" Selected="true" />
                                    <telerik:RadComboBoxItem Value="2" Text="2" />
                                    <telerik:RadComboBoxItem Value="3" Text="3" />
                                    <telerik:RadComboBoxItem Value="4" Text="4" />
                                    <telerik:RadComboBoxItem Value="5" Text="5" />
                                </Items>
                            </telerik:RadComboBox>
                        </div>
                    </div>
                </div>
                <div class="fila">
                    <div class="colum3">
                        <div class="colum4">
                            <asp:Label ID="lblMoneda" runat="server" Text="Moneda" CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="colum6">
                            <telerik:RadComboBox ID="cboMoneda" runat="server" Width="100%"></telerik:RadComboBox>
                        </div>
                    </div>
                    <div class="colum3">
                        <div class="colum4">
                            <asp:Label ID="lblTipoVenta" runat="server" Text="Tipo Venta" CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="colum6">
                            <telerik:RadComboBox ID="cboFormaPago" runat="server" Width="100%"></telerik:RadComboBox>
                        </div>
                    </div>
                    <div class="colum4">
                        <div class="colum2">
                            <asp:Label ID="lblVendedor" runat="server" Text="Vendedor" CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="colum8">
                            <telerik:RadAutoCompleteBox ID="acbVendedor" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text"
                                DropDownHeight="150px" EmptyMessage="Selec. vendedor" AllowCustomEntry="true" OnClientEntryAdding="OnClientEntryAddingHandler">
                                <WebServiceSettings Method="Agenda_BuscarVendedor" Path="frmPedidoMng.aspx" />
                            </telerik:RadAutoCompleteBox>
                        </div>
                    </div>
                </div>
                <div class="fila">
                    <div class="colum3">
                        <div class="colum5">
                            <asp:Label ID="lblOrden" runat="server" Text="Orden del Cliente" CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="colum5">
                            <telerik:RadTextBox ID="txtOrden" runat="server" Width="100%" MaxLength="20"></telerik:RadTextBox>
                        </div>
                    </div>
                </div>
                <div class="fila containerSubTitulo">
                    <div class="colum7">
                        <asp:Label ID="lblFormaPago" runat="server" Text="Datos de la Forma de pago:" CssClass="subTitulo"></asp:Label>
                    </div>
                </div>
                <div class="fila">
                    <div class="colum3">
                        <div class="colum4">
                            <asp:Label ID="lblFechaEmision" runat="server" Text="Fec.Emisión" CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="colum6">
                            <telerik:RadDatePicker ID="dpFechaEmision" runat="server" Width="100%" DateInput-ReadOnly="true" DatePopupButton-Visible="false">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                        </div>
                    </div>
                    <div class="colum3">
                        <div class="colum4">
                            <asp:Label ID="lblFechaVencimiento" runat="server" Text="Fec.Vencim." CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="colum6">
                            <telerik:RadDatePicker ID="dpFechaVencimiento" runat="server" Width="100%" DateInput-ReadOnly="true" DatePopupButton-Visible="false">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                        </div>
                    </div>
                    <div class="colum2">
                        <div class="colum6">
                            <asp:Label ID="lblDiasCredito" runat="server" Text="Días de Cred." CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="colum3">
                            <telerik:RadNumericTextBox ID="txtDiasCredito" runat="server" Width="100%" Type="Number" ReadOnly="true"
                                NumberFormat-DecimalDigits="0" MaxLength="3" MinValue="0" onKeyUp="TextChanged(this,event);">
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <div class="colum2">
                        <telerik:RadComboBox ID="cboTipoCredito" runat="server" Width="100%" EmptyMessage="Tipo de Creédito"
                            AutoPostBack="true" OnSelectedIndexChanged="cboTipoCredito_SelectedIndexChanged">
                        </telerik:RadComboBox>
                    </div>
                </div>
                <div class="fila">
                    <div class="colum10">
                        <asp:Label ID="lblObservacion" runat="server" Text="Observaciones" CssClass="etiqueta"></asp:Label>
                    </div>
                </div>
                <div class="fila">
                    <div class="colum10">
                        <telerik:RadTextBox ID="txtObservacion" runat="server" Width="100%" TextMode="MultiLine" Height="50px" MaxLength="1000"></telerik:RadTextBox>
                    </div>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server" ID="pageItems" CssClass="tabcontainer">
                <div class="fila containerSubTitulo">
                    <div class="colum6">
                        <asp:Label ID="lblDetPedido" runat="server" Text="Detalle del pedido" CssClass="subTitulo"></asp:Label>
                    </div>
                </div>
                <div class="fila">
                    <div class="colum1">
                        <asp:Label ID="lblAlmacen" runat="server" Text="Almacen" CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="colum6">
                        <telerik:RadComboBox ID="cboAlmacen" runat="server" Width="100%"></telerik:RadComboBox>
                    </div>
                </div>
                <div class="fila">
                    <div class="colum1">
                        <asp:Label ID="lblProducto" runat="server" Text="Producto" CssClass="etiqueta"></asp:Label>
                    </div>
                    <div class="colum7">
                        <telerik:RadAutoCompleteBox ID="acbProducto" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text" DropDownWidth="82%"
                            DropDownHeight="150px" EmptyMessage="Buscar producto" AllowCustomEntry="true" OnClientEntryAdding="OnClientEntryAddingHandler">
                            <WebServiceSettings Method="Item_BuscarProducto" Path="frmPedidoMng.aspx" />
                        </telerik:RadAutoCompleteBox>
                    </div>
                    <div class="colum2">
                        <telerik:RadButton ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click">
                            <Icon PrimaryIconUrl="../../Images/Icons/sign-add-16.png" />
                        </telerik:RadButton>
                    </div>
                </div>
                <div class="fila">
                    <div class="colum10">
                        <telerik:RadGrid ID="grdItem" runat="server" Width="100%" Height="180px" AutoGenerateColumns="false"
                            OnItemCommand="grdItem_ItemCommand" OnItemDataBound="grdItem_ItemDataBound">
                            <MasterTableView DataKeyNames="Item_ID" ClientDataKeyNames="Item_ID">
                                <Columns>
                                    <telerik:GridTemplateColumn>
                                        <ItemTemplate>
                                            <asp:Label ID="idAmarre" runat="server" Text='<%# Eval("ID_Amarre") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn HeaderText="Código" DataField="Codigo" UniqueName="Codigo">
                                        <HeaderStyle Width="100px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Descripción" DataField="Item" UniqueName="Item">
                                        <HeaderStyle Width="300px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Kardex" DataField="Item_ID" UniqueName="Item_ID">
                                        <HeaderStyle Width="50px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Precio" DataField="Precio" UniqueName="Precio" DataFormatString="{0:F4}">
                                        <HeaderStyle Width="70px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="ID_Moneda" DataField="ID_Moneda" Visible="false" UniqueName="ID_Moneda">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Factor" DataField="FactorUnidadInv" UniqueName="FactorUnidadInv" DataFormatString="{0:F2}">
                                        <HeaderStyle Width="50px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="U.M.F" DataField="ID_UnidadControl" UniqueName="ID_UnidadControl">
                                        <HeaderStyle Width="50px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Stock" DataField="Stock" UniqueName="Stock" DataFormatString="{0:F0}">
                                        <HeaderStyle Width="70px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Dcto(%)" DataField="Descuento" UniqueName="Descuento">
                                        <HeaderStyle Width="70px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Cantidad" DataField="Cantidad" UniqueName="Cantidad" DataFormatString="{0:F0}">
                                        <HeaderStyle Width="70px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="U.M.C" DataField="ID_UnidadInv" UniqueName="ID_UnidadInv">
                                        <HeaderStyle Width="50px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Importe" DataField="Importe" UniqueName="Importe" DataFormatString="{0:F4}">
                                        <HeaderStyle Width="70px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Observación" DataField="Observacion" UniqueName="Observacion">
                                        <HeaderStyle Width="100px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Elim.">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("Item_ID") %>'
                                                ImageUrl="~/Images/Icons/trashcan-16.png" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="40px" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                                <ClientEvents OnRowDblClick="RowDblClick" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </div>
                </div>
                <div class="fila">
                    <div class="colum5">
                        <telerik:RadGrid ID="grdGlosa" runat="server" AutoGenerateColumns="false" Width="100%">
                            <MasterTableView>
                                <Columns>
                                    <telerik:GridBoundColumn HeaderText="Descripcion" DataField="Descripcion">
                                        <HeaderStyle Width="30px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Base Imp." DataField="BaseImponible" DataFormatString="{0:F4}">
                                        <HeaderStyle Width="30px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Importe" DataField="Importe" DataFormatString="{0:F4}">
                                        <HeaderStyle Width="30px" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server" ID="pageReferencia" CssClass="tabcontainer">
                <div class="fila containerSubTitulo">
                    <div class="colum7">
                        <asp:Label ID="lblSTReferencia" runat="server" Text="Datos de Referencia" CssClass="subTitulo"></asp:Label>
                    </div>
                </div>
                <div class="fila">
                    <div class="colum3">
                        <div class="colum2">
                            <asp:Label ID="lblSede" runat="server" Text="Sede" CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="colum8">
                            <telerik:RadComboBox ID="cboSede" runat="server" Width="100%" EmptyMessage="Seleccionar Sede">
                            </telerik:RadComboBox>
                        </div>
                    </div>
                    <div class="colum3">
                        <div class="colum3">
                            <asp:Label ID="lblContacto" runat="server" Text="Contacto" CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="colum7">
                            <telerik:RadAutoCompleteBox ID="acbContacto" runat="server" Width="95%" TextSettings-SelectionMode="Single" InputType="Text" AllowCustomEntry="true"
                                DropDownHeight="200px" EmptyMessage="Ingresar Código" OnClientEntryAdding="OnClientEntryAddingHandler">
                                <WebServiceSettings Method="Agenda_BuscarContacto" Path="frmPedidoMng.aspx" />
                            </telerik:RadAutoCompleteBox>
                        </div>
                    </div>
                    <div class="colum3">
                        <div class="colum3">
                            <asp:Label ID="lblReferencia" runat="server" Text="Referencia" CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="colum7">
                            <telerik:RadComboBox ID="cboReferencia" runat="server" Width="100%">
                            </telerik:RadComboBox>
                        </div>
                    </div>
                </div>
                <div class="fila containerSubTitulo">
                    <div class="colum7">
                        <asp:Label ID="lblOperacion" runat="server" Text="Operación" CssClass="subTitulo"></asp:Label>
                    </div>
                </div>
                <div class="fila">
                    <div class="colum3">
                        <div class="colum4">
                            <asp:Label ID="lblOpDespacho" runat="server" Text="Tipo Despacho" CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="colum6">
                            <telerik:RadComboBox ID="cboOpDespacho" runat="server" Width="100%"></telerik:RadComboBox>
                        </div>
                    </div>
                    <div class="colum3">
                        <div class="colum4">
                            <asp:Label ID="lblOpTipoPedido" runat="server" Text="Tipo Pedido" CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="colum6">
                            <telerik:RadComboBox ID="cboOpTipoPedido" runat="server" Width="100%"></telerik:RadComboBox>
                        </div>
                    </div>
                    <div class="colum4">
                        <div class="colum3">
                            <asp:Label ID="lblOpDocVenta" runat="server" Text="Doc. Venta" CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="colum7">
                            <telerik:RadComboBox ID="cboOpDocVenta" runat="server" Width="100%"></telerik:RadComboBox>
                        </div>
                    </div>
                </div>
                <div class="fila containerSubTitulo">
                    <div class="colum7">
                        <asp:Label ID="lblSTTransporte" runat="server" Text="Transporte" CssClass="subTitulo"></asp:Label>
                    </div>
                </div>
                <div class="fila">
                    <div class="colum4">
                        <div class="colum3">
                            <asp:Label ID="lblTransporte" runat="server" Text="Transportista" CssClass="etiqueta"></asp:Label>
                        </div>
                        <div class="colum7">
                            <telerik:RadAutoCompleteBox ID="acbTransporte" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text" AllowCustomEntry="true"
                                DropDownHeight="200px" EmptyMessage="Buscar transportista" OnClientEntryAdding="OnClientEntryAddingHandler" DropDownWidth="250px">
                                <WebServiceSettings Method="Agenda_TransporteBuscar" Path="frmPedidoMng.aspx" />
                            </telerik:RadAutoCompleteBox>
                        </div>
                    </div>
                    <div class="colum1">
                        <asp:Label ID="lblTrans" runat="server" Visible="false"></asp:Label>
                    </div>
                </div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>

        <div class="fila">
            <div class="colum1">
                <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click">
                    <Icon PrimaryIconUrl="../../Images/Icons/floppy-16.png" />
                </telerik:RadButton>
            </div>
            <div class="colum6">
                <br />
            </div>
            <div class="colum3">
                <asp:Label ID="lblLineaCredito" runat="server" CssClass="etiqueta"></asp:Label>
            </div>
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mensaje" runat="server">
    <div class="fila">
        <div class="colum10">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>

