<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpS.Master" AutoEventWireup="true" CodeBehind="frmPerfilMng.aspx.cs" Inherits="GS.SISGEGS.Web.Mantenimiento.IntranetGS.Perfil.frmPerfilMng" %>
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
    <telerik:RadAjaxManager ID="ramPerfilMng" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapPerfilMng" LoadingPanelID="ralpPerfulMng"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpPerfulMng" runat="server" ZIndex="9999" IsSticky="true"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="rapPerfilMng" runat="server" Width="100%">
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblEmpresa" runat="server" Text="Empresa " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum3">
                <telerik:RadComboBox ID="cboEmpresa" runat="server" DataTextField="nombreComercial" DataValueField="idEmpresa">
                </telerik:RadComboBox>
            </div>
        </div>
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblNombre" runat="server" Text="Nombre " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum7">
                <telerik:RadTextBox ID="txtNombre" runat="server"></telerik:RadTextBox>
            </div>
        </div>
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblEstado" runat="server" Text="Estado " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum3">
                <telerik:RadComboBox ID="cboEstado" runat="server">
                    <Items>
                        <telerik:RadComboBoxItem Value="1" Text="Activo" Selected="true" />
                        <telerik:RadComboBoxItem Value="0" Text="Inactivo" />
                    </Items>
                </telerik:RadComboBox>
            </div>
        </div>
        <div class="fila">
            <div class="colum5">
                <telerik:RadButton ID="btnRevisarPedido" runat="server" Text="Revisar todos los pedidos" ToggleType="CheckBox" ButtonType="ToggleButton"></telerik:RadButton>
            </div>
        </div>
        <div class="fila">
            <div class="colum5">
                <telerik:RadButton ID="btnAprobarPlanilla0" runat="server" Text="Aprobación Jefe Inm." ToggleType="CheckBox" ButtonType="ToggleButton"></telerik:RadButton>
            </div>
            <div class="colum5">
                <telerik:RadButton ID="btnAprobarPlanilla1" runat="server" Text="Aprobación Contable" ToggleType="CheckBox" ButtonType="ToggleButton"></telerik:RadButton>
            </div>
        </div>
        <div class="fila">
            <div class="colum2">
                <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click">
                    <Icon PrimaryIconUrl="../../../Images/Icons/floppy-16.png" />
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
