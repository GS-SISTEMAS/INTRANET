<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpS.Master" AutoEventWireup="true" CodeBehind="frmUsuarioMng.aspx.cs" Inherits="GS.SISGEGS.Web.Mantenimiento.IntranetGS.Usuario.frmUsuarioMng" %>
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


        function OnClientEntryAddingHandler(sender, eventArgs) {
            if (sender.get_entries().get_count() > 0) {
                eventArgs.set_cancel(true);
                alert("Solo se puede selecionar un elemento.");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <telerik:RadAjaxManager ID="ramUsuarioMng" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapUsuarioMng" LoadingPanelID="ralpUsuarioMng"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpUsuarioMng" runat="server" ZIndex="9999" IsSticky="true"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="rapUsuarioMng" runat="server" Width="100%">
        <div class="fila">
            <div class="colum3">
                <asp:Label ID="lblEmpresa" runat="server" Text="Empresa " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum5">
                <telerik:RadComboBox ID="cboEmpresa" runat="server" AutoPostBack="true"  Width="230px" DataTextField="nombreComercial" DataValueField="idEmpresa" OnSelectedIndexChanged="cboEmpresa_SelectedIndexChanged"  >
                </telerik:RadComboBox>
            </div>
            <div class="colum1">
            </div>
        </div>
        <div class="fila">
            <div class="colum3">
                <asp:Label ID="lblNombre" runat="server" Text="Usuario " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum5">
                    <telerik:RadAutoCompleteBox ID="acbUsuario" runat="server" AllowCustomEntry="true" DropDownHeight="100px" 
                     DropDownWidth="200px"  EmptyMessage="Buscar Usuario" InputType="Text" 
                     OnClientEntryAdding="OnClientEntryAddingHandler" TextSettings-SelectionMode="Single" Width="100%">
                     <WebServiceSettings Method="Agenda_UsuarioBuscar" Path="frmUsuarioMng.aspx" />
                   </telerik:RadAutoCompleteBox>
                   <asp:HiddenField ID="lblCodigoUsuario" runat="server" />
            </div>
            <div class="colum1">
                  <telerik:RadButton ID="btnBuscarUsuario" runat="server" OnClick="btnBuscarUsuario_Click" Text="Selec.">
                  <Icon PrimaryIconUrl="../../../Images/Icons/search-16.png"/></telerik:RadButton>
            </div>
        </div>

               <div class="fila containerSubTitulo">
                  <div class="colum9">
                  <asp:Label ID="Label11" runat="server" Text="Datos Usuario" CssClass="subTitulo">
                  </asp:Label>
                 </div>
              </div>




        <div class="fila">
            <div class="colum3">
                <asp:Label ID="Label1" runat="server" Text="Nombre " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum6">
               <telerik:RadTextBox ID="txtNombre" runat="server" EmptyMessage="Nombre usuario"  Width="230px">
               </telerik:RadTextBox>
           </div>
        </div>

        <div class="fila">
            <div class="colum3">
                <asp:Label ID="Label3" runat="server" Text="Login Usuario " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum6">
               <telerik:RadTextBox ID="txtLogin" runat="server" EmptyMessage="Login Usuario" Width="230px">
               </telerik:RadTextBox>
           </div>

        </div>

        <div class="fila">
            <div class="colum3">
                <asp:Label ID="lblCorreo" runat="server" Text="Correo " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum6">
               <telerik:RadTextBox ID="txtCorreo" runat="server" EmptyMessage="Correo"  Width="230px">
               </telerik:RadTextBox>
           </div>
        </div>
        <div class="fila">
            <div class="colum3">
                <asp:Label ID="lblPerfil" runat="server" Text="Perfil " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum5">
                   <telerik:RadComboBox ID="cboPerfil" runat="server"  Width="230px" DataTextField="nombrePerfil" DropDownWidth="200px" DataValueField="idPerfil"   ></telerik:RadComboBox>
            </div>
            <div class="colum1">
            </div>
        </div>
         <div class="fila">
            <div class="colum3">
                <asp:Label ID="Label2" runat="server" Text="NroDocumento " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum6">
               <telerik:RadTextBox ID="txtNroDocumento" runat="server" EmptyMessage="Nro Documento"   Width="230px">
               </telerik:RadTextBox>
           </div>
        </div>

         <div class="fila">
            <div class="colum3">
                <asp:Label ID="Label4" runat="server" Text="Clave " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum6">
               <telerik:RadTextBox ID="txtClave" runat="server" EmptyMessage="Clave"   Width="230px" Enabled="false">
               </telerik:RadTextBox>
           </div>
        </div>


        <div class="fila">
            <div class="colum3">
                <asp:Label ID="lblEstado" runat="server" Text="Estado " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum6">
                <telerik:RadComboBox ID="cboEstado" runat="server" Width="230px">
                    <Items>
                        <telerik:RadComboBoxItem Value="1" Text="Activo" Selected="true" />
                        <telerik:RadComboBoxItem Value="0" Text="Inactivo" />
                    </Items>
                </telerik:RadComboBox>
            </div>
  
        </div>
        <div class="fila">
            <div class="colum4">
                <telerik:RadButton ID="btnCambioClave" runat="server" Text="Cambio de clave" ToggleType="CheckBox" ButtonType="ToggleButton"></telerik:RadButton>
            </div>
            <div class="colum5">
                <telerik:RadButton ID="btnCambioAmbos" runat="server" Text="Cambio de clave Ambos" ToggleType="CheckBox" ButtonType="ToggleButton"></telerik:RadButton>
            </div>
        </div>
        
        <div class="fila">
            <div class="colum5">
                <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click">
                    <Icon PrimaryIconUrl="../../../Images/Icons/floppy-16.png" />
                </telerik:RadButton>
                 <asp:HiddenField ID="lblClaveUsuario" runat="server" />
            </div>
              <div class="colum4">
                <telerik:RadButton ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click">
                    <Icon PrimaryIconUrl="../../../Images/Icons/trashcan-20.png" />
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
