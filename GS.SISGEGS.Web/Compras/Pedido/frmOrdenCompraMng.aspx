<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmOrdenCompraMng.aspx.cs" Inherits="GS.SISGEGS.Web.Compras.Pedido.frmOrdenCompraMng" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Registrar orden de compra
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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

        //function CloseAndRebind(args) {
        //    GetRadWindow().BrowserWindow.refreshGrid(args);
        //    GetRadWindow().close();
        //}

        //function GetRadWindow() {
        //    var oWindow = null;
        //    if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
        //    else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)

        //    return oWindow;
        //}

        //function CancelEdit() {
        //    GetRadWindow().close();
        //}

        function RowDblClick(sender, eventArgs) {
            var combo = $find('<%=cboAlmacen.ClientID %>');

            window.radopen("frmOrdenCompraItem.aspx?idItem=" + eventArgs.getDataKeyValue("Item_ID") + "&idProveedor=" + document.getElementById('<%= lblCodigoProveedor.ClientID%>').value +
                "&nuevo=" + 0 + "&idAlmacen=" + combo.get_selectedItem().get_value(), "rwPedidoMng");
        }

        function ShowInsertForm(idItem, idProveedor, nuevo, idAlmacen, idMoneda) {
            window.radopen("frmOrdenCompraItem.aspx?idItem=" + idItem + "&idProveedor=" + idProveedor + "&nuevo=" + nuevo + "&idAlmacen=" + idAlmacen + "&idMoneda=" + idMoneda, "rwPedidoMng");
            return false;
        }

        $(document).ready(function () {
            $("#btnGuardar").one('click', function (event) {
                event.preventDefault();
                //do something
                $(this).prop('disabled', true);
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
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
            <telerik:AjaxSetting AjaxControlID="btnBuscarProveedor">
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
            <telerik:AjaxSetting AjaxControlID="acbProveedor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlPedidoMng" LoadingPanelID="ralpPedidoMng" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboFormaPago">
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
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="pnlPedidoMng" runat="server" Width="100%" Height="100%">
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Width="100%" Height="100%">
            <Rows>
                <telerik:LayoutRow>
                    <Columns>
                        <telerik:LayoutColumn Span="11">
                            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Registrar Orden de compra"></asp:Label>
                        </telerik:LayoutColumn>
                        <telerik:LayoutColumn Span="1">
                            <telerik:RadButton ID="btnRegresar" runat="server" Text="Regresar" OnClick="btnRegresar_Click">
                                <Icon PrimaryIconUrl="../../Images/Icons/arrowLeft-16.png"/>
                            </telerik:RadButton>
                        </telerik:LayoutColumn>
                    </Columns>
                </telerik:LayoutRow>
                <telerik:LayoutRow>
                    <Columns>
                        <telerik:LayoutColumn Span="4">
                            <telerik:RadAutoCompleteBox ID="acbProveedor" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text"
                                DropDownHeight="150px" EmptyMessage="Buscar Proveedor" AllowCustomEntry="true" Label="Proveedor">
                                <WebServiceSettings Method="Agenda_BuscarProveedor" Path="frmOrdenCompraMng.aspx" />
                            </telerik:RadAutoCompleteBox>
                        </telerik:LayoutColumn>
                        <telerik:LayoutColumn Span="1">
                            <telerik:RadButton ID="btnBuscarProveedor" runat="server" OnClick="btnBuscarProveedor_Click" Text="Selec.">
                                <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                            </telerik:RadButton>
                        </telerik:LayoutColumn>
                        <telerik:LayoutColumn Span="4">
                            <telerik:RadComboBox ID="cboAlmacen" runat="server" Width="100%" Label="Almacén" Visible="false"
                                OnSelectedIndexChanged="cboAlmacen_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </telerik:LayoutColumn>
                        <telerik:LayoutColumn Span="4"></telerik:LayoutColumn>
                        <telerik:LayoutColumn Span="1"></telerik:LayoutColumn>
                    </Columns>
                </telerik:LayoutRow>
                <telerik:LayoutRow Height="85%">
                    <Columns>
                        <telerik:LayoutColumn Span="12" Height="100%">
                            <telerik:RadTabStrip runat="server" ID="stripPedido" MultiPageID="pagesPedido" SelectedIndex="0">
                                <Tabs>
                                    <telerik:RadTab Text="Datos Principales" Selected="True"></telerik:RadTab>
                                    <telerik:RadTab Text="Productos"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>
                            <telerik:RadMultiPage runat="server" ID="pagesPedido" SelectedIndex="0" Height="93%" Width="100%">
                                <telerik:RadPageView runat="server" ID="pagePrincipal" Height="100%">
                                    <telerik:RadPageLayout ID="RadPageLayout3" runat="server" Width="100%" Height="100%">
                                        <Rows>
                                            <telerik:LayoutRow CssClass="containerSubTitulo">
                                                <Content>
                                                    <div class="col-md-12">
                                                        <asp:Label ID="lblSubTitulo" runat="server" Text="Datos del Proveedor" CssClass="subTitulo"></asp:Label>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="col-md-2">
                                                        <telerik:RadTextBox ID="txtRUC" runat="server" ReadOnly="true" Width="100%" EmptyMessage="Número de RUC" Label="RUC" LabelWidth="30%"></telerik:RadTextBox>
                                                        <asp:HiddenField ID="lblCodigoProveedor" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <telerik:RadTextBox ID="txtProveedor" runat="server" ReadOnly="true" Width="100%" EmptyMessage="Nombre del proveedor" Label="Nombre" LabelWidth="20%"></telerik:RadTextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <telerik:RadComboBox ID="cboSucursal" runat="server" Width="100%" Label="Sucursal"></telerik:RadComboBox>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="col-md-8">
                                                        <telerik:RadComboBox ID="cboFacturacion" runat="server" Width="100%" Label="Factura">
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="col-md-8">
                                                        <telerik:RadComboBox ID="cboDespacho" runat="server" Width="100%" Label="Despacho"></telerik:RadComboBox>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>

                                                    <div class="col-md-2">
                                                        <telerik:RadTextBox ID="txtNroRegistro" runat="server" Width="100%" Enabled="true" Label="Nro.Registro"></telerik:RadTextBox>
                                                    </div>

                                                    <div class="col-md-2">
                                                        <telerik:RadComboBox ID="cboPrioridad" runat="server" Width="100%" Label="Prioridad">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Value="1" Text="1" Selected="true" />
                                                                <telerik:RadComboBoxItem Value="2" Text="2" />
                                                                <telerik:RadComboBoxItem Value="3" Text="3" />
                                                                <telerik:RadComboBoxItem Value="4" Text="4" />
                                                                <telerik:RadComboBoxItem Value="5" Text="5" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <div class="colum3">
                                                            <asp:Label ID="lblTEA" runat="server" Text="TEA" CssClass="etiqueta" Visible="false"></asp:Label>
                                                        </div>
                                                        <div class="colum3">
                                                            <telerik:RadTextBox ID="txtTEA" runat="server" Width="100%" Enabled="true" Visible="false"></telerik:RadTextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2">
                                                      <%--  <telerik:RadTextBox ID="txtOrden" runat="server" Width="100%" MaxLength="20" Label="Ord. Proveedor"></telerik:RadTextBox>--%>
                                                    </div>

                                                    <div class="col-md-2">
                                                       <%-- <telerik:RadComboBox ID="cboTipoEnvio" runat="server" Width="100%" Label="Tipo Envio"></telerik:RadComboBox>--%>
                                                    </div>

                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow CssClass="containerSubTitulo">
                                                <Content>
                                                    <div class="col-md-12">
                                                        <asp:Label ID="lblDatosPedido" runat="server" Text="Datos del Pedido" CssClass="subTitulo"></asp:Label>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="col-md-2">
                                                        <telerik:RadComboBox ID="cboMoneda" runat="server" Width="100%" Label="Moneda"></telerik:RadComboBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <telerik:RadComboBox ID="cboFormaPago" runat="server" Width="100%" OnSelectedIndexChanged="cboFormaPago_SelectedIndexChanged" AutoPostBack="true" Label="Tipo Pago"></telerik:RadComboBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                      <telerik:RadTextBox  id="txtModoPagoSTR" runat="server" Width="100%"   Label="Modo de Pago"   >

                                                      </telerik:RadTextBox>
                                                   <%--   <telerik:RadNumericTextBox ID="txtModoPago" runat="server" Width="100%"   Label="Modo de Pago" >
                                                        </telerik:RadNumericTextBox>--%>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow CssClass="containerSubTitulo">
                                                <Content>
                                                    <div class="colum7">
                                                        <asp:Label ID="lblFormaPago" runat="server" Text="Datos de la Forma de pago:" CssClass="subTitulo"></asp:Label>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="col-md-2">
                                                        <telerik:RadDatePicker ID="dpFechaEmision" runat="server" Width="100%" DateInput-ReadOnly="true" DatePopupButton-Visible="false"
                                                            DateInput-Label="Fec.Emisión" DateInput-DateFormat="dd/MM/yyyy">
                                                        </telerik:RadDatePicker>
                                                    </div>

                                                    <div class="col-md-2">
                                                        <telerik:RadDatePicker ID="dpFechaEntrega" runat="server" Width="100%" DateInput-ReadOnly="true" DatePopupButton-Visible="true"
                                                            DateInput-Label="Fec.Entrega" DateInput-DateFormat="dd/MM/yyyy">
                                                        </telerik:RadDatePicker>
                                                    </div>

                                                    <div class="col-md-2">
                                                        <telerik:RadDatePicker ID="dpFechaVencimiento" runat="server" Width="100%" DateInput-ReadOnly="true" DatePopupButton-Visible="true"
                                                            DateInput-Label="Fec.Vecim." DateInput-DateFormat="dd/MM/yyyy">
                                                        </telerik:RadDatePicker>
                                                    </div>

                                                    <div class="col-md-2">
  
                                                    </div>

                                                  <%--  <div class="col-md-2">
                                                       <telerik:RadComboBox ID="cboTipoCredito" runat="server" Width="100%"
                                                            AutoPostBack="true" OnSelectedIndexChanged="cboTipoCredito_SelectedIndexChanged" Label="Tipo Crédito">
                                                        </telerik:RadComboBox>
                                                    </div>--%>
                                                </Content>
                                            </telerik:LayoutRow>
<%--                                            <telerik:LayoutRow>
                                                <Content>        
                                                    <div class="colum10">
                                                        <div class="colum3">
                                                            <asp:Label ID="lblReferencia" runat="server" Text="Referencia" CssClass="etiqueta"></asp:Label>
                                                        </div>
                                                        <div class="colum7">
                                                            <telerik:RadComboBox ID="cboReferencia" runat="server" Width="100%">
                                                            </telerik:RadComboBox>
                                                        </div>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>--%>

                                           <telerik:LayoutRow>
                                                <Content>
                                                    <div class="colum10">
                                                        <asp:Label ID="lblNotaRecepcion" runat="server" Text="Notas de Recepción" CssClass="etiqueta"></asp:Label>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="colum10">
                                                        <telerik:RadTextBox ID="txtNotaRecepcion" runat="server" Width="100%" TextMode="MultiLine" Height="50px" MaxLength="1000"></telerik:RadTextBox>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
<%--                                            <telerik:LayoutRow CssClass="containerSubTitulo">
                                                <Content>
                                                    <div class="colum7">
                                                        <asp:Label ID="lblOperacion" runat="server" Text="Operación" CssClass="subTitulo"></asp:Label>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="col-md-3">
                                                        <telerik:RadComboBox ID="cboOpDespacho" runat="server" Width="100%" Label="Tipo Despacho"></telerik:RadComboBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <telerik:RadComboBox ID="cboOpTipoPedido" runat="server" Width="100%" Label="Tipo Pedido"></telerik:RadComboBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <telerik:RadComboBox ID="cboOpDocCompra" runat="server" Width="100%" Label="Doc. Compra"></telerik:RadComboBox>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>

                                            <telerik:LayoutRow CssClass="containerSubTitulo">
                                                <Content>
                                                    <div class="colum7">
                                                        <asp:Label ID="lblSTTransporte" runat="server" Text="Transporte" CssClass="subTitulo"></asp:Label>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="col-md-3">
                                                        <telerik:RadComboBox ID="cboSede" runat="server" Width="100%" EmptyMessage="Seleccionar Sede" Label="Sede">
                                                        </telerik:RadComboBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <telerik:RadAutoCompleteBox ID="acbTransporte" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text" AllowCustomEntry="true"
                                                                DropDownHeight="200px" EmptyMessage="Buscar transportista" OnClientEntryAdding="OnClientEntryAddingHandler" DropDownWidth="250px" Label="Transportista">
                                                                <WebServiceSettings Method="Agenda_TransporteBuscar" Path="frmOrdenCompraMng.aspx" />
                                                            </telerik:RadAutoCompleteBox>
                                                    </div>
                                                    <div class="colum1">
                                                        <asp:Label ID="lblTrans" runat="server" Visible="false"></asp:Label>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>--%>

                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="colum10">
                                                        <asp:Label ID="lblObservacion" runat="server" Text="Observaciones" CssClass="etiqueta"></asp:Label>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="colum10">
                                                        <telerik:RadTextBox ID="txtObservacion" runat="server" Width="100%" TextMode="MultiLine" Height="50px" MaxLength="1000"></telerik:RadTextBox>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                        </Rows>
                                    </telerik:RadPageLayout>
                                </telerik:RadPageView>
                                <telerik:RadPageView runat="server" ID="pageItems" Height="100%" Width="100%">
                                    <telerik:RadPageLayout ID="RadPageLayout2" runat="server" Height="100%" Width="100%">
                                        <Rows>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="col-md-12 containerSubTitulo">
                                                        <asp:Label ID="lblDetPedido" runat="server" Text="Detalle del pedido" CssClass="subTitulo"></asp:Label>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                     <div class="col-md-2">

                                                         <telerik:RadComboBox ID="cboCategoria" runat="server" AutoPostBack="true" Label="Categoría" Width="90%" OnSelectedIndexChanged="cboCategoria_SelectedIndexChanged">
                                                              <Items>   
                                                                    <telerik:RadComboBoxItem runat="server" Text="Mercadería" Value="0" />   
                                                                    <telerik:RadComboBoxItem runat="server" Text="Servicio" Value="1"/>   
                                                                    <telerik:RadComboBoxItem runat="server" Text="Gasto" Value="2" /> 
                                                                    <telerik:RadComboBoxItem runat="server" Text="Activo" Value="3" /> 
                                                                </Items>
                                                         </telerik:RadComboBox>
                                                    </div>

                                                    <div class="col-md-2">
                                                        <telerik:RadAutoCompleteBox ID="acbProducto" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text" DropDownWidth="75%"
                                                            DropDownHeight="150px" EmptyMessage="Buscar producto" AllowCustomEntry="true" Label="Producto" LabelWidth="25%">
                                                            <WebServiceSettings Method="Item_BuscarProducto" Path="frmOrdenCompraMng.aspx" />
                                                        </telerik:RadAutoCompleteBox>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <telerik:RadButton ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click">
                                                            <Icon PrimaryIconUrl="../../Images/Icons/sign-add-16.png" />
                                                        </telerik:RadButton>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow Height="60%">
                                                <Columns>
                                                    <telerik:LayoutColumn Span="12" Height="100%">
                                                        <telerik:RadGrid ID="grdItem" runat="server" Width="100%" Height="270px" AutoGenerateColumns="false"
                                                            OnItemCommand="grdItem_ItemCommand" OnItemDataBound="grdItem_ItemDataBound">
                                                            <MasterTableView DataKeyNames="Item_ID" ClientDataKeyNames="Item_ID" Width="900px">
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
                                                                    <telerik:GridBoundColumn HeaderText="Cantidad" DataField="Cantidad" UniqueName="Cantidad" DataFormatString="{0:F0}">
                                                                        <HeaderStyle Width="70px" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn HeaderText="Stock" DataField="Stock" UniqueName="Stock" DataFormatString="{0:F0}">
                                                                        <HeaderStyle Width="70px" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn HeaderText="U.M.C" DataField="ID_UnidadInv" UniqueName="ID_UnidadInv">
                                                                        <HeaderStyle Width="50px" />
                                                                    </telerik:GridBoundColumn>
                                                                   <telerik:GridBoundColumn HeaderText="CCosto" DataField="CCosto" UniqueName="CCosto" >
                                                                        <HeaderStyle Width="70px" />
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn HeaderText="Factor" DataField="FactorUnidadInv" UniqueName="FactorUnidadInv" DataFormatString="{0:F2}">
                                                                        <HeaderStyle Width="50px" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn HeaderText="Precio" DataField="Precio" UniqueName="Precio" DataFormatString="{0:F2}">
                                                                        <HeaderStyle Width="70px" />
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridCalculatedColumn HeaderText="Renta." DataFields="Precio, CostoUnitario" Expression="({0}-{1})*100/{0}" DataFormatString="{0:F0}%">
                                                                        <HeaderStyle Width="70px" />
                                                                    </telerik:GridCalculatedColumn>
                                                                    <telerik:GridBoundColumn HeaderText="Dcto(%)" DataField="Descuento" UniqueName="Descuento">
                                                                        <HeaderStyle Width="70px" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn HeaderText="ID_Moneda" DataField="ID_Moneda" Visible="true" UniqueName="ID_Moneda">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn HeaderText="Importe" DataField="Importe" UniqueName="Importe" DataFormatString="{0:F2}">
                                                                        <HeaderStyle Width="70px" />
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
                                                    </telerik:LayoutColumn>
                                                </Columns>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="col-md-7">
                                                        <asp:Label ID="lblRentabilidad" runat="server" CssClass="etiqueta"></asp:Label>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <telerik:RadGrid ID="grdGlosa" runat="server" AutoGenerateColumns="false" Width="100%">
                                                            <MasterTableView>
                                                                <Columns>
                                                                    <telerik:GridBoundColumn HeaderText="Descripcion" DataField="Descripcion">
                                                                        <HeaderStyle Width="30px" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn HeaderText="Base Imp." DataField="BaseImponible" DataFormatString="{0:F2}">
                                                                        <HeaderStyle Width="30px" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn HeaderText="Importe" DataField="Importe" DataFormatString="{0:F2}">
                                                                        <HeaderStyle Width="30px" />
                                                                    </telerik:GridBoundColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                        </telerik:RadGrid>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                        </Rows>
                                    </telerik:RadPageLayout>
                                </telerik:RadPageView>
                            </telerik:RadMultiPage>
                        </telerik:LayoutColumn>
                    </Columns>
                </telerik:LayoutRow>
                <telerik:LayoutRow>
                    <Columns>
                        <telerik:LayoutColumn Span="1">
                            <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click">
                                <Icon PrimaryIconUrl="../../Images/Icons/floppy-16.png" />
                            </telerik:RadButton>
                        </telerik:LayoutColumn>
                        <telerik:LayoutColumn Span="1">
                            <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click">
                                <Icon PrimaryIconUrl="../../Images/Icons/sign-ban-16.png" />
                            </telerik:RadButton>
                        </telerik:LayoutColumn>
                        <telerik:LayoutColumn Span="1">
                            <telerik:RadButton ID="btnAprobar" runat="server" Text="Aprobar" OnClick="btnAprobar_Click" Visible="false">
                                <Icon PrimaryIconUrl="../../Images/Icons/sign-check-16.png" />
                            </telerik:RadButton>
                        </telerik:LayoutColumn>
                        <telerik:LayoutColumn Span="6">
                        </telerik:LayoutColumn>
                        <telerik:LayoutColumn Span="3">
                            <asp:Label ID="lblLineaCredito" runat="server" CssClass="etiqueta"></asp:Label>
                        </telerik:LayoutColumn>
                    </Columns>
                </telerik:LayoutRow>
            </Rows>
        </telerik:RadPageLayout>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="fila">
        <div class="colum10">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>

