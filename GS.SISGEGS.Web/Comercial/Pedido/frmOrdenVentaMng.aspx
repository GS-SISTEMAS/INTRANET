<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPage.Master" AutoEventWireup="true" CodeBehind="frmOrdenVentaMng.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Pedido.frmOrdenVentaMng" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Grupo Silvestre: Registrar orden de venta
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
<%--    
    <link type="text/css" href="https://intranet.gruposilvestre.com.pe/resources/css/toastr.min.css" rel="stylesheet" />
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/common/jquery.js"></script>
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/common/toastr.min.js"></script>
    <script type="text/javascript" src="https://intranet.gruposilvestre.com.pe/resources/scripts/init.js"></script>
    --%>
    <script>

        function OnClientEntryAddingHandler(sender, eventArgs) {
            if (sender.get_entries().get_count() > 0) {
                eventArgs.set_cancel(true);
                alert("Solo se puede selecionar un elemento.");
            }
        }

        function ShowInsertFormLetras(FechaInicio, FechaFin, idOrdenVenta, id_agenda) {
            window.radopen("frmOrdenLetras.aspx?FechaInicio=" + FechaInicio + "&FechaFin=" + FechaFin + "&idOrdenVenta=" + idOrdenVenta + "&id_agenda=" + id_agenda, "rwPedidoLetrasMng");
            return false;
            
        }

		var owinVerCotizacion = null;
        function ShowInsertFormCorreo(objCliente) {
            window.radopen("frmCorreoCliente.aspx?objCliente=" + objCliente, "rwCorreoMng");
            return false;
        }

		function ShowVerCotizacion() {
			var hdValues = $('#<%=hdValues.ClientID %>').val();
			$('#<%=hdReturn.ClientID %>').val(null);
            owinVerCotizacion = window.radopen("frmVerCotizacion.htm?obj=" + hdValues, "rwVerCotizacionMng");
            return false;
        }
		
		function HideVerCotizacion() {
			owinVerCotizacion.close();
			//owinVerCotizacion = null;
		}
		
		function setObj(IdCotizacion) {
			$('#<%=hdReturn.ClientID %>').val(IdCotizacion);
			setTimeout(function(){document.getElementById('body_btnCargar').click();} , 1000);
			document.getElementById('ctl00_body_btnVerCotizacion_input').disabled = true;
			//alert($('#<%=hdReturn.ClientID %>').val());
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
                $find("<%=ramPedidoMng.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%=ramPedidoMng.ClientID %>").ajaxRequest("RebindAndNavigate(" + arg + ")");
            }
        }

        function refreshGridCorreo(arg) {
                $find("<%=ramPedidoMng.ClientID %>").ajaxRequest("RebindAndNavigateCorreo(" + arg + ")");
       
        }

        function refreshGridLetras(arg) {
            if (!arg) {
                $find("<%=ramPedidoMng.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%=ramPedidoMng.ClientID %>").ajaxRequest("RebindAndNavigateLetras(" + arg + ")");
            }
        }


        function RowDblClick(sender, eventArgs) {
            var combo = $find('<%=cboAlmacen.ClientID %>');
            var comboMoneda = $find('<%=cboMoneda.ClientID %>');

            window.radopen("frmOrdenVentaItem.aspx?idItem=" + eventArgs.getDataKeyValue("Item_ID") + "&idCliente=" + document.getElementById('<%= lblCodigoCliente.ClientID%>').value +
                "&nuevo=" + 0 + "&idAlmacen=" + combo.get_selectedItem().get_value() + "&id_moneda=" + comboMoneda.get_selectedItem().get_value(), "rwPedidoMng");
        }

        function ShowInsertForm(idItem, idCliente, nuevo, idAlmacen, id_moneda) {
            window.radopen("frmOrdenVentaItem.aspx?idItem=" + idItem + "&idCliente=" + idCliente + "&nuevo=" + nuevo + "&idAlmacen=" + idAlmacen + "&id_moneda=" + id_moneda, "rwPedidoMng");
            return false;
        }


        function buscar() {
            alert('Se registro correctamente el Correo Electronico, continuar!');
            document.getElementById('ctl00_body_btnBuscarCliente_input').click();
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

<input type="hidden" runat="server" id="hdValues" />
<input type="hidden" runat="server" id="hdReturn" />
<asp:Button ID="btnCargar" runat="server" OnClick="btnCargar_Click" style="display:none;" ></asp:Button>

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
                     <telerik:AjaxUpdatedControl ControlID="lblLetras" />
                                         <telerik:AjaxUpdatedControl ControlID="txtDiasCredito" />
                     <telerik:AjaxUpdatedControl ControlID="dpFechaVencimiento" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ramPedidoMng">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" LoadingPanelID="ralpPedidoMng"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="grdGlosa"></telerik:AjaxUpdatedControl>
                     <telerik:AjaxUpdatedControl ControlID="lblLetras" />
                     <telerik:AjaxUpdatedControl ControlID="txtDiasCredito" />
                     <telerik:AjaxUpdatedControl ControlID="dpFechaVencimiento" />
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
                     <telerik:AjaxUpdatedControl ControlID="lblLetras" />
                     <telerik:AjaxUpdatedControl ControlID="txtDiasCredito" />
                     <telerik:AjaxUpdatedControl ControlID="dpFechaVencimiento" />

                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlPedidoMng" LoadingPanelID="ralpPedidoMng" />
                    <telerik:AjaxUpdatedControl ControlID="lblLetras" />
                     <telerik:AjaxUpdatedControl ControlID="txtDiasCredito" />
                     <telerik:AjaxUpdatedControl ControlID="dpFechaVencimiento" />

                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnValidarCorreo">
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
             <telerik:RadWindow ID="rwPedidoLetrasMng" runat="server" Width="570px" Height="570px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>

            <telerik:RadWindow ID="rwPedidoMng" runat="server" Width="400px" Height="400px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>

            <telerik:RadWindow ID="rwCorreoMng" runat="server" Width="400px" Height="300px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>

            <telerik:RadWindow ID="rwVerCotizacionMng" runat="server" Width="982px" Height="600px" ReloadOnShow="true"
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
                            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo" Text="Registrar Pedido"></asp:Label>
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
                            <telerik:RadAutoCompleteBox ID="acbCliente" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text"
                                DropDownHeight="150px" EmptyMessage="Buscar cliente" AllowCustomEntry="true" Label="Cliente">
                                <WebServiceSettings Method="Agenda_BuscarCliente" Path="frmOrdenVentaMng.aspx" />
                            </telerik:RadAutoCompleteBox>
                        </telerik:LayoutColumn>

                        <telerik:LayoutColumn Span="4">
                            <telerik:RadComboBox ID="cboAlmacen" runat="server" Width="100%" Label="Almacén"
                                OnSelectedIndexChanged="cboAlmacen_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </telerik:LayoutColumn>

                        <telerik:LayoutColumn Span="1">
                            <telerik:RadButton ID="btnBuscarCliente" runat="server" OnClick="btnBuscarCliente_Click" Text="Seleccionar" Width="200px">
                                <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                            </telerik:RadButton>
                            <br />
                            <telerik:RadButton ID="btnVerCotizacion" runat="server" onclientclicked="ShowVerCotizacion" Text="Ver Cotizacion" Width="200px">
                                <Icon PrimaryIconUrl="../../Images/Icons/search-16.png" />
                            </telerik:RadButton>
                         

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
                                                        <asp:Label ID="lblSubTitulo" runat="server" Text="Datos del Cliente" CssClass="subTitulo"></asp:Label>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="col-md-3">
                                                        <telerik:RadTextBox ID="txtRUC" runat="server" ReadOnly="true" Width="100%" EmptyMessage="Número de RUC" Label="RUC" LabelWidth="15%"   ></telerik:RadTextBox>
                                                        <asp:HiddenField ID="lblCodigoCliente" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <telerik:RadTextBox ID="txtCliente" runat="server" ReadOnly="true" Width="100%" EmptyMessage="Nombre del cliente" Label="Nombre"  LabelWidth="15%" ></telerik:RadTextBox>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <telerik:RadComboBox ID="cboSucursal" runat="server" Width="50%" Label="Sucursal" labelWidth="15%"   ></telerik:RadComboBox>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="col-md-6">
                                                        <telerik:RadTextBox ID="txtCorreo" runat="server" ReadOnly="true" Width="100%" EmptyMessage="Correo Electrónico" Label="Correo Electrónico" LabelWidth="25%" ></telerik:RadTextBox>
                                                    </div>
                                                     <div class="col-md-6">
                                                     </div>

                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="col-md-6">
                                                        <telerik:RadComboBox ID="cboFacturacion" runat="server" Width="100%" Label="Factura" labelWidth="15%"  >
                                                        </telerik:RadComboBox>
                                                    </div>
                                                     <div class="col-md-6">
                                                      
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                     <div class="col-md-6">
                                                        <telerik:RadComboBox ID="cboDespacho" runat="server" Width="100%" Label="Despacho"  labelWidth="15%"  ></telerik:RadComboBox>
                                                    </div>
                                                     <div class="col-md-6">
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>

                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="col-md-2">
                                                        <telerik:RadTextBox ID="txtOrden" runat="server" Width="90%" MaxLength="20" Label="Ord. Cliente" labelWidth="50%" ></telerik:RadTextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <telerik:RadTextBox ID="txtNroRegistro" runat="server" Width="90%" Enabled="false" Label="Nro.Registro" labelWidth="50%"></telerik:RadTextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <telerik:RadComboBox ID="cboTipoEnvio" runat="server" Width="90%" Label="Tipo Envio" labelWidth="40%"></telerik:RadComboBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <telerik:RadComboBox ID="cboPrioridad" runat="server" Width="90%" Label="Prioridad" labelWidth="40%">
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
                                                            <telerik:RadTextBox ID="txtTEA" runat="server" Width="80%" Enabled="false" Visible="false" Label="TEA" labelWidth="40%"></telerik:RadTextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2">
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
                                                        <telerik:RadComboBox ID="cboMoneda"  runat="server" Width="100%" Label="Moneda" ></telerik:RadComboBox>
                                                    </div>
                                                    
                                                    <div class="col-md-2">
                                                        <telerik:RadComboBox ID="cboFormaPago" runat="server" Width="100%" OnSelectedIndexChanged="cboFormaPago_SelectedIndexChanged" AutoPostBack="true" Label="Tipo Venta"></telerik:RadComboBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <telerik:RadAutoCompleteBox ID="acbVendedor" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text" Label="Vendedor"
                                                            DropDownHeight="150px" EmptyMessage="Selec. vendedor" AllowCustomEntry="true" OnClientEntryAdding="OnClientEntryAddingHandler">
                                                            <WebServiceSettings Method="Agenda_BuscarVendedor" Path="frmOrdenVentaMng.aspx" />
                                                        </telerik:RadAutoCompleteBox>
                                                    </div>
                                                   <div class="col-md-4">
                                                    </div>

                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow CssClass="containerSubTitulo">
                                                <Content>
                                                    <div class="col-md-12">
                                                        <asp:Label ID="lblFormaPago" runat="server" Text="Datos de la Forma de pago:" CssClass="subTitulo"></asp:Label>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="col-md-2">
                                                        <telerik:RadDatePicker ID="dpFechaEmision" runat="server" Width="100%"  DateInput-ReadOnly="true" DatePopupButton-Visible="false"
                                                            DateInput-Label="Fec.Emisión" DateInput-DateFormat="dd/MM/yyyy">
                                                        </telerik:RadDatePicker>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <telerik:RadDatePicker ID="dpFechaVencimiento" runat="server" Width="100%" DateInput-ReadOnly="true" DatePopupButton-Visible="false"
                                                            DateInput-Label="Fec.Vecim." DateInput-DateFormat="dd/MM/yyyy">
                                                        </telerik:RadDatePicker>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <telerik:RadNumericTextBox ID="txtDiasCredito" runat="server" Width="100%" Type="Number" ReadOnly="true" Label="Días de Cred."
                                                            NumberFormat-DecimalDigits="0" MaxLength="3" MinValue="0" onKeyUp="TextChanged(this,event);">
                                                        </telerik:RadNumericTextBox>
                                                    </div>

                                                      <div class="col-md-1">
                                                 
                                                               <asp:RadioButton id="rbtFacturas" GroupName="RegularMenu" AutoPostBack="true"  Width="280px"  Font-Size="Small"
                                                                      Text="Factura" runat="server" OnCheckedChanged="radioButton1_CheckedChanged1" Visible="false"/>
                                                                <asp:RadioButton id="rbtLetras" GroupName="RegularMenu" AutoPostBack="true" Font-Size="Small" 
                                                                    OnCheckedChanged="radioButton2_CheckedChanged1" Visible="false"
                                                                     Text="Letras"  runat="server"/>
                                                      </div>
                                                    
 

 
                                                     <div class="col-md-3">
                                                       <div class="col-md-3">
                                                        <telerik:RadComboBox ID="cboTipoCredito" runat="server" Visible="false"  Width="160px" DropDownWidth="200px"
                                                            AutoPostBack="true" OnSelectedIndexChanged="cboTipoCredito_SelectedIndexChanged" Label="Tipo Crédito" >

                                                        </telerik:RadComboBox>
                                                        </div>
 
                                                            <br />
                                                     <div class="col-md-1">
                                                         <asp:Label ID="lblTexto" runat="server" Font-Size="Small" Text="Tipo Crédito" Font-Bold="true"></asp:Label>
                                                       
                                                     </div>
                                                       <div class="col-md-1">
                                                        <telerik:RadButton ID="btnLetras" runat="server" OnClick="btnLetras_Click" Text="Planificar Letras">
                                                            <Icon PrimaryIconUrl="../../Images/Icons/calendario_1.png" />
                                                        </telerik:RadButton>
                                                      </div>
                                                    </div>
                                                      <div class="col-md-1">
                                                            <asp:Label ID="Label1" runat="server" Text="" label=""></asp:Label>
                                                             <br />
                                                         <asp:Label ID="lblLetras" runat="server" Text="" label="Letras:"></asp:Label>
                                                      </div>

                                                      <div class="col-md-1">
                                                      </div>

                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow CssClass="containerSubTitulo">
                                                <Content>
                                                    <div class="col-md-12">
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
                                                        <telerik:RadComboBox ID="cboOpDocVenta" runat="server" Width="100%" Label="Doc. Venta"></telerik:RadComboBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow CssClass="containerSubTitulo">
                                                <Content>
                                                    <div class="col-md-12">
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
                                                                <WebServiceSettings Method="Agenda_TransporteBuscar" Path="frmOrdenVentaMng.aspx" />
                                                            </telerik:RadAutoCompleteBox>
                                                    </div>
                                                    <div class="colum1">
                                                        <asp:Label ID="lblTrans" runat="server" Visible="false"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="col-md-12">
                                                        <asp:Label ID="lblObservacion" runat="server" Text="Observaciones" CssClass="etiqueta"></asp:Label>
                                                    </div>
                                                </Content>
                                            </telerik:LayoutRow>
                                            <telerik:LayoutRow>
                                                <Content>
                                                    <div class="col-md-12">
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
                                            

                                                    <div class="col-md-5">
                                                        <telerik:RadAutoCompleteBox ID="acbProducto" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text" DropDownWidth="82%"
                                                            DropDownHeight="150px" EmptyMessage="Buscar producto" AllowCustomEntry="true" Label="Producto" LabelWidth="20%">
                                                            <WebServiceSettings Method="Item_BuscarProducto" Path="frmOrdenVentaMng.aspx" />
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
                                                                        <HeaderStyle Width="120px" />
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
                                                                    <telerik:GridBoundColumn HeaderText="Factor" DataField="FactorUnidadInv" UniqueName="FactorUnidadInv" DataFormatString="{0:F2}">
                                                                        <HeaderStyle Width="50px" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn HeaderText="Precio" DataField="Precio" UniqueName="Precio" DataFormatString="{0:F4}">
                                                                        <HeaderStyle Width="70px" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridCalculatedColumn HeaderText="Renta." DataFields="Precio, CostoUnitario" Expression="({0}-{1})*100/{0}" DataFormatString="{0:F0}%">
                                                                        <HeaderStyle Width="70px" />
                                                                    </telerik:GridCalculatedColumn>
                                                                    <telerik:GridBoundColumn HeaderText="Dcto(%)" DataField="Descuento" UniqueName="Descuento">
                                                                        <HeaderStyle Width="70px" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn HeaderText="ID_Moneda" DataField="ID_Moneda" Visible="false" UniqueName="ID_Moneda">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn HeaderText="Importe" DataField="Importe" UniqueName="Importe" DataFormatString="{0:F4}">
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

