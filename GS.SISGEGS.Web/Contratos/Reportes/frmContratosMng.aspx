<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpMX.Master" AutoEventWireup="true" CodeBehind="frmContratosMng.aspx.cs" Inherits="GS.SISGEGS.Web.Contratos.Reportes.frmContratosMng" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function CloseAndRebind(args) {
            Sys.Application.add_load(function () {
                var rWindow = GetRadWindow();
                rWindow.BrowserWindow.refreshGrid(args);
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

    <telerik:RadAjaxPanel ID="rapGastosMng" runat="server" Width="100%">
        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblCodigo" runat="server" Text="Código" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum3">
                <telerik:RadTextBox ID="txtCodigoContrato" runat="server" Width="100%"></telerik:RadTextBox>
            </div>
            <div class="colum2">
                <asp:Label ID="lblAreaResponsable" runat="server" Text="Area Responsable" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum3">
                <telerik:RadComboBox ID="cboAreaResponsable" runat="server" Width="100%">
                    </telerik:RadComboBox>
            </div>
        </div>
        <div class="fila">
                <div class="colum2">
                    <asp:Label ID="lblMateriaContrato" runat="server" Text="Materia" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="colum3">
                    <telerik:RadComboBox ID="cboMateria" runat="server" Width="100%">
                    </telerik:RadComboBox>
                </div>
            
                <div class="colum2">
                    <asp:Label ID="lblTipo" runat="server" Text="Tipo" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="colum3">
                    <telerik:RadComboBox ID="cboTipo" runat="server" Width="100%">
                    </telerik:RadComboBox>
                </div>
        </div>
        <div class="fila">
                <div class="colum2">
                    <asp:Label ID="lblRenovar" runat="server" Text="Renovar" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="colum3">
                    <telerik:RadTextBox ID="txtRenovar" runat="server" Width="100%"></telerik:RadTextBox>
                </div>
            
                <div class="colum2">
                    <asp:Label ID="lblCliente" runat="server" Text="Cliente / Proveedor" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="colum3">
                     <telerik:RadComboBox ID="cboCliente" runat="server" Width="100%">
                    </telerik:RadComboBox>
                </div>
        </div>
        <div class="fila">
                <div class="colum2">
                    <asp:Label ID="lblContratante" runat="server" Text="Contratante" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="colum3">
                    <telerik:RadTextBox ID="txtContratante" runat="server" Width="100%"></telerik:RadTextBox>
                </div>
            
                <div class="colum2">
                    <asp:Label ID="lblFechaSuscripcion" runat="server" Text="Fecha Suscripción" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="colum3">
                     <telerik:RadDatePicker ID="dtpFechaSuscripcion" runat="server" Width="100%" DateInput-ReadOnly="true" Culture="es-PE">
                        <DateInput runat="server" DateFormat="dd/MM/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
                </div>
        </div>


        <div class="fila">
                <div class="colum2">
                    <asp:Label ID="lblRenovacion" runat="server" Text="Renovación" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="colum3">
                     <telerik:RadTextBox ID="txtRenovación" runat="server" Width="100%"></telerik:RadTextBox>
                </div>
            <div class="colum2">
                    <asp:Label ID="lblFechaVencimiento" runat="server" Text="Fecha Vencimiento" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="colum3">
                    <telerik:RadDatePicker ID="dtpFechaVencimiento" runat="server" Width="100%" DateInput-ReadOnly="true" Culture="es-PE">
                        <DateInput runat="server" DateFormat="dd/MM/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
                </div>
        </div>
        
        <div class="fila">
                <div class="colum2">
                    <asp:Label ID="lblMonto" runat="server" Text="Monto" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="colum3">
                    <telerik:RadTextBox ID="txtMonto" runat="server" Width="100%"></telerik:RadTextBox>
                </div>
            
                <div class="colum2">
                    <asp:Label ID="lblEstado" runat="server" Text="Estado" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="colum3">
                     <telerik:RadComboBox ID="cboEstado" runat="server" Width="100%">
                    </telerik:RadComboBox>
                </div>
        </div>

        <div class="fila">
                <div class="colum2">
                    <asp:Label ID="lblObjeto" runat="server" Text="Objeto" CssClass="etiqueta"></asp:Label>
                </div>
                <div class="colum6">
                    <telerik:RadTextBox ID="txtObjeto" runat="server" Width="100%" TextMode="MultiLine" Height="60"></telerik:RadTextBox>
                </div>
        </div>
        <div class="fila">
            &nbsp;
        </div>
        <div class="fila">
            <div class="colum2">
                <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" >
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
