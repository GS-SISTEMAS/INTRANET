<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpC.Master" AutoEventWireup="true" CodeBehind="frmModificaCierreContable.aspx.cs" Inherits="GS.SISGEGS.Web.Contabilidad.Planificacion.frmModificaCierreContable" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
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
    <table style="width:50%;">
    <tr>
        <td>Modulo:</td>
        <td>
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>Fecha Nueva</td>
        <td><telerik:RadDatePicker ID="dpUpdate" runat="server" Width="100px"></telerik:RadDatePicker></td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>Solicitante:</td>
        <td><telerik:RadDropDownList runat="server" ID="ddlAgenda" Width="300px"></telerik:RadDropDownList></td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>Detalle:</td>
        <td>
            <asp:TextBox ID="txtDetalle" runat="server" Width="350px"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>Observacion:</td>
        <td>
            <asp:TextBox ID="txtObservacion" runat="server" TextMode="MultiLine" Width="350px"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>
            <telerik:RadButton ID="btnGuardarUp" runat="server" Text="Guardar" OnClick="btnGuardarUp_OnClick" >
                    <Icon PrimaryIconUrl="../../Images/Icons/floppy-16.png" />
                </telerik:RadButton>
        </td>
        <td></td>
    </tr>
</table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mensaje" runat="server">
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
