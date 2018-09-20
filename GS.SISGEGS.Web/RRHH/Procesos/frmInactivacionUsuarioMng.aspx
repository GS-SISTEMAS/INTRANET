<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Security/mstPopUpS.Master" CodeBehind="frmInactivacionUsuarioMng.aspx.cs" Inherits="GS.SISGEGS.Web.RRHH.Procesos.frmInactivacionUsuarioMng" %>
 
 

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
     
     

               <div class="fila containerSubTitulo">
                  <div class="colum9">
                  <asp:Label ID="Label11" runat="server" Text="Datos Usuario" CssClass="subTitulo">
                  </asp:Label>
                 </div>
              </div>



        <div class="fila">
            <div class="colum3">
                <asp:Label ID="Label3" runat="server" Text="Login Usuario " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum6">
               <telerik:RadTextBox ID="txtLogin" runat="server"  AutoPostBack="true" EmptyMessage="Login Usuario" Width="230px" Enabled="false">
               </telerik:RadTextBox>
           </div>

        </div>
        <div class="fila">
            <div class="colum3">
                <asp:Label ID="Label1" runat="server" Text="Nombre " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum6">
               <telerik:RadTextBox ID="txtNombre" runat="server" EmptyMessage="Nombre usuario"  Width="230px" Enabled="false">
               </telerik:RadTextBox>
           </div>
        </div>

        

        <div class="fila">
            <div class="colum3">
                <asp:Label ID="Label2" runat="server" Text="Correo " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum6">
               <telerik:RadTextBox ID="txtCorreo" runat="server" EmptyMessage="Correo" Width="230px" Enabled="false">
               </telerik:RadTextBox>
           </div>

        </div>
  
        <div class="fila">
            <div class="colum5">
                <telerik:RadButton ID="chekSilvestre" runat="server" Text="Silvestre" ToggleType="CheckBox" ButtonType="ToggleButton"></telerik:RadButton>
            </div>
            <div class="colum5">
                <telerik:RadButton ID="checkNeoaground" runat="server" Text="NeoAgrum" ToggleType="CheckBox" ButtonType="ToggleButton"></telerik:RadButton>
            </div>
            <div class="colum5">
                <telerik:RadButton ID="checkInatec" runat="server" Text="Inatec" ToggleType="CheckBox" ButtonType="ToggleButton"></telerik:RadButton>
            </div>
            <div class="colum5">
                <telerik:RadButton ID="checkIntranet" runat="server" Text="Intranet" ToggleType="CheckBox" ButtonType="ToggleButton"></telerik:RadButton>
            </div>
            <div class="colum5">
                <telerik:RadButton ID="checkTicket" runat="server" Text="Ticket" ToggleType="CheckBox" ButtonType="ToggleButton"></telerik:RadButton>
            </div>
        </div>
        
        <div class="fila">
            <div class="colum5">
                <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar"    OnClick="btnGuardar_Click" >
                    <%--<Icon PrimaryIconUrl="../../../Images/Icons/floppy-16.png" />--%>
                </telerik:RadButton>
                 <asp:HiddenField ID="lblClaveUsuario" runat="server" />
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
