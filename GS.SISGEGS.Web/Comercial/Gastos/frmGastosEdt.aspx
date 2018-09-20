<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpS.Master" AutoEventWireup="true" CodeBehind="frmGastosEdt.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Gastos.frmGastosEdt" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" href="http://localhost/resources/css/toastr.min.css" rel="stylesheet" />
    
    <script type="text/javascript" src="http://localhost/resources/scripts/common/jquery.js"></script>
    <script type="text/javascript" src="http://localhost/resources/scripts/common/toastr.min.js"></script>
    <script type="text/javascript" src="http://localhost/resources/scripts/init.js"></script>

    <script type="text/javascript">

        function CloseAndRebind(args) {

            var documento = args.NombreDocumento;
            if (documento.toUpperCase().indexOf("BOLETA") >= 0) {
                var params = JSON.stringify({
                    "action": 5,
                    "parametros": [
						args.ID_Agenda
                    ]
                });
                $.ajax({
                    type: "POST",
                    url: wsnode + "wsMaster.svc/EjecutarTransaccionSU",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: params,
                    async: true,
                    processData: false,
                    cache: false,
                    success: function (transaction) {
                        if (transaction.type == 0) {
                            Sys.Application.add_load(function () {
                                var rWindow = GetRadWindow();
                                rWindow.BrowserWindow.refreshGrid(JSON.stringify(args));
                                rWindow.close();
                            });
                        }
                        else
                            showError(transaction.message);
                    },
                    error: function (transaction) {
                        showError(transaction.statusText);
                    }
                });
            }
            else {
                Sys.Application.add_load(function () {
                    var rWindow = GetRadWindow();
                    rWindow.BrowserWindow.refreshGrid(JSON.stringify(args));
                    rWindow.close();
                });
            }
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

        function CalcularImpuestos_Importe() {
            var cboTipoDoc = $find("<%= cboTipoDocumento.ClientID %>");
            var txtImporte = $find("<%= txtImporte.ClientID %>");
            var txtImpBase = $find("<%= txtImpBase.ClientID %>");
            var txtIGV = $find("<%= txtIGV.ClientID %>");
            var txtInafecto = $find("<%= txtInafecto.ClientID %>");

            if (cboTipoDoc.get_selectedItem().get_value() == "1" || cboTipoDoc.get_selectedItem().get_value() == "12") {
                txtImpBase.set_value(txtImporte.get_value() / 1.18);
                txtIGV.set_value(txtImpBase.get_value() * 0.18);
            }
            else {
                txtImpBase.set_value(0);
                txtIGV.set_value(0);
            }
            txtInafecto.set_value(txtImporte.get_value() - txtImpBase.get_value() - txtIGV.get_value());
        }

        function CalcularImpuestos_ImpBase() {
            var cboTipoDoc = $find("<%= cboTipoDocumento.ClientID %>");
            var txtImporte = $find("<%= txtImporte.ClientID %>");
            var txtImpBase = $find("<%= txtImpBase.ClientID %>");
            var txtIGV = $find("<%= txtIGV.ClientID %>");
            var txtInafecto = $find("<%= txtInafecto.ClientID %>");

            if (cboTipoDoc.get_selectedItem().get_value() == "1" || cboTipoDoc.get_selectedItem().get_value() == "12")
                txtIGV.set_value(txtImpBase.get_value() * 0.18);
            else
                txtIGV.set_value(0);
            txtInafecto.set_value(txtImporte.get_value() - txtImpBase.get_value() - txtIGV.get_value());
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramGastoEdt" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapGastoEdt" LoadingPanelID="ralpGastoEdt"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapGastoEdt" LoadingPanelID="ralpGastoEdt"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpGastoEdt" runat="server" ZIndex="9999" IsSticky="true">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="rapGastoEdt" runat="server" Width="100%">
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblProveedor" runat="server" Text="Proveedor" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum8">
                <telerik:RadAutoCompleteBox ID="acbProveedor" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text"
                    DropDownHeight="150px" EmptyMessage="Buscar proveedor" AllowCustomEntry="true">
                    <WebServiceSettings Method="Agenda_ListarProveedor" Path="frmGastosEdt.aspx" />
                </telerik:RadAutoCompleteBox>
            </div>
        </div>
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblTipoGasto" runat="server" Text="Gasto" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum8">
                <telerik:RadComboBox ID="cboTipoGasto" runat="server" Width="100%"></telerik:RadComboBox>
            </div>
        </div>
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblTipoDocumento" runat="server" Text="Tipo Doc." CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum8">
                <%--<telerik:RadAutoCompleteBox ID="acbTipoDocumento" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text"
                    DropDownHeight="150px" EmptyMessage="Buscar documento" AllowCustomEntry="true">
                    <WebServiceSettings Method="Documento_ListarTipoCompra" Path="frmGastosEdt.aspx" />
                </telerik:RadAutoCompleteBox>--%>
                <telerik:RadComboBox ID="cboTipoDocumento" runat="server" Width="100%" OnSelectedIndexChanged="cboTipoDocumento_SelectedIndexChanged" AutoPostBack="true" ></telerik:RadComboBox>
            </div>
        </div>
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblSerie" runat="server" Text="Serie" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum3">
                <telerik:RadTextBox ID="txtSerie" runat="server" Width="100%" MaxLength="4"></telerik:RadTextBox>
            </div>
            <div class="colum2">
                <asp:Label ID="lblNumero" runat="server" Text="Número" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum3">
                <telerik:RadNumericTextBox ID="txtNumero" runat="server" Type="Number" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator=""
                    Width="100%" NumberFormat-AllowRounding="true" MaxLength="9">
                </telerik:RadNumericTextBox>
            </div>
        </div>
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblFecha" runat="server" Text="Emisión" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum4">
                <telerik:RadDatePicker ID="dpFecEmision" runat="server" Width="100%" Culture="es-PE">
                    <DateInput runat="server" DateFormat="dd/MM/yyyy"></DateInput>
                </telerik:RadDatePicker>
            </div>
            <%--<div class="colum2">
                <asp:Label ID="lblImporte" runat="server" Text="Importe" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum2">
                <telerik:RadNumericTextBox ID="txtImporte" runat="server" Type="Number" NumberFormat-DecimalDigits="2" NumberFormat-AllowRounding="true"
                    MinValue="0" Width="100%">
                </telerik:RadNumericTextBox>
            </div>--%>
        </div>
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblImporte" runat="server" Text="Importe" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum3">
                <telerik:RadNumericTextBox ID="txtImporte" runat="server" Type="Number" NumberFormat-DecimalDigits="2" 
                    NumberFormat-AllowRounding="true" MinValue="0" Width="100%">
                    <ClientEvents OnBlur="CalcularImpuestos_Importe"/>
                </telerik:RadNumericTextBox>
            </div>
            <div class="colum2">
                <asp:Label ID="lblBase" runat="server" Text="Imp.Base" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum3">
                <telerik:RadNumericTextBox ID="txtImpBase" runat="server" Type="Number" NumberFormat-DecimalDigits="2" 
                    NumberFormat-AllowRounding="true" MinValue="0" Width="100%">
                    <ClientEvents OnBlur="CalcularImpuestos_ImpBase"/>
                </telerik:RadNumericTextBox>
            </div>
        </div>
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblIGV" runat="server" Text="IGV" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum3">
                <telerik:RadNumericTextBox ID="txtIGV" runat="server" Type="Number" NumberFormat-DecimalDigits="2" NumberFormat-AllowRounding="true"
                    MinValue="0" Width="100%" ReadOnly="true">
                </telerik:RadNumericTextBox>
            </div>
            <div class="colum2">
                <asp:Label ID="lblInafecto" runat="server" Text="Inafecto" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum3">
                <telerik:RadNumericTextBox ID="txtInafecto" runat="server" Type="Number" NumberFormat-DecimalDigits="2" NumberFormat-AllowRounding="true"
                    MinValue="0" Width="100%" ReadOnly="true">
                </telerik:RadNumericTextBox>
            </div>
        </div>
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="Observación" runat="server" Text="Observación" CssClass="etiqueta"></asp:Label>
            </div>
        </div>
        <div class="fila">
            <div class="colum10">
                <telerik:RadTextBox ID="txtComentario" runat="server" Height="40px" Width="100%" TextMode="MultiLine" MaxLength="100"></telerik:RadTextBox>
            </div>
        </div>
        <div class="fila">
            <div class="colum2">
                <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click">
                    <Icon PrimaryIconUrl="../../Images/Icons/floppy-16.png"/>
                </telerik:RadButton>
            </div>
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mensaje" runat="server">
    <div class="fila">
        <div class="colum10">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
