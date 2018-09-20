<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpCargaMasiva.Master" AutoEventWireup="true" CodeBehind="frmCargaMasiva.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Proyectado.frmCargaMasiva" %>
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

        (function (global) {
            var telerikDemo = global.telerikDemo = {};

            telerikDemo.OnClientClicking = function (sender, args) {
                var $ = $telerik.$;

                if (!sender.get_autoPostBack()) {
                    var label = $(".demo-label")[0];
                    label.innerHTML = "<span>Client-Side Click: <strong>" + sender.get_text() + "</strong> was clicked.</span>";
                }
                if (sender.get_navigateUrl() && sender.get_buttonType() == Telerik.Web.UI.RadButtonType.LinkButton) {
                    var url = sender.get_navigateUrl()
                    radopen(url, url);
                    args.set_cancel(true);
                }
            };

        })(window);

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

      <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>

            <telerik:AjaxSetting AjaxControlID="Panel1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="ConfigurationPanel1" LoadingPanelID="RadAjaxLoadingPanel2"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ConfigurationPanel1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadAjaxLoadingPanel1" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="ConfigurationPanel1" LoadingPanelID="RadAjaxLoadingPanel2"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>

        </AjaxSettings>
      </telerik:RadAjaxManager>

        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
        </telerik:RadAjaxLoadingPanel>
         <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Transparency="70">
        </telerik:RadAjaxLoadingPanel>


        <asp:Panel ID="Panel1" runat="server" Height="120px" CssClass="background1">
                   <div class="fila containerSubTitulo">
                        <div class="colum10">
                            <asp:Label ID="Label11" runat="server" Text="Carga masiva de pronóstico:" CssClass="subTitulo"></asp:Label>
                        </div>
                  </div>
                   <div class="fila">
                    <div class="colum6">
                        <telerik:RadAsyncUpload RenderMode="Lightweight" runat="server" Width="100%" 
                                ID="RadAsyncUpload1" 
                                MultipleFileSelection="Disabled" EnableInlineProgress="false"
                                AutoAddFileInputs="false"  Localization-Select="Seleccionar Archivo"
                                AllowedFileExtensions="xls,xlsx"
                                OnFileUploaded="RadAsyncUpload1_FileUploaded1" />

                        <asp:Label ID="Label1" Text="*Tipo Archivo: XLS" runat="server" Style="font-size: 12px;"></asp:Label>
                        <telerik:RadProgressManager runat="server" ID="RadProgressManager1" />
                        <telerik:RadProgressArea RenderMode="Lightweight" runat="server" ID="RadProgressArea2"  />
                     </div>
                     <div class="colum2">
                        <telerik:RadButton ID="btnCargaMasivaArchivo"  runat="server" Text="Adjuntar" Visible="true" ToolTip="Cargar archivo"> 
                            <Icon PrimaryIconUrl="../../Images/Icons/floppy-16.png" />
                        </telerik:RadButton>
                     </div>
                     <div class="colum2">
                        <telerik:RadButton ID="btnCerrar" runat="server" Text="Cerrar" OnClick="btnCerrar_Click" Visible="true" ToolTip="CERRAR"> 
                        <Icon PrimaryIconUrl="../../Images/Icons/delete-16.png" />
                        </telerik:RadButton>
                    </div>
               </div>
                   <div class="fila">
                        <div class="colum10">
                           <asp:Label ID="lblPositivo" runat="server" CssClass="EtiquetaError" Width="100%"  />
                        </div>
                        <div class="colum10">
                           <asp:Label ID="lblNegativo" runat="server" CssClass="EtiquetaError" Width="100%" Visible="false" />
                        </div>
                        <div class="colum10">
                           <asp:Label ID="lblTotal" runat="server" CssClass="EtiquetaError" Width="100%" Visible="false"/>
                        </div>
                </div>
        </asp:Panel> 

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mensaje" runat="server">
    <div class="fila">
        <div class="colum10">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
