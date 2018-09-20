<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpS.Master" AutoEventWireup="true" CodeBehind="frmHistorialCierreContable.aspx.cs" Inherits="GS.SISGEGS.Web.Contabilidad.Planificacion.frmHistorialCierreContable" %>
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
   </table>
    <div class="col-md-10">
                <telerik:RadGrid ID="grdHistorial" runat="server" Width="80%" AutoGenerateColumns="false" Height="200px">
                    <MasterTableView EditMode="PopUp">
                        <Columns>
                            
                            <telerik:GridBoundColumn DataField="id_Modulo" HeaderText="id_Modulo" UniqueName="id_Modulo" Visible="False">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="FechaCierre" HeaderText="FechaCierre" UniqueName="FechaCierre" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                            
                            <telerik:GridBoundColumn DataField="Responsable" HeaderText="Responsable" UniqueName="Responsable">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Observacion" HeaderText="Observacion" UniqueName="Observacion">
                                <HeaderStyle Width="100px"/>
                            </telerik:GridBoundColumn>
                            
                        </Columns>
                    </MasterTableView>
                    
                    <ClientSettings>
                        <Scrolling AllowScroll="true"/>
                        <Selecting AllowRowSelect="true"/>
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mensaje" runat="server">
</asp:Content>
