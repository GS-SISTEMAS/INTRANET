<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpS.Master" AutoEventWireup="true" CodeBehind="frmPersonalZonaMng.aspx.cs" Inherits="GS.SISGEGS.Web.Comision.Mantenimiento.frmPersonalZonaMng" %>
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
                <telerik:RadComboBox ID="cboEmpresa" Enabled="false" runat="server" DataTextField="nombreComercial" 
                    onselectedindexchanged="cboEmpresa_SelectedIndexChanged" DataValueField="idEmpresa" AutoPostBack="True">
                </telerik:RadComboBox>
            </div>
        </div>

           <div class="fila">
               <div class="colum2">
                       <asp:Label ID="lblUsuario" runat="server" Text="Usuario" CssClass="etiqueta"></asp:Label>
               </div>
               <div class="colum6">
                    <telerik:RadAutoCompleteBox ID="acbUsuario" runat="server" Width="100%" TextSettings-SelectionMode="Single" InputType="Text" 
                         DropDownHeight="150px" EmptyMessage="Buscar Usuario" AllowCustomEntry="true" DropDownWidth="260px">
                         <WebServiceSettings Method="Item_BuscarUsuario" Path="frmPersonalMng.aspx" />
                   </telerik:RadAutoCompleteBox>
               </div>
                <div class="colum1">
                <telerik:RadButton ID="btnBuscarUsuario" runat="server" Text="Image Button" Width="16px" Height="16px" OnClick="btnBuscarUsuario_Click">
                    <Image ImageUrl="../../Images/Icons/search-16.png" />
                </telerik:RadButton>
            </div>

         </div>

        
         <div class="fila">
               <div class="colum2">
                        <asp:Label ID="Label2" runat="server" Text="Cargo" CssClass="etiqueta"></asp:Label>
               </div>
               <div class="colum7">
                        <telerik:RadComboBox ID="cboCargo"  runat="server" AutoPostBack="True" onselectedindexchanged="cboCargo_SelectedIndexChanged" DataTextField="VchDescripcionCargo" DataValueField="chrCodigoCargo" ></telerik:RadComboBox>
               </div>
         </div>

        <div class="fila">
            <div class="colum2">
                <asp:Label ID="Label6" runat="server" Text="NroDocumento" CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum7">
                <telerik:RadTextBox ID="txtNroDocumento" runat="server"></telerik:RadTextBox>
            </div>
        </div>


        <div class="fila">
            <div class="colum2">
                <asp:Label ID="lblNombre" runat="server" Text="Nombre " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum7">
                <telerik:RadTextBox ID="txtNombre" runat="server"></telerik:RadTextBox>
            </div>
        </div>
                <div class="fila">
            <div class="colum2">
                <asp:Label ID="Label1" runat="server" Text="Apellidos " CssClass="etiqueta"></asp:Label>
            </div>
            <div class="colum7">
                <telerik:RadTextBox ID="txtApellidos" runat="server"></telerik:RadTextBox>
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
          <div class="fila" >
            <div class="colum2">
                <asp:Label ID="Label7" runat="server" Text="Imagen" CssClass="etiqueta" Visible="false"></asp:Label>
            </div>
            <div class="colum7">
                <telerik:RadTextBox ID="txtImagen" runat="server" Visible="false"></telerik:RadTextBox>
            </div>
        </div>

       <div class="fila">
               <div class="colum2">
                        <asp:Label ID="Label8" runat="server" Text="Reporte" CssClass="etiqueta"></asp:Label>
               </div>
               <div class="colum7">
                        <telerik:RadComboBox ID="cboReporte"  runat="server"  
                             
                            DataTextField="descripcion" DataValueField="id"  ></telerik:RadComboBox>
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
