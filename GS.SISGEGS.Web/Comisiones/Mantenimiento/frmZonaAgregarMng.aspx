<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpSX.Master" AutoEventWireup="true" CodeBehind="frmZonaAgregarMng.aspx.cs" Inherits="GS.SISGEGS.Web.Comision.Mantenimiento.frmZonaAgregarMng" %>
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
                <telerik:RadComboBox ID="cboEmpresa" runat="server" Enabled="false"
                    DataValueField="idEmpresa"  DataTextField="nombreComercial"  AutoPostBack="True">
                </telerik:RadComboBox>
            </div>
        </div>

        <div class="fila">
               <div class="colum2">
                        <asp:Label ID="Label2" runat="server" Text="Cargo" CssClass="etiqueta"></asp:Label>
               </div>
               <div class="colum7">
                        <telerik:RadComboBox ID="cboCargo"  runat="server" AutoPostBack="True" 
                            onselectedindexchanged="cboCargo_SelectedIndexChanged" DataTextField="VchDescripcionCargo" DataValueField="chrCodigoCargo"  ></telerik:RadComboBox>
               </div>
         </div>

           <div class="fila">
               <div class="colum2">
                       <asp:Label ID="lblUsuario" runat="server" Text="Usuario" CssClass="etiqueta"></asp:Label>
               </div>
               <div class="colum7">
                        <telerik:RadComboBox ID="cboPersonal"  runat="server" AutoPostBack="True" DataValueField="NroDocumento"  DataTextField="NombreCompleto"  
                            onselectedindexchanged="cboPersonal_SelectedIndexChanged"  ></telerik:RadComboBox>
               </div>

         </div>
       <div class="fila">
               <div class="colum2">
                       <asp:Label ID="Label1" runat="server" Text="Zonas" CssClass="etiqueta"></asp:Label>
               </div>
               <div class="colum7">
                        <telerik:RadComboBox ID="cboZonas"  runat="server" AutoPostBack="True"
                            onselectedindexchanged="cboZonas_SelectedIndexChanged"
                             DataValueField="id_zona"  DataTextField="Nombre"                  
                             ></telerik:RadComboBox>
               </div>

         </div>


         <div class="fila">
            <div class="colum2">
                <asp:Label ID="Label3" runat="server" Text="%Comisión" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum2">
                <telerik:RadTextBox ID="txtComision" runat="server"  ></telerik:RadTextBox>
            </div>
           <div class="colum1" >
                 <asp:Label ID="Label4" runat="server" Text="%" CssClass="etiqueta" ></asp:Label>
            </div>
             <div class="colum4">
                  <asp:Label ID="Label5" runat="server"  CssClass="etiqueta" ></asp:Label>
            </div>
        </div>

        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblEstado" runat="server" Text="Comisión" CssClass="etiqueta"></asp:Label>
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
