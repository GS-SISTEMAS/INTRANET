<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpS.Master" AutoEventWireup="true" CodeBehind="frmPasswordMng.aspx.cs" Inherits="GS.SISGEGS.Web.frmPasswordMng" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function CloseAndRebind(args) {
            Sys.Application.add_load(function () {
                var rWindow = GetRadWindow();
                rWindow.BrowserWindow.refreshGrid(args);
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
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="ralpPassword" ControlID="rapPassword"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpPassword" runat="server" IsSticky="true" ZIndex="9999"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="rapPassword" runat="server" Width="100%">
        <div class="fila">
            <div class="colum4">
                <asp:Label ID="lblContrasena1" runat="server" Text="Nueva contraseña: " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum6">
                <telerik:RadTextBox ID="txtContrasena1" runat="server" TextMode="Password" Width="100%" MaxLength="10"></telerik:RadTextBox>
            </div>
        </div>
        <div class="fila">
            <div class="colum4">
                <asp:Label ID="lblContrasena2" runat="server" Text="Confirmar contraseña: " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum6">
                <telerik:RadTextBox ID="txtContrasena2" runat="server" TextMode="Password" Width="100%" MaxLength="10"></telerik:RadTextBox>
            </div>
        </div>
        <div class="fila">
            <div class="colum4">
                <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click">
                    <Icon PrimaryIconUrl="Images/Icons/floppy-16.png" />
                </telerik:RadButton>
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
