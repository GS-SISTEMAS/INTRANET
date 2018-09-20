<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpZona.Master" AutoEventWireup="true" CodeBehind="frmPersonalZonaMng.aspx.cs" Inherits="GS.SISGEGS.Web.Mantenimiento.IntranetGS.Mantenedor.frmPersonalZonaMng" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function CloseAndRebind(args) {
            Sys.Application.add_load(function () {
                var rWindow = GetRadWindow();
                rWindow.BrowserWindow.refreshGrid(args);
                rWindow.close();
            });
        }

        function CloseAndRebindZona(args) {
            Sys.Application.add_load(function () {
                var rWindow = GetRadWindow();
                rWindow.BrowserWindow.refreshGridZona(args);
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
    <telerik:RadAjaxManager ID="ramEmpresaMng" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapEmpresaMng" LoadingPanelID="ralpPerfulMng"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>

               <telerik:AjaxSetting AjaxControlID="btnBuscarUsuario">
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
            <div class="colum3">
                <asp:Label ID="lblEmpresa" runat="server" Text="Empresa " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum5">
                <telerik:RadComboBox ID="cboEmpresa" runat="server" AutoPostBack="true"  DataTextField="nombreComercial" DataValueField="idEmpresa" OnSelectedIndexChanged="cboEmpresa_SelectedIndexChanged"  >
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
                     <WebServiceSettings Method="Agenda_UsuarioBuscar" Path="frmPersonalZonaMng.aspx" />
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
                <asp:Label ID="Label1" runat="server" Text="NroDocumento " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum6">
               <telerik:RadTextBox ID="txtNroDocumento" runat="server" EmptyMessage="Nombre usuario"  Width="100%">
               </telerik:RadTextBox>
                <asp:HiddenField ID="hfIdUsuario" runat="server" />
           </div>
        </div>

        <div class="fila">
            <div class="colum3">
                <asp:Label ID="Label3" runat="server" Text="Nombre " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum6">
               <telerik:RadTextBox ID="txtNombre" runat="server" EmptyMessage="Nombre usuario"  Width="100%">
               </telerik:RadTextBox>
           </div>
        </div>

        <div class="fila">
            <div class="colum3">
                <asp:Label ID="Label2" runat="server" Text="Reporte" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum6">
                    <telerik:RadComboBox ID="cboReporte"  runat="server"  
                    DataTextField="NombreReporte" DataValueField="idReporte" Width="100%"   ></telerik:RadComboBox>
            </div>
        </div>

        <div class="fila">
            <div class="colum3">
                <asp:Label ID="lblEstado" runat="server" Text="Estado" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum6">
                <telerik:RadComboBox ID="cboEstado" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem Value="1" Text="Activo" Selected="true" />
                        <telerik:RadComboBoxItem Value="0" Text="Inactivo" />
                    </Items>
                </telerik:RadComboBox>
            </div>
        </div>

        <div class="fila">
            <div class="colum9">
                <telerik:RadListBox ID="ltbZonas" runat="server" RenderMode="Lightweight" CheckBoxes="true" ShowCheckAll="true" Enabled="false"
                    Width="100%" Height="230px" DataTextField="Nombre" DataValueField="Id_Zona"></telerik:RadListBox>
            </div>
        </div>

        <div class="fila">
          <div class="colum5">
            <telerik:RadButton ID="btnEditar" runat="server" Text="Editar" OnClick="btnEditar_Click">
                <Icon PrimaryIconUrl="../../../Images/Icons/pencil-16.png"/>
            </telerik:RadButton>
            <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" Visible="false" OnClick="btnGuardar_Click">
                <Icon PrimaryIconUrl="../../../Images/Icons/floppy-16.png"/>
            </telerik:RadButton>
          </div>
           <div class="colum4">
                        <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" Visible="false" OnClick="btnCancelar_Click">
                            <Icon PrimaryIconUrl="../../../Images/Icons/sign-ban-16.png"/>
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
