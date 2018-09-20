<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpS.Master" AutoEventWireup="true" CodeBehind="frmMenuMng.aspx.cs" Inherits="GS.SISGEGS.Web.Management.Security.Menu.frmMenuMng" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
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
        </script>
    </telerik:RadCodeBlock>

    <telerik:RadAjaxManager ID="mngMenuMng" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl LoadingPanelID="lpMenuMng"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="lpMenuMng" runat="server" ZIndex="9999">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="pnlMenuMng" runat="server" Width="100%">
        <div class="fila">
            <div class="colum8">
                <telerik:RadTextBox ID="txtNombre" runat="server" Label="Nombre: " Width="70%" LabelWidth="60px" MaxLength="20"></telerik:RadTextBox>
            </div>
        </div>
        <div class="fila">
            <div class="colum10">
                <telerik:RadTextBox ID="txtURL" runat="server" Label="URL: " Width="90%" LabelWidth="60px" MaxLength="400"></telerik:RadTextBox>
            </div>
        </div>
        <div class="fila">
            <div class="colum3">
                <telerik:RadButton ID="ckbActivo" runat="server" Text="Activo" ToggleType="CheckBox" ButtonType="ToggleButton" Width="100%" Checked="true"></telerik:RadButton>
            </div>
            <div class="colum3">
                <telerik:RadButton ID="ckbDefecto" runat="server" Text="Defecto" ToggleType="CheckBox" ButtonType="ToggleButton" Width="100%"></telerik:RadButton>
            </div>
        </div>
        <div class="fila">
            <div class="colum1">
                <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click">
                    <Icon PrimaryIconUrl="../../../Images/Icons/floppy-16.png" />
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

