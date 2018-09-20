<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpS.Master" AutoEventWireup="true" CodeBehind="DetraccionAccionMng.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.Reportes.DetraccionAccionMng" %>
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

        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("btnExcelMng") >= 0)
                args.set_enableAjax(false);

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div style="padding:10px;">
        <table>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td class="auto-style1">
                    <asp:Label ID="lblContancia" runat="server" CssClass="etiqueta" Text="Nro de Contancia: " Width="73px"></asp:Label>
                 </td>
                <td></td>
                <td class="auto-style1">
                    <telerik:RadTextBox  ID="rtbNroContancia" runat="server" Width="200px"></telerik:RadTextBox>
                </td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td class="auto-style1">
                    <asp:Label ID="lblFecha" runat="server" CssClass="etiqueta" Text="Fecha: " Width="73px"></asp:Label>
                </td>
                <td></td>
                <td class="auto-style1">
                    <telerik:RadDatePicker ID="dpFechaConstancia" runat="server" DateInput-ReadOnly="true" Width="150px">
                        <DateInput runat="server" DateFormat="dd/MM/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
                </td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="7" style="height:15px;"></td>
            </tr>
            <tr>
                <td colspan="7">
                    <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_OnClick">
                        <Icon PrimaryIconUrl="../../Images/Icons/floppy-16.png" />
                    </telerik:RadButton>
                </td>
                <%--<td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>--%>
            </tr> 
        </table>
        <div class="fila">
            <div class="colum2">
                <%--<telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_OnClick">
                    <Icon PrimaryIconUrl="../../Images/Icons/floppy-16.png" />
                </telerik:RadButton>--%>

            </div>
        </div>
        <div class="fila">
        <div class="colum10">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mensaje" runat="server">
    
</asp:Content>
