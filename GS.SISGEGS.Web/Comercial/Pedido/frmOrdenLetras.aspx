<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpML.Master" AutoEventWireup="true" CodeBehind="frmOrdenLetras.aspx.cs" Inherits="GS.SISGEGS.Web.Comercial.Pedido.frmOrdenLetras" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function CloseAndRebind(args) {
            Sys.Application.add_load(function () {
                var rWindow = GetRadWindow();
                rWindow.BrowserWindow.refreshGridLetras(args);
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
    <telerik:RadAjaxManager ID="ramDocumento" runat="server">
           <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnAceptar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapDocumento" LoadingPanelID="ralpDocumento" ></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" ></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpDocumento" runat="server" ZIndex="9999">
    </telerik:RadAjaxLoadingPanel>

            <telerik:RadWindowManager ID="rwmPedidoMng" runat="server" EnableShadow="true">
        <Windows>
             <telerik:RadWindow ID="rwPedidoLetrasMng" runat="server" Width="570px" Height="570px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>

            <telerik:RadWindow ID="rwPedidoMng" runat="server" Width="400px" Height="400px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>

            <telerik:RadWindow ID="rwCorreoMng" runat="server" Width="400px" Height="300px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Behaviors="Close, Move" Modal="true">
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="rapDocumento" runat="server" Width="100%">
        <div class="fila containerSubTitulo">
            <div class="colum12">
                <asp:Label ID="lblTituloGuia" runat="server" Text="Planificación de Letras" CssClass="subTitulo"></asp:Label>
            </div>
        </div>
        <div class="fila">
                <div class="colum6">
                    <telerik:RadTextBox Label="Letras:" ID="txtLetras" runat="server" ReadOnly="true" Width="100%" LabelWidth="13%" EmptyMessage="Compromiso de Fechas">
                    </telerik:RadTextBox>
                </div>

               <div class="colum2">
                <telerik:RadButton ID="btnLimpiar" runat="server"  OnClick="btnLimpiar_Click" Text="Limpiar">
                    <Icon PrimaryIconUrl="../../Images/Icons/trashcan-16.png" />
                </telerik:RadButton> 
               </div>

               <div class="colum2">
                <telerik:RadButton ID="btnAceptar" runat="server"  OnClick="btnAceptar_Click" Text="Aceptar">
                    <Icon PrimaryIconUrl="../../Images/Icons/aceptar-16.png" />
                </telerik:RadButton> 
               </div>
              

           </div>
        <div class="fila">
            <div class="colum12">
                 <telerik:RadCalendar 
                     Width="50%"
                     Skin="WebBlue"
                     RenderMode="Lightweight" 
                     runat="server" 
                     ID="cdLetras" 
                     AutoPostBack="true" 
                     MultiViewColumns="2"
                     MultiViewRows="2" RangeSelectionMode="OnKeyHold" EnableViewSelector="true" Height="50%" OnSelectionChanged="cdLetras_SelectionChanged" >
                      <CalendarTableStyle BorderStyle="Solid" BorderWidth="0px" Width="50%" />
                 </telerik:RadCalendar>
            </div>
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="mensaje" runat="server">
    <div class="fila">
        <div class="colum12">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
