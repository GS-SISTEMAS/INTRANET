<%@ Page Title="" Language="C#" MasterPageFile="~/Security/mstPopUpSX.Master" AutoEventWireup="true" CodeBehind="frmDetalleVenta.aspx.cs" Inherits="GS.SISGEGS.Web.Finanzas.Reportes.frmDetalleVenta" %>
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
    <telerik:RadAjaxManager ID="ramUsuarioMngEdit" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rapUsuarioMngEdit" LoadingPanelID="ralpUsuarioMngEdit"/>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="ralpUsuarioMngEdit" runat="server" ZIndex="9999" IsSticky="true"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="rapUsuarioMngEdit" runat="server" Width="100%">

               <div class="fila containerSubTitulo">
                  <div class="colum9">
                  <asp:Label ID="Label11" runat="server" Text="Vencido por periodo: " CssClass="subTitulo">
                  </asp:Label>
                 </div>
              </div>

           <div class="fila">
                <div class="colum2">
            </div>
               <div class="colum7">
                    <telerik:RadGrid ID="grdVencidos" runat="server" AllowMultiRowSelection="false"
                    AutoGenerateColumns="False" Height="220px"  Width="250px"  ShowFooter="true" 
                    AllowSorting="True" OnItemDataBound="grdVencidos_ItemDataBound1" >
                    <MasterTableView TableLayout="Fixed"
                        AllowMultiColumnSorting="true"    >
                        <Columns>

                            <telerik:GridBoundColumn DataField="anho" HeaderText="Año" HeaderStyle-Width="150px" AllowSorting="false" Visible="false"  >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="mes" HeaderText="Mes" HeaderStyle-Width="150px" AllowSorting="false" Visible="false" >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="diascredito" HeaderText="Días Credito" HeaderStyle-Width="150px" AllowSorting="false"  >
                            </telerik:GridBoundColumn>


                            <telerik:GridBoundColumn DataField="Venta" HeaderText="Venta" HeaderStyle-Width="80px" 
                                HeaderStyle-HorizontalAlign="Center" 
                                AllowSorting="true"    FooterStyle-HorizontalAlign="Left" Aggregate="Sum" DataFormatString="${0:#,##0.00}" >
                            
                              <ItemStyle  HorizontalAlign="Left"/> 
                             </telerik:GridBoundColumn>

                        </Columns>
                    </MasterTableView>

                     <ClientSettings>
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                    <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                </ClientSettings>
                    <GroupingSettings ShowUnGroupButton="false" /> 
                </telerik:RadGrid>

            </div>

           </div>
            
        
        <div class="fila">
            <div class="colum2">
                 <asp:HiddenField ID="HiddenField1" runat="server" />
                  <asp:HiddenField ID="HiddenField2" runat="server" />
                 <asp:HiddenField ID="HiddenField3" runat="server" />
            </div>
            <div class="colum7">
                <telerik:RadButton ID="btnCerrar" runat="server" Text="Guardar" OnClick="btnCerrar_Click" Visible="false">
                    <Icon PrimaryIconUrl="../../Images/Icons/floppy-16.png" />
                </telerik:RadButton>
                 <asp:HiddenField ID="lblClaveUsuario" runat="server" />
                  <asp:HiddenField ID="lblCodigoUsuario" runat="server" />
                 <asp:HiddenField ID="lblIdUsuario" runat="server" />
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
