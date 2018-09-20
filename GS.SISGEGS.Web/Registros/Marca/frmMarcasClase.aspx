<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpMX.Master" AutoEventWireup="true" CodeBehind="frmMarcasClase.aspx.cs" Inherits="GS.SISGEGS.Web.Contratos.Reportes.frmMarcasClase" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function CloseAndRebind(args) {
            Sys.Application.add_load(function () {
                var rWindow = GetRadWindow();
                //rWindow.BrowserWindow.refreshGrid(args);
                rWindow.close();
            });
        }

        <%--function refreshGrid(arg) {
            if (!arg) {
                $find("<%= ramGastosMng.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%= ramGastosMng.ClientID %>").ajaxRequest("RebindAndNavigate(" + arg + ")");
            }
        }--%>


        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)

            return oWindow;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <telerik:RadAjaxPanel ID="rapMarcasMng" runat="server" Width="100%">
        <div class="fila">
                <asp:label runat="server" ID="lblTitulo" CssClass="titulo"></asp:label>
            <br /><br />
            </div>
        
        
        <div class="fila">
            &nbsp;
        </div>
        
       
        <div  class="fila">
            <div class="col-md-12">
                <telerik:RadGrid ID="grdGeneralMarcas" runat="server" AutoGenerateColumns="false" Height="300px" Width="100%"  AllowSorting="true"  >
                                    <MasterTableView ShowFooter="true" >
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="idClase" UniqueName="idClase" HeaderText="Clase" >
                                                    <HeaderStyle Width="40" />
                                                </telerik:GridBoundColumn>  
                                                <telerik:GridBoundColumn DataField="Descripcion" UniqueName="Descripcion" HeaderText="Títulos de las Clases" AllowSorting="true">
                                                    <HeaderStyle Width="340" />
                                                </telerik:GridBoundColumn>                                               
                                            </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Scrolling AllowScroll="true" UseStaticHeaders="true"/>
                                    <Selecting AllowRowSelect="true"/>
                                </ClientSettings>
                            </telerik:RadGrid>
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
