<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpS.Master" AutoEventWireup="true" CodeBehind="frmGastosEdt.aspx.cs" Inherits="GS.SISGEGS.Web.Commercial.Gastos.frmGastosEdt" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function CloseAndRebind(args) {
            Sys.Application.add_load(function () {
                var rWindow = GetRadWindow();
                rWindow.BrowserWindow.refreshGrid(JSON.stringify(args));
                rWindow.close();
            });
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
            var acbTipoDocumento = $find("<%= acbTipoDocumento.ClientID %>");
            if (acbTipoDocumento.get_entries().get_count()>0) {
                var txtImporte = $find("<%= txtImporte.ClientID %>");
                var txtImpBase = $find("<%= txtImpBase.ClientID %>");
                var txtIGV = $find("<%= txtIGV.ClientID %>");
                var txtInafecto = $find("<%= txtInafecto.ClientID %>");

                if (acbTipoDocumento.get_entries().getEntry(0).get_text().split("-")[0] == "1") {
                    txtImpBase.set_value(txtImporte.get_value() / 1.18);
                    txtIGV.set_value(txtImpBase.get_value() * 0.18);
                }
                else {
                    txtImpBase.set_value(0);
                    txtIGV.set_value(0);
                }                
                txtInafecto.set_value(txtImporte.get_value() - txtImpBase.get_value() - txtIGV.get_value());
            }
        }

        function CalcularImpuestos_ImpBase() {
            var acbTipoDocumento = $find("<%= acbTipoDocumento.ClientID %>");
            if (acbTipoDocumento.get_entries().get_count() > 0) {
                var txtImporte = $find("<%= txtImporte.ClientID %>");
                var txtImpBase = $find("<%= txtImpBase.ClientID %>");
                var txtIGV = $find("<%= txtIGV.ClientID %>");
                var txtInafecto = $find("<%= txtInafecto.ClientID %>");
                
                if (acbTipoDocumento.get_entries().getEntry(0).get_text().split("-")[0] == "1")
                    txtIGV.set_value(txtImpBase.get_value() * 0.18);
                else
                    txtIGV.set_value(0);  
                txtInafecto.set_value(txtImporte.get_value() - txtImpBase.get_value() - txtIGV.get_value());
            }
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
            <div class="colum7">
                <telerik:RadAutoCompleteBox ID="acbProveedor" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text"
                    DropDownHeight="150px" EmptyMessage="Buscar proveedor" AllowCustomEntry="true">
                    <WebServiceSettings Method="Agenda_ListarProveedor" Path="frmGastosEdt.aspx" />
                </telerik:RadAutoCompleteBox>
                <div class="colum3">
                    <telerik:RadTextBox ID="txtNroRUC" runat="server" EmptyMessage="Nro.RUC" Visible="false" Width="100%" MaxLength="11"></telerik:RadTextBox>
                </div>
                <div class="colum7">
                    <telerik:RadTextBox ID="txtRazonSocial" runat="server" EmptyMessage="Razón social" Visible="false" Width="100%" MaxLength="150"></telerik:RadTextBox>
                </div>
            </div>
            <div class="colum1">
                <telerik:RadButton ID="btnAgregar" runat="server" Width="24px" OnClick="btnAgregar_Click">
                    <Icon PrimaryIconUrl="../../Images/Icons/sign-add-16.png"/>
                </telerik:RadButton>
                <telerik:RadButton ID="btnCancelar" runat="server" Width="24px" Visible="false" OnClick="btnCancelar_Click">
                    <Icon PrimaryIconUrl="../../Images/Icons/sign-error-16.png" />
                </telerik:RadButton>
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
                <telerik:RadAutoCompleteBox ID="acbTipoDocumento" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text"
                    DropDownHeight="150px" EmptyMessage="Buscar documento" AllowCustomEntry="true">
                    <WebServiceSettings Method="Documento_ListarTipoCompra" Path="frmGastosEdt.aspx" />
                </telerik:RadAutoCompleteBox>
            </div>
        </div>
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblSerie" runat="server" Text="Serie" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum3">
                <telerik:RadTextBox ID="txtSerie" runat="server" Width="100%"></telerik:RadTextBox>
            </div>
            <div class="colum2">
                <asp:Label ID="lblNumero" runat="server" Text="Número" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum3">
                <telerik:RadNumericTextBox ID="txtNumero" runat="server" Type="Number" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator=""
                    Width="100%" NumberFormat-AllowRounding="true">
                </telerik:RadNumericTextBox>
            </div>
        </div>
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblFecha" runat="server" Text="Emisión" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum4">
                <telerik:RadDatePicker ID="dpFecEmision" runat="server" Width="100%">
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
