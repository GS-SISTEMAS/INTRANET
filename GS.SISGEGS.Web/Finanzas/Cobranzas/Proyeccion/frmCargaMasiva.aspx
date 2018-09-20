<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpCargaMasiva.Master" AutoEventWireup="true" CodeBehind="frmCargaMasiva.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.Cobranzas.frmCargaMasiva" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function CloseAndRebind(args) {
            Sys.Application.add_load(function () {
                var rWindow = GetRadWindow();
                rWindow.BrowserWindow.refreshGridBuscar(args);
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
    <telerik:RadAjaxManager ID="ramPerfilMng" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapPerfilMng" LoadingPanelID="ralpPerfulMng"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpPerfulMng" runat="server" ZIndex="9999" IsSticky="true"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="rapPerfilMng" runat="server" Width="100%">
         <div class="fila containerSubTitulo">
             <div class="colum9">
                  <asp:Label ID="Label11" runat="server" Text="Carga masiva de Pronostico" CssClass="subTitulo"></asp:Label>
             </div>
              <div class="colum1">
                 <telerik:RadButton ID="btnCerrar" runat="server" Text="Cerrar" OnClick="btnCerrar_Click" Visible="true" ToolTip="CERRAR" Width="180%"> 
                    <Icon PrimaryIconUrl="~/Images/Icons/delete-16.png" />
                </telerik:RadButton>
              </div>
         </div>

        <div class="fila">
            <div class="colum1">
                <asp:Label ID="lblArchivo" runat="server" CssClass="etiqueta" >Archivo</asp:Label>
            </div>

          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
              <div class="colum8">
                 <asp:FileUpload ID="filCargaMasiva" runat="server" ClientIDMode="Static"  Font-Underline="False" Width="100%" />
              </div>
              <div class="colum1">
               <telerik:RadButton ID="btnCargaMasivaArchivo" runat="server" Text="Adjuntar" OnClick="btnCargaMasivaArchivo_Click" Visible="true" ToolTip="Cargar archivo" Width="180%"> 
                    <Icon PrimaryIconUrl="~/Images/Icons/floppy-16.png" />
                </telerik:RadButton>
              </div>
              </ContentTemplate>
              <Triggers>
                  <asp:PostBackTrigger ControlID="btnCargaMasivaArchivo" />
              </Triggers>
            </asp:UpdatePanel>

        </div>
     
        <div class="fila">
            <div class="colum10">
               <asp:Label ID="lblCliCliPriResCarMasPos" runat="server" CssClass="EtiquetaError" Width="100%" Visible="false" />
            </div>
         </div>
        <div class="fila">
            <div class="colum10">
               <asp:Label ID="lblCliCliPriResCarMasNeg" runat="server" CssClass="EtiquetaError" Width="100%" Visible="false" />
            </div>
         </div>
        <div class="fila">
            <div class="colum10">
               <asp:Label ID="lblCliCliPriResCarMasTot" runat="server" CssClass="EtiquetaError" Width="100%" Visible="false"/>
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
