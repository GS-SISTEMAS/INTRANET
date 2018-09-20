<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpS.Master" AutoEventWireup="true" CodeBehind="frmProductoClienteMng.aspx.cs" Inherits="GS.SISGEGS.Web.Mantenimiento.IntranetGS.Producto.frmProductoClienteMng1" %>
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
    <telerik:RadAjaxManager ID="ranProductoMng" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscarProducto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapProductoMng" LoadingPanelID="ralpProductoMng"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapProductoMng" LoadingPanelID="ralpProductoMng"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpProductoMng" runat="server" ZIndex="9999" IsSticky="true"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="rapProductoMng" runat="server" Width="100%">
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblCliente" runat="server" Text="Cliente" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum8">
                <telerik:RadAutoCompleteBox ID="acbCliente" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text"
                    DropDownHeight="150px" EmptyMessage="Buscar cliente" AllowCustomEntry="true" DropDownWidth="260px">
                    <WebServiceSettings Method="Agenda_BuscarCliente" Path="frmProductoClienteMng.aspx" />
                </telerik:RadAutoCompleteBox>
            </div>
        </div>
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblProducto" runat="server" Text="Producto" CssClass="etiqueta"></asp:Label>
            </div>
            <disv class="colum7">
                <telerik:RadAutoCompleteBox ID="acbProducto" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text" 
                    DropDownHeight="150px" EmptyMessage="Buscar producto" AllowCustomEntry="true" DropDownWidth="260px">
                    <WebServiceSettings Method="Item_BuscarProducto" Path="frmProductoClienteMng.aspx" />
                </telerik:RadAutoCompleteBox>
            </disv>
            <div class="colum1">
                <telerik:RadButton ID="btnBuscarProducto" runat="server" Text="Image Button" Width="16px" Height="16px" OnClick="btnBuscarProducto_Click">
                    <Image ImageUrl="../../../Images/Icons/search-16.png" />
                </telerik:RadButton>
            </div>
        </div>
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblKardex" runat="server" Text="Kardex" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum3">
                <telerik:RadTextBox ID="txtKardex" runat="server" Enabled="false" Width="100%"></telerik:RadTextBox>
            </div>
            <div class="colum2">
                <asp:Label ID="lblUnidad" runat="server" Text="Unidad" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum3">
                <telerik:RadTextBox ID="txtUnidad" runat="server" Enabled="false" Width="100%"></telerik:RadTextBox>
            </div>
        </div>
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblPrecio" runat="server" Text="Precio" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum3">
                <telerik:RadTextBox ID="txtPrecio" runat="server" Enabled="false" Width="100%"></telerik:RadTextBox>
            </div>
            <div class="colum2">
                <asp:Label ID="lblMoneda" runat="server" Text="Moneda" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum3">
                <telerik:RadComboBox ID="cboMoneda" runat="server" Enabled="false" Width="100%"></telerik:RadComboBox>
            </div>
        </div>
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblPrecEspecial" runat="server" Text="Prec.Espec." CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum3">
                <telerik:RadNumericTextBox ID="txtPrecEspecial" runat="server" NumberFormat-DecimalDigits="6" Width="100%"></telerik:RadNumericTextBox>
            </div>
            <div class="colum5">
                <telerik:RadButton ID="btnTermino" runat="server" Text="Sin Termino" ToggleType="CheckBox" 
                    ButtonType="ToggleButton" OnCheckedChanged="btnTermino_CheckedChanged"></telerik:RadButton>
            </div>
        </div>
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblFechaInicio" runat="server" Text="Vig.Inicio" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum3">
                <telerik:RadDatePicker ID="dpFechaInicio" runat="server" Width="100%">
                    <DateInput runat="server" DateFormat="dd/MM/yyyy"></DateInput>
                </telerik:RadDatePicker>
            </div>
            <div class="colum2">
                <asp:Label ID="lblFechaFinal" runat="server" Text="Vig.Final" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum3">
                <telerik:RadDatePicker ID="dpFechaFinal" runat="server" Width="100%">
                    <DateInput runat="server" DateFormat="dd/MM/yyyy"></DateInput>
                </telerik:RadDatePicker>
            </div>
        </div>
        <div class="fila">
            <div class="colum5">
                <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click">
                    <Icon PrimaryIconUrl="../../../Images/Icons/floppy-16.png"/>
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
