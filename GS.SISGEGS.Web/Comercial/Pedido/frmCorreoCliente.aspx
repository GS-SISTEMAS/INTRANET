<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpC.Master" AutoEventWireup="true" CodeBehind="frmCorreoCliente.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Pedido.frmCorreoCliente" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function CloseAndRebind(args) {
            Sys.Application.add_load(function () {
                var rWindow = GetRadWindow();
                rWindow.BrowserWindow.refreshGridCorreo(args);
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
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramPedidoItem" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlPedidoItem" LoadingPanelID="ralpPedidoItem" ></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" ></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpPedidoItem" runat="server" ZIndex="9999" IsSticky="true"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="pnlPedidoItem" runat="server">
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblCodigo" runat="server" Text="Código" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum8">
                <telerik:RadTextBox ID="txtCodigo" runat="server" Enabled="false" Width="100%"></telerik:RadTextBox>
            </div>
        </div>
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblCliente" runat="server" Text="Cliente" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum8">
                <telerik:RadTextBox ID="txtCliente" runat="server" Enabled="false" Width="100%"></telerik:RadTextBox>
            </div>
        </div>
        <div class="fila">
           <div class="colum2">
                <asp:Label ID="Label1" runat="server" Text="Email:" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum8">
                           <telerik:RadTextBox  ID="txtCorreo" runat="server"  Width="100%" >
                           </telerik:RadTextBox>

                            <asp:RegularExpressionValidator ID="emailValidator" runat="server" Display="Dynamic"
                                ErrorMessage="Please enter valid e-mail address" ValidationExpression="^[\w\.\-]+@[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]{1,})*(\.[a-zA-Z]{2,3}){1,2}$"
                                ControlToValidate="txtCorreo">
                            </asp:RegularExpressionValidator>

                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" runat="server" Display="Dynamic"
                                ControlToValidate="txtCorreo" ErrorMessage="Please enter an e-mail"></asp:RequiredFieldValidator>

            </div>


        </div>
        <div class="fila">
            <div class="colum2">
                <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" UseSubmitBehavior="false">
                    <Icon PrimaryIconUrl="../../Images/Icons/floppy-16.png" />
                </telerik:RadButton>
            </div>
            <div class="colum2">
                <telerik:RadTextBox ID="lblId_Agenda" runat="server" style="visibility:hidden" Width="0%"></telerik:RadTextBox>
            </div>
            <div class="colum2">
              
            </div>
        </div>
        <div class="fila">
            <div class="colum10">
                <asp:Label ID="lblObservacion" runat="server" Text="Observación" CssClass="etiqueta"></asp:Label>
            </div>
        </div>
        <div class="fila">
            <div class="colum10">
                <telerik:RadTextBox ID="txtObservacion" runat="server" Width="100%" TextMode="MultiLine" Height="40px"></telerik:RadTextBox>
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