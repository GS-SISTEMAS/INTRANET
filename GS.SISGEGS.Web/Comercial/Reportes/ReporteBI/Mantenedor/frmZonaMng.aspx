<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpSX.Master" AutoEventWireup="true" CodeBehind="frmZonaMng.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Reportes.ReportesBI.Mantenedor.frmZonaMng" %>
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
    <telerik:RadAjaxManager ID="ramEmpresaMng" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapEmpresaMng" LoadingPanelID="ralpPerfulMng"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpPerfulMng" runat="server" ZIndex="9999" IsSticky="true"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="rapEmpresaMng" runat="server" Width="100%">
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblEmpresa" runat="server" Text="Empresa " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum3">
                <telerik:RadComboBox ID="cboEmpresa" runat="server" Enabled="false" DataTextField="nombreComercial" DataValueField="idEmpresa">
                </telerik:RadComboBox>
            </div>
        </div>

           <div class="fila">
            <div class="colum2">
                <asp:Label ID="Label2" runat="server" Text="CodZona" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum7">
                <telerik:RadTextBox ID="txtCodigoZona" runat="server"></telerik:RadTextBox>
            </div>
        </div>


        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblNombre" runat="server" Text="Zona" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum7">
                <telerik:RadTextBox ID="txtZona" runat="server"></telerik:RadTextBox>
            </div>
        </div>

         <div class="fila">
            <div class="colum2">
                <asp:Label ID="Label1" runat="server" Text="Procentaje" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum5">
                <telerik:RadTextBox ID="txtProcentaje" runat="server"></telerik:RadTextBox>
            </div>
             <div class="colum2">
                <asp:Label ID="Label3" runat="server" Text="%" CssClass="etiqueta"></asp:Label>
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
            <div class="colum2">
                <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click">
                    <Icon PrimaryIconUrl="../../Images/Icons/floppy-16.png" />
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
