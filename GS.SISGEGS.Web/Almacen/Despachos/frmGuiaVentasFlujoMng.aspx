<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpMS.Master" AutoEventWireup="true" CodeBehind="frmGuiaVentasFlujoMng.aspx.cs" Inherits="GS.SISGEGS.Web.Almacen.Despachos.frmGuiaVentasFlujoMng" %>
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

            if (sender.value != "")
                dateVar.setDate(dateVar.getDate() + parseInt(sender.value));
        }

        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= ramGuiaVentasFlujoMng.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%= ramGuiaVentasFlujoMng.ClientID %>").ajaxRequest("RebindAndNavigate(" + arg + ")");
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

    </script>
</asp:Content>
<asp:Content  ID="Content2" ContentPlaceHolderID="body" runat="server" >
    <telerik:RadAjaxManager ID="ramGuiaVentasFlujoMng" runat="server" OnAjaxRequest="ramGuiaVentasFlujoMng_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlGuiaVentasFlujoMng" LoadingPanelID="ralpGuiaVentasFlujoMng" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="acbCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlGuiaVentasFlujoMng" LoadingPanelID="ralpGuiaVentasFlujoMng" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpGuiaVentasFlujoMng" runat="server" ZIndex="9999" IsSticky="true" Width="550px">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmGuiaVentasFlujoMng" runat="server" EnableShadow="true" Width="550px">
        <Windows>
            <telerik:RadWindow ID="rwGuiaVentasFlujoMng" runat="server" Width="400px" Height="400px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close" Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="pnlGuiaVentasFlujoMng" runat="server" Width="550px">
        <div class="fila">
            <telerik:RadDiagram ID="RadDiagram1" runat="server"></telerik:RadDiagram>
        </div>

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
            </div>
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mensaje" runat="server">
    <div class="fila">
        <div class="colum10">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
             <asp:HiddenField ID="lblOp" runat="server" />
        </div>
    </div>
</asp:Content>

